<?php
// Include the file that establishes a connection to the database
include 'DatabaseConnect.php';

$sql = "SELECT country, age, gender, sessionID, x, y, z, time, interactable FROM users_sessions_interactions";
$result = $conn->query($sql);

if ($result && $result->num_rows > 0) {
    $users_sessions_interactions = [];
    while ($row = $result->fetch_assoc()) {
        $users_sessions_interactions[] = $row;
    }

    echo json_encode([
        "success" => true,
        "data" => ["users_sessions_interactions" => $users_sessions_interactions]
    ]);
} else {
    echo json_encode([
        "success" => false,
        "error" => "No users_sessions_interactions found"
    ]);
}

$conn->close();
?>