<h1>セル情報の更新</h1>
<pre>
<form method="post" action={{url('stages/change')}}?id={{$cell->id}}>
   @csrf
    id<input type="number" name="id" value="{{$cell->id}}" readonly>
    x座標<input type="number" name="x" value="{{$cell->x}}">
    y座標<input type="number" name="y" value="{{$cell->y}}">
    オブジェクトタイプ<input type="number" name="object_id" value="{{$cell->object_id}}">
    <input type="submit" value="更新する">
</form>
</pre>
