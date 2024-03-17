using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    public Animator rigController;
    private float jumpHeight;
    private float gravity;
    private float stepDown;
    private float airControl;
    private float jumpDamp;
    private float groundSpeed;
    private float pushPower;

    private Animator animator;
    private Vector2 userInput;
    private CharacterController playerController;
    private ActiveWeapon activeWeapon;
    private ReloadWeapon reloadWeapon;

    private Vector3 rootMotion;
    private Vector3 velocity;
    private bool isJumping;

    private int isSprintingParam = Animator.StringToHash("IsSprinting");

    private void Awake()
    {
        if (DataManager.HasInstance)
        {
            jumpHeight = DataManager.Instance.DataConfig.JumpHeight;
            gravity = DataManager.Instance.DataConfig.Gravity;
            stepDown = DataManager.Instance.DataConfig.StepDown;
            airControl = DataManager.Instance.DataConfig.AirControl;
            jumpDamp = DataManager.Instance.DataConfig.JumpDamp;
            groundSpeed = DataManager.Instance.DataConfig.GroundSpeed;
            pushPower = DataManager.Instance.DataConfig.PushPower;
        }
    }

    void Start()
    {
        
        animator = GetComponent<Animator>();
        playerController = GetComponent<CharacterController>();
        activeWeapon = GetComponent<ActiveWeapon>();
        reloadWeapon = GetComponent<ReloadWeapon>();
    }

    
    void Update()
    {
        
        GetInput();
        UpdateAnimation();

    }

    private void GetInput()
    {
        userInput.x = Input.GetAxis("Horizontal");
        userInput.y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        UpdateIsSprinting();
    }

    private bool IsSprinting()
    {
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        bool isFiring = activeWeapon.IsFiring();
        bool isReloading = reloadWeapon.isReloading;
        bool isChangingWeapon = activeWeapon.isChangingWeapon;
        return isSprinting && !isFiring && !isReloading && !isChangingWeapon;
    }

    private void UpdateIsSprinting()
    {
        bool isSprinting = IsSprinting();
        animator.SetBool(isSprintingParam, isSprinting);
        rigController.SetBool(isSprintingParam, isSprinting);
    }

    private void UpdateAnimation()
    {
       
        animator.SetFloat("InputX", userInput.x);
        animator.SetFloat("InputY", userInput.y);
    }

    private void OnAnimatorMove()
    {
        
        rootMotion += animator.deltaPosition;
    }

    private void FixedUpdate()
    {
        
        if (isJumping) // In Air State
        {
            UpdateInAir();
        }
        else // Ground State
        {
            UpdateOnGround();
        }
    }

    private void UpdateOnGround()
    {
       
        Vector3 stepForwardAmount = rootMotion * groundSpeed;
        Vector3 stepDownAmount = Vector3.down * stepDown;
        
        playerController.Move(stepForwardAmount + stepDownAmount);
        rootMotion = Vector3.zero;

        
        if (!playerController.isGrounded)
        {
            SetInAirVelocity(0);
        }
    }

    private void UpdateInAir()
    {
        
        velocity.y -= gravity * Time.fixedDeltaTime;
        Vector3 displacement = velocity * Time.fixedDeltaTime;
        displacement += CalculateAirControl();
        playerController.Move(displacement);
        isJumping = !playerController.isGrounded;
        rootMotion = Vector3.zero;
        animator.SetBool("IsJumping", isJumping);
    }

    private Vector3 CalculateAirControl()
    {
        
        return ((transform.forward * userInput.y) + (transform.right * userInput.x)) * (airControl / 100);
    }

    private void Jump()
    {
        
        if (!isJumping)
        {
            float jumpVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);
            SetInAirVelocity(jumpVelocity);
        }
    }

    private void SetInAirVelocity(float jumpVelocity)
    {
        
        isJumping = true;
        velocity = animator.velocity * jumpDamp * groundSpeed;
        velocity.y = jumpVelocity;
        animator.SetBool("IsJumping", true);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
        Rigidbody body = hit.collider.attachedRigidbody;

        
        if (body == null || body.isKinematic)
            return;

        
        if (hit.moveDirection.y < -0.3f)
            return;

       
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

       
        body.velocity = pushDir * pushPower;
    }

    public void OnFootStep()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_FOOTSTEP);
        }
    }

    public void OnJump()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_JUMP);
        }
    }
}

