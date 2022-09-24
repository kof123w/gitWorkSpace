--这个脚本专门用来初始化的  

-- 初始化摄像机
local CameraInit = require("Game/CameraInit")  
CameraInit.init();

-- 初始化默认的光线
--local LightInit = require("Game/LightInit")
--LightInit.init();

-- 初始化UI界面
local UIInit = require("UI/UIInit")   
UIInit.init();

