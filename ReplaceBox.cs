using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SimpleTextEditor
{
    public partial class ReplaceBox : Form
    {
        TextBox Where;

        private int searchStart;
        private int selectionStart;
        private int selectionLength;

        public ReplaceBox(TextBox place)
        {
            InitializeComponent();

            Where = place;

            Selection.Enabled = place.SelectionLength > 0;
            Selection.Checked = place.SelectedText.IndexOf('\n') > 0;
            SearchMode.SelectedIndex = 0;

            selectionStart = place.SelectionStart;
            selectionLength = place.SelectionLength;
        }

        private void ReplaceBox_Load(object sender, EventArgs e)
        {
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if ((ReplaceButton.Enabled = DoSearchNext()) == true)
            {
                Where.SelectionStart = selectionStart;
                Where.SelectionLength = SearchBox.TextLength;
                Where.ScrollToCaret();
                selectionStart += SearchBox.TextLength;
            }
        }

        private bool DoSearchNext()
        {
            if (SearchBox.Modified)
            {
                SearchBox.Modified = false;
                searchStart = Where.SelectionStart;
            }

            DialogResult result = DialogResult.None;
            do
            {
                int searchEnd = Where.TextLength;

                if (Selection.Checked) searchEnd = selectionStart + selectionLength;

                StringComparison comparison = Case.Checked ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;

                int pos = -1;

                if ((searchStart + SearchBox.TextLength) < searchEnd)
                {
                    pos = Where.Text.IndexOf(SearchBox.Text, searchStart, searchEnd - searchStart, comparison);
                }

                if (pos >= 0)
                {
                    searchStart = pos;
                    return true;
                }
                else
                {
                    Where.SelectionLength = 0;
                    if ((Selection.Checked) && (searchEnd < Where.TextLength))
                    {
                        if (MessageBox.Show("Reached the end of the selected area.\nDo you want to expand the search to the rest of the document?",
                            "Search & Replace", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Selection.Checked = false;
                            return DoSearchNext();
                        }
                        else
                        {
                            result = MessageBox.Show("Reached the end of the selection.\nDo you want to continue from the beginning?", "Search & Replace", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            searchStart = selectionStart;
                        }
                    }
                    else
                    {
                        result = MessageBox.Show("Reached the end of the document.\nDo you want to continue from the beginning?", "Search & Replace", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        searchStart = 0;
                    }
                }
            }
            while (result == DialogResult.Yes);

            return false;
        }

        private void DoReplace(string with)
        {
            int start = Where.SelectionStart;
            int length = Where.SelectionLength;

            // adjust selection
            if (start < selectionStart)
            {
                if ((start + length) < selectionStart)
                {
                    selectionStart += (with.Length - length);
                }
                else
                {
                    // weird case: replaced text is part outside and part inside the selection: make it all inside.
                    selectionStart = start;
                }
            }
            else if ((start + length) >= (selectionStart + selectionLength))
            {
                if (start < (selectionStart + selectionLength))
                {
                    // weird case: replaced text is part inside and part outside the selection: make it all inside.
                    selectionLength = (start + length) - selectionStart;
                }
                // else nothing to adjust.
            }
            else
            {
                selectionLength += (with.Length - length);
            }

            string headText = "";
            string tailText = "";
            string replacedText = ReplaceWithBox.Text;

            if (searchStart > 0)
                headText = Where.Text.Substring(0, searchStart);

            if ((searchStart + SearchBox.TextLength) < Where.TextLength)
                tailText = Where.Text.Substring(searchStart + SearchBox.TextLength);

            Where.Text = headText + replacedText + tailText;
            Where.Modified = true;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ReplaceBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            Where.SelectionStart = selectionStart;
            Where.SelectionLength = selectionLength;
        }

        private void ReplaceButton_Click(object sender, EventArgs e)
        {
            DoReplace(ReplaceWithBox.Text);

            if ((ReplaceButton.Enabled = DoSearchNext()) == true)
            {
                Where.SelectionStart = selectionStart;
                Where.SelectionLength = ReplaceWithBox.TextLength;
                Where.ScrollToCaret();
                searchStart += ReplaceWithBox.TextLength;
            }
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            ReplaceButton.Enabled = false;
        }

        private void ReplaceAllButton_Click(object sender, EventArgs e)
        {
            if (SearchBox.Modified)
            {
                SearchBox.Modified = false;
                searchStart = Where.SelectionStart;
            }

            int replaceStart = searchStart;
            int replaceLength = Where.TextLength - replaceStart;
            if (Selection.Checked)
                replaceLength = selectionLength - replaceStart;

            int numMatches = 0;
            bool needToContinue = true;

            int searchEnd = Where.TextLength;

            int searchTextLength = SearchBox.TextLength;

            if (Selection.Checked) searchEnd = selectionStart + selectionLength;

            StringComparison comparison = Case.Checked ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;

            string outText = "";

            do
            {
                int pos = -1;

                if ((searchStart+SearchBox.TextLength) < searchEnd)
                {
                    pos = Where.Text.IndexOf(SearchBox.Text, searchStart, searchEnd - searchStart, comparison);
                }

                if (pos >= 0)
                {
                    outText += Where.Text.Substring(searchStart, pos - searchStart);
                    outText += ReplaceWithBox.Text;
                    numMatches++;
                    searchStart = pos + searchTextLength;
                }
                else
                {
                    needToContinue = false;
                    outText += Where.Text.Substring(searchStart);
                    Where.Text = outText;
                    if ((Selection.Checked) && (searchEnd < Where.TextLength))
                    {
                        if (MessageBox.Show("Reached the end of the selected area.\nDo you want to expand the search to the rest of the document?",
                            "Search & Replace", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Selection.Checked = false;
                            needToContinue = true;
                        }
                        else
                        {
                            needToContinue = (MessageBox.Show("Reached the end of the selection.\nDo you want to continue from the beginning?", "Search & Replace", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
                            searchStart = selectionStart;
                        }
                    }
                    else
                    {
                        needToContinue = (MessageBox.Show("Reached the end of the document.\nDo you want to continue from the beginning?", "Search & Replace", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
                        searchStart = 0;
                    }
                }
            }
            while (needToContinue);

            MessageBox.Show("Replace All finished. Replaced " + numMatches + " matches.");
#if false
            // Notepad's replace sucks because it's terribly slow.
            // I'm guessing it's because it does a loop of "replace next" actions until it reaches the end.
            // I dont' want to copy that.

            // 1. Start where the search start is now
            if (SearchBox.Modified)
            {
                SearchBox.Modified = false;
                searchStart = Where.SelectionStart;
            }

            int replaceStart = searchStart;
            int replaceLength = Where.TextLength - replaceStart;
            if (Selection.Checked)
                replaceLength = selectionLength - replaceStart;

            string headText = "";
            string tailText = "";
            string bodyText = Where.Text.Substring(replaceStart, replaceLength);

            string replacedText = bodyText.Replace(SearchBox.Text, ReplaceWithBox.Text);

            if (replacedText != bodyText)
            {
                if (replaceStart > 0)
                    headText = Where.Text.Substring(0, replaceStart);

                if ((replaceStart + replaceLength) < Where.TextLength)
                    tailText = Where.Text.Substring(replaceStart + replaceLength);


                Where.Text = headText + replacedText + tailText;
                Where.Modified = true;
            }
            else
            {
                MessageBox.Show("No matches.\nReplace All will continue from the beginning if you press again.");

                searchStart = 0;
                if (Selection.Checked)
                    searchStart = selectionStart;
            }
#endif
        }
    }
}
