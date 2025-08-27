@extends('layouts.app')
@section('title','ユーザ一覧')
@section('body')
    <form method="post" action={{url('users/index')}}>
        @csrf
        <input type="text" name="name" placeholder="名前を入力">
        <input type="submit" value="検索">
    </form>

    <table>
        <thead>
        <tr>

            <th scope="col">ID</th>
            <th scope="col">名前</th>
            <th scope="col">プレイ回数</th>
            <th scope="col">クリア回数</th>
            <th scope="col">ステージ制作数</th>

        </tr>
        </thead>
        @foreach($users as $user)

            <thead>
            <tr>

                <td>{{$user->id}}</td>
                <td>{{$user['name']}}</td>
                <td>{{$user->play_time}}</td>
                <td>{{$user->clear_time}}</td>
                <td>{{$user->create_stage}}</td>
            </tr>
            </thead>

        @endforeach
    </table>

    <form method="get" action={{url('main')}}>
        @csrf
        <input type="submit" value="メインメニューへ">
    </form>

@endsection



