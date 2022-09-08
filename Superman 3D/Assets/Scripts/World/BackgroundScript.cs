using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScript : MonoBehaviour
{
    public int maxRubble = 20;
    public GameObject MainMenu;
    public GameObject PlayerCam;

    Text txtCityHP;
    public float maxBuildings;

    public List<GameObject> list_Rubble;

    public List<GameObject> list_Buildings;
    // Start is called before the first frame update
    void Start()
    {
        PlayerCam.GetComponent<MouseView>().enabled = false;
        Cursor.lockState = CursorLockMode.Confined;

        txtCityHP = GameObject.Find("txt_CityHP").GetComponent<Text>();
        recountBuildings();
        maxBuildings = list_Buildings.Count;
        InvokeRepeating("updateHealth", 0f, 1f);
    }

    void updateHealth()
    {
        recountBuildings();
        updateText();
    }

    void updateText()
    {
        float x = list_Buildings.Count;
            
        float i = (x / maxBuildings) * 100f;

        txtCityHP.text = i.ToString() + "%";
        
           
    }

    void recountBuildings()
    {
        list_Buildings.Clear();
        foreach(GameObject i in GameObject.FindGameObjectsWithTag("Building"))
        {
            list_Buildings.Add(i);
        }
    }



    public bool CheckRubbleMax(int add)
    {
        searchForRubble();

        if (add + list_Rubble.Count >= maxRubble)
            return false;
        else
            return true;
    }

    public void RemoveOldestRubble()
    {
        Destroy(list_Rubble[0]);
        list_Rubble.RemoveAt(0);
    }

    void searchForRubble()
    {
        list_Rubble.Clear();
        foreach (GameObject i in GameObject.FindGameObjectsWithTag("Destructable"))
        {
            list_Rubble.Add(i);
        }
        foreach (GameObject i in GameObject.FindGameObjectsWithTag("Obliteratable"))
        {
            list_Rubble.Add(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainMenu.SetActive(true);
            PlayerCam.GetComponent<MouseView>().enabled = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
