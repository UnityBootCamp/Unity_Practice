using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    // ����
    [HideInInspector] public PlayerSpawnQueue PlayerSpawnQueue;       // ���� ���� ť�� ����
    [HideInInspector] public PlayerUnitSpawner PlayerUnitSpawner;     // ���� ����

    // ������ ������ ���ڸ� �����ִ� UI
    [SerializeField] PlayerResourceUI _resourceUI;
    [SerializeField] PlayerSpawnQueueUI _spawnQueueUI;

    public PlayerSpawnedUnitList UnitList = new PlayerSpawnedUnitList();

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
    public bool IsCanSpawnUnit => UnitList.TotalUnitCount() < PlayerUnitSpawner.MaxUnitCount;
    public bool IsCanSpawnFarmingUnit => UnitList.TotalFarmingUnitCount() < PlayerUnitSpawner.MaxFarmingUnitCount;

    #region �̱���
    public static PlayerSpawnManager Instance => _instance;

    static PlayerSpawnManager _instance;

    private void Awake()
    {
        _instance = this;

        Mineral = 5000;

        PlayerSpawnQueue = GetComponent<PlayerSpawnQueue>();
        PlayerUnitSpawner = GetComponent<PlayerUnitSpawner>();


        UpdateUnitResourceUI();
        UpdateFarmingUnitResourceUI();
    }

    #endregion

    #region ���ҽ� UI
    public void UpdateUnitResourceUI()
    {
        _resourceUI.UpdateUnitResource
            ($"{UnitList.TotalUnitCount()}/{PlayerUnitSpawner.MaxUnitCount}");
    }
    public void UpdateFarmingUnitResourceUI()
    {
        _resourceUI.UpdateFarmingUnitResource
            ($"{UnitList.TotalFarmingUnitCount()}/{PlayerUnitSpawner.MaxFarmingUnitCount}");
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