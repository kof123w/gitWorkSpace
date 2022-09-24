local CameraInit = {}

local UnityEngine = CS.UnityEngine;

CameraInit.init = function()
	--初始化摄像机
	local mainCamera = UnityEngine.GameObject();
	mainCamera.name = "mainCamera";
	mainCamera:AddComponent(typeof(UnityEngine.Camera));
	mainCamera:AddComponent(typeof(UnityEngine.AudioListener));
end

return CameraInit;