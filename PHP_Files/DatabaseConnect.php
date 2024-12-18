<?php

// Database connection details
$servername = "localhost";      // Server address, usually "localhost" when hosted locally
$username = "mariogs5";         // Username for database access
$password = "49240835c";        // Password for the database user
$database = "mariogs5";         // Name of the database to connect to

// Create connection to the database
$conn = new mysqli($servername, $username, $password, $database);

// Check connection status
if ($conn->connect_error) {     // If there's an error in the connection...
    die("Connection failed: " . $conn->connect_error); // Display the error and stop script execution
}

// If connection is successful, the script continues running

?>