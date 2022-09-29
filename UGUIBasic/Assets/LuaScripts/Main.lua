
main = {  
  
}

 --收集其他类的update函数，并且进行每帧调用
updateTable = {}

main.awake = function ()
   --todo
end

main.start = function()
   --todo
end

main.update = function()
     for k,v in pairs(updateTable) do
         local tmpFunction = v;
         tmpFunction();
    end
end

updateTable.addUpdateFunction = function (fun)
   table.insert(updateTable,fun);
end

require("InitScript")

local SceneManager = CS.UnityEngine.SceneManagement.SceneManager;
local scene = SceneManager:GetActiveScene ();
print("当前场景:"..scene.name)
----初始化案例

----案例1 点击游戏物体改变一次颜色，被UI遮挡的情况下点击无效
if scene.name == "case001" then
   local case1 = require("Game/init/InitCase1")
   case1.init();
   updateTable.addUpdateFunction(case1.update); 
end

-- 案例2 圆形图片的制作
if scene.name == "case002" then
   local case2 = require("Game/init/InitCase2")
   case2.init();
   updateTable.addUpdateFunction(case2.update);
end

-- 案例5 3d滚动的制作
if scene.name == "case005" then
   local case5 = require("Game/init/InitCase5")
   case5.init();
   --updateTable.addUpdateFunction(case2.update);
end

-- 案例6 3d滚动的制作
if scene.name == "case006" then
   local case6 = require("Game/init/InitCase6")
   case6.init();
   --updateTable.addUpdateFunction(case2.update);
end


