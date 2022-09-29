using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomImage : Image
{
    private PolygonCollider2D polygonCollider2D;
    
    protected override void Awake()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }
    
    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform,screenPoint, eventCamera,out localPos);

        if (localPos == null) {
            return false;
        }

        return polygonCollider2D.OverlapPoint(localPos);
    }
}
