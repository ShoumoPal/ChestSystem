using System;
using UnityEngine;
using UnityEngine.UI;

public enum SlotType
{
    EMPTY,
    FILLED
}

[Serializable]

/* Class for each chest slot */

public class ChestSlot
{
    public GameObject Slot;
    public GameObject EmptyText;
    public SlotType SlotType;
}

/* Chest slot service for taking care of Chest Slots */

public class ChestSlotService : GenericMonoSingleton<ChestSlotService>
{
    public ChestSlot[] ChestSlots;

    public void SetSlotType(ChestSlot chestSlot, SlotType slotType)
    {
        ChestSlot slot = Array.Find(ChestSlots, i => i.Slot == chestSlot.Slot);
        slot.SlotType = slotType;
    }
}
