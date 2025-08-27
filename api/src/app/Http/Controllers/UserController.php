<?php

namespace App\Http\Controllers;

use App\Http\Resources\UserResource;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Validator;

class UserController extends Controller
{
    public function show(Request $request)
    {
        $user = User::findOrFail($request->user_id);
        return response()->json(UserResource::make($user));
    }


    public function index(Request $request)
    {
        $users = User::all();
        $validator = Validator::make($request->all(), [
            'name' => ['required', 'string'],
        ]);

        return response()->json(UserResource::collection($users));

        if ($validator->fails()) {
            return response()->json($validator->errors(), 400);
        }


    }

    public function store(Request $request)
    {
        $validator = Validator::make($request->all(), [
            'name' => ['required', 'string'],
        ]);

        if ($validator->fails()) {
            return response()->json($validator->errors(), 400);
        }

        $user = User::create([
            'name' => $request->name,
            'play_time' => 0,
            'clear_time' => 0,
            'create_stage' => 0
        ]);
//APIトークンを発行する
        $token = $user->createToken($request->name)->plainTextToken;

        //ユーザーIDとAPIトークンを返す
        return response()->json(['user_id' => $user->id, 'token' => $token]);

    }

    public function update(Request $request)
    {
        $validator = Validator::make($request->all(), [
            'name' => ['required']
        ]);


        $user = User::findOrFail($request->user()->id);


        $user->name = $request->name;
        $user->level = $request->level;
        $user->EXP = $request->EXP;

        $user->save();

        return response()->json();
    }

}
