using UnityEngine;

/* Chest Controller for MVC */

public class ChestController
{
    private ChestView _chestView;
    private ChestModel _chestModel;

    public ChestController(ChestView chestView, ChestModel chestModel)
    {
        _chestView = chestView;
        _chestModel = chestModel;
    }

    public void UnlockChest()
    {
        int gemCount = _chestModel.MAX_GEMS_TO_UNLOCK;
        if (ChestService.Instance._currentGems >= gemCount)
        {
            EventService.Instance.InvokeOnUpdateCurrency(0, gemCount);
            _chestModel.ChestState = ChestState.UNLOCKED;
            StartChestParticle();
        }
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
