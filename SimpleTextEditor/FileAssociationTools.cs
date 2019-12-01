using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Globalization;

namespace SimpleTextEditor
{
    static class FileAssociationTools
    {
        const string AppId = "Gigaherz.SimpleTextEditor.1";
        const string ProgId = "Gigaherz.SimpleTextEditor.TextFile.1";
        const string FriendlyTypeName = "@shell32.dll,-8975";
        const string DefaultIcon = "@shell32.dll,-47";

        static readonly string OpenWith = string.Format(CultureInfo.InvariantCulture, "\"{0}\" \"%1\"", Application.ExecutablePath);

        static readonly string[] Extensions = new[] { @".txt" };

        internal static void Initialize()
        {
            NativeMethods.SetCurrentProcessExplicitAppUserModelID(AppId);
        }

        internal static bool CheckFileRegistrations()
        {
            return Extensions.All(CheckFileRegistration);
        }

        public static bool CheckFileRegistration(string extension)
        {
            try
            {
                var openWithKey = Registry.ClassesRoot.OpenSubKey(Path.Combine(extension, @"OpenWithProgIds"));
                
                if (openWithKey == null)
                    return false;

                var value = openWithKey.GetValue(ProgId, null) as string;

                return (value != null);
            }
            catch(SecurityException)
            {
            }

            return false;
        }

        public static bool HandleFileAssociationRegistration_RunAs(bool unregister, bool userSpecific)
        {
            string method;

            if (!unregister)
            {
                method = userSpecific ? @"/RegisterUser" : @"/RegisterSystem";
            }
            else
            {
                method = userSpecific ? @"/UnregisterUser" : @"/UnregisterSystem";
            }

            var psi = new ProcessStartInfo(Application.ExecutablePath)
                          {
                              Arguments = method,
                              UseShellExecute = true,
                              Verb = "runas",
                              WindowStyle = ProcessWindowStyle.Hidden
                          };

            //Launch elevated

            try
            {
                var process = Process.Start(psi);
                process.WaitForExit();
                process.Dispose();

                return true;
            }
            catch (Win32Exception)
            {
            }

            return false;
        }

        public static bool HandleFileAssociationRegistration_Elevated(bool unregister, bool userSpecific)
        {
            try
            {
                var root = userSpecific ? Registry.CurrentUser : Registry.LocalMachine;
                
                var classesRoot = root.OpenSubKey(@"Software\Classes", true);

                //First of all, unregister:
                try
                {
                    foreach (var e in Extensions)
                    {
                        RegisterFileAssociation(classesRoot, e, true);
                        RegisterApplicationSupportedType(classesRoot, e, true);
                    }
                    RegisterProgId(classesRoot, true);
                    RegisterApplication(true);
                }
                catch (Exception)
                { }

                if (!unregister)
                {
                    RegisterApplication(false);
                    RegisterProgId(classesRoot, false);
                    foreach (var e in Extensions)
                    {
                        RegisterFileAssociation(classesRoot, e, false);
                        RegisterApplicationSupportedType(classesRoot, e, false);
                    }
                }

                return true;
            }
            catch (Exception)
            {
            }

            return false;
        }

        private static void RegisterFileAssociation(RegistryKey classesRoot, string extension, bool delete)
        {
            try
            {
                var key = classesRoot.CreateSubKey(Path.Combine(extension, "OpenWithProgIds"));
                if (key != null)
                {
                    if (delete)
                        key.DeleteValue(ProgId);
                    else
                        key.SetValue(ProgId, string.Empty);

                    key.Close();
                }
                else
                {
                    Debug.Assert(delete);
                }
            }
            catch (Exception)
            {
                if (!delete)
                    throw;
            }
        }

        private static void RegisterApplication(bool delete)
        {
            try
            {
                if (delete)
                {

                    var key = Registry.ClassesRoot.OpenSubKey("Applications", true);
                    if (key != null)
                    {
                        key.DeleteSubKeyTree(Path.GetFileName(Application.ExecutablePath));
                        key.Close();
                    }
                }
                else
                {
                    var key = Registry.ClassesRoot.CreateSubKey(Path.Combine(@"Applications", Path.GetFileName(Application.ExecutablePath), @"shell"));
                    if (key != null)
                    {
                        key.SetValue(string.Empty, @"open");
                        var openKey = key.CreateSubKey(Path.Combine("open", "command"));
                        if (openKey != null)
                        {
                            openKey.SetValue(string.Empty, OpenWith);
                            openKey.Close();
                        }
                        key.Close();
                    }
                } 
            }
            catch (Exception)
            {
                if (!delete)
                    throw;
            }
        }

        private static void RegisterApplicationSupportedType(RegistryKey classesRoot, string extension, bool delete)
        {
            try
            {
                RegistryKey key = Registry.ClassesRoot.CreateSubKey(Path.Combine(@"Applications", Path.GetFileName(Application.ExecutablePath), @"SupportedTypes"));
                if (key != null)
                {
                    if (delete)
                        key.DeleteValue(extension);
                    else
                        key.SetValue(extension, string.Empty);
                    key.Close();
                }
                else
                {
                    Debug.Assert(delete);
                }
            }
            catch (Exception)
            {
                if (!delete)
                    throw;
            }
        }

        private static void RegisterProgId(RegistryKey classesRoot, bool delete)
        {
            if (delete)
            {
                try
                {
                    classesRoot.DeleteSubKeyTree(ProgId);
                }
                catch (Exception)
                {
                }
            }
            else
            {
                var progIdKey = classesRoot.CreateSubKey(ProgId);
                if (progIdKey != null)
                {
                    progIdKey.SetValue(@"FriendlyTypeName", FriendlyTypeName);
                    progIdKey.SetValue(@"DefaultIcon", DefaultIcon);
                    progIdKey.SetValue(@"CurVer", ProgId);
                    progIdKey.SetValue(@"AppUserModelID", AppId);

                    var shell = progIdKey.CreateSubKey(@"shell");
                    if (shell != null)
                    {
                        shell.SetValue(string.Empty, @"Open");

                        var open = shell.CreateSubKey(@"Open");
                        if (open != null)
                        {
                            var command = open.CreateSubKey(@"Command");
                            if (command != null)
                            {
                                command.SetValue(string.Empty, OpenWith);
                                command.Close();
                            }
                            open.Close();
                        }
                        shell.Close();
                    }
                    progIdKey.Close();
                }
            }
        }
    }
}