using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject destroyedVariation;
    public bool isDestroying = false;
    private BackgroundScript scr_Background;
    private float MagnitudeToDestroy = 80f;

    bool IgnoreColliders;

    private void Start()
    {
        GameObject GmSetting = GameObject.Find("GameSetting");
        scr_Background = GmSetting.GetComponent<BackgroundScript>();
        MagnitudeToDestroy = GmSetting.GetComponent<GameSettings>().MagnitudeToDestroyBuildings;
        IgnoreColliders = false;
        Invoke("FlipIgnoreColliders", 3f);
    }

    private void FlipIgnoreColliders()
    {
        IgnoreColliders = !IgnoreColliders;
    }

    public void DestructThis()
    {
        isDestroying = true;

        bool isOnList = false;
        Transform t_parent = transform;

        if (transform.parent != null)
            t_parent = transform.parent;

        Debug.Log("Destroyed " + this.name);


        if (destroyedVariation != null)
        {
            GameObject newObj = Instantiate(destroyedVariation, t_parent.position, t_parent.rotation);
            newObj.transform.localScale = t_parent.lossyScale;
            isOnList = true;
            Destroy(t_parent.gameObject);
        }
        Destroy(t_parent.gameObject);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!isDestroying)
        {
            Vector3 tempVelocity = collision.gameObject.GetComponent<Rigidbody>().velocity;
            Debug.Log(this.gameObject + ": Checking if " + collision.gameObject + " will destroy me!");
            if (collision.gameObject.tag == "Player")
            {
                if (collision.gameObject.GetComponent<PlayerMovement>().isSuperSpeed)
                {
                    DestructThis();
                }
            }else if (tempVelocity.magnitude > MagnitudeToDestroy)
                DestructThis();
        }
    }

}
