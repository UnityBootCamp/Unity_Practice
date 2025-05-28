using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    // 참조
    [HideInInspector] public SpawnQueue SpawnQueue;                   // 생산 예약 큐를 관리
    [HideInInspector] public PlayerUnitSpawner PlayerUnitSpawner;     // 유닛 생산

    // 생성된 유닛의 숫자를 보여주는 UI
    [SerializeField] ResourceUI _resourceUI;
    [SerializeField] SpawnQueueUI _spawnQueueUI;


    // 플레이어 보유자원
    public int Mineral
    {
        get
        {
            return _mineral;
        }
        set
        {
            _mineral = value;
            UpdateResourceUI();
        }
    }
    int _mineral;

    // 유닛이 생성가능한지 확인하는 bool
    public bool IsCanSpawnUnit => PlayerUnitSpawner.UnitList.TotalUnitCount() < PlayerUnitSpawner.MaxUnitCount;
    public bool IsCanSpawnFarmingUnit => PlayerUnitSpawner.UnitList.TotalFarmingUnitCount() < PlayerUnitSpawner.MaxFarmingUnitCount;

    #region 싱글톤
    public static PlayerSpawnManager Instance => _instance;

    static PlayerSpawnManager _instance;

    private void Awake()
    {
        _instance = this;

        Mineral = 200;

        SpawnQueue = GetComponent<SpawnQueue>();
        PlayerUnitSpawner = GetComponent<PlayerUnitSpawner>();


        UpdateUnitResourceUI();
        UpdateFarmingUnitResourceUI();
    }

    #endregion

    #region 리소스 UI
    public void UpdateUnitResourceUI()
    {
        _resourceUI.UpdateUnitResource
            ($"{PlayerUnitSpawner.UnitList.TotalUnitCount()}/{PlayerUnitSpawner.MaxUnitCount}");
    }
    public void UpdateFarmingUnitResourceUI()
    {
        _resourceUI.UpdateFarmingUnitResource
            ($"{PlayerUnitSpawner.UnitList.TotalFarmingUnitCount()}/{PlayerUnitSpawner.MaxFarmingUnitCount}");
    }
    public void UpdateResourceUI()
    {
        _resourceUI.UpdateResource(Mineral);
    }
    #endregion


    #region 스폰 큐 UI
    public void SetSlider(float value)
    {
        _spawnQueueUI.SetSlider(value);
    }

    public void SetAfterSpawn(PlayerUnitType unitType)
    {
        _spawnQueueUI.SetSlider(1);
        _spawnQueueUI.UpdateQueueUI(unitType);
        _spawnQueueUI.WaitingUnits--;
    }
    #endregion
}