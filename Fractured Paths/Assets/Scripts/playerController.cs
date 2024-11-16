// Written by Ahmed Ghazi
// Citing Sources for camera and player code: https://www.youtube.com/watch?v=c-R8sB2zDLQ

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rigidBody;
    public Transform head;
    public Camera newcamera;
    PauseMenu pauseMenu;

    [Header("Config")]
    public float speed;
    public float jumpSpeed;

    [Header("Runtime")]
    Vector3 newVelocity;
    bool isGrounded;
    bool isJumping;

    [Header("Audio")]
    public AudioSource audioWalk;


    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();

        //makes the cursor stay in the middle of the screen so the player can rotate
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseMenu != null && pauseMenu.isPaused)
        {
            return;
        }
        //enables horizontal rotation by taking mouse x input as rotation input
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 2f);
        newVelocity = Vector3.up * rigidBody.velocity.y;
        newVelocity.x = Input.GetAxis("Horizontal") * speed;
        newVelocity.z = Input.GetAxis("Vertical") * speed;

        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                newVelocity.y = jumpSpeed;
                isJumping = true;
            }
        }
        rigidBody.velocity = transform.TransformDirection(newVelocity);

        bool isMovingOnGround = ((Input.GetAxis("Vertical") != 0f || Input.GetAxis("Horizontal") != 0f) && isGrounded);

        //Audio
        if (pauseMenu != null && pauseMenu.isPaused)
        {
            audioWalk.enabled = false;
        }
        else
        {
            audioWalk.enabled = isMovingOnGround;
        }
    }
   void FixedUpdate()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1f))
            isGrounded = true;
        else isGrounded = false;
    }

    void LateUpdate()
    {
        if (pauseMenu != null && pauseMenu.isPaused)
        {
            return;
        }
        Vector3 e = head.eulerAngles;
        e.x -= Input.GetAxis("Mouse Y") * 2f;
        e.x = RestrictAngle(e.x, -85f, 85f);
        head.eulerAngles = e;
    }

    public static float RestrictAngle(float angle, float angleMin, float angleMax)
    {
        if (angle > 180) 
            angle -= 360;
        
        else if (angle < -180)
            angle += 360;

        if (angle > angleMax)
            angle = angleMax;
        if (angle < angleMin)
            angle = angleMin;

    return angle;
    }

    void OnCollisionStay(Collision col){
        isGrounded = true;
        isJumping = false;
    }

    void OnCollisionExit(Collision col){
        isGrounded = false;
        isJumping = true;
    }

   public void SetSpeed(float newSpeed)
   {
      speed = newSpeed;
      // audioWalk.pitch = newSpeed/2;
   }

   public void SetJumpSpeed(float newJumpSpeed)
   {
      jumpSpeed = newJumpSpeed;
   }

}

