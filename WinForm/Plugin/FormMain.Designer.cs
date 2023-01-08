namespace WinForm.Plugin
{
    partial class FormMain
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
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnLoadPlug = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(221, 69);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(114, 23);
            this.btnExecute.TabIndex = 0;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnLoadPlug
            // 
            this.btnLoadPlug.Location = new System.Drawing.Point(221, 22);
            this.btnLoadPlug.Name = "btnLoadPlug";
            this.btnLoadPlug.Size = new System.Drawing.Size(114, 23);
            this.btnLoadPlug.TabIndex = 1;
            this.btnLoadPlug.Text = "Load Plugins";
            this.btnLoadPlug.UseVisualStyleBackColor = true;
            this.btnLoadPlug.Click += new System.EventHandler(this.btnLoadPlug_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(13, 22);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(182, 238);
            this.listBox1.TabIndex = 2;
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(218, 138);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(35, 13);
            this.lblMsg.TabIndex = 3;
            this.lblMsg.Text = "label1";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 338);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnLoadPlug);
            this.Controls.Add(this.btnExecute);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnLoadPlug;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label lblMsg;
    }
}