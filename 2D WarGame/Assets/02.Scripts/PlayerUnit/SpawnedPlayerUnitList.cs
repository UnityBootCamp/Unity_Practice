
using System.Collections.Generic;
using Unity.VisualScripting;

public class SpawnedPlayerUnitList
{

    public int FarmerCount;
    public int SwordManCount;
    public int ArcherCount;
    public int PaladinCount;
    public int KnightCount;

    public List<int> UnitsCount = new List<int>(5);

    public SpawnedPlayerUnitList()
    {
        FarmerCount = 0;
        SwordManCount = 0;
        ArcherCount = 0;
        PaladinCount = 0;
        KnightCount = 0;

        UnitsCount.Add(FarmerCount);
        UnitsCount.Add(SwordManCount);
        UnitsCount.Add(ArcherCount);
        UnitsCount.Add(PaladinCount);
        UnitsCount.Add(KnightCount);
    }

    public int UnitCount(int index)
    {
        return UnitsCount[index];
    }

    public void Spawn(string unitName)
    {
        switch (unitName)
        {
            case "Farmer":
                FarmerCount++;
                break;
            case "SwordMan":
                FarmerCount++;
                break;
            case "Archer":
                FarmerCount++;
                break;
            case "Paladin":
                FarmerCount++;
                break;
            case "Knight":
                FarmerCount++;
                break;
            default:
                break;
        }

    }
}