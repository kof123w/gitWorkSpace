-- 案例3 技能CD
local case2 = {}

--命名控件
local UnityEngine = CS.UnityEngine;
local Vector3 =  CS.UnityEngine.Vector3;
local Vector2 = CS.UnityEngine.Vector2;
local Color = CS.UnityEngine.Color;
local Time = CS.UnityEngine.Time;
local UI = CS.UnityEngine.UI;

--引用的变量
local image;
local updateNum = 0;
case2.init = function ()
	-- 创建UI 一个image
	local imageObject = UnityEngine.GameObject("Image");
	local rectTransform = imageObject:AddComponent(typeof(UnityEngine.RectTransform)); 
	local canvas = UnityEngine.GameObject.Find("Canvas");
	local canvasRectTransform = canvas:GetComponent(typeof(UnityEngine.RectTransform));
	imageObject.transform:SetParent(canvasRectTransform);
	rectTransform.anchoredPosition = Vector3(0,0,0);
	rectTransform.sizeDelta = Vector2(500,500);
	imageObject:AddComponent(typeof(UnityEngine.CanvasRenderer));
	image = imageObject:AddComponent(typeof(CS.CircleImage));
	image.color = Color.white;
	image.sprite=CS.TexManger.Instance:getSpriteByAssetName("minmap"); 
	imageObject:AddComponent(typeof(UI.Button));
end



--声明update函数
case2.update = function()
     if image.percent >=1 then
       image.percent = 0;
     end 
    image.percent = image.percent + Time.deltaTime / 5;
    --修改渲染相关，需要调用ICavasElement的ReBuild()方法才会重新生效
    --需要让其重新刷新以下顶点数据
    image:SetVerticesDirty();
end

return case2;