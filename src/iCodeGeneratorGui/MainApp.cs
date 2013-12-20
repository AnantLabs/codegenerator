using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using iCodeGenerator.DatabaseNavigator;
using iCodeGenerator.DatabaseStructure;
using iCodeGenerator.DataTypeConverter;
using iCodeGenerator.Generator;
using WeifenLuo.WinFormsUI.Docking;

namespace iCodeGenerator.iCodeGeneratorGui
{
    public partial class MainApp :Form
    {
        
        public MainApp()
        {
            InitializeComponent();
            InitializeControls();
        }

        DatabaseNavigationForm _dnf;
        PropertiesForm _pf;
        CustomValuesForm _cvf;
        SnippetsForm _sf;
        DocumentForm _df;
        ResultForm _rf;

        private void InitializeControls()
        {
            _dnf = new DatabaseNavigationForm();
            _dnf.Show(dockPanel, DockState.DockLeft);
            _dnf.Icon = Icon.ExtractAssociatedIcon(@"idb.ico");
            _dnf.TableSelected += DnfTableSelected;
            _dnf.DatabaseSelected += DnfDatabaseSelected;
            _dnf.ColumnSelected += DnfColumnSelected;

            _sf = new SnippetsForm();
            _sf.SnippetSelected += SfSnippetSelected;
            _sf.Show(dockPanel, DockState.DockLeftAutoHide);
            _sf.Icon = Icon.ExtractAssociatedIcon(@"isnippet.ico");

            _df = new DocumentForm();
            _df.Text = "Template";
            _df.Show(dockPanel, DockState.Document);
            _df.Icon = Icon.ExtractAssociatedIcon(@"itemplate.ico");

            _rf = new ResultForm();
            _rf.Text = "Results";
            _rf.Show(dockPanel, DockState.Document);
            _rf.Icon = Icon.ExtractAssociatedIcon(@"iresult.ico");

            _pf = new PropertiesForm();
            _pf.Show(dockPanel, DockState.DockRight);
            _pf.Icon = Icon.ExtractAssociatedIcon(@"igen.ico");

            _cvf = new CustomValuesForm();
            _cvf.Show(dockPanel, DockState.DockRight);
            _cvf.Icon = Icon.ExtractAssociatedIcon(@"icustom.ico");
        }

        void SfSnippetSelected(object sender, SnippetEventArgs args)
        {
            _df.ContentText = _df.ContentText.Insert(_df.SelectionStart,new SnippetsHelper().Snippets[args.Snippet].ToString());
        }

		private static Table _selectedTable;
        private void DnfColumnSelected(object sender, ColumnEventArgs args)
        {
			_selectedTable = args.Column.ParentTable;
			_pf.SelectedObject = args.Column;
        }

        private void DnfDatabaseSelected(object sender, DatabaseEventArgs args)
        {
            _pf.SelectedObject = args.Database;
        }

        private void DnfTableSelected(object sender, TableEventArgs args)
        {
			_selectedTable = args.Table;
			_pf.SelectedObject = args.Table;
        }

        private void GenerateCode()
		{
			try
			{
				if (_selectedTable == null) return;
				var cgenerator = new Client {CustomValues = _cvf.CustomValues};
			    _rf.ContentText = cgenerator.Parse(_selectedTable, _df.ContentText);
			}
			catch (DataTypeManagerException ex)
			{
				MessageBox.Show(this, ex.Message, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

        [STAThread]
		public static void Main()
		{
			Application.Run(new MainApp());
		}

        private void DatabaseConnectClick(object sender, EventArgs e)
        {
		    _dnf.Connect();	
        }

        private void DatabaseDisconnectClick(object sender, EventArgs e)
        {
            _dnf.Disconnect();
        }

        private void EditConfigDatabaseClick(object sender, EventArgs e)
        {
			_dnf.ShowEditConnectionString();
        }

        private void GenerateCodeClick(object sender, EventArgs e)
        {
            GenerateCode();
        }

        private void GenerateFilesClick(object sender, EventArgs e)
        {
            GenerateFiles();
        }

		DirectorySelectionWindow _selectionWindow;
		private void SelectTemplatesDirectory()
		{
		    if (_selectionWindow == null)
		    {
    		    _selectionWindow = new DirectorySelectionWindow();
                _selectionWindow.InputFolderSelected += SelectionWindowInputFolderSelected;
                _selectionWindow.OutputFolderSelected += SelectionWindowOutputFolderSelected;
		    }
			_selectionWindow.ShowDialog(this);
		}

        static void SelectionWindowOutputFolderSelected(object sender, FolderEventArgs args)
        {
            _OutputTemplateFolder = args.FolderName;
        }

        static void SelectionWindowInputFolderSelected(object sender, FolderEventArgs args)
        {
            _InputTemplateFolder = args.FolderName;
        }

		private static string _InputTemplateFolder = String.Empty;
		private static string _OutputTemplateFolder = String.Empty;

		private void GenerateFiles()
		{
			if (_selectedTable == null) return;
			if (IsValidFolder(_InputTemplateFolder) && IsValidFolder(_OutputTemplateFolder))
			{
				try
				{
					var generator = new FileGenerator();
					generator.OnComplete += FileGeneratorCompleted;
				    generator.CustomValue = _cvf.CustomValues;
					generator.Generate(_selectedTable, _InputTemplateFolder, _OutputTemplateFolder);
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message);
           
				}
			}
			else
			{
				SelectTemplatesDirectory();
			}
		}

        private void FileGeneratorCompleted(object sender, EventArgs e)
		{
			//MessageBox.Show("File Generation Completed");
            if (IsValidFolder(_OutputTemplateFolder))
			{
				Process.Start(_OutputTemplateFolder);
			}
		}

		private bool IsValidFolder(string folder)
		{
			return folder.Length > 0 && Directory.Exists(folder);
		}
    }
}
