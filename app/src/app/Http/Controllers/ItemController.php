<?php

namespace App\Http\Controllers;

use App\Models\Item;
use Illuminate\Http\Request;
use function Laravel\Prompts\progress;
use function Termwind\parse;

class ItemController extends Controller
{
    public function index()
    {
        return view('create/item/index');
    }

    public function updateIndex(Request $request)
    {
        $item = Item::findOrFail($request->id);
        return view('create/item/update_index', ['item' => $item]);

    }

    public function updateStore(Request $request)
    {
        $item = Item::findOrFail($request->id);

        $item->name = $request['name'];
        $item->type_id = $request['type_id'];
        $item->profile_text = $request['profile'];


        $item->save();

        return redirect()->route('items.update.result', ['name' => $request['name']]);

    }

    //
    public function store(Request $request)
    {
        

        //dd($request);
        //レコード追加
        Item::create([
            'name' => $request['name'],
            'type_id' => parse($request['type']),
            'profile_text' => $request['profile']
        ]);
        return redirect()->route('items.result', ['name' => $request['name']]);

    }

    public function result(Request $request)
    {
        return view('create/item/result', ['name' => $request['name']]);
    }

    public function delete(Request $request)
    {
        // idで検索後にレコードを削除 (delete)
        $item = Item::findOrFail($request->id);
        $item->delete();

        return redirect()->route('items.delete.result', ['name' => $item['name']]);

    }

    public function checkDelete(Request $request)
    {
        $item = Item::findOrFail($request->id);
        return view('create/item/delete_check', ['item' => $item]);
    }

    public function deleteResult(Request $request)
    {
        return view('create/item/delete_result', ['name' => $request['name']]);
    }

    public function updateResult(Request $request)
    {
        return view('create/item/update_result', ['name' => $request['name']]);

    }


}
