using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float drag = 10f;
    public float acceleration = 50f;
    public float walkSpeed = 10f;
    public float sprintSpeed = 20f;
    public float airSpeed = 1f;
    public float jumpForce = 20f;

    [Header("Camera")]
    public float sensitivity = 2f;

    private Camera cam;
    private Rigidbody rb;
    private bool grounded = false;
    private float currSpeed;

    private Vector2 rotation = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        currSpeed = walkSpeed;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        grounded = rb.velocity.y == 0;

        UpdateSpeed();

        HandleJump();

        HandleMouseInput();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void UpdateSpeed()
    {
        currSpeed = grounded ? (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed) : airSpeed;
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void HandleMouseInput()
    {
        rotation.x += Input.GetAxis("Mouse X") * sensitivity;
        rotation.y -= Input.GetAxis("Mouse Y") * sensitivity;

        rotation.y = Mathf.Clamp(rotation.y, -90f, 90f);

        var xQuat = Quaternion.Euler(0, rotation.x, 0);
        var yQuat = Quaternion.Euler(rotation.y, 0, 0);

        cam.transform.localRotation = yQuat;
        transform.rotation = xQuat;
    }

    void HandleMovement()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(inputX, 0, inputY);
        Vector3 velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        movement.Normalize();
        movement *= acceleration * Time.fixedDeltaTime;

        if (velocity.magnitude < currSpeed)
            rb.AddRelativeForce(movement, ForceMode.VelocityChange);

        if (movement.magnitude < 0.1f && rb.velocity.magnitude > 0.1f)
            rb.AddForce(-(currSpeed * drag) * Time.fixedDeltaTime * velocity);
    }
}