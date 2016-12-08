<?php namespace App\Exception;

/**
 * Exception thrown during authentication process.
 *
 * @author Konstantin Schaper
 */
class AuthException extends \Exception {

	/**
	 * Exception thrown during authentication process.
	 *
	 * @param string $message The message to display to the user
	 */
    public function __construct(string $message) {
        $this->message = $message;
        $this->code = 401;
    }
	
}

?>