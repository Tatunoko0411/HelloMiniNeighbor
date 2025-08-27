<?php

namespace App\Http\Controllers;

use App\Models\User;
use Illuminate\Http\Request;

class userController extends Controller
{
    public function index(Request $request)
    {
        //テーブルのすべてのレコードを取得
        $users = User::All();

        $title = 'アカウント一覧';


        if (!empty($request->name)) {
            $users = User::where('name', '=', "{$request->name}")->simplePaginate(10);
        }

        return view('users/index', ['title' => $title, 'users' => $users]);
    }
}
