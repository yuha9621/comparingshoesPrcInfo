
namespace comparePrice.front
{
    partial class inputControl
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.intervalCheck = new System.Windows.Forms.CheckBox();
            this.intervalPanel = new System.Windows.Forms.Panel();
            this.interval = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.intervalHourText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.intervalMmText = new System.Windows.Forms.TextBox();
            this.inputCancelBtn = new System.Windows.Forms.Button();
            this.inputOkBtn = new System.Windows.Forms.Button();
            this.kreamUrlText = new System.Windows.Forms.TextBox();
            this.srockXurlText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.test = new System.Windows.Forms.Button();
            this.intervalPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // intervalCheck
            // 
            this.intervalCheck.AutoSize = true;
            this.intervalCheck.Location = new System.Drawing.Point(62, 316);
            this.intervalCheck.Name = "intervalCheck";
            this.intervalCheck.Size = new System.Drawing.Size(181, 36);
            this.intervalCheck.TabIndex = 21;
            this.intervalCheck.Text = "interval 사용";
            this.intervalCheck.UseVisualStyleBackColor = true;
            this.intervalCheck.CheckedChanged += new System.EventHandler(this.intervalCheck_CheckedChanged);
            // 
            // intervalPanel
            // 
            this.intervalPanel.Controls.Add(this.interval);
            this.intervalPanel.Controls.Add(this.label4);
            this.intervalPanel.Controls.Add(this.intervalHourText);
            this.intervalPanel.Controls.Add(this.label3);
            this.intervalPanel.Controls.Add(this.intervalMmText);
            this.intervalPanel.Location = new System.Drawing.Point(356, 304);
            this.intervalPanel.Name = "intervalPanel";
            this.intervalPanel.Size = new System.Drawing.Size(400, 200);
            this.intervalPanel.TabIndex = 20;
            this.intervalPanel.Visible = false;
            // 
            // interval
            // 
            this.interval.AutoSize = true;
            this.interval.Location = new System.Drawing.Point(16, 39);
            this.interval.Name = "interval";
            this.interval.Size = new System.Drawing.Size(93, 32);
            this.interval.TabIndex = 6;
            this.interval.Text = "interval";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(288, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 32);
            this.label4.TabIndex = 10;
            this.label4.Text = "분";
            // 
            // intervalHourText
            // 
            this.intervalHourText.Location = new System.Drawing.Point(134, 32);
            this.intervalHourText.Name = "intervalHourText";
            this.intervalHourText.Size = new System.Drawing.Size(136, 39);
            this.intervalHourText.TabIndex = 7;
            this.intervalHourText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.intervalHourText_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(288, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 32);
            this.label3.TabIndex = 9;
            this.label3.Text = "시간";
            // 
            // intervalMmText
            // 
            this.intervalMmText.Location = new System.Drawing.Point(134, 107);
            this.intervalMmText.Name = "intervalMmText";
            this.intervalMmText.Size = new System.Drawing.Size(136, 39);
            this.intervalMmText.TabIndex = 8;
            // 
            // inputCancelBtn
            // 
            this.inputCancelBtn.Location = new System.Drawing.Point(1129, 618);
            this.inputCancelBtn.Name = "inputCancelBtn";
            this.inputCancelBtn.Size = new System.Drawing.Size(150, 46);
            this.inputCancelBtn.TabIndex = 19;
            this.inputCancelBtn.Text = "초기화";
            this.inputCancelBtn.UseVisualStyleBackColor = true;
            this.inputCancelBtn.Click += new System.EventHandler(this.inputCancelBtn_Click);
            // 
            // inputOkBtn
            // 
            this.inputOkBtn.Location = new System.Drawing.Point(951, 618);
            this.inputOkBtn.Name = "inputOkBtn";
            this.inputOkBtn.Size = new System.Drawing.Size(150, 46);
            this.inputOkBtn.TabIndex = 18;
            this.inputOkBtn.Text = "확인";
            this.inputOkBtn.UseVisualStyleBackColor = true;
            this.inputOkBtn.Click += new System.EventHandler(this.inputOkBtn_Click);
            // 
            // kreamUrlText
            // 
            this.kreamUrlText.Location = new System.Drawing.Point(159, 192);
            this.kreamUrlText.Name = "kreamUrlText";
            this.kreamUrlText.Size = new System.Drawing.Size(1145, 39);
            this.kreamUrlText.TabIndex = 17;
            // 
            // srockXurlText
            // 
            this.srockXurlText.Location = new System.Drawing.Point(159, 58);
            this.srockXurlText.Name = "srockXurlText";
            this.srockXurlText.Size = new System.Drawing.Size(1145, 39);
            this.srockXurlText.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 32);
            this.label2.TabIndex = 15;
            this.label2.Text = "kream";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 32);
            this.label1.TabIndex = 14;
            this.label1.Text = "stockX";
            // 
            // test
            // 
            this.test.Location = new System.Drawing.Point(757, 618);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(150, 46);
            this.test.TabIndex = 22;
            this.test.Text = "test";
            this.test.UseVisualStyleBackColor = true;
            this.test.Visible = false;
            this.test.Click += new System.EventHandler(this.test_Click);
            // 
            // inputControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.test);
            this.Controls.Add(this.intervalCheck);
            this.Controls.Add(this.intervalPanel);
            this.Controls.Add(this.inputCancelBtn);
            this.Controls.Add(this.inputOkBtn);
            this.Controls.Add(this.kreamUrlText);
            this.Controls.Add(this.srockXurlText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "inputControl";
            this.Size = new System.Drawing.Size(1345, 727);
            this.Load += new System.EventHandler(this.inputControl_Load);
            this.intervalPanel.ResumeLayout(false);
            this.intervalPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox intervalCheck;
        private System.Windows.Forms.Panel intervalPanel;
        private System.Windows.Forms.Label interval;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox intervalHourText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox intervalMmText;
        private System.Windows.Forms.Button inputCancelBtn;
        private System.Windows.Forms.Button inputOkBtn;
        private System.Windows.Forms.TextBox kreamUrlText;
        private System.Windows.Forms.TextBox srockXurlText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button test;
    }
}
