using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PictureData : MonoBehaviour
{
    public static PictureData instance;
    public Texture2D outputTex;
    public static Texture2D previewTex;

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
        }

    public void Monotone()
    {

        Texture2D C_data = CameraImageExample.saveTex;
        outputTex = new Texture2D(C_data.width, C_data.height, TextureFormat.ARGB32, false);
        // カメラのピクセルデータを設定
        Color[] inputColors = C_data.GetPixels();
        Color[] outputColors = new Color[C_data.width * C_data.height];
        // //モノクロ処理
        for (int y = 0; y < C_data.height; y++)
        {
            for (int x = 0; x < C_data.width; x++)
            {
                var color = inputColors[(C_data.width * y) + x];
                float average = (color.r + color.g + color.b) / 3;
                outputColors[(C_data.width * y) + x] = new Color(average, average, average);
            }
        }
        outputTex.SetPixels(outputColors);
        outputTex.Apply();

        previewTex = outputTex;

    }  

}