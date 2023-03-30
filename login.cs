using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using Unity.Mathematics;
using UnityEditor.Compilation;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class login : MonoBehaviour
{

    public TMP_InputField UsernameInput;
    public TMP_InputField passwordInput;
    
    public Button loginButton;
    public TMP_Text loginButtonText;
    public GameObject currentPlayerObj;

    public void Login()
    {
        loginButton.interactable = false;
        if(UsernameInput.text.Length < 3)
        {

            ErrorOnLoginMessage("Check username");

        }else if(passwordInput.text.Length < 3)
        {
            ErrorOnLoginMessage("Check password");

        }
        else
        {
            Debug.Log("sending...");
            StartCoroutine(SendLoginForm());
        }

    }

    // if user is not found on database...

    public void ErrorOnLoginMessage(string message)
    {

        loginButton.GetComponent<Image>().color = Color.red;
        loginButtonText.text = message;



    }

    // if inputfield is pressed again...
    public void ResetLoginButton()
    {

        loginButton.GetComponent<Image>().color = Color.white;
        loginButtonText.text = "Login";
        loginButton.interactable = true;


    }

    IEnumerator SendLoginForm() // log in procedure 
    {
        //1. make new WWWForm
        //2. make new fields
        //3. send login request to php file
        //4. get response from php website
        
        // lets make random text for secret text
        // this way login security is better

        char[] random_characters = {'4','2','6','h','f','!','@','R','B','?','D'};
        char[] random_id = { '0','1', '2', '3', '4', '5', '6', '7', '8', '9',  };
        int count = random_characters.Length;
        
        int random = UnityEngine.Random.Range(0, 10); // return random number between...

        StringBuilder secret_password = new StringBuilder(); // empty string
        StringBuilder secret_id = new StringBuilder(); // empty string
        for (int val = 0; val < count; val++)
        {
            
            char character = random_characters[random]; // set random number to check array
            char character2 = random_id[random];
            secret_password.Append(character);     // add random character to a string
            secret_id.Append(character); // add random character to a string
        }
      string secret_string = secret_password.ToString(); // add to new variable...
      string id = secret_id.ToString();


        WWWForm LoginInfo = new WWWForm();                // make new form for login information
        LoginInfo.AddField("apppassword", secret_string); // and pass it to the field 
        LoginInfo.AddField("username", UsernameInput.text);
        LoginInfo.AddField("password", passwordInput.text);
        
        UnityWebRequest loginRequest = UnityWebRequest.Post("http://localhost/loginuser.php",LoginInfo);
        yield return loginRequest.SendWebRequest();

        if(loginRequest.error == null)
        {
            string response = loginRequest.downloadHandler.text;

            if(response == "logged in")
            {
                Debug.Log(response);

            }

            if(response == "login is not coming from unity app!")
            {

                Debug.Log(response);
                Errorbutton();

            }

            if(response == "failed to login")

            Debug.Log("form sent...");
            Instantiate(currentPlayerObj, new Vector3(0, 0, 0), Quaternion.identity);
        }


        

        else
        {

            Debug.Log(loginRequest.error);


        }

    }    

    void Errorbutton()
    {

        loginButton.image.color = Color.red;
        loginButtonText.text = "login failed";

    }


}
