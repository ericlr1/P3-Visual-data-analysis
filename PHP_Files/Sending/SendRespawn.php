<?php
// Include the file that establishes a connection to the database
include 'DatabaseConnect.php';

try {
    // Receive data sent from Unity via POST request
    $sessionID = isset($_POST["sessionID"]) ? intval($_POST["sessionID"]) : null; // Get 'x' or null if not provided
    $x = isset($_POST["x"]) ? floatval($_POST["x"]) : null; // Get 'x' or null if not provided
    $y = isset($_POST["y"]) ? floatval($_POST["y"]) : null; // Get 'y' or null if not provided
    $z = isset($_POST["z"]) ? floatval($_POST["z"]) : null; // Get 'z' or null if not provided
    $time = isset($_POST["time"]) ? floatval($_POST["time"]) : null; // Get 'z' or null if not provided

    // Check if all parameters are provided and valid
    if (is_null($sessionID) || is_null($x) || is_null($y) || is_null($z) || is_null($time)) {
        throw new Exception("Missing or invalid parameters. Ensure sessionID, x, y, z and time are provided correctly.");
    }

    // Prepare an SQL query to insert the data into the player_positions table
    $stmt = $conn->prepare("INSERT INTO `player_respawns`(`sessionID`, `x`, `y`, `z`, `time`) VALUES (?, ?, ?, ?, ?)");
    if (!$stmt) {
        throw new Exception("Failed to prepare SQL statement: " . $conn->error);
    }

    // Bind parameters (double, double, double, double) to the SQL query
    if (!$stmt->bind_param("idddd", $sessionID, $x, $y, $z, $time)) {
        throw new Exception("Failed to bind parameters: " . $stmt->error);
    }

    // Execute the SQL query
    if (!$stmt->execute()) {
        throw new Exception("Failed to execute SQL statement: " . $stmt->error);
    }

    // Get the last inserted ID from the database (auto-increment field)
    $last_id = $conn->insert_id;
    echo json_encode([
        "success" => true,
        "message" => "Record inserted successfully.",
        "last_id" => $last_id
    ]);

} catch (Exception $e) {
    // Return a JSON error response with the exception message
    echo json_encode([
        "success" => false,
        "error" => $e->getMessage()
    ]);
} finally {
    // Close the prepared statement if it exists
    if (isset($stmt) && $stmt) {
        $stmt->close();
    }

    // Close the database connection
    if (isset($conn) && $conn) {
        $conn->close();
    }
}
?>