using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnQueue<T> : MonoBehaviour
    where T :UnitData
{
    protected T _nextSpawnUnit;

    protected Queue<T> _queue;
    public Queue<T> Queue => _queue;




    private void Awake()
    {
        _queue = new Queue<T>(5);
        
    }


    public void UnitEnqueue(T unitData)
    {
        _queue.Enqueue(unitData);

    }


}
