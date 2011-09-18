namespace SimpleTextEditor
{
    partial class ReplaceBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CloseButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Selection = new System.Windows.Forms.CheckBox();
            this.Case = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ReplaceWithBox = new System.Windows.Forms.TextBox();
            this.SearchMode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ReplaceAllButton = new System.Windows.Forms.Button();
            this.ReplaceButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Location = new System.Drawing.Point(255, 138);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 8;
            this.CloseButton.Text = "&Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchButton.Location = new System.Drawing.Point(12, 138);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 5;
            this.SearchButton.Text = "Find &Next";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // SearchBox
            // 
            this.SearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchBox.Location = new System.Drawing.Point(93, 12);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(237, 20);
            this.SearchBox.TabIndex = 0;
            this.SearchBox.TextChanged += new System.EventHandler(this.SearchBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Search for:";
            // 
            // Selection
            // 
            this.Selection.AutoSize = true;
            this.Selection.Enabled = false;
            this.Selection.Location = new System.Drawing.Point(27, 91);
            this.Selection.Name = "Selection";
            this.Selection.Size = new System.Drawing.Size(121, 17);
            this.Selection.TabIndex = 3;
            this.Selection.Text = "Restrict to &Selection";
            this.Selection.UseVisualStyleBackColor = true;
            // 
            // Case
            // 
            this.Case.AutoSize = true;
            this.Case.Location = new System.Drawing.Point(27, 114);
            this.Case.Name = "Case";
            this.Case.Size = new System.Drawing.Size(83, 17);
            this.Case.TabIndex = 4;
            this.Case.Text = "&Match Case";
            this.Case.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Replace with:";
            // 
            // ReplaceWithBox
            // 
            this.ReplaceWithBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ReplaceWithBox.Location = new System.Drawing.Point(93, 38);
            this.ReplaceWithBox.Name = "ReplaceWithBox";
            this.ReplaceWithBox.Size = new System.Drawing.Size(237, 20);
            this.ReplaceWithBox.TabIndex = 1;
            // 
            // SearchMode
            // 
            this.SearchMode.Enabled = false;
            this.SearchMode.FormattingEnabled = true;
            this.SearchMode.Items.AddRange(new object[] {
            "Plain Text",
            "Wildcarded text",
            "Regular Expressions"});
            this.SearchMode.Location = new System.Drawing.Point(93, 64);
            this.SearchMode.Name = "SearchMode";
            this.SearchMode.Size = new System.Drawing.Size(237, 21);
            this.SearchMode.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(9, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Search mode:";
            // 
            // ReplaceAllButton
            // 
            this.ReplaceAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ReplaceAllButton.Location = new System.Drawing.Point(174, 138);
            this.ReplaceAllButton.Name = "ReplaceAllButton";
            this.ReplaceAllButton.Size = new System.Drawing.Size(75, 23);
            this.ReplaceAllButton.TabIndex = 7;
            this.ReplaceAllButton.Text = "Replace &All";
            this.ReplaceAllButton.UseVisualStyleBackColor = true;
            this.ReplaceAllButton.Click += new System.EventHandler(this.ReplaceAllButton_Click);
            // 
            // ReplaceButton
            // 
            this.ReplaceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ReplaceButton.Enabled = false;
            this.ReplaceButton.Location = new System.Drawing.Point(93, 138);
            this.ReplaceButton.Name = "ReplaceButton";
            this.ReplaceButton.Size = new System.Drawing.Size(75, 23);
            this.ReplaceButton.TabIndex = 6;
            this.ReplaceButton.Text = "&Replace";
            this.ReplaceButton.UseVisualStyleBackColor = true;
            this.ReplaceButton.Click += new System.EventHandler(this.ReplaceButton_Click);
            // 
            // ReplaceBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(342, 173);
            this.Controls.Add(this.ReplaceButton);
            this.Controls.Add(this.ReplaceAllButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SearchMode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ReplaceWithBox);
            this.Controls.Add(this.Case);
            this.Controls.Add(this.Selection);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SearchBox);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.CloseButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReplaceBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Replace";
            this.Load += new System.EventHandler(this.ReplaceBox_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReplaceBox_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox Selection;
        private System.Windows.Forms.CheckBox Case;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ReplaceWithBox;
        private System.Windows.Forms.ComboBox SearchMode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ReplaceAllButton;
        private System.Windows.Forms.Button ReplaceButton;
    }
}