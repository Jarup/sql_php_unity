<?php
// mysqli_connect(host,username,password,database name)
    $con = mysqli_connect('localhost', 'root', 'root','unity_db');

// Error Codes

    if(mysqli_connect_errno()){

echo("1");
exit();


// Error Codes
//1 - Database Connection Error

}
// getting data from unity form
$username = $_POST["username"];
$userid = $_POST["userid"];
$usernameClean = filter_var($username, FILTER_SANITIZE_EMAIL);
$email = $_POST["email"];
$emailClean = filter_var($email, FILTER_SANITIZE_EMAIL);
$password = $_POST["password"];
// hashing password algorithm 
$passhas = password_hash($password,PASSWORD_DEFAULT);


//SQL query
$usernamecheckquery = "SELECT username FROM player_information WHERE username = '".$usernameClean."';";
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
$con->close();
?>