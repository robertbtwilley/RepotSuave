using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSGameDataManifest : MonoBehaviour
{
    public static SSGameDataManifest GameDataInstance { get; private set; }

    public void Awake() //Singleton
    {
        if (GameDataInstance == null)
        {
            GameDataInstance = this;
        }

        else
        {
            if (GameDataInstance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] SOItemDatabase GameItemDatabase;


}
