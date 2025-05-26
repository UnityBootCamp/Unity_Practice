using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnQueue : MonoBehaviour
{
    SpawnQueueUI _spawnQueueUI;
    PlayerUnitData _nextSpawnUnit;

    Queue<PlayerUnitData> _queue;
    public Queue<PlayerUnitData> Queue => _queue;




    private void Awake()
    {
        _queue = new Queue<PlayerUnitData>(5);
        _spawnQueueUI = GetComponent<SpawnQueueUI>();
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
        _nextSpawnUnit = _queue.Peek();
        
        while(spawnCool< _nextSpawnUnit.SpawnCoolDown)
        {
            _spawnQueueUI.SetSlider(spawnCool / _nextSpawnUnit.SpawnCoolDown);
            spawnCool += Time.deltaTime;
            yield return null;
        }

        _spawnQueueUI.SetSlider(1);

        _queue.Dequeue();
        _spawnQueueUI.UpdateQueue(_nextSpawnUnit.UnitType);
        _nextSpawnUnit = null;
        _spawnQueueUI.WaitingUnits--;

    }


    public void UnitEnqueue(PlayerUnitData unitData)
    {
        _queue.Enqueue(unitData);

    }


}
