using dnlib.DotNet;
using dnlib.DotNet.Emit;
using RzyFixer.Core;

namespace RzyFixer.Protections
{
    public class CalliFix
    {
        public static string Name => "Calli";
        public static void Execute(ModuleDefMD module)
        {
                foreach (var type in module.GetTypes())
                    foreach (var method in type.Methods)
                    {
                    if (!method.HasBody)
                        continue;
                    for (int i = 2; i < method.Body.Instructions.Count; i++)
                    {
                        if (method.Body.Instructions[i].OpCode.Code == Code.Ldftn && method.Body.Instructions[i + 1].OpCode.Code == Code.Calli)
                        {
                            Logger.Write($"Fixing {Name} in the method: {method.Name} at the line: {i}", Logger.Type.Info);
                            method.Body.Instructions[i].OpCode = OpCodes.Call;
                            method.Body.Instructions[i + 1].OpCode = OpCodes.Nop;
                        }
                    }
               }
          }
     }
}

