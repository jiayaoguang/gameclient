using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class TipText : MonoBehaviour
{
    public float showOffCountDwon = 2;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.DontDestroyOnLoad(this.transform.root);
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Text>().text = msgObj.content;

        string textContent = text.text;
        if (textContent == null || textContent.Length == 0) {


            if (InstanceManager.instance.playerManager.tipQueue.Count == 0)
            {
                return;
            }
            else {
                text.text = InstanceManager.instance.playerManager.tipQueue.Dequeue();
                this.gameObject.SetActive(true);
                showOffCountDwon = 2;
            }
            
        }


        showOffCountDwon -= Time.deltaTime;

        if (showOffCountDwon > 0) {
            return;
        }
        text.text = "";
        this.gameObject.SetActive(false);
    }
}
