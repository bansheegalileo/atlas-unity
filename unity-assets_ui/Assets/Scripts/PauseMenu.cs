using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject cameraControllerObject;
    private CameraController cameraController;
    public bool isPaused = false;


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
    }

    public void Restart()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Resume();
    }

    public void Options()
    {  
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt("ActivatePauseMenu", 1);
        SceneManager.LoadScene("Options");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
