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
    public class MathsFix
    {
        public static string Name => "Math Equation";
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
                            if (method.Body.Instructions[i].OpCode == OpCodes.Add)
                            {
                                if (method.Body.Instructions[i - 1].IsLdcI4() && method.Body.Instructions[i - 2].IsLdcI4())
                                {
                                    int num = method.Body.Instructions[i - 2].GetLdcI4Value() + method.Body.Instructions[i - 1].GetLdcI4Value();
                                    method.Body.Instructions[i].OpCode = OpCodes.Ldc_I4;
                                    method.Body.Instructions[i].Operand = num;
                                    method.Body.Instructions[i - 2].OpCode = OpCodes.Nop;
                                    method.Body.Instructions[i - 1].OpCode = OpCodes.Nop;
                                    Logger.Write($"Fixing {Name} in the method: {method.Name} at the line: {i}", Logger.Type.Info);

                                }
                            }
                            else if (method.Body.Instructions[i].OpCode == OpCodes.Sub && method.Body.Instructions[i - 1].IsLdcI4() && method.Body.Instructions[i - 2].IsLdcI4())
                            {
                                int num3 = method.Body.Instructions[i - 2].GetLdcI4Value() - method.Body.Instructions[i - 1].GetLdcI4Value();
                                method.Body.Instructions[i].OpCode = OpCodes.Ldc_I4;
                                method.Body.Instructions[i].Operand = num3;
                                method.Body.Instructions[i - 2].OpCode = OpCodes.Nop;
                                method.Body.Instructions[i - 1].OpCode = OpCodes.Nop;
                                Logger.Write($"Fixing {Name} in the method: {method.Name} at the line: {i}", Logger.Type.Info);
                            }
                            else if (method.Body.Instructions[i].OpCode == OpCodes.Mul)
                            {
                                if (method.Body.Instructions[i - 1].IsLdcI4() && method.Body.Instructions[i - 2].IsLdcI4())
                                {
                                    int num2 = method.Body.Instructions[i - 2].GetLdcI4Value() * method.Body.Instructions[i - 1].GetLdcI4Value();
                                    method.Body.Instructions[i].OpCode = OpCodes.Ldc_I4;
                                    method.Body.Instructions[i].Operand = num2;
                                    method.Body.Instructions[i - 2].OpCode = OpCodes.Nop;
                                    method.Body.Instructions[i - 1].OpCode = OpCodes.Nop;
                                    Logger.Write($"Fixing {Name} in the method: {method.Name} at the line: {i}", Logger.Type.Info);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
