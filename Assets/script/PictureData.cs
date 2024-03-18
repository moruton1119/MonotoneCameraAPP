using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PictureData : MonoBehaviour
{
    //画像の処理と異なるScene間で変数を渡すようのscript


    public static PictureData instance;
    public Texture2D outputTex;

    //モノクロ画像保存用
    public static Texture2D previewTex;
    public AudioSource shuttersound;

    //Sceneにひとつのみ存在し破棄されないオブジェクト化するメソッド
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

    //カメラ画像を処理する準備
    void Start()
    {
        CameraImageExample CIE  = FindObjectOfType<CameraImageExample>();
        
    }
    void Update()
        { 
        }
    //シャッターの音を鳴らす
    public void Shuttersound()
    {
        shuttersound.Play();
    }

    //モノトーン処理を行う
    public void Monotone()
    {
        //カメラ画像取得
        Texture2D C_data = CameraImageExample.saveTex;
        //カメラ画像を入れるTextureを作成
        outputTex = new Texture2D(C_data.width, C_data.height, TextureFormat.ARGB32, false);
        //カメラ画像の色を取得
        Color[] inputColors = C_data.GetPixels();
        //モノクロ処理した色を入れるcolorを作成
        Color[] outputColors = new Color[C_data.width * C_data.height];
        //モノクロ処理
        for (int y = 0; y < C_data.height; y++)
        {
            for (int x = 0; x < C_data.width; x++)
            {
                var color = inputColors[(C_data.width * y) + x];
                float average = (color.r + color.g + color.b) / 3;
                outputColors[(C_data.width * y) + x] = new Color(average, average, average);
            }
        }
        //モノクロ処理した色をセットして確定させる
        outputTex.SetPixels(outputColors);
        outputTex.Apply();

        //保存用のTextureに格納
        previewTex = outputTex;

    }  

}