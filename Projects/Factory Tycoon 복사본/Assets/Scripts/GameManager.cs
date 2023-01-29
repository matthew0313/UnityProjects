using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int Money = 0;
    public Text MoneyText;
    public List<GameObject> ResearchIndicator = new List<GameObject>();
    public int CurrentResearch;
    public int OpenedResearch = -1;
    public Research[] Research;
    [SerializeField] GameObject ResearchMenu;
    [SerializeField] Text ResearchText;
    [SerializeField] GameObject[] ResearchRequirementText;
    [SerializeField] Text ResearchUnlockText;
    [SerializeField] Text ResearchTierText;
    public GameObject[] ItemList;
    public List<Structure> BuiltObjects = new List<Structure>();
    public List<Structure> TickUpdate2Users = new List<Structure>();
    public List<Structure> TickUpdate3Users = new List<Structure>();
    [SerializeField] GameObject BuildMenu;
    [SerializeField] GameObject BuildIndicator;
    [SerializeField] GameObject[] BuildMenuStuff;
    [SerializeField] GameObject[] SmallerBuildMenu;
    [SerializeField] int MapSizeX;
    [SerializeField] int MapSizeY;
    [SerializeField] GameObject Map;
    public LandObject[,] Land = new LandObject[100, 100];
    [SerializeField] LayerMask WhatToHit;
    [SerializeField] PlayerScript PlayerScript;
    bool BuildMenuOn = false;
    public bool BuildMode = false;
    public GameObject BuildingStructure;
    public int BuildingCost = 0;
    public GameObject NullIdentifier;
    public int BuildRotation = 0;
    Transform Previous = null;
    int g = 0;
    void Start(){
        for(int i = 0 ; i < MapSizeX ; i++){
            for(int k = 0 ; k < MapSizeY ; k++){
                Land[i,k].Object = Map.transform.GetChild(i).GetChild(k).gameObject;
                Land[i,k].Script = Land[i,k].Object.GetComponent<LandScript>();
                Land[i,k].Script.Coordinates.x = i;
                Land[i,k].Script.Coordinates.y = k;
            }
        }
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            if(BuildMenuOn){
                BuildMenu.SetActive(false);
                PlayerScript.CanMove = true;
                BuildMenuOn = false;
            }
            else{
                if(BuildMode){
                    BuildIndicator.SetActive(false);
                    BuildMode = false;
                    BuildingStructure = null;
                }
                BuildMenu.SetActive(true);
                PlayerScript.CanMove = false;
                BuildMenuOn = true;
            }
        }
        if(Input.GetKeyDown(KeyCode.P)){
            if(CurrentResearch!=-1){
                Research[CurrentResearch].Researched = true;
                for(int k = 0 ; k < Research[CurrentResearch].Unlocks.Length ; k++){
                    Research[CurrentResearch].Unlocks[k].SetActive(true);
                }
                ResearchIndicator.Remove(Research[CurrentResearch].ResearchUI);
                Destroy(Research[CurrentResearch].ResearchUI);
                for(int k = 0 ; k < Research[CurrentResearch].UnlockingResearch.Length ; k++){
                    bool yea = true;
                    for(int o = 0 ; o < Research[CurrentResearch].UnlockingResearch[k].RequiredResearch.Length ; o++){
                        if(Research[Research[CurrentResearch].UnlockingResearch[k].RequiredResearch[o]].Researched == false){
                            yea = false;
                            break;
                        }
                    }
                    if(yea){
                        ResearchIndicator.Add(Research[Research[CurrentResearch].UnlockingResearch[k].UnlockingNumber].ResearchUI);
                        ResearchIndicator[ResearchIndicator.Count-1].transform.GetChild(0).GetComponent<Text>().text = Research[Research[CurrentResearch].UnlockingResearch[k].UnlockingNumber].ResearchName;
                        ResearchIndicator[ResearchIndicator.Count-1].SetActive(true);
                    }
                }
                if(Research[CurrentResearch].UnlockingRegion != 0){
                    for(int k = 0 ; k < MapSizeX ; k++){
                        for(int o = 0 ; o < MapSizeY ; o++){
                            Land[k,o].Script.Unlock(Research[CurrentResearch].UnlockingRegion);
                        }
                    }
                }
                UpdateResearchIndicators();
                CurrentResearch = -1;
            }
        }
        if(BuildMode){
            BuildIndicator.SetActive(true);
            RaycastHit hit;
            Ray cast = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1));
            Debug.DrawLine(cast.origin, cast.origin+15*new Vector3(0,0,1));
            if(Physics.Raycast(cast, out hit, 15.0f, WhatToHit)){
                if(Previous==null){
                    Previous = hit.transform;
                    if((int)hit.transform.GetComponent<LandScript>().Landtype != 3){
                        hit.transform.GetComponent<SpriteRenderer>().color = hit.transform.GetComponent<LandScript>().Landcolor + new Color(0, 0, 0.4f, 0);
                        BuildIndicator.transform.position = hit.transform.position;
                    }
                }
                else if(Previous!=hit.transform){
                    Previous.GetComponent<SpriteRenderer>().color = Previous.GetComponent<LandScript>().Landcolor;
                    Previous = hit.transform;
                    BuildIndicator.transform.position = hit.transform.position;
                    hit.transform.GetComponent<SpriteRenderer>().color = hit.transform.GetComponent<LandScript>().Landcolor + new Color(0, 0, 0.2f, 0);
                    
                }
                if(Input.GetMouseButtonDown(0) && (int)hit.transform.GetComponent<LandScript>().Landtype != 3 && Money>=BuildingCost){
                    if(hit.transform.GetComponent<LandScript>().Structure == null && BuildingStructure != NullIdentifier){
                        LandScript LS = hit.transform.GetComponent<LandScript>();
                        Previous.GetComponent<SpriteRenderer>().color = Previous.GetComponent<LandScript>().Landcolor;
                        LS.Structure = Instantiate(BuildingStructure, hit.transform.position, Quaternion.Euler(0, 0, -90.0f*BuildRotation));
                        BuiltObjects.Add(LS.Structure.GetComponent<Structure>());
                        if(LS.Structure.GetComponent<Structure>().Use2ndTickUpdate) TickUpdate2Users.Add(LS.Structure.GetComponent<Structure>());
                        if(LS.Structure.GetComponent<Structure>().Use3rdTickUpdate) TickUpdate3Users.Add(LS.Structure.GetComponent<Structure>());
                        BuiltObjects[BuiltObjects.Count-1].Land = LS;
                        BuiltObjects[BuiltObjects.Count-1].Rotation = BuildRotation;
                        BuiltObjects[BuiltObjects.Count-1].Manager = this;
                        Money-=BuildingCost;
                        MoneyTextUpdate();
                    }
                    else if(hit.transform.GetComponent<LandScript>().Structure != null && BuildingStructure == NullIdentifier){
                        LandScript LS = hit.transform.GetComponent<LandScript>();
                        Money += LS.Structure.GetComponent<Structure>().Price;
                        MoneyTextUpdate();
                        BuiltObjects.Remove(LS.Structure.GetComponent<Structure>());
                        if(LS.Structure.GetComponent<Structure>().Use2ndTickUpdate) TickUpdate2Users.Remove(LS.Structure.GetComponent<Structure>());
                        if(LS.Structure.GetComponent<Structure>().Use3rdTickUpdate) TickUpdate3Users.Remove(LS.Structure.GetComponent<Structure>());
                        Destroy(LS.Structure);
                    }
                }
            }
            else if(Previous!=null){
                Previous.GetComponent<SpriteRenderer>().color = Previous.GetComponent<LandScript>().Landcolor;
                Previous = null;
            }
            if(Input.GetKeyDown(KeyCode.R)){
                BuildRotation++;
                BuildIndicator.transform.Rotate(-90.0f);
                if(BuildRotation>3) BuildRotation = 0;
            }
        }

    }
    public void OpenSmallMenu(int Menu){
        for(int i = 0 ; i < BuildMenuStuff.Length ; i++){
            BuildMenuStuff[i].SetActive(false);
        }
        SmallerBuildMenu[Menu].SetActive(true);
    }
    public void CloseSmallMenu(int Menu){
        SmallerBuildMenu[Menu].SetActive(false);
        for(int i = 0 ; i < BuildMenuStuff.Length ; i++){
            BuildMenuStuff[i].SetActive(true);
        }
    }
    public void OpenResearchTab(int o){
        OpenedResearch = o;
        SmallerBuildMenu[5].SetActive(false);
        ResearchMenu.SetActive(true);
        ResearchText.text = Research[o].ResearchName;
        for(int i = 0 ; i < Research[o].Requirement.Length ; i++){
            ResearchRequirementText[i].SetActive(true);
        }
        ResearchUnlockText.text = "Unlocks: " + Research[o].UnlocksText;
        ResearchTierText.text = "Tier: " + Research[o].RequiredTier;
        UpdateResearchProgress(o);
    }
    public void CloseResearchTab(){
        OpenedResearch = -1;
        SmallerBuildMenu[5].SetActive(true);
        ResearchMenu.SetActive(false);
        for(int i = 0 ; i < 5 ; i++){
            ResearchRequirementText[i].SetActive(false);
        }
    }
    public void SetResearch(){
        CurrentResearch = OpenedResearch;
    }
    public void ResearchProgress(ItemList a, int tier){
        if(tier < Research[CurrentResearch].RequiredTier) return;
        for(int i = 0 ; i < Research[CurrentResearch].Requirement.Length ; i++){
            if(Research[CurrentResearch].Requirement[i].RequiredItem == a){
                Research[CurrentResearch].Requirement[i].AcquiredAmount++;
                if(OpenedResearch==CurrentResearch || OpenedResearch==-1) UpdateResearchProgress(CurrentResearch);
                if(Research[CurrentResearch].Requirement[i].AcquiredAmount >= Research[CurrentResearch].Requirement[i].RequiredAmount){
                    Research[CurrentResearch].Requirement[i].Acquired = true;
                    bool yes = true;
                    for(int k = 0 ; k < Research[CurrentResearch].Requirement.Length ; k++){
                        if(Research[CurrentResearch].Requirement[k].Acquired == false){
                            yes = false;
                            break;
                        }
                    }
                    if(yes){
                        Research[CurrentResearch].Researched = true;
                        for(int k = 0 ; k < Research[CurrentResearch].Unlocks.Length ; k++){
                            Research[CurrentResearch].Unlocks[k].SetActive(true);
                        }
                        ResearchIndicator.Remove(Research[CurrentResearch].ResearchUI);
                        Destroy(Research[CurrentResearch].ResearchUI);
                        for(int k = 0 ; k < Research[CurrentResearch].UnlockingResearch.Length ; k++){
                            bool yea = true;
                            for(int o = 0 ; o < Research[CurrentResearch].UnlockingResearch[k].RequiredResearch.Length ; o++){
                                if(Research[Research[CurrentResearch].UnlockingResearch[k].RequiredResearch[o]].Researched == false){
                                    yea = false;
                                    break;
                                }
                            }
                            if(yea){
                                ResearchIndicator.Add(Research[Research[CurrentResearch].UnlockingResearch[k].UnlockingNumber].ResearchUI);
                                ResearchIndicator[ResearchIndicator.Count-1].transform.GetChild(0).GetComponent<Text>().text = Research[Research[CurrentResearch].UnlockingResearch[k].UnlockingNumber].ResearchName;
                                ResearchIndicator[ResearchIndicator.Count-1].SetActive(true);
                            }
                        }
                        if(Research[CurrentResearch].UnlockingRegion != 0){
                            for(int k = 0 ; k < MapSizeX ; k++){
                                for(int o = 0 ; o < MapSizeY ; o++){
                                    Land[k,o].Script.Unlock(Research[CurrentResearch].UnlockingRegion);
                                }
                            }
                        }
                        UpdateResearchIndicators();
                        CurrentResearch = -1;
                    }
                }
                break;
            }
        }
    }
    public void UpdateResearchProgress(int o){
        for(int i = 0 ; i < Research[o].Requirement.Length ; i++){
            ResearchRequirementText[i].GetComponent<Text>().text = ItemList[(int)Research[o].Requirement[i].RequiredItem].name + ": " + Research[o].Requirement[i].AcquiredAmount + "/" + Research[o].Requirement[i].RequiredAmount; 
        }
    }
    public void UpdateResearchIndicators(){
        float e = 200.0f;
        for(int i = 0 ; i < ResearchIndicator.Count ; i++){
            ResearchIndicator[i].transform.localPosition = new Vector3(0, e, 0);
            e -= 75.0f;
        }
    }
    public void EnterBuildMenu(GameObject BuildingObject){
        BuildingStructure = BuildingObject;
        BuildMode = true;
        BuildMenu.SetActive(false);
        BuildMenuOn = false;
        BuildingCost = BuildingObject.GetComponent<Structure>().Price;
        PlayerScript.CanMove = true;
    }
    public void MoneyTextUpdate(){
        MoneyText.text = "Money: " + Money;
    }
}
[System.Serializable] public struct LandObject{
    public GameObject Object;
    public LandScript Script;
}
[System.Serializable] public struct Recipe{
    public Require[] Requirements;
    public ItemList ResultItem;
}
[System.Serializable] public struct Require{
    public ItemList TriggerItem;
    public float HeldAmount;
    public float RequiredAmount;
}
[System.Serializable] public struct Cord{
    public int x;
    public int y;
}
public enum Dir{
    Front, Right, Back, Left
}
[System.Serializable] public struct Research{
    public bool Researched;
    public int RequiredTier;
    public string ResearchName;
    public string UnlocksText;
    public int UnlockingRegion;
    public GameObject ResearchUI;
    public UnlockingResearch[] UnlockingResearch;
    public ResearchRequirement[] Requirement;
    public GameObject[] Unlocks;
}
[System.Serializable] public struct ResearchRequirement{
    public ItemList RequiredItem;
    public int RequiredAmount;
    public int AcquiredAmount;
    public bool Acquired;
}
[System.Serializable] public struct UnlockingResearch{
    public int UnlockingNumber;
    public int[] RequiredResearch;
}
