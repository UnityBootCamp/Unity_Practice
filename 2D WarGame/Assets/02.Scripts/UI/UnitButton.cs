using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour
{
    [SerializeField] Button[] _buttons;
    PlayerUnitSpawner _unitSpawner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _unitSpawner = FindFirstObjectByType<PlayerUnitSpawner>();
    }

    
}
