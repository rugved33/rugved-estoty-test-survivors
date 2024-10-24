using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorGame
{
    public interface IEnemyFactory 
    {
        EnemyPresenter CreateEnemy(EnemyType enemyType, Vector3 spawnPosition, IPlayer player);
    }
    public enum EnemyType
    {
        WeakEnemy = 0,
        BasicEnemy = 1,
        FastEnemy = 2,
        StrongEnemy = 3,
    }
}