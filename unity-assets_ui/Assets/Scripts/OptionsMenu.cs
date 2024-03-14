using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    private string lastSceneName;

    void Start()
    {
        lastSceneName = PlayerPrefs.GetString("LastScene", "MainMenu");
    }

    public void Back()
    {
        SceneManager.LoadScene(lastSceneName);
    }
}
