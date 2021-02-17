
using System.Collections.Generic;

namespace GFEAppManager
{
    partial class ManageApplicationsForm
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
            this.ListeningAppsList = new System.Windows.Forms.ListBox();
            this.ListeningAppsLabel = new System.Windows.Forms.Label();
            this.CurrentlyRunningAppsLabel = new System.Windows.Forms.Label();
            this.CurrentlyRunningAppsList = new System.Windows.Forms.ListBox();
            this.RefreshCurrentlyRunningListButton = new System.Windows.Forms.Button();
            this.AddFromListButton = new System.Windows.Forms.Button();
            this.AppNameTextBox = new System.Windows.Forms.TextBox();
            this.AddToListButton = new System.Windows.Forms.Button();
            this.RemoveFromTheListButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ListeningAppsList
            // 
            this.ListeningAppsList.FormattingEnabled = true;
            this.ListeningAppsList.ItemHeight = 15;
            this.ListeningAppsList.Location = new System.Drawing.Point(12, 42);
            this.ListeningAppsList.Name = "ListeningAppsList";
            this.ListeningAppsList.Size = new System.Drawing.Size(193, 319);
            this.ListeningAppsList.TabIndex = 0;
            // 
            // ListeningAppsLabel
            // 
            this.ListeningAppsLabel.AutoSize = true;
            this.ListeningAppsLabel.Location = new System.Drawing.Point(12, 24);
            this.ListeningAppsLabel.Name = "ListeningAppsLabel";
            this.ListeningAppsLabel.Size = new System.Drawing.Size(82, 15);
            this.ListeningAppsLabel.TabIndex = 1;
            this.ListeningAppsLabel.Text = "Apps to listen:";
            // 
            // CurrentlyRunningAppsLabel
            // 
            this.CurrentlyRunningAppsLabel.AutoSize = true;
            this.CurrentlyRunningAppsLabel.Location = new System.Drawing.Point(294, 24);
            this.CurrentlyRunningAppsLabel.Name = "CurrentlyRunningAppsLabel";
            this.CurrentlyRunningAppsLabel.Size = new System.Drawing.Size(104, 15);
            this.CurrentlyRunningAppsLabel.TabIndex = 3;
            this.CurrentlyRunningAppsLabel.Text = "Currently running:";
            // 
            // CurrentlyRunningAppsList
            // 
            this.CurrentlyRunningAppsList.FormattingEnabled = true;
            this.CurrentlyRunningAppsList.ItemHeight = 15;
            this.CurrentlyRunningAppsList.Location = new System.Drawing.Point(294, 43);
            this.CurrentlyRunningAppsList.Name = "CurrentlyRunningAppsList";
            this.CurrentlyRunningAppsList.Size = new System.Drawing.Size(193, 319);
            this.CurrentlyRunningAppsList.TabIndex = 4;
            // 
            // RefreshCurrentlyRunningListButton
            // 
            this.RefreshCurrentlyRunningListButton.Location = new System.Drawing.Point(294, 368);
            this.RefreshCurrentlyRunningListButton.Name = "RefreshCurrentlyRunningListButton";
            this.RefreshCurrentlyRunningListButton.Size = new System.Drawing.Size(75, 23);
            this.RefreshCurrentlyRunningListButton.TabIndex = 5;
            this.RefreshCurrentlyRunningListButton.Text = "Refresh";
            this.RefreshCurrentlyRunningListButton.UseVisualStyleBackColor = true;
            this.RefreshCurrentlyRunningListButton.Click += new System.EventHandler(this.RefreshCurrentlyRunningListButton_Click);
            // 
            // AddFromListButton
            // 
            this.AddFromListButton.Location = new System.Drawing.Point(211, 183);
            this.AddFromListButton.Name = "AddFromListButton";
            this.AddFromListButton.Size = new System.Drawing.Size(75, 23);
            this.AddFromListButton.TabIndex = 0;
            this.AddFromListButton.Text = "<";
            this.AddFromListButton.UseVisualStyleBackColor = true;
            this.AddFromListButton.Click += new System.EventHandler(this.AddFromListButton_Click);
            // 
            // AppNameTextBox
            // 
            this.AppNameTextBox.Location = new System.Drawing.Point(13, 367);
            this.AppNameTextBox.Name = "AppNameTextBox";
            this.AppNameTextBox.Size = new System.Drawing.Size(192, 23);
            this.AppNameTextBox.TabIndex = 6;
            this.AppNameTextBox.Text = "example.exe";
            this.AppNameTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AppNameTextBox_KeyDown);
            // 
            // AddToListButton
            // 
            this.AddToListButton.Location = new System.Drawing.Point(12, 397);
            this.AddToListButton.Name = "AddToListButton";
            this.AddToListButton.Size = new System.Drawing.Size(75, 23);
            this.AddToListButton.TabIndex = 7;
            this.AddToListButton.Text = "Add to list";
            this.AddToListButton.UseVisualStyleBackColor = true;
            this.AddToListButton.Click += new System.EventHandler(this.AddToListButton_Click);
            // 
            // RemoveFromTheListButton
            // 
            this.RemoveFromTheListButton.Location = new System.Drawing.Point(93, 397);
            this.RemoveFromTheListButton.Name = "RemoveFromTheListButton";
            this.RemoveFromTheListButton.Size = new System.Drawing.Size(112, 23);
            this.RemoveFromTheListButton.TabIndex = 8;
            this.RemoveFromTheListButton.Text = "Remove Selected";
            this.RemoveFromTheListButton.UseVisualStyleBackColor = true;
            this.RemoveFromTheListButton.Click += new System.EventHandler(this.RemoveFromTheListButton_Click);
            // 
            // ManageApplicationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 450);
            this.Controls.Add(this.RemoveFromTheListButton);
            this.Controls.Add(this.AddToListButton);
            this.Controls.Add(this.AppNameTextBox);
            this.Controls.Add(this.AddFromListButton);
            this.Controls.Add(this.RefreshCurrentlyRunningListButton);
            this.Controls.Add(this.CurrentlyRunningAppsList);
            this.Controls.Add(this.CurrentlyRunningAppsLabel);
            this.Controls.Add(this.ListeningAppsLabel);
            this.Controls.Add(this.ListeningAppsList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ManageApplicationsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Applications";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ListeningAppsList;
        private System.Windows.Forms.Label CurrentlyRunningAppsLabel;
        private System.Windows.Forms.Label ListeningAppsLabel;
        private System.Windows.Forms.ListBox CurrentlyRunningAppsList;
        private System.Windows.Forms.Button RefreshCurrentlyRunningListButton;
        private System.Windows.Forms.Button AddFromListButton;
        private System.Windows.Forms.TextBox AppNameTextBox;
        private System.Windows.Forms.Button AddToListButton;
        private System.Windows.Forms.Button RemoveFromTheListButton;
    }
}