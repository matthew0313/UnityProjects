using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Researcher : Structure
{
    [SerializeField] int tier = 0;
    [SerializeField] int Cooldown;
    [SerializeField] RectTransform ProgressBar;
    int CooldownTmp;

    public override void TickUpdate(){
        if(CooldownTmp<Cooldown){
            CooldownTmp++;
            ProgressBar.localScale = new Vector3((float)CooldownTmp/(float)Cooldown, 1, 1);
        }
        if(CooldownTmp>=Cooldown){
            if(HeldItem!=null){
                if(Manager.CurrentResearch != -1){
                    Manager.ResearchProgress(HeldItem.GetComponent<Item>().ItemCode, tier);
                }
                Destroy(HeldItem);
                CooldownTmp = 0;
                ProgressBar.localScale = new Vector3(0, 1, 1);
            }
        }
    }
}
