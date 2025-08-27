<table>

    <table>
        <thead>
        <tr>

            <th scope="col">ID</th>
            <th scope="col">ユーザID</th>
            <th scope="col">ステージ名</th>
            <th scope="col">初期ポイント</th>
            <th scope="col">プレイされた回数</th>
            <th scope="col">クリアされた回数</th>

        </tr>
        @foreach($stages as $stage)
            <tr>
                <td><a href={{url('stages/config')}}?id={{$stage->id}}>{{$stage->id}}</a></td>
                <td>{{$stage->user_id}}</td>
                <td>{{$stage->name}}</td>
                <td>{{$stage->point}}</td>
                <td>{{$stage->play_time}}</td>
                <td>{{$stage->clear_time}}</td>

            </tr>
        @endforeach
    </table>

    <form method="get" action={{url('main')}}>
        @csrf
        <input type="submit" value="メインメニューへ">
    </form>
