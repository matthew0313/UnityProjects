using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : Structure
{
    int MiningCooldown;
    [SerializeField] RectTransform ProgressBar;
    public int CoolDownTmp;
    [SerializeField] MineAble[] MineableOres;
    public int MineableCode;
    [SerializeField] bool CanMine = false;
    [SerializeField] GameObject CantMineText;
    void Start(){
        base.Start();
        for(int i = 0 ; i < MineableOres.Length ; i++){
            if(Land.Landtype == MineableOres[i].WhatToMine){
                CanMine = true; MineableCode = i; MiningCooldown = MineableOres[i].Cooldown;
                break;
            }
        }
        if(CanMine==false){
            CantMineText.SetActive(true);
        }
    }
    public override void TickUpdate(){
        if(CoolDownTmp<MiningCooldown){
            CoolDownTmp += 1;
            ProgressBar.localScale = new Vector3((float)CoolDownTmp/(float)MiningCooldown, 1, 1);
        }
        if(CoolDownTmp >= MiningCooldown){
            if(HeldItem==null && CanMine==true){
                HeldItem = Instantiate(MineableOres[MineableCode].MinedObject, transform.position, Quaternion.Euler(0,0,0));
                CoolDownTmp = 0;
                ProgressBar.localScale = new Vector3(0, 1, 1);
            }
        }
        if(HeldItem!=null){
            for(int i = 0 ; i < OutputDir.Length ; i++){
                if(Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure!=null && Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Structure>().HeldItem==null){
                    for(int k = 0 ; k < Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Structure>().InputDir.Length ; k++){
                        if(Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Structure>().InputPos[k].x == Position.x && Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Structure>().InputPos[k].y == Position.y){
                            if(Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Conveyor>()==null) Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Structure>().HeldItem = HeldItem;
                            else if(Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Conveyor>().QueueItem==null) Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Conveyor>().QueueItem = HeldItem;
                            else break;
                            HeldItem.transform.position = Manager.Land[OutputPos[i].x, OutputPos[i].y].Object.transform.position;
                            HeldItem = null;
                        }
                    }
                }
            }
        }
    }
}
[System.Serializable] public struct MineAble{
    public LT WhatToMine;
    public GameObject MinedObject;
    public int Cooldown;
}

