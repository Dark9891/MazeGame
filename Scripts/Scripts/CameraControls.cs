using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 2.0f;
    private float rotationX = 0f;
    private float rotationY = 0f;
    private bool gameActive = false;
    private bool cursorLocked = false;

    void Update()
    {
        if (gameActive)
        {
            if (!cursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                cursorLocked = true;
            }


            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            rotationY += mouseX;
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -75f, 75f); 

            transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);

        }
        else
        {
            if (cursorLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void cameraGameActive()
    {
        gameActive = true;
    }
    public void cameraGameNotActive()
    {
        gameActive = false;
    }
    public void cameraGameToggle()
    {
        gameActive = !gameActive;
    }
    public void mouseToggle()
    {
        cursorLocked = !cursorLocked;
    }
}
