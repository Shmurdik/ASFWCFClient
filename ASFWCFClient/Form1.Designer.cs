namespace ASFWCFClient
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("For Bot(s)", System.Windows.Forms.HorizontalAlignment.Center);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox_command = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown_delay = new System.Windows.Forms.NumericUpDown();
            this.textBox_args = new System.Windows.Forms.TextBox();
            this.comboBox_command = new System.Windows.Forms.ComboBox();
            this.button_execute = new System.Windows.Forms.Button();
            this.richTextBox_result = new System.Windows.Forms.RichTextBox();
            this.label_result = new System.Windows.Forms.Label();
            this.listView_botname = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip_botname = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox_search_in_res = new System.Windows.Forms.TextBox();
            this.textBox_search_botname = new System.Windows.Forms.TextBox();
            this.groupBox_command.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_delay)).BeginInit();
            this.contextMenuStrip_botname.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_command
            // 
            this.groupBox_command.AutoSize = true;
            this.groupBox_command.Controls.Add(this.label1);
            this.groupBox_command.Controls.Add(this.numericUpDown_delay);
            this.groupBox_command.Controls.Add(this.textBox_args);
            this.groupBox_command.Controls.Add(this.comboBox_command);
            this.groupBox_command.Location = new System.Drawing.Point(158, 12);
            this.groupBox_command.Name = "groupBox_command";
            this.groupBox_command.Size = new System.Drawing.Size(362, 87);
            this.groupBox_command.TabIndex = 0;
            this.groupBox_command.TabStop = false;
            this.groupBox_command.Text = "Command";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Delay";
            // 
            // numericUpDown_delay
            // 
            this.numericUpDown_delay.Location = new System.Drawing.Point(46, 48);
            this.numericUpDown_delay.Name = "numericUpDown_delay";
            this.numericUpDown_delay.Size = new System.Drawing.Size(43, 20);
            this.numericUpDown_delay.TabIndex = 5;
            this.numericUpDown_delay.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // textBox_args
            // 
            this.textBox_args.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_args.ForeColor = System.Drawing.Color.Gray;
            this.textBox_args.Location = new System.Drawing.Point(133, 19);
            this.textBox_args.Name = "textBox_args";
            this.textBox_args.Size = new System.Drawing.Size(223, 20);
            this.textBox_args.TabIndex = 4;
            this.textBox_args.Text = "Arguments...";
            this.textBox_args.WordWrap = false;
            this.textBox_args.Enter += new System.EventHandler(this.textBox_args_Enter);
            this.textBox_args.Leave += new System.EventHandler(this.textBox_args_Leave);
            // 
            // comboBox_command
            // 
            this.comboBox_command.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox_command.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox_command.FormattingEnabled = true;
            this.comboBox_command.Location = new System.Drawing.Point(6, 19);
            this.comboBox_command.Name = "comboBox_command";
            this.comboBox_command.Size = new System.Drawing.Size(121, 21);
            this.comboBox_command.TabIndex = 3;
            // 
            // button_execute
            // 
            this.button_execute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_execute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_execute.Font = new System.Drawing.Font("Lucida Console", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_execute.ForeColor = System.Drawing.Color.ForestGreen;
            this.button_execute.Location = new System.Drawing.Point(600, 12);
            this.button_execute.Name = "button_execute";
            this.button_execute.Size = new System.Drawing.Size(144, 82);
            this.button_execute.TabIndex = 2;
            this.button_execute.Text = "Execute";
            this.button_execute.UseVisualStyleBackColor = true;
            this.button_execute.Click += new System.EventHandler(this.button_execute_Click);
            // 
            // richTextBox_result
            // 
            this.richTextBox_result.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_result.BackColor = System.Drawing.Color.Black;
            this.richTextBox_result.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_result.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBox_result.ForeColor = System.Drawing.Color.LimeGreen;
            this.richTextBox_result.Location = new System.Drawing.Point(158, 134);
            this.richTextBox_result.Name = "richTextBox_result";
            this.richTextBox_result.Size = new System.Drawing.Size(586, 342);
            this.richTextBox_result.TabIndex = 3;
            this.richTextBox_result.Text = "";
            // 
            // label_result
            // 
            this.label_result.AutoSize = true;
            this.label_result.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_result.Location = new System.Drawing.Point(158, 115);
            this.label_result.Name = "label_result";
            this.label_result.Size = new System.Drawing.Size(52, 16);
            this.label_result.TabIndex = 4;
            this.label_result.Text = "Result";
            // 
            // listView_botname
            // 
            this.listView_botname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listView_botname.BackColor = System.Drawing.SystemColors.Control;
            this.listView_botname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView_botname.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView_botname.ContextMenuStrip = this.contextMenuStrip_botname;
            this.listView_botname.FullRowSelect = true;
            this.listView_botname.GridLines = true;
            listViewGroup1.Header = "For Bot(s)";
            listViewGroup1.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup1.Name = "listViewGroup_bots";
            this.listView_botname.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            this.listView_botname.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView_botname.HideSelection = false;
            this.listView_botname.Location = new System.Drawing.Point(12, 38);
            this.listView_botname.Name = "listView_botname";
            this.listView_botname.Size = new System.Drawing.Size(140, 438);
            this.listView_botname.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView_botname.TabIndex = 6;
            this.listView_botname.UseCompatibleStateImageBehavior = false;
            this.listView_botname.View = System.Windows.Forms.View.Details;
            this.listView_botname.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_botname_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 134;
            // 
            // contextMenuStrip_botname
            // 
            this.contextMenuStrip_botname.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.contextMenuStrip_botname.Name = "contextMenuStrip_botname";
            this.contextMenuStrip_botname.Size = new System.Drawing.Size(136, 48);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(135, 22);
            this.toolStripMenuItem1.Text = "Select All";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(135, 22);
            this.toolStripMenuItem2.Text = "Deselect All";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // textBox_search_in_res
            // 
            this.textBox_search_in_res.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_search_in_res.ForeColor = System.Drawing.Color.Gray;
            this.textBox_search_in_res.Location = new System.Drawing.Point(216, 114);
            this.textBox_search_in_res.Name = "textBox_search_in_res";
            this.textBox_search_in_res.Size = new System.Drawing.Size(147, 20);
            this.textBox_search_in_res.TabIndex = 8;
            this.textBox_search_in_res.Text = "Search...";
            this.textBox_search_in_res.Enter += new System.EventHandler(this.textBox_search_in_res_Enter);
            this.textBox_search_in_res.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_search_in_res_KeyUp);
            this.textBox_search_in_res.Leave += new System.EventHandler(this.textBox_search_in_res_Leave);
            // 
            // textBox_search_botname
            // 
            this.textBox_search_botname.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_search_botname.ForeColor = System.Drawing.Color.Gray;
            this.textBox_search_botname.Location = new System.Drawing.Point(12, 12);
            this.textBox_search_botname.Name = "textBox_search_botname";
            this.textBox_search_botname.Size = new System.Drawing.Size(140, 20);
            this.textBox_search_botname.TabIndex = 9;
            this.textBox_search_botname.Text = "Botname...";
            this.textBox_search_botname.Enter += new System.EventHandler(this.textBox_search_botname_Enter);
            this.textBox_search_botname.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_search_botname_KeyUp);
            this.textBox_search_botname.Leave += new System.EventHandler(this.textBox_search_botname_Leave);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 488);
            this.Controls.Add(this.textBox_search_botname);
            this.Controls.Add(this.textBox_search_in_res);
            this.Controls.Add(this.listView_botname);
            this.Controls.Add(this.label_result);
            this.Controls.Add(this.richTextBox_result);
            this.Controls.Add(this.button_execute);
            this.Controls.Add(this.groupBox_command);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "ASF WCF Client by Shmurdik";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox_command.ResumeLayout(false);
            this.groupBox_command.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_delay)).EndInit();
            this.contextMenuStrip_botname.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_command;
        private System.Windows.Forms.ComboBox comboBox_command;
        private System.Windows.Forms.Button button_execute;
        private System.Windows.Forms.RichTextBox richTextBox_result;
        private System.Windows.Forms.Label label_result;
        private System.Windows.Forms.ListView listView_botname;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_botname;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TextBox textBox_args;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.TextBox textBox_search_in_res;
        private System.Windows.Forms.NumericUpDown numericUpDown_delay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_search_botname;
    }
}

