namespace Vitros350
{
    partial class frmMain
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
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkLisPro = new System.Windows.Forms.CheckBox();
            this.chkEnableTwoWayProcess = new System.Windows.Forms.CheckBox();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtServerIp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtMachineLocation = new System.Windows.Forms.TextBox();
            this.txtMachineSection = new System.Windows.Forms.TextBox();
            this.txtMachineSerialNo = new System.Windows.Forms.TextBox();
            this.txtMachineName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControlDatabase = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtDatabasePort = new System.Windows.Forms.TextBox();
            this.txtDatabaseName = new System.Windows.Forms.TextBox();
            this.txtDatabasePass = new System.Windows.Forms.TextBox();
            this.txtDatabaseUsername = new System.Windows.Forms.TextBox();
            this.txtDatabaseServer = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControlLog = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lblVersion = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.cboComPort = new System.Windows.Forms.ComboBox();
            this.tabControlSettings.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControlDatabase.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabControlLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControlSettings.Controls.Add(this.tabPage1);
            this.tabControlSettings.Controls.Add(this.tabPage2);
            this.tabControlSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlSettings.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlSettings.Location = new System.Drawing.Point(0, 0);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.Padding = new System.Drawing.Point(10, 3);
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.Size = new System.Drawing.Size(409, 214);
            this.tabControlSettings.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage1.Controls.Add(this.chkLisPro);
            this.tabPage1.Controls.Add(this.chkEnableTwoWayProcess);
            this.tabPage1.Controls.Add(this.txtServerPort);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtServerIp);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 31);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(401, 179);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Server Settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkLisPro
            // 
            this.chkLisPro.AutoSize = true;
            this.chkLisPro.Font = new System.Drawing.Font("Arial", 10F);
            this.chkLisPro.Location = new System.Drawing.Point(136, 128);
            this.chkLisPro.Name = "chkLisPro";
            this.chkLisPro.Size = new System.Drawing.Size(72, 20);
            this.chkLisPro.TabIndex = 6;
            this.chkLisPro.Text = "LIS Pro";
            this.chkLisPro.UseVisualStyleBackColor = true;
            // 
            // chkEnableTwoWayProcess
            // 
            this.chkEnableTwoWayProcess.AutoSize = true;
            this.chkEnableTwoWayProcess.Font = new System.Drawing.Font("Arial", 10F);
            this.chkEnableTwoWayProcess.Location = new System.Drawing.Point(136, 99);
            this.chkEnableTwoWayProcess.Name = "chkEnableTwoWayProcess";
            this.chkEnableTwoWayProcess.Size = new System.Drawing.Size(185, 20);
            this.chkEnableTwoWayProcess.TabIndex = 5;
            this.chkEnableTwoWayProcess.Text = "Enable Two Way Process";
            this.chkEnableTwoWayProcess.UseVisualStyleBackColor = true;
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(136, 55);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(225, 27);
            this.txtServerPort.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10F);
            this.label2.Location = new System.Drawing.Point(6, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Server Port:";
            // 
            // txtServerIp
            // 
            this.txtServerIp.Location = new System.Drawing.Point(136, 14);
            this.txtServerIp.Name = "txtServerIp";
            this.txtServerIp.Size = new System.Drawing.Size(225, 27);
            this.txtServerIp.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F);
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server IP Address:";
            // 
            // tabPage2
            // 
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage2.Controls.Add(this.cboComPort);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.txtMachineLocation);
            this.tabPage2.Controls.Add(this.txtMachineSection);
            this.tabPage2.Controls.Add(this.txtMachineSerialNo);
            this.tabPage2.Controls.Add(this.txtMachineName);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Location = new System.Drawing.Point(4, 31);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(401, 179);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Machine Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtMachineLocation
            // 
            this.txtMachineLocation.Location = new System.Drawing.Point(139, 136);
            this.txtMachineLocation.Name = "txtMachineLocation";
            this.txtMachineLocation.Size = new System.Drawing.Size(249, 27);
            this.txtMachineLocation.TabIndex = 21;
            // 
            // txtMachineSection
            // 
            this.txtMachineSection.Location = new System.Drawing.Point(139, 105);
            this.txtMachineSection.Name = "txtMachineSection";
            this.txtMachineSection.Size = new System.Drawing.Size(249, 27);
            this.txtMachineSection.TabIndex = 20;
            // 
            // txtMachineSerialNo
            // 
            this.txtMachineSerialNo.Location = new System.Drawing.Point(139, 73);
            this.txtMachineSerialNo.Name = "txtMachineSerialNo";
            this.txtMachineSerialNo.Size = new System.Drawing.Size(249, 27);
            this.txtMachineSerialNo.TabIndex = 19;
            // 
            // txtMachineName
            // 
            this.txtMachineName.Location = new System.Drawing.Point(139, 10);
            this.txtMachineName.Name = "txtMachineName";
            this.txtMachineName.Size = new System.Drawing.Size(249, 27);
            this.txtMachineName.TabIndex = 18;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 10F);
            this.label10.Location = new System.Drawing.Point(6, 141);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 16);
            this.label10.TabIndex = 17;
            this.label10.Text = "Location:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 10F);
            this.label11.Location = new System.Drawing.Point(6, 106);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 16);
            this.label11.TabIndex = 16;
            this.label11.Text = "Section:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 10F);
            this.label12.Location = new System.Drawing.Point(6, 73);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 16);
            this.label12.TabIndex = 15;
            this.label12.Text = "Serial No:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial", 10F);
            this.label13.Location = new System.Drawing.Point(6, 15);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(104, 16);
            this.label13.TabIndex = 14;
            this.label13.Text = "Machine Name:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tabControlSettings);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(411, 216);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tabControlDatabase);
            this.panel2.Location = new System.Drawing.Point(429, 13);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(419, 215);
            this.panel2.TabIndex = 2;
            // 
            // tabControlDatabase
            // 
            this.tabControlDatabase.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControlDatabase.Controls.Add(this.tabPage3);
            this.tabControlDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlDatabase.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlDatabase.Location = new System.Drawing.Point(0, 0);
            this.tabControlDatabase.Name = "tabControlDatabase";
            this.tabControlDatabase.Padding = new System.Drawing.Point(10, 3);
            this.tabControlDatabase.SelectedIndex = 0;
            this.tabControlDatabase.Size = new System.Drawing.Size(417, 213);
            this.tabControlDatabase.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage3.Controls.Add(this.txtDatabasePort);
            this.tabPage3.Controls.Add(this.txtDatabaseName);
            this.tabPage3.Controls.Add(this.txtDatabasePass);
            this.tabPage3.Controls.Add(this.txtDatabaseUsername);
            this.tabPage3.Controls.Add(this.txtDatabaseServer);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Location = new System.Drawing.Point(4, 31);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(409, 178);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Database Settings";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtDatabasePort
            // 
            this.txtDatabasePort.Location = new System.Drawing.Point(152, 103);
            this.txtDatabasePort.Name = "txtDatabasePort";
            this.txtDatabasePort.Size = new System.Drawing.Size(249, 27);
            this.txtDatabasePort.TabIndex = 12;
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.Location = new System.Drawing.Point(152, 135);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new System.Drawing.Size(249, 27);
            this.txtDatabaseName.TabIndex = 13;
            // 
            // txtDatabasePass
            // 
            this.txtDatabasePass.Location = new System.Drawing.Point(152, 72);
            this.txtDatabasePass.Name = "txtDatabasePass";
            this.txtDatabasePass.PasswordChar = '*';
            this.txtDatabasePass.Size = new System.Drawing.Size(249, 27);
            this.txtDatabasePass.TabIndex = 11;
            // 
            // txtDatabaseUsername
            // 
            this.txtDatabaseUsername.Location = new System.Drawing.Point(152, 40);
            this.txtDatabaseUsername.Name = "txtDatabaseUsername";
            this.txtDatabaseUsername.Size = new System.Drawing.Size(249, 27);
            this.txtDatabaseUsername.TabIndex = 10;
            // 
            // txtDatabaseServer
            // 
            this.txtDatabaseServer.Location = new System.Drawing.Point(152, 8);
            this.txtDatabaseServer.Name = "txtDatabaseServer";
            this.txtDatabaseServer.Size = new System.Drawing.Size(249, 27);
            this.txtDatabaseServer.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 10F);
            this.label7.Location = new System.Drawing.Point(19, 140);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 16);
            this.label7.TabIndex = 8;
            this.label7.Text = "Database Name:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 10F);
            this.label6.Location = new System.Drawing.Point(19, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "Database Port:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 10F);
            this.label5.Location = new System.Drawing.Point(19, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "Password:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 10F);
            this.label4.Location = new System.Drawing.Point(19, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Username:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 10F);
            this.label3.Location = new System.Drawing.Point(19, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Database Server:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.tabControlLog);
            this.panel3.Location = new System.Drawing.Point(13, 234);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(830, 313);
            this.panel3.TabIndex = 3;
            // 
            // tabControlLog
            // 
            this.tabControlLog.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControlLog.Controls.Add(this.tabPage4);
            this.tabControlLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlLog.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlLog.Location = new System.Drawing.Point(0, 0);
            this.tabControlLog.Name = "tabControlLog";
            this.tabControlLog.Padding = new System.Drawing.Point(10, 3);
            this.tabControlLog.SelectedIndex = 0;
            this.tabControlLog.Size = new System.Drawing.Size(828, 311);
            this.tabControlLog.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage4.Location = new System.Drawing.Point(4, 31);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(820, 276);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Communication Log";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(9, 561);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(69, 14);
            this.lblVersion.TabIndex = 4;
            this.lblVersion.Text = "version no:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(9, 583);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(184, 16);
            this.label9.TabIndex = 5;
            this.label9.Text = "Connection Status: Connected";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(327, 576);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(408, 576);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 7;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(605, 576);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(75, 23);
            this.btnClearLog.TabIndex = 8;
            this.btnClearLog.Text = "Clear Logs";
            this.btnClearLog.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(686, 576);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 9;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(767, 576);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 10F);
            this.label8.Location = new System.Drawing.Point(6, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 16);
            this.label8.TabIndex = 22;
            this.label8.Text = "Machine Port:";
            // 
            // cboComPort
            // 
            this.cboComPort.FormattingEnabled = true;
            this.cboComPort.Location = new System.Drawing.Point(139, 41);
            this.cboComPort.Name = "cboComPort";
            this.cboComPort.Size = new System.Drawing.Size(249, 27);
            this.cboComPort.TabIndex = 23;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 606);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vitros 350";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.tabControlSettings.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabControlDatabase.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tabControlLog.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControlDatabase;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TabControl tabControlLog;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox txtServerIp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkLisPro;
        private System.Windows.Forms.CheckBox chkEnableTwoWayProcess;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDatabasePort;
        private System.Windows.Forms.TextBox txtDatabaseName;
        private System.Windows.Forms.TextBox txtDatabasePass;
        private System.Windows.Forms.TextBox txtDatabaseUsername;
        private System.Windows.Forms.TextBox txtDatabaseServer;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.TextBox txtMachineLocation;
        private System.Windows.Forms.TextBox txtMachineSection;
        private System.Windows.Forms.TextBox txtMachineSerialNo;
        private System.Windows.Forms.TextBox txtMachineName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboComPort;
    }
}

