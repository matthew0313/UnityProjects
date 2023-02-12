using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateManager : MonoBehaviour
{
    public int season, month, date;
    float timer;
    void Update(){
        if(timer>=120.0f){
            date++;
            if(date>30){
                date = 0;
                month++;
                if(month==3) season = 1;
                else if(month==6) season = 2;
                else if(month==9) season = 3;
                else if(month==12) season = 4;
                else if(month>12) month = 1;
            }
        }
        timer += Time.deltaTime;
    }

}
