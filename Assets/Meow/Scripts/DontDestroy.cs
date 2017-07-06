using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


/**
 * Auth: wangdy@youzu.com
 * Date: 2016/8/24 16:46:58
 * File: DontDestroy
 * Desc: 挂了这个脚本的object在切换场景时不会被Destroy
 * */

public class DontDestroy : MonoBehaviour
{
    public static GameObject gameCamera;
    // Use this for initialization
    void Awake()
    {
        gameCamera = GameObject.Find("CameraMain");
    }
    void Start()
    {
        GameObject.DontDestroyOnLoad(this);
    }

}



