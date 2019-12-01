using System;
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
            var registration = false;
            var unregister = false;
            var userSpecific = false;

            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case @"/RegisterUser":
                        registration = true;
                        userSpecific = true;
                        break;
                    case @"/RegisterSystem":
                        registration = true;
                        break;
                    case @"/UnregisterUser":
                        registration = true;
                        unregister = true;
                        userSpecific = true;
                        break;
                    case @"/UnregisterSystem":
                        registration = true;
                        unregister = true;
                        break;
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
