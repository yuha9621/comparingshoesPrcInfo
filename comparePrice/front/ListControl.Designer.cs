
namespace comparePrice.front
{
    partial class ListControl
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
            this.inputListView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // inputListView
            // 
            this.inputListView.HideSelection = false;
            this.inputListView.Location = new System.Drawing.Point(23, 18);
            this.inputListView.Name = "inputListView";
            this.inputListView.Size = new System.Drawing.Size(1307, 697);
            this.inputListView.TabIndex = 0;
            this.inputListView.UseCompatibleStateImageBehavior = false;
            // 
            // ListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.inputListView);
            this.Name = "ListControl";
            this.Size = new System.Drawing.Size(1345, 727);
            this.Load += new System.EventHandler(this.ListControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView inputListView;
    }
}
