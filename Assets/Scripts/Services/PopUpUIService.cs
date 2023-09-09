using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* Service for our PopUp UI */

public class PopUpUIService : GenericMonoSingleton<PopUpUIService>
{
    [SerializeField] private GameObject _popUp;
    private Button okButton;

    private void Start()
    {
        EventService.Instance.OnSlotsFull += OnSlotsFull;
    }
    public void OnSlotsFull()
    {
        TextMeshProUGUI popUpText = _popUp.GetComponentInChildren<TextMeshProUGUI>();
        okButton = _popUp.GetComponentInChildren<Button>();
        popUpText.text = "Slots are full! Unlock some chest first.";
        okButton.onClick.AddListener(ClearPopUp);
        _popUp.SetActive(true);
    }
    public void ClearPopUp()
    {
        _popUp.SetActive(false);
    }
    private void OnDisable()
    {
        EventService.Instance.OnSlotsFull -= OnSlotsFull;
    }
}
