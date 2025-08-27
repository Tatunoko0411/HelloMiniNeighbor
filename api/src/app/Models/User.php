<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Laravel\Sanctum\HasApiTokens;
use Illuminate\Foundation\Auth\User as Authenticatable;


class User extends Authenticatable
{
    use HasFactory;
    use HasApiTokens;


    protected $guarded = [
        'id',
    ];

    public function detail()
    {
        return $this->hasOne(UserDetail::class);
    }

    public function items()
    {
        return $this->belongsToMany(
            Item::class, 'user_items', 'user_id', 'item_id'
        )->withPivot('amount');
    }
}
