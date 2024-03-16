using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.XR.ARFoundation;

public class ARCamera : MonoBehaviour
{
    

    void Start()
    {
    }

    void Update()
    {

    }

    public void shutter()
    {
        PictureData pD = FindObjectOfType<PictureData>();
        SceneChange sC = FindObjectOfType<SceneChange>();

    //ここに3秒のカウントを行う処理を書く

        
    // モノトーン処理を行いJPGで保存する
        pD.save();
    //結果をプレビューするシーンに移動
        sC.OnLoadScene1();
    }

}