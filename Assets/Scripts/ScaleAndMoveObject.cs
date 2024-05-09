using UnityEngine;

public class ScaleAndMoveObject : MonoBehaviour
{
    public float scaleYSpeed = 0.1f; // Speed at which the object scales along the Y-axis
    public float moveYSpeed = 0.1f; // Speed at which the object moves along the Y-axis
    public ParticleSystem particleSystem; // Reference to the particle system
    private bool scaleAndMove = true; // Flag to control scaling and movement

    void Update()
    {
        if (scaleAndMove)
        {
            ScaleObject();
            MoveObject();
            UpdateParticleSystemPosition();
        }
    }

    void ScaleObject()
    {
        Vector3 scale = transform.localScale;
        scale.y += scaleYSpeed * Time.deltaTime;
        transform.localScale = scale;
    }

    void MoveObject()
    {
        Vector3 position = transform.position;
        position.y += moveYSpeed * Time.deltaTime;
        transform.position = position;
    }

    void UpdateParticleSystemPosition()
    {
        if (particleSystem != null)
        {
            Bounds colliderBounds = GetComponent<BoxCollider2D>().bounds;
            Vector3 topPosition = new Vector3(colliderBounds.center.x, colliderBounds.max.y, colliderBounds.center.z);
            particleSystem.transform.position = topPosition;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && scaleAndMove)
        {
            collision.gameObject.GetComponent<PlayerController>().FreezeMovement(); // Prevent player movement
            collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            Animator playerAnimator = collision.gameObject.GetComponent<Animator>();
            playerAnimator.SetBool("IsDie", true);
            playerAnimator.SetBool("IsRunning", false);
            playerAnimator.SetBool("IsJumping", false);



            scaleAndMove = false; // Stop scaling and movement


        }
    }

}

