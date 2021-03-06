﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RegexDocs.General
{
    public class Logging
    {
        private static readonly string Assemblyfolder =
          Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\";
        private static readonly string Logfile = Assemblyfolder + "console.log";

        public static void WriteLog(string texto, bool carriage = false)
        {
            var now = DateTime.Now;
            var date = "[" + now.ToString("MM/dd/yyyy") + " - " + now.ToString("H:mm:ss") + "] ";

            try
            {
                if (carriage == false)
                {
                    texto = Environment.NewLine + "[Process " + Process.GetCurrentProcess().Id.ToString() + "]" + date + texto;
                    if (!File.Exists(Logfile)) File.WriteAllText(Logfile, texto);
                    else File.AppendAllText(Logfile, texto);
                    Console.Write(texto);
                }
                else
                {
                    var fileContent = File.ReadLines(Logfile).ToList();
                    texto = date + texto;
                    fileContent[fileContent.Count - 1] = texto;
                    File.WriteAllLines(Logfile, fileContent);
                    File.WriteAllText(Logfile, File.ReadAllText(Logfile).Substring(0, File.ReadAllText(Logfile).Length - 2));

                    // Limpar conteúdo da última linha do console:
                    int currentLineCursor = Console.CursorTop;
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, currentLineCursor);
                    //
                    Console.Write(texto);
                }
            }
            catch (Exception e)
            {
                texto = texto + e;
                Console.Write(texto);
            }
        }
    }
}