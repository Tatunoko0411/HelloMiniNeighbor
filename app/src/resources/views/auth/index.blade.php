<html lang="ja">
<body>
<h1>ログイン画面</h1>
@if(!empty($error_id))
    <p>{{$error_id}}</p>
@endif
@if($errors->any())
    <ul>
        @foreach($errors->all() as $error)
            <li>{{$error}}</li>
        @endforeach
    </ul>
@endif
<form method="post" action="{{url('login')}}">
    @csrf
    <input type="text" name="name">名前
    <input type="text" name="password">パスワード
    <input type="submit" value="ログインする">
</form>
</body>
</html>
