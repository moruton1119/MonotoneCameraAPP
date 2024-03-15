using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARPreview : MonoBehaviour
{
    public Texture2D cTex;
    public RawImage cImg;
    // Start is called before the first frame update
    void Start()
    {
        PictureData PD = FindObjectOfType<PictureData>();
        Texture2D cTex = PictureData.outputTexture;
        cImg.texture = cTex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
