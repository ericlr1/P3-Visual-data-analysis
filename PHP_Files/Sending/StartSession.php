<?php
// Include the file that establishes a connection to the database
include 'DatabaseConnect.php';

try {
    // Get POST parameters
    $userID = isset($_POST['userID']) ? intval($_POST['userID']) : null;

    // Validate input
    if (is_null($userID)) {
        throw new Exception("Missing or invalid userID.");
    }

    // Prepare the SQL query
    $stmt = $conn->prepare("INSERT INTO `sessions` (`userID`, `startTime`) VALUES (?, NOW())");
    if (!$stmt) {
        throw new Exception("Failed to prepare SQL statement: " . $conn->error);
    }

    // Bind parameters
    if (!$stmt->bind_param("i", $userID)) {
        throw new Exception("Failed to bind parameters: " . $stmt->error);
    }

    // Execute the query
    if (!$stmt->execute()) {
        throw new Exception("Failed to execute SQL statement: " . $stmt->error);
    }

    // Return the last inserted ID (session ID)
    echo $conn->insert_id;

    // Close the statement
    $stmt->close();
} catch (Exception $e) {
    // Output error for debugging (optional, remove in production)
    echo "Error: " . $e->getMessage();
} finally {
    // Close the database connection
    $conn->close();
}
?>