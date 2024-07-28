using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private bool _isOccuppied;
    [SerializeField] private Enemy _currentEnemy;
    [SerializeField] private Obstacle _obstacle;

    [SerializeField] private Transform _centerPosition;

    public void SetEnemy(Enemy enemy)
    {
        _currentEnemy = enemy;
        _isOccuppied = true;
    }

    public void ClearTile()
    {
        _currentEnemy = null;
        _isOccuppied = false;
    }

    public bool IsOccuppied()
    {
        return _isOccuppied;
    }

    public Enemy GetEnemy()
    {
        return _currentEnemy;
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
