<?php
// Include the file that establishes a connection to the database
include 'DatabaseConnect.php';

$sql = "SELECT sessionID, x, y, z, time, damager, damager_x, damager_y, damager_z, amount FROM player_damaged";
$result = $conn->query($sql);

if ($result && $result->num_rows > 0) {
    $player_damaged = [];
    while ($row = $result->fetch_assoc()) {
        $player_damaged[] = $row;
    }

    echo json_encode([
        "success" => true,
        "data" => ["player_damaged" => $player_damaged]
    ]);
} else {
    echo json_encode([
        "success" => false,
        "error" => "No player_damaged found"
    ]);
}

$conn->close();
?>