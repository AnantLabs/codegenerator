using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace iCodeGenerator.iCodeGeneratorGui
{
	/// <summary>
	/// Summary description for UpdatesWindow.
	/// </summary>
	public class UpdatesWindow : Form
	{
		private Label label3;
		private RichTextBox uiNoticesTextbox;
		private LinkLabel uiApplicationUrl;
		private Label uiCodeGenerator;
		private Label uiNewVersion;
		private Label uiCurrentVersion;
		private Label uiDownloadMessage;
	
		public string NewVersion
		{
			set { uiNewVersion.Text = value;}
			get { return uiNewVersion.Text.Trim(); }
		}
		public string CurrentVersion
		{
			set { uiCurrentVersion.Text = value; }
			get { return uiCurrentVersion.Text; }
		}
		public string Url
		{
			set { uiApplicationUrl.Text = value; }
			get { return uiApplicationUrl.Text; }
		}
		public string Notice
		{
			set { uiNoticesTextbox.Text = value; }
			get { return uiNoticesTextbox.Text; }
		}
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public UpdatesWindow()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UpdatesWindow));
			this.uiCodeGenerator = new System.Windows.Forms.Label();
			this.uiCurrentVersion = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.uiNewVersion = new System.Windows.Forms.Label();
			this.uiApplicationUrl = new System.Windows.Forms.LinkLabel();
			this.uiNoticesTextbox = new System.Windows.Forms.RichTextBox();
			this.uiDownloadMessage = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// uiCodeGenerator
			// 
			this.uiCodeGenerator.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.uiCodeGenerator.Location = new System.Drawing.Point(19, 8);
			this.uiCodeGenerator.Name = "uiCodeGenerator";
			this.uiCodeGenerator.Size = new System.Drawing.Size(100, 16);
			this.uiCodeGenerator.TabIndex = 0;
			this.uiCodeGenerator.Text = "iCodeGenerator";
			this.uiCodeGenerator.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// uiCurrentVersion
			// 
			this.uiCurrentVersion.Location = new System.Drawing.Point(123, 8);
			this.uiCurrentVersion.Name = "uiCurrentVersion";
			this.uiCurrentVersion.Size = new System.Drawing.Size(100, 16);
			this.uiCurrentVersion.TabIndex = 1;
			this.uiCurrentVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(219, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "New Version";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// uiNewVersion
			// 
			this.uiNewVersion.Location = new System.Drawing.Point(324, 8);
			this.uiNewVersion.Name = "uiNewVersion";
			this.uiNewVersion.Size = new System.Drawing.Size(100, 16);
			this.uiNewVersion.TabIndex = 3;
			this.uiNewVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// uiApplicationUrl
			// 
			this.uiApplicationUrl.Location = new System.Drawing.Point(85, 56);
			this.uiApplicationUrl.Name = "uiApplicationUrl";
			this.uiApplicationUrl.Size = new System.Drawing.Size(272, 16);
			this.uiApplicationUrl.TabIndex = 4;
			this.uiApplicationUrl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.uiApplicationUrl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.uiApplicationUrl_LinkClicked);
			// 
			// uiNoticesTextbox
			// 
			this.uiNoticesTextbox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.uiNoticesTextbox.Location = new System.Drawing.Point(0, 72);
			this.uiNoticesTextbox.Name = "uiNoticesTextbox";
			this.uiNoticesTextbox.ReadOnly = true;
			this.uiNoticesTextbox.Size = new System.Drawing.Size(442, 112);
			this.uiNoticesTextbox.TabIndex = 5;
			this.uiNoticesTextbox.Text = "";
			this.uiNoticesTextbox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.uiNoticesTextbox_LinkClicked);
			// 
			// uiDownloadMessage
			// 
			this.uiDownloadMessage.Location = new System.Drawing.Point(85, 32);
			this.uiDownloadMessage.Name = "uiDownloadMessage";
			this.uiDownloadMessage.Size = new System.Drawing.Size(272, 23);
			this.uiDownloadMessage.TabIndex = 6;
			this.uiDownloadMessage.Text = "Download from the following address";
			this.uiDownloadMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// UpdatesWindow
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(442, 184);
			this.Controls.Add(this.uiDownloadMessage);
			this.Controls.Add(this.uiNoticesTextbox);
			this.Controls.Add(this.uiApplicationUrl);
			this.Controls.Add(this.uiNewVersion);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.uiCurrentVersion);
			this.Controls.Add(this.uiCodeGenerator);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "UpdatesWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Update Notice";
			this.ResumeLayout(false);

		}
		#endregion

		private void uiApplicationUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			uiApplicationUrl.LinkVisited = true;
			Close();
			Process.Start(Url);
		}

		private void uiNoticesTextbox_LinkClicked(object sender, System.Windows.Forms.LinkClickedEventArgs e)
		{
			Process.Start(e.LinkText);
		}
	}
}
