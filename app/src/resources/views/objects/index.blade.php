@extends('layouts.app')
@section('title','ステージオブジェクト一覧')
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
            <th scope="col">ステージID</th>
            <th scope="col">X座標</th>
            <th scope="col">Y座標</th>
            <th scope="col">角度</th>
            <th scope="col">オブジェクトID</th>

        </tr>
        </thead>
        @foreach($objects as $object)

            <thead>
            <tr>

                <td><a href={{url('accounts/userDate')}}?id={{$object->id}}>{{$object->id}}</td>
                <td>{{$object->stage_id}}</td>
                <td>{{$object->x}}</td>
                <td>{{$object->y}}</td>
                <td>{{$object->rot}}</td>
                <td>{{$object->object_id}}</td>
            </tr>
            </thead>

        @endforeach
    </table>

@endsection

