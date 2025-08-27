<?php

namespace Database\Seeders;

use App\Models\StageCell;
use App\Models\User;

// use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class DatabaseSeeder extends Seeder
{
    /**
     * Seed the application's database.
     */
    public function run(): void
    {
        User::factory(10)->create();
        $this->call(AccountsTableSeeder::class);
        $this->call(ItemsSeeder::class);
        $this->call(UserItemsSeeder::class);
        $this->call(UsersTableSeeder::class);
        $this->call(UserDetailsTableSeeder::class);
        $this->call(StagesTableSeeder::class);
        $this->call(StageCellsTableSeeder::class);
        $this->call(ObjectTypeTableSeeder::class);


    }
}
