using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Enemy _currentEnemy;
    [SerializeField] private Box _obstacle;
    [SerializeField] private SpikeTrap _spikeTrap;

    [SerializeField] private Transform _centerPosition;

    public void SetEnemy(Enemy enemy)
    {
        _currentEnemy = enemy;
    }

    public void SetObstacle(Box obstacle)
    {
        _obstacle = obstacle;
    }

    public void SetSpikeTrap(SpikeTrap spikeTrap)
    {
        _spikeTrap = spikeTrap;
    }

    public void ClearTile()
    {
        if (_currentEnemy == null) return;

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

    public bool TryGetSpikeTrap(out SpikeTrap spikeTrap)
    {
        if (_spikeTrap != null)
        {
            spikeTrap = _spikeTrap;
            return true;
        }

        spikeTrap = null;
        return false;
    }

    public Vector3 GetTargetPosition()
    {
        return _centerPosition.position;
    }
}
