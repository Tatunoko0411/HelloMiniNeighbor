<?php

namespace App\Http\Controllers;

use App\Http\Resources\StageObjectResource;
use App\Http\Resources\StageResource;
use App\Models\StageObject;
use http\Env\Response;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Validator;

class StageObjectController extends Controller
{
    public function show(Request $request)
    {
        $cell = StageCell::findOrFail($request->stage_id);
        return response()->json(StageObjectResource::make($cell));
    }

    public function index(Request $request)
    {
        $cells = StageCell::all();
        $validator = Validator::make($request->all(), [
            'name' => ['required', 'string'],
        ]);
        return response()->json(StageObjectResource::collection($cells));


        if ($validator->fails()) {
            return response()->json($validator->errors(), 400);
        }

        return $stages;

    }

    public function store(Request $request)
    {

        $validator = Validator::make($request->all(), [
            'stage_id' => ['required', 'int'],
        ]);

        if ($validator->fails()) {
            return response()->json($validator->errors(), 400);
        }

        $object = StageObject::create([
            'stage_id' => $request->stage_id,
            'x' => $request->x,
            'y' => $request->y,
            'rot' => $request->rot,
            'object_id' => $request->object_id
        ]);

        return response()->json();
    }

    public function get(Request $request)
    {
        $objects = StageObject::where("stage_id", '=', $request->stage_id)
            ->get();

        return response()->json($objects);
    }
}
