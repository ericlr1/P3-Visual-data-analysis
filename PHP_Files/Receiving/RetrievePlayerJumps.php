<?php
// Include the file that establishes a connection to the database
include 'DatabaseConnect.php';

$sql = "SELECT sessionID, x, y, z, time FROM player_jumps";
$result = $conn->query($sql);

if ($result && $result->num_rows > 0) {
    $player_jumps = [];
    while ($row = $result->fetch_assoc()) {
        $player_jumps[] = $row;
    }

    echo json_encode([
        "success" => true,
        "data" => ["player_jumps" => $player_jumps]
    ]);
} else {
    echo json_encode([
        "success" => false,
        "error" => "No player_jumps found"
    ]);
}

$conn->close();
?>