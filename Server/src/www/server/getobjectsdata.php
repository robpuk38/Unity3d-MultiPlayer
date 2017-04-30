<?php
include "dbconfig.php";

$sql ="SELECT * FROM gamobject_waypoints";
$results = mysqli_query($conn,$sql);

while($rows = mysqli_fetch_assoc($results))
    {
    	$rowid = $rows['id'];
        $relatedid = $rows['relatedid'];
        $xpos = $rows['xpos'];
       $ypos = $rows['ypos'];
       $zpos = $rows['zpos'];
        $xrot = $rows['xrot'];
         $yrot = $rows['yrot'];
          $zrot = $rows['zrot'];
$sql_1 = "SELECT * FROM gameobjects WHERE id = '$relatedid' LIMIT 1";
  $query_1 = mysqli_query($conn, $sql_1) or trigger_error("Query Failed: " . mysqli_error()); 
 if (mysqli_num_rows($query_1) > 0) 
 {
          $row = mysqli_fetch_assoc($query_1);
          $id = $row['id'];
           $name = $row['name'];
           $model = $row['model'];
           $size = $row['size'];

          echo "|Id|".$rowid.
          "|RelatedId|".$relatedid. 
          "|Name|".$name.
          "|ModleId|".$model.
          "|Size|".$size.
   "|Xpos|".$xpos.
   "|Ypos|".$ypos.
   "|Zpos|".$zpos.
   "|Xrot|".$xrot.
   "|Yrot|".$yrot.
   "|Zrot|".$zrot;
	}

}
?>