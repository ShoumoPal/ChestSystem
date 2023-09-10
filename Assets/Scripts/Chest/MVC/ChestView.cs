using UnityEngine;
using UnityEngine.EventSystems;

/* Chest View for MVC */

public class ChestView : MonoBehaviour, IPointerClickHandler
{ 
    private ChestController _chestController;

    public ChestController GetChestController()
    {
        return _chestController;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundService.Instance.PlayClip(SoundType.ButtonClick);
        EventService.Instance.InvokeOnChestClicked(_chestController);
    }

    public void SetChestController(ChestController chestController)
    {
        _chestController = chestController;
    }
}
