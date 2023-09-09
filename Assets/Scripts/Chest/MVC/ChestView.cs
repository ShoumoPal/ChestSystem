using UnityEngine;

/* Chest View for MVC */

public class ChestView : MonoBehaviour
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
