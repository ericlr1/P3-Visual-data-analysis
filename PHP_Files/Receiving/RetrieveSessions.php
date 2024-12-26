<?php
// Include the file that establishes a connection to the database
include 'DatabaseConnect.php';

$sql = "SELECT sessionID, userID, startTime, endTime FROM sessions";
$result = $conn->query($sql);

if ($result && $result->num_rows > 0) {
    $sessions = [];
    while ($row = $result->fetch_assoc()) {
        $sessions[] = $row;
    }

    echo json_encode([
        "success" => true,
        "data" => ["sessions" => $sessions]
    ]);
} else {
    echo json_encode([
        "success" => false,
        "error" => "No sessions found"
    ]);
}

$conn->close();
?>