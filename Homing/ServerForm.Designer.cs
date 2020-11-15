
namespace Homing
{
    partial class ServerForm
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
            this.serverView = new System.Windows.Forms.TreeView();
            this.activeLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ChAllView = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.tabControl1.SuspendLayout();
            this.ChAllView.SuspendLayout();
            this.SuspendLayout();
            // 
            // serverView
            // 
            this.serverView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.serverView.Location = new System.Drawing.Point(12, 31);
            this.serverView.Name = "serverView";
            this.serverView.Size = new System.Drawing.Size(172, 400);
            this.serverView.TabIndex = 0;
            // 
            // activeLabel
            // 
            this.activeLabel.AutoSize = true;
            this.activeLabel.Location = new System.Drawing.Point(12, 9);
            this.activeLabel.Name = "activeLabel";
            this.activeLabel.Size = new System.Drawing.Size(76, 13);
            this.activeLabel.TabIndex = 1;
            this.activeLabel.Text = "<active users>";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(190, 411);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(513, 20);
            this.textBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(190, 395);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Announce to all channels: (Enter to announce)";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.ChAllView);
            this.tabControl1.Location = new System.Drawing.Point(193, 9);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(510, 383);
            this.tabControl1.TabIndex = 4;
            // 
            // ChAllView
            // 
            this.ChAllView.Controls.Add(this.listView1);
            this.ChAllView.Location = new System.Drawing.Point(4, 22);
            this.ChAllView.Name = "ChAllView";
            this.ChAllView.Padding = new System.Windows.Forms.Padding(3);
            this.ChAllView.Size = new System.Drawing.Size(502, 357);
            this.ChAllView.TabIndex = 0;
            this.ChAllView.Text = "All Channels";
            this.ChAllView.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(496, 351);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 443);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.activeLabel);
            this.Controls.Add(this.serverView);
            this.MaximumSize = new System.Drawing.Size(823, 555);
            this.MinimumSize = new System.Drawing.Size(507, 424);
            this.Name = "ServerForm";
            this.Text = "Server Manager";
            this.tabControl1.ResumeLayout(false);
            this.ChAllView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView serverView;
        private System.Windows.Forms.Label activeLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage ChAllView;
        private System.Windows.Forms.ListView listView1;
    }
}