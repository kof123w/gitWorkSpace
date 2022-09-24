
require("InitScript")

main = {  
  
}

 --收集其他类的update函数，并且进行每帧调用
local updateTable = {}

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

return updateTable;




