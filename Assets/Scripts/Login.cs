using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager
{
    // Start is called before the first frame update
    public void Init()
    {
        GameObject loginBtnGameObject = GameObject.Find("LoginButton");

        loginBtnGameObject.GetComponent<Button>().onClick.AddListener(SendLoginRequest);
    }

    private void SendLoginRequest()
    {
        GameObject nameInputField = GameObject.Find("NameInputField");
        GameObject passwdInputField = GameObject.Find("PasswdInputField");
        if (nameInputField == null || passwdInputField == null) {
            Debug.LogError("nameInputField == null || passwdInputField == null");
            return;

        }
        string name = nameInputField.GetComponent<InputField>().text;
        string password = passwdInputField.GetComponent<InputField>().text;

        msg.LoginRequestMsg loginRequest = new msg.LoginRequestMsg();
        loginRequest.name = name;
        loginRequest.password = password;

        InstanceManager.instance.netClient.Send(loginRequest);

        Debug.Log(" SendLoginRequest......... ");

    }

    // Update is called once per frame
    void Update()
    {

    }
}