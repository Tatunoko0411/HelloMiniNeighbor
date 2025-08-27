<?php

namespace App\Http\Controllers;


use App\Models\Account;
use App\Models\User;
use Illuminate\Http\Request;
use Barryvdh\Debugbar\Facades\Debugbar;
use Illuminate\Support\Facades\DB;

class AccountController extends Controller
{
    private $data = [
        [
            'id' => 1,
            'name' => 'name01',
            'password' => 'aaa'
        ],
        [
            'id' => 2,
            'name' => 'name002',
            'password' => 'bbb'
        ]
    ];

    private $scores =
        [
            [
                'id' => 1,
                'name' => 'name01',
                'value' => 10000
            ],
            [
                'id' => 2,
                'name' => 'name002',
                'value' => 8900
            ]
        ];

    //アカウント一覧を表示する
    public function index(Request $request)
    {
        //テーブルのすべてのレコードを取得
        // $users = User::simplePaginate(10);
        $users = User::All();
        $accounts = Account::All();
        $title = 'アカウント一覧';
        $count = Account::count();//レコード数
        $account = Account::findOrFail(1);//ID検索(無ければ404エラー)
        $account = Account::where('name', "=", 'jobi')->get();//条件指定で検索
        $account = Account::where(Account::where('name', '=', 'jobi')
            ->where('created_at', '>=', '2024-06-08')
            ->get());

        if (!empty($request->name)) {
            $users = User::where('name', '=', "{$request->name}")->simplePaginate(10);
        }

        return view('accounts/index', ['title' => $title, 'users' => $users]);
    }

    public function userDate(Request $request)
    {
        $user = User::where('id', '=', $request->id)->get();

        //dd($user);

        $userItems = DB::table('user_items')
            ->join('users', 'users.id', '=', 'user_items.user_id')
            ->join('items', 'items.id', '=', 'user_items.item_id')
            ->select('user_items.id as id', 'user_items.id as item_id', 'items.name as name', 'amount')
            ->where('user_items.user_id', '=', "{$request->id}")
            ->get();

        return view('accounts/user_date', ['user' => $user, 'items' => $userItems]);
    }


}
