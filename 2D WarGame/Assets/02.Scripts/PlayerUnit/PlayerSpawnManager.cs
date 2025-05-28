using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    // ����
    [HideInInspector] public SpawnQueue SpawnQueue;                   // ���� ���� ť�� ����
    [HideInInspector] public PlayerUnitSpawner PlayerUnitSpawner;     // ���� ����

    // ������ ������ ���ڸ� �����ִ� UI
    [SerializeField] ResourceUI _resourceUI;
    [SerializeField] SpawnQueueUI _spawnQueueUI;


    // �÷��̾� �����ڿ�
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

    // ������ ������������ Ȯ���ϴ� bool
    public bool IsCanSpawnUnit => PlayerUnitSpawner.UnitList.TotalUnitCount() < PlayerUnitSpawner.MaxUnitCount;
    public bool IsCanSpawnFarmingUnit => PlayerUnitSpawner.UnitList.TotalFarmingUnitCount() < PlayerUnitSpawner.MaxFarmingUnitCount;

    #region �̱���
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

    #region ���ҽ� UI
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


    #region ���� ť UI
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