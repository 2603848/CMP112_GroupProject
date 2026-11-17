using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    //Movement
    public float walkSpeed = 5f;
    private float moveSpeed;
    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;

    public float groundDrag = 4f;
    public float airMultiplier = 0.5f;
    public float airAccel = 12f;

    //Ground Check
    public LayerMask groundLayer;
    public float playerHeight = 2f;
    public float playerRadius = 0.5f;

    public Transform orientation;
    private Rigidbody rb;

    //Movement State
    public enum MovementState { walking, air }
    public MovementState state;
    public bool grounded;

    //Input
    private float horizontalInput;
    private float verticalInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        //Get Movement Input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        //Check if the player is on the ground
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
        
        StateHandler();

        rb.linearDamping = grounded ? groundDrag : 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    #region Movement
    private void StateHandler()
    {
        //Apply a different speed / accelaration based on if the player is on the ground or in the air
        if (grounded) { state = MovementState.walking; desiredMoveSpeed = walkSpeed; }
        else { state = MovementState.air; }

        if (Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed != 0)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLerpMoveSpeed());
        }
        else moveSpeed = desiredMoveSpeed;
        lastDesiredMoveSpeed = desiredMoveSpeed;
    }

    private void MovePlayer()
    {
        //Calculate the move direction
        Vector3 moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        if (grounded)
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
        else
        {
            Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            Vector3 wishDir = moveDir.normalized;
            float maxAccel = airAccel;
            float dot = Vector3.Dot(flatVel.normalized, wishDir);
            if (flatVel.magnitude < moveSpeed || dot < 0.99f)
                rb.AddForce(wishDir * maxAccel, ForceMode.Acceleration);

            rb.AddForce(moveDir.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private IEnumerator SmoothlyLerpMoveSpeed() //Decently self explanatory, lerps the move speed over time so that instead of
                                                //snapping to the walking/running speed the player smoothly accelarates to it.
    {
        float time = 0f;
        float diff = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startVal = moveSpeed;

        while (time < diff)
        {   
            moveSpeed = Mathf.Lerp(startVal, desiredMoveSpeed, time / diff);
            time += Time.deltaTime * 5f;
            yield return null;
        }
        moveSpeed = desiredMoveSpeed;
    }
    #endregion
}