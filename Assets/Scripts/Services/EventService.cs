using System;

public class EventService : GenericMonoSingleton<EventService>
{
    public event Action OnSlotsFull;

    public void InvokeOnSlotsFull()
    {
        OnSlotsFull?.Invoke();
    }
}
