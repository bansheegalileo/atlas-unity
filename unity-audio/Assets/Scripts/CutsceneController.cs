using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public Animator cutsceneAnimator;
    public GameObject mainCamera;
    public PlayerController playerController;
    public GameObject timerCanvas;

    private bool cutsceneFinished = false;
    public MuMan muMan;

    void Start()
    {
        mainCamera.SetActive(false);
        playerController.enabled = false;
        timerCanvas.SetActive(false);
        muMan.StartBGM();
    }

    void Update()
    {
        if (!cutsceneFinished && cutsceneAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !cutsceneAnimator.IsInTransition(0))
        {
            EndCutscene();
        }
    }

    private void EndCutscene()
    {
        mainCamera.SetActive(true);
        playerController.enabled = true;
        timerCanvas.SetActive(true);
        gameObject.SetActive(false);
        cutsceneFinished = true;
        enabled = false;
    }
}
