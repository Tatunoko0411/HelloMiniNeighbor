<?php

namespace App\Http\Controllers;

use App\Http\Resources\ItemResource;
use App\Http\Resources\UserResource;
use App\Models\Item;
use App\Models\user_items;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Validator;
use function PHPUnit\Framework\isEmpty;

class ItemController extends Controller
{
    public function show(Request $request)
    {
        $items = Item::findOrFail($request->item_id);
        return response()->json(ItemResource::make($items));
    }

    public function index(Request $request)
    {
        $items = Item::all();
        $validator = Validator::make($request->all(), [
            'name' => ['required', 'string'],
        ]);
        return response()->json(ItemResource::collection($items));


        if ($validator->fails()) {
            return response()->json($validator->errors(), 400);
        }


    }

    public function get(Request $request)
    {

        $user_item = user_items::where("user_id", '=', $request->user_id)
            ->where("user_id", '=', $request->item_id)->first();


        $user_item->amount += $request->amount;

        $user_item->save();

        return response()->json();
    }

    public function use(Request $request)
    {
        $user_item = user_items::where("user_id", '=', $request->user_id)
            ->where("user_id", '=', $request->item_id)->first();


        $user_item->amount -= $request->amount;

        $user_item->save();

        return response()->json();
    }
}
