using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SimpleTextEditor.Properties;

namespace SimpleTextEditor
{
    partial class DropDownHistoryBox : Form
    {
        public string HistoryClassText { get; set; }
        public List<object> HistoryItems { get; set; }
        public int LastSelected { get; set; }

        public event EventHandler ItemClick;

        public DropDownHistoryBox()
        {
            HistoryClassText = @"Undo";
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

            LastSelected = listBox1.TopIndex + (int)Math.Ceiling(e.Y / (double)listBox1.ItemHeight) - 1;

            for (int i = 0; i <= LastSelected; i++)
            {
                listBox1.SelectedItems.Add(listBox1.Items[i]);
            }
            label1.Text = string.Format(Resources.HistoryBoxLabel, HistoryClassText, LastSelected + 1);
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();

            if(ItemClick != null)
                ItemClick(this, EventArgs.Empty);
        }

        private void DropDownHistoryBox_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            foreach (var t in HistoryItems)
            {
                listBox1.Items.Add(t);
            }

            listBox1.Height = Math.Min(12, listBox1.Items.Count) * listBox1.ItemHeight;

            Height = (Height - ClientSize.Height) + listBox1.Height + 32;
        }

        private void DropDownHistoryBox_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void DropDownHistoryBox_Deactivate(object sender, EventArgs e)
        {
            Close();
        }
    }
}
