using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    public event Action OnLevelComplete;

    private List<Enemy> _enemies = new List<Enemy>();

    private void Awake()
    {
        _enemies = GetComponentsInChildren<Enemy>().ToList();

        foreach (Enemy enemy in _enemies)
        {
            enemy.OnDeath += Enemy_OnDeath;
        }
    }

    private void Enemy_OnDeath(Enemy enemy)
    {
        enemy.OnDeath -= Enemy_OnDeath;
        _enemies.Remove(enemy);

        if (CheckIfAllEnemiesDead())
        {
            OnLevelComplete?.Invoke();
        }
    }

    public bool CheckIfAllEnemiesDead()
    {
        return _enemies.Count == 0;
    }
}
