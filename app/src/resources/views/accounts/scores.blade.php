<html lang=“ja”>
<body>
<h1>■スコア一覧</h1>

<table>
    <thead>
    <tr>
        <th scope="col">ID</th>
        <th scope="col">名前</th>
        <th scope="col">スコア</th>
    </tr>
    </thead>
    @foreach($accounts as $account)
        <thead>
        <tr>
            <td>{{$account['id']}}</td>
            <td>{{$account['name']}}</td>
            <td>{{$account['value']}}</td>
        </tr>
        </thead>

    @endforeach
</table>

<form method="post" action={{url('index')}}>
    @csrf
    <input type="submit" value="ユーザー一覧へ">
</form>
<form method="post" action={{url('logout')}}>
    @csrf
    <input type="submit" value="ログアウト">
</form>
</body>
</html>
