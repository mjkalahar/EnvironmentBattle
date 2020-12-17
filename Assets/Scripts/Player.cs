using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float baseSpeed;
    public float runningAmplifier;

    public bool lockCursor;

    public GameObject pauseMenu;

    private CharacterController characterController;
    private Animator animator;

    private float airTime;
    private float gravity = -9.81f;

    private PlayerMovementInfo playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        playerMovement = new PlayerMovementInfo();
        playerMovement.baseSpeed = baseSpeed;
        playerMovement.runningAmplifier = runningAmplifier;

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        airTime = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Instantiate(pauseMenu);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        
        ProcessInput();

        PerformBlendTreeAnimation();

        CalculateDirectionAndDistance();
        PerformPhysicalMovement();

        RotatePlayerWithCamera();
    }

    public void ProcessInput()
    {
        playerMovement.leftAndRight = Input.GetAxis("Horizontal");
        playerMovement.forwardAndBackward = Input.GetAxis("Vertical");

        playerMovement.movingForwards = playerMovement.forwardAndBackward > 0.0f;
        playerMovement.movingBackwards = playerMovement.forwardAndBackward < 0.0f;

       //bool running = (playerMovement.movingForwards && Input.GetKey(KeyCode.LeftShift)) || (!playerMovement.movingBackwards && (playerMovement.leftAndRight > 0.0f || playerMovement.leftAndRight < 0.0f));

        bool running = Input.GetKey(KeyCode.LeftShift);

        if (running)
        {
            playerMovement.speed = playerMovement.baseSpeed * playerMovement.runningAmplifier;
        }
        else
        {
            playerMovement.speed = playerMovement.baseSpeed;

            playerMovement.forwardAndBackward = playerMovement.forwardAndBackward / 2.0f;
            playerMovement.leftAndRight = playerMovement.leftAndRight / 2.0f; 
        }
    }

    public void PerformBlendTreeAnimation()
    {
        float leftAndRight = playerMovement.leftAndRight;

        if (playerMovement.movingBackwards)
        {
            leftAndRight = 0.0f;
        }

        animator.SetFloat("leftAndRight", leftAndRight);
        animator.SetFloat("forwardAndBackward", playerMovement.forwardAndBackward);
    }

    public void CalculateDirectionAndDistance()
    {
        Vector3 moveDirectionForward = transform.forward * playerMovement.forwardAndBackward;
        Vector3 moveDirectionSide = transform.right * playerMovement.leftAndRight;

        playerMovement.direction = moveDirectionForward + moveDirectionSide;
        playerMovement.normalizedDirection = playerMovement.direction.normalized;

        playerMovement.distance = playerMovement.normalizedDirection * playerMovement.speed * Time.deltaTime;

        GroundPlayer();
    }

    public void PerformPhysicalMovement()
    {
        characterController.Move(playerMovement.distance);
    }

    public void GroundPlayer()
    {
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
    }

    public void RotatePlayerWithCamera()
    {
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
    }
}
