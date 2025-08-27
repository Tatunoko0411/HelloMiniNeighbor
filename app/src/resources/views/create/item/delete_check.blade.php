<html lang="ja">
<head>
    <title>アイテム作成</title>
    <link href="/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
<script src="/js/bootstrap.bundle.min.js"></script>
<h1>■アイテム削除</h1>
<p>{{$item->name}}を削除しますか？</p>


<form method="post" action={{url('items/delete')}}?id={{$item->id}}>
    @csrf
    <input type="submit" value="削除">
</form>
<form method="post" action={{url('items')}}>
    @csrf
    <input type="submit" value="アイテム一覧へ">
</form>


</body>
</html>
