using dnlib.DotNet;
using dnlib.DotNet.Emit;
using RzyFixer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RzyFixer.Protections
{
    public class SizeOfFix
    {
        public static string Name => "SizeOf";
        public static void Execute(ModuleDefMD module)
        {
            foreach (TypeDef typeDef in module.Types)
            {
                foreach (MethodDef method in typeDef.Methods)
                {
                    if (method.HasBody)
                    {
                        for (int i = 0; i < method.Body.Instructions.Count; i++)
                        {
                            if (method.Body.Instructions[i].OpCode == OpCodes.Sizeof)
                            {
                                string key;
                                switch (key = (method.Body.Instructions[i].Operand as ITypeDefOrRef).ToString())
                                {
                                    case "System.Boolean":
                                        method.Body.Instructions[i].OpCode = OpCodes.Ldc_I4_1;
                                        Logger.Write($"Fixing {Name} at the offset: {method.Body.Instructions[i].GetOffset().ToString()}", Logger.Type.Info);
                                        break;
                                    case "System.Byte":
                                        method.Body.Instructions[i].OpCode = OpCodes.Ldc_I4_1;
                                        Logger.Write($"Fixing {Name} at the offset: {method.Body.Instructions[i].GetOffset().ToString()}", Logger.Type.Info);
                                        break;
                                    case "System.Decimal":
                                        method.Body.Instructions[i] = OpCodes.Ldc_I4.ToInstruction(16);
                                        Logger.Write($"Fixing {Name} at the offset: {method.Body.Instructions[i].GetOffset().ToString()}", Logger.Type.Info);
                                        break;
                                    case "System.Double":
                                    case "System.Int64":
                                    case "System.UInt64":
                                        method.Body.Instructions[i].OpCode = OpCodes.Ldc_I4_8;
                                        Logger.Write($"Fixing {Name} at the offset: {method.Body.Instructions[i].GetOffset().ToString()}", Logger.Type.Info);
                                        break;
                                    case "System.Guid":
                                        method.Body.Instructions[i] = OpCodes.Ldc_I4.ToInstruction(16);
                                        Logger.Write($"Fixing {Name} at the offset: {method.Body.Instructions[i].GetOffset().ToString()}", Logger.Type.Info);
                                        break;
                                    case "System.Int16":
                                    case "System.UInt16":
                                        method.Body.Instructions[i].OpCode = OpCodes.Ldc_I4_2;
                                        Logger.Write($"Fixing {Name} at the offset: {method.Body.Instructions[i].GetOffset().ToString()}", Logger.Type.Info);
                                        break;
                                    case "System.Int32":
                                    case "System.Single":
                                    case "System.UInt32":
                                        method.Body.Instructions[i].OpCode = OpCodes.Ldc_I4_4;
                                        Logger.Write($"Fixing {Name} at the offset: {method.Body.Instructions[i].GetOffset().ToString()}", Logger.Type.Info);
                                        break;
                                    case "System.SByte":
                                        method.Body.Instructions[i].OpCode = OpCodes.Ldc_I4_1;
                                        Logger.Write($"Fixing {Name} at the offset: {method.Body.Instructions[i].GetOffset().ToString()}", Logger.Type.Info);
                                        break;
                                }
                               
                            }
                        }
                    }
                }
            }
        }
    }
}
