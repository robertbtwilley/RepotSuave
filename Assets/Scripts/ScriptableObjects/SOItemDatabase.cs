using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNCore;

[CreateAssetMenu]
public class SOItemDatabase : ScriptableObject
{
    public SOItem[] AllSOItems;
    public SOShip[] AllSOShips;
    public SOModule[] AllSOModules;
    public SOWeapon[] AllSOWeapons;

}
