using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.IO;

public class Camera : MonoBehaviour
{
    public RawImage rawImage;
    WebCamTexture webCamTexture;


    // Start is called before the first frame update
    void Start()
    {
        if (webCamTexture == null)
        {
            // カメラ画面の取得
            webCamTexture = new WebCamTexture();
        }
        // カメラ画面を画像に描画
        rawImage.texture = webCamTexture;
        // カメラを起動
        webCamTexture.Play();
    }
    public async void Shutter()
    {
        await Task.Delay(3000);
        this.GetComponent<SceneChange>().OnLoadPicture();
        this.GetComponent<CameraManager>().save();
    }
}