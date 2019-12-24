using cui;
using dnlib.DotNet;
using dnlib.DotNet.Writer;
using RzyFixer.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RzyFixer
{
    class Program
    {
      

        static void Main(string[] args)
        {

            MainMenu.directory = args[0];
            new CuiManager(new CuiSettings
            {
                DisableControlC = true
            }).DrawMenu(new MainMenu());


        }
    }
}
