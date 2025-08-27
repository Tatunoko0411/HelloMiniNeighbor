<?php

namespace Database\Seeders;

use App\Models\ObjectType;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class ObjectTypeTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        ObjectType::create(
            [
                'name' => '静止オブジェクト'
            ]
        );
        ObjectType::create(
            [
                'name' => '移動オブジェクト'
            ]
        );
        ObjectType::create(
            [
                'name' => 'トラップ系オブジェクト'
            ]
        );
    }
}
