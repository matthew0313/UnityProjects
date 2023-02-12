using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilledLand : MonoBehaviour
{
    public int wet = 0;
    public bool hasCrop = false;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] Material[] colors = new Material[10];
    public void WetUpdate(int amount){
        wet += amount;
        if(wet>100) wet = 100;
        for(int i = 9 ; i >= 0 ; i++){
            if(wet>i*10){
                mesh.material = colors[i];
                break;
            }
        }
    }
}
