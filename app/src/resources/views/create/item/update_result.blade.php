<html lang="ja">
<head>
    <title>アイテム作成</title>
    <link href="/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
<script src="/js/bootstrap.bundle.min.js"></script>
<h1>■アイテム更新完了</h1>
<p>{{$name}}を更新しました</p>

<form method="post" action={{url('items')}}>
    @csrf
    <input type="submit" value="アイテム一覧へ">
</form>

</body>
</html>
