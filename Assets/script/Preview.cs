using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Preview : MonoBehaviour
{
    public RawImage rawImage;
 
    // Start is called before the first frame update
    void Start()
    {
        // rawImage= CameraManager.rawI;

        // // RawImageコンポーネントを探す
        RawImage rawImage = FindObjectOfType<RawImage>();

        // //モノクロ写真を入れる
        rawImage.texture = CameraManager.rawI.texture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
