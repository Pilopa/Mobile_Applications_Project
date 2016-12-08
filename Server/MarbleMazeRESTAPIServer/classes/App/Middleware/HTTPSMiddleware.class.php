<?php namespace App\Middleware;

/**
 * Forces https on the client.<br>
 * Not currently used in development builds.
 *
 * @author Konstantin Schaper
 */
class HTTPSMiddleware extends Slim\Middleware {

	public function call() {
		$this->app->hook('slim.before.dispatch', array($this, 'verify')); //Middlewares may not use the '$app->halt' method so a hook is needed.
		$this->next->call(); // Calls the following middleware
	}
	
	private function verify() {
		$app = $this->app;
		if ($app->environment['slim.url_scheme'] !== 'https' ) {
			$app->redirect('/requiressl');    // or render response and $app->stop();
		}
	}

}

?>