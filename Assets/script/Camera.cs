using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class Camera : MonoBehaviour
{
    // カメラの映像を映す
    public RawImage rawImage;
    WebCamTexture webCamTexture;

    // Start is called before the first frame update
    void Start()
    {
        webCamTexture = new WebCamTexture();
        rawImage.texture = webCamTexture;
        webCamTexture.Play();
    }
    public async void Shutter()
    {
        await Task.Delay(3000);
        webCamTexture.Stop();
        await Task.Delay(3000);
        webCamTexture.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}