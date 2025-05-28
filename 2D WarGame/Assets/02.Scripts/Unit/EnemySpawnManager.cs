using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    // ����
    public EnemySpawnQueue EnemySpawnQueue;       // ���� ���� ť�� ����
    public EnemyUnitSpawner EnemyUnitSpawner;     // ���� ����

    int _baseMineralGen;


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
        _baseMineralGen = 50;

        EnemySpawnQueue = GetComponent<EnemySpawnQueue>();
        EnemyUnitSpawner = GetComponent<EnemyUnitSpawner>();


    }
    #endregion

    public void Update()
    {
        EnemyMineral += _baseMineralGen;
    }
}