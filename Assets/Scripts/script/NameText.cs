using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        GameObject nameText = GameObject.Find("NameText");


        nameText.GetComponent<Text>().text = "name : " + InstanceManager.instance.playerManager.myPlayerInfo.name;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
