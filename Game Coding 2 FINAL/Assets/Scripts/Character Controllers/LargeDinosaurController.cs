using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LargeDinosaurController : MonoBehaviour
{
    //Moves the player through moving the hips
    private Rigidbody hipsRB;

    [Header("Movement Controls")]
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float strafeSpeed;
    [SerializeField] private float jumpStrength;

    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;

    [Header("GroundCheck")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundLayer;

    public static event Action<bool> StartedMoving;
    public static event Action<Vector3> MoveDirection;
    private bool isMoving;
    //Used for the current direction the camera is facing

    private Vector2 lookInput;
    public static event Action<Vector2> movedMouse;
    private Vector2 moveInput;

    private bool isGrounded = false;
    void Start()
    {
        hipsRB = GetComponent<Rigidbody>();
        StartedMoving?.Invoke(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        Move();

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void Move()
    {
        Vector3 nextMove = ((cameraTransform.forward * moveInput.y * forwardSpeed) + (cameraTransform.right * moveInput.x * forwardSpeed));

        if (!isMoving && moveInput.magnitude > 0.1)
        {
            isMoving = true;
            StartedMoving?.Invoke(true);
        }
        else if (isMoving && moveInput.magnitude < 0.1)
        {
            isMoving = false;
            StartedMoving?.Invoke(false);
        }

        if (isMoving) MoveDirection?.Invoke(nextMove.normalized);
        hipsRB.AddForce(nextMove);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            hipsRB.AddForce(new Vector3(0, jumpStrength, 0), ForceMode.Impulse);
        }
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
        movedMouse?.Invoke(lookInput);

    }
    private void CheckGround()
    {
        if (groundCheck == null)
        {
            isGrounded = false;
            return;
        }

        groundCheck.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z);

        isGrounded = Physics.SphereCast(groundCheck.position, groundCheckRadius, Vector3.down,
            out RaycastHit hit, groundCheckDistance, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 end = groundCheck.position + Vector3.down * groundCheckDistance;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(end, groundCheckRadius);
    }
}
