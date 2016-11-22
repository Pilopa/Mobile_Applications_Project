<?php namespace App\Middleware;

class RequestTimingMiddleware {

	private $container;

    public function __construct($container) {
        $this->container = $container;
    }

	public function __invoke($request, $response, $next)
    {
		///////////////////////////////////
		//// Before routing ////
		//////////////////////////////////
		
		// Do nothing here
		
		// Continue Middleware chain and do routing
		$response = $next($request, $response);
		
		///////////////////////////////////
		//// After routing	   ////
		//////////////////////////////////
		
		// Check if it was a user authenticated request
		if (isset($this->container["auth"]) && isset($this->container["auth"]["bearer"]) ) {
		
			$bearer = $this->container["auth"]["bearer"];
			
			// Attempt to update the time of last request
			$timestamp = date("Y-m-d H:i:s", time());
			if (!$this->container['db']->prepare("UPDATE user SET lastRequestTimestamp=? where idUser=?")->execute([$timestamp, $bearer])) {
			
				$this->container['logger'] ->warning("LastRequestTimestamp could not be updated for user with id = " . $bearer);
				
			} else {
			
				$this->container['logger'] ->info("lastRequestTimestamp updated for user with id = " . $bearer);
				
			}
			
		}

        return $response;
    }

}

?>