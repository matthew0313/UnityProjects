using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public PlayerMovements M_PlayerMovements;
    public PlayerInfo M_PlayerInfo;
    public SpriteRenderer player;
    public float rise, midair;
    private void Awake()
    {
        M_PlayerMovements = GetComponent<PlayerMovements>();
        M_PlayerInfo = GetComponent<PlayerInfo>();
    }
    private void Update()
    {
        if (M_PlayerMovements.grounded)
        {
            player.sprite = Resources.Load("Images/Characters/" + M_PlayerInfo.animalType.ToString() + "/idle") as Sprite;
        }
        else
        {
            if(M_PlayerMovements.rb.velocity.y >= rise)
            {
                player.sprite = Resources.Load("Images/Characters/" + M_PlayerInfo.animalType.ToString() + "/rise") as Sprite;
            }
            else if (M_PlayerMovements.rb.velocity.y >= midair)
            {
                player.sprite = Resources.Load("Images/Characters/" + M_PlayerInfo.animalType.ToString() + "/midair") as Sprite;
            }
            else
            {
                player.sprite = Resources.Load("Images/Characters/" + M_PlayerInfo.animalType.ToString() + "/fall") as Sprite;
            }
        }
    }
}
