using UnityEngine;

public class UnitSize:MonoBehaviour
{
    [SerializeField] GameObject[] _playerUnits;

    public static Vector3[] UnitSizes = new Vector3[5];


    private void Awake()
    {
        for(int i=0; i<_playerUnits.Length; i++)
        {
            UnitSizes[i] = CalcSpriteScale(_playerUnits[i]);
            Debug.Log(UnitSizes[i]);
        }        
    }



    Vector3 CalcSpriteScale(GameObject playerUnit)
    {
        Bounds totalBounds = new Bounds(playerUnit.transform.position, Vector3.zero);

        foreach (var sr in playerUnit.GetComponentsInChildren<SpriteRenderer>())
        {
            totalBounds.Encapsulate(sr.bounds); // 각 스프라이트의 영역을 통합
        }

        return totalBounds.size;
    }
}