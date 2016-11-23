<?php
return [
    'settings' => [
        'displayErrorDetails' => true, // set to false in production
        'addContentLengthHeader' => false, // Allow the web server to send the content-length header
		'authTokenTimeout' => 120000, // Time in seconds before token comes inactive after a successful request
		
		'db' => [
			'host' => "mysql.hostinger.co.uk",
			'user' => "u472008720_me",
			'password' => "qwe123",
			'dbname' => "u472008720_maze"
		],

        // Renderer settings
        'renderer' => [
            'template_path' => __DIR__ . '/../templates/',
        ],

        // Monolog settings
        'logger' => [
            'name' => 'slim-app',
            'path' => __DIR__ . '/../logs/app.log',
            'level' => \Monolog\Logger::DEBUG,
        ],
    ],
];
