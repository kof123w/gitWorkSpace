using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ScrollImage : MonoBehaviour
{  
    public int Order = 0;         //UI元素的排列方式 

    /// <summary>
    /// 这个物体的UI布局组件
    /// </summary>
    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// 设置父物体
    /// </summary> 
    public void SetParent(Transform transform) {
        rectTransform.SetParent(transform);
    }

    /// <summary>
    /// 获取物体的Rect
    /// </summary> 
    public RectTransform GetRectTransform() { 
       return rectTransform;
    }


    /// <summary>
    /// 修改层级显示
    /// </summary>
    public void SetScrollImageData(int order) {
        this.Order = order;
    }


    public int CompareTo(ScrollImage scrollImage)
    {
        if (this.rectTransform.localScale.x > scrollImage.rectTransform.localScale.x)
            return -1;
        else
            return 1;
    }
}
