using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 挂上这个组件自动申请别的组件
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class CustomLifeBarComponet : MonoBehaviour 
{
    RectTransform rectTransform;
    RectTransform liftBarRect;

    /// <summary>
    /// 健康血量颜色
    /// </summary>
    Color color1 = new Color(83/255f,137/255f,64/255f);

    /// <summary>
    /// 风险血量颜色
    /// </summary>
    Color color2 = new Color(243 / 255f, 124 / 255f, 54 / 255f);

    /// <summary>
    /// 风险血量颜色
    /// </summary>
    Color color3 = new Color(243 / 255f, 65 / 255f, 74 / 255f);

    public  float curHitPoint = 1f; 
    void Update() {
        if (Input.GetKey(KeyCode.D)) {
            curHitPoint +=  1/10f * Time.deltaTime * 2;
            if (curHitPoint >= 1) {
                curHitPoint = 1;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            curHitPoint -= 1 / 10f*Time.deltaTime * 2;

            if (curHitPoint <= 0){
                curHitPoint = 0;
            }
        } 
        liftBarRect.offsetMax = new Vector2((curHitPoint - 1) * rectTransform.rect.width, liftBarRect.offsetMax.y);
        if (curHitPoint > 0.5) {
            liftBarRect.GetComponent<Image>().color = color1;
        }

        if (curHitPoint>0.2 && curHitPoint <= 0.5 ) {
            liftBarRect.GetComponent<Image>().color = color2;
        }

        if (curHitPoint< 0.2) {
            liftBarRect.GetComponent<Image>().color = color3;
        }
    }

    //当前血量

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
        liftBarRect = gameObject.transform.GetChild(1).GetComponent<RectTransform>();  
    }
     
}
