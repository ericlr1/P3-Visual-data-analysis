<?php
// Include the file that establishes a connection to the database
include 'DatabaseConnect.php';

$sql = "SELECT country, age, gender, sessionID, x, y, z, time FROM users_sessions_jumps";
$result = $conn->query($sql);

if ($result && $result->num_rows > 0) {
    $users_sessions_jumps = [];
    while ($row = $result->fetch_assoc()) {
        $users_sessions_jumps[] = $row;
    }

    echo json_encode([
        "success" => true,
        "data" => ["users_sessions_jumps" => $users_sessions_jumps]
    ]);
} else {
    echo json_encode([
        "success" => false,
        "error" => "No users_sessions_jumps found"
    ]);
}

$conn->close();
?>