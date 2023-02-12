using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerActions : MonoBehaviour
{
    [SerializeField] PlayerAnimations M_PlayerAnimations;
    [SerializeField] DateManager M_DateManager;
    [SerializeField] LandGridManager M_LandGridManager;
    [SerializeField] PlayerInventory M_PlayerInventory;
    [SerializeField] GameObject TilledLandModel;
    public GameObject InstantiateModel;
    public UnityAction[] Actions = new UnityAction[10];
    public bool Acting;
    public List<StructureScript> StructureQueue = new List<StructureScript>();
    int gridX{
        get{
            return (int)transform.position.x/4;
        }
    }
    int gridY{
        get{
            return (int)transform.position.z/4;
        }
    }
    float landPosX{
        get{
            return (float)((int)transform.position.x/4*4+2);
        }
    }
    float landPosY{
        get{
            return (float)((int)transform.position.z/4*4+2);
        }
    }
    bool outOfBase{
        get{
            if(transform.position.x<0.0f || transform.position.x>196.0f || transform.position.z<0.0f || transform.position.z>196.0f) return true;
            else return false;
        }
    }
    void Start(){
        Actions[0] = Tilling;
        Actions[1] = SeedPlanting;
    }
    void Update(){
        if(Input.GetKeyDown((KeyCode)('f'))&&StructureQueue.Count!=0){
            
        }
    }
    public void Tilling(){
        if(outOfBase||M_LandGridManager.land[gridX,gridY].tilled==true){
            return;
        }
        Acting = true;
        M_PlayerAnimations.AnimationTilling();
    }
    public void AnimationTill(){
        GameObject a = Instantiate(TilledLandModel);
        a.transform.localPosition = new Vector3(landPosX, 0.75f, landPosY);
        M_LandGridManager.land[gridX,gridY].tilled = true;
        M_LandGridManager.land[gridX,gridY].tilled_script = a.GetComponent<TilledLand>();

    }
    public void SeedPlanting(){
        if(outOfBase||M_LandGridManager.land[gridX,gridY].tilled==false){
            return;
        }
        Acting = true;
        M_PlayerAnimations.AnimationSeedPlanting();
    }
    public void AnimationSeedPlant(){
        GameObject a = Instantiate(InstantiateModel);
        a.transform.localPosition = new Vector3(landPosX, 2.8f, landPosY);

        CropScript c = a.GetComponent<CropScript>();
        c.M_DateManager = M_DateManager;
        c.land = M_LandGridManager.land[gridX,gridY].tilled_script;
        InstantiateModel = null;
        M_PlayerInventory.Slots[M_PlayerInventory.CurrentSlot].Amount -= 1;
        if(M_PlayerInventory.Slots[M_PlayerInventory.CurrentSlot].Amount == 0){
            M_PlayerInventory.Unequip();
        }
    }
    public void ActionFinish(){
        Acting = false;
    }
}
