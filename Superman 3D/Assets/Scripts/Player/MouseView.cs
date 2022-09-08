using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseView : MonoBehaviour
{
    public float mSensitivity = 100f;
    
    public Transform PlayerBody;

    float xRotation = 0f;
    float CamSpeed;

    // Start is called before the first frame update
    void Start()
    {
        lockCamera();
        CamSpeed = PlayerBody.GetComponent<PlayerMovement>().tempSpeed;
    }

    public void lockCamera()
    {

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mSensitivity * Time.deltaTime;

        if (mouseX != 0 || mouseY != 0)
        {
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            PlayerBody.Rotate(Vector3.up * mouseX);

        }
        else
        {
            PlayerBody.Rotate(Vector3.up * 0f);
        }


        if(Input.mouseScrollDelta.y < 0 && transform.localPosition.z > -6)
        {
            transform.localPosition = transform.localPosition + Vector3.back;
        }else if(Input.mouseScrollDelta.y > 0 && transform.localPosition.z < -3)
        {
            transform.localPosition = transform.localPosition + Vector3.forward;

        }

        //CamSpeed = PlayerBody.GetComponent<PlayerMovement>().tempSpeed;
        //transform.position = Vector3.MoveTowards(transform.position, PlayerBody.transform.position + camPosition, CamSpeed);
    }
}
