using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    Vector2 mouse;
    RaycastHit hit;
    float range = 1000.0f;
    LineRenderer lLine = new LineRenderer(), rLine = new LineRenderer();
    public GameObject LEye, REye;
    Ray ray;


     void Start()
    {
        lLine = init(LEye);
        rLine = init(REye);
    }

    LineRenderer init(GameObject eye)
    {
        LineRenderer line = new LineRenderer();
        line = eye.GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.startWidth = 0.1f;
        line.endWidth = 0.25f;

        return line;
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, range);
        //if (hit.transform.gameObject != null)

        //if (Physics.Raycast(ray, out hit, range))
        //{
        if (Input.GetMouseButtonDown(1))
        {
            InvokeRepeating("startLaser", 0f, 0.05f);
        }
        else if (Input.GetMouseButtonUp(1))
                Invoke("stopLaser", 0.1f);
        //}
    }

    void startLaser()
    {
        activateEye(lLine);
        activateEye(rLine);
    }

    void activateEye(LineRenderer line)
    {
        //line.enabled = true;
        //line.SetPosition(0, line.gameObject.transform.position);
        //line.SetPosition(1, (ray.direction * 10) - ray.origin);

        if (hit.transform.gameObject != null)
        {
            if (hit.transform.gameObject.tag != "Player")
            {
                line.enabled = true;
                line.SetPosition(0, line.gameObject.transform.position);
                line.SetPosition(1, hit.point + hit.normal);

                if (hit.transform.gameObject.tag == "Destructable")
                {
                    Destructable dScript = hit.transform.gameObject.GetComponent<Destructable>();
                    if (!dScript.isDestroying)
                        dScript.DestructThis();
                }
                else if (hit.transform.gameObject.name == "Debris")
                    Destroy(hit.transform.gameObject);
            }
        }

    }

    void stopLaser()
    {
        lLine.enabled = false;
        rLine.enabled = false;
        CancelInvoke();
    }
}
