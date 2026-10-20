using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    public Button quitButtonHolder; 
    void Start()
    {
        quitButtonHolder.onClick.AddListener(HandleQuitButtonClick);
    }

    public void HandleQuitButtonClick()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
