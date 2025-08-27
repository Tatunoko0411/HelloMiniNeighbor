<h1>セルの追加</h1>
<pre>
<form method="post" action={{url('stages/create')}}?id={{$stage_id}}>
   @csrf
    ステージid<input type="number" name="stage_id" value="{{$stage_id}}" readonly>
    x座標<input type="number" name="x">
    y座標<input type="number" name="y">
    オブジェクトタイプ<input type="number" name="object_id">
    <input type="submit" value="追加する">
</form>
</pre>
