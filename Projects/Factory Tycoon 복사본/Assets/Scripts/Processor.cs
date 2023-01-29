using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Processor : Structure
{
    [SerializeField] Recipe Recipe;
    [SerializeField] GameObject OutputItem;
    [SerializeField] RectTransform ProgressBar;
    [SerializeField] int ProcessCooldown;
    int ProcessCooldownTmp = 0;
    void Update(){
        if(HeldItem!=null){
            bool used = false;
            for(int k = 0 ; k < Recipe.Requirements.Length ; k++){
                if(HeldItem.GetComponent<Item>().ItemCode == Recipe.Requirements[k].TriggerItem){
                    Recipe.Requirements[k].HeldAmount+=1.0f;
                    if(Recipe.Requirements[k].HeldAmount > 100.0f) Recipe.Requirements[k].HeldAmount = 100.0f;
                    used = true;
                    break;
                }
            }
            Destroy(HeldItem);
        }
    }
    void OnDestroy(){
        base.OnDestroy();
        if(OutputItem!=null) Destroy(OutputItem);
    }
    public override void TickUpdate(){
        if(ProcessCooldownTmp<ProcessCooldown){
            ProcessCooldownTmp++;
            ProgressBar.localScale = new Vector3((float)ProcessCooldownTmp/(float)ProcessCooldown, 1, 1);
        }
        if(ProcessCooldown<=ProcessCooldownTmp){
            bool canCraft = true;
            for(int i = 0 ; i < Recipe.Requirements.Length ; i++){
                if(Recipe.Requirements[i].HeldAmount < Recipe.Requirements[i].RequiredAmount){
                    canCraft = false;
                    break;
                }
            }
            if(canCraft && OutputItem==null){
                OutputItem = Instantiate(Manager.ItemList[(int)Recipe.ResultItem], transform.position, Quaternion.Euler(0,0,0));
                for(int i = 0 ; i < Recipe.Requirements.Length ; i++){
                    Recipe.Requirements[i].HeldAmount -= Recipe.Requirements[i].RequiredAmount;
                }
                ProcessCooldownTmp = 0;
                ProgressBar.localScale = new Vector3(0, 1, 1);
            }
        }
        if(OutputItem!=null){
            for(int i = 0 ; i < OutputDir.Length ; i++){
                if(Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure!=null && Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Structure>().HeldItem==null){
                    for(int k = 0 ; k < Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Structure>().InputDir.Length ; k++){
                        if(Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Structure>().InputPos[k].x == Position.x && Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Structure>().InputPos[k].y == Position.y){
                            if(Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Conveyor>()==null) Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Structure>().HeldItem = OutputItem;
                            else if(Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Conveyor>().QueueItem==null) Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Conveyor>().QueueItem = OutputItem;
                            else break;
                            OutputItem.transform.position = Manager.Land[OutputPos[i].x, OutputPos[i].y].Object.transform.position;
                            OutputItem = null;
                        }
                    }
                }
            }
        }
    }
}

