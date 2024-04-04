using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;
    private bool isFalling = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = transform.parent.GetComponent<CharacterController>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        bool isMoving = (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f);

        animator.SetBool("IsMoving", isMoving);

        bool isGrounded = characterController.isGrounded;
        animator.SetBool("IsGrounded", isGrounded);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
            isFalling = true;
        }

        if (!isGrounded && isFalling)
        {
            animator.SetBool("IsFalling", true);
        }
        else
        {
            animator.SetBool("IsFalling", false);
        }
    }

    public void OnFallingAnimationComplete()
    {
        isFalling = false;
    }
}
