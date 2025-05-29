using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    private readonly Dictionary<string, IPool> poolDict = new();

    // �ܺο��� ���Թ޴� ������ �ε� �Լ� (Resource, Addressables ��� �и� ����)
    public Func<string, GameObject> resourceLoader;

    // �̱��� �ʱ�ȭ
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // ���ο� ������Ʈ Ǯ ���� (������ ���� �Լ��� �޾� �̸� preloadCount��ŭ ����)
    public void CreatePool(string key, Func<GameObject> factory, int preloadCount = 50)
    {
        if (poolDict.ContainsKey(key))
            return;

        // �ش� Ǯ�� ������Ʈ���� ���� �θ� ������Ʈ ����
        var root = new GameObject($"{key}_PoolRoot").transform;
        root.SetParent(transform);

        // Ǯ ���� �� �ʱ�ȭ
        var pool = new ObjectPool { Trans = root };
        pool.Preload(factory, preloadCount);


        // ��ųʸ��� ���
        poolDict.Add(key, pool);
    }

 

    // ������Ʈ�� Ǯ���� ���� ��ȯ (��θ� ���� ����)
    public GameObject Get(string key, Action<GameObject> onGet = null)
    {
        if (!poolDict.TryGetValue(key, out var pool))
        {
            Debug.LogWarning($"No pool found for key: {key}");
            return null;
        }

        return pool.Get(onGet);
    }

    // ������Ʈ�� Ǯ�� ��ȯ
    public void Release(string key, GameObject obj, Action<GameObject> onRelease = null)
    {
        if (!poolDict.TryGetValue(key, out var pool))
        {
            Debug.LogWarning($"No pool found for key: {key}");
            return;
        }

        pool.Release(obj, onRelease);
    }

    // ���� �Լ�: ���ҽ� ��� ������� ������Ʈ Ǯ ����
    public void CreatePoolFromPath(string path, int preloadCount = 5)
    {
        CreatePool(path, () => {
            var obj = resourceLoader?.Invoke(path);
            if (obj == null)
                throw new Exception($"Failed to load prefab at path: {path}");
            return obj;
        }, preloadCount);
    }
}