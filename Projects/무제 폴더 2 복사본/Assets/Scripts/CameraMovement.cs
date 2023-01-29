using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float Sensitivity;
    [SerializeField] int YReverse;
    [SerializeField] float moveSpeed;
    void Update(){
        if(Input.GetMouseButton(1)){
            Vector3 a = Camera.main.transform.eulerAngles;
            Camera.main.transform.eulerAngles = new Vector3(a.x+Input.GetAxis("Mouse Y")*Sensitivity*YReverse, a.y+Input.GetAxis("Mouse X")*Sensitivity, a.z);
        }
        Vector3 normalDir = Vector3.forward+Vector3.right;
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Camera.main.transform.Translate(moveSpeed*Vector3.Scale(normalDir, new Vector3(hor, 0.0f, ver)));
        /*Camera.main.transform.Translate(Vector3.forward*moveSpeed*Input.GetAxis("Vertical"));
        Camera.main.transform.Translate(Vector3.right*moveSpeed*Input.GetAxis("Horizontal"));*/
    }
}
