using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class CustomPloyImageHandler : Image,IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / rectTransform.lossyScale.x;

        //通知父物体进行脏标记
        rectTransform.parent.gameObject.GetComponent<CustomPolyImage>().SetVerticesDirty();
    }

    /// <summary>
    /// 修改父物物体
    /// </summary>
    /// <param name="transform"></param>
    public void SetParent(Transform transform){  
        rectTransform.SetParent(transform);
    }
}
