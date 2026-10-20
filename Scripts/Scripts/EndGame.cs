using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private GameObject player;
    private GameObject buttonController;
    private CameraController cameraController;
    private PlayerMovement playerMovement;
    private TimerScript timerScript;
    private UIManager UIManager;
    private float prevTime;
    private int prevLength;
    private int prevWidth;
    private void Start()
    {

        player = GameObject.FindGameObjectWithTag("playerTag");
        buttonController = GameObject.FindGameObjectWithTag("ButtonController");
        cameraController = player.GetComponent<CameraController>();
        playerMovement = player.GetComponent<PlayerMovement>();
        UIManager = buttonController.GetComponent<UIManager>();
        
        timerScript = FindObjectOfType<TimerScript>();

        prevTime = PlayerPrefs.GetFloat("PreviousTime", 0f);
        prevLength = PlayerPrefs.GetInt("PreviousLength", 0);
        prevWidth = PlayerPrefs.GetInt("PreviousWidth", 0);
    }

    public void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Trigger entered");
        prevTime = timerScript.ReturnTimeValue();
        prevLength = UIManager.returnRows();
        prevWidth = UIManager.returnColumns();
        Debug.Log(prevTime);
        PlayerPrefs.SetFloat("PreviousTime", prevTime);
        PlayerPrefs.SetInt("PreviousLength", prevLength);
        PlayerPrefs.SetInt("PreviousWidth", prevWidth);
        timerScript.StopTimer();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        cameraController.cameraGameNotActive();

    }
}
