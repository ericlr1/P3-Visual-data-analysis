<?php
// Include the file that establishes a connection to the database
include 'DatabaseConnect.php';

try {
    // Get POST parameters
    $name = isset($_POST['name']) ? trim($_POST['name']) : null;
    $country = isset($_POST['country']) ? trim($_POST['country']) : null;
    $age = isset($_POST['age']) ? intval($_POST['age']) : null;
    $gender = isset($_POST['gender']) ? trim($_POST['gender']) : null;

    // Validate input
    if (is_null($name) || is_null($country) || is_null($age) || is_null($gender)) {
        throw new Exception("Missing or invalid parameters. Ensure name, country, age, and gender are provided.");
    }

    // Prepare the SQL query
    $stmt = $conn->prepare("INSERT INTO `users` (`name`, `country`, `age`, `gender`, `dateOfCreation`) VALUES (?, ?, ?, ?, NOW())");
    if (!$stmt) {
        throw new Exception("Failed to prepare SQL statement: " . $conn->error);
    }

    // Bind parameters
    if (!$stmt->bind_param("ssis", $name, $country, $age, $gender)) {
        throw new Exception("Failed to bind parameters: " . $stmt->error);
    }

    // Execute the query
    if (!$stmt->execute()) {
        throw new Exception("Failed to execute SQL statement: " . $stmt->error);
    }

    // Return the last inserted ID
    echo $conn->insert_id;

    // Close the statement
    $stmt->close();
} catch (Exception $e) {
    // Output error for debugging (optional, remove in production)
    echo "Error: " . $e->getMessage();
} finally {
    // Close the database connection
    $conn->close();
}

?>