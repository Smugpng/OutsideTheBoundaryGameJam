using UnityEngine;

public class ShipSteering : MonoBehaviour
{
    public Transform cameraTransform;
    public float interactionDistance = 3f;
    public LayerMask interactableLayer;
    public GameObject player;  // The player object to disable
    public GameObject ship;    // The ship object to control
    public GameObject shipWheel; // Reference to the ship's wheel object
    public float steeringSpeed = 50f;
    public float shipSpeed = 40f;

    private Camera playerCamera;
    public Camera shipCamera;
    private bool isSteering = false;

    private Vector3 frozenPlayerPosition;

    private Transform originalParent; // To store the original parent of the player


    private void Start()
    {
        playerCamera = cameraTransform.GetComponent<Camera>();

        originalParent = transform.parent;

        // Ensure the ship's camera is initially disabled
        if (shipCamera != null)
        {
            shipCamera.enabled = false;
        }
    }

    private void Update()
    {
        if (isSteering)
        {
            HandleSteering();
            HandleShipMovement();
            //FreezePlayerPosition();
        }
        else
        {
            CheckForWheelInteraction();
        }
    }

    private void CheckForWheelInteraction()
    {
        // Perform a raycast from the camera to detect if the player is looking at the wheel
        Ray ray = new(cameraTransform.position, cameraTransform.forward);
        Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.yellow);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, interactableLayer))
        {
            if (hit.collider.gameObject == shipWheel)
            {
                // Display UI or other indication that the player can interact
                // This is where you'd show a prompt like "Press E to steer the ship"
                Debug.Log("Press E to take the wheel");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    EnterSteeringMode();
                }
            }
        }
    }

    private void EnterSteeringMode()
    {
        isSteering = true;
        player.GetComponent<PlayerController>().enabled = false;

        transform.SetParent(ship.transform);
        // Switch to the ship's camera
        if (playerCamera != null)
        {
            playerCamera.enabled = false;
        }
        if (shipCamera != null)
        {
            shipCamera.enabled = true;
        }
        // Lock the cursor for steering mode
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void ExitSteeringMode()
    {
        Debug.Log("Exiting steering mode");
        isSteering = false;
        player.GetComponent<PlayerController>().enabled = true;

        transform.SetParent(originalParent);
        // Switch back to the player's camera
        if (playerCamera != null)
        {
            playerCamera.enabled = true;
        }
        if (shipCamera != null)
        {
            shipCamera.enabled = false;
        }
        // Unlock the cursor after exiting steering mode
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void HandleSteering()
    {
        float steeringInput = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            steeringInput = -1f; // Steer left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            steeringInput = 1f;  // Steer right
        }

        // Apply steering logic
        float rotation = steeringInput * steeringSpeed * Time.deltaTime;
        ship.transform.Rotate(0, rotation, 0);



        // Rotate the ship's wheel object visually
        shipWheel.transform.Rotate(0, 0, -rotation);

        // Exit steering mode with "E" again
        if (Input.GetKeyDown(KeyCode.E))
        {
            ExitSteeringMode();
        }
    }

    private void HandleShipMovement()
    {
        // Read input for moving the ship forward and backward using W and S keys
        float movementInput = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            movementInput = 1f; // Move forward
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movementInput = -1f; // Move backward
        }

        // Apply movement logic
        Vector3 movement = movementInput * shipSpeed * Time.deltaTime * ship.transform.forward;
        ship.transform.position += movement;
    }

    private void FreezePlayerPosition()
    {
        // Force the player's position to stay at the frozen position
        transform.position = frozenPlayerPosition;
    }
}
