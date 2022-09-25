
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
print(scene.name)
----初始化案例
----案例1 解决UI把物体挡住时依然相应鼠标事件物体
if scene.name == "case001" then
   local case1 = require("Game/init/InitCase1")
   case1.init();
   updateTable.addUpdateFunction(case1.update);
end


