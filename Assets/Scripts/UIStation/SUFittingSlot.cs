using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNCore;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using PygmyMonkey.ColorPalette;

public class SUFittingSlot : MonoBehaviour, IPointerDownHandler
{
    public SEHardPointLocation FittingPointLocation;
    public SEHullPoint HullPoint;
    public SEModuleType[] FittingAcceptableModuleTypes;
    [SerializeField] Image slotEmptyIcon;

    public Image FitSlotModuleIcon;
    public TextMeshProUGUI FSLevelLabelTMP;
    public TextMeshProUGUI FSLevelValueTMP;
    public SUSlotOverlay FitSlotOverlay;
    public SUFittingPanel parentFitPanel;

    [SerializeField] SOModule fitModuleSO;

    public SOModule FitModuleSO
    {
        get { return fitModuleSO; }
        set
        {
            fitModuleSO = value;

            if (value == null)
            {
                FitSlotModuleIcon.sprite = null;
                FitSlotModuleIcon.enabled = false;
                slotEmptyIcon.enabled = true;
            }
            else
            {
                slotEmptyIcon.enabled = false;
                FitSlotModuleIcon.sprite = fitModuleSO.itemIcon;
                FitSlotModuleIcon.enabled = true;
                //FSLevelLabelTMP.enabled = false;
                //FSLevelValueTMP.enabled = false;
                //FSLevelValueTMP.text = fitModuleSO.ItemLevel.ToString();
            }
        }
    }


    public event Action<SOModule, SUFittingSlot> OnFitSlotClickEvent;



    public void ActivateSlotOverlay(SOModule module, bool isActive)
    {
        FitSlotOverlay.moduleToFit = module;
        FitSlotOverlay.gameObject.SetActive(isActive);
        
    }

    public void TurnOnOverlay(SOModule module)
    {
        if (module != null)
        {
            FitSlotOverlay.moduleToFit = module;
        }

        FitSlotOverlay.gameObject.SetActive(true);
        //parentFitPanel.AddListeners(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Left)
        {
            if (FitModuleSO != null && OnFitSlotClickEvent != null)
            {
                OnFitSlotClickEvent(FitModuleSO, this);
            }
        }
    }
}


