using System;
using System.Collections;
using UnityEngine;

public class EnemySpawnQueue : SpawnQueue<EnemyUnitData>
{
    float _spawnCoolDown;
    float _currentCoolDown;
    public int index;

    private void Update()
    {

        if (_currentCoolDown>= _spawnCoolDown)
        {
            UnitEnqueue(EnemySpawnManager.Instance.EnemyUnitSpawner.Units[index]);
        }


        if (_queue.Count != 0 && _nextSpawnUnit == null)
        {
            StartCoroutine(C_SpawnCool());
        }
    }

    IEnumerator C_SpawnCool()
    {
        float spawnCool = 0f;
        _nextSpawnUnit = _queue.Peek(); // 소환대기 유닛

        while (spawnCool < _nextSpawnUnit.SpawnCoolDown)
        {
            spawnCool += Time.deltaTime;
            yield return null;
        }

        EnemySpawnManager.Instance.EnemyUnitSpawner.Spawn(_nextSpawnUnit.UnitType);

        _queue.Dequeue();
        _nextSpawnUnit = null;


    }


}
