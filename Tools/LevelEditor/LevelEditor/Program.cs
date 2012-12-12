using LevelData.Log;
using LevelData.Utility;
using LevelEditor.Forms;
using LevelEditor.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelEditor
{
    public class Program
    {
        private static MainForm _mainForm = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // без visual styles, ImageList неверно обрабатывает Transparent color и некоторые картинки могут не отображаться.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += Application_ThreadException;

            if (args.Length == 0)
                _mainForm = new MainForm();                
            else
                _mainForm = new MainForm(args[0]);

            Application.Run(_mainForm);
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string fullDetail = String.Format("Unhandled application error occurred: {0}{1}{0}{0}{2}", Environment.NewLine, e.Exception.Message, e.Exception.ToString());
            bool isLogged = false;

            if (_mainForm != null)
            {
                var log = _mainForm.ActiveMdiChild as ILog;
                if (log != null)
                {
                    log.Write(fullDetail);
                    isLogged = true;
                }
            }
            
            if (isLogged)
                DialogUtility.ShowError("{1}{0}{0}See log tab for detail.", Environment.NewLine, e.Exception.Message);
            else
                DialogUtility.ShowError(fullDetail);
        }
    }
}
