<?php
// Routes

$app->post('/register', function ($request, $response, $args) use ($container) {

	// Get Input and Check it
	$input = json_decode($request->getBody(), true);
	
	if ($input != null) {
	
		if (array_key_exists('username', $input)) {
			if (array_key_exists('password', $input)) {
				$username = $input['username'];
				$password = $input['password'];
				
				// Check if username is already registered
				$statement = $container['db'] ->prepare('SELECT * FROM user WHERE username = ?');
					
					if ($statement) {
					
						$statement->execute([$username]);
						$user = $statement->fetch();
						
						if ($user != FALSE) {
							$response = $response->withJson(array(
								"errorCode" => 400,
								"errorMessage" => "There has been an internal database error or the username already taken"
							), 400);
						} else {
						
							// Attempt to create password hash
							$cost = 10;
							$salt = strtr(base64_encode(mcrypt_create_iv(16, MCRYPT_DEV_URANDOM)), '+', '.');
							$salt = sprintf("$2a$%02d$", $cost) . $salt;
							$passwordHash = crypt($password, $salt);
							
							if (!$passwordHash) {
								$response = $response->withJson(array(
									"errorCode" => 500,
									"errorMessage" => "Password hash could not be created"
								), 500);
							} else {
								// Store user and return result
								if ($container['db']->prepare("INSERT INTO user (username, passwordHash) VALUES (?, ?)")->execute([$username, $passwordHash])) {
									$id = $container['db']->lastInsertId();
									
									$response = $response->withJson(array(
										"user" => array(
											"id" => $id,
											"username" => $username
										)
									), 201);
								} else {
									$response = $response->withJson(array(
										"errorCode" => 500,
										"errorMessage" => "User could not be created"
									), 500);
								}
							}
						}
					} else {
						$response = $response->withJson(array(
							"errorCode" => 500,
							"errorMessage" => "Database could not be checked for duplicate username entries"
						), 500);
					}
			} else {
				$response = $response->withJson(array(
					"errorCode" => 400,
					"errorMessage" => "InputBody is missing password"
				), 400);
			}
		} else {
			$response = $response->withJson(array(
				"errorCode" => 400,
				"errorMessage" => "InputBody is missing username"
			), 400);
		}
		
	} else {
		$response = $response->withJson(array(
			"errorCode" => 400,
			"errorMessage" => "InputBody is incorrectly formatted"
		), 400);
	}
	
	return $response;
});

$app->post('/login', function ($request, $response, $args) use ($container) {

	// Get Input and Check it
	$input = json_decode($request->getBody(), true);
	
	if ($input != null) {
	
		if (array_key_exists('username', $input)) {
			if (array_key_exists('password', $input)) {
				$username = $input['username'];
				$password = $input['password'];
				
				// Retrieve user from database
				$statement = $container['db'] ->prepare('SELECT * FROM user WHERE username = ?');
					
				if ($statement) {
				
					$statement->execute([$username]);
					$user = $statement->fetch();
					
					if ($user == FALSE) {
						$response = $response->withJson(array(
							"errorCode" => 500,
							"errorMessage" => "There has been an internal database error or the requested user is not existant"
						), 500);
					} else {
						// Attempt to create password hash
						$passwordHash = crypt($password, $user['passwordHash']);
						
						if (!$passwordHash) {
								$response = $response->withJson(array(
									"errorCode" => 500,
									"errorMessage" => "Password hash could not be created"
								), 500);
						} else {
							
							// Check given password against the database
							if (hash_equals($user['passwordHash'], $passwordHash)) {
								
								// Generate AuthToken
								$userId = $user['idUser'];
								$authHash = bin2hex(openssl_random_pseudo_bytes(16));
								$authToken = $userId . ":" . $authHash;
				
								// Attempt to store auth hash
								if ($container['db']->prepare("UPDATE user SET authToken=?, lastRequestTimestamp=now() where idUser=?")->execute([$authToken, $userId])) {
									
									$response = $response->withJson(array(
										"authToken" => $authToken
									), 200);
									
								} else {
									$response = $response->withJson(array(
										"errorCode" => 500,
										"errorMessage" => "AuthHash could not be stored"
									), 500);
								}
								
							} else {
								$response = $response->withJson(array(
									"errorCode" => 400,
									"errorMessage" => "Incorrect password",
									"data" => array(
										"provided" => $passwordHash,
										"expected" => $user['passwordHash']
									)
								), 400);
							}	
						}
					}
				} else {
					$response = $response->withJson(array(
						"errorCode" => 500,
						"errorMessage" => "Could not retrieve user from database"
					), 500);
				}
				
			} else {
				$response = $response->withJson(array(
					"errorCode" => 400,
					"errorMessage" => "InputBody is missing password"
				), 400);
			}
		} else {
			$response = $response->withJson(array(
				"errorCode" => 400,
				"errorMessage" => "InputBody is missing username"
			), 400);
		}
		
	} else {
		$response = $response->withJson(array(
			"errorCode" => 400,
			"errorMessage" => "InputBody is incorrectly formatted"
		), 400);
	}
	
	return $response;
	
});

$app->post('/logout', function ($request, $response, $args) {

	
	return $response->withStatus(501);
});

$app->get('/highscore/{id}', function ($request, $response, $args) {

	
	return $response->withStatus(501);
});

$app->get('/highscore/{id}/level/{levelIndex}', function ($request, $response, $args) {
	
	return $response->withStatus(501);
});

$app->post('/highscore/{id}/level/{levelIndex}', function ($request, $response, $args) {

	
	return $response->withStatus(501);
});