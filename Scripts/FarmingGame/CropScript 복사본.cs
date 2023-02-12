using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropScript : MonoBehaviour
{
    public float growth;
    public int currentLevel = 0;
    public int quality = 6;
    public bool rotten = false;
    public bool fullyGrown = false;
    public DateManager M_DateManager;
    public TilledLand land;
    [SerializeField] Level[] growthModel;
    [SerializeField] float[] SeasonGrowSpeed = new float[4];
    float timer = 0.0f;
    int qualityDropCounter = 5;
    void Update(){
        if(timer>=1.0f){
            if(rotten) return;
            if(land.wet>0){
                GrowthUpdate(SeasonGrowSpeed[M_DateManager.season]);
                land.WetUpdate(-1);
            }
            else{
                qualityDropCounter--;
                if(qualityDropCounter<=0){
                    if(quality==0){
                        growthModel[currentLevel].model.SetActive(false);
                        growthModel[growthModel.Length-1].model.SetActive(true);
                        rotten = true;
                    }
                    else{
                        quality--;
                        qualityDropCounter = (7-quality)*10;
                    }
                }
            }
            timer = 0.0f;
        }
        timer += Time.deltaTime;
    }
    public void GrowthUpdate(float amount){
        growth+=amount;
        if(growth>=growthModel[currentLevel+1].reachGrowth){
            growthModel[currentLevel].model.SetActive(false);
            growthModel[currentLevel+1].model.SetActive(true);
            currentLevel++;
            if(currentLevel==growthModel.Length-2) fullyGrown = true;
            else if(currentLevel==growthModel.Length-1) rotten=true;
        }
    }
}
[System.Serializable] struct Level{
    public int reachGrowth;
    public GameObject model;
}