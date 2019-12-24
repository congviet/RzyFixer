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
            int decrypted = 0;
                foreach (var type in module.GetTypes())
                    foreach (var method in type.Methods)
                    {
                    if (!method.HasBody)
                        continue;
                    var instructions = method.Body.Instructions;
                    for (int i = 2; i < instructions.Count; i++)
                    {

                        if (instructions[i].OpCode.Code == Code.Ldftn && instructions[i + 1].OpCode.Code == Code.Calli)
                        {
                            Logger.Write($"Fixing {Name} at the offset: {method.Body.Instructions[i].GetOffset().ToString()}", Logger.Type.Info);
                            instructions[i].OpCode = OpCodes.Call;
                            instructions[i + 1].OpCode = OpCodes.Nop;
                            decrypted++;

                        }
                    }
               }
          }
     }
}

