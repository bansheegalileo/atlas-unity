using UnityEngine;

public class FallingAnimationBehavior : StateMachineBehaviour
{
    private static PlayerAnim playerAnim;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerAnim == null)
        {
            playerAnim = GameObject.FindObjectOfType<PlayerAnim>();
            if (playerAnim == null)
            {
                Debug.LogWarning("PlayerAnim reference is not found in the scene.");
                return;
            }
        }

        playerAnim.PlayThumpSound();
    }
}
