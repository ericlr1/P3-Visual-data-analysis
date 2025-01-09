<?php
// Include the file that establishes a connection to the database
include 'DatabaseConnect.php';

$sql = "SELECT sessionID, x, y, z, time FROM player_heals";
$result = $conn->query($sql);

if ($result && $result->num_rows > 0) {
    $player_heals = [];
    while ($row = $result->fetch_assoc()) {
        $player_heals[] = $row;
    }

    echo json_encode([
        "success" => true,
        "data" => ["player_heals" => $player_heals]
    ]);
} else {
    echo json_encode([
        "success" => false,
        "error" => "No player_heals found"
    ]);
}

$conn->close();
?>