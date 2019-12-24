using dnlib.DotNet;
using dnlib.DotNet.Emit;
using RzyFixer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RzyFixer.Protections
{
    public class Base64Fix
    {
        public static string Name => "Base64 Encoding";
        public static void Execute(ModuleDefMD module)
        {
            foreach (var type in module.GetTypes())
                foreach (var method in type.Methods)
                {
                    for (int i = 2; i < method.Body.Instructions.Count; i++)
                    {
                        if (method.Body.Instructions[i].OpCode == OpCodes.Call && method.Body.Instructions[i].Operand.ToString().Contains("get_UTF8") && method.Body.Instructions[i + 1].OpCode == OpCodes.Ldstr && method.Body.Instructions[i + 2].Operand.ToString().Contains("FromBase64String"))
                        {
                            var valuebase64 = System.Convert.FromBase64String(method.Body.Instructions[i + 1].Operand.ToString());
                            Logger.Write($"Fixing {Name} at the offset: {method.Body.Instructions[i].GetOffset().ToString()}", Logger.Type.Info);
                            method.Body.Instructions[i].OpCode = OpCodes.Nop;
                            method.Body.Instructions[i + 1].OpCode = OpCodes.Ldstr;
                            method.Body.Instructions[i + 1].Operand = System.Text.Encoding.UTF8.GetString(valuebase64);
                            method.Body.Instructions[i + 2].OpCode = OpCodes.Nop;
                            method.Body.Instructions[i + 3].OpCode = OpCodes.Nop;

                        }
                    }
                }
            }
        }
    }
