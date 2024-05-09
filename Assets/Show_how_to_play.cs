using UnityEngine;

public class Show_how_to_play : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    private bool isCanvasVisible = false; // Flag to track canvas visibility

    // Method called when the button is pressed
    public void ToggleHowToPlay()
    {
        isCanvasVisible = !isCanvasVisible; // Toggle the flag
        canvas.gameObject.SetActive(isCanvasVisible); // Set the canvas visibility based on the flag
    }
}
