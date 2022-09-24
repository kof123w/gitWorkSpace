using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using XLua;

public class LuaManager : UnitySingleton<LuaManager>
{
    private LuaEnv env ; 
    private static string luaScriptFolder = "LuaScripts";

    private bool isInitFinish = false;

    /// <summary>
    /// 外界获取环境
    /// </summary>
    /// <returns></returns>
    public LuaEnv GetLuaEnv() { 
       return env;
    }

    public override void Awake() { 
        base.Awake();

        //初始化lua环境
        initLuaEnv();
    }

    /// <summary>
    /// lua脚本自定义加载器
    /// </summary> 
    private byte[] LuaScriptLoader(ref string filepath) {
        string scriptPath = string.Empty;
        scriptPath = filepath.Replace(".", "/") + ".lua";
        //编辑模型下运行一下
#if UNITY_EDITOR
        string[] pathCombineParam = {
            Application.dataPath,
            luaScriptFolder,
            scriptPath
        }; 
        scriptPath = Path.Combine(pathCombineParam);
        byte[] data = File.ReadAllBytes(scriptPath);
        
        return data;
#endif 
         //如果不是编辑器模式下的lua代码 
        return null;
    }

    /// <summary>
    /// 初始化lua环境
    /// </summary>
    private void initLuaEnv() {
        env = new LuaEnv();
        env.AddLoader(LuaScriptLoader);
    }

    /// <summary>
    /// 进入游戏逻辑
    /// </summary>
    public void runLuaScript() { 
        //Debug.Log("运行lua代码");
        this.env.DoString("require('Main')");
        this.env.DoString("main.awake()");
        isInitFinish = true; 
    }

    public void Update()
    {
        if (isInitFinish == false)
        {
            return;
        }
        //Debug.Log("运行lua代码"); 
        this.env.DoString("main.update()");
    }
}
