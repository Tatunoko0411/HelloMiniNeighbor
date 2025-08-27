<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use App\Models\user_items;

class UserItemsSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        user_items::create(
            [
                'user_id' => 1,
                'item_id' => 1,
                'amount' => 3
            ]
        );
        user_items::create([
            'user_id' => 2,
            'item_id' => 3,
            'amount' => 10
        ]);
        user_items::create([
            'user_id' => 3,
            'item_id' => 1,
            'amount' => 80
        ]);
        user_items::create([
            'user_id' => 3,
            'item_id' => 2,
            'amount' => 40
        ]);
        user_items::create([
            'user_id' => 3,
            'item_id' => 3,
            'amount' => 99
        ]);
        user_items::create([
            'user_id' => 2,
            'item_id' => 2,
            'amount' => 2
        ]);
    }
}
