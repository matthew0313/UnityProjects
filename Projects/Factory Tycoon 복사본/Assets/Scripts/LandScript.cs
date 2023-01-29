using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandScript : MonoBehaviour
{
    [SerializeField] GameManager GameManager;
    public bool Locked = false;
    public int Region;
    public LT Landtype;
    public Color32 Landcolor;
    public GameObject Structure = null;
    public Cord Coordinates;
    SpriteRenderer SpriteRenderer;
    Color OriginalColor;
    LT OriginalLandtype;
    void Start(){
        SpriteRenderer = transform.GetComponent<SpriteRenderer>();
        Landcolor = SpriteRenderer.color;
        if(Locked){
            OriginalLandtype = Landtype;
            Landtype = (LT)3;
            SpriteRenderer.color = new Color32(150, 0, 0, 255);
        }
    }
    public void Unlock(int a){
        if(Locked==false||a!=Region){
            return;
        }
        else{
            Landtype = OriginalLandtype;
            SpriteRenderer.color = Landcolor;
        }
    }
}
public enum LT{
    normal,
    copper,
    coal,
    unbuildable,
    iron,
    water,
    gold
}