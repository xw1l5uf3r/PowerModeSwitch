namespace PowerModeSwitch
{
    partial class SettingsForm
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
            languageComboBox = new ComboBox();
            languageLabel = new Label();
            tabControl1 = new TabControl();
            appTabPage = new TabPage();
            mode2ComboBox = new ComboBox();
            mode2label = new Label();
            mode1ComboBox = new ComboBox();
            mode1label = new Label();
            switchKeyLabel = new Label();
            switchKeyTextBox = new TextBox();
            applyButton = new Button();
            systemTabPage = new TabPage();
            autorunCheckBox = new CheckBox();
            aboutTabPage = new TabPage();
            linkLabel1 = new LinkLabel();
            label1 = new Label();
            versionLabel = new Label();
            tabControl1.SuspendLayout();
            appTabPage.SuspendLayout();
            systemTabPage.SuspendLayout();
            aboutTabPage.SuspendLayout();
            SuspendLayout();
            // 
            // languageComboBox
            // 
            languageComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            languageComboBox.Font = new Font("Segoe UI", 11F);
            languageComboBox.FormattingEnabled = true;
            languageComboBox.Location = new Point(6, 26);
            languageComboBox.Name = "languageComboBox";
            languageComboBox.Size = new Size(170, 28);
            languageComboBox.TabIndex = 0;
            // 
            // languageLabel
            // 
            languageLabel.Font = new Font("Segoe UI", 11F);
            languageLabel.Location = new Point(6, 3);
            languageLabel.Name = "languageLabel";
            languageLabel.Size = new Size(170, 20);
            languageLabel.TabIndex = 1;
            languageLabel.Text = "Language";
            languageLabel.TextAlign = ContentAlignment.MiddleCenter;
            languageLabel.UseMnemonic = false;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(appTabPage);
            tabControl1.Controls.Add(systemTabPage);
            tabControl1.Controls.Add(aboutTabPage);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Multiline = true;
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(385, 457);
            tabControl1.TabIndex = 3;
            // 
            // appTabPage
            // 
            appTabPage.Controls.Add(mode2ComboBox);
            appTabPage.Controls.Add(mode2label);
            appTabPage.Controls.Add(mode1ComboBox);
            appTabPage.Controls.Add(mode1label);
            appTabPage.Controls.Add(switchKeyLabel);
            appTabPage.Controls.Add(switchKeyTextBox);
            appTabPage.Controls.Add(applyButton);
            appTabPage.Controls.Add(languageLabel);
            appTabPage.Controls.Add(languageComboBox);
            appTabPage.Location = new Point(4, 24);
            appTabPage.Name = "appTabPage";
            appTabPage.Padding = new Padding(3);
            appTabPage.Size = new Size(377, 429);
            appTabPage.TabIndex = 0;
            appTabPage.Text = "Application";
            appTabPage.UseVisualStyleBackColor = true;
            // 
            // mode2ComboBox
            // 
            mode2ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            mode2ComboBox.Font = new Font("Segoe UI", 11F);
            mode2ComboBox.FormattingEnabled = true;
            mode2ComboBox.Items.AddRange(new object[] { "High performance", "Energy saving", "Balanced" });
            mode2ComboBox.Location = new Point(6, 196);
            mode2ComboBox.Name = "mode2ComboBox";
            mode2ComboBox.Size = new Size(170, 28);
            mode2ComboBox.TabIndex = 8;
            mode2ComboBox.SelectedIndexChanged += mode2ComboBox_SelectedIndexChanged;
            // 
            // mode2label
            // 
            mode2label.Font = new Font("Segoe UI", 11F);
            mode2label.Location = new Point(8, 170);
            mode2label.Name = "mode2label";
            mode2label.Size = new Size(168, 23);
            mode2label.TabIndex = 7;
            mode2label.Text = "Mode 2";
            mode2label.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // mode1ComboBox
            // 
            mode1ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            mode1ComboBox.Font = new Font("Segoe UI", 11F);
            mode1ComboBox.FormattingEnabled = true;
            mode1ComboBox.Items.AddRange(new object[] { "High performance", "Energy saving", "Balanced" });
            mode1ComboBox.Location = new Point(6, 139);
            mode1ComboBox.Name = "mode1ComboBox";
            mode1ComboBox.Size = new Size(170, 28);
            mode1ComboBox.TabIndex = 6;
            mode1ComboBox.SelectedIndexChanged += mode1ComboBox_SelectedIndexChanged;
            // 
            // mode1label
            // 
            mode1label.Font = new Font("Segoe UI", 11F);
            mode1label.Location = new Point(6, 113);
            mode1label.Name = "mode1label";
            mode1label.Size = new Size(170, 23);
            mode1label.TabIndex = 5;
            mode1label.Text = "Mode 1";
            mode1label.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // switchKeyLabel
            // 
            switchKeyLabel.Font = new Font("Segoe UI", 11F);
            switchKeyLabel.Location = new Point(8, 57);
            switchKeyLabel.Name = "switchKeyLabel";
            switchKeyLabel.Size = new Size(168, 23);
            switchKeyLabel.TabIndex = 4;
            switchKeyLabel.Text = "Switch Key";
            switchKeyLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // switchKeyTextBox
            // 
            switchKeyTextBox.Font = new Font("Segoe UI", 11F);
            switchKeyTextBox.Location = new Point(6, 83);
            switchKeyTextBox.Name = "switchKeyTextBox";
            switchKeyTextBox.ReadOnly = true;
            switchKeyTextBox.Size = new Size(170, 27);
            switchKeyTextBox.TabIndex = 3;
            // 
            // applyButton
            // 
            applyButton.Dock = DockStyle.Bottom;
            applyButton.Font = new Font("Segoe UI", 11F);
            applyButton.Location = new Point(3, 385);
            applyButton.Name = "applyButton";
            applyButton.Size = new Size(371, 41);
            applyButton.TabIndex = 2;
            applyButton.Text = "Apply";
            applyButton.UseVisualStyleBackColor = true;
            applyButton.Click += applyButton_Click;
            // 
            // systemTabPage
            // 
            systemTabPage.Controls.Add(autorunCheckBox);
            systemTabPage.Location = new Point(4, 24);
            systemTabPage.Name = "systemTabPage";
            systemTabPage.Padding = new Padding(3);
            systemTabPage.Size = new Size(377, 429);
            systemTabPage.TabIndex = 1;
            systemTabPage.Text = "System";
            systemTabPage.UseVisualStyleBackColor = true;
            // 
            // autorunCheckBox
            // 
            autorunCheckBox.AutoSize = true;
            autorunCheckBox.Location = new Point(8, 6);
            autorunCheckBox.Name = "autorunCheckBox";
            autorunCheckBox.Size = new Size(70, 19);
            autorunCheckBox.TabIndex = 3;
            autorunCheckBox.Text = "Autorun";
            autorunCheckBox.UseVisualStyleBackColor = true;
            // 
            // aboutTabPage
            // 
            aboutTabPage.Controls.Add(versionLabel);
            aboutTabPage.Controls.Add(label1);
            aboutTabPage.Controls.Add(linkLabel1);
            aboutTabPage.Location = new Point(4, 24);
            aboutTabPage.Name = "aboutTabPage";
            aboutTabPage.Size = new Size(377, 429);
            aboutTabPage.TabIndex = 2;
            aboutTabPage.Text = "About";
            aboutTabPage.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("Segoe UI", 11F);
            linkLabel1.Location = new Point(8, 9);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(202, 20);
            linkLabel1.TabIndex = 0;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "https://github.com/xw1l5uf3r";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.Location = new Point(8, 29);
            label1.Name = "label1";
            label1.Size = new Size(60, 20);
            label1.TabIndex = 1;
            label1.Text = "Version:";
            // 
            // versionLabel
            // 
            versionLabel.AutoSize = true;
            versionLabel.Font = new Font("Segoe UI", 11F);
            versionLabel.Location = new Point(74, 29);
            versionLabel.Name = "versionLabel";
            versionLabel.Size = new Size(39, 20);
            versionLabel.TabIndex = 2;
            versionLabel.Text = "1.0.0";
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(380, 457);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            Name = "SettingsForm";
            ShowIcon = false;
            Text = "Settings";
            tabControl1.ResumeLayout(false);
            appTabPage.ResumeLayout(false);
            appTabPage.PerformLayout();
            systemTabPage.ResumeLayout(false);
            systemTabPage.PerformLayout();
            aboutTabPage.ResumeLayout(false);
            aboutTabPage.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBox1;
        internal ComboBox languageComboBox;
        private Label languageLabel;
        private TabControl tabControl1;
        private TabPage appTabPage;
        private TabPage systemTabPage;
        private CheckBox autorunCheckBox;
        private TabPage aboutTabPage;
        private Button applyButton;
        private Label switchKeyLabel;
        private TextBox switchKeyTextBox;
        private Label mode1label;
        private Label mode2label;
        private ComboBox mode1ComboBox;
        private ComboBox mode2ComboBox;
        private LinkLabel linkLabel1;
        private Label versionLabel;
        private Label label1;
    }
}