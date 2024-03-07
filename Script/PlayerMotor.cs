using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMotion : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 1f;
    private bool dashing = true;
    private float dashingPower = 20f;
    private float dashingTime = 0.3f;
    private float dashingCooldown = 0.75f;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        // Check for dashing input and execute Dash coroutine
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashing)
        {
            StartCoroutine(Dash());
        }
    }

    //receive input for InputManager.cs and apply them to character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

   private IEnumerator Dash() 
{
    dashing = false; 

    Vector3 dashDirection = transform.forward; 
    if (Input.GetKey(KeyCode.W)) {
        dashDirection = transform.forward;
    } else if (Input.GetKey(KeyCode.S)) {
        dashDirection = -transform.forward;
    } else if (Input.GetKey(KeyCode.A)) {
        dashDirection = -transform.right;
    } else if (Input.GetKey(KeyCode.D)) {
        dashDirection = transform.right;
    }
    playerVelocity = new Vector3(dashDirection.x * dashingPower, 0f, dashDirection.z * dashingPower); 
    yield return new WaitForSeconds(dashingTime);

    playerVelocity = Vector3.zero; 
    yield return new WaitForSeconds(dashingCooldown); 
    dashing = true; 
}


}