using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    public Timer timerScript;
    private bool isPlayerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isPlayerInside)
        {
            EnableTimer();
            isPlayerInside = false;
        }
    }

    private void EnableTimer()
    {
        if (timerScript != null)
        {
            timerScript.enabled = true;
        }
    }
}
