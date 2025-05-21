namespace MTM_WIP_App.Main_Form
{
    internal class DgvDesigner
    {
        // Configures the appearance and behavior of a DataGridView control.

        public static void SizeDataGrid(DataGridView dataGridView)
        {
            try
            {
                if (dataGridView.Columns.Count == 0)
                {
                    AppLogger.Log("SizeDataGrid: No columns to size.");
                }
                else
                {
                    int i;
                    for (i = 0; i < dataGridView.Columns.Count; i++)
                    {
                        dataGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    }

                    dataGridView.Columns[i - 1].Frozen = false;
                    dataGridView.Columns[i - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    dataGridView.ColumnHeadersVisible = true;
                    dataGridView.RowHeadersVisible = false;
                    dataGridView.Font = new Font(dataGridView.Font.Name, SQL.Default.Theme_TextSize,
                        dataGridView.Font.Style);

                    AppLogger.Log("SizeDataGrid: DataGridView sized successfully.");
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in SizeDataGrid: " + ex.Message);
            }
        }

        public static void InitializeDataGridView(DataGridView dataGridView, string theme)
        {
            try
            {
                AppLogger.Log("Initializing DataGridView with theme: " + theme);

                string bgColorHex;
                Color bgColor;
                string selBackColorHex;
                Color selBackColor;
                string selForeColorHex;
                Color selForeColor;
                string rowBackColorHex;
                Color rowBackColor;
                string aRowBackColorHex;
                Color aRowBackColor;

                SizeDataGrid(dataGridView);

                // Light Grey Theme (Default Theme)
                if (theme == "Default (Black and White)")
                {
                    //------------------------------------------//
                    dataGridView.BackgroundColor = Color.LightGray;
                    dataGridView.BorderStyle = BorderStyle.Fixed3D;
                    dataGridView.DefaultCellStyle.SelectionBackColor = Color.Blue;
                    dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
                    dataGridView.RowsDefaultCellStyle.BackColor = Color.White;
                    dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
                    dataGridView.DefaultCellStyle.Font = new Font(dataGridView.Font.Name, SQL.Default.Theme_TextSize,
                        dataGridView.Font.Style);
                    dataGridView.AutoResizeColumns();
                }

                // Light Blue Theme
                if (theme == "Light Blue")
                {
                    dataGridView.BackgroundColor = Color.LightGray;
                    dataGridView.BorderStyle = BorderStyle.Fixed3D;
                    dataGridView.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue;
                    dataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;
                    dataGridView.RowsDefaultCellStyle.BackColor = Color.LightBlue;
                    dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.Azure;
                    dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
                    dataGridView.RowHeadersDefaultCellStyle.BackColor = Color.Black;
                    dataGridView.DefaultCellStyle.Font = new Font(dataGridView.Font.Name, SQL.Default.Theme_TextSize,
                        dataGridView.Font.Style);
                    dataGridView.AutoResizeColumns();
                }

                // Light Red Theme
                if (theme == "Light Red")
                {
                    bgColorHex = "#EEEEEE";
                    bgColor = ColorTranslator.FromHtml(bgColorHex); // Light Grey
                    selBackColorHex = "#0000ff";
                    selBackColor = ColorTranslator.FromHtml(selBackColorHex); // Dark Grey/Blue
                    selForeColorHex = "#ffffff";
                    selForeColor = ColorTranslator.FromHtml(selForeColorHex); // Off-White
                    rowBackColorHex = "#FF0000";
                    rowBackColor = ColorTranslator.FromHtml(rowBackColorHex); // Dark Grey
                    aRowBackColorHex = "#f4cccc";
                    aRowBackColor = ColorTranslator.FromHtml(aRowBackColorHex); // Off-Grey
                    //------------------------------------------//
                    dataGridView.BackgroundColor = bgColor;
                    dataGridView.BorderStyle = BorderStyle.Fixed3D;
                    dataGridView.DefaultCellStyle.SelectionBackColor = selBackColor;
                    dataGridView.DefaultCellStyle.SelectionForeColor = selForeColor;
                    dataGridView.RowsDefaultCellStyle.BackColor = rowBackColor;
                    dataGridView.AlternatingRowsDefaultCellStyle.BackColor = aRowBackColor;
                    dataGridView.DefaultCellStyle.Font = new Font(dataGridView.Font.Name, SQL.Default.Theme_TextSize,
                        dataGridView.Font.Style);
                    dataGridView.AutoResizeColumns();
                }

                // Light Grey Theme
                if (theme == "Light Grey")
                {
                    bgColorHex = "#EEEEEE";
                    bgColor = ColorTranslator.FromHtml(bgColorHex); // Light Grey
                    selBackColorHex = "#434C5B";
                    selBackColor = ColorTranslator.FromHtml(selBackColorHex); // Dark Grey/Blue
                    selForeColorHex = "#F5F3F5";
                    selForeColor = ColorTranslator.FromHtml(selForeColorHex); // Off-White
                    rowBackColorHex = "#a6a6a6";
                    rowBackColor = ColorTranslator.FromHtml(rowBackColorHex); // Dark Grey
                    aRowBackColorHex = "#cccccc";
                    aRowBackColor = ColorTranslator.FromHtml(aRowBackColorHex); // Off-Grey
                    //------------------------------------------//
                    dataGridView.BackgroundColor = bgColor;
                    dataGridView.BorderStyle = BorderStyle.Fixed3D;
                    dataGridView.DefaultCellStyle.SelectionBackColor = selBackColor;
                    dataGridView.DefaultCellStyle.SelectionForeColor = selForeColor;
                    dataGridView.RowsDefaultCellStyle.BackColor = rowBackColor;
                    dataGridView.AlternatingRowsDefaultCellStyle.BackColor = aRowBackColor;
                    dataGridView.DefaultCellStyle.Font = new Font(dataGridView.Font.Name, SQL.Default.Theme_TextSize,
                        dataGridView.Font.Style);
                    dataGridView.AutoResizeColumns();
                }

                AppLogger.Log("DataGridView initialized successfully with theme: " + theme);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in InitializeDataGridView: " + ex.Message);
            }
        }
    }
}