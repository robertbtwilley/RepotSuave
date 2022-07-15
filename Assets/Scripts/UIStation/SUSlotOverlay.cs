using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNCore;
using System;
using UnityEngine.EventSystems;

public class SUSlotOverlay : MonoBehaviour, IPointerDownHandler
{
    public event Action<SOModule, SUFittingSlot> OnFitSlotOverlayClickEvent;

    public SOModule moduleToFit;
    public SUFittingSlot parentSlot;



    //public void FitToParentSlot()
    //{
    //    parentSlot.parentFitPanel.FitModule(moduleToFit, parentSlot);
    //    parentSlot.parentFitPanel.FitInfoPopup.ClosePopup();
    //    parentSlot.parentFitPanel.ActivateSelectedSlotOverlay(moduleToFit);
    //}

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    if (eventData != null && eventData.button == PointerEventData.InputButton.Left)
    //    {
    //        if (moduleToFit != null && OnFitSlotOverlayClickEvent != null)
    //        {
    //            OnFitSlotOverlayClickEvent(moduleToFit, parentSlot);
    //        }

    //    }
    //}

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Left)
        {
            if (moduleToFit != null && OnFitSlotOverlayClickEvent != null)
            {
                OnFitSlotOverlayClickEvent(moduleToFit, parentSlot);
            }

        }
    }
}