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
	
	if ($input != NULL) {
	
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
								$authHash =  bin2hex(openssl_random_pseudo_bytes(16));
		
								// Do not override previous auth tokens
								$authToken = $user['authToken'] != NULL ? $user['authToken'] : $userId . ":" . $authHash;
				
								// Attempt to store auth hash
								if ($container['db']->prepare("UPDATE user SET authToken=? where idUser=?")->execute([$authToken, $userId])) {
								
									// Save values for middleware to use
									$container['auth'] = array(
											"token" => $authHash,
											"bearer" => $userId
									);
									
									$container['logger'] ->debug("authValues stored");
									
									// Build response object
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
		throw new \Exception("InputBody is incorrectly formatted: " . $request->getBody(), 400);
	}
	
	return $response;
	
});

$app->post('/logout', function ($request, $response, $args) use ($container) {

	// Check if it was a user authenticated request
	if (isset($container["auth"]) && isset($container["auth"]["bearer"]) ) {
	
			// Delete auth token from database
			if (!$container['db']->prepare("UPDATE user SET authToken=? where idUser=?")->execute([null, $container["auth"]["bearer"]])) {
				$container['logger'] ->error("Logout due to inactivity could not be executed due to a database error for user with id = " . $container["auth"]["bearer"]);
			}
			
	} else {
	
		throw new \App\Exception\AuthException("User is not logged in. This error should never occur.");
		
	}
	
	return $response;
});

$app->get('/highscore/{id}', function ($request, $response, $args) use ($container) {

	// Check if it was a user authenticated request
	if (isset($container["auth"]) && isset($container["auth"]["bearer"]) ) {
	
			$bearer = $container["auth"]["bearer"];
	
			if ($args["id"] == $bearer) {
			
				// Retrieve Values from Database
				$statement = $container['db'] ->prepare('SELECT * FROM highscore WHERE User_idUser = ?');
				
				if ($statement->execute([$bearer])) {
				
					if (($highscores = $statement->fetchAll()) != FALSE) {
					
						$resultData = array();
					
						// Iterate through result set to fill result data
						foreach ($highscores as $highscore) {
							$resultData[$highscore['Level_idLevel']] = intval($highscore['value']);
						}
						
						$response = $response->withJson($resultData);
						
					} else {
					
						throw new \Exception("Database error: result is empty", 500);
						
					}
					
				} else {
					
						throw new \Exception("Database error: Statement could not be executed", 500);
						
				}
				
			} else {
				throw new \Exception("The given user does not have the required permissions to request this resource", 403);
			}
			
	} else {
	
		throw new \App\Exception\AuthException("User is not logged in. This error should never occur.");
		
	}
	
	return $response;
});

$app->get('/highscore/{id}/level/{levelIndex}', function ($request, $response, $args) use ($container) {

	// Check if it was a user authenticated request
	if (isset($container["auth"]) && isset($container["auth"]["bearer"]) ) {
	
			$bearer = $container["auth"]["bearer"];
	
			if ($args["id"] == $bearer) {
			
				// Retrieve Values from Database
				$statement = $container['db'] ->prepare('SELECT * FROM highscore WHERE User_idUser = ? AND Level_idLevel = ?');
				
				if ($statement->execute([$bearer, $args["levelIndex"]])) {
				
					if (($highscore = $statement->fetch()) != FALSE) {
					
							$response = $response->withJson(array(
								$args['levelIndex'] => intval($highscore['value'])
							));
						
					} else {
					
							$response = $response->withJson(array(
									$args['levelIndex'] => 0
							));
						
					}
					
				} else {
					
						throw new \Exception("Database error: Statement could not be executed", 500);
						
				}
				
			} else {
				throw new \Exception("The given user does not have the required permissions to request this resource", 403);
			}
			
	} else {
	
		throw new \App\Exception\AuthException("User is not logged in. This error should never occur.");
		
	}
	
	return $response;
});

$app->post('/highscore/{id}/level/{levelIndex}', function ($request, $response, $args) use ($container) {

	// Check if it was a user authenticated request
	if (isset($container["auth"]) && isset($container["auth"]["bearer"]) ) {
	
			$bearer = $container["auth"]["bearer"];
	
			if ($args["id"] == $bearer) {
			
			// Get Input and Check it
			$input = json_decode($request->getBody(), true);
			
				if ($input != null) {
					if (array_key_exists("value", $input) && is_numeric($input['value'])) {
						$newVal = intval($input['value']);
						
						// Check if highscore entry already exists in database
						$statement = $container['db'] ->prepare('SELECT * FROM highscore WHERE User_idUser = ? AND Level_idLevel = ?');
						
						if ($statement->execute([$bearer, $args["levelIndex"]])) {
						
							if (($highscore = $statement->fetch()) != FALSE) {
							
									// Highscore entry exists: update it
									if (!$container['db']->prepare("UPDATE highscore SET value=? WHERE User_idUser = ? AND Level_idLevel = ?")->execute([$newVal, $bearer, $args['levelIndex']])) {
										throw new \Exception("Database error.", 500);
									}
								
							} else {
							
									// Highscore entry doesnt exist: insert it
									if (!$container['db']->prepare("INSERT INTO highscore VALUES (?, ?, ?)")->execute([$bearer, $args['levelIndex'], $newVal])) {
										throw new \Exception("Database error.", 500);
									}
								
							}
						}
					} else {
						throw new \Exception("InputBody is incorrectly formatted. Input is missing value or the value is not a number.", 400);
					}
				} else {
					throw new \Exception("InputBody is incorrectly formatted. InputType must be json.", 400);
				}
				
			} else {
				throw new \Exception("The given user does not have the required permissions to request this resource", 403);
			}
			
	} else {
	
		throw new \App\Exception\AuthException("User is not logged in. This error should never occur.");
		
	}
	
	return $response;
});