using comparePrice.service;
using comparePrice.vo;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace comparePrice.front
{
    public partial class ListControl : UserControl
    {
        Dictionary<int, InputVo> inputMap = new Dictionary<int, InputVo>();
        Dictionary<int, unitServiceImpl> threadMap = new Dictionary<int, unitServiceImpl>();

        public ListControl()
        {
            InitializeComponent();
        }

        private void ListControl_Load(object sender, EventArgs e)
        {

        }
        public void setInputMap (Dictionary<int, InputVo> inputMap)
        {
            this.inputMap = inputMap;
        }

        public void cleanItems()
        {
            inputListView.Clear();
        }
        public void showItems()
        {
            inputListView.View = View.Details;
            inputListView.GridLines = true;
            inputListView.FullRowSelect = true;

            inputListView.Columns.Add("No", 50);
            inputListView.Columns.Add("kreamUrl", 480);
            inputListView.Columns.Add("stockXUrl",480);
            inputListView.Columns.Add("Hr", 55);
            inputListView.Columns.Add("Mm", 55);
            inputListView.Columns.Add("registered", 200);

            if (inputMap.Count > 0)
            {
                foreach (int key in inputMap.Keys)
                {
                    ListViewItem lvt = new ListViewItem();
                    lvt.Text = inputMap[key].no.ToString();
                    lvt.SubItems.Add(inputMap[key].kreamUrl.ToString());
                    lvt.SubItems.Add(inputMap[key].stockXUrl.ToString());
                    lvt.SubItems.Add(inputMap[key].intervalHr.ToString());
                    lvt.SubItems.Add(inputMap[key].intervalMm.ToString());
                    lvt.SubItems.Add(inputMap[key].registDate.ToString());
                    inputListView.Items.Add(lvt);
                }

            }
            
        }
    }
}
