<?php

namespace App\Http\Resources;

use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class ItemResource extends JsonResource
{
    /**
     * Transform the resource into an array.
     *
     * @return array<string, mixed>
     */
    public function toArray(Request $request): array
    {
        return [
            'name' => $this->name,
            'type_id' => $this->type_id,
            'profile_text' => $this->profile_text,
            'created_at' => $this->created_at
                ->format('Y/m/d H:i:s')
        ];
    }
}
