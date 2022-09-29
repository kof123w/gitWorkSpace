-- 3d滚动图实现
local case6 = {}

local UnityEngine = CS.UnityEngine;
local Color = CS.UnityEngine.Color;
local Vector2 = CS.UnityEngine.Vector2;

case6.init = function ()
	--找到画布
	local canvas = UnityEngine.GameObject.Find("Canvas");
	PloyImage = UnityEngine.GameObject("PloyImage");
	rect = PloyImage:AddComponent(typeof(UnityEngine.RectTransform)); 
	rect:SetParent(canvas.transform);
	PloyImage:AddComponent(typeof(CS.CustomPolyImage));
	rect.anchoredPosition = Vector2(0,0);
end

return case6