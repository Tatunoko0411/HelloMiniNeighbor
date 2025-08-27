@extends('layouts.app')
@section('title','ログイン画面')
@section('body')
    <form method="post" action={{url('accounts/index')}}>
        @csrf
        <input type="text" name="name" placeholder="名前を入力">
        <input type="submit" value="検索">
    </form>

    <table>
        <thead>
        <tr>

            <th scope="col">ID</th>
            <th scope="col">名前</th>
            <th scope="col">レベル</th>
            <th scope="col">経験値</th>
        </tr>
        </thead>
        @foreach($users as $user)
            @isset($user->detail->exp)
                <thead>
                <tr>

                    <td><a href={{url('accounts/userDate')}}?id={{$user->id}}>{{$user->id}}</td>
                    <td>{{$user['name']}}</td>
                    <td>{{$user->detail->exp}}</td>
                    <td>{{$user->detail->level}}</td>
                </tr>
                </thead>
            @endisset

        @endforeach
    </table>

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

