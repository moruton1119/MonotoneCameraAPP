using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ARPreview : MonoBehaviour
{
    // public Texture2D cTex;
    public RawImage cImg;
    public AspectRatioFitter aRF;
    private Texture2D cTex;
    public Text sT;
    // Start is called before the first frame update
    void Start()
    {
        PictureData PD = FindObjectOfType<PictureData>();
        cTex = PictureData.previewTex;
        //縦持ちの場合
        // aRF.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;

        //横持ちの場合
        // aRF.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;

        // AspectRatioFitterの設定
        aRF.aspectRatio = (float)cTex.width / cTex.height;
        Debug.Log(aRF.aspectRatio);
        // RawImageに撮影したモノクロ写真のテクスチャを渡す
        cImg.texture = cTex; 


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Retrun()
    {
        PictureData pD = FindObjectOfType<PictureData>();
        SceneChange sC = FindObjectOfType<SceneChange>();

        //写真データを破棄
        Object.Destroy(cTex);

        //カメラに移動
        sC.OnLoadScene1();
    }

    public void Save()
    {
        GameObject sB = GameObject.Find("Save");
        sB.SetActive(false);
        sT.text = "保存しました。";
        // ファイルを保存
    #if UNITY_ANDROID
        
        NativeGallery.SaveImageToGallery(cTex, "MonotoneCamera", "test.jpg");


        Debug.Log("android画像書き込んだよ");

    #else
        // Encode Windows確認用
        byte[] bin = cTex.EncodeToJPG();
        File.WriteAllBytes(Application.dataPath + "/test.jpg", bin);
        Debug.Log("PC画像書き込んだよ");

    #endif 
        Debug.Log("処理完了");
    }
}
