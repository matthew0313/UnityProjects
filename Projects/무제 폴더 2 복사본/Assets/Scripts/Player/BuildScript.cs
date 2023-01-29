using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildScript : MonoBehaviour
{
    [SerializeField] float gridSize;
    [SerializeField] float yHeight;
    [SerializeField] float BuildReach;
    [SerializeField] GameObject Grid;
    [SerializeField] LayerMask[] WhatToHit;
    public bool BuildMode;
    Building BuildingObject;
    GameObject PlaceModel;
    PlaceModel PlaceModel_S;
    bool canBuild = false;
    [SerializeField] Building test;
    [SerializeField] float BuildAreaBound;
    [SerializeField] BuildingScan M_BuildingScan;
    void Awake(){

    }
    void Start(){
        StartBuild(test);
    }
    public void StartBuild(Building Object){
        BuildMode = true;
        Grid.SetActive(true);
        BuildingObject = Object;
        PlaceModel = Instantiate(BuildingObject.PlaceModel);
        PlaceModel_S = PlaceModel.GetComponent<PlaceModel>();
        M_BuildingScan.enabled = false;
    }
    void Update(){
        if(BuildMode){
            Ray h = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(h.origin, h.direction, out hit, BuildReach, WhatToHit[0])){
                float x = Mathf.Round(hit.point.x*(1.0f/gridSize))/(1.0f/gridSize);
                float z = Mathf.Round(hit.point.z*(1.0f/gridSize))/(1.0f/gridSize);
                PlaceModel.transform.position = new Vector3(x, yHeight, z);
            }
            else{
                PlaceModel.transform.position = h.origin+h.direction*BuildReach;
            }
            if(Input.GetKeyDown(KeyCode.R)){
                PlaceModel.transform.Rotate(new Vector3(0.0f, 90.0f, 0.0f), Space.World);
            }
            /*if(Input.GetKeyDown(KeyCode.T)){
                PlaceModel.transform.Rotate(new Vector3(90.0f, 0.0f, 0.0f), Space.World);
            }*/
            if(Input.GetKeyDown(KeyCode.UpArrow)){
                if(Input.GetKey(KeyCode.LeftShift)){
                    yHeight += gridSize*10.0f;
                }
                else yHeight += gridSize;
            }
            if(Input.GetKeyDown(KeyCode.DownArrow)){
                if(Input.GetKey(KeyCode.LeftShift)){
                    yHeight -= gridSize*10.0f;
                }
                else yHeight -= gridSize;
                if(yHeight<0.0f) yHeight = 0.0f;
            }
            if(Input.GetKeyDown(KeyCode.Q)){
                Destroy(PlaceModel);
                BuildMode = false;
                Grid.SetActive(false);
            }
            Grid.transform.position = new Vector3(0.0f, yHeight, 0.0f);
            if(Input.GetMouseButtonDown(0)){
                if(PlaceModel_S.canBeBuilt){
                    Instantiate(BuildingObject.Model, PlaceModel.transform.position, PlaceModel.transform.rotation);
                    if(!Input.GetKey(KeyCode.LeftShift)){
                        Destroy(PlaceModel);
                        BuildMode = false;
                        M_BuildingScan.enabled = true;
                        Grid.SetActive(false);
                    }
                }
            }
        }
    }
}
[System.Serializable] public struct Building{
    public GameObject Model;
    public GameObject PlaceModel;
}
