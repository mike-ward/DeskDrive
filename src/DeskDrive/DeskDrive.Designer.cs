// Copyright (c) 2006 Blue Onion Software
// All rights reserved

namespace BlueOnion
{
    public partial class DeskDrive
    {
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Timer checkDrivesTimer;
        private System.Windows.Forms.Label drivesLabel;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox CDCheckBox;
        private System.Windows.Forms.CheckBox removableCheckBox;
        private System.Windows.Forms.CheckBox fixedCheckBox;
        private System.Windows.Forms.CheckBox networkedCheckBox;
        private System.Windows.Forms.CheckBox ramCheckBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.GroupBox groupBox1;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeskDrive));
            this.checkDrivesTimer = new System.Windows.Forms.Timer(this.components);
            this.drivesLabel = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CDCheckBox = new System.Windows.Forms.CheckBox();
            this.removableCheckBox = new System.Windows.Forms.CheckBox();
            this.fixedCheckBox = new System.Windows.Forms.CheckBox();
            this.networkedCheckBox = new System.Windows.Forms.CheckBox();
            this.ramCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.excludedTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.hideTrayCheckBox = new System.Windows.Forms.CheckBox();
            this.startupCheckBox = new System.Windows.Forms.CheckBox();
            this.minimizeAllCheckBox = new System.Windows.Forms.CheckBox();
            this.locusEffectCheckBox = new System.Windows.Forms.CheckBox();
            this.rememberIconPositionsCheckBox = new System.Windows.Forms.CheckBox();
            this.openExplorerCheckBox = new System.Windows.Forms.CheckBox();
            this.setWorkingSetSizeTimer = new System.Windows.Forms.Timer(this.components);
            this.hideButton = new System.Windows.Forms.Button();
            this.remindRemoveMediaCheckBox = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkDrivesTimer
            // 
            this.checkDrivesTimer.Enabled = true;
            this.checkDrivesTimer.Interval = 3000;
            this.checkDrivesTimer.Tick += new System.EventHandler(this.CheckDrivesTimerTick);
            // 
            // drivesLabel
            // 
            this.drivesLabel.AutoSize = true;
            this.drivesLabel.Location = new System.Drawing.Point(12, 21);
            this.drivesLabel.Name = "drivesLabel";
            this.drivesLabel.Size = new System.Drawing.Size(0, 13);
            this.drivesLabel.TabIndex = 0;
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Desk Drive";
            this.notifyIcon.Visible = true;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(126, 54);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showToolStripMenuItem.Image")));
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.showToolStripMenuItem.Text = "Settings...";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.ShowToolStripMenuItemClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(122, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripMenuItem.Image")));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(18, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 31);
            this.label1.TabIndex = 3;
            this.label1.Text = "Desk Drive";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Copyright 2015 Mike Ward";
            // 
            // CDCheckBox
            // 
            this.CDCheckBox.AutoSize = true;
            this.CDCheckBox.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.CDCheckBox.Location = new System.Drawing.Point(7, 24);
            this.CDCheckBox.Name = "CDCheckBox";
            this.CDCheckBox.Size = new System.Drawing.Size(69, 17);
            this.CDCheckBox.TabIndex = 7;
            this.CDCheckBox.Text = "CD ROM";
            this.CDCheckBox.UseVisualStyleBackColor = true;
            // 
            // removableCheckBox
            // 
            this.removableCheckBox.AutoSize = true;
            this.removableCheckBox.Location = new System.Drawing.Point(7, 47);
            this.removableCheckBox.Name = "removableCheckBox";
            this.removableCheckBox.Size = new System.Drawing.Size(80, 17);
            this.removableCheckBox.TabIndex = 8;
            this.removableCheckBox.Text = "Removable";
            this.removableCheckBox.UseVisualStyleBackColor = true;
            // 
            // fixedCheckBox
            // 
            this.fixedCheckBox.AutoSize = true;
            this.fixedCheckBox.Location = new System.Drawing.Point(7, 71);
            this.fixedCheckBox.Name = "fixedCheckBox";
            this.fixedCheckBox.Size = new System.Drawing.Size(51, 17);
            this.fixedCheckBox.TabIndex = 9;
            this.fixedCheckBox.Text = "Fixed";
            this.fixedCheckBox.UseVisualStyleBackColor = true;
            // 
            // networkedCheckBox
            // 
            this.networkedCheckBox.AutoSize = true;
            this.networkedCheckBox.Location = new System.Drawing.Point(7, 95);
            this.networkedCheckBox.Name = "networkedCheckBox";
            this.networkedCheckBox.Size = new System.Drawing.Size(78, 17);
            this.networkedCheckBox.TabIndex = 10;
            this.networkedCheckBox.Text = "Networked";
            this.networkedCheckBox.UseVisualStyleBackColor = true;
            // 
            // ramCheckBox
            // 
            this.ramCheckBox.AccessibleDescription = "";
            this.ramCheckBox.AutoSize = true;
            this.ramCheckBox.Location = new System.Drawing.Point(7, 119);
            this.ramCheckBox.Name = "ramCheckBox";
            this.ramCheckBox.Size = new System.Drawing.Size(50, 17);
            this.ramCheckBox.TabIndex = 11;
            this.ramCheckBox.Text = "RAM";
            this.ramCheckBox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Version 2.1.1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(238, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Automatic drive/media shortcuts for your desktop";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(21, 116);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(155, 13);
            this.linkLabel1.TabIndex = 14;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://mike-ward.net/deskdrive";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.removableCheckBox);
            this.groupBox1.Controls.Add(this.CDCheckBox);
            this.groupBox1.Controls.Add(this.fixedCheckBox);
            this.groupBox1.Controls.Add(this.networkedCheckBox);
            this.groupBox1.Controls.Add(this.ramCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(24, 150);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 146);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Enable";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(191, 333);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Exclude";
            // 
            // excludedTextBox
            // 
            this.excludedTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.excludedTextBox.Location = new System.Drawing.Point(191, 349);
            this.excludedTextBox.Name = "excludedTextBox";
            this.excludedTextBox.Size = new System.Drawing.Size(125, 20);
            this.excludedTextBox.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(191, 372);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "( example: C:\\, D:\\ )";
            // 
            // hideTrayCheckBox
            // 
            this.hideTrayCheckBox.AutoSize = true;
            this.hideTrayCheckBox.Location = new System.Drawing.Point(191, 159);
            this.hideTrayCheckBox.Name = "hideTrayCheckBox";
            this.hideTrayCheckBox.Size = new System.Drawing.Size(91, 17);
            this.hideTrayCheckBox.TabIndex = 19;
            this.hideTrayCheckBox.Text = "Hide tray icon";
            this.hideTrayCheckBox.UseVisualStyleBackColor = true;
            this.hideTrayCheckBox.CheckedChanged += new System.EventHandler(this.HideTrayCheckBoxCheckedChanged);
            // 
            // startupCheckBox
            // 
            this.startupCheckBox.AutoSize = true;
            this.startupCheckBox.Location = new System.Drawing.Point(191, 182);
            this.startupCheckBox.Name = "startupCheckBox";
            this.startupCheckBox.Size = new System.Drawing.Size(96, 17);
            this.startupCheckBox.TabIndex = 20;
            this.startupCheckBox.Text = "Run on startup";
            this.startupCheckBox.UseVisualStyleBackColor = true;
            // 
            // minimizeAllCheckBox
            // 
            this.minimizeAllCheckBox.AutoSize = true;
            this.minimizeAllCheckBox.Location = new System.Drawing.Point(191, 205);
            this.minimizeAllCheckBox.Name = "minimizeAllCheckBox";
            this.minimizeAllCheckBox.Size = new System.Drawing.Size(123, 17);
            this.minimizeAllCheckBox.TabIndex = 21;
            this.minimizeAllCheckBox.Text = "Minimize all windows";
            this.minimizeAllCheckBox.UseVisualStyleBackColor = true;
            // 
            // locusEffectCheckBox
            // 
            this.locusEffectCheckBox.AutoSize = true;
            this.locusEffectCheckBox.Location = new System.Drawing.Point(191, 228);
            this.locusEffectCheckBox.Name = "locusEffectCheckBox";
            this.locusEffectCheckBox.Size = new System.Drawing.Size(130, 17);
            this.locusEffectCheckBox.TabIndex = 22;
            this.locusEffectCheckBox.Text = "Show positional effect";
            this.locusEffectCheckBox.UseVisualStyleBackColor = true;
            // 
            // rememberIconPositionsCheckBox
            // 
            this.rememberIconPositionsCheckBox.AutoSize = true;
            this.rememberIconPositionsCheckBox.Location = new System.Drawing.Point(191, 251);
            this.rememberIconPositionsCheckBox.Name = "rememberIconPositionsCheckBox";
            this.rememberIconPositionsCheckBox.Size = new System.Drawing.Size(144, 17);
            this.rememberIconPositionsCheckBox.TabIndex = 23;
            this.rememberIconPositionsCheckBox.Text = "Remember icon positions";
            this.rememberIconPositionsCheckBox.UseVisualStyleBackColor = true;
            // 
            // openExplorerCheckBox
            // 
            this.openExplorerCheckBox.AutoSize = true;
            this.openExplorerCheckBox.Location = new System.Drawing.Point(191, 276);
            this.openExplorerCheckBox.Name = "openExplorerCheckBox";
            this.openExplorerCheckBox.Size = new System.Drawing.Size(140, 17);
            this.openExplorerCheckBox.TabIndex = 26;
            this.openExplorerCheckBox.Text = "Open Windows Explorer";
            this.openExplorerCheckBox.UseVisualStyleBackColor = true;
            // 
            // setWorkingSetSizeTimer
            // 
            this.setWorkingSetSizeTimer.Enabled = true;
            this.setWorkingSetSizeTimer.Interval = 60000;
            // 
            // hideButton
            // 
            this.hideButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.hideButton.Location = new System.Drawing.Point(332, 405);
            this.hideButton.Name = "hideButton";
            this.hideButton.Size = new System.Drawing.Size(75, 23);
            this.hideButton.TabIndex = 27;
            this.hideButton.Text = "OK";
            this.hideButton.UseVisualStyleBackColor = true;
            this.hideButton.Click += new System.EventHandler(this.HideButtonClick);
            // 
            // remindRemoveMediaCheckBox
            // 
            this.remindRemoveMediaCheckBox.AutoSize = true;
            this.remindRemoveMediaCheckBox.Location = new System.Drawing.Point(191, 299);
            this.remindRemoveMediaCheckBox.Name = "remindRemoveMediaCheckBox";
            this.remindRemoveMediaCheckBox.Size = new System.Drawing.Size(160, 17);
            this.remindRemoveMediaCheckBox.TabIndex = 28;
            this.remindRemoveMediaCheckBox.Text = "Remind me to remove media";
            this.remindRemoveMediaCheckBox.UseVisualStyleBackColor = true;
            // 
            // DeskDrive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 441);
            this.Controls.Add(this.remindRemoveMediaCheckBox);
            this.Controls.Add(this.hideButton);
            this.Controls.Add(this.openExplorerCheckBox);
            this.Controls.Add(this.rememberIconPositionsCheckBox);
            this.Controls.Add(this.locusEffectCheckBox);
            this.Controls.Add(this.minimizeAllCheckBox);
            this.Controls.Add(this.startupCheckBox);
            this.Controls.Add(this.hideTrayCheckBox);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.excludedTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.drivesLabel);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(10000, 10000);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeskDrive";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Desk Drive";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DeskDriveFormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DeskDriveFormClosed);
            this.contextMenuStrip.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox excludedTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox hideTrayCheckBox;
        private System.Windows.Forms.CheckBox startupCheckBox;
        private System.Windows.Forms.CheckBox minimizeAllCheckBox;
        private System.Windows.Forms.CheckBox locusEffectCheckBox;
        private System.Windows.Forms.CheckBox rememberIconPositionsCheckBox;
        private System.Windows.Forms.CheckBox openExplorerCheckBox;
        private System.Windows.Forms.Timer setWorkingSetSizeTimer;
        private System.Windows.Forms.Button hideButton;
        private System.Windows.Forms.CheckBox remindRemoveMediaCheckBox;
    }
}

