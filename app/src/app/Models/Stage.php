<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Stage extends Model
{

    public function objects()
    {
        return $this->hasMany(StageObject::class);
    }

    public function buttons()
    {
        return $this->hasMany(StageButton::class);
    }
}
