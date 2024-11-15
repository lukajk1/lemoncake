using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAndCamMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Camera mainCamera; 
    [SerializeField] private InputSystem_Actions actions;

    [SerializeField] float moveSpeed;

    private float sensitivity = 420f;
    private float xRotation;
    private float yRotation;

    private InputAction move;

    private void Awake()
    {
        actions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        move = actions.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        player.transform.position += DetermineMovementVector() * Time.deltaTime;
        DetermineCamMovement();

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
        Vector3 forward = transform.forward * moveDir.y; // Move forward/backward along the z-axis
        Vector3 right = transform.right * moveDir.x;     // Move left/right along the x-axis

        Vector3 combined = forward + right;
        return new Vector3 (combined.x, 0, combined.z);
    }


}
