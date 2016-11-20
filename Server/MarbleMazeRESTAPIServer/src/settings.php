<?php
return [
    'settings' => [
        'displayErrorDetails' => true, // set to false in production
        'addContentLengthHeader' => false, // Allow the web server to send the content-length header
		'authTokenTimeout' => 120000, // Time in seconds before token comes inactive after a successful request
		
		'db' => [
			'host' => "localhost",
			'user' => "root",
			'password' => "1337",
			'dbname' => "marblemaze"
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
