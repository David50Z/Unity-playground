using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovementScript : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController controller;
    public Transform cam;

    [SerializeField] public LayerMask playerMask;

    public float speed = 6f;

    float horizontal;
    float vertical;
    float targetAngle;

    Vector3 direction;
    private Vector3 velocity;

    private float angle;
    private float turnSmoothVelocity;
    private float turnSmoothTime = 0.1f;

    // Update is called once per frame
    void Update()
    {
         horizontal = Input.GetAxisRaw("Horizontal");
         vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            velocity = direction * speed;

            controller.Move(moveDir * speed * Time.deltaTime);

        } else
        {
            velocity = Vector3.zero;
        }
    }
}
