using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamScript : MonoBehaviour
{
    [SerializeField] float Sensitivity;
    [SerializeField] int YReverse;
    [SerializeField] float moveSpeed;
    [SerializeField] Camera Camera;
    [Header("Repositioning Area Bounds")][SerializeField] float x1;
    [SerializeField] float x2;
    [SerializeField] float z1;
    [SerializeField] float z2;
    [Header("Repositioning Spot")][SerializeField] float x, y, z;
    void OnEnable(){
        if(Camera.transform.position.x<x1 || Camera.transform.position.x>x2 || Camera.transform.position.z<z1 || Camera.transform.position.z>z2){
            Camera.transform.position = new Vector3(x, y, z);
        }
    }
    void Update(){
        if(Input.GetMouseButton(1)){
            Vector3 a = Camera.transform.eulerAngles;
            Camera.transform.eulerAngles = new Vector3(a.x+Input.GetAxis("Mouse Y")*Sensitivity*YReverse, a.y+Input.GetAxis("Mouse X")*Sensitivity, a.z);
        }
        Vector3 normalDir = Vector3.forward+Vector3.right;
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Camera.transform.Translate(moveSpeed*Vector3.Scale(normalDir, new Vector3(hor, 0.0f, ver)));
    }
}
