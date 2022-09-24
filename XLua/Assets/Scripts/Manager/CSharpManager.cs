using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSharpManager:UnitySingleton<CSharpManager>
{
    private GameObject mainCamera; 

    public override void Awake()
    {
        //父类单例管理
        base.Awake();

        //初始化摄像机
        initCamera();

        //子类扩展添加脚本注册
        this.gameObject.AddComponent<HotFixTest>();
    }

    /// <summary>
    /// 初始化摄像机
    /// </summary>
    private void initCamera() {
        GameObject go = new GameObject();
        go.AddComponent<Camera>();
        go.AddComponent<AudioListener>();
        go.name = "mainCamera";
        mainCamera = go;
    }

    /// <summary>
    /// 外界获取摄像机代码
    /// </summary> 
    public Camera GetMainCamera() {
        return mainCamera.GetComponent<Camera>();
    }
}
