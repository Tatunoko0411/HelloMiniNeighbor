<?php

namespace Database\Seeders;

use App\Models\Stage;
use App\Models\StageCell;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class StageCellsTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        StageCell::create(
            [
                'stage_id' => 1,
                'x' => 10,
                'y' => 15,
                'object_id' => 1,
            ]);

        StageCell::create(
            [
                'stage_id' => 2,
                'x' => 20,
                'y' => 35,
                'object_id' => 3,
            ]
        );
        //
    }
}
