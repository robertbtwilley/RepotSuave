using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SX_UI_Menu_Controller : MonoBehaviour
{
    public SX_UI_Menu_Section DefaultSection;
    public SX_UI_Menu_Section ActiveSection;
    

    [SerializeField] SX_UI_Menu_Section[] menuSections;

    public void Start()
    {

        ActiveSection = DefaultSection;

        foreach (SX_UI_Menu_Section section in menuSections)
        {
            section.RefreshMenuSection();
        }

    }


    public void ToggleActiveSection (SX_UI_Menu_Section newActiveSection)
    {
        //This shouldn't be able to get called if the newActiveSection IS the ActiveSection, but adding a check, just in case.

        if (ActiveSection != newActiveSection)
        {
            ActiveSection = newActiveSection;

            foreach (SX_UI_Menu_Section section in menuSections)
            {
                section.RefreshMenuSection();
            }

        }

        else
            Debug.Log("You are trying to toggle a section to active, that may already be active.");

    }

}
