using TMPro;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    // For Test
    [SerializeField] TextMeshProUGUI EnemyMineralText;

    public EnemySpawnedUnitList UnitList = new EnemySpawnedUnitList();



    // ����
    public EnemySpawnQueue EnemySpawnQueue;       // ���� ���� ť�� ����
    public EnemyUnitSpawner EnemyUnitSpawner;     // ���� ����

    int _baseMineralGen;
    float _mineralGainCool;
    float _currentMineralGainCool;

    // �� �����ڿ�
    public int EnemyMineral
    {
        get
        {
            return _mineral;
        }
        set
        {
            _mineral = value;
        }
    }
    int _mineral;

    // ������ ������������ Ȯ���ϴ� bool


    #region �̱���
    public static EnemySpawnManager Instance => _instance;

    static EnemySpawnManager _instance;

    private void Awake()
    {
        _instance = this;

        EnemyMineral = 200;
        _baseMineralGen = 0;
        _mineralGainCool = 1f;

        EnemySpawnQueue = GetComponent<EnemySpawnQueue>();
        EnemyUnitSpawner = GetComponent<EnemyUnitSpawner>();


    }
    #endregion

    public void Update()
    {
        _currentMineralGainCool += Time.deltaTime;
        if (_currentMineralGainCool > _mineralGainCool)
        {
            EnemyMineral += _baseMineralGen;
            _currentMineralGainCool = 0;
        }

        EnemyMineralText.text = EnemyMineral.ToString();
    }
}