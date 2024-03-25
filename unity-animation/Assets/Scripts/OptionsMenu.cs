using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle InvertYToggle;
    private string lastSceneName;

    void Start()
    {
        lastSceneName = PlayerPrefs.GetString("LastScene", "MainMenu");

        bool isInverted = PlayerPrefs.GetInt("InvertYAxis", 0) == 1;
        InvertYToggle.isOn = isInverted;
    }

    public void Apply()
    {
        int isInverted = InvertYToggle.isOn ? 1 : 0;
        PlayerPrefs.SetInt("InvertYAxis", isInverted);
        PlayerPrefs.Save();

        SceneManager.LoadScene(lastSceneName);
    }

    public void Back()
    {
        SceneManager.LoadScene(lastSceneName);
    }
}
