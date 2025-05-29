using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyUnitSpawner : UnitSpawner<EnemyUnitData>
{


    EnemyUnit _prevEnemyUnit;                           // ���� ���� ����


    // ��������
    Vector3 _spawnPos = new Vector3(19f, -3.1f, 0f);    // ���� ��ġ
    int _totalWeight;                                   // �� ����ġ
    float _spawnCoolDown;                               // ���� ���� ��
    float _currentCoolDown;                             // ���� �����ð�

    bool _isOnSpawnCool;

    protected override void Start()
    {
        base.Start();

        _isOnSpawnCool = true;
        _spawnCoolDown = 1.2f;

        for (int i = 0; i < _units.Count; i++)
        {
            PoolManager.Instance.CreatePool(_units[i].UnitType.ToString(), () => Instantiate(_units[i].UnitPrefab, _spawnPos, Quaternion.identity));
        }

        for(int i =0; i<_units.Count; i++)
        {
            _totalWeight += _units[i].Weight;
        }

    }

    private void Update()
    {
        // ������ �������� ������, ���� �� ������ ������ ������ �̳׶��� �����ϰ� �ִٸ�
        if (EnemySpawnManager.Instance.EnemyMineral>= Units[0].Cost&& _isOnSpawnCool && EnemySpawnManager.Instance.IsCanSpawnUnit)
        {
            EnemyUnitData randomEnemyUnitData = null;

            
            randomEnemyUnitData = ChooseRandomUnit(); 

               
            //  ������ ���� �Ҵ��� �ȵǸ�
            if(CanSpawn(randomEnemyUnitData) == false || randomEnemyUnitData == null)
            {
                StartCoroutine(C_SpawnCool()); // ���� �� ����
                return;
            }

            EnemySpawnManager.Instance.UnitList.UnitsCount[(int)randomEnemyUnitData.UnitType]++;


            EnemySpawnManager.Instance.EnemyMineral -= randomEnemyUnitData.Cost;
            EnemySpawnManager.Instance.EnemySpawnQueue.UnitEnqueue(randomEnemyUnitData);
            _currentCoolDown = 0;
        }

    }

    IEnumerator C_SpawnCool()
    {
        _isOnSpawnCool = false;
        yield return new WaitForSeconds(2f);
        _isOnSpawnCool = true;
    }

    public bool CanSpawn(EnemyUnitData enemyUnitData)
    {
        if(enemyUnitData.Cost<= EnemySpawnManager.Instance.EnemyMineral)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public EnemyUnitData ChooseRandomUnit()
    {
        int currentWeight=0;
        int randInt = Random.Range(0, _totalWeight);

        foreach(var unit in Units)
        {
            currentWeight += unit.Weight;
            if(randInt<currentWeight)
            {
                return unit;
            }
        }
        return Units[0];
    }

    public void Spawn(EnemyUnitType unitType)
    {
        EnemyUnit spawnedUnit = PoolManager.Instance.Get(unitType.ToString()).transform.GetChild(0).GetComponent<EnemyUnit>();

        // ������ ������ �� ������ �����ϰ� ��
        if (_prevEnemyUnit == null)
        {
            spawnedUnit.SetPrevUnit(spawnedUnit);
        }
        else
        {
            spawnedUnit.SetPrevUnit(_prevEnemyUnit);
        }

        // ���� ���ָ���Ʈ�� ����
        if (EnemySpawnManager.Instance.UnitList.SpawnedBattleUnit.Count ==0)
        {
            // ���� ���� ���� ���� ������ UnitAttackManager���� �����ϰ� �ְ� ��
            UnitAttackManager.Instance.SetEnemyFirstUnit(spawnedUnit);
        }

        EnemySpawnManager.Instance.UnitList.EnqueueUnitList(spawnedUnit);


        // ���� ���� ������ ����
        _prevEnemyUnit = spawnedUnit;


    }



}
