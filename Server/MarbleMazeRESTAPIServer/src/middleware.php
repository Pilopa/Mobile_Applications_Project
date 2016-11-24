<?php
// Application middleware

$app->add(new \App\Middleware\RequestTimingMiddleware($container));