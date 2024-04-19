using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject cameraControllerObject;
    private CameraController cameraController;
    public bool isPaused = false;
    public MuMan muMan;
    public AudioMixerSnapshot normalSnapshot;
    public AudioMixerSnapshot muffledSnapshot;

    void Start()
    {   
        cameraController = cameraControllerObject.GetComponent<CameraController>();
        if (PlayerPrefs.GetInt("ActivatePauseMenu", 0) == 1)
        {
            Pause();
        }
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;
        muffledSnapshot.TransitionTo(0f);
        Time.timeScale = 0f;
        pauseCanvas.SetActive(true);
        cameraController.enabled = false;
        Cursor.visible = true;
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
        cameraController.enabled = true;
        Cursor.visible = false;
        normalSnapshot.TransitionTo(0f);
    }

    public void Restart()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        PlayerPrefs.SetInt("ActivatePauseMenu", 0);
        normalSnapshot.TransitionTo(0.5f);
    }

    public void Options()
    {  
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt("ActivatePauseMenu", 1);
        SceneManager.LoadScene("Options");
    }

    public void MainMenu()
    {
        muMan.StopBGM();
        SceneManager.LoadScene("MainMenu");
    }
}
