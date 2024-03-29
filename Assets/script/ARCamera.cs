using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class ARCamera : MonoBehaviour
{
    //コルーチンを使い指定秒数待ちシャッターを押す

    public Text countdown;
    public int counts;
    public GameObject shutterimage;
    public AudioSource shuttercounts;

    void Start()
    {
        //画面を自動回転に変更
        Screen.orientation = ScreenOrientation.AutoRotation;
    }

    void Update()
    {

    }

    public void Shutter()
    {
        StartCoroutine(Camera());
    }

    //コルーチン処理
    private IEnumerator Camera()
    {
        //モノトーン処理、シーンチェンジの準備
        PictureData pD = FindObjectOfType<PictureData>();
        SceneChange sC = FindObjectOfType<SceneChange>();

        // 3秒間待つ countsの秒数数える処理
        for(int x=counts; x>=1 ; --x)
        {
            countdown.text = x.ToString();
            shuttercounts.Play();
            yield return new WaitForSeconds(1);
        }
        //シャッター音
        pD.Shuttersound();
        // モノトーン処理を行う
        pD.Monotone();
        //シャッターを切るときのフラッシュ表現
        shutterimage.SetActive(true);
        //結果をプレビューするシーンに移動
        sC.OnLoadScene1();
    }

}