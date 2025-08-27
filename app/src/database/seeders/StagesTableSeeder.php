<?php

namespace Database\Seeders;

use App\Models\Item;
use App\Models\Stage;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class StagesTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        Stage::create(
            [
                'name' => 'かんたんステージ',
            ]
        );
        Stage::create(
            [
                'name' => '普通のステージ',
            ]
        );
    }
}
