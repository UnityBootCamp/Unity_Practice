using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnQueue : MonoBehaviour
{
    PlayerUnitData _nextSpawnUnit;

    Queue<PlayerUnitData> _queue;
    public Queue<PlayerUnitData> Queue => _queue;




    private void Awake()
    {
        _queue = new Queue<PlayerUnitData>(5);
    }

    private void Update()
    {
        if (_queue.Count != 0 && _nextSpawnUnit == null)
        {
            StartCoroutine(C_SpawnCool());

        }
    }

    IEnumerator C_SpawnCool()
    {
        float spawnCool = 0f;
        _nextSpawnUnit = _queue.Peek(); // 소환대기 유닛
        
        while(spawnCool< _nextSpawnUnit.SpawnCoolDown)
        {
            PlayerSpawnManager.Instance.SetSlider(spawnCool / _nextSpawnUnit.SpawnCoolDown);
            spawnCool += Time.deltaTime;
            yield return null;
        }

        
        PlayerSpawnManager.Instance.SetAfterSpawn(_nextSpawnUnit.UnitType);

        _queue.Dequeue();
        _nextSpawnUnit = null;


    }


    public void UnitEnqueue(PlayerUnitData unitData)
    {
        _queue.Enqueue(unitData);

    }


}
