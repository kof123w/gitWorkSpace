using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;


/// <summary>
/// 主要是展示以下AB包读取方式
/// </summary>
public class LoadAssertBundle : MonoBehaviour
{

    string path = "Assets/AssertPackage/";  //之前打包的路径
    string loadPacket = "perfabs/cube.perfab";  //读取的报名(要携带后缀名)
    string perfabName = "Cube";   //打包的预制体名称 

    private void Start()
    {
        //LoadAssert();
        //StartCoroutine(this.LoadAssertAsync());
        //StartCoroutine(this.WWWLoadAssertBundle());
    }

    /// <summary>
    /// 本地同步加载AssertBundle资源，并且创建出来
    /// </summary>
    void LoadAssert() { 
        AssetBundle bundle = AssetBundle.LoadFromFile(path + loadPacket);
        GameObject perfab = bundle.LoadAsset<GameObject>(perfabName);
        GameObject go = Instantiate(perfab);  //创建实例
        go.name = "我是本地同步加载出来的";
    }


    /// <summary>
    /// 本地异步加载AssertBundle资源，并且创建出来
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadAssertAsync() {
        AssetBundleCreateRequest request = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(path + loadPacket));

        yield return request;   //等待加载结束在往下执行
        GameObject perfab = request.assetBundle.LoadAsset<GameObject>(perfabName);
        GameObject go = Instantiate(perfab);  //创建实例
        go.name = "本地异步加载出来的";
    }

    /// <summary>
    /// 使用www进行加载。当然www也可以通过请求服务器下载ab包再加载
    /// </summary>
    /*IEnumerator WWWLoadAssertBundle() {
        while (!Caching.ready) {
           yield return null;
        }

        //这里url的写法可以使用 http:// + 网址
        string url = @"file:///F:/unity/XLua/Assets/AssertPackage/perfabs/cube.perfab"; 

        WWW www = WWW.LoadFromCacheOrDownload(url,1);
        yield return www;  //等待下载结束

        if (!string.IsNullOrEmpty(www.error)){ 
            yield break;
        }
        AssetBundle bundle = www.assetBundle;
        GameObject perfab = bundle.LoadAsset<GameObject>(perfabName);
        GameObject go = Instantiate(perfab);
        go.name = "我是通过www请求加载出来的";
    }
*/

    /// <summary>
    /// 最后是unity官方推荐使用的远程请求下载并且加载Assert包的方法
    /// </summary>
    /// <returns></returns>
    IEnumerator UnityWebRequestLoadAssertBundle() {
        //因为目前服务器还没搭建，先放着把
        string url = @"http://localhost/AssertPackage/perfabs/cube.perfab";
        UnityWebRequest webReq = UnityWebRequest.Get(url);
        yield return webReq;  //等待下载完成
        if (!string.IsNullOrEmpty(webReq.error)) { 
           yield break;
        }
         
        byte[] data = webReq.downloadHandler.data;
        AssetBundleCreateRequest request = AssetBundle.LoadFromMemoryAsync(data);
        yield return request;

        AssetBundle bundle = request.assetBundle;
        GameObject perfab = bundle.LoadAsset <GameObject>(perfabName);
        Instantiate(perfab);

    }
}
