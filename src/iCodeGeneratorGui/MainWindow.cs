using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using iCodeGenerator.DatabaseNavigator;
using iCodeGenerator.DatabaseStructure;
using iCodeGenerator.DataTypeConverter;
using iCodeGenerator.Generator;
using iCodeGenerator.Updater;
using TD.SandBar;
using TD.SandDock;
using ToolBar=TD.SandBar.ToolBar;

namespace iCodeGenerator.iCodeGeneratorGui
{
	public class MainWindow : Form
	{
		#region Constants

		public const string Version = "0.1.0";

		#endregion

		#region Attributes

		private DockContainer leftSandDock;
		private DockContainer rightSandDock;
		private DockContainer bottomSandDock;
		private DockContainer topSandDock;
		private ToolBarContainer leftSandBarDock;
		private ToolBarContainer rightSandBarDock;
		private ToolBarContainer bottomSandBarDock;
		private ToolBarContainer topSandBarDock;
		private MenuBarItem menuBarItem1;
		private MenuBarItem menuBarItem2;
		private MenuBarItem menuBarItem3;
		private MenuBarItem menuBarItem5;
		private StatusBar uiStatusBar;
		private DockControl uiNavigatorDock;
		private MenuBar uiMenuBar;
		private ToolBar uiToolBar;
		private DocumentContainer uiTemplateContainer;
		private SandDockManager uiSandDockManager;
		private SandBarManager uiSandBarManager;
		private DockControl uiTemplateDock;
		private RichTextBox uiTemplateTextBox;
		private MenuButtonItem uiExitMenuButton;
		private NavigatorControl uiNavigatorControl;
		private ButtonItem uiGenerateButton;
		private Container components = null;
		private DockControl uiGenerateCodeDock;
		private RichTextBox uiGeneratedCodeTextBox;
		private MenuButtonItem uiCutButton;
		private MenuButtonItem uiCopyButton;
		private MenuButtonItem uiPasteButton;
		private MenuButtonItem uiUndoButton;
		private MenuButtonItem uiRedoButton;
		private MenuButtonItem uiViewNavigatorButton;
		private MenuButtonItem uiViewPropertiesButton;
		private MenuButtonItem uiAboutButton;
		private MenuButtonItem uiOpenTemplateButton;
		private MenuButtonItem uiSaveResultButton;
		private MenuButtonItem uiSaveTemplateButton;
		private MenuButtonItem uiSaveAsTemplateButton;
		private OpenFileDialog uiOpenTemplateDialog;
		private SaveFileDialog uiSaveDialog;
		private MenuBarItem menuBarItem6;
		private MenuButtonItem uiGenerateCodeButton;
		private Panel uiPropertiesPanel;
		private DockControl uiPropertiesDock;
		private PropertyGrid uiPropertyEditor;
		private ButtonItem uiMergeButton;
		private static Table selectedTable = null;
		private OpenFileDialog uiOpenMergeDialog;
		private DataGrid uiCustomValuesDataGrid;
		private DockControl uiCustomValuesDock;
		private DataGridTableStyle uiCustomValuesGridStyle;
		private string _TempFilename;
		private DataGridTextBoxColumn uiName;
		private DataGridTextBoxColumn uiValue;
		private static string _TemplateFilename;
		private MenuButtonItem uiViewCustomValuesButton;

		private ButtonItem uiFileGenerator;
		private MenuButtonItem uiFileGeneratorConfigButton;
		private MenuButtonItem uiFileGenerateButton;
		private static string _InputTemplateFolder = String.Empty;
		private static string _OutputTemplateFolder = String.Empty;
		private MenuButtonItem uiOpenOutputFolder;
		private MenuButtonItem uiOpenConnectionString;
		private MenuButtonItem uiConnect;
		private DockControl uiSnippets;
		private MenuButtonItem uiDisconnect;

		#endregion

		#region Properties

		public static string InputTemplateFolder
		{
			get { return _InputTemplateFolder; }
			set { _InputTemplateFolder = value; }
		}

		public static string OutputTemplateFolder
		{
			get { return _OutputTemplateFolder; }
			set { _OutputTemplateFolder = value; }
		}

		#endregion

		public MainWindow()
		{
			UpdateCheckerThread();
			InitializeComponent();
			InitializeCustomValuesDataGrid();
			LoadSnippets();
		}

		private SnippetsHelper _SnippetsHelper;
		private void LoadSnippets()
		{
			_SnippetsHelper = new SnippetsHelper();
			foreach (string key in _SnippetsHelper.Snippets.Keys)
			{
				AddSnippetButton(key);
			}
		}

		private void AddSnippetButton(string s)
		{
			Button button = new Button();
			button.Text = s;
			button.Dock = DockStyle.Top;
			button.FlatStyle = FlatStyle.Popup;
			button.Font = new Font("Microsoft Sans Serif", 
				7.2567F, 
				FontStyle.Regular, 
				GraphicsUnit.Point, ((Byte) (0)));
			button.Click += new EventHandler(button_Click);
			uiSnippets.Controls.Add(button);
		}

		private void button_Click(object sender, EventArgs e)
		{
			Button b = (Button) sender;
			RichTextBox tbox = uiTemplateTextBox;
			tbox.Text = tbox.Text.Insert(tbox.SelectionStart, _SnippetsHelper.Snippets[b.Text].ToString());
		}

		private void UpdateCheckerThread()
		{
			var thread = new Thread(CheckUpdates) {IsBackground = true};
		    thread.Start();
		}

		private void CheckUpdates()
		{
			var checker = new UpdateChecker();
			if (checker.LoadConfiguration("http://codegenerator.sourceforge.net/iCodeGenerator.php?id=" + Version))
			{
				if (checker.Version != Version)
				{
					UpdatesWindow window = new UpdatesWindow();
					window.Name = "Update Notice";
					window.Url = checker.ApplicationUrl;
					window.Notice = checker.LatestChanges;
					window.NewVersion = checker.Version;
					window.CurrentVersion = Version;
					window.ShowDialog();
				}
			}
		}

		private static string _CustomValuesFilename = AppDomain.CurrentDomain.BaseDirectory + "CustomValues.xml";
		private DataSet _CustomValues;

		private void InitializeCustomValuesDataGrid()
		{
			_CustomValues = new DataSet();
			DataTable dt = new DataTable();
			_CustomValues.Tables.Add(dt);
			_CustomValues.Tables[0].Columns.Add("Name");
			_CustomValues.Tables[0].Columns.Add("Value");
			if (File.Exists(_CustomValuesFilename))
			{
				_CustomValues.ReadXml(_CustomValuesFilename);
			}
			uiCustomValuesDataGrid.DataSource = _CustomValues.Tables[0];
		}

		[STAThread]
		public static void Main()
		{
			Application.Run(new MainWindow());
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.uiStatusBar = new System.Windows.Forms.StatusBar();
            this.uiSandDockManager = new TD.SandDock.SandDockManager();
            this.leftSandDock = new TD.SandDock.DockContainer();
            this.uiNavigatorDock = new TD.SandDock.DockControl();
            this.uiNavigatorControl = new iCodeGenerator.DatabaseNavigator.NavigatorControl();
            this.uiSnippets = new TD.SandDock.DockControl();
            this.uiPropertiesDock = new TD.SandDock.DockControl();
            this.uiPropertiesPanel = new System.Windows.Forms.Panel();
            this.uiPropertyEditor = new System.Windows.Forms.PropertyGrid();
            this.uiCustomValuesDock = new TD.SandDock.DockControl();
            this.uiCustomValuesDataGrid = new System.Windows.Forms.DataGrid();
            this.uiCustomValuesGridStyle = new System.Windows.Forms.DataGridTableStyle();
            this.uiName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.uiValue = new System.Windows.Forms.DataGridTextBoxColumn();
            this.rightSandDock = new TD.SandDock.DockContainer();
            this.bottomSandDock = new TD.SandDock.DockContainer();
            this.topSandDock = new TD.SandDock.DockContainer();
            this.uiSandBarManager = new TD.SandBar.SandBarManager();
            this.bottomSandBarDock = new TD.SandBar.ToolBarContainer();
            this.leftSandBarDock = new TD.SandBar.ToolBarContainer();
            this.rightSandBarDock = new TD.SandBar.ToolBarContainer();
            this.topSandBarDock = new TD.SandBar.ToolBarContainer();
            this.uiMenuBar = new TD.SandBar.MenuBar();
            this.menuBarItem1 = new TD.SandBar.MenuBarItem();
            this.uiConnect = new TD.SandBar.MenuButtonItem();
            this.uiDisconnect = new TD.SandBar.MenuButtonItem();
            this.uiOpenConnectionString = new TD.SandBar.MenuButtonItem();
            this.uiOpenTemplateButton = new TD.SandBar.MenuButtonItem();
            this.uiSaveTemplateButton = new TD.SandBar.MenuButtonItem();
            this.uiSaveAsTemplateButton = new TD.SandBar.MenuButtonItem();
            this.uiSaveResultButton = new TD.SandBar.MenuButtonItem();
            this.uiExitMenuButton = new TD.SandBar.MenuButtonItem();
            this.menuBarItem2 = new TD.SandBar.MenuBarItem();
            this.uiUndoButton = new TD.SandBar.MenuButtonItem();
            this.uiRedoButton = new TD.SandBar.MenuButtonItem();
            this.uiCutButton = new TD.SandBar.MenuButtonItem();
            this.uiCopyButton = new TD.SandBar.MenuButtonItem();
            this.uiPasteButton = new TD.SandBar.MenuButtonItem();
            this.menuBarItem3 = new TD.SandBar.MenuBarItem();
            this.uiViewNavigatorButton = new TD.SandBar.MenuButtonItem();
            this.uiViewPropertiesButton = new TD.SandBar.MenuButtonItem();
            this.uiViewCustomValuesButton = new TD.SandBar.MenuButtonItem();
            this.menuBarItem6 = new TD.SandBar.MenuBarItem();
            this.uiGenerateCodeButton = new TD.SandBar.MenuButtonItem();
            this.uiFileGeneratorConfigButton = new TD.SandBar.MenuButtonItem();
            this.uiFileGenerateButton = new TD.SandBar.MenuButtonItem();
            this.uiOpenOutputFolder = new TD.SandBar.MenuButtonItem();
            this.menuBarItem5 = new TD.SandBar.MenuBarItem();
            this.uiAboutButton = new TD.SandBar.MenuButtonItem();
            this.uiToolBar = new TD.SandBar.ToolBar();
            this.uiGenerateButton = new TD.SandBar.ButtonItem();
            this.uiFileGenerator = new TD.SandBar.ButtonItem();
            this.uiMergeButton = new TD.SandBar.ButtonItem();
            this.uiTemplateContainer = new TD.SandDock.DocumentContainer();
            this.uiTemplateDock = new TD.SandDock.DockControl();
            this.uiTemplateTextBox = new System.Windows.Forms.RichTextBox();
            this.uiGenerateCodeDock = new TD.SandDock.DockControl();
            this.uiGeneratedCodeTextBox = new System.Windows.Forms.RichTextBox();
            this.uiOpenTemplateDialog = new System.Windows.Forms.OpenFileDialog();
            this.uiSaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.uiOpenMergeDialog = new System.Windows.Forms.OpenFileDialog();
            this.leftSandDock.SuspendLayout();
            this.uiNavigatorDock.SuspendLayout();
            this.uiPropertiesDock.SuspendLayout();
            this.uiPropertiesPanel.SuspendLayout();
            this.uiCustomValuesDock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiCustomValuesDataGrid)).BeginInit();
            this.rightSandDock.SuspendLayout();
            this.topSandBarDock.SuspendLayout();
            this.uiTemplateContainer.SuspendLayout();
            this.uiTemplateDock.SuspendLayout();
            this.uiGenerateCodeDock.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiStatusBar
            // 
            this.uiStatusBar.Location = new System.Drawing.Point(200, 544);
            this.uiStatusBar.Name = "uiStatusBar";
            this.uiStatusBar.Size = new System.Drawing.Size(392, 22);
            this.uiStatusBar.TabIndex = 0;
            // 
            // uiSandDockManager
            // 
            this.uiSandDockManager.DockingManager = TD.SandDock.DockingManager.Whidbey;
            this.uiSandDockManager.OwnerForm = this;
            // 
            // leftSandDock
            // 
            this.leftSandDock.Controls.Add(this.uiNavigatorDock);
            this.leftSandDock.Controls.Add(this.uiSnippets);
            this.leftSandDock.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftSandDock.Guid = new System.Guid("4447f9b6-bb46-4444-9654-653e0fac0c75");
            this.leftSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400, System.Windows.Forms.Orientation.Horizontal, new TD.SandDock.LayoutSystemBase[] {
            ((TD.SandDock.LayoutSystemBase)(new TD.SandDock.ControlLayoutSystem(196, 516, new TD.SandDock.DockControl[] {
                        this.uiNavigatorDock,
                        this.uiSnippets}, this.uiSnippets)))});
            this.leftSandDock.Location = new System.Drawing.Point(0, 50);
            this.leftSandDock.Manager = this.uiSandDockManager;
            this.leftSandDock.Name = "leftSandDock";
            this.leftSandDock.Size = new System.Drawing.Size(200, 516);
            this.leftSandDock.TabIndex = 2;
            // 
            // uiNavigatorDock
            // 
            this.uiNavigatorDock.Controls.Add(this.uiNavigatorControl);
            this.uiNavigatorDock.Guid = new System.Guid("be35efbb-904d-42cd-ab9a-e897e77040b6");
            this.uiNavigatorDock.Location = new System.Drawing.Point(0, 18);
            this.uiNavigatorDock.Name = "uiNavigatorDock";
            this.uiNavigatorDock.Size = new System.Drawing.Size(196, 475);
            this.uiNavigatorDock.TabIndex = 0;
            this.uiNavigatorDock.Text = "Navigator";
            // 
            // uiNavigatorControl
            // 
            this.uiNavigatorControl.ConnectionString = "";
            this.uiNavigatorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiNavigatorControl.Location = new System.Drawing.Point(0, 0);
            this.uiNavigatorControl.Name = "uiNavigatorControl";
            this.uiNavigatorControl.ProviderType = iCodeGenerator.GenericDataAccess.DataProviderType.SqlClient;
            this.uiNavigatorControl.Size = new System.Drawing.Size(196, 475);
            this.uiNavigatorControl.TabIndex = 0;
            this.uiNavigatorControl.TableSelect += new iCodeGenerator.DatabaseNavigator.NavigatorControl.TableEventHandler(this.uiNavigatorControl_TableSelect);
            this.uiNavigatorControl.DatabaseSelect += new iCodeGenerator.DatabaseNavigator.NavigatorControl.DatabaseEventHandler(this.uiNavigatorControl_DatabaseSelect);
            //this.uiNavigatorControl.ColumnSelect += new iCodeGenerator.DatabaseNavigator.NavigatorControl.ColumnEventHandler(this.uiNavigatorControl_ColumnSelect);
            //this.uiNavigatorControl.ColumnShowProperties += new iCodeGenerator.DatabaseNavigator.NavigatorControl.ColumnEventHandler(this.uiNavigatorControl_ColumnShowProperties);
            // 
            // uiSnippets
            // 
            this.uiSnippets.Guid = new System.Guid("a7b3b4f4-8ba9-4862-9b34-5e2c54880aec");
            this.uiSnippets.Location = new System.Drawing.Point(0, 18);
            this.uiSnippets.Name = "uiSnippets";
            this.uiSnippets.Size = new System.Drawing.Size(196, 475);
            this.uiSnippets.TabIndex = 1;
            this.uiSnippets.Text = "Snippets";
            this.uiSnippets.Closing += new System.ComponentModel.CancelEventHandler(this.dockControl1_Closing);
            // 
            // uiPropertiesDock
            // 
            this.uiPropertiesDock.Controls.Add(this.uiPropertiesPanel);
            this.uiPropertiesDock.Guid = new System.Guid("4f16c3df-5375-4159-85ae-522ff2daa5b4");
            this.uiPropertiesDock.Location = new System.Drawing.Point(4, 18);
            this.uiPropertiesDock.Name = "uiPropertiesDock";
            this.uiPropertiesDock.Size = new System.Drawing.Size(196, 215);
            this.uiPropertiesDock.TabIndex = 1;
            this.uiPropertiesDock.Text = "Properties";
            // 
            // uiPropertiesPanel
            // 
            this.uiPropertiesPanel.Controls.Add(this.uiPropertyEditor);
            this.uiPropertiesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPropertiesPanel.Location = new System.Drawing.Point(0, 0);
            this.uiPropertiesPanel.Name = "uiPropertiesPanel";
            this.uiPropertiesPanel.Size = new System.Drawing.Size(196, 215);
            this.uiPropertiesPanel.TabIndex = 0;
            // 
            // uiPropertyEditor
            // 
            this.uiPropertyEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPropertyEditor.HelpVisible = false;
            this.uiPropertyEditor.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.uiPropertyEditor.Location = new System.Drawing.Point(0, 0);
            this.uiPropertyEditor.Name = "uiPropertyEditor";
            this.uiPropertyEditor.Size = new System.Drawing.Size(196, 215);
            this.uiPropertyEditor.TabIndex = 0;
            this.uiPropertyEditor.ToolbarVisible = false;
            // 
            // uiCustomValuesDock
            // 
            this.uiCustomValuesDock.Controls.Add(this.uiCustomValuesDataGrid);
            this.uiCustomValuesDock.Guid = new System.Guid("efe8850a-621c-48c4-a603-1a963b397082");
            this.uiCustomValuesDock.Location = new System.Drawing.Point(4, 278);
            this.uiCustomValuesDock.Name = "uiCustomValuesDock";
            this.uiCustomValuesDock.Size = new System.Drawing.Size(196, 215);
            this.uiCustomValuesDock.TabIndex = 2;
            this.uiCustomValuesDock.Text = "Custom Values";
            this.uiCustomValuesDock.Leave += new System.EventHandler(this.uiCustomValuesDock_Leave);
            // 
            // uiCustomValuesDataGrid
            // 
            this.uiCustomValuesDataGrid.CaptionVisible = false;
            this.uiCustomValuesDataGrid.DataMember = "";
            this.uiCustomValuesDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiCustomValuesDataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.uiCustomValuesDataGrid.Location = new System.Drawing.Point(0, 0);
            this.uiCustomValuesDataGrid.Name = "uiCustomValuesDataGrid";
            this.uiCustomValuesDataGrid.PreferredColumnWidth = 125;
            this.uiCustomValuesDataGrid.RowHeadersVisible = false;
            this.uiCustomValuesDataGrid.Size = new System.Drawing.Size(196, 215);
            this.uiCustomValuesDataGrid.TabIndex = 0;
            this.uiCustomValuesDataGrid.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.uiCustomValuesGridStyle});
            // 
            // uiCustomValuesGridStyle
            // 
            this.uiCustomValuesGridStyle.DataGrid = this.uiCustomValuesDataGrid;
            this.uiCustomValuesGridStyle.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.uiName,
            this.uiValue});
            this.uiCustomValuesGridStyle.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            // 
            // uiName
            // 
            this.uiName.Format = "";
            this.uiName.FormatInfo = null;
            this.uiName.HeaderText = "Tag";
            this.uiName.MappingName = "Name";
            this.uiName.Width = 50;
            // 
            // uiValue
            // 
            this.uiValue.Format = "";
            this.uiValue.FormatInfo = null;
            this.uiValue.HeaderText = "Value";
            this.uiValue.MappingName = "Value";
            this.uiValue.Width = 75;
            // 
            // rightSandDock
            // 
            this.rightSandDock.Controls.Add(this.uiPropertiesDock);
            this.rightSandDock.Controls.Add(this.uiCustomValuesDock);
            this.rightSandDock.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightSandDock.Guid = new System.Guid("5a7c170b-4aad-4827-bf24-de523a3bac08");
            this.rightSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400, System.Windows.Forms.Orientation.Horizontal, new TD.SandDock.LayoutSystemBase[] {
            ((TD.SandDock.LayoutSystemBase)(new TD.SandDock.ControlLayoutSystem(196, 256, new TD.SandDock.DockControl[] {
                        this.uiPropertiesDock}, this.uiPropertiesDock))),
            ((TD.SandDock.LayoutSystemBase)(new TD.SandDock.ControlLayoutSystem(196, 256, new TD.SandDock.DockControl[] {
                        this.uiCustomValuesDock}, this.uiCustomValuesDock)))});
            this.rightSandDock.Location = new System.Drawing.Point(592, 50);
            this.rightSandDock.Manager = this.uiSandDockManager;
            this.rightSandDock.Name = "rightSandDock";
            this.rightSandDock.Size = new System.Drawing.Size(200, 516);
            this.rightSandDock.TabIndex = 3;
            // 
            // bottomSandDock
            // 
            this.bottomSandDock.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomSandDock.Guid = new System.Guid("864e047d-2a9d-4fba-bc18-f83754ae5214");
            this.bottomSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
            this.bottomSandDock.Location = new System.Drawing.Point(0, 566);
            this.bottomSandDock.Manager = this.uiSandDockManager;
            this.bottomSandDock.Name = "bottomSandDock";
            this.bottomSandDock.Size = new System.Drawing.Size(792, 0);
            this.bottomSandDock.TabIndex = 4;
            // 
            // topSandDock
            // 
            this.topSandDock.Dock = System.Windows.Forms.DockStyle.Top;
            this.topSandDock.Guid = new System.Guid("24d50ee0-b600-4b52-a431-7e9715efbf6d");
            this.topSandDock.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400);
            this.topSandDock.Location = new System.Drawing.Point(0, 50);
            this.topSandDock.Manager = this.uiSandDockManager;
            this.topSandDock.Name = "topSandDock";
            this.topSandDock.Size = new System.Drawing.Size(792, 0);
            this.topSandDock.TabIndex = 5;
            // 
            // uiSandBarManager
            // 
            this.uiSandBarManager.BottomContainer = this.bottomSandBarDock;
            this.uiSandBarManager.LeftContainer = this.leftSandBarDock;
            this.uiSandBarManager.OwnerForm = this;
            this.uiSandBarManager.RightContainer = this.rightSandBarDock;
            this.uiSandBarManager.TopContainer = this.topSandBarDock;
            // 
            // bottomSandBarDock
            // 
            this.bottomSandBarDock.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomSandBarDock.Location = new System.Drawing.Point(0, 566);
            this.bottomSandBarDock.Manager = this.uiSandBarManager;
            this.bottomSandBarDock.Name = "bottomSandBarDock";
            this.bottomSandBarDock.Size = new System.Drawing.Size(792, 0);
            this.bottomSandBarDock.TabIndex = 8;
            // 
            // leftSandBarDock
            // 
            this.leftSandBarDock.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftSandBarDock.Location = new System.Drawing.Point(0, 50);
            this.leftSandBarDock.Manager = this.uiSandBarManager;
            this.leftSandBarDock.Name = "leftSandBarDock";
            this.leftSandBarDock.Size = new System.Drawing.Size(0, 516);
            this.leftSandBarDock.TabIndex = 6;
            // 
            // rightSandBarDock
            // 
            this.rightSandBarDock.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightSandBarDock.Location = new System.Drawing.Point(792, 50);
            this.rightSandBarDock.Manager = this.uiSandBarManager;
            this.rightSandBarDock.Name = "rightSandBarDock";
            this.rightSandBarDock.Size = new System.Drawing.Size(0, 516);
            this.rightSandBarDock.TabIndex = 7;
            // 
            // topSandBarDock
            // 
            this.topSandBarDock.Controls.Add(this.uiMenuBar);
            this.topSandBarDock.Controls.Add(this.uiToolBar);
            this.topSandBarDock.Dock = System.Windows.Forms.DockStyle.Top;
            this.topSandBarDock.Location = new System.Drawing.Point(0, 0);
            this.topSandBarDock.Manager = this.uiSandBarManager;
            this.topSandBarDock.Name = "topSandBarDock";
            this.topSandBarDock.Size = new System.Drawing.Size(792, 50);
            this.topSandBarDock.TabIndex = 9;
            // 
            // uiMenuBar
            // 
            this.uiMenuBar.Buttons.AddRange(new TD.SandBar.ToolbarItemBase[] {
            this.menuBarItem1,
            this.menuBarItem2,
            this.menuBarItem3,
            this.menuBarItem6,
            this.menuBarItem5});
            this.uiMenuBar.Guid = new System.Guid("6230f6dc-3087-4a3d-9ed3-eabccee99562");
            this.uiMenuBar.Location = new System.Drawing.Point(2, 0);
            this.uiMenuBar.Name = "uiMenuBar";
            this.uiMenuBar.Size = new System.Drawing.Size(790, 24);
            this.uiMenuBar.TabIndex = 0;
            // 
            // menuBarItem1
            // 
            this.menuBarItem1.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
            this.uiConnect,
            this.uiDisconnect,
            this.uiOpenConnectionString,
            this.uiOpenTemplateButton,
            this.uiSaveTemplateButton,
            this.uiSaveAsTemplateButton,
            this.uiSaveResultButton,
            this.uiExitMenuButton});
            this.menuBarItem1.Text = "&File";
            // 
            // uiConnect
            // 
            this.uiConnect.Text = "&Connect";
            this.uiConnect.Activate += new System.EventHandler(this.uiConnect_Activate);
            // 
            // uiDisconnect
            // 
            this.uiDisconnect.Text = "&Disconnect";
            this.uiDisconnect.Activate += new System.EventHandler(this.uiDisconnect_Activate);
            // 
            // uiOpenConnectionString
            // 
            this.uiOpenConnectionString.Text = "Confi&gure Connection";
            this.uiOpenConnectionString.Activate += new System.EventHandler(this.uiOpenConnectionString_Activate);
            // 
            // uiOpenTemplateButton
            // 
            this.uiOpenTemplateButton.BeginGroup = true;
            this.uiOpenTemplateButton.Text = "&Open Template";
            this.uiOpenTemplateButton.Activate += new System.EventHandler(this.uiOpenTemplateButton_Activate);
            // 
            // uiSaveTemplateButton
            // 
            this.uiSaveTemplateButton.Enabled = false;
            this.uiSaveTemplateButton.Text = "&Save Template";
            this.uiSaveTemplateButton.Activate += new System.EventHandler(this.uiSaveTemplateButton_Activate);
            // 
            // uiSaveAsTemplateButton
            // 
            this.uiSaveAsTemplateButton.Text = "Save &As Template";
            this.uiSaveAsTemplateButton.Activate += new System.EventHandler(this.uiSaveAsTemplateButton_Activate);
            // 
            // uiSaveResultButton
            // 
            this.uiSaveResultButton.Text = "Save Result";
            this.uiSaveResultButton.Activate += new System.EventHandler(this.uiSaveResultButton_Activate);
            // 
            // uiExitMenuButton
            // 
            this.uiExitMenuButton.Text = "E&xit";
            this.uiExitMenuButton.Activate += new System.EventHandler(this.uiExitMenuButton_Activate);
            // 
            // menuBarItem2
            // 
            this.menuBarItem2.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
            this.uiUndoButton,
            this.uiRedoButton,
            this.uiCutButton,
            this.uiCopyButton,
            this.uiPasteButton});
            this.menuBarItem2.Text = "&Edit";
            // 
            // uiUndoButton
            // 
            this.uiUndoButton.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
            this.uiUndoButton.Text = "&Undo";
            this.uiUndoButton.Activate += new System.EventHandler(this.uiUndoButton_Activate);
            // 
            // uiRedoButton
            // 
            this.uiRedoButton.Shortcut = System.Windows.Forms.Shortcut.CtrlY;
            this.uiRedoButton.Text = "&Redo";
            this.uiRedoButton.Activate += new System.EventHandler(this.uiRedoButton_Activate);
            // 
            // uiCutButton
            // 
            this.uiCutButton.BeginGroup = true;
            this.uiCutButton.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
            this.uiCutButton.Text = "Cu&t";
            this.uiCutButton.Activate += new System.EventHandler(this.uiCutButton_Activate);
            // 
            // uiCopyButton
            // 
            this.uiCopyButton.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.uiCopyButton.Text = "&Copy";
            this.uiCopyButton.Activate += new System.EventHandler(this.uiCopyButton_Activate);
            // 
            // uiPasteButton
            // 
            this.uiPasteButton.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
            this.uiPasteButton.Text = "&Paste";
            this.uiPasteButton.Activate += new System.EventHandler(this.uiPasteButton_Activate);
            // 
            // menuBarItem3
            // 
            this.menuBarItem3.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
            this.uiViewNavigatorButton,
            this.uiViewPropertiesButton,
            this.uiViewCustomValuesButton});
            this.menuBarItem3.Text = "&View";
            // 
            // uiViewNavigatorButton
            // 
            this.uiViewNavigatorButton.Text = "Navigator";
            this.uiViewNavigatorButton.Activate += new System.EventHandler(this.uiViewNavigatorButton_Activate);
            // 
            // uiViewPropertiesButton
            // 
            this.uiViewPropertiesButton.Text = "Properties";
            this.uiViewPropertiesButton.Activate += new System.EventHandler(this.uiViewPropertiesButton_Activate);
            // 
            // uiViewCustomValuesButton
            // 
            this.uiViewCustomValuesButton.Text = "Custom Values";
            this.uiViewCustomValuesButton.Activate += new System.EventHandler(this.uiViewCustomValuesButton_Activate);
            // 
            // menuBarItem6
            // 
            this.menuBarItem6.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
            this.uiGenerateCodeButton,
            this.uiFileGeneratorConfigButton,
            this.uiFileGenerateButton,
            this.uiOpenOutputFolder});
            this.menuBarItem6.Text = "&Generator";
            // 
            // uiGenerateCodeButton
            // 
            this.uiGenerateCodeButton.Shortcut = System.Windows.Forms.Shortcut.CtrlG;
            this.uiGenerateCodeButton.Text = "Generate Code";
            this.uiGenerateCodeButton.Activate += new System.EventHandler(this.uiGenerateCodeButton_Activate);
            // 
            // uiFileGeneratorConfigButton
            // 
            this.uiFileGeneratorConfigButton.BeginGroup = true;
            this.uiFileGeneratorConfigButton.Text = "File Generator Configuration";
            this.uiFileGeneratorConfigButton.Activate += new System.EventHandler(this.uiFileGeneratorConfigButton_Activate);
            // 
            // uiFileGenerateButton
            // 
            this.uiFileGenerateButton.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
            this.uiFileGenerateButton.Text = "File Generate";
            this.uiFileGenerateButton.Activate += new System.EventHandler(this.uiFileGenerateButton_Activate);
            // 
            // uiOpenOutputFolder
            // 
            this.uiOpenOutputFolder.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.uiOpenOutputFolder.Text = "Open Output Folder";
            this.uiOpenOutputFolder.Activate += new System.EventHandler(this.uiOpenOutputFolder_Activate);
            // 
            // menuBarItem5
            // 
            this.menuBarItem5.MenuItems.AddRange(new TD.SandBar.MenuButtonItem[] {
            this.uiAboutButton});
            this.menuBarItem5.Text = "&Help";
            // 
            // uiAboutButton
            // 
            this.uiAboutButton.Text = "&About...";
            this.uiAboutButton.Activate += new System.EventHandler(this.uiAboutButton_Activate);
            // 
            // uiToolBar
            // 
            this.uiToolBar.Buttons.AddRange(new TD.SandBar.ToolbarItemBase[] {
            this.uiGenerateButton,
            this.uiFileGenerator,
            this.uiMergeButton});
            this.uiToolBar.DockLine = 1;
            this.uiToolBar.Guid = new System.Guid("699b1d0f-d336-4759-8097-bfbb67d31c66");
            this.uiToolBar.Location = new System.Drawing.Point(2, 24);
            this.uiToolBar.Name = "uiToolBar";
            this.uiToolBar.Size = new System.Drawing.Size(254, 26);
            this.uiToolBar.TabIndex = 1;
            this.uiToolBar.Text = "toolBar1";
            // 
            // uiGenerateButton
            // 
            this.uiGenerateButton.Image = ((System.Drawing.Image)(resources.GetObject("uiGenerateButton.Image")));
            this.uiGenerateButton.Text = "Generate";
            this.uiGenerateButton.ToolTipText = "Generate Code";
            this.uiGenerateButton.Activate += new System.EventHandler(this.uiGenerateButton_Activate);
            // 
            // uiFileGenerator
            // 
            this.uiFileGenerator.Text = "File Generate";
            this.uiFileGenerator.Activate += new System.EventHandler(this.uiFileGenerator_Activate);
            // 
            // uiMergeButton
            // 
            this.uiMergeButton.Text = "Merge Code";
            this.uiMergeButton.ToolTipText = "Run External Merging Application";
            this.uiMergeButton.Activate += new System.EventHandler(this.uiMergeButton_Activate);
            // 
            // uiTemplateContainer
            // 
            this.uiTemplateContainer.Controls.Add(this.uiTemplateDock);
            this.uiTemplateContainer.Controls.Add(this.uiGenerateCodeDock);
            this.uiTemplateContainer.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiTemplateContainer.Guid = new System.Guid("e2d7d100-338b-414b-a8f5-696d61a3348a");
            this.uiTemplateContainer.LayoutSystem = new TD.SandDock.SplitLayoutSystem(250, 400, System.Windows.Forms.Orientation.Horizontal, new TD.SandDock.LayoutSystemBase[] {
            ((TD.SandDock.LayoutSystemBase)(new TD.SandDock.DocumentLayoutSystem(390, 492, new TD.SandDock.DockControl[] {
                        this.uiTemplateDock,
                        this.uiGenerateCodeDock}, this.uiTemplateDock)))});
            this.uiTemplateContainer.Location = new System.Drawing.Point(200, 50);
            this.uiTemplateContainer.Manager = null;
            this.uiTemplateContainer.Name = "uiTemplateContainer";
            this.uiTemplateContainer.Size = new System.Drawing.Size(392, 494);
            this.uiTemplateContainer.TabIndex = 10;
            // 
            // uiTemplateDock
            // 
            this.uiTemplateDock.Closable = false;
            this.uiTemplateDock.Controls.Add(this.uiTemplateTextBox);
            this.uiTemplateDock.Guid = new System.Guid("6065db1e-e6bf-4209-8cb9-03c7805d045a");
            this.uiTemplateDock.Location = new System.Drawing.Point(3, 23);
            this.uiTemplateDock.Name = "uiTemplateDock";
            this.uiTemplateDock.Size = new System.Drawing.Size(386, 468);
            this.uiTemplateDock.TabIndex = 0;
            this.uiTemplateDock.Text = "Template";
            // 
            // uiTemplateTextBox
            // 
            this.uiTemplateTextBox.AcceptsTab = true;
            this.uiTemplateTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTemplateTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiTemplateTextBox.Location = new System.Drawing.Point(0, 0);
            this.uiTemplateTextBox.Name = "uiTemplateTextBox";
            this.uiTemplateTextBox.Size = new System.Drawing.Size(386, 468);
            this.uiTemplateTextBox.TabIndex = 0;
            this.uiTemplateTextBox.Text = "";
            // 
            // uiGenerateCodeDock
            // 
            this.uiGenerateCodeDock.Closable = false;
            this.uiGenerateCodeDock.Controls.Add(this.uiGeneratedCodeTextBox);
            this.uiGenerateCodeDock.Guid = new System.Guid("26ae2cc4-bc92-4191-b399-333e1ca9da35");
            this.uiGenerateCodeDock.Location = new System.Drawing.Point(3, 23);
            this.uiGenerateCodeDock.Name = "uiGenerateCodeDock";
            this.uiGenerateCodeDock.Size = new System.Drawing.Size(532, 468);
            this.uiGenerateCodeDock.TabIndex = 1;
            this.uiGenerateCodeDock.Text = "Generated Code";
            // 
            // uiGeneratedCodeTextBox
            // 
            this.uiGeneratedCodeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGeneratedCodeTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGeneratedCodeTextBox.Location = new System.Drawing.Point(0, 0);
            this.uiGeneratedCodeTextBox.Name = "uiGeneratedCodeTextBox";
            this.uiGeneratedCodeTextBox.ReadOnly = true;
            this.uiGeneratedCodeTextBox.Size = new System.Drawing.Size(532, 468);
            this.uiGeneratedCodeTextBox.TabIndex = 0;
            this.uiGeneratedCodeTextBox.Text = "";
            // 
            // uiOpenTemplateDialog
            // 
            this.uiOpenTemplateDialog.Filter = "All Files|*.*|Text Files|*.txt";
            this.uiOpenTemplateDialog.Title = "Open Template";
            // 
            // MainWindow
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.uiTemplateContainer);
            this.Controls.Add(this.uiStatusBar);
            this.Controls.Add(this.leftSandDock);
            this.Controls.Add(this.rightSandDock);
            this.Controls.Add(this.bottomSandDock);
            this.Controls.Add(this.topSandDock);
            this.Controls.Add(this.leftSandBarDock);
            this.Controls.Add(this.rightSandBarDock);
            this.Controls.Add(this.bottomSandBarDock);
            this.Controls.Add(this.topSandBarDock);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "iCode+Generator";
            this.leftSandDock.ResumeLayout(false);
            this.uiNavigatorDock.ResumeLayout(false);
            this.uiPropertiesDock.ResumeLayout(false);
            this.uiPropertiesPanel.ResumeLayout(false);
            this.uiCustomValuesDock.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiCustomValuesDataGrid)).EndInit();
            this.rightSandDock.ResumeLayout(false);
            this.topSandBarDock.ResumeLayout(false);
            this.uiTemplateContainer.ResumeLayout(false);
            this.uiTemplateDock.ResumeLayout(false);
            this.uiGenerateCodeDock.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private void uiExitMenuButton_Activate(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void uiNavigatorControl_DatabaseSelect(object sender, DatabaseEventArgs args)
		{
			uiStatusBar.Text = args.Database.Name;
			uiPropertyEditor.SelectedObject = args.Database;
		}

		private void uiNavigatorControl_TableSelect(object sender, TableEventArgs args)
		{
			selectedTable = args.Table;
			uiStatusBar.Text = args.Table.ParentDatabase.Name + " > " +
			                   args.Table.Name;
			uiPropertyEditor.SelectedObject = args.Table;
		}

		private void uiNavigatorControl_ColumnSelect(object sender, ColumnEventArgs args)
		{
			uiStatusBar.Text = args.Column.ParentTable.ParentDatabase.Name + " > " +
			                   args.Column.ParentTable.Name + " > " +
			                   args.Column.Name;
			uiPropertyEditor.SelectedObject = args.Column;
		}

		private void uiGenerateButton_Activate(object sender, EventArgs e)
		{
			GenerateCode();
		}

		private void GenerateCode()
		{
			try
			{
				if (selectedTable == null) return;
				Client cgenerator = new Client();
				cgenerator.CustomValues = GetCustomValues();
				uiGeneratedCodeTextBox.Text = cgenerator.Parse(selectedTable, uiTemplateTextBox.Text);
			}
			catch (DataTypeManagerException ex)
			{
				MessageBox.Show(this, ex.Message, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private IDictionary GetCustomValues()
		{
			IDictionary customValues = new Hashtable();
			foreach (DataRow row in _CustomValues.Tables[0].Rows)
			{
				customValues[row["Name"].ToString().ToUpper()] = row["Value"].ToString();
			}
			return customValues;
		}

		private void uiNavigatorControl_ColumnShowProperties(object sender, FolderEventArgs args)
		{
			uiPropertiesDock.Open();
		}

		#region Copy, Paste & Methods

		[DllImport("user32.dll", CharSet=CharSet.Auto, CallingConvention=CallingConvention.Winapi)]
		private static extern IntPtr GetFocus();

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		private static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);

		private const int WM_CUT = 0x300;
		private const int WM_COPY = 0x301;
		private const int WM_PASTE = 0x302;
		private const int WM_UNDO = 0x304;


		private void uiCutButton_Activate(object sender, EventArgs e)
		{
			Control focusedControl = GetFocusedControl();
			if (focusedControl is TextBoxBase)
				((TextBoxBase) focusedControl).Cut();
				//			else if (focusedControl is RichTextBox)
				//			{
				//				// Special application processing for this event
				//			}
			else
				SendMessage(new HandleRef(this, GetFocus()), WM_CUT, 0, 0);
		}

		private void uiCopyButton_Activate(object sender, EventArgs e)
		{
			Control focusedControl = GetFocusedControl();
			if (focusedControl is TextBoxBase)
				((TextBoxBase) focusedControl).Copy();
				//			else if (focusedControl is RichTextBox)
				//			{
				//				// Special application processing for this event
				//			}
			else
				SendMessage(new HandleRef(this, GetFocus()), WM_COPY, 0, 0);
		}

		private void uiPasteButton_Activate(object sender, EventArgs e)
		{
			Control focusedControl = GetFocusedControl();
			if (focusedControl is TextBoxBase)
				((TextBoxBase) focusedControl).Paste();
				//			else if (focusedControl is RichTextBox)
				//			{
				//				// Special application processing for this event
				//			}
			else
				SendMessage(new HandleRef(this, GetFocus()), WM_PASTE, 0, 0);
		}

		private void uiUndoButton_Activate(object sender, EventArgs e)
		{
			Control focusedControl = GetFocusedControl();
			if (focusedControl is TextBoxBase)
				((TextBoxBase) focusedControl).Undo();
				//			else if (focusedControl is RichTextBox)
				//			{
				//				// Special application processing for this event
				//			}
			else
				SendMessage(new HandleRef(this, GetFocus()), WM_UNDO, 0, 0);
		}

		private Control GetFocusedControl()
		{
			// Try and find the .net control instance with the focus
			IntPtr focus = GetFocus();
			if (focus == IntPtr.Zero)
				return null;
			else
				return FromHandle(focus);
		}

		private void uiRedoButton_Activate(object sender, EventArgs e)
		{
			Control focusedControl = GetFocusedControl();
			if (focusedControl is RichTextBox)
				((RichTextBox) focusedControl).Redo();
				//			else if (focusedControl is RichTextBox)
				//			{
				//				// Special application processing for this event
				//			}
			else
				SendMessage(new HandleRef(this, GetFocus()), WM_UNDO, 0, 0); // 
		}

		#endregion

		#region Methods

		private void uiViewNavigatorButton_Activate(object sender, EventArgs e)
		{
			if (uiNavigatorDock.IsOpen)
			{
				uiNavigatorDock.Close();
			}
			else
			{
				uiNavigatorDock.Open();
			}
		}

		private void uiViewPropertiesButton_Activate(object sender, EventArgs e)
		{
			if (uiPropertiesDock.IsOpen)
			{
				uiPropertiesDock.Close();
			}
			else
			{
				uiPropertiesDock.Open();
			}
		}

		private void uiViewCustomValuesButton_Activate(object sender, EventArgs e)
		{
			if (uiCustomValuesDock.IsOpen)
			{
				uiCustomValuesDock.Close();
			}
			else
			{
				uiCustomValuesDock.Open();
			}
		}

		private void uiAboutButton_Activate(object sender, EventArgs e)
		{
			new AboutWindow().ShowDialog();
		}

		private void uiOpenTemplateButton_Activate(object sender, EventArgs e)
		{
			if (uiOpenTemplateDialog.ShowDialog() == DialogResult.OK)
			{
				_TemplateFilename = uiOpenTemplateDialog.FileName;
				uiTemplateTextBox.LoadFile(_TemplateFilename, RichTextBoxStreamType.PlainText);
				uiSaveTemplateButton.Enabled = true;
			}
		}

		private void uiSaveTemplateButton_Activate(object sender, EventArgs e)
		{
			uiTemplateTextBox.SaveFile(_TemplateFilename, RichTextBoxStreamType.PlainText);
		}

		private void uiSaveAsTemplateButton_Activate(object sender, EventArgs e)
		{
			if (uiSaveDialog.ShowDialog() == DialogResult.OK)
			{
				uiTemplateTextBox.SaveFile(uiSaveDialog.FileName, RichTextBoxStreamType.PlainText);
			}
		}

		private void uiSaveResultButton_Activate(object sender, EventArgs e)
		{
			if (uiSaveDialog.ShowDialog() == DialogResult.OK)
			{
				uiGeneratedCodeTextBox.SaveFile(uiSaveDialog.FileName, RichTextBoxStreamType.PlainText);
			}
		}

		private void uiGenerateCodeButton_Activate(object sender, EventArgs e)
		{
			GenerateCode();
		}

		private void uiMergeButton_Activate(object sender, EventArgs e)
		{
			if (uiOpenMergeDialog.ShowDialog() == DialogResult.OK)
			{
				_TempFilename = CreateTempFile(uiGeneratedCodeTextBox.Text);
				try
				{
					RunMergeProcess("WinMerge.exe", _TempFilename, uiOpenMergeDialog.FileName);
				}
				catch (Win32Exception ex)
				{
					Debug.WriteLine(ex);
					MessageBox.Show(this,
					                "Merge unavailable.\nPlease install WinMerge to have this option or make sure it is in your PATH.\nhttp://winmerge.sourceforge.net/",
					                "Merge unavailable.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				uiGeneratedCodeTextBox.LoadFile(_TempFilename, RichTextBoxStreamType.PlainText);
				DeleteFile(ref _TempFilename);
			}
		}

		private static void RunMergeProcess(string mergeExec, string tempFilename, string oldFilename)
		{
			Process process = new Process();
			process.StartInfo.FileName = mergeExec;
			process.StartInfo.Arguments = "-e \"" + tempFilename + "\"" + " " + "\"" + oldFilename + "\"";
			process.Start();
			process.WaitForExit();
		}

		private static void DeleteFile(ref string filename)
		{
			File.Delete(filename);
			filename = String.Empty;
		}

		private string CreateTempFile(string textContent)
		{
			string filename = Path.GetTempFileName();
			FileInfo tempFileInfo = new FileInfo(filename);
			tempFileInfo.Attributes = FileAttributes.Temporary;
			StreamWriter writer = File.AppendText(filename);
			writer.Write(textContent);
			writer.Flush();
			writer.Close();
			return filename;
		}

		private void uiCustomValuesDock_Leave(object sender, EventArgs e)
		{
			FilterEmptyCustomValues();
			SaveCustomValues();
		}

		private void SaveCustomValues()
		{
			_CustomValues.WriteXml(_CustomValuesFilename);
		}

		private void FilterEmptyCustomValues()
		{
			DataRow[] rows = _CustomValues.Tables[0].Select();
			for (int i = 0; i < rows.Length; i++)
			{
				if (rows[i][0].ToString().Trim().Length == 0)
				{
					rows[i].Delete();
				}
				else
				{
					rows[i][0] = rows[i][0].ToString().ToUpper().Trim();
				}
			}
			uiCustomValuesDataGrid.Refresh();
		}

		private void uiFileGenerator_Activate(object sender, EventArgs e)
		{
			GenerateFiles();
		}

		private void uiFileGeneratorConfigButton_Activate(object sender, EventArgs e)
		{
			SelectTemplatesDirectory();
		}

		private void uiFileGenerateButton_Activate(object sender, EventArgs e)
		{
			GenerateFiles();
		}

		private void SelectTemplatesDirectory()
		{
			DirectorySelectionWindow selectionWindow = new DirectorySelectionWindow();
			selectionWindow.ShowDialog(this);
		}

		private void GenerateFiles()
		{
			if (IsValidFolder(_InputTemplateFolder) && IsValidFolder(_OutputTemplateFolder))
			{
				try
				{
					FileGenerator generator = new FileGenerator();
					generator.OnComplete += new EventHandler(fileGenerator_Completed);
					generator.CustomValue = GetCustomValues();
					generator.Generate(selectedTable, _InputTemplateFolder, _OutputTemplateFolder);
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

		private void fileGenerator_Completed(object sender, EventArgs e)
		{
			uiStatusBar.Text = "File Generation Completed";
			MessageBox.Show("File Generation Completed");
		}

		private void uiOpenOutputFolder_Activate(object sender, EventArgs e)
		{
			if (IsValidFolder(_OutputTemplateFolder))
			{
				Process.Start(_OutputTemplateFolder);
			}
			else
			{
				SelectTemplatesDirectory();
			}
		}

		private bool IsValidFolder(string folder)
		{
			return folder.Length > 0 && Directory.Exists(folder);
		}

		#endregion

		private void uiOpenConnectionString_Activate(object sender, EventArgs e)
		{
			uiNavigatorControl.ShowEditConnectionStringDialog();
		}

		private void uiConnect_Activate(object sender, EventArgs e)
		{
			uiNavigatorControl.Connect();
		}

		private void uiDisconnect_Activate(object sender, EventArgs e)
		{
			uiNavigatorControl.Disconnect();
		}

		private void dockControl1_Closing(object sender, CancelEventArgs e)
		{
		}
	}
}