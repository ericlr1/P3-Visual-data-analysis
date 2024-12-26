<?php
// Include the file that establishes a connection to the database
include 'DatabaseConnect.php';

try {
    // Prepare the SQL query to count the total number of users
    $countQuery = "SELECT COUNT(*) as total FROM `users`";
    $result = $conn->query($countQuery);

    if (!$result) {
        throw new Exception("Failed to count users: " . $conn->error);
    }

    // Fetch the total number of users
    $row = $result->fetch_assoc();
    $totalUsers = $row['total'];

    if ($totalUsers == 0) {
        throw new Exception("No users found in the database.");
    }

    // Generate a random offset
    $randomOffset = rand(0, $totalUsers - 1);

    // Query to select one random user based on the offset
    $selectQuery = "SELECT `userID` FROM `users` LIMIT 1 OFFSET ?";
    $stmt = $conn->prepare($selectQuery);
    if (!$stmt) {
        throw new Exception("Failed to prepare SQL statement: " . $conn->error);
    }

    // Bind the offset parameter
    if (!$stmt->bind_param("i", $randomOffset)) {
        throw new Exception("Failed to bind parameters: " . $stmt->error);
    }

    // Execute the query
    if (!$stmt->execute()) {
        throw new Exception("Failed to execute SQL statement: " . $stmt->error);
    }

    // Fetch the result
    $result = $stmt->get_result();
    $user = $result->fetch_assoc();

    if (!$user) {
        throw new Exception("Failed to fetch a random user.");
    }

    // Return the user ID
    echo $user['userID'];

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