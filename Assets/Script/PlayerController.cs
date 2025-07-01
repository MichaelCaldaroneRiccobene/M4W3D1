using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speedWalk;
    [SerializeField] float speedLerp;
    [SerializeField] float jumpForce;
    [SerializeField] float coyoteTime = 0.1f;
    [SerializeField] int jumpsAir = 2;


    public Vector3 Direction;
    private Vector3 velocity;

    private float speedMove;
    private float getCojoteTime;
    private float x;
    private float z;
    private int getJumpsAir;
    public Rigidbody rb {  get; private set; }
    private GroundCheck groundCheck;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundCheck = GetComponentInChildren<GroundCheck>();
        animator = GetComponentInChildren<Animator>();

        getJumpsAir = jumpsAir;
        getCojoteTime = coyoteTime;
    }

    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        velocity = rb.velocity;
        velocity.x = speedMove;
        velocity.z = speedMove;

        Direction = new Vector3(x,0,z);


        bool isRun = Input.GetKey(KeyCode.LeftShift) ? true : false;
        speedMove = isRun ? speedWalk * 2 : speedWalk;

        if (groundCheck.IsOnGround())
        {
            jumpsAir = getJumpsAir;
            coyoteTime = getCojoteTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                coyoteTime = 0;
            }
        }
        else
        {
            if(coyoteTime > -0.01)
            {
                velocity.y = 0;
                coyoteTime -= 0.01f;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    coyoteTime = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) && jumpsAir > 0 && coyoteTime < 0)
            {
                jumpsAir--;
                velocity.y = 0;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        if(animator !=  null)
        {
            animator.SetFloat("Speed", Mathf.Clamp(rb.velocity.magnitude, 0, 1));
            animator.SetBool("isRun", isRun);
        }
        if (rb.velocity.magnitude > (speedMove / 100)) rb.velocity = rb.velocity.normalized * (speedMove / 100);
    }

    private void FixedUpdate()
    {
        //rb.MovePosition(rb.position + Direction.normalized * (speedMove * Time.fixedDeltaTime));
        if (Direction.sqrMagnitude > 0.1f) transform.forward = Vector3.Slerp(transform.forward, Direction, speedLerp * Time.fixedDeltaTime);
        rb.AddForce(Direction.normalized * speedMove * Time.fixedDeltaTime, ForceMode.Force);
    }
}
