<?php
// Application middleware

$app->add(new \App\Middleware\AuthenticationMiddleware($container));
$app->add(new \App\Middleware\RequestTimingMiddleware($container));