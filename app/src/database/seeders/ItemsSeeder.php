<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use App\Models\Item;

class ItemsSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        Item::create(
            [
                'name' => 'サンドイッチ',
                'type_id' => 1,
                'profile_text' => 'とってもおいしい！！！！'
            ]
        );
        Item::create([
            'name' => '大きなつばさ',
            'type_id' => 2,
            'profile_text' => 'これでどこでもひとっ飛び！'
        ]);
        Item::create([
            'name' => 'やくそう',
            'type_id' => 1,
            'profile_text' => '怪我したときにはこれ！'
        ]);
    }
}
