using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
        }

    public void save()
    {

        Texture2D C_data = CameraImageExample.saveTex;
        outputTexture = new Texture2D(C_data.width, C_data.height, TextureFormat.ARGB32, false);
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
        outputTexture.SetPixels(outputColors);
        outputTexture.Apply();

        // Encode
        byte[] bin = outputTexture.EncodeToJPG();
        // Encodeが終わったら削除
        // Object.Destroy(outputTexture);

        // ファイルを保存
    #if UNITY_ANDROID
        
        NativeGallery.SaveImageToGallery(outputTexture, "MonotoneCamera", "test.jpg");


        Debug.Log("android画像書き込んだよ");

    #endif
        File.WriteAllBytes(Application.dataPath + "/test.jpg", bin);
        Debug.Log("画像書き込んだよ");

    }  

}
