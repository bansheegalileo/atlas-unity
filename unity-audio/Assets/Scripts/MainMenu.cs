using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public AudioMixer mixer;
    private const string BGMVolumeKey = "BGMVolume";
    private float defaultBGMVolume = 0.8f;
    private void Start()
    {
        float savedBGMVolume = PlayerPrefs.GetFloat(BGMVolumeKey, defaultBGMVolume);
        ApplyBGMVolume(savedBGMVolume);
    }
    public void LevelSelect(int level)
    {
        PlayerPrefs.SetInt("ActivatePauseMenu", 0);
        switch (level)
        {
            case 1:
                SceneManager.LoadScene("Level01");
                break;
            case 2:
                SceneManager.LoadScene("Level02");
                break;
            case 3:
                SceneManager.LoadScene("Level03");
                break;
            default:
                Debug.LogError("that dont exist");
                break;
        }
    }

    public void Options()
    {
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Options");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exited");
    }
    public void ApplyBGMVolume(float volume)
    {
        mixer.SetFloat("BGMVolume", volume);
    }
}

