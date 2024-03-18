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
    public int counts;
    public GameObject shutterimage;

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

        // //愚直な実装感否めない・・・
        // 3秒間待つ
        for(int x=counts; x>=1 ; --x)
        {
            countdown.text = x.ToString();
            yield return new WaitForSeconds(1);
        }
        shutterimage.SetActive(true);
        // モノトーン処理を行う
        pD.Monotone();
        //結果をプレビューするシーンに移動
        sC.OnLoadScene1();
    }

}