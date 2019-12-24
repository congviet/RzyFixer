using dnlib.DotNet;
using RzyFixer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RzyFixer.Protections
{
    public class AntiDe4dotFix
    {
        public static string Name => "AntiDe4dot";
        public static void Execute(ModuleDefMD module)
        {
            for (int i = 0; i < module.Types.Count; i++)
            {
                TypeDef type = module.Types[i];
                if (type.HasInterfaces)
                {
                    for (int j = 0; j < type.Interfaces.Count; j++)
                    {
                        if (type.Interfaces[j].Interface != null)
                        {
                            if (type.Interfaces[j].Interface.Name.Contains(type.Name) ||
                            type.Name.Contains(type.Interfaces[j].Interface.Name))
                            {
                                module.Types.RemoveAt(i);
                                Logger.Write($"Fixing Anti De4dot", Logger.Type.Info);
                            }
                        }
                    }
                }
            }
        }
    }
}
