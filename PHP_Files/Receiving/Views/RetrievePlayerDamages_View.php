<?php
// Include the file that establishes a connection to the database
include 'DatabaseConnect.php';

$sql = "SELECT country, age, gender, sessionID, x, y, z, time, damager, damager_x, damager_y, damager_z, amount FROM users_sessions_damaged";
$result = $conn->query($sql);

if ($result && $result->num_rows > 0) {
    $users_sessions_damaged = [];
    while ($row = $result->fetch_assoc()) {
        $users_sessions_damaged[] = $row;
    }

    echo json_encode([
        "success" => true,
        "data" => ["users_sessions_damaged" => $users_sessions_damaged]
    ]);
} else {
    echo json_encode([
        "success" => false,
        "error" => "No users_sessions_damaged found"
    ]);
}

$conn->close();
?>