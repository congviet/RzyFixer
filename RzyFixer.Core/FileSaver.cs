using dnlib.DotNet;
using dnlib.DotNet.Writer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RzyFixer.Core
{
    public class FileSaver
    {

        public static void SaveFile(string directory, ModuleDefMD module)
        {
            string text = Path.GetDirectoryName(directory);
            if (!text.EndsWith("\\"))
            {
                text += "\\";
            }
            string filename = string.Format("{0}{1}-Desintegrated{2}", text, Path.GetFileNameWithoutExtension(directory), Path.GetExtension(directory));
            ModuleWriterOptions writerOptions = new ModuleWriterOptions(module);
            writerOptions.MetadataOptions.Flags |= MetadataFlags.PreserveAll;
            writerOptions.Logger = DummyLogger.NoThrowInstance;
            NativeModuleWriterOptions NativewriterOptions = new NativeModuleWriterOptions(module, true);
            NativewriterOptions.MetadataOptions.Flags |= MetadataFlags.PreserveAll;
            NativewriterOptions.Logger = DummyLogger.NoThrowInstance;
            if (module.IsILOnly) { module.Write(filename, writerOptions); } else { module.NativeWrite(filename, NativewriterOptions); }

            Logger.Write($"File saved at: {filename}", Logger.Type.Done);
            Console.ReadKey();
            Environment.Exit(0);
        }

    }
}
