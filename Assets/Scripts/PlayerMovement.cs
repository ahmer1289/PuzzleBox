using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float mouseSensitivity = 2f;

    private CharacterController controller;
    public Transform playerCamera;
    private bool isGrounded;
    private float xRotation = 0f;
    private bool isUsingKeypad = false;
    private Vector3 velocity;
    public float gravity = 9.81f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!isUsingKeypad) 
        {
            MovePlayer();
            LookAround();
            CheckGround();

            
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
            }
        }

        
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal"); 
        float moveZ = Input.GetAxis("Vertical"); 

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); 

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

 
    void CheckGround()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            velocity.y = -2f; 
        }
    }

   
    public void StartUsingKeypad()
    {
        isUsingKeypad = true;
    }


    public void StopUsingKeypad()
    {
        isUsingKeypad = false;
    }

}
