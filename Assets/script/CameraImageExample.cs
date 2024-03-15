using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

using System.IO;

public class CameraImageExample : MonoBehaviour
{
    Texture2D m_Texture;
    [SerializeField]ARCameraManager cameraManager = null;
    public static Texture2D outputTexture;

    void OnEnable()
    {
        cameraManager.frameReceived += OnCameraFrameReceived;
    }

    void OnDisable()
    {
        cameraManager.frameReceived -= OnCameraFrameReceived;
    }

    public unsafe void OnCameraFrameReceived(ARCameraFrameEventArgs eventArgs)
    {
        // Debug.Log("動いているよ");
        if (!cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
            return;

        var conversionParams = new XRCpuImage.ConversionParams
        {
            // Get the entire image.
            inputRect = new RectInt(0, 0, image.width, image.height),

            // Downsample by 2.
            outputDimensions = new Vector2Int(image.width / 2, image.height / 2),

            // Choose RGBA format.
            outputFormat = TextureFormat.RGBA32,

            // Flip across the vertical axis (mirror image).
            transformation = XRCpuImage.Transformation.MirrorY
        };

        // See how many bytes you need to store the final image.
        int size = image.GetConvertedDataSize(conversionParams);

        // Allocate a buffer to store the image.
        var buffer = new NativeArray<byte>(size, Allocator.Temp);

        // Extract the image data
        image.Convert(conversionParams, new IntPtr(buffer.GetUnsafePtr()), buffer.Length);

        // The image was converted to RGBA32 format and written into the provided buffer
        // so you can dispose of the XRCpuImage. You must do this or it will leak resources.
        image.Dispose();

        // At this point, you can process the image, pass it to a computer vision algorithm, etc.
        // In this example, you apply it to a texture to visualize it.

        // You've got the data; let's put it into a texture so you can visualize it.
        m_Texture = new Texture2D(
            conversionParams.outputDimensions.x,
            conversionParams.outputDimensions.y,
            conversionParams.outputFormat,
            false);

        m_Texture.LoadRawTextureData(buffer);
        m_Texture.Apply();

        // Done with your temporary data, so you can dispose it.
        buffer.Dispose();
    }

    public void save()
    {

        Texture2D C_data = m_Texture;
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

        // RawImageコンポーネントを探す
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
        
        NativeGallery.SaveImageToGallery(outputTexture, "MonotoneCamera", "test.jpg");


        Debug.Log("android画像書き込んだよ");

    #endif
        File.WriteAllBytes(Application.dataPath + "/test.jpg", bin);
        Debug.Log("画像書き込んだよ");

    }

}
