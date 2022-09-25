using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XLua;

public class CheckIsClickUI : UnitySingleton<CheckIsClickUI> { 
    /// <summary>
    /// 判断是否点击到UI
    /// </summary>
    [LuaCallCSharp]
    public bool isClickUI() {
        GraphicRaycaster graphicRaycaster = FindObjectOfType<GraphicRaycaster>();
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.pressPosition = Input.mousePosition;
        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, results);
        return results.Count > 0;
    }

    /// <summary>
    /// 判断是否点击到物品
    /// </summary>
    [LuaCallCSharp]
    public bool isClickObject()
    {
        PhysicsRaycaster physicsRaycaster = FindObjectOfType<PhysicsRaycaster>();
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.pressPosition = Input.mousePosition;
        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        physicsRaycaster.Raycast(eventData, results);
        return results.Count > 0;
    }
}
