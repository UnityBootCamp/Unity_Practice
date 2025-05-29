using UnityEngine;

public class UnitSize : MonoBehaviour
{


    protected Vector3 CalcSpriteScale(GameObject unit)
    {
        Bounds totalBounds = new Bounds(unit.transform.position, Vector3.zero);

        foreach (var sr in unit.GetComponentsInChildren<SpriteRenderer>())
        {
            totalBounds.Encapsulate(sr.bounds); // �� ��������Ʈ�� ������ ����
        }

        return totalBounds.size;
    }
}