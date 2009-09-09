using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;

namespace SimpleTextEditor
{
    public partial class TextEditorWindow : Form
    {
        Properties.Settings appSettings;

        string filePath;
        string fileName;

        int lastFormat;

        string stringToPrint;

        string searchFor;
        int searchStart;
        bool searchCase = false;

        List<ToolStripMenuItem> recentList;

        private class HistoryItem
        {
            private string itemType;

            public string UndoType
            {
                get { return itemType; }
                set { itemType = value; }
            }

            private string itemText;

            public string Text
            {
                get { return itemText; }
                set { itemText = value; }
            }

            private int start;

            public int Start
            {
                get { return start; }
                set { start = value; }
            }

            private int length;

            public int Length
            {
                get { return length; }
                set { length = value; }
            }

            public HistoryItem(string type, string text, int first, int count)
            {
                itemType = type;
                itemText = text;
                start = first;
                length = count;
            }

            public override string ToString()
            {
                if ((!string.IsNullOrEmpty(itemText)) && (itemText.Length < 32))
                {
                    return itemType + " \"" + itemText + "\"";
                }
                else if (!string.IsNullOrEmpty(itemText))
                {
                    return itemType + " \"" + itemText.Substring(0, 30) + "[...]\"";
                }
                return itemType;
            }
        }

        List<HistoryItem> historyItems;
        HistoryItem lastItem;

        List<HistoryItem> redoHistoryItems;

        string[] cmdLineArgs;

        Size unmaximizedSize;

        public TextEditorWindow(string[] args)
        {
            InitializeComponent();
            cmdLineArgs = args;
        }

        private void btnInsertTime_Click(object sender, EventArgs e)
        {
            editBox.SelectedText = DateTime.Now.ToString();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            editBox.Cut();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            editBox.Copy();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            editBox.Paste();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DoUndo(1);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            editBox.Undo();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            if (!AskSave())
                return;
            filePath = "";
            fileName = "";
            Text = "(untitled) - SimpleTextEditor";
            editBox.Text = "";

            historyItems.Clear();
            redoHistoryItems.Clear();
            btnUndo.Enabled = false;
            btnRedo.Enabled = false;
        }

        private bool AskSave()
        {
            if (!editBox.Modified)
                return true;

            if ((editBox.TextLength == 0) && (fileName.Length == 0))
                return true;

            DialogResult result = MessageBox.Show("The text has been modified. Do you want to save before continuing?", "Attention", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Cancel)
                return false;

            if (result == DialogResult.Yes)
                return DoSave(Path.Combine(filePath, fileName), lastFormat);

            return true;
        }

        private bool DoSave(string fullFileName, int saveFormat)
        {
            if (fullFileName.Length == 0)
            {
                return DoSaveAs();
            }
            else
            {
                try
                {
                    Encoding encoding = Encoding.ASCII;
                    string text = editBox.Text;

                    if (saveFormat == 1)
                    {
                        // unix
                        text = editBox.Text.Replace("\r\n", "\n");
                    }

                    if (saveFormat == 2)
                    {
                        encoding = Encoding.UTF8;
                    }

                    StreamWriter wrt = new StreamWriter(new FileStream(fullFileName, FileMode.Create, FileAccess.Write, FileShare.None), encoding);
                    wrt.Write(editBox.Text);
                    wrt.Close();

                    lastFormat = saveFormat;

                    editBox.Modified = false;

                    return true;
                }
                catch (IOException e)
                {
                    MessageBox.Show("Error saving '" + saveFileDialog1.FileName + "':\n" + e.Message);
                    return false;
                }
            }
        }

        private bool DoSaveAs()
        {
            saveFileDialog1.InitialDirectory = filePath;
            saveFileDialog1.FileName = fileName;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (DoSave(saveFileDialog1.FileName, saveFileDialog1.FilterIndex - 1))
                {
                    fileName = Path.GetFileName(saveFileDialog1.FileName);
                    filePath = saveFileDialog1.FileName.Substring(0, saveFileDialog1.FileName.Length - fileName.Length);
                    Text = fileName + " - SimpleTextEditor";
                    return true;
                }
            }
            return false;
        }

        private void TextEditorWindow_Load(object sender, EventArgs e)
        {
            appSettings = new SimpleTextEditor.Properties.Settings();
            appSettings.Reload();

            filePath = "";
            fileName = "";
            if (appSettings.Font != null)
            {
                editBox.Font = appSettings.Font;
                editBox.ForeColor = appSettings.TextColor;
                editBox.BackColor = appSettings.BackColor;
            }
            mnuWordWrap.Checked = appSettings.UseWordWrap;
            mnuStatusBar.Checked = appSettings.ShowStatusBar;

            editBox.WordWrap = appSettings.UseWordWrap;
            statusBar.Visible = appSettings.ShowStatusBar;

            searchBar.Visible = false;
            foreach (string s in appSettings.SearchHistory)
            {
                searchBox.AutoCompleteCustomSource.Add(s);
            }

            btnUndo.Enabled = false;
            btnRedo.Enabled = false;

            historyItems = new List<HistoryItem>();
            redoHistoryItems = new List<HistoryItem>();

            recentList = new List<ToolStripMenuItem>();

            if (appSettings.RecentList == null) appSettings.RecentList = new System.Collections.Specialized.StringCollection();

            UpdateRecentList();

            UpdateStatusBar();

            if (appSettings.WindowSizeSaved)
            {
                if ((appSettings.WindowSizeWidth > 0) && (appSettings.WindowSizeHeight > 0))
                    this.Size = new Size(appSettings.WindowSizeWidth,appSettings.WindowSizeHeight);
            }

            unmaximizedSize = this.Size;

            // show the window before starting to load the data
            this.Visible = true;
            this.Refresh();

            if (cmdLineArgs.Length > 0)
            {
                DoOpenFile(cmdLineArgs[0]);
            }
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            TheAboutBox aboutBox = new TheAboutBox();
            aboutBox.ShowDialog();
        }

        private void saveToolStripButton_ButtonClick(object sender, EventArgs e)
        {
            DoSave(Path.Combine(filePath, fileName), lastFormat);
        }

        private void saveAsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoSaveAs();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
        }

        private void openToolStripButton_ButtonClick(object sender, EventArgs e)
        {
            if (!AskSave())
                return;

            openFileDialog1.InitialDirectory = filePath;
            openFileDialog1.FileName = fileName;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DoOpenFile(openFileDialog1.FileName);
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = editBox.Font;
            fontDialog1.Color = editBox.ForeColor;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                DoSetFont(fontDialog1.Font, fontDialog1.Color);
            }
        }

        private void DoSetFont(Font font, Color color)
        {
            editBox.Font = font;
            editBox.ForeColor = fontDialog1.Color;
            appSettings.Font = font;
            appSettings.TextColor = editBox.ForeColor;
            appSettings.Save();
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editBox.WordWrap = !editBox.WordWrap;
            mnuWordWrap.Checked = editBox.WordWrap;
            appSettings.UseWordWrap = editBox.WordWrap;
            appSettings.Save();
        }

        private void printToolStripButton_ButtonClick(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void pageOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.Document = printDocument1;
            if (pageSetupDialog1.ShowDialog() == DialogResult.OK)
            {
                // ...?
            }
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            try
            {
                printPreviewDialog1.ShowDialog();
            }
            catch (Win32Exception)
            {
            }
        }

        private void printDialogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                // ...?
                printDocument1.Print();
            }
        }

        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            stringToPrint = editBox.Text;
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int charactersOnPage = 0;
            int linesPerPage = 0;

            // Sets the value of charactersOnPage to the number of characters 
            // of stringToPrint that will fit within the bounds of the page.
            e.Graphics.MeasureString(stringToPrint, editBox.Font,
                e.MarginBounds.Size, StringFormat.GenericTypographic,
                out charactersOnPage, out linesPerPage);

            // Draws the string within the bounds of the page
            e.Graphics.DrawString(stringToPrint, editBox.Font, Brushes.Black,
                e.MarginBounds, StringFormat.GenericTypographic);

            // Remove the portion of the string that has been printed.
            stringToPrint = stringToPrint.Substring(charactersOnPage);

            // Check to see if more pages are to be printed.
            e.HasMorePages = (stringToPrint.Length > 0);
        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {
            DoSetFont(fontDialog1.Font, fontDialog1.Color);
        }

        private void UpdateStatusBar()
        {
            int ln = editBox.GetLineFromCharIndex(editBox.SelectionStart);
            int col = editBox.SelectionStart - editBox.GetFirstCharIndexOfCurrentLine();

            lblPosition.Text = "Ln. " + (ln + 1).ToString() + " Col. " + (col + 1).ToString();
        }

        private void mruToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openToolStripButton_ButtonClick(sender, e);
        }

        private void statusbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusBar.Visible = !statusBar.Visible;
            mnuStatusBar.Checked = statusBar.Visible;
            appSettings.ShowStatusBar = statusBar.Visible;
            appSettings.Save();
        }

        private void TextEditorWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AskSave() && (e.CloseReason == CloseReason.UserClosing))
            {
                e.Cancel = true;
                return;
            }

            if (unmaximizedSize.Width > 0 && unmaximizedSize.Height > 0)
            {
                appSettings.WindowSizeSaved = true;
                appSettings.WindowSizeWidth = unmaximizedSize.Width;
                appSettings.WindowSizeHeight = unmaximizedSize.Height;
                appSettings.Save();
            }
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileDrop = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                if (fileDrop.Length != 1)
                    return;

                e.Effect = (DragDropEffects.Copy | DragDropEffects.Move) & e.AllowedEffect;
            }
            else if (e.Data.GetDataPresent(DataFormats.Text, true) | e.Data.GetDataPresent(DataFormats.UnicodeText, true))
            {
                e.Effect = (DragDropEffects.Copy | DragDropEffects.Move) & e.AllowedEffect;
            }
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);

            pt = editBox.PointToClient(pt);

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileDrop = (string[])e.Data.GetData(DataFormats.FileDrop, false);

                if (!AskSave())
                    return;

                DoOpenFile(fileDrop[0]);

            }
            else if (e.Data.GetDataPresent(DataFormats.UnicodeText, true))
            {
                int t = editBox.GetCharIndexFromPosition(pt);
                editBox.SelectionLength = 0;
                editBox.SelectionStart = t;
                editBox.SelectedText = (string)e.Data.GetData(DataFormats.UnicodeText, true);

                AddUndoHistoryItem("Paste", "", editBox.SelectionStart, 0);
                lastItem.Text = editBox.SelectedText;
                lastItem.Length = lastItem.Text.Length;
            }
            else if (e.Data.GetDataPresent(DataFormats.Text, true))
            {
                int t = editBox.GetCharIndexFromPosition(pt);
                editBox.SelectionLength = 0;
                editBox.SelectionStart = t;
                editBox.SelectedText = (string)e.Data.GetData(DataFormats.Text, true);

                AddUndoHistoryItem("Paste", "", editBox.SelectionStart, 0);
                lastItem.Text = editBox.SelectedText;
                lastItem.Length = lastItem.Text.Length;
            }

        }

        private void textBox1_DragOver(object sender, DragEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);

            pt = editBox.PointToClient(pt);

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = (DragDropEffects.Copy | DragDropEffects.Move) & e.AllowedEffect;
            }
            else if (e.Data.GetDataPresent(DataFormats.UnicodeText, true))
            {
                int t = editBox.GetCharIndexFromPosition(pt);
                editBox.SelectionLength = 0;
                editBox.SelectionStart = t;
                e.Effect = (DragDropEffects.Copy | DragDropEffects.Move) & e.AllowedEffect;
            }
            else if (e.Data.GetDataPresent(DataFormats.Text, true))
            {
                int t = editBox.GetCharIndexFromPosition(pt);
                editBox.SelectionLength = 0;
                editBox.SelectionStart = t;
                e.Effect = (DragDropEffects.Copy | DragDropEffects.Move) & e.AllowedEffect;
            }
        }

        private void DoOpenFile(string fullFileName)
        {
            try
            {
                byte[] bytes;

                FileStream stream = new FileStream(fullFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);

                //00 00 FE FF  	UTF-32, big-endian
                //FF FE 00 00 	UTF-32, little-endian
                //FE FF 	UTF-16, big-endian
                //FF FE 	UTF-16, little-endian
                //EF BB BF 	UTF-8

                bool isUnicode = false;
                if ((bytes.Length > 2) && (bytes[0] == 0xFF) && (bytes[1] == 0xFE)) isUnicode = true;
                if ((bytes.Length > 2) && (bytes[0] == 0xFE) && (bytes[1] == 0xFF)) isUnicode = true;
                if ((bytes.Length > 4) && (bytes[0] == 0x00) && (bytes[1] == 0x00) && (bytes[2] == 0xFE) && (bytes[3] == 0xFF)) isUnicode = true;
                if ((bytes.Length > 3) && (bytes[0] == 0xEF) && (bytes[1] == 0xBB) && (bytes[2] == 0xBF)) isUnicode = true;

                StreamReader rdr = new StreamReader(new MemoryStream(bytes), true);
                editBox.Text = rdr.ReadToEnd();
                rdr.Close();

                // Detect formatting
                if (editBox.Text.IndexOf("\r\n") < 0) // if doesn't have DOS line endings
                {
                    lastFormat = 0;
                    if (editBox.Text.IndexOf('\n') >= 0)
                    {
                        // Uses unix line endings
                        editBox.Text = editBox.Text.Replace("\n", "\r\n");
                        lastFormat = 1;
                    }
                    else if (editBox.Text.IndexOf('\r') >= 0)
                    {
                        // Uses MAC line endings
                        editBox.Text = editBox.Text.Replace("\r", "\r\n");
                    }
                }
                if (isUnicode) lastFormat = 2;

                fileName = Path.GetFileName(fullFileName);
                filePath = fullFileName.Substring(0, fullFileName.Length - fileName.Length);
                Text = fileName + " - SimpleTextEditor";

                if (appSettings.RecentList.Contains(fullFileName))
                    appSettings.RecentList.Remove(fullFileName);
                appSettings.RecentList.Insert(0, fullFileName);
                while (appSettings.RecentList.Count > appSettings.MaxRecentListSize)
                    appSettings.RecentList.RemoveAt(appSettings.RecentList.Count - 1);
                appSettings.Save();

                UpdateRecentList();

                editBox.SelectionStart = 0;
                editBox.SelectionLength = 0;
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error opening '" + fullFileName + "':\n" + ex.Message);
            }
        }

        private void UpdateRecentList()
        {
            foreach (ToolStripMenuItem it in recentList)
            {
                it.Visible = false;
                btnOpen.DropDownItems.Remove(it);
                it.Dispose();
            }

            foreach (string fullPath in appSettings.RecentList)
            {
                string path = fullPath;
                if (fullPath.Length > 64)
                {
                    string[] pathParts = fullPath.Split('\\', '/');

                    string left = pathParts[0];
                    string right = pathParts[pathParts.Length - 1];

                    int iLeft = 1;
                    int iRight = pathParts.Length - 2;

                    bool even = true;

                    while ((iLeft <= iRight) && ((left.Length + right.Length) < 50))
                    {
                        if (even)
                        {
                            left += "\\" + pathParts[iLeft++];
                        }
                        else
                        {
                            right = pathParts[iRight--] + "\\" + right;
                        }
                        even = !even;
                    }

                    if (iLeft <= iRight)
                        path = left + "\\...\\" + right;
                    else
                        path = left + right;

                }

                ToolStripMenuItem item = new ToolStripMenuItem(path, null, new EventHandler(RecentList_Click));
                item.Tag = fullPath;
                btnOpen.DropDownItems.Add(item);
                recentList.Add(item);
            }

            mnuRecent.Visible = (appSettings.RecentList.Count == 0);
        }

        private void editBox_TextChanged(object sender, EventArgs e)
        {
            UpdateStatusBar();
        }

        private void editBox_Click(object sender, EventArgs e)
        {
            UpdateStatusBar();
        }

        private void editBox_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateStatusBar();
        }

        private void editBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.A))
            {
                editBox.SelectionStart = 0;
                editBox.SelectionLength = editBox.TextLength;
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.F))
            {
                searchBar.Visible = true;
                btnSearch.CheckState = CheckState.Checked;
                searchBox.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.Z))
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                DoUndo(1);
            }
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.Y))
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                DoRedo(1);
            }

            if ((e.KeyCode == Keys.Delete) && (e.Modifiers == Keys.None))
            {
                if (editBox.TextLength > 0)
                {
                    if ((lastItem == null) || (lastItem.UndoType != "Delete"))
                    {
                        AddUndoHistoryItem("Delete", "", editBox.SelectionStart, 0);
                    }
                    if (editBox.SelectionStart != (lastItem.Start))
                    {
                        AddUndoHistoryItem("Delete", "", editBox.SelectionStart, 0);
                    }
                    lastItem.Length++;
                    lastItem.Text += editBox.Text[lastItem.Start];
                }
            }

            UpdateStatusBar();
        }

        private void btnUndo_DropDownOpening(object sender, EventArgs e)
        {
            Point pos = btnUndo.Bounds.Location + new Size(0, btnUndo.Bounds.Height);

            pos = toolBar.PointToScreen(pos);

            DropDownHistoryBox history = new DropDownHistoryBox();
            history.HistoryItems = historyItems.ConvertAll<object>(new Converter<HistoryItem, object>(CvtHItoObj));
            history.Show();
            history.SetDesktopLocation(pos.X, pos.Y);
            history.ItemClick += new EventHandler(History_ItemClick);

        }

        private void History_ItemClick(object sender, EventArgs e)
        {
            DoUndo(((DropDownHistoryBox)sender).LastSelected + 1);
        }

        private void RedoHistory_ItemClick(object sender, EventArgs e)
        {
            DoRedo(((DropDownHistoryBox)sender).LastSelected + 1);
        }

        private void DoUndo(int actionsToUndo)
        {
            if (historyItems.Count < actionsToUndo)
                actionsToUndo = historyItems.Count;
            //throw new NotImplementedException();
            for (int i = 0; i < actionsToUndo; i++)
            {
                switch (historyItems[0].UndoType)
                {
                    case "Type":
                    case "Paste":
                        editBox.SelectionStart = historyItems[0].Start;
                        editBox.SelectionLength = historyItems[0].Length;
                        editBox.SelectedText = "";
                        break;
                    case "Delete":
                    case "Replace":
                    case "Cut":
                        editBox.SelectionStart = historyItems[0].Start;
                        editBox.SelectionLength = 0;
                        editBox.SelectedText = historyItems[0].Text;
                        break;
                }
                redoHistoryItems.Insert(0, historyItems[0]);
                btnRedo.Enabled = true;
                historyItems.RemoveAt(0);
            }
            if (historyItems.Count == 0)
            {
                btnUndo.Enabled = false;
            }
            lastItem = null;
        }

        private void DoRedo(int actionsToRedo)
        {
            if (redoHistoryItems.Count < actionsToRedo)
                actionsToRedo = redoHistoryItems.Count;

            //throw new NotImplementedException();
            for (int i = 0; i < actionsToRedo; i++)
            {
                switch (redoHistoryItems[0].UndoType)
                {
                    case "Type":
                    case "Paste":
                        editBox.SelectionStart = redoHistoryItems[0].Start;
                        editBox.SelectionLength = 0;
                        editBox.SelectedText = redoHistoryItems[0].Text;
                        break;
                    case "Delete":
                    case "Replace":
                    case "Cut":
                        editBox.SelectionStart = redoHistoryItems[0].Start;
                        editBox.SelectionLength = redoHistoryItems[0].Length;
                        editBox.SelectedText = "";
                        break;
                }
                historyItems.Insert(0, redoHistoryItems[0]);
                btnUndo.Enabled = true;
                redoHistoryItems.RemoveAt(0);
            }
            if (redoHistoryItems.Count == 0)
            {
                btnRedo.Enabled = false;
            }
            lastItem = null;
        }

        private object CvtHItoObj(HistoryItem i) { return i; }

        private void toolBar_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void AddUndoHistoryItem(string type, string data, int start, int length)
        {
            lastItem = new HistoryItem(type, data, start, length);
            historyItems.Insert(0, lastItem);
            btnUndo.Enabled = true;
            btnRedo.Enabled = false;
            redoHistoryItems.Clear();
        }

        private void editBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch ((int)e.KeyChar)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 10:
                case 11:
                case 12:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 23:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                    // nothing to do on those control codes
                    break;
                case 22:
                    if (editBox.SelectionLength > 0)
                    {
                        AddUndoHistoryItem("Replaced", "", editBox.SelectionStart, editBox.SelectionLength);
                        lastItem.Text = editBox.SelectedText;
                    }
                    if (Clipboard.ContainsText())
                    {
                        AddUndoHistoryItem("Paste", "", editBox.SelectionStart, 0);
                        lastItem.Text = Clipboard.GetText();
                        lastItem.Length = lastItem.Text.Length;
                    }
                    break;
                case 24:
                    if (editBox.SelectionLength > 0)
                    {
                        AddUndoHistoryItem("Cut", "", editBox.SelectionStart, editBox.SelectionLength);
                        lastItem.Text = editBox.SelectedText;
                    }
                    break;
                case 8:
                    if (editBox.SelectionStart > 0)
                    {
                        if ((lastItem == null) || (lastItem.UndoType != "Delete"))
                        {
                            AddUndoHistoryItem("Delete", "", editBox.SelectionStart, 0);
                        }
                        if (editBox.SelectionStart != (lastItem.Start))
                        {
                            AddUndoHistoryItem("Delete", "", editBox.SelectionStart, 0);
                        }
                        lastItem.Length++;
                        lastItem.Start--;
                        lastItem.Text = editBox.Text[lastItem.Start] + lastItem.Text;
                    }
                    break;
                default:
                    if (editBox.SelectionLength > 0)
                    {
                        AddUndoHistoryItem("Replaced", "", editBox.SelectionStart, editBox.SelectionLength);
                        lastItem.Text = editBox.SelectedText;
                    }
                    if ((lastItem == null) || (lastItem.UndoType != "Type"))
                    {
                        AddUndoHistoryItem("Type", "", editBox.SelectionStart, 0);
                    }
                    if (editBox.SelectionStart != (lastItem.Start + lastItem.Length))
                    {
                        AddUndoHistoryItem("Type", "", editBox.SelectionStart, 0);
                    }
                    lastItem.Text += e.KeyChar;
                    lastItem.Length++;
                    break;
            }

        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            DoRedo(1);
        }

        private void toolStripSplitButton1_DropDownOpening(object sender, EventArgs e)
        {
            Point pos = btnRedo.Bounds.Location + new Size(0, btnRedo.Bounds.Height);

            pos = toolBar.PointToScreen(pos);

            DropDownHistoryBox history = new DropDownHistoryBox();
            history.HistoryClassText = "Redo";
            history.HistoryItems = redoHistoryItems.ConvertAll<object>(new Converter<HistoryItem, object>(CvtHItoObj));
            history.Show();
            history.SetDesktopLocation(pos.X, pos.Y);
            history.ItemClick += new EventHandler(RedoHistory_ItemClick);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            searchBar.Visible = !searchBar.Visible;
            btnSearch.CheckState = searchBar.Visible ? CheckState.Checked : CheckState.Unchecked;
        }

        private bool DoSearchNext()
        {
            StringComparison comparison = searchCase ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;

            int searchEnd = editBox.TextLength;
            int pos = -1;

            if ((searchStart + searchFor.Length) < searchEnd)
            {
                pos = editBox.Text.IndexOf(searchFor, searchStart, searchEnd - searchStart,  comparison  );
            }

            if (pos >= 0)
            {
                editBox.SelectionStart = pos;
                editBox.SelectionLength = searchFor.Length;
                editBox.ScrollToCaret();
                searchStart = pos + searchFor.Length;
                return true;
            }
            else
            {
                editBox.SelectionLength = 0;
                return false;
            }
        }

        private void searchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            searchBar.Visible = false;
            btnSearch.CheckState = CheckState.Unchecked;
        }

        private void searchBox_Click(object sender, EventArgs e)
        {

        }

        private void searchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (searchBox.Modified)
                {
                    searchBox.Modified = false;
                    appSettings.SearchHistory.Add(searchBox.Text);
                    searchBox.AutoCompleteCustomSource.Add(searchBox.Text);
                    while (appSettings.SearchHistory.Count > 128)
                    {
                        appSettings.SearchHistory.RemoveAt(0);
                        searchBox.AutoCompleteCustomSource.RemoveAt(0);
                    }

                    searchStart = editBox.SelectionStart;
                }

                searchFor = searchBox.Text;
                if (!DoSearchNext())
                {
                    searchEndLabel.Visible = true;
                    searchStart = 0;
                }
                else
                {
                    searchStart++;
                    searchEndLabel.Visible = false;
                }
                e.Handled = true;
            }

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            searchFor = searchBox.Text;
            if (!DoSearchNext())
            {
                searchEndLabel.Visible = true;
                searchStart = 0;
            }
            else searchEndLabel.Visible = false;
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = editBox.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                editBox.BackColor = colorDialog1.Color;
                appSettings.BackColor = editBox.BackColor;
                appSettings.Save();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ReplaceBox replace = new ReplaceBox(editBox);
            replace.ShowDialog();
        }

        private void TextEditorWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (ToolStripMenuItem it in recentList)
            {
                btnOpen.DropDownItems.Remove(it);
                it.Dispose();
            }
        }

        private void RecentList_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem it = (ToolStripMenuItem)sender;

            if (!AskSave())
                return;

            DoOpenFile((string)it.Tag);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            searchCase = !searchCase;
            toolStripButton5.CheckState = searchCase ? CheckState.Unchecked : CheckState.Checked;
        }

        private void editBox_Click_1(object sender, EventArgs e)
        {
            UpdateStatusBar();
        }

        private void TextEditorWindow_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                unmaximizedSize = this.Size;
            }
        }
    }
}