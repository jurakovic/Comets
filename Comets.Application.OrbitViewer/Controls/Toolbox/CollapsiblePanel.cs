﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Comets.Application.OrbitViewer.Controls
{
	[Designer(typeof(CollapsiblePanelDesigner))]
	public partial class CollapsiblePanel : UserControl
	{
		#region Const

		private readonly int HeightCollapsed = 30;

		#endregion

		#region Fields

		private string _text = String.Empty;

		#endregion

		#region Properties

		public int HeightExpanded { get; set; }

		public bool IsCollapsed
		{
			get { return this.Height == HeightCollapsed; }
			set
			{
				this.Height = value ? HeightCollapsed : HeightExpanded;
				this.panel.Visible = !value;
				SetText();
			}
		}

		public new string Text
		{
			get { return _text; }
			set { _text = value; SetText(); }
		}

		[Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Panel WorkingArea
		{
			get { return this.panel; }
		}

		#endregion

		#region Constructor

		public CollapsiblePanel()
		{
			InitializeComponent();
		}

		#endregion

		#region EventHandling

		private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (HeightExpanded == 0)
				throw new ArgumentException("HeightExpanded not set");

			this.IsCollapsed = !this.IsCollapsed;

			MovePanels();
			SetText();
		}

		#endregion

		#region Methods

		private void SetText()
		{
			linkLabel.Text = String.Format("{0}  {1}", IsCollapsed ? "▼" : "▲", _text.ToUpper().PadRight(255));
		}

		private void MovePanels()
		{
			int offset = HeightExpanded - HeightCollapsed;

			Control container =
				this.Parent as Form
				?? (this.Parent as ContainerControl) as Control
				?? (this.Parent as Panel) as Control;

			foreach (Control c in container.Controls.OfType<CollapsiblePanel>())
				if (c.Left == this.Left && c.Top > this.Top)
					c.Top += this.panel.Visible ? offset : -offset; //move up or down
		}

		#endregion
	}

	#region CollapsiblePanelDesigner

	public class CollapsiblePanelDesigner : ParentControlDesigner
	{
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);

			if (this.Control is CollapsiblePanel)
				this.EnableDesignMode(((CollapsiblePanel)this.Control).WorkingArea, "WorkingArea");
		}
	}

	#endregion
}
