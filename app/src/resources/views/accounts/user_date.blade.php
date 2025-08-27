@extends('layouts.app')
@section('title','ログイン画面')
@section('body')

    <h1>ユーザー情報</h1>
    <p>ユーザーID　{{$user[0]->id}}</p>
    <p>ユーザー名　{{$user[0]->name}}</p>
    <table>
        <thead>
        <tr>

            <th scope="col">ID</th>
            <th scope="col">アイテムID</th>
            <th scope="col">アイテム名</th>
            <th scope="col">所持数</th>
        </tr>
        <h1>所持アイテム一覧</h1>
        </thead>
        @foreach($items as $item)
            <thead>
            <tr>

                <td>{{$item->id}}</td>
                <td>{{$item->item_id}}</td>
                <td>{{$item->name}}</td>
                <td>{{$item->amount}}</td>
            </tr>
            </thead>

        @endforeach
    </table>
    <form method="post" action={{url('accounts/index')}}>
        @csrf
        <input type="submit" value="ユーザー一覧へ">
    </form>

    <form method="post" action={{url('items')}}>
        @csrf
        <input type="submit" value="アイテム一覧へ">
    </form>
    <form method="post" action={{url('userItems')}}>
        @csrf
        <input type="submit" value="所持アイテム一覧へ">
    </form>
    <form method="post" action={{url('logout')}}>
        @csrf
        <input type="submit" value="ログアウト">
    </form>
@endsection

