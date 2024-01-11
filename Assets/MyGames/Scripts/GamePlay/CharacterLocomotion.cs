using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    private Animator animator;
    private Vector2 userInput;

    private bool isSprinting;
    private bool isJumping;
    private bool isGrounded;

    private CharacterController characterController;
    private float originalStepOffset;
    private float yForce;
    private float? lastGroundedTime;
    private float? jumpButtonPressTime;
    private float turnSpeed;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintingSpeed = 1.5f;
    [SerializeField] private float jumpForce = 3.0f;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float gravityMultiplier;
    [SerializeField] private float jumpButtonGracePeriod;
    

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;

        CharacterAiming characterAimingScript = FindObjectOfType<CharacterAiming>();
        if (characterAimingScript != null)
        {
            turnSpeed = characterAimingScript.turnSpeed;
        }
        else
        {
            Debug.LogError("Không tìm thấy đối tượng CharacterAiming trong scene.");
        }
    }

    void Update()
    {
        HandleInput();
        ApplyGravity();
        HandleJump();
        MoveCharacter();
    }

    private void HandleInput()
    {
        userInput.x = Input.GetAxis("Horizontal");
        userInput.y = Input.GetAxis("Vertical");
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        animator.SetFloat("InputX", userInput.x);
        animator.SetFloat("InputY", userInput.y);
    }

    private void ApplyGravity()
    {
        float gravity = Physics.gravity.y * gravityMultiplier;

        if (isJumping && yForce > 0 && !Input.GetButton("Jump"))
        {
            gravity *= 2;
        }

        yForce += gravity * Time.deltaTime;
    }

    private void HandleJump()
    {
        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            yForce = -0.5f;
            characterController.stepOffset = originalStepOffset;
            isGrounded = true;
            isJumping = false;

            if (Time.time - jumpButtonPressTime <= jumpButtonGracePeriod)
            {
                yForce = Mathf.Sqrt(jumpForce * -3.0f * Physics.gravity.y * gravityMultiplier);
                isJumping = true;
                jumpButtonPressTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0;
            isGrounded = false;

            if ((isJumping && yForce < 0) || yForce < -2)
            {
                // Xử lý Animation khi đang rơi
            }
        }
    }

    private void MoveCharacter()
    {
        if (!isGrounded)
        {
            Vector3 movementDirection = new Vector3(userInput.x, 0, userInput.y).normalized;

            float currentMoveSpeed = isSprinting ? sprintingSpeed : moveSpeed;

            // Kiểm tra nếu đang nhảy và có input di chuyển
            if (isJumping && movementDirection != Vector3.zero)
            {
                // Chuyển hướng nhân vật dựa trên hướng di chuyển
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
            }

            Vector3 forwardDirection = transform.forward;
            Vector3 velocity = forwardDirection * currentMoveSpeed;
            velocity.y = yForce;

            characterController.Move(velocity * Time.deltaTime);
        }
    }

    private void OnAnimatorMove()
    {
        if (isGrounded)
        {
            Vector3 velocity = animator.deltaPosition * moveSpeed;
            velocity.y = yForce * Time.deltaTime;

            characterController.Move(velocity);
        }
    }
}




