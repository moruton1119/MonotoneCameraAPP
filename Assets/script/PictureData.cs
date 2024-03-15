using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureData : MonoBehaviour
{
    public static PictureData instance;
    public static Texture2D outputTexture;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        CameraImageExample CIE  = FindObjectOfType<CameraImageExample>();
    }
    void Update()
        {
        Texture2D outputTexture = CameraImageExample.outputTexture;
            
        }

}
