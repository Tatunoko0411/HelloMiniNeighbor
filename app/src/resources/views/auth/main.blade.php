<form method="get" action={{url('users/index')}}>
    @csrf
    <input type="submit" value="ユーザー一覧へ">
</form>
<form method="get" action={{url('stages/index')}}>
    @csrf
    <input type="submit" value="ステージ一覧へ">
</form>

</form>
