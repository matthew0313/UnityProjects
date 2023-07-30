using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public Animator anim, playerAnim;
    public Text talkerText, talkText;
    public GameObject progressText;
    public float talkSpeed, talkWaitSpeed;
    public bool cutscene = false, talking = false, talkEnded = false, animating = false, allowProgress = false;
    GameObject player { get { return GameManager.Instance.player; } }
    int currentCutscene = 0, currentContent = 0, currentTalk = 0;
    IEnumerator talk = null;
    [SerializeField] public List<Cutscene> contents;
    CutsceneElement current { get { return contents[currentCutscene].element[currentContent]; } }
    private void Start()
    {
        CutsceneStart(0);
    }
    private void Update()
    {
        if (cutscene)
        {
            if (talking && Input.GetMouseButtonDown(0))
            {
                if (talkEnded)
                {
                    progressText.SetActive(false);
                    talkEnded = false;
                    currentTalk++;
                    if (currentTalk == current.talker.Length){
                        talkerText.gameObject.SetActive(false);
                        talkText.gameObject.SetActive(false);
                        anim.SetBool("Talk", false);
                        talking = false;
                        CutsceneProgress();
                    }
                    else
                    {
                        talk = Talk(current.talker[currentTalk], current.talkContent[currentTalk]);
                        StartCoroutine(talk);
                    }
                }
                else
                {
                    StopCoroutine(talk);
                    talkText.text = null;
                    for(int i = 0; i < current.talkContent[currentTalk].Length; i++)
                    {
                        if (current.talkContent[currentTalk][i] != '|')
                        {
                            talkText.text += current.talkContent[currentTalk][i];
                        }
                    }
                    talkEnded = true;
                    progressText.SetActive(true);
                }
            }
            if (animating && allowProgress && Input.GetMouseButtonDown(0))
            { 
                animating = false;
                allowProgress = false;
                progressText.SetActive(false);
                CutsceneProgress();
            }
        }
    }
    public void CutsceneStart(int cutsceneNum)
    {
        GameManager.Instance.M_PlayerMovements.canMove = false;
        GameManager.Instance.freeCam = true;
        cutscene = true;
        anim.SetBool("Cutscene", true);
        currentCutscene = cutsceneNum;
        currentContent = -1;
        CutsceneProgress();
    }
    public void CutsceneProgress()
    {
        currentContent++;
        if (currentContent == contents[currentCutscene].element.Length)
        {
            GameManager.Instance.M_PlayerMovements.canMove = true;
            GameManager.Instance.freeCam = false;
            anim.SetBool("Cutscene", false);
            cutscene = false;
        }
        else if (current.talk == true)
        {
            anim.SetBool("Talk", true);
        }
        else if(current.animationTrigger == true)
        {
            animating = true;
            if (!current.playerAnimationTrigger) anim.SetTrigger(current.triggerName);
            else playerAnim.SetTrigger(current.triggerName);
        }
    }
    public void talkOpenFinish()
    {
        currentTalk = 0;
        talkerText.gameObject.SetActive(true);
        talkText.gameObject.SetActive(true);
        talking = true;
        talk = Talk(current.talker[currentTalk], current.talkContent[currentTalk]);
        StartCoroutine(talk);
    }
    public void AllowCutsceneProgress()
    {
        if (current.autoSkip)
        {
            CutsceneProgress();
        }
        else
        {
            allowProgress = true;
            progressText.SetActive(true);
        }
    }
    IEnumerator Talk(string talker, string talkContent)
    {
        talkText.text = "";
        talkerText.text = talker;
        for(int i = 0; i < talkContent.Length; i++)
        {
            if (talkContent[i] == '|')
            {
                yield return new WaitForSeconds(talkWaitSpeed);
            }
            else
            {
                yield return new WaitForSeconds(talkSpeed);
                talkText.text += talkContent[i];
            }
        }
        talkEnded = true;
        progressText.SetActive(true);
    }
    [System.Serializable]
    public class CutsceneElement
    {
        public bool animationTrigger;
        public bool playerAnimationTrigger;
        public bool autoSkip;
        public string triggerName;
        public bool talk;
        public string[] talker, talkContent;
    }
    [System.Serializable]
    public class Cutscene
    {
        [SerializeField] public CutsceneElement[] element;
    }
}
