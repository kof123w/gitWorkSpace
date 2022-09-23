using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class BuildAB
{
    [MenuItem("build/build assert bundle")]
    static void buildAssertBundle() {
        string path = "Assets/AssertPackage";  //我们打包的路径
        //判断对于的文件夹是否存在，没有就创建一个文件夹
        if (!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
        }

        //打包API调用,第一个参数为打包路径 ， 第二个参数选择为None(意思是使用LZMA格式进行压缩)，第三个参数为打包资源的目标平台为Win64
        BuildPipeline.BuildAssetBundles(path,BuildAssetBundleOptions.None,BuildTarget.StandaloneWindows64);
    }
}
