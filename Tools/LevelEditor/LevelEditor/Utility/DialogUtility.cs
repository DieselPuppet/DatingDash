using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelEditor.Utility
{
    public static class DialogUtility
    {
        public static void ShowInfo(string text)
        {
            Show(MessageBoxButtons.OK, MessageBoxIcon.Information, "Info", text);
        }

        public static void ShowInfo(string format, params object[] args)
        {
            ShowInfo(String.Format(format, args));
        }

        public static void ShowWarning(string text)
        {
            Show(MessageBoxButtons.OK, MessageBoxIcon.Warning, "Warning", text);
        }

        public static void ShowWarning(string format, params object[] args)
        {
            ShowWarning(String.Format(format, args));
        }

        public static void ShowError(string text)
        {
            Show(MessageBoxButtons.OK, MessageBoxIcon.Error, "Error", text);
        }

        public static void ShowError(string format, params object[] args)
        {
            ShowError(String.Format(format, args));
        }

        public static DialogResult ShowConfirm(bool canCancel, string format, params object[] args)
        {
            return ShowConfirm(canCancel, String.Format(format, args));
        }

        public static DialogResult ShowConfirm(bool canCancel, string text)
        {
            return Show((canCancel ? MessageBoxButtons.YesNoCancel : MessageBoxButtons.YesNo), MessageBoxIcon.Question, "Confirm", text);
        }

        private static DialogResult Show(MessageBoxButtons buttons, MessageBoxIcon icon, string caption, string format, params object[] args)
        {
            return Show(buttons, icon, caption, String.Format(format, args));
        }

        private static DialogResult Show(MessageBoxButtons buttons, MessageBoxIcon icon, string caption, string text)
        {
            return XtraMessageBox.Show(text, caption, buttons, icon);
        }
    }
}
