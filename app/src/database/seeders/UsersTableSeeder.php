<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use App\Models\User;

class UsersTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {

        user::create(
            [
                'name' => 'aaa',
                'play_time' => 0,
                'clear_time' => 0,
                'create_stage' => 0
            ]

        );
        user::create([
            'name' => 'bbb',
            'play_time' => 0,
            'clear_time' => 0,
            'create_stage' => 0
        ]);
        user::create([
            'name' => 'ccc',
            'play_time' => 0,
            'clear_time' => 0,
            'create_stage' => 0
        ]);
    }
}
