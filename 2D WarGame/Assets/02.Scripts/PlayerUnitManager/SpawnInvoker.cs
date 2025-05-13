using UnityEngine;
using UnityEngine.Events;

public class SpawnInvoker : MonoBehaviour
{
    public IntEvent onSpawn;

    [System.Serializable]
    public class IntEvent : UnityEvent<int> { }

    public void InvokeSpawn(int type)
    {
        onSpawn.Invoke(type);
    }
}