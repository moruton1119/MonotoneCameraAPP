using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.IO;
using UnityEngine.EventSystems;

public class Camera : MonoBehaviour
{
    // カメラの映像を映す
    public RawImage rawImage;
    WebCamTexture webCamTexture;
    public GameObject Scene;

    // Start is called before the first frame update
    void Start()
    {
        // カメラ画面の取得
        webCamTexture = new WebCamTexture();
        // カメラ画面を画像に描画
        rawImage.texture = webCamTexture;
        // カメラを起動
        webCamTexture.Play();
    }
    public async void Shutter()
    {
        await Task.Delay(3000);
        webCamTexture.Pause();
        this.GetComponent<SceneChange>().OnLoadPicture();
        save();
    }

    public void save()
    {
        // Texture2Dを新規作成
        Texture2D outputTexture = new Texture2D(webCamTexture.width, webCamTexture.height, TextureFormat.ARGB32, false);
        

        // カメラのピクセルデータを設定
        // Color[] colors = webCamTexture.GetPixels();
        // texture.SetPixels(colors);
        // TextureをApply
        // texture.Apply();

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

        rawImage.texture = outputTexture;




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