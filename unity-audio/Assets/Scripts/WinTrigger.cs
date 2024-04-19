using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public Timer timerScript;
    public GameObject winCanvas;

    private bool hasPlayerWon = false;
    public MuMan muMan;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayerWon)
        {
            WinGame();
            muMan.StopBGM();
            muMan.Winsong();
        }
    }

    private void WinGame()
    {
        if (timerScript != null)
        {
            timerScript.Win();
        }

        if (winCanvas != null)
        {
            winCanvas.SetActive(true);
        }

        hasPlayerWon = true;
    }
}
