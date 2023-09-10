using UnityEngine;

/* Chest Controller for MVC */

public class ChestController
{
    private ChestView _chestView;
    private ChestModel _chestModel;
    private ChestSM chestSM;

    public ChestController(ChestView chestView, ChestModel chestModel)
    {
        _chestView = chestView;
        _chestModel = chestModel;
    }

    public void SetChestSM(ChestController _chestController)
    {
        chestSM = new ChestSM(_chestController);
    }

    public ChestSM GetChestSM()
    {
        return chestSM;
    }

    public void StartChestParticle()
    {
        _chestView.StartParticle();
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
