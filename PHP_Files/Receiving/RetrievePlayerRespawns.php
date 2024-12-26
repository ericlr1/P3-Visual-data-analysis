<?php
// Include the file that establishes a connection to the database
include 'DatabaseConnect.php';

$sql = "SELECT sessionID, x, y, z, time FROM player_respawns";
$result = $conn->query($sql);

if ($result && $result->num_rows > 0) {
    $player_respawns = [];
    while ($row = $result->fetch_assoc()) {
        $player_respawns[] = $row;
    }

    echo json_encode([
        "success" => true,
        "data" => ["player_respawns" => $player_respawns]
    ]);
} else {
    echo json_encode([
        "success" => false,
        "error" => "No player_respawns found"
    ]);
}

$conn->close();
?>