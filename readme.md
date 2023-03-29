<h1>Unity login projekti</h1>
<p>Projektiin kuuluu:</p>
<ul>
<li>MySQL</li>
<li>Unity</li>
<li>MAMP</<li>
</ul>
https://www.mysql.com/<br>
https://unity.com/download<br>
https://www.w3schools.com/sql/default.asp
<h2>C# koodaus</h2>
https://docs.unity3d.com/ScriptReference/

  <p>Projektin ideana on näyttää miten Unitysta viedään tiedot PHP tiedostolle ja sitä kautta SQL tietokantaan ja toisinpäin.</p>
  <p>Ideana on tallentaa käyttäjän tiedot tietokantaan ja salata salasanat Hash algoritmilla ja vähän kertoa tietoturvasta</p>
  
  
       WWWForms                     $_POST[""]                        
       CreatePostRequest            INSERT_INTO
      UNITY           ->            PHP             ->            SQL
      response        <-        SELECT * FROM table <-
     loginRequest


<h2>MYSQL:n ja MAMP:in asennus</h2>
<p>MYSQLstä valitaan asennuksen yhteydessä perusasetukset ja samoin MAMPISSA.</p>

<h3>PHP tiedostojen ja uuden tietokannan luominen</3>
<p>MAMPIN asennuksen jälkeen suunnataan sen document roottiin joka löytyy siis MAMP/htdocs/</p>
<p>Tämän jälkeen luodaan uusi tekstitiedosto joka muunnetaan muotoon php</p>


<h4>Jotta php sivu voi yhdistää ja käyttää tietokannan tietoja täytyy php tiedostoon luoda sql funktio joka yhdistää tietokantaan</h4>


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
$EmailClean = filter_var($username, FILTER_SANITIZE_EMAIL);    // https://www.php.net/manual/en/filter.filters.sanitize.php 
                                                               // ylimääräiset symbolit kuten " tai ' voivat antaa pääsyn ulkopuoliselle siis hakkerille koodiin ja                                                                    // näin aiheuttaa tietomurron ja pääsyn tietokannan dataan.
$passhas = password_hash($password, PASSWORD_DEFAULT);         // password_hash on hashaus algoritmi joka kryptaa salasanan. Salasana näkyy tietokannassa randomeina                                                                  // symboleina ja kirjaimina sekä numeroina. SQL tietokannassa merkkimäärä 255 VARCHARISSA jotta                                                                        // erroreilta/bugeilta. PASSWORD_DEFAULT on yleinen algoritmi phpssa.

$usernamecheckquery = "SELECT username FROM player_info WHERE username = '".$usernameClean."':";


if(mysqli_num_rows($usernamecheckquery)>0){ // mysqli_num_rows ---> tarkastetaan löytyykö kyseistä tietoa tietokannasta.
echo("username already exists");        
}



if(mysqli_num_rows($emailcheckquery)>0){ // mysqli_num_rows ---> tarkastetaan löytyykö kyseistä tietoa tietokannasta.
echo("username already exists");        
}



<?



