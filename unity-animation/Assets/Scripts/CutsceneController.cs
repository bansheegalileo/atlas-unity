using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public Animator cutsceneAnimator;
    public GameObject mainCamera;
    public GameObject player;
    public GameObject timerCanvas;
    public GameObject cutsceneCamera;


    private bool cutsceneFinished = false;

    private void Update()
    {
        if (cutsceneAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && !cutsceneFinished)
        {
            EndCutscene();
            cutsceneFinished = true;
        }
    }

    private void EndCutscene()
    {
        player.GetComponent<PlayerController>().enabled = true;
        timerCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
}
