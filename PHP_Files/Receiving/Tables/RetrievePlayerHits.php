<?php
// Include the file that establishes a connection to the database
include 'DatabaseConnect.php';

$sql = "SELECT sessionID, x, y, z, time, target, amount FROM player_hits";
$result = $conn->query($sql);

if ($result && $result->num_rows > 0) {
    $player_hits = [];
    while ($row = $result->fetch_assoc()) {
        $player_hits[] = $row;
    }

    echo json_encode([
        "success" => true,
        "data" => ["player_hits" => $player_hits]
    ]);
} else {
    echo json_encode([
        "success" => false,
        "error" => "No player_hits found"
    ]);
}

$conn->close();
?>