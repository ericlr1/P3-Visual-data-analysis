<?php
// Include the file that establishes a connection to the database
include 'DatabaseConnect.php';

$sql = "SELECT sessionID, x, y, z, time, interactable FROM player_interactions";
$result = $conn->query($sql);

if ($result && $result->num_rows > 0) {
    $player_interactions = [];
    while ($row = $result->fetch_assoc()) {
        $player_interactions[] = $row;
    }

    echo json_encode([
        "success" => true,
        "data" => ["player_interactions" => $player_interactions]
    ]);
} else {
    echo json_encode([
        "success" => false,
        "error" => "No player_interactions found"
    ]);
}

$conn->close();
?>