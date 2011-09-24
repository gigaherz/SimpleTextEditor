using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SimpleTextEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool registration = false;
            bool unregister = false;
            bool userSpecific = false;

            if (args.Length > 0)
            {
                if (args[0] == "/RegisterUser")
                {
                    registration = true;
                    userSpecific = true;
                }
                else if (args[0] == "/RegisterSystem")
                {
                    registration = true;
                    userSpecific = true;
                }
                else if (args[0] == "/UnregisterUser")
                {
                    registration = true;
                    unregister = true;
                    userSpecific = true;
                }
                else if (args[0] == "/UnregisterSystem")
                {
                    registration = true;
                    unregister = true;
                    userSpecific = true;
                }
            }

            FileAssociationTools.Initialize();

            if (registration)
            {
                FileAssociationTools.HandleFileAssociationRegistration_Elevated(unregister, userSpecific);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new TextEditorWindow(args));
            }
        }
    }
}
