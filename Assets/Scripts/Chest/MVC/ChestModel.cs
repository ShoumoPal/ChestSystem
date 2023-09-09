using UnityEngine;

public enum ChestType
{
    COMMON,
    RARE,
    EPIC,
    LEGENDARY
}

public class ChestModel
{
    private ChestController _chestController;

    public ChestController GetChestController() 
    { 
        return _chestController; 
    }
    public void SetChestController(ChestController chestController)
    {
        _chestController = chestController;
    }
}
