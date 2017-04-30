<?php
include "dbconfig.php";

if(isset($_POST['CMD']) && 
	isset($_POST['AppKey'])&& 
	isset($_POST['xpos'])&& 
	isset($_POST['ypos'])&& 
	isset($_POST['zpos'])&& 
	isset($_POST['xrot'])&& 
	isset($_POST['yrot'])&& 
	isset($_POST['zrot']))
{
 $CMD = $_POST['CMD'];
 $AppKey =$_POST['AppKey'];
  $xpos =$_POST['xpos'];
   $ypos =$_POST['ypos'];
    $zpos =$_POST['zpos'];
     $xrot =$_POST['xrot'];
     $yrot =$_POST['yrot'];
     $zrot =$_POST['zrot'];


 if($AppKey == "appidkeyiswhatwesayitis")
 {
// we made it in

 $sql = 'INSERT INTO gamobject_waypoints '.
      '(
      relatedid,
      xpos,
      ypos,
      zpos,
      xrot,
      yrot,
      zrot
      )'.
      'VALUES ( 
      "'.$CMD.'",
      "'.$xpos.'",
      "'.$ypos.'",
      "'.$zpos.'",
      "'.$xrot.'",
      "'.$yrot.'",
      "'.$zrot.'"
      )';
      $res = mysqli_query($conn,$sql);

 }
}
?>