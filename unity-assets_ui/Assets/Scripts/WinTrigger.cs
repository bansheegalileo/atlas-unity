using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public Timer timerScript;
    public GameObject winCanvas;

    private bool hasPlayerWon = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayerWon)
        {
            WinGame();
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
