<h1>ステージ詳細</h1>

<table>ステージID　{{$stage->id}}</table>
<table>ステージ名　{{$stage->name}}</table>

<h2>オブジェクト情報</h2>
<table>
    <tr>
        <td>セルID</td>
        <td>X座標</td>
        <td>Y座標</td>
        <td>オブジェクトタイプ</td>
    </tr>
    @foreach($stage->objects as $object)
        <tr>
            <td>{{$object->id}}</td>
            <td>{{$object->x}}</td>
            <td>{{$object->y}}</td>
            <td>{{$object->object_id}}</td>
            <td>

                </form>
            </td>
        </tr>
    @endforeach
</table>

<h2>ボタン情報</h2>
<table>
    <tr>
        <td>ID</td>
        <td>オブジェクトタイプ</td>
    </tr>
    @foreach($stage->buttons as $button)
        <tr>
            <td>{{$button->id}}</td>
            <td>{{$button->object_id}}</td>
            <td>

                </form>
            </td>
        </tr>
    @endforeach
</table>


<form method="get" action={{url('stages/index')}}>
    @csrf
    <input type="submit" value="戻る">
</form>
