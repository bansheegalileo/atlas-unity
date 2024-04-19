using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
public class OptionsMenu : MonoBehaviour
{
    public Toggle InvertYToggle;
    private string lastSceneName;
    public Slider BGMSlider;
    public Slider SFXSSlider;
    public AudioMixer mixer;


    private const string BGMVolumeKey = "BGMVolume";
    private float defaultBGMVolume = 0.8f;

    void Start()
    {
        lastSceneName = PlayerPrefs.GetString("LastScene", "MainMenu");

        bool isInverted = PlayerPrefs.GetInt("InvertYAxis", 0) == 1;
        InvertYToggle.isOn = isInverted;
        float savedBGMVolume = PlayerPrefs.GetFloat(BGMVolumeKey, defaultBGMVolume);
        BGMSlider.value = savedBGMVolume;
        UpdateBGMVolume(savedBGMVolume);
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
    public void OnBGMSliderValueChanged(float value)
    {
        UpdateBGMVolume(value);
        SaveBGMVolume(value);
    }

    private void UpdateBGMVolume(float volume)
    {
        float volumeInDB = Mathf.Log10(volume) * 20;
        mixer.SetFloat("BGMVolume", volumeInDB);
    }

    private void SaveBGMVolume(float volume)
    {
        PlayerPrefs.SetFloat(BGMVolumeKey, volume);
        PlayerPrefs.Save();
    }
}
