
namespace HomingClient
{
    partial class ClientForm
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ChMain = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.textingBox = new System.Windows.Forms.TextBox();
            this.activeLabel = new System.Windows.Forms.Label();
            this.serverView = new System.Windows.Forms.TreeView();
            this.tabControl1.SuspendLayout();
            this.ChMain.SuspendLayout();
            this.SuspendLayout();
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
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.ChMain);
            this.tabControl1.Location = new System.Drawing.Point(193, 10);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(510, 383);
            this.tabControl1.TabIndex = 9;
            // 
            // ChMain
            // 
            this.ChMain.Controls.Add(this.listView1);
            this.ChMain.Location = new System.Drawing.Point(4, 22);
            this.ChMain.Name = "ChMain";
            this.ChMain.Padding = new System.Windows.Forms.Padding(3);
            this.ChMain.Size = new System.Drawing.Size(502, 357);
            this.ChMain.TabIndex = 0;
            this.ChMain.Text = "<Main Channel>";
            this.ChMain.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(190, 396);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Send a message: (Enter to send)";
            // 
            // textingBox
            // 
            this.textingBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textingBox.Location = new System.Drawing.Point(190, 412);
            this.textingBox.Name = "textingBox";
            this.textingBox.Size = new System.Drawing.Size(513, 20);
            this.textingBox.TabIndex = 7;
            // 
            // activeLabel
            // 
            this.activeLabel.AutoSize = true;
            this.activeLabel.Location = new System.Drawing.Point(12, 10);
            this.activeLabel.Name = "activeLabel";
            this.activeLabel.Size = new System.Drawing.Size(76, 13);
            this.activeLabel.TabIndex = 6;
            this.activeLabel.Text = "<active users>";
            // 
            // serverView
            // 
            this.serverView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.serverView.Location = new System.Drawing.Point(12, 32);
            this.serverView.Name = "serverView";
            this.serverView.Size = new System.Drawing.Size(172, 400);
            this.serverView.TabIndex = 5;
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 443);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textingBox);
            this.Controls.Add(this.activeLabel);
            this.Controls.Add(this.serverView);
            this.MaximumSize = new System.Drawing.Size(823, 555);
            this.MinimumSize = new System.Drawing.Size(507, 424);
            this.Name = "ClientForm";
            this.Text = " ";
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.ChMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage ChMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textingBox;
        private System.Windows.Forms.Label activeLabel;
        private System.Windows.Forms.TreeView serverView;
    }
}