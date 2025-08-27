<?php

namespace App\Http\Controllers;

use App\Models\StageButton;
use App\Models\StageObject;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Validator;

class StageButtonController extends Controller
{
    public function get(Request $request)
    {
        $objects = StageButton::where("stage_id", '=', $request->stage_id)
            ->get();

        return response()->json($objects);
    }

    public function store(Request $request)
    {
        $validator = Validator::make($request->all(), [
            'stage_id' => ['required', 'int'],
        ]);

        if ($validator->fails()) {
            return response()->json($validator->errors(), 400);
        }

        $button = StageButton::create([
            'stage_id' => $request->stage_id,
            'object_id' => $request->object_id
        ]);

        return response()->json();
    }

}
