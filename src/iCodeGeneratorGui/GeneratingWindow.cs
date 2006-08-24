using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace iCodeGenerator.iCodeGeneratorGui
{
	/// <summary>
	/// Summary description for GeneratingWindow.
	/// </summary>
	public class GeneratingWindow : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label uiGeneratingLabel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public GeneratingWindow()
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
			this.uiGeneratingLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// uiGeneratingLabel
			// 
			this.uiGeneratingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.uiGeneratingLabel.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.uiGeneratingLabel.Location = new System.Drawing.Point(82, 45);
			this.uiGeneratingLabel.Name = "uiGeneratingLabel";
			this.uiGeneratingLabel.TabIndex = 0;
			this.uiGeneratingLabel.Text = "Generating...";
			// 
			// GeneratingWindow
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(264, 112);
			this.Controls.Add(this.uiGeneratingLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "GeneratingWindow";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.ResumeLayout(false);

		}
		#endregion
	}
}
