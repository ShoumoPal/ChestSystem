using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/* Service for our PopUp UI */

public class PopUpUIService : GenericMonoSingleton<PopUpUIService>
{
    [SerializeField] private GameObject _popUp;
    [SerializeField] private GameObject _backGroundUI;
    [SerializeField] private Button _okButton;
    [SerializeField] private Button _unlockButton;
    [SerializeField] private Button _startTimerButton;
    [SerializeField] private Button _backButton;

    private TextMeshProUGUI _popUpText;
    private ChestController _currentChest;

    private void Start()
    {
        _popUpText = _popUp.GetComponentInChildren<TextMeshProUGUI>();
        _okButton = _popUp.GetComponentInChildren<Button>();
        _okButton.onClick.AddListener(ClearPopUp);
        _backButton.onClick.AddListener(ClearPopUp);
        _unlockButton.onClick.AddListener(UnlockChest);
        _startTimerButton.onClick.AddListener(StartUnlockTimer);

        EventService.Instance.OnSlotsFull += OnSlotsFull;
        EventService.Instance.OnChestSpawn += ChestSpawned;
        EventService.Instance.OnChestClicked += ChestClicked;
    }

    private void StartUnlockTimer()
    {
        _currentChest.GetChestModel().ChestState = ChestState.UNLOCKING;
    }

    public void UnlockChest()
    {
        Debug.Log(ChestService.Instance);
        int gemCount = _currentChest.GetChestModel().MAX_GEMS_TO_UNLOCK;
        if (ChestService.Instance._currentGems >= gemCount)
        {
            EventService.Instance.InvokeOnUpdateCurrency(0, gemCount);
            _currentChest.GetChestModel().ChestState = ChestState.UNLOCKED;
            _currentChest.GetChestSM().ChangeState(ChestState.UNLOCKED);
            _currentChest.StartChestParticle();

            ClearPopUp();
        }
        else
        {
            ShowNotEnoughGems();
        }
    }

    private void ShowChestRewards(int coins, int gems)
    {
        _popUpText.text = "Chest opened!\n\nRewards:\n\nCoins: " + coins + "\nGems: " + gems;
        _popUp.SetActive(true);
        DisableAllButtons();
        _okButton.gameObject.SetActive(true);
    }

    private void DisableAllButtons()
    {
        _okButton.gameObject.SetActive(false);
        _backButton.gameObject.SetActive(false);
        _unlockButton.gameObject.SetActive(false);
        _startTimerButton.gameObject.SetActive(false);
    }
    private void ShowNotEnoughGems()
    {
        _popUpText.text = "Not enough gem!";
        _popUp.SetActive(true);

        DisableAllButtons();
        _okButton.gameObject.SetActive(true);
    }

    private void ChestClicked(ChestController chestController)
    {
        ChestState state = chestController.GetChestModel().ChestState;
        _currentChest = chestController;

        switch (state)
        {
            case ChestState.LOCKED: //Locked state click

                _popUpText.text = "Start timer or unlock with gems?";
                _unlockButton.GetComponentInChildren<TextMeshProUGUI>().text = "Unlock " + chestController.GetChestModel().MAX_GEMS_TO_UNLOCK + " <sprite name=\"gem\">";
                _popUp.gameObject.SetActive(true);

                // Modifying buttons
                _okButton.gameObject.SetActive(false);
                _unlockButton.gameObject.SetActive(true);
                _startTimerButton.gameObject.SetActive(true);
                _backButton.gameObject.SetActive(true);

                break;

            case ChestState.UNLOCKED: //Unlocked State Click
                int randomCoins = Random.Range(_currentChest.GetChestModel().COINS_RANGE.x, _currentChest.GetChestModel().COINS_RANGE.y);
                int randomGems = Random.Range(_currentChest.GetChestModel().GEMS_RANGE.x, _currentChest.GetChestModel().GEMS_RANGE.y);
                ChestSlot slot = Array.Find(ChestSlotService.Instance.ChestSlots, i => i.ChestController == _currentChest);

                //Destroying chest object
                Destroy(_currentChest.GetChestView().gameObject);

                slot.SlotType = SlotType.EMPTY;
                slot.TimerText.text = "";
                ChestService.Instance.SetEmptyText(slot);

                ShowChestRewards(randomCoins, randomGems);
                EventService.Instance.InvokeOnUpdateCurrency(-randomCoins, -randomGems);

                break;
        }
    }

    private void ChestSpawned(ChestController chestController)
    {
        // Setting background to non-interactable
        _backGroundUI.GetComponent<CanvasGroup>().blocksRaycasts = false;

        ChestType type = chestController.GetChestModel().ChestType;
        Vector2Int coinsRange = chestController.GetChestModel().COINS_RANGE;

        switch (type)
        {
            case ChestType.COMMON:
                _popUpText.text = "Common Chest Obtained!\nCoins range: " + coinsRange.x + " - " + coinsRange.y;
                break;
            case ChestType.RARE:
                _popUpText.text = "Rare Chest Obtained!\nCoins range: " + coinsRange.x + " - " + coinsRange.y;
                break;
            case ChestType.EPIC:
                _popUpText.text = "Epic Chest Obtained!\nCoins range: " + coinsRange.x + " - " + coinsRange.y;
                break;
            case ChestType.LEGENDARY:
                _popUpText.text = "Legendary Chest Obtained!\nCoins range: " + coinsRange.x + " - " + coinsRange.y;
                break;
        }

        // Activating pop-up and buttons
        _popUp.SetActive(true);
        _okButton.gameObject.SetActive(true);

        // Deactivating unused buttons
        _unlockButton.gameObject.SetActive(false);
        _startTimerButton.gameObject.SetActive(false);
        _backButton.gameObject.SetActive(false);
    }

    public void OnSlotsFull()
    {
        _popUpText.text = "Slots are full! Unlock some chest first.";
        _popUp.SetActive(true);
    }
    public void ClearPopUp()
    {
        SoundService.Instance.PlayClip(SoundType.ButtonClick);
        _backGroundUI.GetComponent<CanvasGroup>().blocksRaycasts = true;
        _popUp.SetActive(false);
    }
    private void OnDisable()
    {
        EventService.Instance.OnSlotsFull -= OnSlotsFull;
        EventService.Instance.OnChestSpawn -= ChestSpawned;
    }
}
