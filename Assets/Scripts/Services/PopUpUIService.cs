using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        EventService.Instance.OnSlotsFull += OnSlotsFull;
        EventService.Instance.OnChestSpawn += ChestSpawned;
        EventService.Instance.OnChestClicked += ChestClicked;
    }

    public void UnlockChest()
    {
        Debug.Log(ChestService.Instance);
        int gemCount = _currentChest.GetChestModel().MAX_GEMS_TO_UNLOCK;
        if (ChestService.Instance._currentGems >= gemCount)
        {
            EventService.Instance.InvokeOnUpdateCurrency(0, gemCount);
            _currentChest.GetChestModel().ChestState = ChestState.UNLOCKED;
            _currentChest.StartChestParticle();

            ClearPopUp();
        }
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
                Debug.Log("Chest unlocked!");
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
