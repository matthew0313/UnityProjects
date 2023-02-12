using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildModeTrigger : MonoBehaviour
{
    [SerializeField] GameObject buildCam;
    [SerializeField] GameObject player;
    [Space(5)][Header("UI")][SerializeField] GameObject onButton;
    [SerializeField] GameObject offButton;

    [Space(5)][Header("Enabling Scripts")][SerializeField] BuildModeScript M_BuildModeScript;
    [SerializeField] FreeCamScript M_FreeCamScript;
    public void TurnOnBuild(){
        buildCam.Show();
        player.Hide();
         M_FreeCamScript.enabled = true;
        onButton.Hide();
        offButton.Show();
        M_BuildModeScript.enabled = true;
    }
    public void TurnOffBuild(){
        buildCam.Hide();
        M_FreeCamScript.enabled = false;
        player.Show();
        onButton.Show();
        offButton.Hide();
        M_BuildModeScript.End();
    }
}
