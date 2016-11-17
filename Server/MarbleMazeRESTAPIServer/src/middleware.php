<?php
// Application middleware

$app->add(new \App\Middleware\AuthenticationMiddleware($container));