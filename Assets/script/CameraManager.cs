using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CameraManager : MonoBehaviour
{

    public static CameraManager instance;
    public RawImage rawImage;
    WebCamTexture webCamTexture;
    public static RawImage rawI;

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


    // Start is called before the first frame update
    void Start()
    {
        
    }

        public void save()
    {
        // Texture2Dを新規作成
        Texture2D outputTexture = new Texture2D(webCamTexture.width, webCamTexture.height, TextureFormat.ARGB32, false);
        // カメラのピクセルデータを設定
        Color[] inputColors = webCamTexture.GetPixels();
        Color[] outputColors = new Color[webCamTexture.width * webCamTexture.height];
        for (int y = 0; y < webCamTexture.height; y++)
        {
            for (int x = 0; x < webCamTexture.width; x++)
            {
                var color = inputColors[(webCamTexture.width * y) + x];
                float average = (color.r + color.g + color.b) / 3;
                outputColors[(webCamTexture.width * y) + x] = new Color(average, average, average);
            }
        }
        outputTexture.SetPixels(outputColors);
        outputTexture.Apply();

        // // RawImageコンポーネントを探す
        // RawImage rawImage = FindObjectOfType<RawImage>();

        // //モノクロ写真を入れる
        // rawImage.texture = outputTexture;

        rawI.texture  = outputTexture;

        // Encode
        byte[] bin = outputTexture.EncodeToJPG();
        // Encodeが終わったら削除
        // Object.Destroy(outputTexture);

        // ファイルを保存
    #if UNITY_ANDROID
        File.WriteAllBytes(Application.persistentDataPath + "/test.jpg", bin);
    #endif
        File.WriteAllBytes(Application.dataPath + "/test.jpg", bin);
        Debug.Log("画像書き込んだよ");

    }

}
