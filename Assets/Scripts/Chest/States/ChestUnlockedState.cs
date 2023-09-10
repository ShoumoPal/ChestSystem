
using System;

public class ChestUnlockedState : ChestBaseState
{
    public ChestUnlockedState(ChestSM chestSM) : base(chestSM) { }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        SetTimerText();
    }

    private void SetTimerText()
    {
        ChestController chest = chestSM.GetChestController();
        ChestSlot slot = Array.Find(ChestSlotService.Instance.ChestSlots, i => i.ChestController == chest);
        slot.TimerText.text = "Open";
    }
}
