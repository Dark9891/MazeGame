using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dropDownController : MonoBehaviour
{
    public UIManager uiManager; 

    public void HandleInputData(int val)
    {
        
        uiManager.SetSelectedValue(val);
    }
}
