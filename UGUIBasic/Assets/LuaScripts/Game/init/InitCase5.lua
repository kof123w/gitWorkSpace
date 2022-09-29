-- 3d滚动图实现
local case5 = {}

local UnityEngine = CS.UnityEngine;
local Color = CS.UnityEngine.Color;

case5.init = function ()
	--找到画布
	local canvas = UnityEngine.GameObject.Find("Canvas");
	scroll3DImage = UnityEngine.GameObject("scroll3DImage");
	rect = scroll3DImage:AddComponent(typeof(UnityEngine.RectTransform));
	rect:SetParent(canvas.transform);
	scroll3DImage:AddComponent(typeof(CS.Scroll3DImage));
end

return case5