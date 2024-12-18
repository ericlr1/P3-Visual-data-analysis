<?php
// Include the file that establishes a connection to the database
include 'DatabaseConnect.php';

// Receive data sent from Unity via POST request
$x = isset($_POST["x"]) ? floatval($_POST["x"]) : 0;     // Get 'ItemID' and convert it to an integer, or use 0 if not provided
$y = isset($_POST["y"]) ? floatval($_POST["y"]) : 0;     // Get 'DateTime' or set to an empty string if not provided
$z = isset($_POST["z"]) ? floatval($_POST["z"]) : 0;     // Get 'SessionID' and convert to an integer, or use 0 if not provided

// Check if all necessary data is present (non-zero for IDs and non-empty date)
if ($x > 0 && $y > 0 && $z > 0) 
{
    // Prepare an SQL query to insert the data into the ItemSales table
    $stmt = $conn->prepare("INSERT INTO `player_positions`(`x`, `y`, `z`) VALUES (?, ?, ?)");
    $stmt->bind_param("fff", $x, $y, $z);  // Bind parameters (float, float, float) to the SQL query

    // Execute the SQL query
    if ($stmt->execute()) {    
        // Get the last inserted ID from the database (auto-increment field)
        $last_id = $conn->insert_id;
        echo "Record inserted successfully. Last inserted ID is: " . $last_id;
    } else {
        // If an error occurs during execution, display the error message
        echo "Error al insertar el registro: " . $stmt->error;
    }

    // Close the prepared statement
    $stmt->close();
} else {
    // If parameters are missing, output an error message
    echo "Missing parameters";
}

// Close the database connection
$conn->close();

?>