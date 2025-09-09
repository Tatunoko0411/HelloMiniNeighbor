<?php

namespace App\Http\Controllers;

use App\Http\Resources\StageResource;
use App\Http\Resources\UserResource;
use App\Models\Stage;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Validator;

class StageController extends Controller
{
    public function show(Request $request)
    {
        $stage = Stage::where('user_id', '=', "{$request->user_id}")->get();
        return response()->json($stage);
    }

    public function index(Request $request)
    {
        $stages = Stage::all();
        $validator = Validator::make($request->all(), [
            'name' => ['required', 'string'],
        ]);
        return response()->json($stages);


    }

    public function store(Request $request)
    {
        $validator = Validator::make($request->all(), [
            'user_id' => ['required', 'int'],
        ]);

        if ($validator->fails()) {
            return response()->json($validator->errors(), 400);
        }

        $stage = Stage::create([
            'user_id' => $request->user_id,
            'name' => $request->name,
            'point' => $request->point,
            'play_time' => 0,
            'clear_time' => 0,

        ]);

        return response()->json(["stage_id" => $stage->id]);
    }

}
