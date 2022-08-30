using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WaveOfEnemies", menuName = "WaveOfEnemies", order = 3)]
public class WaveOfEnemies : ScriptableObject
{
    [SerializeField] private List<EnemyEntry> _enemies;
    [SerializeField] private float _spawnTime;

    public List<EnemyEntry> Enemies => _enemies;

    public float SpawnTime => _spawnTime;

    public int EnemiesCount()
    {
        int count = 0;
        foreach(EnemyEntry enemyEntry in _enemies)
            count += enemyEntry.EnemyCount;
        return count;
    }
}

    [System.Serializable]
    public class EnemyEntry
    {
        public GameObject Enemy;
        public int EnemyCount;
    }
