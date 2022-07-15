using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNCore;
using System;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using PygmyMonkey.ColorPalette;


public class SUModuleSlot : MonoBehaviour, IPointerDownHandler
{
    public event Action<SOModule> OnModuleSlotClickEvent;

    public Image SlotModuleIcon;
    public GameObject SlotOverlayGO;
    public TextMeshProUGUI LevelLabelTMP;
    public TextMeshProUGUI LevelValueTMP;
    public SUSlotOverlay SlotOverlay;
    public SUInventoryPanel InventoryPanel;

    [SerializeField] SOModule moduleSO;

    public SOModule ModuleSO
    {
        get { return moduleSO; }
        set
        {
            moduleSO = value;

            if (value == null)
            {
                SlotModuleIcon.sprite = null;
                SlotModuleIcon.enabled = false;
                SetUISlotStyle(true);
            }
            else
            {
                SlotModuleIcon.sprite = moduleSO.itemIcon;
                SlotModuleIcon.enabled = true;
                SetUISlotStyle(false);
                LevelLabelTMP.enabled = false;
                LevelValueTMP.enabled = false;
                LevelValueTMP.text = moduleSO.ItemLevel.ToString();
            }
        }
    }

    public virtual void SetUISlotStyle(bool isEmpty)
    {

    }

    public void ActivateSlotOverlay(bool isActive)
    {
        SlotOverlayGO.SetActive(isActive);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Left)
        {
            if (ModuleSO != null && OnModuleSlotClickEvent != null)
            {
                OnModuleSlotClickEvent(moduleSO);
            }
        }
    }
}


