<?php
// Include the file that establishes a connection to the database
include 'DatabaseConnect.php';

$sql = "SELECT sessionID, x, y, z, time FROM player_positions";
$result = $conn->query($sql);

if ($result && $result->num_rows > 0) {
    $player_positions = [];
    while ($row = $result->fetch_assoc()) {
        $player_positions[] = $row;
    }

    echo json_encode([
        "success" => true,
        "data" => ["player_positions" => $player_positions]
    ]);
} else {
    echo json_encode([
        "success" => false,
        "error" => "No player_positions found"
    ]);
}

$conn->close();
?>