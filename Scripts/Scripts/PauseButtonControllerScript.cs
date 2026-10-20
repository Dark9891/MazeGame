using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseButtonControllerScript : MonoBehaviour
{
    public Button ResetButton;
    public Button MainMenuButton;
    public Button ResumeButton;
    public UIManager uiManager;
    private CameraController cameraController;
    private TimerScript timerScript;
    public Canvas pauseCanvas;
    public GameObject Player;
    void Start()
    {
        ResetButton.onClick.AddListener(HandleResetButtonClick);
        MainMenuButton.onClick.AddListener(HandleMainMenuButtonClick);
        ResumeButton.onClick.AddListener(HandleResumeButtonClick);
        
        cameraController = Player.GetComponent<CameraController>();
        timerScript = FindObjectOfType<TimerScript>();
    }

    public void HandleResetButtonClick()
    {
        uiManager.playerToStart();
    }
    public void HandleMainMenuButtonClick()
    {
        timerScript.StopTimer();
        pauseCanvas.gameObject.SetActive(!pauseCanvas.gameObject.activeSelf);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        cameraController.cameraGameNotActive();
    }
    public void HandleResumeButtonClick()
    {
        uiManager.TogglePauseMenu();
        Cursor.visible = !Cursor.visible;
        if (!Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}

