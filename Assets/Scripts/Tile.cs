using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Enemy _currentEnemy;
    [SerializeField] private Obstacle _obstacle;

    [SerializeField] private Transform _centerPosition;

    public void SetEnemy(Enemy enemy)
    {
        _currentEnemy = enemy;
    }

    public void ClearTile()
    {
        _currentEnemy.Die();
        _currentEnemy = null;
    }

    public bool TryGetEnemy(out Enemy enemy)
    {
        if (_currentEnemy == null)
        {
            enemy = null;
            return false;
        }

        enemy = _currentEnemy;
        return true;
    }


    public bool HasObstacle()
    {
        return _obstacle != null;
    }

    public Vector3 GetTargetPosition()
    {
        return _centerPosition.position;
    }
}
