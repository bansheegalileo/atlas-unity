using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text finalTimeText;

    private float startTime;
    private bool finished = false;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (!finished)
        {
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        float t = Time.time - startTime;
        string minutes = Mathf.Floor(t / 60).ToString("00");
        string seconds = (t % 60).ToString("00.00");

        timerText.text = $"{minutes}:{seconds}";
    }

    public void Win()
    {
        finished = true;
        finalTimeText.text = timerText.text;
    }
}
