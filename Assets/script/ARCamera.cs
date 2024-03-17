using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.XR.ARFoundation;

public class ARCamera : MonoBehaviour
{
    public Text countdown;

    void Start()
    {
    }

    void Update()
    {

    }

    public void Shutter()
    {
        StartCoroutine(Camera());
    }

    private IEnumerator Camera()
    {
        Debug.Log("コルーチン起動");
        PictureData pD = FindObjectOfType<PictureData>();
        SceneChange sC = FindObjectOfType<SceneChange>();

        //ここに3秒のカウントを行う処理を書く
        //愚直な実装感否めない・・・

        // 3秒間待つ
        countdown.text = "3";
        yield return new WaitForSeconds(1);
        countdown.text = "2";
        yield return new WaitForSeconds(1);
        countdown.text = "1";
        yield return new WaitForSeconds(1);

        // モノトーン処理を行う
        pD.Monotone();
        //結果をプレビューするシーンに移動
        sC.OnLoadScene1();
    }

}