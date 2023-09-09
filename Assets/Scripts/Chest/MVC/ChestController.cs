using UnityEngine;

public class ChestController
{
    private ChestView _chestView;
    private ChestModel _chestModel;

    public ChestController(ChestView chestView, ChestModel chestModel)
    {
        _chestView = chestView;
        _chestModel = chestModel;
    }

    public ChestView GetChestView()
    {
        return _chestView;

    }
    public ChestModel GetChestModel()
    {
        return _chestModel;
    }
}
