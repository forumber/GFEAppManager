
namespace GFEAppManager
{
    partial class TriggeringAppListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TriggeringAppsDataGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.TriggeringAppsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // TriggeringAppsDataGrid
            // 
            this.TriggeringAppsDataGrid.AllowUserToAddRows = false;
            this.TriggeringAppsDataGrid.AllowUserToDeleteRows = false;
            this.TriggeringAppsDataGrid.AllowUserToResizeRows = false;
            this.TriggeringAppsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TriggeringAppsDataGrid.Location = new System.Drawing.Point(12, 12);
            this.TriggeringAppsDataGrid.Name = "TriggeringAppsDataGrid";
            this.TriggeringAppsDataGrid.ReadOnly = true;
            this.TriggeringAppsDataGrid.RowHeadersVisible = false;
            this.TriggeringAppsDataGrid.RowTemplate.Height = 25;
            this.TriggeringAppsDataGrid.Size = new System.Drawing.Size(370, 426);
            this.TriggeringAppsDataGrid.TabIndex = 0;
            // 
            // TriggeringAppListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 450);
            this.Controls.Add(this.TriggeringAppsDataGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TriggeringAppListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Triggering Apps";
            ((System.ComponentModel.ISupportInitialize)(this.TriggeringAppsDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView TriggeringAppsDataGrid;
    }
}