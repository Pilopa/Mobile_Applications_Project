<?php
if (PHP_SAPI == 'cli-server') {
    // To help the built-in PHP dev server, check if the request was actually for
    // something which should probably be served as a static file
    $url  = parse_url($_SERVER['REQUEST_URI']);
    $file = __DIR__ . $url['path'];
    if (is_file($file)) {
        return false;
    }
}

require __DIR__ . '/../vendor/autoload.php';

spl_autoload_register(function ($classname) {
    require ("../classes/" . str_replace("\\", "/", $classname) . ".class.php");
});

session_start();

// Instantiate the app
$settings = require __DIR__ . '/../src/settings.php';
$app = new \Slim\App($settings);

// Set up container
$container = $app->getContainer();

// Set up logging
$container['logger'] = function($c) {
    $logger = new \Monolog\Logger('my_logger');
    $file_handler = new \Monolog\Handler\StreamHandler("../logs/app.log");
    $logger->pushHandler($file_handler);
    return $logger;
};

// Set up database connection
$container['db'] = function ($c) {
    $db = $c['settings']['db'];
    $pdo = new PDO("mysql:host=" . $db['host'] . ";dbname=" . $db['dbname'],
        $db['user'], $db['password']);
    $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    $pdo->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_ASSOC);
    return $pdo;
};

// Add error handler
$container['errorHandler'] = function ($container) {
    return function ($request, $response, $exception) use ($container) {
		$container['logger'] ->debug("(" . $exception->getCode() . ") " . $exception->getMessage());
		$code = $exception->getCode() <= 0 || $exception->getCode() >= 600 ? 500 : $exception->getCode();
        return $container['response']
							->withJson(array(
								"errorCode" => $code,
								"errorMessage" => $exception->getMessage()
							), $code);
    };
};

// Set up dependencies
require __DIR__ . '/../src/dependencies.php';

// Register middleware
require __DIR__ . '/../src/middleware.php';

// Register routes
require __DIR__ . '/../src/routes.php';

// Run app
$app->run();
