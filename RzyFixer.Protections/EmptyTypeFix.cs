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
    public class EmptyTypeFix
    {
        public static string Name => "EmptyType";
        public static void Execute(ModuleDefMD module)
        {
            foreach (var type in module.GetTypes())
                foreach (var method in type.Methods)
                {
                    for (int i = 2; i < method.Body.Instructions.Count; i++)
                    {
                        if (method.Body.Instructions[i].OpCode == OpCodes.Ldsfld && method.Body.Instructions[i].Operand.ToString().Contains("EmptyTypes") && method.Body.Instructions[i + 1].OpCode == OpCodes.Ldlen && method.Body.Instructions[i + 2].OpCode == OpCodes.Add && method.Body.Instructions[i + 3].OpCode == OpCodes.Ldc_I4)
                        {
                            Logger.Write($"Fixing {Name} in the method: {method.Name} at the line: {i}", Logger.Type.Info);
                            method.Body.Instructions[i].OpCode = OpCodes.Nop;
                            method.Body.Instructions[i + 1].OpCode = OpCodes.Nop;
                            method.Body.Instructions[i + 2].OpCode = OpCodes.Nop;
                            method.Body.Instructions[i + 3].OpCode = OpCodes.Nop;
                            method.Body.Instructions[i + 4].OpCode = OpCodes.Nop;


                        }
                    }
                }
            }
        }
    }
