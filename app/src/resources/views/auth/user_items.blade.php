<html lang=“ja”>
<body>
<h1>■アイテム一覧</h1>
<form method="post" action={{url('userItems')}}>
    @csrf
    <input type="text" name="name" placeholder="名前を入力">
    <input type="submit" value="検索">
</form>
<table>
    <thead>
    <tr>

        <th scope="col">ID</th>
        <th scope="col">ユーザー名</th>
        <th scope="col">アイテム名</th>
        <th scope="col">個数</th>
    </tr>
    </thead>
    @foreach($users as $user)
        @foreach($user->items as $item)
            <thead>
            <tr>

                <td>{{$user->id}}</td>
                <td>{{$user->name}}</td>
                <td>{{$item->name}}</td>
                <td>{{$item->pivot->amount}}</td>
            </tr>
            </thead>

        @endforeach
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
<form method="post" action={{url('logout')}}>
    @csrf
    <input type="submit" value="ログアウト">
</form>
</body>
</html>
