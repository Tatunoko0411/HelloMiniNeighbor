<?php

namespace App\Http\Controllers;

use App\Models\UserDetail;
use Illuminate\Http\Request;

class UserDetailController extends Controller
{
    public function update(Request $request)
    {


        $user = UserDetail::findOrFail($request->user_id);

        $user->level = $request->level;
        $user->EXP = $request->EXP;

        $user->save();

        return response()->json();
    }
}
