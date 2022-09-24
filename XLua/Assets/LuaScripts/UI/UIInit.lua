-- 主要用于初始化UI界面
local UIInit = {}

local UnityEngine = CS.UnityEngine;
local UI = CS.UnityEngine.UI;
local EventSystems = CS.UnityEngine.EventSystems;

UIInit.init = function()
	--创建UI Canvas
	local cas = UnityEngine.GameObject();
    cas.name = 'Canvas';
    cas:AddComponent(typeof(UnityEngine.RectTransform));
    cas:AddComponent(typeof(UnityEngine.Canvas));
    cas:AddComponent(typeof(UI.CanvasScaler));
    cas:AddComponent(typeof(UI.GraphicRaycaster));
    
    --创建UI 事件系统
    local eSystem = UnityEngine.GameObject();
    eSystem.name = 'EventSystem';
    eSystem:AddComponent(typeof(EventSystems.EventSystem));
    eSystem:AddComponent(typeof(EventSystems.StandaloneInputModule));
end

return UIInit;