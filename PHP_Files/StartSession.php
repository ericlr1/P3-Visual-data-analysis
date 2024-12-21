<?php
// Include the file that establishes a connection to the database
include 'DatabaseConnect.php';

// Insert the session and get the ID
$sql = "INSERT INTO `sessions` (startTime) VALUES (NOW())";
if ($conn->query($sql) === TRUE) {
    echo $conn->insert_id;
} else {
    echo "Error: " . $sql . "<br>" . $conn->error;
}

$conn->close();
?>