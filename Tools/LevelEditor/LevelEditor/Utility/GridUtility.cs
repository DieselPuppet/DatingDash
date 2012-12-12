using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor.Utility
{
    public static class GridUtility
    {
        public static void ConfigureInstantCommit(GridView view, RepositoryItem editor)
        {
            EventHandler valueChanged = (object sender, EventArgs e) =>
            {
                view.CloseEditor();
                view.UpdateCurrentRow();
            };

            editor.EditValueChanged += valueChanged;

            EventHandler disposed = null;
            disposed = (object sender, EventArgs e) =>
            {
                view.Disposed -= disposed;
                editor.EditValueChanged -= valueChanged;
            };

            view.Disposed += disposed;
        }
    }
}
