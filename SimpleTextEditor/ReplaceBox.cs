using System;
using System.Windows.Forms;
using SimpleTextEditor.Properties;

namespace SimpleTextEditor
{
    partial class ReplaceBox : Form
    {
        readonly TextBox searchIn;

        private int searchStart;
        private int selectionStart;
        private int selectionLength;

        public ReplaceBox(TextBox place)
        {
            InitializeComponent();

            searchIn = place;

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
            if ((ReplaceButton.Enabled = DoSearchNext()) != true) 
                return;

            searchIn.SelectionStart = selectionStart;
            searchIn.SelectionLength = SearchBox.TextLength;
            searchIn.ScrollToCaret();
            selectionStart += SearchBox.TextLength;
        }

        private bool DoSearchNext()
        {
            if (SearchBox.Modified)
            {
                SearchBox.Modified = false;
                searchStart = searchIn.SelectionStart;
            }

            DialogResult result;
            do
            {
                var searchEnd = searchIn.TextLength;

                if (Selection.Checked) searchEnd = selectionStart + selectionLength;

                var comparison = Case.Checked ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;

                var pos = -1;

                if ((searchStart + SearchBox.TextLength) < searchEnd)
                {
                    pos = searchIn.Text.IndexOf(SearchBox.Text, searchStart, searchEnd - searchStart, comparison);
                }

                if (pos >= 0)
                {
                    searchStart = pos;
                    return true;
                }

                searchIn.SelectionLength = 0;
                if ((Selection.Checked) && (searchEnd < searchIn.TextLength))
                {
                    if (MessageBox.Show(Resources.EndOfSelectedAreaPrompt,
                                        Resources.SearchReplaceTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Selection.Checked = false;
                        return DoSearchNext();
                    }

                    result = MessageBox.Show(Resources.EndOfSelectionPrompt, Resources.SearchReplaceTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    searchStart = selectionStart;
                }
                else
                {
                    result = MessageBox.Show(Resources.EndOfDocumentPrompt, Resources.SearchReplaceTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    searchStart = 0;
                }
            }
            while (result == DialogResult.Yes);

            return false;
        }

        private void DoReplace(string with)
        {
            int start = searchIn.SelectionStart;
            int length = searchIn.SelectionLength;

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

            var headText = "";
            var tailText = "";
            var replacedText = ReplaceWithBox.Text;

            if (searchStart > 0)
                headText = searchIn.Text.Substring(0, searchStart);

            if ((searchStart + SearchBox.TextLength) < searchIn.TextLength)
                tailText = searchIn.Text.Substring(searchStart + SearchBox.TextLength);

            searchIn.Text = headText + replacedText + tailText;
            searchIn.Modified = true;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ReplaceBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            searchIn.SelectionStart = selectionStart;
            searchIn.SelectionLength = selectionLength;
        }

        private void ReplaceButton_Click(object sender, EventArgs e)
        {
            DoReplace(ReplaceWithBox.Text);

            ReplaceButton.Enabled = DoSearchNext();
            if (!ReplaceButton.Enabled)
                return;

            searchIn.SelectionStart = selectionStart;
            searchIn.SelectionLength = ReplaceWithBox.TextLength;
            searchIn.ScrollToCaret();
            searchStart += ReplaceWithBox.TextLength;
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
                searchStart = searchIn.SelectionStart;
            }

#if false
            int replaceStart = searchStart;
            int replaceLength = Where.TextLength - replaceStart;
            if (Selection.Checked)
                replaceLength = selectionLength - replaceStart;
#endif

            int numMatches = 0;
            bool needToContinue = true;

            int searchEnd = searchIn.TextLength;

            int searchTextLength = SearchBox.TextLength;

            if (Selection.Checked) searchEnd = selectionStart + selectionLength;

            StringComparison comparison = Case.Checked ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;

            string outText = "";

            do
            {
                int pos = -1;

                if ((searchStart+SearchBox.TextLength) < searchEnd)
                {
                    pos = searchIn.Text.IndexOf(SearchBox.Text, searchStart, searchEnd - searchStart, comparison);
                }

                if (pos >= 0)
                {
                    outText += searchIn.Text.Substring(searchStart, pos - searchStart);
                    outText += ReplaceWithBox.Text;
                    numMatches++;
                    searchStart = pos + searchTextLength;
                }
                else
                {
                    outText += searchIn.Text.Substring(searchStart);
                    searchIn.Text = outText;
                    if ((Selection.Checked) && (searchEnd < searchIn.TextLength))
                    {
                        if (MessageBox.Show(Resources.EndOfSelectedAreaPrompt,
                            Resources.SearchReplaceTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Selection.Checked = false;
                        }
                        else
                        {
                            needToContinue = (MessageBox.Show(Resources.EndOfSelectionPrompt, Resources.SearchReplaceTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
                            searchStart = selectionStart;
                        }
                    }
                    else
                    {
                        needToContinue = (MessageBox.Show(Resources.EndOfDocumentPrompt, Resources.SearchReplaceTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
                        searchStart = 0;
                    }
                }
            }
            while (needToContinue);

            MessageBox.Show(string.Format(Resources.ReplaceAllFinishedMessage, numMatches));
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
