<?php

namespace App\Http\Controllers;

use App\Models\Item;
use App\Models\StageCell;
use Illuminate\Http\Request;
use App\Models\Stage;
use function Termwind\parse;

class StageController extends Controller
{
    // 一覧表示
    public function index(Request $request)
    {
        //モデル取得
        $stages = Stage::All();

        return view('stage/index',
            ['stages' => $stages]);
    }

    public function config(Request $request)
    {
        //モデル取得
        $stage = Stage::findOrFail($request->id);
        // dd($stage->cells);


        return view('stage/config',
            ['stage' => $stage]);
    }

    public function update(Request $request)
    {
        $cell = StageCell::findOrFail($request->id);

        // dd($cell);

        return view('stage/update',
            ['cell' => $cell]);
    }

    public function change(Request $request)
    {
        dd($request->id);

        $cell = StageCell::findOrFail($request->id);

        $cell->x = $request->x;
        $cell->y = $request->y;
        $cell->object_id = $request->object_id;

        $cell->save();

        return redirect('stages/index');
    }

    public function add(Request $request)
    {
        return view('stage/add',
            ['stage_id' => $request->id]);
    }

    public function create(Request $request)
    {


        StageCell::create(
            [
                'stage_id' => parse($request['stage_id']),
                'x' => parse($request['x']),
                'y' => parse($request['y']),
                'object_id' => parse($request['object_id'])
            ]
        );


        return redirect('stages/index');
    }

}
