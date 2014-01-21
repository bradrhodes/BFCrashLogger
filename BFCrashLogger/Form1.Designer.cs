namespace BFCrashLogger
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.reportCrash = new System.Windows.Forms.Button();
            this.viewData = new System.Windows.Forms.Button();
            this.sendData = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            this.sendDataOnExit = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(13, 13);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(301, 225);
            this.listBox1.TabIndex = 0;
            // 
            // reportCrash
            // 
            this.reportCrash.Location = new System.Drawing.Point(13, 244);
            this.reportCrash.Name = "reportCrash";
            this.reportCrash.Size = new System.Drawing.Size(133, 23);
            this.reportCrash.TabIndex = 1;
            this.reportCrash.Text = "Report Unhandled Crash";
            this.reportCrash.UseVisualStyleBackColor = true;
            this.reportCrash.Click += new System.EventHandler(this.reportCrash_Click);
            // 
            // viewData
            // 
            this.viewData.Location = new System.Drawing.Point(13, 273);
            this.viewData.Name = "viewData";
            this.viewData.Size = new System.Drawing.Size(133, 23);
            this.viewData.TabIndex = 2;
            this.viewData.Text = "View Collected Data";
            this.viewData.UseVisualStyleBackColor = true;
            this.viewData.Click += new System.EventHandler(this.viewData_Click);
            // 
            // sendData
            // 
            this.sendData.Location = new System.Drawing.Point(181, 244);
            this.sendData.Name = "sendData";
            this.sendData.Size = new System.Drawing.Size(133, 23);
            this.sendData.TabIndex = 3;
            this.sendData.Text = "Send Data Now";
            this.sendData.UseVisualStyleBackColor = true;
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(181, 273);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(133, 23);
            this.exit.TabIndex = 4;
            this.exit.Text = "Exit";
            this.exit.UseVisualStyleBackColor = true;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // sendDataOnExit
            // 
            this.sendDataOnExit.AutoSize = true;
            this.sendDataOnExit.Checked = true;
            this.sendDataOnExit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sendDataOnExit.Location = new System.Drawing.Point(202, 302);
            this.sendDataOnExit.Name = "sendDataOnExit";
            this.sendDataOnExit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.sendDataOnExit.Size = new System.Drawing.Size(112, 17);
            this.sendDataOnExit.TabIndex = 5;
            this.sendDataOnExit.Text = "Send Data on Exit";
            this.sendDataOnExit.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 326);
            this.Controls.Add(this.sendDataOnExit);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.sendData);
            this.Controls.Add(this.viewData);
            this.Controls.Add(this.reportCrash);
            this.Controls.Add(this.listBox1);
            this.Name = "Form1";
            this.Text = "BF4 Crash Logger";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button reportCrash;
        private System.Windows.Forms.Button viewData;
        private System.Windows.Forms.Button sendData;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.CheckBox sendDataOnExit;
    }
}

