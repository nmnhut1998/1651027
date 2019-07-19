using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement2 : MonoBehaviour
{
    // Start is called before the first frame update
    public float HP = 10.0f;
    public bool isSliding = false;
    public bool isJumping = false;
    public float llllll = 0.0f;
    public Rigidbody2D rigidBody2D;
    public float thrustForce = 0.2f;
    public Animator animator;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float mmmmm = 0;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidBody2D.AddForce(Vector2.left);
            //animator.SetFloat("Speed", 0.2f); 
            //transform.position.Set(transform.position.x + thrustForce, transform.position.y, transform.position.z);

            mmmmm = 2.0f;
        }
        else
            if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidBody2D.AddForce(Vector2.right);
            //animator.SetFloat("Speed", 0.2f); 
            //transform.position.Set(transform.position.x + thrustForce, transform.position.y, transform.position.z);

            mmmmm = 2.0f;
        }
        //else llllll = 0;

        animator.SetFloat("characterSpeed", mmmmm);
        animator.SetBool("isSliding", isSliding);
        animator.SetBool("isJumping", isJumping);
        animator.SetFloat("HP", HP);
    }
}
