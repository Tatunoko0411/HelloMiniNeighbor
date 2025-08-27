<?php

namespace App\Http\Middleware;

use Closure;
use Illuminate\Http\Request;
use Symfony\Component\HttpFoundation\Response;

class AuthMiddleware
{
    /**
     * Handle an incoming request.
     *
     * @param \Closure(\Illuminate\Http\Request): (\Symfony\Component\HttpFoundation\Response) $next
     */
    public function handle(Request $request, Closure $next): Response
    {
        $value = $request->session()->get('login');
        if (!$value/*ログインしていない場合*/) {

            return redirect()->route('login');
        }

        $response = $next($request); //ここでController実行

        return $response;
    }
}
