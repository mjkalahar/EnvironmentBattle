using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public float baseSpeed;
    public float runningAmplifier;

    public bool lockCursor;

    private CharacterController characterController;
    private Animator animator;

    private float airTime;
    private float gravity = -9.81f;

    private PlayerMovementInfo playerMovement;

    Camera playerCamera;

    RectTransform hudRectTransform;


    void Start()
    {
        playerCamera = transform.Find("Camera").GetComponent<Camera>();
        SetupHUDBar();

        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        
        playerMovement = new PlayerMovementInfo();
        playerMovement.baseSpeed = baseSpeed;
        playerMovement.runningAmplifier = runningAmplifier;

        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        airTime = 0;
    }

    public Camera GetPlayerCamera()
    {
        return playerCamera;
    }

    public void SetupHUDBar()
    {
        HUD hud = GameManager.GetHUD();
        var foreground = hud.transform.Find("Canvas").transform.Find("HealthBar").transform.Find("Foreground");
        hudRectTransform = foreground.GetComponent<RectTransform>();
    }

    public void UpdateHUDBar()
    {
        hudRectTransform.sizeDelta = new Vector2(GetHP() * 2, hudRectTransform.sizeDelta.y);
    }

    public override void Update()
    {
        if (!isStunned() && !isDead())
        {
            playerMovement.leftAndRight = Input.GetAxis("Horizontal");
            playerMovement.forwardAndBackward = Input.GetAxis("Vertical");

            playerMovement.movingForwards = playerMovement.forwardAndBackward > 0.0f;
            playerMovement.movingBackwards = playerMovement.forwardAndBackward < 0.0f;

            bool running = (playerMovement.movingForwards && Input.GetKey(KeyCode.LeftShift))
                           || (!playerMovement.movingBackwards
                               &&
                               (playerMovement.leftAndRight > 0.0f || playerMovement.leftAndRight < 0.0f)
                            );
            if (running)
            {
                playerMovement.speed = playerMovement.baseSpeed * playerMovement.runningAmplifier;
            }
            else
            {
                playerMovement.speed = playerMovement.baseSpeed;
                playerMovement.forwardAndBackward = playerMovement.forwardAndBackward / 2.0f;
            }

          

            Vector3 moveDirectionForward = transform.forward * playerMovement.forwardAndBackward;
            Vector3 moveDirectionSide = transform.right * playerMovement.leftAndRight;

            playerMovement.direction = moveDirectionForward + moveDirectionSide;
            playerMovement.normalizedDirection = playerMovement.direction.normalized;

            playerMovement.distance = playerMovement.normalizedDirection * playerMovement.speed * Time.deltaTime;

            if (characterController.isGrounded)
            {
                airTime = 0;
            }
            else
            {
                airTime += Time.deltaTime;
                Vector3 direction = playerMovement.normalizedDirection;

                direction.y += 0.5f * gravity * airTime;

                playerMovement.normalizedDirection = direction;
                playerMovement.distance = playerMovement.normalizedDirection * airTime;
            }

            characterController.Move(playerMovement.distance);
        }
        else
        {
            playerMovement.leftAndRight = 0.0f;
            playerMovement.forwardAndBackward = 0.0f;
            playerMovement.movingForwards = false;
            playerMovement.movingBackwards = false;
            playerMovement.speed = 0;
            playerMovement.direction = Vector3.zero;
            playerMovement.normalizedDirection = Vector3.zero;
            playerMovement.distance = Vector3.zero;

        }

        float leftAndRight = playerMovement.leftAndRight;

        if (playerMovement.movingBackwards)
        {
            leftAndRight = 0.0f;
        }

        animator.SetFloat("leftAndRight", leftAndRight);
        animator.SetFloat("forwardAndBackward", playerMovement.forwardAndBackward);


        Vector3 rotation;

        if (Input.GetKey(KeyCode.T))
        {
            return;
        }
        else
        {
            rotation = Camera.main.transform.eulerAngles;
            rotation.x = 0;
            rotation.z = 0;

            transform.eulerAngles = rotation;
        }

        UpdateHUDBar();
    }
}
