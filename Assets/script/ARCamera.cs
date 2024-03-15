using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;

public class ARCamera : MonoBehaviour
{
    [SerializeField]
    ARCameraBackground arCamBg;
    public RenderTexture renderTexture; // レンダリングされた画像を格納する RenderTexture
    private Texture2D capturedTexture;   // キャプチャされたテクスチャを格納する変数
    public static Texture2D outputTexture;

    void Start()
    {

    }

    void Update()
    {

    }

    public void shutter()
    {
        // Texture2Dを新規作成
        outputTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false);
        Graphics.Blit(outputTexture, arCamBg.material);

    // レンダリングされた画像を RenderTexture から Texture2D にコピーする
        // GetCapturedTexture();
    // モノトーン処理を行いJPGで保存する
        save();
    }

    



    // キャプチャされたテクスチャを取得するメソッド
    public Texture2D GetCapturedTexture()
    {
        // レンダリングされた画像を RenderTexture から Texture2D にコピーする
        RenderTexture.active = renderTexture;
        capturedTexture = new Texture2D(Screen.width, Screen.height);
        capturedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        capturedTexture.Apply();

        return capturedTexture;
    }
    public void save()
    {
        // カメラのピクセルデータを設定
        Color[] inputColors = outputTexture.GetPixels();
        Color[] outputColors = new Color[Screen.width * Screen.height];
        // //モノクロ処理
        for (int y = 0; y < Screen.height; y++)
        {
            for (int x = 0; x < Screen.width; x++)
            {
                var color = inputColors[(Screen.width * y) + x];
                float average = (color.r + color.g + color.b) / 3;
                outputColors[(Screen.width * y) + x] = new Color(average, average, average);
            }
        }
        outputTexture.SetPixels(outputColors);
        outputTexture.Apply();

        // // RawImageコンポーネントを探す
        // RawImage rawImage = FindObjectOfType<RawImage>();

        // //モノクロ写真を入れる
        // rawImage.texture = outputTexture;

        // rawI.texture  = outputTexture;

        // Encode
        byte[] bin = outputTexture.EncodeToJPG();
        // Encodeが終わったら削除
        // Object.Destroy(outputTexture);

        // ファイルを保存
    #if UNITY_ANDROID
        


        
        Debug.Log(filePath+"に画像書き込んだよ");

    #endif
        File.WriteAllBytes(Application.dataPath + "/test.jpg", bin);
        Debug.Log("画像書き込んだよ");

    }
}
