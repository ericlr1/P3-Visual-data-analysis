<?php
// Include the file that establishes a connection to the database
include 'DatabaseConnect.php';

// Inicializar la consulta SQL base
$sql = $_POST['sql'];

$result = $conn->query($sql);

if ($result && $result->num_rows > 0) {
    $users = [];
    while ($row = $result->fetch_assoc()) {
        $users[] = $row;
    }

    echo json_encode([
        "success" => true,
        "data" => ["users" => $users]
    ]);
} else {
    echo json_encode([
        "success" => false,
        "error" => "No users found or query failed"
    ]);
}

$conn->close();
?>
