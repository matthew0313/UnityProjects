using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float CameraMoveSpeed;
    public bool CanMove = true;
    void Update(){
        if(CanMove){
            transform.position = new Vector3(transform.position.x + CameraMoveSpeed*Input.GetAxisRaw("Horizontal"), transform.position.y + CameraMoveSpeed*Input.GetAxisRaw("Vertical"), transform.position.z);
        }
    }
}
