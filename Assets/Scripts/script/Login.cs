using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        //GameObject loginBtnGameObject = GameObject.Find("LoginButton");

        this.gameObject.GetComponent<Button>().onClick.AddListener(SendLoginRequest);
    }





    public void SendLoginRequest()
    {

        try {
            GameObject nameInputField = GameObject.Find("NameInputField");
            GameObject passwdInputField = GameObject.Find("PasswdInputField");
            if (nameInputField == null || passwdInputField == null)
            {
                Debug.LogError("nameInputField == null || passwdInputField == null");
                return;

            }
            string name = nameInputField.GetComponent<InputField>().text;
            string password = passwdInputField.GetComponent<InputField>().text;

            msg.LoginRequestMsg loginRequest = new msg.LoginRequestMsg();
            loginRequest.name = name;
            loginRequest.password = password;

            InstanceManager.instance.netClient.Send(loginRequest);
            InstanceManager.instance.playerManager.tipQueue.Enqueue("SendLoginRequest.........");
        }
        catch (Exception e) {
            StreamWriter sw = null;
            try {
                sw = new StreamWriter("unityout.txt", true);
                sw.Write("message : " + e.Message);
                sw.Write(e.StackTrace);
            } catch (Exception ioe) {
                if (sw != null) { 
                    sw.Close();
                }
            }
            
            

        }

        


        


    }




    // Update is called once per frame
    void Update()
    {

    }
}