using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameStarter : UnitySingleton<GameStarter>
{
    public override void Awake()
    {
        base.Awake();
        //初始化工具
        this.gameObject.AddComponent<CheckIsClickUI>();

        //初始化游戏框架
        this.gameObject.AddComponent<LuaManager>();

        //资源管理于初始化 

    }

    private void Start()
    {
        //进入游戏
        this.StartCoroutine(this.GameStart());
    }


    /// <summary>
    /// 检查热更新携程
    /// </summary> 
    IEnumerator checkHotUpdate() {
        yield return 0;
        
    }

    IEnumerator GameStart() {
        yield return this.StartCoroutine(this.checkHotUpdate());

        //进入Lua虚拟机代码，运行lua代码 
        LuaManager.Instance.runLuaScript();
    }
}
