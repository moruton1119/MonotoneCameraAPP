using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.IO;

public class Camera : MonoBehaviour
{
    // カメラの映像を映す
    public RawImage rawImage;
    WebCamTexture webCamTexture;

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
        save();
        await Task.Delay(3000);
        webCamTexture.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void save()
    {
        // Texture2Dを新規作成
        Texture2D texture = new Texture2D(webCamTexture.width, webCamTexture.height, TextureFormat.ARGB32, false);
        // カメラのピクセルデータを設定
        Color[] colors = webCamTexture.GetPixels();
        texture.SetPixels(colors);
        // TextureをApply
        texture.Apply();

        // Encode
        byte[] bin = texture.EncodeToJPG();
        // Encodeが終わったら削除
        Object.Destroy(texture);

        // ファイルを保存
    #if UNITY_ANDROID
        File.WriteAllBytes(Application.persistentDataPath + "/test.jpg", bin);
    #endif
        File.WriteAllBytes(Application.dataPath + "/test.jpg", bin);
        Debug.Log("画像書き込んだよ");

    }
}