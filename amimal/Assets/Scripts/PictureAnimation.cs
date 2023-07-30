using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureAnimation : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer renderer;
    public PlayerMovements M_PlayerMovements;
    private void Update()
    {
        anim.SetBool("Grounded", M_PlayerMovements.grounded);
        anim.SetFloat("Velocity", M_PlayerMovements.rb.velocity.y);
        if (M_PlayerMovements.rb.velocity.x > 0) renderer.flipX = false;
        if (M_PlayerMovements.rb.velocity.x < 0) renderer.flipX = true;
    }
}
