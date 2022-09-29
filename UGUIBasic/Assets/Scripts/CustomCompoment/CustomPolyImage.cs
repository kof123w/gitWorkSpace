using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 自定义多边形UI组件
/// </summary> 
public class CustomPolyImage : Image
{
    //构成的点集合
    List<CustomPloyImageHandler> pointList = null;


    /// <summary>
    /// 构成多边形的点数
    /// </summary>
    [SerializeField]
    public int pointNum = 5;

    protected override void Awake()
    {
        base.Awake();
        fresh();
        SetVerticesDirty();

    }

    /// <summary>
    /// 重新编写顶点数据
    /// </summary>
    /// <param name="toFill"></param>
    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        toFill.Clear();
        AddVerts(toFill);
        AddTriangles(toFill);

    }


    /// <summary>
    /// 添加顶点
    /// </summary>
    void AddVerts(VertexHelper toFill) {
        for(int index = 0;index < pointList.Count;index++ ) {
            CustomPloyImageHandler handler = pointList[index];
            UIVertex v = new UIVertex();
            if (index != 0) {
                v.color = Color.red; ;
            }
            v.position = handler.rectTransform.anchoredPosition;
            toFill.AddVert(v);
        }
    }

    /// <summary>
    /// 添加三角形索引
    /// </summary>
    void AddTriangles(VertexHelper toFill) {
        int id = 1;
        for (int i = 0;i<pointNum;i++){

            int nextId = id + 1;
            if (nextId > pointNum) {
                nextId = 1;
            }
            toFill.AddTriangle(id, 0, nextId);
            id++;

        }  
    }

    /// <summary>
    /// 初始化点
    /// </summary>
    void InitPoint() {  
        for (int index = 0; index < pointNum + 1;index++) {
            GameObject go = new GameObject ("Point"+index); 
            CustomPloyImageHandler handler = go.AddComponent<CustomPloyImageHandler>();
            handler.SetParent(transform.transform);
            handler.color = Color.blue;
            InitPointRect(index, handler);
            pointList.Add (handler);
        }
    }

    /// <summary>
    /// 初始化点的位置
    /// </summary>
    void InitPointRect(int index, CustomPloyImageHandler handler){
        handler.rectTransform.sizeDelta = new Vector2(10,10);

        //第一个为中心点,必定是0，0处
        if (index == 0) {
            handler.rectTransform.anchoredPosition = Vector2.zero;
            return;
        }
         

        //弧度
        float radius = 2 * Mathf.PI /(float)pointNum * (index - 1);
        //半径，直接认为是宽度
        float width = rectTransform.rect.width / 2;

        float x = Mathf.Cos(radius) * width;
        float y = Mathf.Sin(radius) * width;

        handler.rectTransform.anchoredPosition = new Vector2(x, y);
    }

    void ClearPoint() {

        foreach (CustomPloyImageHandler handler in pointList) {
            DestroyImmediate (handler);  //立即销毁
        }

        pointList.Clear ();
    }

    /// <summary>
    /// 刷新函数
    /// </summary>
    void fresh() {
        pointList = new List<CustomPloyImageHandler>();
        rectTransform.sizeDelta = new Vector2(500,500);
        ClearPoint();
        InitPoint();
    }
}