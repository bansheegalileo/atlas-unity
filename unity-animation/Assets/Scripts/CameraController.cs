using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float sensitivity = 2f;
    public float distanceFromPlayer = 5f; // Distance between camera and player

    private float rotationX = 0f;
    private float rotationY = 0f; // Added rotationY for vertical camera movement
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
            // Set initial camera position relative to player
            Vector3 offset = -player.forward * distanceFromPlayer;
            transform.position = player.position + offset;

            // Look at the player
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

        rotationX += mouseX; // Added mouseX to rotationX
        rotationY -= mouseY; // Subtract mouseY from rotationY to invert vertical camera movement
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);

        // Rotate camera around player based on rotationX and rotationY
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0f);
        Vector3 offset = rotation * new Vector3(0f, 0f, -distanceFromPlayer);
        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }

    private void FollowPlayer()
    {
        Vector3 offset = transform.rotation * new Vector3(0f, 0f, -5f);
        transform.position = player.position + offset;
    }
}
