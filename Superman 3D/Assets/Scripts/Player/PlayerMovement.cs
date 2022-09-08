using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera PlayerCam;

    float gravity = -9.8f;

    public float speed = 12f, tempSpeed = 0f;
    public float jumpForce = 10f, ascentSpeed = 50f;
    public float flightMultiplier = 2, sSpeedMultiplier = 2f;

    public bool isFlying = false, isSuperSpeed = false;

    //public float groundDistance = 0.15f;
    //public Transform groundCheck;
    public LayerMask groundMask;

    Vector3 velocity;
    public Rigidbody rgbd;
    public GameObject obj_trail;
    bool touchingGround;

    bool CheckIfGrounded()
    {
        //    return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        return touchingGround;
    }

    void CheckFlightStatus()
    {
        if (CheckIfGrounded() && velocity.y <= 0)
        {
            Debug.Log("Landed");
            //velocity.y = -2f;
            isFlying = false;
        }
    }

    void InputFlightComparison()
    {
        if (CheckIfGrounded())
        {
            Debug.Log("Jumping Up");
            //velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            velocity.y = 100f;
        }
        else if (!isFlying)
        {
            Debug.Log("Starting to fly");
            velocity.y = 0f;
            isFlying = true;
        } else
        {
            if (Input.GetButton("Jump"))
            {
                transform.position += (Vector3.up * returnSpeed(ascentSpeed)) * Time.deltaTime;
                Debug.Log("Flying up");
            } else if (Input.GetKey(KeyCode.LeftControl))
            {
                transform.position -= (Vector3.up * returnSpeed(ascentSpeed / 2)) * Time.deltaTime;
                Debug.Log("Flying down");
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        CheckFlightStatus();

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = PlayerCam.transform.right * x + PlayerCam.transform.forward * z;
        rgbd.velocity = Vector3.zero;
        rgbd.angularVelocity = Vector3.zero;

        transform.position += (move * returnSpeed(speed) * Time.deltaTime);

        if (Input.GetButtonDown("Jump") || ((Input.GetButton("Jump") || Input.GetKey(KeyCode.LeftControl)) && isFlying))
        {
            InputFlightComparison();
            
        }

        isGravityActive(!isFlying);
        
    }

    void isGravityActive(bool active)
    {
        GetComponent<Rigidbody>().useGravity = active;
    }

    private float returnSpeed(float varSpeed)
    {
        tempSpeed = varSpeed;

        if (isFlying)
            tempSpeed *= flightMultiplier;


        if (Input.GetKey(KeyCode.LeftShift))
        {
            tempSpeed *= sSpeedMultiplier;
            isSuperSpeed = true;
            obj_trail.SetActive(true);
        }
        else
        {
            isSuperSpeed = false;
            obj_trail.SetActive(false);
        }

        return tempSpeed;
    }
    private void OnCollisionEnter(Collision hit)
    {         
        //if (hit.gameObject.tag == "Destructable" && isSuperSpeed)
        //    hit.gameObject.GetComponent<Destructable>().DestructThis();
        //else 
        if (hit.gameObject.name == "Debris" || hit.gameObject.layer == 9)
        {
            Rigidbody body = hit.collider.attachedRigidbody;

            // no rigidbody
            if (body == null || body.isKinematic)
            {
                return;
            }

            // We dont want to push objects below us
            //if (hit.transform.moveDirection.y < -0.3)
            //{
            //    return;
            //}

            // Calculate push direction from move direction,
            // we only push objects to the sides never up and down
            Vector3 pushDir = this.transform.forward;

            // If you know how fast your character is trying to move,
            // then you can also multiply the push velocity by that.

            // Apply the push
            body.velocity = pushDir * (speed * sSpeedMultiplier) * 10;
        } else
        {
            if (hit.gameObject.tag == "Ground")
                touchingGround = true;
            else
                touchingGround = false;

        }
    }

}
