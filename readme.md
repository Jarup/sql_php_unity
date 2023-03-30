<h1>Unity käyttäjä tietokanta projekti</h1>
<p>Projektiin kuuluu:</p>
<ul>
<li>MySQL</li>
<li>Unity</li>
<li>MAMP</<li>
</ul>
https://www.mysql.com/<br>
https://unity.com/download<br>
https://www.w3schools.com/sql/default.asp
https://www.php.net/
<h2>C# koodaus</h2>
https://docs.unity3d.com/ScriptReference/

  <p>Projektin ideana on näyttää miten Unitysta viedään tiedot PHP tiedostolle ja sitä kautta SQL tietokantaan ja toisinpäin.</p>
  <p>Ideana on tallentaa käyttäjän tiedot tietokantaan ja salata salasanat Hash algoritmilla ja kertoa miten tietokanta toimii</p>
  
  
       WWWForms                     $_POST[""]                    TRUNCATE, ALTER TABLE, DROP TABLE    
       CreatePostRequest            INSERT_INTO                   PRIMARY KEY,FOREIGN KEY
      UNITY           ->            PHP             ->            SQL
      response        <-        SELECT * FROM table <-            INNER JOIN, SELECT * FROM
     loginRequest


<h2>MYSQL:n ja MAMP:in asennus</h2>
<p>MYSQLstä valitaan asennuksen yhteydessä perusasetukset ja samoin MAMPISSA.</p>

<p>Tämän jälkeen suunnataan phpadmin sivustolle</p>
<a href=http://localhost/phpMyAdmin>http://localhost/phpMyAdmin</a>

  1. Luodaan uusi tietokanta vasemmasta valikosta.
  2. Klikataan tietokantaa, jonka jälkeen avautuu uusi sivu.
  3. Uuden taulukon voi luoda joko SQL scriptillä tai alhaalta klikkaamalla tekstikenttää ja kirjoittamalla taulukon nimi ja sarakkeiden määrä.
  


<h3>PHP tiedostojen ja uuden tietokannan luominen</3>
<p>MAMPIN asennuksen jälkeen suunnataan sen document roottiin joka löytyy siis MAMP/htdocs/</p>
<p>Tämän jälkeen luodaan uusi tekstitiedosto joka muunnetaan muotoon php</p>
<p>Kaikkiin php sivuihin pääsee osoitteella localhost/tiedostonimi.php</p>

<h4>Jotta php sivu voi yhdistää ja käyttää tietokannan tietoja täytyy php tiedostoon luoda sql funktio joka yhdistää tietokantaan</h4>

<h3>Alhaalla on esimerkki siitä kun tiedot on lähetetty eteenpäin formista</h3>

```<?php
// $ on variaatio merkintä php kielessä jonka jälkeen lähdetään kirjoittamaan koodia.
// Mikäli jos php:koodissa on jotain väärin palauttaa php tiedosto virhe koodin 500.
//500: konditionaali jota ei voida käsitellä.
//404: tiedostoa ei löydy
//200: onnistuneesti ladattu
//304: Mitään ei ole muokattu sivulla tarkoittaa sitä että välimuistissa tallennettuja tietoja ei ole muokattu.
$con = mysqli_connect('localhost',root_user,rootpassword,database_name); // mikäli jos jokin parametreistä on väärin antaa sivusto virheeksi
                                                                         //Connect failed: Access denied for user 'root'@'localhost'
if(mysqli_connect_errno()){
echo("error! connection failed!");  // echo on responssi sivustolle jonka täytyy olla string siis teksti muodossa.

}
$email = $_POST["Email"];
$password = $_POST["password"]; // haetun arvon pitää olla nimeltään sama kuin formissa.
$username = $_POST["username]; // PHP käsittelee sivun formin tiedot $_POST funktiolla ja annetun datan pitää olla dictionary muodossa.
$usernameClean = filter_var($username, FILTER_SANITIZE_EMAIL); // PHPssä on valmiiksi sisään rakennettuja funktioita jotka siivoavat tekstistä ylimääräiset symbolit.
$EmailClean = filter_var($username, FILTER_SANITIZE_EMAIL);    // https://www.php.net/manual/en/filter.filters.sanitize.php ----> muita käytettäviä filttereitä.
                                                               // ylimääräiset symbolit kuten " tai ' voivat antaa pääsyn ulkopuoliselle siis hakkerille koodiin ja                                                                    // näin aiheuttaa tietomurron ja pääsyn tietokannan dataan.
$passhas = password_hash($password, PASSWORD_DEFAULT);         // password_hash on hashaus algoritmi joka kryptaa salasanan. Salasana näkyy tietokannassa randomeina                                                                  // symboleina ja kirjaimina sekä numeroina. SQL tietokannassa merkkimäärä 255 VARCHARISSA jotta                                                                        // erroreilta/bugeilta. PASSWORD_DEFAULT on yleinen algoritmi phpssa.

//SQL query
$usernamecheckquery = "SELECT username FROM users WHERE username = '".$usernameClean."';";
$usernamecheck = mysqli_query($con, $usernamecheckquery);

if(mysqli_num_rows($usernamecheck)>0){
echo("user already exists");
exit();
}

//SQL Query
$emailcheckquery = "SELECT email FROM users WHERE email = '".$emailClean."';";
$emailcheck = mysqli_query($con, $emailcheckquery);

if(mysqli_num_rows($emailcheck)>0){

    echo("5");
    exit();

}//Error Codes
//1 - Database connection Error
//2 - usernamecheck query run into an error
//3 - User already exists;


// '".$username."' -> when making new query in sql format


// our query code
$insertuserquery = "INSERT INTO users(username,email,password,user_id) VALUES('".$usernameClean."','".$emailClean."','".$passhas."','".$userid."');";
mysqli_query($con, $insertuserquery); // send information database
$insertuserquery2 = "INSERT INTO user_info(user_id) VALUES('".$userid."');";
mysqli_query($con, $insertuserquery2);



print("query completed");
$con->close(); // kun tietokannan tietoja on käsitelty täytyy yhteys tämän jälkeen sulkea.
<?
```


<h2>Unity sovelluksesta tietojen lähettäminen php tiedostolle/sivulle</h2>
```C#

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
```
