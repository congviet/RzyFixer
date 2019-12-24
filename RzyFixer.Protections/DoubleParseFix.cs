using dnlib.DotNet;
using dnlib.DotNet.Emit;
using RzyFixer.Core;

namespace RzyFixer.Protections
{
    public class DoubleParseFix
    {
        public static string Name => "Double Parse";
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

                            if (method.Body.Instructions[i].OpCode == OpCodes.Call && method.Body.Instructions[i].Operand.ToString().Contains("Parse") && method.Body.Instructions[i + 1].OpCode == OpCodes.Conv_I4 && method.Body.Instructions[i - 1].OpCode == OpCodes.Ldstr)
                            {
                                Logger.Write($"Fixing {Name} at the offset: {method.Body.Instructions[i].GetOffset().ToString()}", Logger.Type.Info);
                                string valueToPut = method.Body.Instructions[i - 1].Operand.ToString();
                                string secondvalue = method.Body.Instructions[i - 2].Operand.ToString();
                                method.Body.Instructions[i].OpCode = OpCodes.Nop;
                                method.Body.Instructions[i - 2].OpCode = OpCodes.Ldc_I4;
                                method.Body.Instructions[i - 2].Operand = (int)double.Parse(secondvalue) + (int)double.Parse(valueToPut);
                                method.Body.Instructions[i - 1].OpCode = OpCodes.Nop;
                                method.Body.Instructions[i + 2].OpCode = OpCodes.Nop;
                                decrypted++;
                            }
                       }
                   }
              }
          }
     }

