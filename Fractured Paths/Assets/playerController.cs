using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rigidBody;
    public Transform head;
    public Camera camera;

    [Header("Config")]
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        //makes the cursor stay in the middle of the screen so the player can rotate
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //enables horizontal rotation by taking mouse x input as rotation input
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 2f);
    }

    void FixedUpdate()
    {
        Vector3 newVelocity = Vector3.up * rigidBody.velocity.y;
        newVelocity.x = Input.GetAxis("Horizontal") * speed;
        newVelocity.z = Input.GetAxis("Vertical") * speed;
        rigidBody.velocity = transform.TransformDirection(newVelocity);
    }

    void LateUpdate()
    {
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
}

