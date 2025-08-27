<?php

namespace App\Http\Controllers;

use App\Models\Account;
use App\Models\Item;
use App\Models\user_items;
use App\Models\User;
use Database\Seeders\UserItemsSeeder;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Hash;
use Illuminate\Support\Facades\DB;

class AuthController extends Controller
{
    public function scores(Request $request)
    {
        $title = 'アカウント一覧';
        return view('accounts/scores', ['title' => $title, 'accounts' => $this->scores]);
    }

    public function index(Request $request)
    {

        return view('auth/index', ['error_id' => $request->error_id]);
    }

    public function logout(Request $request)
    {
        $request->session()->flush();

        return view('auth/index', ['error_id' => $request->error_id]);
    }

    public function items(Request $request)
    {
        $items = Item::All();
        return view('auth/items', ['items' => $items]);
    }

    public function userItems(Request $request)
    {
        $users = User::All();
        $userItems = DB::table('user_items')
            ->join('users', 'users.id', '=', 'user_items.user_id')
            ->join('items', 'items.id', '=', 'user_items.item_id')
            ->select('user_items.id as id', 'users.name as user_name', 'items.name as item_name', 'amount')
            ->get();
        if (!empty($request->name)) {
            $userItems = DB::table('user_items')
                ->join('users', 'users.id', '=', 'user_items.user_id')
                ->join('items', 'items.id', '=', 'user_items.item_id')
                ->select('user_items.id as id', 'users.name as user_name', 'items.name as item_name', 'amount')
                ->where('users.name', '=', "{$request->name}")
                ->get();
        }

        //  dd($userItems);
        return view('auth/user_items', ['userItems' => $userItems, 'users' => $users]);
    }

    public function login(Request $request)
    {
        $request->session()->put('login', true);
        $account = Account::where('name', "=", "{$request['name']}")->first();#first()は一件のみget()は配列


        /* if (Hash::check($request->password, $account->password)) {
             $request->session()->put('login', true);
             return redirect('accounts/index');
         }*/


        return redirect('main');

        // return redirect('/1');

    }

    public function main()
    {
        return view('auth/main');
    }
}
