using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float baseRotationSpeed = 200f;
    public float jumpRotationSpeedMultiplier = 2f;
    public GameObject playerModel;

    public float jumpForce = 8f;
    public float gravity = 20f;
    public float resetYPosition = -14.9f;

    private CharacterController characterController;
    private Vector3 moveDirection;
    private Vector3 startPosition;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        startPosition = transform.position;
    }

    private void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalMovement, 0f, verticalMovement);
        movement = transform.TransformDirection(movement);
        movement *= moveSpeed * Time.deltaTime;

        if (movement.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            float angleDifference = Quaternion.Angle(playerModel.transform.rotation, targetRotation);

            float rotationSpeed = baseRotationSpeed + (jumpRotationSpeedMultiplier * angleDifference);
            
            playerModel.transform.rotation = Quaternion.RotateTowards(playerModel.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (characterController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpForce;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(movement + moveDirection * Time.deltaTime);

        if (transform.position.y < resetYPosition)
        {
            ResetPlayer();
        }
    }

    public void ResetPlayer()
    {
        transform.position = startPosition;
        moveDirection = Vector3.zero;
    }
}
