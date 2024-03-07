using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float speed = 12f;

    [SerializeField]
    private float jumpHeight = 1f;

    [SerializeField]
    private bool canJump = true;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private float groundDistance = 0.4f;

    [SerializeField]
    private LayerMask groundMask;

    private Vector3 velocity;
    private float gravity = Physics.gravity.y * 3f;

    private bool onGround;

    private CharacterController controller;

    private void Awake() {
        controller = GetComponent<CharacterController>();
    }

    void Update() {
        onGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (onGround && velocity.y < 0) {
            velocity.y = -2f;
        }

        ConfigureInput();

        if (Input.GetKey(KeyCode.Space) && onGround && canJump) {
            Jump();
        }

        ApplyGravity();
        ConfigureTwist();
    }

    private void ConfigureInput() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z);
        if (move.magnitude > 1f) {
            move = move.normalized;
        }

        if (move.magnitude > 0) {
            SoundManager.PlaySound(SoundManager.Sound.step);
        }

        controller.Move(move * speed * Time.deltaTime);
    }

    private void ConfigureTwist() {
        Shader.SetGlobalFloat("_PlayerY", transform.position.y);
    }

    private void ApplyGravity() {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Jump() {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}
