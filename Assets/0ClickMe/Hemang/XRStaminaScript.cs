using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class StaminaManager : MonoBehaviour
{
    public Slider staminaBar; // The UI slider for stamina
    public Image staminaBarFill; // Reference to the Image component of the stamina bar fill
    public Image StaminaOrb;
    public Color fullStaminaColor = Color.green; // Color when stamina is full
    public Color emptyStaminaColor = Color.red;  // Color when stamina is empty

    public float maxStamina = 100f; // Max stamina value
    public float currentStamina; // Current stamina
    public float staminaDrainRate = 10f; // Stamina drained per second when running
    public float staminaRegenRate = 5f; // Stamina regeneration rate per second when not running
    public float runSpeedMultiplier = 2f; // Speed multiplier when running
    public float normalSpeed = 5f; // Normal movement speed

    public CharacterController characterController; // CharacterController component for movement
    public Transform cameraTransform; // Reference to the player's camera (for direction)

    private XRInputActions inputActions;  // Auto-generated class from Input Action Asset
    private bool isRunning;
    private bool canRun = true; // Whether the player is allowed to run
    private Vector2 movementInput; // Input for movement

    void Awake()
    {
        // Initialize the input action asset
        inputActions = new XRInputActions();
    }

    void OnEnable()
    {
        // Enable the action map
        inputActions.PlayerActions.Enable();

        // Subscribe to the run action input
        inputActions.PlayerActions.Run.performed += OnRunPerformed;
        inputActions.PlayerActions.Run.canceled += OnRunCanceled;

        // Subscribe to movement input
        inputActions.PlayerActions.Move.performed += OnMovePerformed;
        inputActions.PlayerActions.Move.canceled += OnMoveCanceled;
    }

    void OnDisable()
    {
        // Unsubscribe from input actions to avoid memory leaks
        inputActions.PlayerActions.Run.performed -= OnRunPerformed;
        inputActions.PlayerActions.Run.canceled -= OnRunCanceled;

        inputActions.PlayerActions.Move.performed -= OnMovePerformed;
        inputActions.PlayerActions.Move.canceled -= OnMoveCanceled;

        // Disable the action map
        inputActions.PlayerActions.Disable();
    }

    void Start()
    {
        // Initialize stamina
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = currentStamina;
        staminaBarFill.color = fullStaminaColor; // Set the initial color to green
        StaminaOrb.color = fullStaminaColor;
    }

    void Update()
    {
        HandleStamina();
        UpdateStaminaBar();

        // Only move if there is movement input
        if (movementInput != Vector2.zero && canRun)
        {
            if (isRunning && currentStamina > 0)
            {
                Run();
            }
            else
            {
                Walk();
            }
        }
    }

    void HandleStamina()
    {
        // Drain stamina only when running and moving
        if (isRunning && currentStamina > 0 && movementInput != Vector2.zero)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
            if (currentStamina <= 0)
            {
                currentStamina = 0;
                StopRunning(); // Stop running if stamina is depleted
                canRun = false; // Disable running until stamina is fully regenerated
                staminaBarFill.color = emptyStaminaColor; // Turn stamina bar red
            }
        }
        else if ((!isRunning || movementInput == Vector2.zero) && currentStamina < maxStamina)
        {
            // Regenerate stamina when not running
            currentStamina += staminaRegenRate * Time.deltaTime;
            if (currentStamina >= maxStamina)
            {
                currentStamina = maxStamina;
                canRun = true; // Allow running again when stamina is fully regenerated
                staminaBarFill.color = fullStaminaColor; // Turn stamina bar green
            }
        }

        // Ensure the player moves at normal speed when stamina is depleted
        if (!canRun && movementInput != Vector2.zero)
        {
            Walk(); // Force walking when stamina is depleted
        }
    }

    // Movement with running speed
    void Run()
    {
        MoveCharacter(normalSpeed * runSpeedMultiplier);
    }

    // Movement with walking speed
    void Walk()
    {
        MoveCharacter(normalSpeed);
    }

    // General character movement method
    void MoveCharacter(float speed)
    {
        // Get the direction relative to the camera
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Flatten the movement on the horizontal plane
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // Calculate the movement direction based on input
        Vector3 moveDirection = (forward * movementInput.y + right * movementInput.x) * speed * Time.deltaTime;

        // Move the character
        characterController.Move(moveDirection);
    }

    void UpdateStaminaBar()
    {
        // Update the stamina bar UI
        staminaBar.value = currentStamina;
    }

    // Called when the "Run" button is pressed
    void OnRunPerformed(InputAction.CallbackContext context)
    {
        if (canRun) // Only allow running if stamina is usable
        {
            isRunning = true;
        }
    }

    // Called when the "Run" button is released
    void OnRunCanceled(InputAction.CallbackContext context)
    {
        isRunning = false;
    }

    // Called when movement input is received
    void OnMovePerformed(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    // Called when movement input is canceled (no input)
    void OnMoveCanceled(InputAction.CallbackContext context)
    {
        movementInput = Vector2.zero; // Reset movement input when there’s no input
    }

    void StopRunning()
    {
        isRunning = false;
    }
}
