<html lang=“ja”>
<body>
<h1>■アイテム一覧</h1>
<form method="post" action={{url('items/create')}}>
    @csrf
    <input type="submit" value="追加する">
</form>
<table>
    <thead>
    <tr>

        <th scope="col">ID</th>
        <th scope="col">名前</th>
        <th scope="col">種類</th>
        <th scope="col">説明</th>
        <th scope="col">操作</th>
    </tr>
    </thead>
    @foreach($items as $item)
        <thead>
        <tr>

            <td>{{$item->id}}</td>
            <td>{{$item['name']}}</td>
            <td>{{$item['type_id']}}</td>
            <td>{{$item['profile_text']}}</td>
            <td>
                <form method="post" action={{url('items/delete/check')}}?id={{$item->id}}>
                    @csrf
                    <input type="submit" value="削除">
                </form>
                <form method="post" action={{url('items/update/index')}}?id={{$item->id}}>
                    @csrf
                    <input type="submit" value="更新">
                </form>
            </td>
        </tr>
        </thead>

    @endforeach
</table>

<form method="post" action={{url('accounts/index')}}>
    @csrf
    <input type="submit" value="ユーザー一覧へ">
</form>
<form method="post" action={{url('userItems')}}>
    @csrf
    <input type="submit" value="所持アイテム一覧へ">
</form>
<form method="post" action={{url('logout')}}>
    @csrf
    <input type="submit" value="ログアウト">
</form>
</body>
</html>
