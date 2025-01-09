<?php
// Include the file that establishes a connection to the database
include 'DatabaseConnect.php';

$sql = "SELECT country, age, gender, sessionID, x, y, z, time, target, amount FROM users_sessions_hits";
$result = $conn->query($sql);

if ($result && $result->num_rows > 0) {
    $users_sessions_hits = [];
    while ($row = $result->fetch_assoc()) {
        $users_sessions_hits[] = $row;
    }

    echo json_encode([
        "success" => true,
        "data" => ["users_sessions_hits" => $users_sessions_hits]
    ]);
} else {
    echo json_encode([
        "success" => false,
        "error" => "No users_sessions_hits found"
    ]);
}

$conn->close();
?>