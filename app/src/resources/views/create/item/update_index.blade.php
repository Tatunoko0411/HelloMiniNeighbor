<html lang="ja">
<head>
    <title>アイテム作成</title>
    <link href="/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
<script src="/js/bootstrap.bundle.min.js"></script>
<h1>■アイテム作成</h1>
<form method="post" action="{{url('items/update/store')}}">
    @csrf
    <table border="1">
        <tr>
            <td>アイテム名</td>
            <td><input type="text" name="name" size="30" value="{{$item['name']}}"></td>
        </tr>
        <tr>
            <td>タイプ</td>
            <td><input type="number" name="type_id" size="30" value="{{$item['type_id']}}"></td>
        </tr>
        <tr>
            <td>説明</td>
            <td><input type="text" name="profile" size="30" value="{{$item['profile_text']}}"></td>
        </tr>
    </table>
    <input type="hidden" name="id" value="{{$item->id}}">
    <input type="submit" value="更新">
</form>

<form method="post" action={{url('items')}}>
    @csrf
    <input type="submit" value="アイテム一覧へ">
</form>
</body>
</html>
