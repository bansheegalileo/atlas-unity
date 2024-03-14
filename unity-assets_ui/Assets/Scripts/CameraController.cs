using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float sensitivity = 2f;

    private float rotationX = 0f;
    public PauseMenu pauseMenu; 
    public bool isInverted = false;

private void Start()
{
    LoadSettings();
    if (pauseMenu != null)
    {
        if (!pauseMenu.isPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    if (player != null)
    {
        transform.LookAt(player);
    }
}

    private void Update()
    {
        if (player != null)
        {
            RotateCamera();
            FollowPlayer();
        }
    }
    private void LoadSettings()
    {
        isInverted = PlayerPrefs.GetInt("InvertYAxis", 0) == 1;
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        if (isInverted)
        {
            mouseY *= -1;
        }

        player.Rotate(Vector3.up, mouseX, Space.World);

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.rotation = Quaternion.Euler(rotationX, player.eulerAngles.y, 0f);
    }

    private void FollowPlayer()
    {
        Vector3 offset = transform.rotation * new Vector3(0f, 0f, -5f);
        transform.position = player.position + offset;
    }
}
