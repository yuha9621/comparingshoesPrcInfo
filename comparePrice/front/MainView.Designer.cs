
namespace comparePrice.front
{
    partial class MainView
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
            this.inputBtn = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.threadListView = new System.Windows.Forms.ListView();
            this.listBtn = new System.Windows.Forms.Button();
            this.menuPanel = new System.Windows.Forms.Panel();
            this.threadBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.mainPanel.SuspendLayout();
            this.menuPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // inputBtn
            // 
            this.inputBtn.Location = new System.Drawing.Point(9, 21);
            this.inputBtn.Name = "inputBtn";
            this.inputBtn.Size = new System.Drawing.Size(168, 105);
            this.inputBtn.TabIndex = 2;
            this.inputBtn.Text = "input";
            this.inputBtn.Click += new System.EventHandler(this.inputBtn_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.threadListView);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(186, 3);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1345, 727);
            this.mainPanel.TabIndex = 3;
            // 
            // threadListView
            // 
            this.threadListView.HideSelection = false;
            this.threadListView.Location = new System.Drawing.Point(12, 9);
            this.threadListView.Name = "threadListView";
            this.threadListView.Size = new System.Drawing.Size(1324, 678);
            this.threadListView.TabIndex = 8;
            this.threadListView.UseCompatibleStateImageBehavior = false;
            this.threadListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.threadListView_MouseDoubleClick);
            // 
            // listBtn
            // 
            this.listBtn.Location = new System.Drawing.Point(9, 158);
            this.listBtn.Name = "listBtn";
            this.listBtn.Size = new System.Drawing.Size(168, 103);
            this.listBtn.TabIndex = 4;
            this.listBtn.Text = "History";
            this.listBtn.Click += new System.EventHandler(this.listBtn_Click);
            // 
            // menuPanel
            // 
            this.menuPanel.Controls.Add(this.threadBtn);
            this.menuPanel.Controls.Add(this.listBtn);
            this.menuPanel.Controls.Add(this.inputBtn);
            this.menuPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuPanel.Location = new System.Drawing.Point(3, 3);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(177, 727);
            this.menuPanel.TabIndex = 5;
            // 
            // threadBtn
            // 
            this.threadBtn.Location = new System.Drawing.Point(9, 297);
            this.threadBtn.Name = "threadBtn";
            this.threadBtn.Size = new System.Drawing.Size(168, 103);
            this.threadBtn.TabIndex = 7;
            this.threadBtn.Text = "Thread";
            this.threadBtn.Click += new System.EventHandler(this.threadBtn_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 183F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.mainPanel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.menuPanel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1534, 733);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1534, 733);
            this.Controls.Add(this.tableLayoutPanel1);
            this.HelpButton = true;
            this.MaximumSize = new System.Drawing.Size(3000, 1500);
            this.MinimumSize = new System.Drawing.Size(1560, 804);
            this.Name = "MainView";
            this.Text = "MainView";
            this.Load += new System.EventHandler(this.MainView_Load);
            this.mainPanel.ResumeLayout(false);
            this.menuPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button inputBtn;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Button listBtn;
        private System.Windows.Forms.Panel menuPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button threadBtn;
        private System.Windows.Forms.ListView threadListView;
    }
}