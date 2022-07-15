using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSPlayerDataManifest : MonoBehaviour
{
    public static SSPlayerDataManifest PlayerDataInstance { get; private set; }

    public void Awake() //Singleton
    {
        if (PlayerDataInstance == null)
        {
            PlayerDataInstance = this;
        }

        else
        {
            if (PlayerDataInstance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    public List<SUShipSlot> playerShipSlots;
    public List<SUModuleSlot> playerModulesSlots;
    
}
