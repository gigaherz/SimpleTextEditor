
using System;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SimpleTextEditor
{
    static class FileAssociationTools
    {
        const string AppId = "SimpleTextEditor";
        const string ProgId = "SimpleTextEditor.TextFile";
        const string FriendlyTypeName = "@shell32.dll,-8975";
        const string DefaultIcon = "@shell32.dll,-47";

        static string[] Extensions = new string[] { ".txt" };

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
        
        public static bool HandleFileAssociationRegistration(bool unregister, bool userSpecific)
        {
            //if (HandleFileAssociationRegistration_Elevated(unregister, userSpecific))
            //    return true;

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

            string openWith = string.Format("\"{0}\" %1", Application.ExecutablePath);

            try
            {
                if (userSpecific)
                    classesRoot = Registry.CurrentUser.OpenSubKey(@"Software\Classes", true);
                else
                    classesRoot = Registry.LocalMachine.OpenSubKey(@"Software\Classes", true);

                try
                {
                    //First of all, unregister:
                    foreach (var e in Extensions)
                        RegisterFileAssociation(classesRoot, ProgId, e, true);
                    UnregisterProgId(classesRoot, ProgId);
                }
                catch (Exception)
                { }

                if (!unregister)
                {
                    RegisterProgId(classesRoot, ProgId, AppId, openWith);
                    foreach(var e in Extensions)
                        RegisterFileAssociation(classesRoot, ProgId, e, false);
                }

                return true;
            }
            catch (Exception)
            {
            }

            return false;
        }

        public static void RegisterFileAssociation(RegistryKey classesRoot, string progId, string extension, bool delete)
        {
            RegistryKey key = classesRoot.CreateSubKey(Path.Combine(extension, "OpenWithProgIds"));
            if (delete)
                key.DeleteValue(progId);
            else
                key.SetValue(progId, string.Empty);
            key.Close();
        }

        private static void RegisterProgId(RegistryKey classesRoot, string progId, string appId, string openWith)
        {
            RegistryKey progIdKey = classesRoot.CreateSubKey(progId);
            progIdKey.SetValue("FriendlyTypeName", FriendlyTypeName);
            progIdKey.SetValue("DefaultIcon", DefaultIcon);
            progIdKey.SetValue("CurVer", progId);
            progIdKey.SetValue("AppUserModelID", appId);

            RegistryKey shell = progIdKey.CreateSubKey("shell");
            shell.SetValue(string.Empty, "Open");
            shell = shell.CreateSubKey("Open");
            shell = shell.CreateSubKey("Command");
            shell.SetValue(string.Empty, openWith);
            shell.Close();

            progIdKey.Close();
        }

        public static void UnregisterProgId(RegistryKey classesRoot, string progId)
        {
            classesRoot.DeleteSubKeyTree(progId);
        }

    }
}
