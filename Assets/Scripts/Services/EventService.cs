using System;

/* Class for creating all events occuring in the Scene (Observer Pattern) */

public class EventService : GenericMonoSingleton<EventService>
{
    public event Action OnSlotsFull;
    public event Action<ChestController> OnChestSpawn;
    public event Action<ChestController> OnChestClicked;

    public void InvokeOnSlotsFull()
    {
        OnSlotsFull?.Invoke();
    }
    public void InvokeOnChestSpawn(ChestController chestController)
    {
        OnChestSpawn?.Invoke(chestController);
    }
    public void InvokeOnChestClicked(ChestController chestController)
    {
        OnChestClicked?.Invoke(chestController);
    }
}
