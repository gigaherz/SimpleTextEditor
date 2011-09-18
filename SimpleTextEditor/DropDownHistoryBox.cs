using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SimpleTextEditor
{
    partial class DropDownHistoryBox : Form
    {
        private string historyClassText = "Undo";

        public string HistoryClassText
        {
            get { return historyClassText; }
            set { historyClassText = value; }
        }

        private List<object> history;

        public List<object> HistoryItems
        {
            get { return history; }
            set { history = value; }
        }

        private int lastSelected;

        public int LastSelected
        {
            get { return lastSelected; }
            set { lastSelected = value; }
        }

        public event EventHandler ItemClick;

        public DropDownHistoryBox()
        {
            InitializeComponent();
        }

        private void label1_TextChanged(object sender, EventArgs e)
        {
            label1.Left = (ClientSize.Width - label1.Width) / 2;
            label1.Top = listBox1.Height + (ClientSize.Height - listBox1.Height - label1.Height) / 2;
        }

        private void listBox1_MouseMove(object sender, MouseEventArgs e)
        {
            listBox1.SelectedItems.Clear();

            lastSelected = listBox1.TopIndex + (int)Math.Ceiling(e.Y / (double)listBox1.ItemHeight) - 1;

            for (int i = 0; i <= lastSelected; i++)
            {
                listBox1.SelectedItems.Add(listBox1.Items[i]);
            }
            label1.Text = historyClassText + " " + (lastSelected + 1) + " Actions.";
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
            ItemClick(this, EventArgs.Empty);
        }

        private void DropDownHistoryBox_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (object t in history)
            {
                listBox1.Items.Add(t);
            }

            if (listBox1.Items.Count < 12)
            {
                listBox1.Height = listBox1.Items.Count * listBox1.ItemHeight;
            }
            else
            {
                listBox1.Height = 12 * listBox1.ItemHeight;
            }
            this.Height = (this.Height - this.ClientSize.Height) + listBox1.Height + 32;
        }

        private void DropDownHistoryBox_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void DropDownHistoryBox_Deactivate(object sender, EventArgs e)
        {
            Close();
        }
    }
}
