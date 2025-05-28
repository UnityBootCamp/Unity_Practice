
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class SpawnedPlayerUnitList
{

   

    public List<int> UnitsCount = new List<int>(6);

    public SpawnedPlayerUnitList()
    {
        for(int i=0; i<UnitsCount.Capacity; i++)
        {
            UnitsCount.Add(0);
        }
    }


    public void Spawn(string unitName)
    {
        switch (unitName)
        {
            case "Farmer":
                UnitsCount[0]++;
                break;
            case "SwordMan":
                UnitsCount[1]++;
                break;
            case "Archer":
                UnitsCount[2]++;
                break;
            case "Paladin":
                UnitsCount[3]++;
                break;
            case "Wizard":
                UnitsCount[4]++;
                break;
            case "Knight":
                UnitsCount[5]++;
                break;
            default:
                break;
        }

    }
    public int TotalFarmingUnitCount()
    {
        return UnitsCount[0];
    }

    public int TotalUnitCount()
    {
        int res = 0;

        for (int i = 1; i < UnitsCount.Capacity; i++)
        {
            res += UnitsCount[i];
        }

        return res;
    }
}