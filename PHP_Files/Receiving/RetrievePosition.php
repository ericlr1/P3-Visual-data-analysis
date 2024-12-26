<?php
// Include the file that establishes a connection to the database
include 'DatabaseConnect.php';

try {
    // Prepare an SQL query to select all rows from the player_positions table
    $stmt = $conn->prepare("SELECT `x`, `y`, `z`, `time` FROM `player_positions`");
    if (!$stmt) {
        throw new Exception("Failed to prepare SQL statement: " . $conn->error);
    }

    // Execute the query
    if (!$stmt->execute()) {
        throw new Exception("Failed to execute SQL statement: " . $stmt->error);
    }

    // Fetch the result
    $result = $stmt->get_result();
    $positions = [];

    while ($row = $result->fetch_assoc()) {
        $positions[] = [
            "x" => (float) $row["x"],
            "y" => (float) $row["y"],
            "z" => (float) $row["z"],
            "time" => (float) $row["time"]
        ];
    }

    // Return the positions as a JSON array
    echo json_encode([
        "success" => true,
        "positions" => $positions
    ]);

} catch (Exception $e) {
    // Return an error message as JSON
    echo json_encode([
        "success" => false,
        "error" => $e->getMessage()
    ]);
} finally {
    // Close the statement and connection
    if (isset($stmt) && $stmt) {
        $stmt->close();
    }
    if (isset($conn) && $conn) {
        $conn->close();
    }
}
?>