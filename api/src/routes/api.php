<?php

use App\Http\Controllers\ItemController;
use App\Http\Controllers\StageButtonController;
use App\Http\Controllers\StageObjectController;
use App\Http\Controllers\StageController;
use App\Http\Controllers\TestController;
use App\Http\Controllers\UserController;
use App\Http\Controllers\UserDetailController;
use App\Models\StageButton;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;

Route::get('users/index',
    [UserController::class, 'index'])
    ->name('users.index');

Route::get('users/{user_id}',
    [UserController::class, 'show'])
    ->name('users.show');


Route::get('stages/index',
    [StageController::class, 'index'])
    ->name('users.index');


Route::get('stages/{user_id}',
    [StageController::class, 'show'])
    ->name('users.show');

Route::get('stageCells/index',
    [StageObjectController::class, 'index'])
    ->name('users.index');


Route::get('stageCells/{stage_id}',
    [StageObjectController::class, 'show'])
    ->name('users.show');

Route::get('items/index',
    [ItemController::class, 'index'])
    ->name('items.index');

Route::get('items/{item_id}',
    [ItemController::class, 'show'])
    ->name('items.show');

Route::post('users/store',
    [UserController::class, 'store'])
    ->name('users.store');

Route::post('users/update',
    [UserController::class, 'update'])
    ->middleware('auth:sanctum')->name('users.update');

Route::post('userDetails/update',
    [UserDetailController::class, 'update'])
    ->name('userDetails.update');

Route::post('items/get',
    [ItemController::class, 'get'])
    ->name('items.get');

Route::post('items/use',
    [ItemController::class, 'use'])
    ->name('items.use');

Route::post('stages/store',
    [StageController::class, 'store'])
    ->name('stages.store');

Route::post('stages/object/store',
    [StageObjectController::class, 'store'])
    ->name('stages.object.store');

Route::get('stages/object/get/{stage_id}',
    [StageObjectController::class, 'get'])
    ->name('stages.object.get');

Route::post('stages/button/store',
    [StageButtonController::class, 'store'])
    ->name('stages.button.store');

Route::get('stages/button/get/{stage_id}',
    [StageButtonController::class, 'get'])
    ->name('stages.button.get');


Route::get('tests', [TestController::class, 'test']);
