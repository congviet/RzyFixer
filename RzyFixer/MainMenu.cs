using cui.Abstractions;
using cui.Controls;
using dnlib.DotNet;
using dnlib.DotNet.Writer;
using RzyFixer.Core;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace RzyFixer
{

    public class MainMenu : MenuBase
    {
        public static ModuleDefMD module;
        public static Assembly asm;
        public static string directory { get; set; }
        public MainMenu() : base("RzyFixer")
        {
            Console.Title = "RzyFixer - Made by RZY on C.TO";
            Console.ForegroundColor = ConsoleColor.Yellow;
            try { module = ModuleDefMD.Load(directory); asm = Assembly.LoadFrom(directory); }
            catch { Logger.Write("Not a .NET Assembly...", Logger.Type.Error); Console.ReadKey(); Environment.Exit(0); }


            Controls.Add(new Button("Anti De4dot Fixer", sender =>
            {
                Console.Clear();
                try { RzyFixer.Protections.AntiDe4dotFix.Execute(module); }
                catch (Exception e) { Logger.Write($"Error while trying to remove Anti De4dot Protection." + e, Logger.Type.Error); }
                SaveFile();
            }));
            Controls.Add(new Button("Base64 Encoding Fixer", sender =>
            {
                Console.Clear();
                try { RzyFixer.Protections.Base64Fix.Execute(module); }
                catch (Exception e) { Logger.Write($"Error while trying to remove Base64 Encoding Protection." + e, Logger.Type.Error); }
            }));
            Controls.Add(new Button("Call to Calli Fixer", sender =>
            {
                Console.Clear();
                try { RzyFixer.Protections.CalliFix.Execute(module); }
                catch (Exception e) { Logger.Write($"Error while trying to remove Calli Protection." + e, Logger.Type.Error); }
            }));
            Controls.Add(new Button("DoubleParse Fixer", sender =>
            {
                Console.Clear();
                try { RzyFixer.Protections.DoubleParseFix.Execute(module); }
                catch (Exception e) { Logger.Write($"Error while trying to remove Double Parse Protection." + e, Logger.Type.Error); }
                SaveFile();
            }));
            Controls.Add(new Button("Maths Equation Fixer", sender =>
            {
                Console.Clear();
                try { RzyFixer.Protections.MathsFix.Execute(module); }
                catch (Exception e) { Logger.Write($"Error while trying to remove Maths Equations Protection." + e, Logger.Type.Error); }
            }));
            Controls.Add(new Button("SizeOf Fixer", sender =>
            {
                Console.Clear();
                try { RzyFixer.Protections.MathsFix.Execute(module); }
                catch (Exception e) { Logger.Write($"Error while trying to remove SizeOf Protection." + e, Logger.Type.Error); }
            }));

            Controls.Add(new Label("TIPS: Choose the protection to remove by using the arrow on your keyboard. When selected press enter."));
            Controls.Add(new Label($"Assembly: {directory}"));


        }



        

        public static void SaveFile()
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
