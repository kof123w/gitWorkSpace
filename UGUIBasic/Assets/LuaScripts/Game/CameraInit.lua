local CameraInit = {}

local UnityEngine = CS.UnityEngine;
local EventSystems = CS.UnityEngine.EventSystems;

CameraInit.init = function()
	--初始化摄像机
	local mainCamera = UnityEngine.GameObject();
	mainCamera.name = "mainCamera";
	mainCamera:AddComponent(typeof(UnityEngine.Camera));
	mainCamera:AddComponent(typeof(UnityEngine.AudioListener));

	--添加射线检测发射装置
	mainCamera:AddComponent(typeof(EventSystems.PhysicsRaycaster));
end

return CameraInit;