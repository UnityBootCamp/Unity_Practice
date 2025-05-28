using System;
using UnityEngine;


public class UnitData : ScriptableObject
{
    [field: SerializeField] public GameObject UnitPrefab { get; set; }      // ���� ������
    [field: SerializeField] public int Cost { get; set; }                   // ���� �ڽ�Ʈ
    [field: SerializeField] public float SpawnCoolDown { get; set; }        // ���� ������ �ð�
    [field: SerializeField] public float Hp { get; set; }                   // ���� ü��
    [field: SerializeField] public float AttackForce { get; set; }          // ���� ���ݷ�
    [field: SerializeField] public float MoveSpeed { get; set; }            // ���� �̵��ӵ�
    [field: SerializeField] public float AttackDelay { get; set; }          // ���� ���ݵ�����


    
}
