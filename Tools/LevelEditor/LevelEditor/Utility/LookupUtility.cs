using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor.Utility
{
    public static class LookupUtility
    {
        private const string DefaultNullText = "<None>";

        public static void Configure(RepositoryItemLookUpEdit cmb, string[] dataSource, string nullText = DefaultNullText)
        {
            cmb.NullText = nullText;
            cmb.ShowHeader = false;
            cmb.ShowFooter = false;

            cmb.DataSource = dataSource;
        }
    }
}
