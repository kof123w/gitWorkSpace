--案例1、点击游戏物体改变一次颜色，被UI遮挡的情况下点击无效
local case1 = {}

--相当于引用命名空间
local UnityEngine = CS.UnityEngine
local Vector3 = CS.UnityEngine.Vector3;
local PrimitiveType = CS.UnityEngine.PrimitiveType;
local UI = CS.UnityEngine.UI;
local Color = CS.UnityEngine.Color;
local Input = CS.UnityEngine.Input; 
local EventSystems = CS.UnityEngine.EventSystems; 

-- 被脚本的全局变量
local cubeObject = nil;  --成本创建的盒子物体
local clickNum = 0;     --鼠标点击次数

case1.init = function ()
	-- 创建一个盒子 并且初始化角度
	cubeObject = UnityEngine.GameObject.CreatePrimitive(PrimitiveType.Cube);
	local initPosOffset = Vector3(0,0,10);
	cubeObject.transform.position = Vector3.zero + initPosOffset;
	cubeObject.transform.localScale = Vector3(2,2,2);
	cubeObject.transform.eulerAngles = Vector3(16,21,0);

	-- 创建UI 一个image
	local imageObject = UnityEngine.GameObject("Image");
	local rectTransform = imageObject:AddComponent(typeof(UnityEngine.RectTransform)); 
	local canvas = UnityEngine.GameObject.Find("Canvas");
	local canvasRectTransform = canvas:GetComponent(typeof(UnityEngine.RectTransform));
	imageObject.transform:SetParent(canvasRectTransform);
	rectTransform.anchoredPosition = Vector3(0,0,0);
	imageObject:AddComponent(typeof(UnityEngine.CanvasRenderer));
	local image = imageObject:AddComponent(typeof(UnityEngine.UI.Image));
	image.color = Color.red;
end

-- 检查有没有点击到UI
local function checkClickUI()
	 checkUtil = CS.CheckIsClickUI.Instance;
	 res = checkUtil:isClickUI();
	 return res;
end

--检查是否点击到物体
local function checkClickObject() 
	 checkUtil = CS.CheckIsClickUI.Instance;
	 res = checkUtil:isClickObject();
	 return res;
end

-- 这个函数里面的update
case1.update = function() 
    if Input:GetMouseButtonDown(0) and cubeObject then
       if checkClickUI() then
          return;
       end

       if checkClickObject()==false then
          return;
       end

       material = cubeObject:GetComponent(typeof(UnityEngine.MeshRenderer)).material;
       if clickNum % 2 == 0 then
          material:SetColor("_Color",UnityEngine.Color.blue);
       else
          material:SetColor("_Color",UnityEngine.Color.green);
       end
       clickNum = clickNum + 1;
    end
end 

return case1;