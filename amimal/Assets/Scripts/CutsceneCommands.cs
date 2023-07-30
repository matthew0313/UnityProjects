using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneCommands : MonoBehaviour
{
    GameObject player { get { return GameManager.Instance.player; } }
    public void AllowCutsceneProgress()
    {
        GameManager.Instance.M_CutsceneManager.AllowCutsceneProgress();
    }
    public void SetPlayerPosX(float x)
    {
        player.transform.position = new Vector2(x, player.transform.position.y);
    }
    public void SetPlayerPosY(float y)
    {
        player.transform.position = new Vector2(player.transform.position.x, y);
    }
}
