using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 滚动3D图片组件
/// </summary>
public class Scroll3DImage : MonoBehaviour, IDragHandler, IEndDragHandler
{
    //默认4张图片
    public int itemNum = 4;

    //原来的4张
    private int itemNumCache = 4;

    //播放动画时间
    private int animalTime = 1;

    public Vector3 minScaler = new Vector3(0.5f, 0.5f, 1);
    public Vector3 maxScaler = Vector3.one;


    //拿到当前物体的RectTransform组件
    RectTransform rectTransform;

    //做图片位置用
    public List<ScrollImage> imageObjects;
    public List<ScrollImage> cahceList;

    void Start() {
        imageObjects = new List<ScrollImage>();
        cahceList = new List<ScrollImage> ();
        itemNumCache = itemNum;
        rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(800, 600);
        rectTransform.anchoredPosition = Vector2.zero;
        InitSpriteRes();
    }

    void Update() {
        fresh();
    }

    /// <summary>
    /// 监听并且修改元素，一般情况下不会动这个函数
    /// </summary>
    void fresh() {
        if (itemNum == itemNumCache) {
            return;
        }

        //发现和原来不相等重新刷新并且生成滚动的子物体
        itemNumCache = itemNum;
        InitSpriteRes();
    }


    /// <summary>
    /// 初始化资源
    /// </summary>
    void InitSpriteRes() {
        //把原来的东西销毁掉
        //刚刚初始化的时候物体必定为空的东西
        for (int i = 0; i < imageObjects.Count; i++) {
            Destroy(imageObjects[i].transform.gameObject);
        }

        imageObjects.Clear(); 
        cahceList.Clear();
        //获取对象模板
        GameObject temple = CreateItemTemp();

        //先做颜色缓存
        float offsetColor = 1 / (float)itemNum;

        for (int i = itemNum -1; i >=0; i--) {
            GameObject item = Instantiate(temple);
            ScrollImage scrollImage = item.AddComponent<ScrollImage>(); 
            scrollImage.SetParent(transform);
            //为了区分不同的item稍微改改颜色
            item.GetComponent<Image>().color = new Color(offsetColor * i, offsetColor * i, offsetColor * i);
            imageObjects.Add(scrollImage);
            scrollImage.Order = i;
            //初始化坐标
            float x = GetX(scrollImage.Order);
            scrollImage.GetRectTransform().sizeDelta = new Vector2(200, 200); 
            scrollImage.GetRectTransform().anchoredPosition = new Vector3(x, 0, 0);
            scrollImage.GetRectTransform().localScale = GetScale(i);
            cahceList.Add(scrollImage);
            //scrollImage.transform.SetSiblingIndex(scrollImage.Order);
        }
        cahceList.Sort(cacheListSort);
        
        for (int i = 0;i < cahceList.Count;i++ ) {
            cahceList[i].transform.SetSiblingIndex(i);
        }

        //模板用完丢掉
        Destroy(temple);
    }

    public int cacheListSort(ScrollImage x, ScrollImage y) {
        if (x.GetRectTransform().localScale.x > y.GetRectTransform().localScale.x)
            return 1;
        else
            return -1;
    }

    /// <summary>
    /// 获取这个元素的x
    /// </summary>
    /// <param name="index">元素的索引</param> 
    public float GetX(int index) {
        float gradient = GetGradient(index);

        return 2 * rectTransform.rect.width * gradient;
    }

    /// <summary>
    /// 获取变化梯度
    /// </summary>
    /// <returns></returns>
    public float GetGradient(int index) {
        float gradient = 1 / (float)itemNum * (float)index;

        if (gradient >= 0 && gradient <= 0.25)
        {
            return gradient;
        }
        else if (gradient > 0.25 && gradient <= 0.5)
        {
            return (0.5f - gradient);
        }
        else if (gradient > 0.5 && gradient <= 0.75)
        {
            return 0.5f - gradient;
        }
        else
        {
            return -(1f - gradient);
        }
    }

    /// <summary>
    /// 获取缩放
    /// </summary> 
    public Vector3 GetScale(int index) { 
        Vector3 offsetScale = (maxScaler - minScaler) / 0.5f;
        float offsetX = GetX(index);
        float gradient = 1 / (float)itemNum * (float)index; ;
        //返回最大和最小的情况
        if (gradient == 0 || gradient == 1) {
            return maxScaler;
        }
        if (gradient == 0.5) {
            return minScaler ;
        }

        if (gradient > 0f && gradient < 0.5f) {
            return maxScaler - (gradient  * offsetScale);
        }

        return maxScaler - ((1f- gradient) * offsetScale);
    }

    /// <summary>
    /// 创建模板对象
    /// </summary>
    GameObject CreateItemTemp() {
        GameObject gameObject = new GameObject("Item");

        Image image = gameObject.AddComponent<Image>();
        Sprite sprite = TexManger.Instance.getSpriteByAssetName("minmap");
        image.sprite = sprite;
        return gameObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 vector = eventData.delta;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 vector = eventData.delta;
        //Debug.Log(vector);
        ChangeOrder(vector.x);
    }


    /// <summary>
    /// 修改层级
    /// </summary>
    public void ChangeOrder(float offsetX) {
        int moveX = offsetX > 0 ? 1 : -1;
        for (int i = 0; i < imageObjects.Count; i++) {
            int lastIndex = imageObjects[i].Order + moveX;
            if (lastIndex < 0)
            {
                lastIndex = imageObjects.Count - 1;
            }
            else if (lastIndex > imageObjects.Count - 1) {
                lastIndex = 0;
            }
            imageObjects[i].Order = lastIndex;
            imageObjects[i].SetScrollImageData(lastIndex); 
        }

        cahceList.Clear();

        for (int i = 0; i < imageObjects.Count; i++) { 
           updateData(imageObjects[i]);
            cahceList.Add(imageObjects[i]);
        }

        cahceList.Sort(cacheListSort);
         
        for (int i = 0; i < cahceList.Count; i++)
        {
            cahceList[i].transform.SetSiblingIndex(i);
        }
    }

    /// <summary>
    /// 等一般的动画时间过后才能往下执行换元素顺序
    /// </summary>
    /// <returns></returns>
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(animalTime/2);
        for (int i = 0; i < cahceList.Count; i++)
        {
            cahceList[i].transform.SetSiblingIndex(i);
        }
    }

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="scrollImage"></param>
    public void updateData(ScrollImage scrollImage)
    {
        float newX = GetX(scrollImage.Order);
        Vector3 newScaler =  GetScale(scrollImage.Order);

        scrollImage.GetRectTransform().localScale = newScaler; 
        scrollImage.GetRectTransform().DOAnchorPos(new Vector2(newX, 0), animalTime);
        scrollImage.GetRectTransform().DOScale(newScaler, animalTime);
    } 
}
