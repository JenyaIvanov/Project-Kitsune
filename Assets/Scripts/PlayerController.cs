using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody rigidBody;
    public float moveSpeed;

    private Vector2 moveInput;

    public Animator spriteAnimator;
    public Animator headAnimator;
    public Animator playerAnimator;

    public SpriteRenderer spriteRenderer;
    public SpriteRenderer headRenderer;

    public float Temp_AttackCD_Time = 500;
    private float Temp_AttackCD = 0;

    public GameObject hitBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Temp_AttackCD <= 0) { 
            moveInput.x = Input.GetAxis("Horizontal");
            moveInput.y = Input.GetAxis("Vertical");

            moveInput.Normalize();

            rigidBody.velocity = new Vector3(moveInput.x * moveSpeed, rigidBody.velocity.y, moveInput.y * moveSpeed);

            spriteAnimator.SetFloat("movementSpeed", rigidBody.velocity.magnitude);
            headAnimator.SetFloat("moving", rigidBody.velocity.magnitude);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && Temp_AttackCD <= 0)
        {
            spriteAnimator.SetFloat("movementSpeed", 0);
            Temp_AttackCD = Temp_AttackCD_Time;
            spriteAnimator.SetTrigger("Attack");
            headAnimator.SetFloat("moving", moveSpeed);
        }

        // Change Direction (Flip) script.
        if(!spriteRenderer.flipX && moveInput.x > 0)
        {
            spriteRenderer.flipX = true;
            headRenderer.flipX = true;

            hitBox.transform.position += new Vector3(0.5f,0,0);


            playerAnimator.SetTrigger("Flip");

        } else if (spriteRenderer.flipX && moveInput.x < 0)
        {
            spriteRenderer.flipX = false;
            headRenderer.flipX = false;

            hitBox.transform.position += new Vector3(-0.5f, 0, 0);


            playerAnimator.SetTrigger("Flip");
        }

        if (Temp_AttackCD > 0)
        {
            Temp_AttackCD--;
        }

        if (Temp_AttackCD < 0)
        {
            Temp_AttackCD = 0;
        }

        //print("RB.V: " + rigidBody.velocity.magnitude);
    }

}
