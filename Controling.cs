using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Controling : MonoBehaviour
{

    public float moveSpeed = 1.0f;
    public float runSpeed = 2.0f;
    public float jumpForce = 10.0f;
    private bool isJump = false;

    public float mouse_x = 10.0f;
    public float mouse_y = 10.0f;

    public float max_angle = 70.0f;
    public float min_angle = -60.0f;

    Landing Land = null;
    bool ifLanding = false;

    float angle = 0;

    public Transform cam = null;

    private Vector3 moving;



    Rigidbody rb = null;

    [SerializeField] float speed = 0.5f;

    public KeyCode runButton = KeyCode.LeftShift;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Land = GetComponentInChildren<Landing>();

    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            isJump = true;
            ifLanding = Land.Landig_return();

        }
        Vector2 mouseRotation = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        transform.rotation *= Quaternion.Euler(0, mouseRotation.x * mouse_x * Time.deltaTime, 0);

        angle = Mathf.Clamp(angle - mouseRotation.y * mouse_y * Time.deltaTime, -max_angle, -min_angle);

        cam.localRotation = Quaternion.Euler(angle, 0, 0);
    }

    private void FixedUpdate()
    {
        moving = Vector3.ClampMagnitude(moving, speed);
        moving = transform.rotation * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moving.x += rb.velocity.x;
        moving.z += rb.velocity.z;

        speed = moveSpeed;

        if (Input.GetKeyDown(runButton) == true)
        {
            speed = runSpeed;
        }



        if (isJump == true && ifLanding == true)
        {
            rb.AddForce(Vector3.up * jumpForce);
            isJump = false;
        }
        moving.y = rb.velocity.y;
        rb.velocity = moving;
    }
}
