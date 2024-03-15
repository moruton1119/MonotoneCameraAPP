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
        PictureData PD = FindObjectOfType<PictureData>();
        
    // モノトーン処理を行いJPGで保存する
        PD.save();
    }

}