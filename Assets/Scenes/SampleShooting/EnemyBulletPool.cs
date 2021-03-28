﻿using UnityEngine;

namespace Scenes.SampleShooting
{
    public interface IEnemyBulletPool
    {
        public ObjectPool.ObjectPool Pool();
    }
    
    public class EnemyBulletPool: MonoBehaviour, IEnemyBulletPool
    {
        [SerializeField] private GameObject enemyBullet;
        [SerializeField] private uint enemyBulletPoolSize = 40;
        private ObjectPool.ObjectPool _enemyBulletPool;
        private void Awake()
        {
            _enemyBulletPool = new ObjectPool.ObjectPool(enemyBulletPoolSize, enemyBullet, transform);
        }
        public ObjectPool.ObjectPool Pool() => _enemyBulletPool;
    }
}