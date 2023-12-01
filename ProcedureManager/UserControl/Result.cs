using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcedureManager
{
    public partial class Result : UserControl
    {
        public Result()
        {
            InitializeComponent();
        }

        public void SetDataGrid(DataSet? dataSet)
        {
            tc_Result.TabPages.Clear();
            if (dataSet != null)
            {
                for (int i = 0; i < dataSet.Tables.Count; i++)
                {
                    DataGridView dataGridView = new DataGridView();
                    dataGridView.Dock = DockStyle.Fill;
                    dataGridView.AllowUserToDeleteRows = false;
                    dataGridView.AllowUserToAddRows = false;
                    dataGridView.AllowUserToResizeRows = false;
                    dataGridView.ReadOnly = true;
                    dataGridView.DataSource = dataSet.Tables[i];

                    TabPage tabPage = new TabPage();
                    tabPage.Text = $"Table {i + 1}";
                    tabPage.Controls.Add(dataGridView);

                    tc_Result.TabPages.Add(tabPage);
                }
            }
        }
    }
}
