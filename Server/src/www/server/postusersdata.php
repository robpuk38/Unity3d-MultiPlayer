<?php
include "dbconfig.php";
if(isset($_POST['UserId']) 
  && isset($_POST['UserPic'])
  && isset($_POST['UserAccessToken'])
  && isset($_POST['UserName'])
  && isset($_POST['UserFirstName'])
  && isset($_POST['UserLastName'])
  && isset($_POST['UserState'])
  && isset($_POST['UserGpsX'])
  && isset($_POST['UserGpsY'])
  && isset($_POST['UserGpsZ'])
  && isset($_POST['FirstTimeLogin']))
{

	


    $UserId= $_POST['UserId'];
    $UserPic= $_POST['UserPic'];
    $UserAccessToken= $_POST['UserAccessToken'];
    $UserName= $_POST['UserName'];
    $UserFirstName= $_POST['UserFirstName'];
    $UserLastName= $_POST['UserLastName'];
    $UserState= $_POST['UserState'];
    $UserGpsX = $_POST['UserGpsX'];
    $UserGpsY= $_POST['UserGpsY'];
    $UserGpsZ= $_POST['UserGpsZ'];
    $FirstTimeLogin = $_POST['FirstTimeLogin'];


    
    

	
				
				$sql = "SELECT * FROM clients WHERE  UserId = '$UserId' LIMIT 1"; 
  $query = mysqli_query($conn,$sql) or trigger_error("Query Failed: " . mysqli_error()); 
 
  
  if (mysqli_num_rows($query) < 1) 
  { 
  
  
	  
	  
	  
	   $sql = 'INSERT INTO Clients '.
      '(
      UserId,
      UserPic,
      UserAccessToken,
      UserName,
      UserFirstName,
      UserLastName,
      UserState,
      UsersXpos,
      UsersYpos,
      UsersZpos,
      UsersXrot,
      UsersYrot,
      UsersZrot,
      UserGpsX,
      UserGpsY,
      UserGpsZ,
      FirstTimeLogin
      )'.
      'VALUES ( 
      "'.$UserId.'",
      "'.$UserPic.'",
      "'.$UserAccessToken.'",
      "'.$UserName.'",
       "'.$UserFirstName.'",
       "'.$UserLastName.'",
       "'.$UserState.'",
       "'.$UserGpsX.'",
       "'.$UserGpsY.'",
       "'.$UserGpsZ.'",
       "0",
       "0",
       "0",
       "'.$UserGpsX.'",
       "'.$UserGpsY.'",
       "'.$UserGpsZ.'",
       "'.$FirstTimeLogin.'"
     
      )';
      $res = mysqli_query($conn,$sql);
	  
	  

  }
  else
  {
    
    $sql_2 = "UPDATE clients 
    SET UserAccessToken = '$UserAccessToken'
      WHERE UserId = '$UserId'" ;
      $res = mysqli_query( $conn,$sql_2 );
  }
  
				
		return;
}
else
{
	echo 0;
	return;
}





			

?>