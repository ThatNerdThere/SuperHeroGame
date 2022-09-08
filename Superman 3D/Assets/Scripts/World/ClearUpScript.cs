using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearUpScript : MonoBehaviour
{

    public float CleanUpDelay = 15f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Clearup", CleanUpDelay);
    }

    void Clearup()
    {
        Destroy(this.gameObject);
    }
}
