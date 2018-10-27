using SMT.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMT.GUI
{
    public partial class UserInterface : UserControl 
    {
        /**
         * We Need: 
         * Label: URL GDoc
         * TextBox: URL GDoc
         * Button: Test Connection
         * Label: Static Member
         * ListBox: Static Member
         * Button: Update Static Member
         * Label: Drops To Track
         * ListBox: Possible Drops To Track
         * Label: Messages
         * TextBox: Display Messages
         * Label: Import Log
         * Textbox: Logfile
         * Button: Browse For Logs
         * Button: Import Log
         */

        private Label labelUrlGoogleDoc, labelStaticMembers, labelDropsToTrack, labelSelectLogFile, labelMessages;

        private TextBox textBoxUrlGoogleDoc, textBoxLogFile, textBoxMessages;

        private Button buttonTestConnection, buttonUpdateStaticMembers, buttonBrowseLogFile, buttonImportLogfile;

        private ListBox listBoxStaticMembers;

        private CheckedListBox listBoxPossibleDropsToTrack;

        private TableLayoutPanel tableLayoutPanel;

        private FlowLayoutPanel flowLayoutPanel;

        private List<CharacterNameDTO> staticMembers;

        private List<string> trackableDrops;

        public List<string> trackedDrops { get; set; }

        public event OnTrackedDropsChangeHandler TrackedDropsChanged;

        public delegate void OnTrackedDropsChangeHandler();
        

        public UserInterface(List<CharacterNameDTO> staticMembers, List<string> trackableDrops)
        {
            this.staticMembers = staticMembers;
            this.trackableDrops = trackableDrops;

            //ini
            this.trackedDrops = new List<string>();

            //labels
            this.labelUrlGoogleDoc = new Label();
            this.labelStaticMembers = new Label();
            this.labelDropsToTrack = new Label();
            this.labelMessages = new Label();
            this.labelSelectLogFile = new Label();

            //textboxes
            this.textBoxUrlGoogleDoc = new TextBox();
            this.textBoxMessages = new TextBox();
            this.textBoxLogFile = new TextBox();

            //Buttons
            this.buttonTestConnection = new Button();
            this.buttonUpdateStaticMembers = new Button();
            this.buttonBrowseLogFile = new Button();
            this.buttonImportLogfile = new Button();

            //listBoxes
            this.listBoxStaticMembers = new ListBox();
            this.listBoxPossibleDropsToTrack = new CheckedListBox();

            //LayoutPanels
            this.tableLayoutPanel = new TableLayoutPanel();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            
        }

        
       
        // Event Handler        

        private void ListBoxPossibleDropsToTrack_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //cast here just so we don't have to do it again and again when updating the actual data-source
            var item = (sender as CheckedListBox).Items[e.Index] as string;

            if (e.NewValue == CheckState.Checked)
            {
                if (!trackedDrops.Contains(item))
                {
                    trackedDrops.Add(item);
                }
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                if (trackedDrops.Contains(item))
                {
                    trackedDrops.Remove(item);
                }
            }

            //Raise an event to share the current trackedDrops.
            TrackedDropsChanged();
        }

        private void ButtonUpdateStaticMembers_Click(object sender, EventArgs e)
        {
            /**
             * This Method does: 
             * 1) Flush the staticMembers List
             * 2) Set a wait-cursor
             * 3) call the method that checks for static Members
             * 4) awaits it's result
             * 5) update the listBox with the newly received List
             */

            DisplayMessage(true, "Update Static Memebers Clicked");
        }

        private void ButtonTestConnection_Click(object sender, EventArgs e)
        {
            /**
             * This Method does: 
             * 1) Call the Method that checks the google-doc-connection
             */
            DisplayMessage(true, "Test Connection Clicked!");
        }

        private void ButtonImportLogfile_Click(object sender, EventArgs e)
        {
            /**
             * This Method does: 
             * 1) Open the file specified by browse
             * 2) call the method that parses the log for drops
             */
            DisplayMessage(true, "Import Logfile Clicked!");
        }

        private void ButtonBrowseLogFile_Click(object sender, EventArgs e)
        {
            /**
             * This Method does: 
             * 1) Open a FileExplorer
             * 2) Post the result into the Textbox
             */
            DisplayMessage(true, "Browse Logfile Clicked!");

            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Title = "Browse for Log file";
            fileDialog.Filter = "Log files | *.log";

            fileDialog.ShowDialog();

            textBoxLogFile.Text = fileDialog.FileName;
           

        }

        private void TextBoxMessages_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            textBox.SelectionStart = textBox.Text.Length;
            textBox.ScrollToCaret();
        }

        //Helper Methods

        //Method that just adds controls onto other controls. Helpfull so we dont have to repeat the same Code X times since Win Forms wants controls nested in controls and so on

        private void AddControlsToControl(Control parent, Control[] children)
        {
            parent.Controls.AddRange(children);
        }


        //This method just posts a Message into the messagebox. I just found it to be easier to have one point in the code where this box can accessed. Upside, can also be access from outside this class. 
        public void DisplayMessage(bool appendText, string text)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (appendText)
                stringBuilder.AppendLine(textBoxMessages.Text);

            stringBuilder.Append(text);

            textBoxMessages.Text = stringBuilder.ToString();
        }


        // Initialize Methods for GUI

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        public void InitializeComponent()
        {

            this.tableLayoutPanel.SuspendLayout();
            this.flowLayoutPanel.SuspendLayout();
            this.SuspendLayout();

            InitializeLabels();

            InitializeTextBoxes();

            InitializeButtons();

            InitializeListBoxes();

            InitializeLayoutContainers();

            InitializeUserInterface();

            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.flowLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void InitializeLabels()
        {
            // 
            // labelUrlGoogleDoc
            // 
            this.labelUrlGoogleDoc.Location = new Point(3, 20);
            this.labelUrlGoogleDoc.Margin = new Padding(3, 20, 3, 0);
            this.labelUrlGoogleDoc.Name = "labelUrlGoogleDoc";
            this.labelUrlGoogleDoc.Size = new Size(100, 23);
            this.labelUrlGoogleDoc.TabIndex = 0;
            this.labelUrlGoogleDoc.Text = "Google Doc Url";
            this.labelUrlGoogleDoc.TextAlign = ContentAlignment.MiddleCenter;
            
            // 
            // labelStaticMembers
            // 
            this.labelStaticMembers.Location = new Point(3, 63);
            this.labelStaticMembers.Margin = new Padding(3, 20, 3, 0);
            this.labelStaticMembers.Name = "labelStaticMembers";
            this.labelStaticMembers.Size = new Size(100, 23);
            this.labelStaticMembers.TabIndex = 1;
            this.labelStaticMembers.Text = "Static Members";
            this.labelStaticMembers.TextAlign = ContentAlignment.MiddleCenter;
            
            // 
            // labelDropsToTrack
            // 
            this.labelDropsToTrack.Location = new Point(3, 220);
            this.labelDropsToTrack.Margin = new Padding(3, 20, 3, 0);
            this.labelDropsToTrack.Name = "labelDropsToTrack";
            this.labelDropsToTrack.Size = new Size(100, 23);
            this.labelDropsToTrack.TabIndex = 2;
            this.labelDropsToTrack.Text = "Drops to track";
            this.labelDropsToTrack.TextAlign = ContentAlignment.MiddleCenter;
            
            // 
            // labelMessages
            // 
            this.labelMessages.Location = new Point(3, 506);
            this.labelMessages.Margin = new Padding(3, 20, 10, 0);
            this.labelMessages.Name = "labelMessages";
            this.labelMessages.Size = new Size(73, 23);
            this.labelMessages.TabIndex = 3;
            this.labelMessages.Text = "Messages";
            this.labelMessages.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // labelSelectLogFile
            // 
            this.labelSelectLogFile.Location = new Point(15, 423);
            this.labelSelectLogFile.Margin = new Padding(15, 20, 3, 0);
            this.labelSelectLogFile.Name = "labelSelectLogFile";
            this.labelSelectLogFile.Size = new Size(100, 23);
            this.labelSelectLogFile.TabIndex = 10;
            this.labelSelectLogFile.Text = "Import Log File";
        }

        private void InitializeTextBoxes()
        {
            // 
            // textBoxUrlGoogleDoc
            // 
            this.textBoxUrlGoogleDoc.Location = new Point(121, 20);
            this.textBoxUrlGoogleDoc.Margin = new Padding(3, 20, 3, 3);
            this.textBoxUrlGoogleDoc.Name = "textBoxUrlGoogleDoc";
            this.textBoxUrlGoogleDoc.Size = new Size(443, 20);
            this.textBoxUrlGoogleDoc.TabIndex = 4;
            this.textBoxUrlGoogleDoc.Text = "Google Doc";
            
            // 
            // textBoxMessages
            // 
            this.textBoxMessages.Location = new Point(15, 532);
            this.textBoxMessages.Margin = new Padding(15, 3, 15, 3);
            this.textBoxMessages.Multiline = true;
            this.textBoxMessages.Name = "textBoxMessages";
            this.textBoxMessages.ReadOnly = true;
            this.textBoxMessages.Size = new Size(770, 111);
            this.textBoxMessages.TabIndex = 5;
            this.textBoxMessages.Text = "Messages TB";
            this.textBoxMessages.ScrollBars = ScrollBars.Both;
            this.textBoxMessages.TextChanged += TextBoxMessages_TextChanged;

            // 
            // textBoxLogFile
            // 
            this.textBoxLogFile.Location = new Point(15, 449);
            this.textBoxLogFile.Margin = new Padding(15, 3, 3, 3);
            this.textBoxLogFile.Name = "textBoxLogFile";
            this.textBoxLogFile.ReadOnly = true;
            this.textBoxLogFile.Size = new Size(549, 20);
            this.textBoxLogFile.TabIndex = 11;
        }

        private void InitializeButtons()
        {
            // 
            // buttonTestConnection
            // 
            this.buttonTestConnection.Location = new Point(577, 20);
            this.buttonTestConnection.Margin = new Padding(10, 20, 15, 3);
            this.buttonTestConnection.Name = "buttonTestConnection";
            this.buttonTestConnection.Size = new Size(205, 20);
            this.buttonTestConnection.TabIndex = 6;
            this.buttonTestConnection.Text = "Test Connection";

            // 
            // buttonUpdateStaticMembers
            // 
            this.buttonUpdateStaticMembers.Location = new Point(577, 89);
            this.buttonUpdateStaticMembers.Margin = new Padding(10, 3, 15, 3);
            this.buttonUpdateStaticMembers.Name = "buttonUpdateStaticMembers";
            this.buttonUpdateStaticMembers.Size = new Size(205, 23);
            this.buttonUpdateStaticMembers.TabIndex = 7;
            this.buttonUpdateStaticMembers.Text = "Update Static Members";

            // 
            // buttonBrowseLogFile
            // 
            this.buttonBrowseLogFile.Location = new Point(3, 0);
            this.buttonBrowseLogFile.Margin = new Padding(3, 0, 3, 3);
            this.buttonBrowseLogFile.Name = "buttonBrowseLogFile";
            this.buttonBrowseLogFile.Size = new Size(75, 20);
            this.buttonBrowseLogFile.TabIndex = 0;
            this.buttonBrowseLogFile.Text = "Browse";

            // 
            // buttonImportLogfile
            // 
            this.buttonImportLogfile.Location = new Point(84, 0);
            this.buttonImportLogfile.Margin = new Padding(3, 0, 3, 3);
            this.buttonImportLogfile.Name = "buttonImportLogfile";
            this.buttonImportLogfile.Size = new Size(83, 20);
            this.buttonImportLogfile.TabIndex = 1;
            this.buttonImportLogfile.Text = "Import Log";

            // Click-Events

            this.buttonBrowseLogFile.Click += ButtonBrowseLogFile_Click;
            this.buttonImportLogfile.Click += ButtonImportLogfile_Click;
            this.buttonTestConnection.Click += ButtonTestConnection_Click;
            this.buttonUpdateStaticMembers.Click += ButtonUpdateStaticMembers_Click;
        }

        
        private void InitializeListBoxes()
        {
            // 
            // listBoxStaticMembers
            // 
            this.listBoxStaticMembers.Location = new Point(15, 89);
            this.listBoxStaticMembers.Margin = new Padding(15, 3, 3, 3);
            this.listBoxStaticMembers.Name = "listBoxStaticMembers";
            this.listBoxStaticMembers.Size = new Size(549, 108);
            this.listBoxStaticMembers.TabIndex = 8;
            this.listBoxStaticMembers.DataSource = staticMembers;

            // 
            // listBoxPossibleDropsToTrack
            //
            this.listBoxPossibleDropsToTrack.Location = new Point(15, 246);
            this.listBoxPossibleDropsToTrack.Margin = new Padding(15, 3, 3, 3);
            this.listBoxPossibleDropsToTrack.Name = "listBoxPossibleDropsToTrack";
            this.listBoxPossibleDropsToTrack.Size = new Size(549, 154);
            this.listBoxPossibleDropsToTrack.TabIndex = 9;
            this.listBoxPossibleDropsToTrack.DataSource = trackableDrops;
            this.listBoxPossibleDropsToTrack.ItemCheck += ListBoxPossibleDropsToTrack_ItemCheck;
        }


        private void InitializeLayoutContainers()
        {
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.Dock = DockStyle.Fill;
            this.tableLayoutPanel.Location = new Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";

            this.tableLayoutPanel.Size = new Size(800, 700);
            this.tableLayoutPanel.TabIndex = 10;


            // Columns
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());

            //Rows
            this.tableLayoutPanel.RowCount = 11;
            this.tableLayoutPanel.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 0F));

            // I've decided against using the Method AddControlsToControls here since A TableLayoutPanel is "special". I have to declare which row / col each element uses anyway, so if i add the controls in that method and then have 
            // for each control within the table a new line here for row / col doesn't really save any space. So might as well make it all at once. 

            //Adding Controls here
            this.tableLayoutPanel.Controls.Add(labelStaticMembers, 0, 1);
            this.tableLayoutPanel.Controls.Add(labelDropsToTrack, 0, 3);
            this.tableLayoutPanel.Controls.Add(labelMessages, 0, 8);
            this.tableLayoutPanel.Controls.Add(textBoxUrlGoogleDoc, 1, 0);
            this.tableLayoutPanel.Controls.Add(buttonTestConnection, 3, 0);
            this.tableLayoutPanel.Controls.Add(buttonUpdateStaticMembers, 3, 2);
            this.tableLayoutPanel.Controls.Add(listBoxStaticMembers, 0, 2);
            this.tableLayoutPanel.Controls.Add(listBoxPossibleDropsToTrack, 0, 4);
            this.tableLayoutPanel.Controls.Add(labelUrlGoogleDoc, 0, 0);
            this.tableLayoutPanel.Controls.Add(labelSelectLogFile, 0, 5);
            this.tableLayoutPanel.Controls.Add(textBoxLogFile, 0, 6);
            this.tableLayoutPanel.Controls.Add(textBoxMessages, 2, 8);
            this.tableLayoutPanel.Controls.Add(flowLayoutPanel, 2, 6);

            //Layout specifica
            this.tableLayoutPanel.SetColumnSpan(this.textBoxLogFile, 2);
            this.tableLayoutPanel.SetColumnSpan(this.listBoxPossibleDropsToTrack, 2);
            this.tableLayoutPanel.SetColumnSpan(this.textBoxMessages, 3);
            this.tableLayoutPanel.SetColumnSpan(this.listBoxStaticMembers, 2);

            // 
            // flowLayoutPanel 
            // 
            AddControlsToControl(flowLayoutPanel, new Control[] { buttonBrowseLogFile, buttonImportLogfile });
            this.flowLayoutPanel.Location = new Point(582, 449);
            this.flowLayoutPanel.Margin = new Padding(15, 3, 3, 3);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new Size(215, 34);
            this.flowLayoutPanel.TabIndex = 12;
        }

        private void InitializeUserInterface()
        {
            AddControlsToControl(this, new Control[] { tableLayoutPanel });
            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.Name = "UserInterface";
            this.Size = new Size(800, 700);
        }


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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

    }
}