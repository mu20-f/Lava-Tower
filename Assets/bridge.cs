using UnityEngine;

public class bridge : MonoBehaviour
{
    PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.KeyCounter() == 4)
        {
            OpenBridge();
        }
    }
    public void OpenBridge()
    {
        GetComponent<Animator>().SetTrigger("OpenBridge");
    }
}
