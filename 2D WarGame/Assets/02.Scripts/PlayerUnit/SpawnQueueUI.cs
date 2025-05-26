using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpawnQueueUI : MonoBehaviour
{
    [SerializeField] List<Image> _unitPortraits;
    [SerializeField] Slider _spawnSlider;

    PlayerUnitSpawner _playerUnitSpawner;
    SpawnQueue _spawnQueue;

    Sprite _unitPortrait;

    public int WaitingUnits;


    private void Awake()
    {
        _playerUnitSpawner = FindFirstObjectByType<PlayerUnitSpawner>();
        _spawnQueue = GetComponent<SpawnQueue>();

        _spawnSlider.value = 0;
        
        for(int i =0; i<_unitPortraits.Capacity; i++)
        {
            _unitPortraits[i].sprite = null;
        }
    }

    public void SetSlider(float value)
    {
        _spawnSlider.value = value;
    }

    public void OnSpawn(int index)
    {
        if (WaitingUnits < 5 && _playerUnitSpawner.unitList.UnitCount(index)<5)
        {
            _unitPortrait = _playerUnitSpawner.Units[index].UnitPortrait;
            _unitPortraits[WaitingUnits].sprite = _unitPortrait;
            _spawnQueue.UnitEnqueue(_playerUnitSpawner.Units[index]);
            WaitingUnits++;
            _playerUnitSpawner.unitList.UnitsCount[index]++;
        }

    }
    
    public void UpdateQueue(PlayerUnitType unitType)
    {
        _playerUnitSpawner.Spawn(unitType);
        for(int i=0; i<_unitPortraits.Count-1; i++)
        {
            _unitPortraits[i].sprite = _unitPortraits[i + 1].sprite;
        }
        _unitPortraits[_unitPortraits.Count - 1].sprite = null;
        SetSlider(0);
    }

}
