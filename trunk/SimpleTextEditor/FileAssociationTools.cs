
using System;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
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

        static string openWith = string.Format(CultureInfo.InvariantCulture, "\"{0}\" \"%1\"", Application.ExecutablePath);

        static string[] Extensions = new string[] { ".txt" };

        internal static void Initialize()
        {
            NativeMethods.SetCurrentProcessExplicitAppUserModelID(AppId);
        }

        internal static bool CheckFileRegistrations()
        {
            foreach (var s in Extensions)
            {
                if (!CheckFileRegistration(s))
                    return false;
            }
            return true;
        }

        public static bool CheckFileRegistration(string extension)
        {
            bool registered = false;

            try
            {
                RegistryKey openWithKey = Registry.ClassesRoot.OpenSubKey(Path.Combine(extension, "OpenWithProgIds"));
                string value = openWithKey.GetValue(ProgId, null) as string;

                registered = (value != null);
            }
            finally
            {
            }
            return registered;
        }

        public static bool HandleFileAssociationRegistration_RunAs(bool unregister, bool userSpecific)
        {
            string method = "";

            if (!unregister)
            {
                if (userSpecific)
                    method = "/RegisterUser";
                else
                    method = "/RegisterSystem";
            }
            else
            {
                if (userSpecific)
                    method = "/UnregisterUser";
                else
                    method = "/UnregisterSystem";
            }

            ProcessStartInfo psi = new ProcessStartInfo(Application.ExecutablePath);
            psi.Arguments = method;
            psi.UseShellExecute = true;
            psi.Verb = "runas"; //Launch elevated
            psi.WindowStyle = ProcessWindowStyle.Hidden;

            try
            {
                var process = Process.Start(psi);
                process.WaitForExit();
                process.Dispose();

                return true;
            }
            catch (Exception)
            {
            }

            return false;
        }

        public static bool HandleFileAssociationRegistration_Elevated(bool unregister, bool userSpecific)
        {
            RegistryKey classesRoot;

            try
            {
                if (userSpecific)
                    classesRoot = Registry.CurrentUser.OpenSubKey(@"Software\Classes", true);
                else
                    classesRoot = Registry.LocalMachine.OpenSubKey(@"Software\Classes", true);

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
                RegistryKey key = classesRoot.CreateSubKey(Path.Combine(extension, "OpenWithProgIds"));
                if (delete)
                    key.DeleteValue(ProgId);
                else
                    key.SetValue(ProgId, string.Empty);
                key.Close();
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

                    RegistryKey key = Registry.ClassesRoot.OpenSubKey("Applications", true);
                    key.DeleteSubKeyTree(Path.GetFileName(Application.ExecutablePath));
                    key.Close();
                }
                else
                {
                    RegistryKey key = Registry.ClassesRoot.CreateSubKey(Path.Combine("Applications", Path.GetFileName(Application.ExecutablePath), "shell"));
                    key.SetValue(string.Empty, "open");
                    RegistryKey openKey = key.CreateSubKey(Path.Combine("open", "command"));
                    openKey.SetValue(string.Empty, openWith);
                    openKey.Close();
                    key.Close();
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
                RegistryKey key = Registry.ClassesRoot.CreateSubKey(Path.Combine("Applications", Path.GetFileName(Application.ExecutablePath), "SupportedTypes"));
                if (delete)
                    key.DeleteValue(extension);
                else
                    key.SetValue(extension, "", RegistryValueKind.String);
                key.Close();
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
                RegistryKey progIdKey = classesRoot.CreateSubKey(ProgId);
                progIdKey.SetValue("FriendlyTypeName", FriendlyTypeName);
                progIdKey.SetValue("DefaultIcon", DefaultIcon);
                progIdKey.SetValue("CurVer", ProgId);
                progIdKey.SetValue("AppUserModelID", AppId);

                RegistryKey shell = progIdKey.CreateSubKey("shell");
                shell.SetValue(string.Empty, "Open");
                shell = shell.CreateSubKey("Open");
                shell = shell.CreateSubKey("Command");
                shell.SetValue(string.Empty, openWith);
                shell.Close();

                progIdKey.Close();
            }
        }
    }
}