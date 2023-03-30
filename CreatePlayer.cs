using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TMPro;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CreatePlayer : MonoBehaviour
{

    // Input fields for setting username,email and password
    public TMP_InputField usernameInput;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;

    public Button RegisterButton;
    public TMP_Text RegisterButtonText;



   public void registernewplayer() // some conditions for input fields
    {

        if(usernameInput.text == "")
        {

            ErrorMessage("username is not filled out");

        }
        else if(emailInput.text == "")
        {
            ErrorMessage("email is not filled out");

        }
        else if(passwordInput.text == "")
        {

            ErrorMessage("Password is not filled out");
        }
        else { Debug.Log("sending information...");
            SetButtonToSending();
            StartCoroutine(CreatePlayerPostRequest());
        
        }
    }


   
    public void ErrorMessage(string message) // error when input is not correct
    {
        RegisterButton.GetComponent<Image>().color = Color.red;
        RegisterButtonText.text = message; 
        RegisterButtonText.fontSize = 24;
      
    }

    // if there was an error and field was clicked again...
    public void ResetRegisterButton() // reset button to original state after clicking field input
    {

        RegisterButton.GetComponent<Image>().color = Color.white;
        RegisterButtonText.text = "Register";
        RegisterButtonText.fontSize = 24;

    }

    public void SetButtonToSending() // set register button text to sending...
    {

        RegisterButton.GetComponent<Image>().color = Color.gray;
        RegisterButtonText.text = "sending...";
        RegisterButtonText.fontSize = 20;


    }

    public void SetButtonSuccess() // successfuly sended information
    {

        RegisterButton.GetComponent<Image>().color = Color.green;
        RegisterButtonText.text = "Success";
        RegisterButtonText.fontSize = 20;

    }



  IEnumerator CreatePlayerPostRequest() // check new data and send informtation to php file and to database
    {

        char[] random_id = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', };
       
        StringBuilder secret_id = new StringBuilder(); // empty string
        for (int val = 0; val < 9; val++)
        {
            int random = UnityEngine.Random.Range(0, 10); // return random number between...
            char character2 = random_id[random];
            secret_id.Append(character2); // add random character to a string
        }
        string id = secret_id.ToString(); // give it to a new variable in string format

        WWWForm newplayerInfo = new WWWForm(); // make new input form...
        //newplayerInfo.AddField("apppassword", "thisisfromtheapp!");
        newplayerInfo.AddField("username", usernameInput.text);
        newplayerInfo.AddField("email", emailInput.text);
        newplayerInfo.AddField("password", passwordInput.text);
        newplayerInfo.AddField("userid", id); // set id to form
        UnityWebRequest CreatePostRequest = UnityWebRequest.Post("http://localhost/newplayer.php",newplayerInfo); // search php file 
        yield return CreatePostRequest.SendWebRequest();



        if(CreatePostRequest.error == null) // if there is no error...
        {
            // start sending information...
            Debug.Log("Good to go");
            string response = CreatePostRequest.downloadHandler.text;

            Debug.Log(response);
            // get response from website
            if(response == "1" || response == "2" || response == "4" || response == "6")
            {
                ErrorMessage("Server Error");


            }else if( response == "3")
            {
                ErrorMessage("Username already exists");
            }
               else if(response == "5")
            {

                ErrorMessage("Email Already Exists");

            }

           

            else
            {
                Debug.Log("Sending information...");
                SetButtonSuccess();
            }

           

        }


        else
        {
            Debug.Log(CreatePostRequest.error);

        }

    }

}
