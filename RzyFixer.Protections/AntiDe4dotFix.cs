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
                if (module.Types[i].HasInterfaces)
                {
                    for (int j = 0; j < module.Types[i].Interfaces.Count; j++)
                    {
                        if (module.Types[i].Interfaces[j].Interface != null)
                        {
                            if (module.Types[i].Interfaces[j].Interface.Name.Contains(module.Types[i].Name) ||
                            module.Types[i].Name.Contains(module.Types[i].Interfaces[j].Interface.Name))
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
