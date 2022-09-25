local LightInit = {}

local UnityEngine = CS.UnityEngine;

--初始化默认的直射光
LightInit.init = function()
	local light = UnityEngine.GameObject();
	light.name = "DirectionalLight";
	local lightComponent = light:AddComponent(typeof(UnityEngine.Light));

	--设定光线为平行光
	local LightType = UnityEngine.LightType;
	lightComponent.type = LightType.Directional;

    --设定阴影类型
    local ShadowType = UnityEngine.LightShadows;
    lightComponent.shadows = ShadowType.Soft; 
end

return LightInit;