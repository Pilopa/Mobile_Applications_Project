<?php namespace App\Exception;

class AuthException extends \Exception {
    public function __construct($message) {
        $this->message = $message;
        $this->code = 401;
    }
}

?>