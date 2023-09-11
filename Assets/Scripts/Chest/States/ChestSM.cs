using UnityEngine;

public class ChestSM : MonoBehaviour
{
    private ChestController _chestController;

    private ChestBaseState _currentState = null;

    private ChestLockedState _chestLockedState;
    private ChestUnlockedState _chestUnlockedState;
    private ChestUnlockingState _chestUnlockingState;

    public ChestSM(ChestController chestController)
    {
        _chestController = chestController;
        _chestLockedState = new ChestLockedState(this);
        _chestUnlockedState = new ChestUnlockedState(this);
        _chestUnlockingState = new ChestUnlockingState(this);

        ChangeState(ChestState.LOCKED);
    }
    private void Update()
    {
        //_currentState?.Tick();
    }
    public void ChangeState(ChestState state)
    {
        ChestBaseState newChestState = GetChestStateFromEnum(state);

        if (_currentState == newChestState)
        {
            return;
        }
        _currentState?.OnStateExit();

        _currentState = newChestState;
        _chestController.GetChestModel().ChestState = state;
        _currentState.OnStateEnter();
    }
    public ChestBaseState GetChestStateFromEnum(ChestState state)
    {
        switch (state)
        {
            case ChestState.LOCKED:
                return _chestLockedState;
            case ChestState.UNLOCKED:
                return _chestUnlockedState;
            case ChestState.UNLOCKING:
                return _chestUnlockingState;
        }
        return null;
    }
    public ChestController GetChestController()
    {
        return _chestController;
    }
}
