<?php

namespace App\Http\Controllers;

use App\Models\StageObject;
use Illuminate\Http\Request;

class StageObjectController extends Controller
{
    public function index(Request $request)
    {
        //モデル取得
        $objects = StageObject::All();

        return view('objects/index',
            ['objects' => $objects]);
    }
}
