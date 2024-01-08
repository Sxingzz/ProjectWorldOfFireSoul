using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    private Animator animator;
    private Vector2 userInput;
    private bool isSprinting;
    private bool isJumping;
    private Rigidbody rb;


    [SerializeField]
    private float SprintingSpeed = 1.5f;
    [SerializeField]
    private float JumpForce = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Rigidbody rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        userInput.x = Input.GetAxis("Horizontal");
        userInput.y = Input.GetAxis("Vertical");

        isSprinting = Input.GetKey(KeyCode.LeftShift);
        isJumping = Input.GetKeyDown(KeyCode.Space);


        animator.SetFloat("InputX", userInput.x);
        animator.SetFloat("InputY", userInput.y);

        //print($"User Input: {userInput}");

        if (isSprinting)
        {
            //jump();
        }

    }
    void jump()
    {
        if ( rb != null )
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }

    //private void OnAnimatorMove()
    //{
    //    Vector3 position = animator.deltaPosition;

    //    position.y = 0;

 
    //    float speedMultiplier = isSprinting ? SprintingSpeed : 1.0f;
    //    Debug.Log(speedMultiplier);

    //    if ( isSprinting )
    //    {
    //        position *= speedMultiplier;
    //        transform.position = position;
    //    }
    //}
}
