<?php

$con = mysqli_connect('localhost', 'root', 'root','unity_db'); // connecting database and mysql



if(mysqli_connect_errno()){

echo("1");
exit();
}

$app_password = $_POST["apppassword"];
if($app_password != $app_password) // if login is not coming from unity app script... 
{

echo("login is not coming from unity app!"); // then exit and return response to unity
exit();
}

$username = $_POST["username"];
$usernameClean = filter_var($username, FILTER_SANITIZE_EMAIL);
$password = $_POST["password"];

$usernamecheckquery = "SELECT * FROM users WHERE username = '".$usernameClean."';";
$usernamecheckresult = mysqli_query($con, $usernamecheckquery) or die("2");




//mysqli_fetch_assoc ----> get related array value from row. if matched then continue log in procedure.
$fetchpassword = mysqli_fetch_assoc($usernamecheckresult)["password"];
if(password_verify(($password), $fetchpassword)){
	echo("logged in");
	$con->close(); // close database connection.
	
	
}else // if password is not matched with username then return response failed to login.
{	  // Unity has a script that gets the response automaticly.
	echo("failed to login");
	$con->close(); // close database connection
}




//1 - Database connection error
//2 - Username query error
//3 - Username not existing or there is more than 1 in the table
//4 - Password not able to be verified
?>
