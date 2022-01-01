using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game constants
/// </summary>
public static class GameConstants
{
    // bullets
    public const float BulletImpulseForce = 10f;

    // ship
    public const float ShipMoveUnitsPerSecond = 10f;
    public const float ShipBulletOffset = 0.35f;
    public const int ShipBulletCollisionDamage = 1;
    public const int ShipEnemyCollisionDamage = 5;

    // enemies
    public const float EnemyImpulseForce = -3f;
    public const float EnemyBulletXOffset = -0.5f;
    public const float EnemyBulletYOffset = 0.04f;
    public const float EnemyShootDelaySeconds = 0.5f;

    // object pools
    public const int InitialBulletPoolCapacity = 100;
    public const int InitialEnemyPoolCapacity = 15;

    // enemy spawning
    public const float EnemySpawnDelaySeconds = 1f;
}
