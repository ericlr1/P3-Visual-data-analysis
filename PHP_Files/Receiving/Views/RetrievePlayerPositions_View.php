<?php
// Include the file that establishes a connection to the database
include 'DatabaseConnect.php';

$sql = "SELECT country, age, gender, sessionID, x, y, z, time FROM users_sessions_positions";
$result = $conn->query($sql);

if ($result && $result->num_rows > 0) {
    $users_sessions_positions = [];
    while ($row = $result->fetch_assoc()) {
        $users_sessions_positions[] = $row;
    }

    echo json_encode([
        "success" => true,
        "data" => ["users_sessions_positions" => $users_sessions_positions]
    ]);
} else {
    echo json_encode([
        "success" => false,
        "error" => "No users_sessions_positions found"
    ]);
}

$conn->close();
?>