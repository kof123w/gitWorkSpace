using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 资源管理器
/// </summary>
public class TexManger : UnitySingleton<TexManger>
{
    //存放资源字典
    private Dictionary<string, Object> spriteMangerDic = new Dictionary<string, Object>(); 
    public override void Awake() { 
        base.Awake();
        LoadAllRes();
    }

    void LoadAllRes()
    {  
        string assetBundlespath = "Assets/AssertPackage"; 
        AssetBundle assetBundle = AssetBundle.LoadFromFile(assetBundlespath + "/AssertPackage"); 
        string[] assetNames = assetBundle.GetAllAssetNames();
        for (int i = 0; i < assetNames.Length; i++)
        {
            Debug.Log("加载资源:" + assetNames[i]);
        }
        string manifestPath = "assetbundlemanifest";
        if (!assetBundle.Contains(manifestPath))
        {
            Debug.Log("manifestpath is null");

        }
        
        AssetBundleManifest manifest = assetBundle.LoadAsset<AssetBundleManifest>("assetbundlemanifest");
        //加载所有资源
        string[] allAssetBundleName = manifest.GetAllAssetBundles();
        foreach (string assetBunleName in allAssetBundleName) {
            Debug.Log("加载资源:" + assetBunleName);
            string[] splitName = assetBunleName.Split('/');
            string assetName = splitName[splitName.Length-1].Split('.')[0];
            AssetBundle bundle = AssetBundle.LoadFromFile(assetBundlespath +"/"+ assetBunleName);
            //没有做资源类型分类，暂时先这样写
            //todo
            Sprite tmp = bundle.LoadAsset<Sprite>(assetName);
            spriteMangerDic.Add(tmp.name, tmp);
        }

        //加载依赖包
        //string[] dependencies = manifest.GetAllDependencies();
        //foreach (string dependency in dependencies)
        //{
        //AssetBundle.LoadFromFile(Path);
        //Debug.Log(dependency);
        //}
    }

    /// <summary>
    /// 获取精灵图
    /// </summary> 
    public Sprite getSpriteByAssetName(string key) {
        return (Sprite)spriteMangerDic[key]; 
    }

}
