using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ARPreview : MonoBehaviour
{
    //画損の保存、カメラのsceneに戻る処理

    public RawImage cImg;
    public AspectRatioFitter aRF;
    private Texture2D cTex;
    public Text sT;

    // Start is called before the first frame update
    void Start()
    {
        // 撮影した写真のアスペクト比が崩れないよう撮影した時の向きでデバイスの向き固定する
        ScreenOrientation deviceOrientation = Screen.orientation;
        //縦持ちの場合・真逆の場合も
        if (deviceOrientation == ScreenOrientation.Portrait)
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
        else if (deviceOrientation == ScreenOrientation.PortraitUpsideDown)
        {
            Screen.orientation = ScreenOrientation.PortraitUpsideDown;
        }
        //横持ちの場合・真逆の場合も
        else if (deviceOrientation == ScreenOrientation.LandscapeLeft)
        {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        else if (deviceOrientation == ScreenOrientation.LandscapeRight)
        {
        Screen.orientation = ScreenOrientation.LandscapeRight;
        }

        //モノクロ写真のテクスチャを取得
        PictureData PD = FindObjectOfType<PictureData>();
        cTex = PictureData.previewTex;
        // AspectRatioFitterでRowImageのアスペクト比をモノクロテクスチャに合わせる
        aRF.aspectRatio = (float)cTex.width / cTex.height;
        // RawImageに撮影したモノクロ写真のテクスチャを渡す
        cImg.texture = cTex; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Retrun()
    {
        SceneChange sC = FindObjectOfType<SceneChange>();

        //写真データを破棄
        Object.Destroy(cTex);

        //カメラに移動
        sC.OnLoadScene1();
    }

    public void Save()
    {
        //フォルダのアイコンを探して取得
        GameObject sB = GameObject.Find("Save");
        sB.SetActive(false);
        //非アクティブスタートの為定義済み 保存しましたを表示する
        sT.text = "保存しました。";

        // ファイルを保存する処理
    #if UNITY_ANDROID
        NativeGallery.SaveImageToGallery(cTex, "MonotoneCamera", "test.jpg");
    #endif
        //エディターでの確認用
        Debug.Log("処理完了");
    }
}
