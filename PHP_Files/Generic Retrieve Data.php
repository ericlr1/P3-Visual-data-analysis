<?php
// Include the file that establishes a connection to the database
include 'DatabaseConnect.php';

// Inicializar la consulta SQL base
$sql = "SELECT userID, name, country, age, gender, dateOfCreation FROM users WHERE 1=1";

// Recorrer los filtros enviados por POST
foreach ($_POST as $key => $value) {
    if (!empty($value)) {
        // Escapar las cadenas y agregar condiciones según el filtro
        switch ($key) {
            case 'country':
                $sql .= " AND country = '" . $conn->real_escape_string($value) . "'";
                break;
            case 'age':
                $sql .= " AND age = " . intval($value);
                break;
            case 'username':
                $sql .= " AND name = '" . $conn->real_escape_string($value) . "'";
                break;
            // Agrega más filtros según sea necesario
        }
    }
}

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
