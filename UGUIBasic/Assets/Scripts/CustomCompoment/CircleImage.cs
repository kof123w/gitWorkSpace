using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;

/// <summary>
/// 实现圆形头像，基于Image所以直接继承Image类
/// </summary>
public class CircleImage : Image
{
    /// <summary>
    /// 百分比
    /// </summary>
    [SerializeField]
    public float percent = 1;
    private List<Vector2> vertexCache;

    /// <summary>
    /// 创建使用，先对顶点加工运行
    /// </summary>
    /// <param name="toFill"></param>
    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        //清掉原来的顶点数据
        toFill.Clear();
        vertexCache = new List<Vector2>();

        //uv的中心点，必须要从uv的min坐标开始算
        Vector2 uvCenter = getUvCenter();

        //转换向量比例计算
        Vector2 convertVect = getConvertVect();

        //计算并且添加顶点
        addVertex(toFill, uvCenter,convertVect);

    }


    private void addVertex(VertexHelper toFill,Vector2 uvCenter,Vector2 convertVect) {

        //圆心
        UIVertex cirleCenter = new UIVertex();
        cirleCenter.color = color;
        if (percent < 1)
        {
            cirleCenter.color = new Color(90 / 255f, 90 / 255f, 90 / 255f);
        }
        
        //顶点坐标。因为是一模型坐标来生成的顶点信息，所以直接认为中心点为0
        cirleCenter.position = new Vector2(0.5f, 0.5f) - rectTransform.pivot;
        //vertexCache.Add(cirleCenter.position);
        cirleCenter.uv0 = uvCenter;
        toFill.AddVert(cirleCenter);
        rectTransform.localScale = new Vector3(1, 1, 1);
        //把圆分成的份数
        int bran = 5;
        int inParceBran = (int)(bran * Mathf.Min(percent, 1));
        float radian = (2 * Mathf.PI) / bran;//弧度
        float radius = rectTransform.rect.width / 2;  //半径
        float curRadian = 0;
        //填充顶点
        for (int i = 0; i < bran + 1; i++)
        {
            float x = Mathf.Cos(curRadian) * radius;
            float realX = x + cirleCenter.position.x;
            float y = Mathf.Sin(curRadian) * radius + cirleCenter.position.y;
            float realY = y + cirleCenter.position.y; 
            curRadian += radian;

            UIVertex vertex = new UIVertex();
            vertex.position = new Vector2(realX, realY);
            vertexCache.Add(vertex.position);
            vertex.color = color;
           /* if (i >= inParceBran)
            {
                vertex.color = color;
            }
            else
            {
                //不在访问的都设定为灰色
                vertex.color = new Color(90 / 255f, 90 / 255f, 90 / 255f);
            }*/

            //因为顶点带有负数，所以要变成正数
            vertex.uv0 = new Vector2(x, y) * convertVect + uvCenter;
            toFill.AddVert(vertex);
        }
        rectTransform.sizeDelta  = new Vector2(500,500);
        //输入顶点顺序
        int index = 1;
        for (int i = 0; i < bran; i++)
        { 
            toFill.AddTriangle(index, 0, index + 1);
            index++;
        }
    }

    /// <summary>
    /// 获取Uv的宽度和高度
    /// </summary>
    /// <returns></returns>
    private Vector2 getUvWidAndHei() {
        //获取UV数据。xy分量为min坐标,zw为max坐标
        Vector4 uv = Vector4.zero;
        if (overrideSprite != null)
        {
            uv = DataUtility.GetOuterUV(overrideSprite);
        }

        //获得uv的长度和宽度
        float uvWid = uv.z - uv.x;
        float uvHei = uv.w - uv.y;

        return new Vector2 (uvWid, uvHei);
    }

    /// <summary>
    /// 得到UV坐标中心
    /// </summary>
    /// <returns></returns>
    private Vector2 getUvCenter(){

        Vector2 uvWH = getUvWidAndHei();
        

          //获取当前UI的长度和宽度
        float wid = rectTransform.rect.width;
        float hei = rectTransform.rect.height;

       return new Vector2(uvWH.x  / 2, uvWH.y / 2);
    }
    
    /// <summary>
    /// 获取转换用的坐标向量
    /// </summary>
    /// <returns></returns>
    private Vector2 getConvertVect() {
        Vector2 uvWH = getUvWidAndHei();
        //获取当前UI的长度和宽度
        float wid = rectTransform.rect.width;
        float hei = rectTransform.rect.height;
        return new Vector2(uvWH.x / wid, uvWH.y / hei);
    }

    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        Vector2 localPostion;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform,screenPoint,eventCamera,out localPostion);

        //兼容处理
        if (localPostion == null) {
            return false;
        }
        return IsValid(localPostion);
    }

    /// <summary>
    /// 判断操作是否有效
    /// </summary>
    /// <param name="localPoint"></param>
    /// <returns></returns>
    private bool IsValid(Vector2 localPoint) {
        int num = GetCrossPointNum(localPoint, vertexCache); 
       return num % 2 == 1;
    }

    /// <summary>
    /// 用点击的点沿着屏幕x轴发射一条线与UI轮廓获取次数
    /// </summary>
    /// <param name="localPoint"></param>
    /// <returns></returns>
    private int GetCrossPointNum(Vector2 localPoint,List<Vector2> vertexList) {
        int crossPointNum = 0;
        
        int vertextNum = vertexList.Count; 
        for (int i = 0;i< vertextNum; i++) {
            Vector2 vect1 = vertexList[i];
            Vector2 vect2 = vertexList[(i+1)% vertextNum];

            //做一些过滤，以屏幕的y坐标为参考，y坐标不在线段范围内直接过滤掉
            if (vect1.y > vect2.y)
            {
                if (localPoint.y > vect1.y)
                {
                    continue;
                }

                if (localPoint.y < vect2.y)
                {
                    continue;
                }
            }




            else if (vect2.y > vect1.y) {
                if (localPoint.y > vect2.y)
                {
                    continue;
                }

                if (localPoint.y < vect1.y) {
                    continue;
                }
            }
                 

            //用方程 y = localPosition.y 与 y = kx + b(满足vect2.x,vect2.y与vect1.x，vect1.y的方程相交再判断交点x的范围)
            float k = (vect1.y - vect2.y)/(vect1.x-vect2.x);
            float b = vect2.y - vect2.x * k;
            float localPosX = localPoint.y/k - b/k;

            //不往右边延长
            if (localPosX < localPoint.x) continue;

            if (vect1.x > vect2.x)
            {  
                if (localPosX > vect1.x)
                {
                    continue;
                }
                if (localPosX < vect2.x)
                {
                    continue;
                }
            }
            else if (vect1.x < vect2.x  )
            {
               if (localPosX > vect2.x) {
                    continue;
                }

                if (localPosX < vect1.x) {
                    continue;
                }
            
            }

            //运行到最好++
            crossPointNum ++;
        }

        return crossPointNum;
    }
}

//扩展以下上面的类
[CustomEditor(typeof(CircleImage),true)]
[CanEditMultipleObjects]
public class CircleImageEditor : UnityEditor.UI.ImageEditor {
    SerializedProperty _fillPercent;
    //SerializedProperty _segement;

    protected override void OnEnable() { 
      base.OnEnable();
        _fillPercent = serializedObject.FindProperty("percent");
       // _segement = serializedObject.FindProperty("segement");

    }

    public override void OnInspectorGUI() { 
        base.OnInspectorGUI();

        serializedObject.Update();
        EditorGUILayout.Slider(_fillPercent,0,1,new GUIContent("percent"));
        //EditorGUILayout.PropertyField(_segement);
        
        serializedObject.ApplyModifiedProperties();

        if (GUI.changed) {
            EditorUtility.SetDirty(target);
        }
    }
}