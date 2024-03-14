using UnityEngine;
using TMPro;

public class WinTrigger : MonoBehaviour
{
    public Timer timerScript; // Assign Timer script in the Unity Editor
    public TMP_Text winText;

    public float increasedFontSize = 60f;
    public Color winColor = Color.green;

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
            timerScript.enabled = false; // Stop the timer
        }

        if (winText != null)
        {
            // Increase font size and change color
            winText.fontSize += increasedFontSize;
            winText.color = winColor;
        }

        hasPlayerWon = true;
    }
}
