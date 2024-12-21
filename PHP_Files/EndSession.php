<?php
// Include the file that establishes a connection to the database
include 'DatabaseConnect.php';

try {
    // Receive data sent from Unity via POST request
    $sessionID = isset($_POST["sessionID"]) ? intval($_POST["sessionID"]) : null;

    // Check if all parameters are provided and valid
    if (is_null($sessionID)) {
        throw new Exception("Missing or invalid parameters. Ensure sessionID is provided as a valid int.");
    }

    // Prepare an SQL query to insert the data into the player_positions table
    $stmt = $conn->prepare("UPDATE `sessions` SET endTime = NOW() WHERE id = ?");
    if (!$stmt) {
        throw new Exception("Failed to prepare SQL statement: " . $conn->error);
    }

    // Bind parameters to the SQL query
    if (!$stmt->bind_param("i", $sessionID)) {
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