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
        User::factory(100)->create();
        /*  user::create(
              [
                  'name' => '田中',
                  'level' => 14,
                  'EXP' => 1460
              ]

          );
          user::create([
              'name' => '佐藤',
              'level' => 10,
              'EXP' => 1010
          ]);
          user::create([
              'name' => 'マイケル',
              'level' => 63,
              'EXP' => 103398
          ]);*/
    }
}
