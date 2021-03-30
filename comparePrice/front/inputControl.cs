using comparePrice.service;
using comparePrice.vo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace comparePrice.front
{
    public partial class inputControl : UserControl
    {
        Dictionary<int, InputVo> inputMap = new Dictionary<int, InputVo>();
        Dictionary<int, unitServiceImpl> threadMap = new Dictionary<int, unitServiceImpl>();
        int no = 0;
        
        public inputControl()
        {
            InitializeComponent();
        }

        private void inputOkBtn_Click(object sender, EventArgs e)
        {
            InputVo inputVo = new InputVo();
            inputVo.kreamUrl = kreamUrlText.Text;
            inputVo.stockXUrl = srockXurlText.Text;
            inputVo.intervalHr = int.Parse(intervalHourText.Text);
            inputVo.intervalMm = int.Parse(intervalMmText.Text);
            inputVo.no = this.no;
            inputVo.registDate= DateTime.Now.ToString("yyyy-MM-dd , HH:mm");


            if ((String.IsNullOrWhiteSpace(kreamUrlText.Text) || String.IsNullOrWhiteSpace(srockXurlText.Text)))
            {
                MessageBox.Show("url을 입력해주세요.");
            }

            else if (!intervalCheck.Checked || (intervalHourText.Text.Equals("0") && intervalMmText.Text.Equals("0")))
            {
                inputMap.Add(no, inputVo);

                ThreadPool.QueueUserWorkItem(new unitServiceImpl().runOnce, inputVo);

                this.no++;

                inputDataClean();
            }

            else if (intervalCheck.Checked && (String.IsNullOrWhiteSpace(intervalHourText.Text) || String.IsNullOrWhiteSpace(intervalMmText.Text)))

            {
                MessageBox.Show("시간을 입력해주세요.");
            }
            else
            {
                unitServiceImpl thread = new unitServiceImpl();
                threadMap.Add(this.no, thread);
                inputMap.Add(no, inputVo);
                ThreadPool.QueueUserWorkItem(thread.runOnce, inputVo);
                inputVo.interval = inputVo.intervalHr * 3600000 + inputVo.intervalMm * 60000;
                ThreadPool.QueueUserWorkItem(thread.createThread, inputVo);
                this.no++;

                inputDataClean();
            }

        }
        private void intervalHourText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back))) { e.Handled = true; }

        }

        private void intervalMmText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back))) { e.Handled = true; }

        }

        private void intervalCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (intervalCheck.Checked)
            {
                intervalPanel.Visible = true;
            }
            else
            {
                intervalPanel.Visible = false;
            }
        }

        private void inputControl_Load(object sender, EventArgs e)
        {
            kreamUrlText.Text = "";
            srockXurlText.Text = "";
            intervalHourText.Text = 0.ToString();
            intervalMmText.Text = 0.ToString();
            inputDataClean();
        }

        private void inputCancelBtn_Click(object sender, EventArgs e)
        {
            inputDataClean();
        }

        private void inputDataClean()
        {
            kreamUrlText.Text = "";
            srockXurlText.Text = "";
            intervalHourText.Text = 0.ToString();
            intervalMmText.Text = 0.ToString();
        }
        public Dictionary<int, InputVo> getInputList() {
            return this.inputMap;
        }

        public Dictionary<int, unitServiceImpl> getThreadList()

        {
            foreach (int key in threadMap.Keys)
            {
                if (threadMap[key] == null || threadMap[key].getState().Equals("error")) {
                    threadMap.Remove(key);
                }
                
            }
                return this.threadMap;
        }

        private void test_Click(object sender, EventArgs e)
        {
            GettingInfoServiceImpl gettingInfoService = new GettingInfoServiceImpl();
            SearchVo searchVo = new SearchVo();
            searchVo.clothesYn = false;
            searchVo.sizeType = "";
            searchVo.kreamUrl = "https://kream.co.kr/products/23219";
            Dictionary<string, DetailInfo> kreamInfoMap = gettingInfoService.crawlingFromkream(searchVo);
        }
    }
}
