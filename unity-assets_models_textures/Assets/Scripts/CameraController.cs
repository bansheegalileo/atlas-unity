using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float sensitivity = 2f;

    private float rotationX = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

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

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotate the player horizontally based on mouse X input
        player.Rotate(Vector3.up, mouseX, Space.World);

        // Rotate the camera vertically based on mouse Y input
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
