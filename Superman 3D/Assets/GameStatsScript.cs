using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatsScript : MonoBehaviour
{
    public bool AreStatsOn = true;

    public Text txtVelocity;
    public GameObject objPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        txtVelocity = GameObject.Find("txt_Velocity").GetComponent<Text>();
        if (objPlayer == null)
            objPlayer = GameObject.FindGameObjectWithTag("Player");

        //InvokeRepeating("velocityDetection", 0.1f, 0.1f);
    }

    void velocityDetection()
    {
        Vector3 temp = GameObject.Find("PlayerBody").GetComponent<CharacterController>().velocity;

        txtVelocity.text = (temp.magnitude.ToString());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
}
