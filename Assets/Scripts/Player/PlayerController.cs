using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 1f;
    public float gravity = 20f;
    public float pickupDistance;
    public LayerMask collectables;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    public Vector3 moveDirection = Vector3.zero;
    public Transform floatPoint;
    float rotationX = 0;

    public bool canMove = true;
    public bool inWater = false;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded)
        {
            inWater = false;
            
        }
        #region Handles player movement

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press LShift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float cursorSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float cursorSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float moveDirectionY = moveDirection.y;
        moveDirection = (forward * cursorSpeedX) + (right * cursorSpeedY);

        #endregion

        #region Handles jumping

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;

        }

        else
        {
            moveDirection.y = moveDirectionY;

        }

        if (!characterController.isGrounded && !inWater)
        {
            moveDirection.y -= gravity * Time.deltaTime;

        }
        else if (!characterController.isGrounded && inWater)
        {
            moveDirection.y = 0;
 
            //float newY = transform.position.y - floatPoint.position.y;
            
          //Vector3 fuck = new Vector3(transform.position.x, floatPoint.position.y, transform.position.z);
            //transform.position = Vector3.MoveTowards(transform.position,fuck,2);
        }
        #endregion

        #region Handles rotation and camera movement

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);


        }
        #endregion

       
    }
    public void Test(float y)
    {
        if (!characterController.isGrounded && inWater)
        {
            Vector3 fuck = new Vector3(transform.position.x, y, transform.position.z);
            transform.position = fuck;
        }
    }



}

