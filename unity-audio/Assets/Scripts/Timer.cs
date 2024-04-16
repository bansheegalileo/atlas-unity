using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    public GameObject winCanvas;
    public TMP_Text finalTimeText;

    private float timer;

    private void Start()
    {
        timer = 0f;
    }

    private void Update()
    {
        if (enabled)
        {
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime;

        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = (timer % 60).ToString("00.00");

        timerText.text = $"{minutes}:{seconds}";
    }

    public void Win()
    {
        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = (timer % 60).ToString("00.00");

        finalTimeText.text = $"{minutes}:{seconds}";

        enabled = false;
    }
}
