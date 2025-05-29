using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : IPool
{
    // Ǯ�� �θ� ������Ʈ Ʈ������ (�� �������� ���� �뵵)
    public Transform Trans { get; set; }

    // ������Ʈ�� �����ϱ� ���� ť (��Ȱ��ȭ�� ������Ʈ �����)
    private Queue<GameObject> pool = new();

    // ���� ������Ʈ�� �ߺ����� Release �Ǵ� ���� �����ϱ� ���� ����
    private readonly HashSet<GameObject> pooledSet = new();


    // Ǯ���� ������Ʈ�� ���� �� ���
    public GameObject Get(Action<GameObject> onGet = null)
    {
        if (pool.Count == 0)
        {
            Debug.LogWarning("ObjectPool is empty!");
            return null;
        }

        var obj = pool.Dequeue();         // ť���� �ϳ� ������
        pooledSet.Remove(obj);            // HashSet������ ����
        obj.SetActive(true);              // Ȱ��ȭ
        onGet?.Invoke(obj);               // ��ó�� �ݹ� (�ʱ� ��ġ ���� ��)
        return obj;
    }

    // ������Ʈ�� Ǯ�� ��ȯ (��Ȱ��ȭ, �θ� ������ ��)
    public void Release(GameObject obj, Action<GameObject> onRelease = null)
    {
        if (pooledSet.Contains(obj))
        {
            Debug.LogWarning("This object is already in the pool.");
            return;
        }

        obj.SetActive(false);             // ��Ȱ��ȭ
        obj.transform.SetParent(Trans);   // �θ� Ǯ�� ����
        pool.Enqueue(obj);                // ť�� �߰�
        pooledSet.Add(obj);               // �ߺ� ������ ���� ����
        onRelease?.Invoke(obj);           // ��ó�� �ݹ� (���� �ʱ�ȭ ��)
    }

    // ������Ʈ�� �̸� �����Ͽ� Ǯ�� �����صδ� �޼���
    public void Preload(Func<GameObject> factory, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var obj = factory.Invoke();   // ������ ����
            obj.SetActive(false);         // ��Ȱ��ȭ
            obj.transform.SetParent(Trans, worldPositionStays : true);
            pool.Enqueue(obj);            // Ǯ�� �߰�
            pooledSet.Add(obj);           // �ߺ� ������ HashSet�� ���
        }
    }
}