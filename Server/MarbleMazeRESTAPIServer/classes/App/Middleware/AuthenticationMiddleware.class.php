<?php namespace App\Middleware;

require '/../vendor/autoload.php';

class AuthenticationMiddleware {

	private $container;

    public function __construct($container) {
        $this->container = $container;
    }

	public function __invoke($request, $response, $next)
    {
		$resource = $request->getUri()->getPath();
		
		// Check if the requested resource requires authentication
		if (!in_array($resource, ['/login', '/register'])) {
		
			// Check if header contains auth header
			$headers = apache_request_headers();
			
			if (array_key_exists("authorization", $headers)) {
				
				// Check for auth token in request header
				$authHeader = $headers["authorization"];
				
					// Retrieve Auth Values
					$authValues = explode(":", base64_decode(explode(" ", $authHeader)[1]));
					$bearer = $authValues[0];
					$token = $authValues[1];
					
					// Retrieve Values from Database
					$statement = $this->container['db'] ->prepare('SELECT * FROM user WHERE idUser = ?');
					
					if ($statement) {
					
						$statement->execute([$bearer]);
						$user = $statement->fetch();
						
						if ($user) {
						
							if ($user['authToken'] != $bearer. ":" . $token) {
					
								throw new \App\Exception\AuthException(json_encode(array(
									"errorMesage" => "AuthToken incorrect.",
									"data" => array(
										"expected" => $user['authToken'],
										"provided" => $token
									)
								)));
							
							}
							
						} else {
						
							throw new \App\Exception\AuthException("There is no database entry for the requested auth token bearer.");
							
						}	
						
					} else {
					
						throw new \App\Exception\AuthException("AuthToken could not retrieved from database or user is not existant.");
						
					}
					
			} else {
			
				throw new \App\Exception\AuthException(json_encode("AuthHeader is missing in request."));
			
			}
			
		}

        return $next($request, $response);
    }

}

?>