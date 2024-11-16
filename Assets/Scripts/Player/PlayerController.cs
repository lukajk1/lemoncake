using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Camera mainCamera; 

    [SerializeField] private InputSystem_Actions actions;

    [SerializeField] private Rigidbody rb; 
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;

    private float sensitivity = 420f;
    private float xRotation;
    private float yRotation;

    private bool isGrounded;

    private InputAction move;
    private InputAction jump;

    private void Awake()
    {
        actions = new InputSystem_Actions(); 

        rb = player.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void OnEnable()
    {
        move = actions.Player.Move;
        move.Enable();

        jump = actions.Player.Jump;
        jump.Enable();

        jump.performed += OnJumpPerformed;
    }

    private void OnDisable()
    {
        move.Disable();

        jump.performed -= OnJumpPerformed;
        jump.Disable();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, .35f, groundLayer);
        Debug.Log(isGrounded);
    }

    private void Update()
    {
        Vector3 movement = DetermineMovementVector();
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);

        DetermineCamMovement();

    }
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (isGrounded) // possibly do a check to Mathf.Abs(rb.linearVelocity.y) < 0.01f but this could return true in cases where it shouldn't
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // Reset vertical velocity
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void DetermineCamMovement()
    {
        xRotation -= Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
        yRotation += Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // prevent looking above/below 90

        mainCamera.transform.localEulerAngles = new Vector3(xRotation, yRotation, 0);
        transform.localEulerAngles = new Vector3(xRotation, yRotation, 0);
    }
    private Vector3 DetermineMovementVector()
    {
        Vector2 moveDir = move.ReadValue<Vector2>().normalized * moveSpeed;

        // Calculate movement relative to the player's current rotation
        Vector3 forward = transform.forward * moveDir.y;
        Vector3 right = transform.right * moveDir.x;    

        Vector3 combined = forward + right;
        return new Vector3 (combined.x, 0, combined.z);
    }


}
