using UnityEngine;
using SNCore;
/* - TurretBay is typical turret mount that is not concealed RailGuns and large Weapon should be mounted on TurretBays - */
/* - PDCBay is generally housed inside parent bay.   The stand alone PDCBay is generally found on a Deck weapon point - */
/* - PDCGroups are usually a pair (Starboard / Port) of PDC Turrets - */
/* - FixedDirectionBays are keel or aft mounted weapons that can only target ships based on the heading their ship - */
/* - LauncherBays are torpedo and missile systems that launch from fixed direction tubes and rely on missile tracking systems to find targets - */



[CreateAssetMenu]
public class SOTurret : SOWeapon
{
    
    [SerializeField] float maxPivotAngle;
    [SerializeField] float maxTurnSpeed;
    
    #region ACCESSORS

    public float MaxPivotAngle { get { return maxPivotAngle; } set { maxPivotAngle = value; } }
    public float MaxTurnSpeed { get { return maxTurnSpeed; } set { maxTurnSpeed = value; } }

    #endregion ACCESSORS

    private void Awake()
    {
        ItemSOType = SEItemType.Module;
    }



}
