﻿namespace CCMemory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.processTextBox = new System.Windows.Forms.TextBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.autoRefreshCheckBox = new System.Windows.Forms.CheckBox();
            this.processNameLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.monstersPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // processTextBox
            // 
            this.processTextBox.Location = new System.Drawing.Point(101, 12);
            this.processTextBox.Name = "processTextBox";
            this.processTextBox.Size = new System.Drawing.Size(66, 23);
            this.processTextBox.TabIndex = 0;
            this.processTextBox.Text = "otvdmw";
            // 
            // refreshButton
            // 
            this.refreshButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.refreshButton.Location = new System.Drawing.Point(173, 11);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 23);
            this.refreshButton.TabIndex = 2;
            this.refreshButton.Text = "&Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // propertyGrid
            // 
            this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.propertyGrid.Enabled = false;
            this.propertyGrid.HelpVisible = false;
            this.propertyGrid.Location = new System.Drawing.Point(12, 43);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propertyGrid.Size = new System.Drawing.Size(316, 260);
            this.propertyGrid.TabIndex = 3;
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGrid_PropertyValueChanged);
            // 
            // logTextBox
            // 
            this.logTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logTextBox.Font = new System.Drawing.Font("Consolas", 10F);
            this.logTextBox.Location = new System.Drawing.Point(12, 309);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logTextBox.Size = new System.Drawing.Size(631, 140);
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
            this.autoRefreshCheckBox.Location = new System.Drawing.Point(254, 13);
            this.autoRefreshCheckBox.Name = "autoRefreshCheckBox";
            this.autoRefreshCheckBox.Size = new System.Drawing.Size(99, 20);
            this.autoRefreshCheckBox.TabIndex = 5;
            this.autoRefreshCheckBox.Text = "Auto-refresh";
            this.autoRefreshCheckBox.UseVisualStyleBackColor = true;
            // 
            // processNameLabel
            // 
            this.processNameLabel.AutoSize = true;
            this.processNameLabel.Location = new System.Drawing.Point(12, 15);
            this.processNameLabel.Name = "processNameLabel";
            this.processNameLabel.Size = new System.Drawing.Size(83, 15);
            this.processNameLabel.TabIndex = 6;
            this.processNameLabel.Text = "Process name:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(470, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 23);
            this.textBox1.TabIndex = 7;
            this.textBox1.Leave += new System.EventHandler(this.TextBox1_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(349, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Monster list address:";
            // 
            // monstersPropertyGrid
            // 
            this.monstersPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.monstersPropertyGrid.HelpVisible = false;
            this.monstersPropertyGrid.Location = new System.Drawing.Point(334, 43);
            this.monstersPropertyGrid.Name = "monstersPropertyGrid";
            this.monstersPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.monstersPropertyGrid.Size = new System.Drawing.Size(309, 260);
            this.monstersPropertyGrid.TabIndex = 9;
            this.monstersPropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.MonstersPropertyGrid_PropertyValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(655, 461);
            this.Controls.Add(this.monstersPropertyGrid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.processNameLabel);
            this.Controls.Add(this.autoRefreshCheckBox);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.propertyGrid);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.processTextBox);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(427, 500);
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
        private System.Windows.Forms.Label processNameLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PropertyGrid monstersPropertyGrid;
    }
}

