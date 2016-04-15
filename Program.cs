using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using Shell32;
using IWshRuntimeLibrary;

namespace ambi {
    class Program {
        [DllImport("user32.dll")]
        public static extern Int32 SwapMouseButton(Int32 bSwap);

        static void Main(string[] args) {
            if (args.Length == 0) {
                Swap();
            } else {
                for (int i = 0; i < args.Length; i++) {
                    string arg = args[i];
                    if (arg == "path") {
                        SetEnvironmentVariable();
                    } else if (arg == "swap") {
                        Swap();
                    } else if (arg == "help") {
                        PrintHelp();
                    } else if (arg == "listen") {
                        StartListener();
                    } else if (arg == "hiddenListener") {
                        StartListen();
                    } else if (arg == "startup") {
                        if (args.Length > 1) {
                            if (args[i + 1] == "remove") {
                                RemoveStartup();
                                break;
                            }
                        }
                        SetStartup();
                    }
                }
            }
        }

        static void SetStartup() {
            var wsh = new IWshShell_Class();
            IWshRuntimeLibrary.IWshShortcut shortcut = wsh.CreateShortcut(
                Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\Ambidextrous.lnk") as IWshRuntimeLibrary.IWshShortcut;
            shortcut.TargetPath = Process.GetCurrentProcess().MainModule.FileName;
            shortcut.Save();
            Console.WriteLine("Added shortcut to Startup folder");
        }

        static void RemoveStartup() {
            System.IO.File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\Ambidextrous.lnk");
            Console.WriteLine("Removed shortcut from Startup folder");
        }

        static void StartListener() {
            Process p = new Process();
            p.StartInfo.FileName = Process.GetCurrentProcess().MainModule.FileName;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.Arguments = "hiddenListener";
            p.Start();
        }

        static void StartListen() {
            Hotkey.RegisterHotKey(Keys.K, KeyModifiers.Alt);
            Hotkey.HotKeyPressed += new EventHandler<HotKeyEventArgs>(HotkeyPressed);
            Console.ReadLine();
        }
        static void HotkeyPressed(object sender, HotKeyEventArgs e) {
            Swap();
        }

        static void SetEnvironmentVariable() {
            try {
                string path = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.User) + ";" + AppDomain.CurrentDomain.BaseDirectory;
                Environment.SetEnvironmentVariable("Path", path, EnvironmentVariableTarget.User);
                Console.WriteLine("Added to current user's PATH");
            } catch {
                Console.WriteLine("Adding to PATH failed - Run the command prompt as administrator!");
            }
        }

        static void Swap() {
            int rightButtonIsAlreadyPrimary = SwapMouseButton(1);
            if (rightButtonIsAlreadyPrimary != 0) {
                SwapMouseButton(0);
            }
            Console.WriteLine("Mouse buttons swapped");
        }

        static void PrintHelp() {
            Console.WriteLine("Ambidextrous - A mouse button swapping utility for Windows");
            Console.WriteLine("For the most recent version & help see the Github repository: http://github.com/Nanofus/ambidextrous");
            Console.WriteLine("(c) Ville Talonpoika 2016 - Licensed under the MIT license");
            Console.WriteLine();
            Console.WriteLine("Command line parameters:");
            Console.WriteLine("ambi - Swap the mouse buttons.");
            Console.WriteLine("ambi listen - Start a background process that swaps on Alt+K.");
            Console.WriteLine("ambi path - Save Ambidextrous in your environment variables to call it from anywhere.");
            Console.WriteLine("ambi startup - Start Ambidextrous when Windows starts.");
            Console.WriteLine("ambi startup remove - Do not start Ambidextrous when Windows starts.");
            Console.WriteLine("ambi help - Show help. But you already knew this.");
        }
    }
}
