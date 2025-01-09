<?php
// Include the file that establishes a connection to the database
include 'DatabaseConnect.php';

$sql = "SELECT sessionID, x, y, z, time FROM player_deaths";
$result = $conn->query($sql);

if ($result && $result->num_rows > 0) {
    $player_deaths = [];
    while ($row = $result->fetch_assoc()) {
        $player_deaths[] = $row;
    }

    echo json_encode([
        "success" => true,
        "data" => ["player_deaths" => $player_deaths]
    ]);
} else {
    echo json_encode([
        "success" => false,
        "error" => "No player_deaths found"
    ]);
}

$conn->close();
?>