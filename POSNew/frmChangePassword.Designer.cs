namespace POSNew
{
    partial class frmChangePassword
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
            this.panelDashbaord = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtConfirmNewPassword = new MetroFramework.Controls.MetroTextBox();
            this.txtNewPassword = new MetroFramework.Controls.MetroTextBox();
            this.txtOldPassword = new MetroFramework.Controls.MetroTextBox();
            this.panelDashbaord.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDashbaord
            // 
            this.panelDashbaord.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.panelDashbaord.Controls.Add(this.label1);
            this.panelDashbaord.Controls.Add(this.button10);
            this.panelDashbaord.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDashbaord.Location = new System.Drawing.Point(0, 0);
            this.panelDashbaord.Name = "panelDashbaord";
            this.panelDashbaord.Size = new System.Drawing.Size(399, 50);
            this.panelDashbaord.TabIndex = 2;
            this.panelDashbaord.Paint += new System.Windows.Forms.PaintEventHandler(this.panelDashbaord_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(28, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "CHANGE PASSWORD";
            // 
            // button10
            // 
            this.button10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button10.FlatAppearance.BorderSize = 0;
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button10.ForeColor = System.Drawing.Color.Red;
            this.button10.Image = global::POSNew.Properties.Resources.cancel_24;
            this.button10.Location = new System.Drawing.Point(340, 3);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(55, 40);
            this.button10.TabIndex = 0;
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(33, 237);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(338, 40);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtConfirmNewPassword
            // 
            // 
            // 
            // 
            this.txtConfirmNewPassword.CustomButton.Image = null;
            this.txtConfirmNewPassword.CustomButton.Location = new System.Drawing.Point(314, 2);
            this.txtConfirmNewPassword.CustomButton.Name = "";
            this.txtConfirmNewPassword.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtConfirmNewPassword.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtConfirmNewPassword.CustomButton.TabIndex = 1;
            this.txtConfirmNewPassword.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtConfirmNewPassword.CustomButton.UseSelectable = true;
            this.txtConfirmNewPassword.CustomButton.Visible = false;
            this.txtConfirmNewPassword.DisplayIcon = true;
            this.txtConfirmNewPassword.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txtConfirmNewPassword.Icon = global::POSNew.Properties.Resources.lock1;
            this.txtConfirmNewPassword.Lines = new string[0];
            this.txtConfirmNewPassword.Location = new System.Drawing.Point(33, 181);
            this.txtConfirmNewPassword.MaxLength = 32767;
            this.txtConfirmNewPassword.Name = "txtConfirmNewPassword";
            this.txtConfirmNewPassword.PasswordChar = '●';
            this.txtConfirmNewPassword.PromptText = "Confirm New  Password";
            this.txtConfirmNewPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtConfirmNewPassword.SelectedText = "";
            this.txtConfirmNewPassword.SelectionLength = 0;
            this.txtConfirmNewPassword.SelectionStart = 0;
            this.txtConfirmNewPassword.ShortcutsEnabled = true;
            this.txtConfirmNewPassword.Size = new System.Drawing.Size(338, 26);
            this.txtConfirmNewPassword.TabIndex = 3;
            this.txtConfirmNewPassword.UseSelectable = true;
            this.txtConfirmNewPassword.UseSystemPasswordChar = true;
            this.txtConfirmNewPassword.WaterMark = "Confirm New  Password";
            this.txtConfirmNewPassword.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtConfirmNewPassword.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtNewPassword
            // 
            // 
            // 
            // 
            this.txtNewPassword.CustomButton.Image = null;
            this.txtNewPassword.CustomButton.Location = new System.Drawing.Point(314, 2);
            this.txtNewPassword.CustomButton.Name = "";
            this.txtNewPassword.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtNewPassword.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtNewPassword.CustomButton.TabIndex = 1;
            this.txtNewPassword.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtNewPassword.CustomButton.UseSelectable = true;
            this.txtNewPassword.CustomButton.Visible = false;
            this.txtNewPassword.DisplayIcon = true;
            this.txtNewPassword.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txtNewPassword.Icon = global::POSNew.Properties.Resources.lock1;
            this.txtNewPassword.Lines = new string[0];
            this.txtNewPassword.Location = new System.Drawing.Point(33, 134);
            this.txtNewPassword.MaxLength = 32767;
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '●';
            this.txtNewPassword.PromptText = "New Password";
            this.txtNewPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNewPassword.SelectedText = "";
            this.txtNewPassword.SelectionLength = 0;
            this.txtNewPassword.SelectionStart = 0;
            this.txtNewPassword.ShortcutsEnabled = true;
            this.txtNewPassword.Size = new System.Drawing.Size(338, 26);
            this.txtNewPassword.TabIndex = 2;
            this.txtNewPassword.UseSelectable = true;
            this.txtNewPassword.UseSystemPasswordChar = true;
            this.txtNewPassword.WaterMark = "New Password";
            this.txtNewPassword.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtNewPassword.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtOldPassword
            // 
            // 
            // 
            // 
            this.txtOldPassword.CustomButton.Image = null;
            this.txtOldPassword.CustomButton.Location = new System.Drawing.Point(314, 2);
            this.txtOldPassword.CustomButton.Name = "";
            this.txtOldPassword.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtOldPassword.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtOldPassword.CustomButton.TabIndex = 1;
            this.txtOldPassword.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtOldPassword.CustomButton.UseSelectable = true;
            this.txtOldPassword.CustomButton.Visible = false;
            this.txtOldPassword.DisplayIcon = true;
            this.txtOldPassword.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txtOldPassword.Icon = global::POSNew.Properties.Resources.lock1;
            this.txtOldPassword.Lines = new string[0];
            this.txtOldPassword.Location = new System.Drawing.Point(33, 91);
            this.txtOldPassword.MaxLength = 32767;
            this.txtOldPassword.Name = "txtOldPassword";
            this.txtOldPassword.PasswordChar = '●';
            this.txtOldPassword.PromptText = "Old Password";
            this.txtOldPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtOldPassword.SelectedText = "";
            this.txtOldPassword.SelectionLength = 0;
            this.txtOldPassword.SelectionStart = 0;
            this.txtOldPassword.ShortcutsEnabled = true;
            this.txtOldPassword.Size = new System.Drawing.Size(338, 26);
            this.txtOldPassword.TabIndex = 1;
            this.txtOldPassword.UseSelectable = true;
            this.txtOldPassword.UseSystemPasswordChar = true;
            this.txtOldPassword.WaterMark = "Old Password";
            this.txtOldPassword.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtOldPassword.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // frmChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 295);
            this.ControlBox = false;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtConfirmNewPassword);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.txtOldPassword);
            this.Controls.Add(this.panelDashbaord);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmChangePassword_KeyDown);
            this.panelDashbaord.ResumeLayout(false);
            this.panelDashbaord.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDashbaord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button10;
        public System.Windows.Forms.Button btnSave;
        private MetroFramework.Controls.MetroTextBox txtOldPassword;
        private MetroFramework.Controls.MetroTextBox txtNewPassword;
        private MetroFramework.Controls.MetroTextBox txtConfirmNewPassword;
    }
}