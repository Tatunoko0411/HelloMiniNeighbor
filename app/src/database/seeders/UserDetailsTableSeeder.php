<?php

namespace Database\Seeders;

use App\Models\UserDetail;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class UserDetailsTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        //
        UserDetail::create(
            [
                'user_id' => 1,
                'exp' => 100,
                'level' => 1
            ]
        );
        UserDetail::create(
            [
                'user_id' => 2,
                'exp' => 1000,
                'level' => 10
            ]
        );
        UserDetail::create(
            [
                'user_id' => 3,
                'exp' => 562800,
                'level' => 64
            ]
        );
    }
}
