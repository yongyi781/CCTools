namespace CCMemory
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.processTextBox = new System.Windows.Forms.TextBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.autoRefreshCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // processTextBox
            // 
            this.processTextBox.Location = new System.Drawing.Point(14, 14);
            this.processTextBox.Name = "processTextBox";
            this.processTextBox.Size = new System.Drawing.Size(116, 23);
            this.processTextBox.TabIndex = 0;
            this.processTextBox.Text = "otvdmw";
            // 
            // refreshButton
            // 
            this.refreshButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.refreshButton.Location = new System.Drawing.Point(136, 13);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 23);
            this.refreshButton.TabIndex = 2;
            this.refreshButton.Text = "&Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // propertyGrid
            // 
            this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid.Location = new System.Drawing.Point(12, 43);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propertyGrid.Size = new System.Drawing.Size(459, 517);
            this.propertyGrid.TabIndex = 3;
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGrid_PropertyValueChanged);
            // 
            // logTextBox
            // 
            this.logTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logTextBox.Font = new System.Drawing.Font("Consolas", 10F);
            this.logTextBox.Location = new System.Drawing.Point(12, 566);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logTextBox.Size = new System.Drawing.Size(459, 140);
            this.logTextBox.TabIndex = 4;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 125;
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // autoRefreshCheckBox
            // 
            this.autoRefreshCheckBox.AutoSize = true;
            this.autoRefreshCheckBox.Checked = true;
            this.autoRefreshCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoRefreshCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.autoRefreshCheckBox.Location = new System.Drawing.Point(217, 15);
            this.autoRefreshCheckBox.Name = "autoRefreshCheckBox";
            this.autoRefreshCheckBox.Size = new System.Drawing.Size(99, 20);
            this.autoRefreshCheckBox.TabIndex = 5;
            this.autoRefreshCheckBox.Text = "Auto-refresh";
            this.autoRefreshCheckBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 718);
            this.Controls.Add(this.autoRefreshCheckBox);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.propertyGrid);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.processTextBox);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "Form1";
            this.Text = "Chips Memory Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox processTextBox;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.CheckBox autoRefreshCheckBox;
    }
}

