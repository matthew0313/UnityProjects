using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] Animator Animator;
    public void AnimationEquippedState(bool a){
        Animator.SetBool("Equipped", a);
    }
    public void ForceToIdle(){
        Animator.SetTrigger("ForcedIdle");
    }
    public void AnimationTilling(){
        Animator.SetTrigger("Till");
    }
    public void AnimationSeedPlanting(){
        Animator.SetTrigger("SeedPlant");
    }
}
