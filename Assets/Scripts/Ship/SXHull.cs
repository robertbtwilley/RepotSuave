using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNCore;

public class SXHull : MonoBehaviour
{
    [SerializeField] SEHullPoint hullPointType;
    //Most hulls have just one primary mesh, one mesh collider.  Engineering has multiple mesh (probably too many), but wanted an example
    public SXHardSlot[] HullHardSlots; //gets set on the prefab
    [SerializeField] MeshCollider[] meshColliders; //gets set on the prefab
    [SerializeField] SXHullComponent[] hullComponents; //should be as large as the mesh colliders, gets set on the prefab;

    public SEHullPoint HullPointType
    {
        get { return hullPointType; }
        set { hullPointType = value; }
    }

    public List<SXHardPoint> ActiveHardPoints = new List<SXHardPoint>();

}
