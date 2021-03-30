using comparePrice.service;
using comparePrice.vo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace comparePrice.front
{
    public partial class MainView : Form
    {
        inputControl inputPanel = new inputControl();
        ListControl listPanel = new ListControl();
        Dictionary<int, InputVo> inputMap = new Dictionary<int, InputVo>();
        Dictionary<int, unitServiceImpl> threadMap = new Dictionary<int, unitServiceImpl>();
        public MainView()
        {
            InitializeComponent();
        }

        private void inputBtn_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            listPanel.cleanItems();
            mainPanel.Controls.Add(inputPanel);
        }

        private void MainView_Load(object sender, EventArgs e)
        {
            

        }

        private void listBtn_Click(object sender, EventArgs e)
        {
            listPanel.cleanItems();
            inputMap = inputPanel.getInputList();

            listPanel.setInputMap(inputMap);
            listPanel.showItems();
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(listPanel);
        }

        private void testBtn_Click(object sender, EventArgs e)
        {
            threadMap = inputPanel.getThreadList();
            Debug.WriteLine("main 1 : " + threadMap.Keys.Count);
            Debug.WriteLine("service  : " + inputPanel.getThreadList().Keys.Count);
        }

        private void test2Btn_Click(object sender, EventArgs e)
        {
            threadMap.Remove(0);
            Debug.WriteLine("main 2 : " + threadMap.Keys.Count);
        }

        private void threadBtn_Click(object sender, EventArgs e)
        {
            threadListView.Clear();
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(threadListView);

            threadMap = inputPanel.getThreadList();
            inputMap = inputPanel.getInputList();

            threadListView.View = View.Details;
            threadListView.GridLines = true;
            threadListView.FullRowSelect = true;

            threadListView.Columns.Add("No", 50);
            threadListView.Columns.Add("kreamUrl", 480);
            threadListView.Columns.Add("stockXUrl", 480);
            threadListView.Columns.Add("Hr", 55);
            threadListView.Columns.Add("Mm", 55);
            threadListView.Columns.Add("registered", 200);

            if (threadMap.Count > 0)
            {
                foreach (int key in threadMap.Keys)
                {
                    ListViewItem lvt = new ListViewItem();
                    lvt.Text = inputMap[key].no.ToString();
                    lvt.SubItems.Add(inputMap[key].kreamUrl.ToString());
                    lvt.SubItems.Add(inputMap[key].stockXUrl.ToString());
                    lvt.SubItems.Add(inputMap[key].intervalHr.ToString());
                    lvt.SubItems.Add(inputMap[key].intervalMm.ToString());
                    lvt.SubItems.Add(inputMap[key].registDate.ToString());
                    threadListView.Items.Add(lvt);
                }

            }
        }

        private void threadListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (threadListView.SelectedItems.Count == 1)
            {
                ListView.SelectedListViewItemCollection items = threadListView.SelectedItems;
                ListViewItem lvItem = items[0];
                string threadNo = lvItem.SubItems[0].Text;

                if (MessageBox.Show(threadNo +"번 스레드가 중지됩니다.", "스레드 중지", MessageBoxButtons.YesNo) == DialogResult.Yes)
                { //확인시 내용 MessageBox.Show("확인버튼 누름");
                    threadMap[int.Parse(threadNo)].Dispose();
                    threadMap.Remove(int.Parse(threadNo));
                }
            }
        }
    }
}
