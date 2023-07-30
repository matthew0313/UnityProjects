using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool freeCam = false;
    public GameObject player;
    public PlayerMovements M_PlayerMovements;
    public CutsceneManager M_CutsceneManager;
    private void Awake()
    {
        M_PlayerMovements = player.GetComponent<PlayerMovements>();
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else Instance = this;
    }
    private void Update()
    {
        if(!freeCam) Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10.0f);
    }
}
