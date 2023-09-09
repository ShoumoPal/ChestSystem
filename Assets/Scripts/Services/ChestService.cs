using System;
using UnityEngine;
using UnityEngine.UI;

/* Chest Service for Spawning of Chests */

[RequireComponent(typeof(Button))]
public class ChestService : GenericMonoSingleton<ChestService>
{
    [SerializeField] private Button spawnButton;
    [SerializeField] private ChestScriptableObjectList ChestScriptableObjectList;

    private void Awake()
    {
        spawnButton.onClick.AddListener(SpawnChest);
    }

    private void SpawnChest()
    {
        for(int i = 0; i < ChestSlotService.Instance.ChestSlots.Length; i++)
        {
            ChestSlot slot = ChestSlotService.Instance.ChestSlots[i];
            if(slot.SlotType == SlotType.EMPTY)
            {
                ChestSlotService.Instance.SetSlotType(slot, SlotType.FILLED);
                GenerateRandomChest(slot);
                slot.EmptyText.SetActive(false);
                break;
            }
            if(i == ChestSlotService.Instance.ChestSlots.Length - 1)
            {
                EventService.Instance.InvokeOnSlotsFull();
            }
        }
    }

    private void GenerateRandomChest(ChestSlot _slot)
    {
        // Getting random SO from list
        int randomRange = (int)UnityEngine.Random.Range(0, ChestScriptableObjectList.ChestList.Length);
        ChestScriptableObject obj = ChestScriptableObjectList.ChestList[randomRange];

        // Setting all MVC components and Instantiating the view
        ChestModel model = new ChestModel(obj);
        ChestView view = GameObject.Instantiate<ChestView>(model.ChestView);
        view.transform.SetParent(_slot.Slot.transform);
        view.gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;

        // Linking all components
        ChestController newChest = new ChestController(view, model);
        view.SetChestController(newChest);
        model.SetChestController(newChest);
    }
}
