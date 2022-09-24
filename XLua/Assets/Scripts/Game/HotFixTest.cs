using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[Hotfix]
public class HotFixTest : MonoBehaviour
{
    void Start()
    {
        hotFixTest();
    } 

    [LuaCallCSharp]
    public void hotFixTest()
    {
        Debug.Log("我是C#代码的输出");
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 300, 80), "Hotfix"))
        {
            LuaManager.Instance.GetLuaEnv().DoString("hotFixTest.hotHotFixTest()");
            hotFixTest();
        } 
    } 
}
