﻿using Comets.Application.Common.Controls.DateAndTime;
using Comets.Core;
using Comets.Core.Extensions;
using Comets.Core.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DataTypeEnum = Comets.Core.Managers.FilterManager.DataTypeEnum;
using PropertyEnum = Comets.Core.Managers.CometManager.PropertyEnum;

namespace Comets.Application.Common.Managers
{
	public static class FilterPanelManager
	{
		#region Const

		public static readonly string CheckedName = "Checked";
		public static readonly string PropertyName = "Property";
		public static readonly string CompareName = "Compare";
		public static readonly string StringName = "String";
		public static readonly string ValueName = "Value";
		public static readonly string DateName = "Date";
		public static readonly string LabelName = "Label";
		public static readonly string RemoveName = "Remove";

		private static readonly string[] StringCompare = new string[] { "Contains", "Does not contain", "Starts with", "Ends with" };
		private static readonly string[] ValueCompare = new string[] { "Greather than (>)", "Less than (<)", "Equals (=)" };

		#endregion

		#region Methods

		public static DataTypeEnum GetDataType(PropertyEnum property)
		{
			return PanelDefinitions.Single(x => x.Property == property).DataType;
		}

		private static string GetText(PropertyEnum property)
		{
			return PanelDefinitions.Single(x => x.Property == property).Text;
		}

		#endregion

		#region ValidateFilters

		public static string ValidateFilters(FilterCollection filters)
		{
			string retval = null;

			Filter filter = filters.FirstOrDefault(x => x.Checked && !x.IsValid);
			if (filter != null)
				retval = String.Format("Please enter value for \"{0}\"", GetText(filter.Property));

			return retval;
		}

		#endregion

		#region PanelDefinitions

		#region PanelDefinition

		private class PanelDefinition
		{
			public PropertyEnum Property;
			public string Text;
			public int InitialCompareIndex;
			public bool StringVisible;
			public bool ValueVisible;
			public bool DateVisible;
			public string LabelText;
			public DataTypeEnum DataType;
			public ValNum Validator;

			public PanelDefinition(
				PropertyEnum property,
				string text,
				int compareIx,
				bool stringVisible,
				bool valueVisible,
				bool dateVisible,
				string labelText,
				DataTypeEnum
				dataType,
				ValNum validator)
			{
				Property = property;
				Text = text;
				InitialCompareIndex = compareIx;
				StringVisible = stringVisible;
				ValueVisible = valueVisible;
				DateVisible = dateVisible;
				LabelText = labelText;
				DataType = dataType;
				Validator = validator;
			}
		}

		#endregion

		private static List<PanelDefinition> PanelDefinitions = new List<PanelDefinition>
		{
			new PanelDefinition(PropertyEnum.full,              "Full name",                        0, true,    false,  false,  "",     DataTypeEnum.String, null),
			new PanelDefinition(PropertyEnum.name,              "Discoverer",                       0, true,    false,  false,  "",     DataTypeEnum.String, null),
			new PanelDefinition(PropertyEnum.id,                "Designation",                      0, true,    false,  false,  "",     DataTypeEnum.String, null),
			new PanelDefinition(PropertyEnum.Tn,                "Perihelion date",                  0, false,   false,  true,   "",     DataTypeEnum.Double, null),
			new PanelDefinition(PropertyEnum.q,                 "Perihelion distance",              1, false,   true,   false,  "AU",   DataTypeEnum.Double, new ValNum(0.0, 15.0, 6)),
			new PanelDefinition(PropertyEnum.PerihEarthDist,    "Perihelion distance from Earth",   1, false,   true,   false,  "AU",   DataTypeEnum.Double, new ValNum(0.0, 15.0, 6)),
			new PanelDefinition(PropertyEnum.PerihMag,          "Perihelion magnitude",             1, false,   true,   false,  "",     DataTypeEnum.Double, ValNum.VMagnitude),
			new PanelDefinition(PropertyEnum.CurrentSunDist,    "Current distance from Sun",        1, false,   true,   false,  "AU",   DataTypeEnum.Double, new ValNum(0.0, 150.0, 6)),
			new PanelDefinition(PropertyEnum.CurrentEarthDist,  "Current distance from Earth",      1, false,   true,   false,  "AU",   DataTypeEnum.Double, new ValNum(0.0, 150.0, 6)),
			new PanelDefinition(PropertyEnum.CurrentMag,        "Current magnitude",                1, false,   true,   false,  "",     DataTypeEnum.Double, ValNum.VMagnitude),
			new PanelDefinition(PropertyEnum.P,                 "Period",                           1, false,   true,   false,  "years",DataTypeEnum.Double, new ValNum(0.0, 10000.0, 6)),
			new PanelDefinition(PropertyEnum.Q,                 "Aphelion distance",                1, false,   true,   false,  "AU",   DataTypeEnum.Double, new ValNum(0.0, 1000.0, 6)),
			new PanelDefinition(PropertyEnum.a,                 "Semi-major axis",                  1, false,   true,   false,  "AU",   DataTypeEnum.Double, new ValNum(0.0, 1000.0, 6)),
			new PanelDefinition(PropertyEnum.e,                 "Eccentricity",                     1, false,   true,   false,  "",     DataTypeEnum.Double, new ValNum(0.0, 1.2, 6)),
			new PanelDefinition(PropertyEnum.i,                 "Inclination",                      1, false,   true,   false,  "°",    DataTypeEnum.Double, new ValNum(0.0, 179.9999, 4)),
			new PanelDefinition(PropertyEnum.N,                 "Longitude of Ascending Node",      1, false,   true,   false,  "°",    DataTypeEnum.Double, new ValNum(0.0, 359.9999, 4)),
			new PanelDefinition(PropertyEnum.w,                 "Argument of Pericenter",           1, false,   true,   false,  "°",    DataTypeEnum.Double, new ValNum(0.0, 359.9999, 4))
		};

		#endregion

		#region CreateFilterPanel

		public static void CreateFilterPanel(Control parent, int id, Point location, DateTime dt, Filter filter, PropertyEnum? property)
		{
			parent.SuspendLayout();

			Panel pnlPanel = new Panel();

			CheckBox cbxChecked = new CheckBox();
			ComboBox cboProperty = new ComboBox();
			ComboBox cboCompare = new ComboBox();

			TextBox txtString = new TextBox();
			TextBox txtValue = new TextBox();
			SelectDateControl sdc = new SelectDateControl();

			Label lblLabel = new Label();
			Button btnRemove = new Button();

			pnlPanel.SuspendLayout();

			pnlPanel.Controls.Add(cbxChecked);
			pnlPanel.Controls.Add(cboProperty);
			pnlPanel.Controls.Add(cboCompare);

			pnlPanel.Controls.Add(txtString);
			pnlPanel.Controls.Add(txtValue);
			pnlPanel.Controls.Add(sdc);

			pnlPanel.Controls.Add(lblLabel);
			pnlPanel.Controls.Add(btnRemove);

			// 
			// Panel
			// 
			pnlPanel.Location = location;
			pnlPanel.Name = id.ToString();
			pnlPanel.Size = new Size(550, 25);
			pnlPanel.TabIndex = id;

			// 
			// Checked
			// 
			cbxChecked.AutoSize = true;
			cbxChecked.Location = new Point(3, 6);
			cbxChecked.Name = CheckedName;
			cbxChecked.Size = new Size(15, 14);
			cbxChecked.TabIndex = 0;
			cbxChecked.UseVisualStyleBackColor = true;
			cbxChecked.Visible = false;

			// 
			// Property
			// 
			cboProperty.DropDownStyle = ComboBoxStyle.DropDownList;
			cboProperty.FormattingEnabled = true;
			cboProperty.Items.AddRange(PanelDefinitions.Select(x => x.Text).ToArray());
			cboProperty.Location = new Point(20, 2);
			cboProperty.Name = PropertyName;
			cboProperty.Size = new Size(190, 21);
			cboProperty.TabIndex = 1;

			// 
			// Compare
			// 
			cboCompare.DropDownStyle = ComboBoxStyle.DropDownList;
			cboCompare.FormattingEnabled = true;
			cboCompare.Location = new Point(214, 2);
			cboCompare.Name = CompareName;
			cboCompare.Size = new Size(118, 21);
			cboCompare.TabIndex = 2;
			cboCompare.Visible = false;

			// 
			// String
			// 
			txtString.Location = new Point(336, 2);
			txtString.Name = StringName;
			txtString.Size = new Size(182, 20);
			txtString.TabIndex = 3;
			txtString.Visible = false;

			// 
			// Value
			// 
			txtValue.Location = new Point(336, 2);
			txtValue.MaxLength = 100;
			txtValue.Name = ValueName;
			txtValue.Size = new Size(113, 20);
			txtValue.TabIndex = 4;
			txtValue.Visible = false;

			// 
			// Date
			// 
			sdc.Location = new Point(336, 1);
			sdc.Name = DateName;
			sdc.Size = new Size(182, 23);
			sdc.TabIndex = 5;
			sdc.SelectedDateTime = dt;
			sdc.OnSelectedDatetimeChanged += sdc_OnSelectedDatetimeChanged;
			sdc.Visible = false;

			// 
			// Label
			// 
			lblLabel.AutoSize = true;
			lblLabel.Location = new Point(450, 5);
			lblLabel.Name = LabelName;
			lblLabel.Size = new Size(22, 13);
			lblLabel.TabIndex = 6;

			// 
			// Remove
			// 
			btnRemove.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
			btnRemove.Location = new Point(522, 1);
			btnRemove.Name = RemoveName;
			btnRemove.Size = new Size(25, 23);
			btnRemove.TabIndex = 7;
			btnRemove.Text = "-";
			btnRemove.TextAlign = ContentAlignment.TopCenter;
			btnRemove.UseVisualStyleBackColor = true;

			//populate data

			PanelDefinition definition;
			bool isChecked = false;
			int propertyIndex = -1;
			int compareIndex = -1;

			if (filter != null || property != null)
			{
				if (filter != null)
				{
					definition = PanelDefinitions.Single(x => x.Property == filter.Property);
					propertyIndex = (int)filter.Property;
					compareIndex = filter.CompareIndex;
					isChecked = filter.Checked;

					if (filter.DataType == DataTypeEnum.String)
					{
						txtString.Text = filter.Text;
					}
					else if (filter.Property == PropertyEnum.Tn)
					{
						sdc.SelectedDateTime = EphemerisManager.JDToDateTime(Convert.ToDecimal(filter.Value));
					}
					else
					{
						txtValue.Text = filter.Text;
					}
				}
				else
				{
					definition = PanelDefinitions.Single(x => x.Property == property);
					isChecked = false;
					propertyIndex = PanelDefinitions.IndexOf(definition);
					compareIndex = definition.InitialCompareIndex;
				}

				txtString.Visible = definition.StringVisible;

				txtValue.Visible = definition.ValueVisible;
				txtValue.Tag = definition?.Validator;

				sdc.Visible = definition.DateVisible;

				lblLabel.Text = definition.LabelText;

				if (definition.DataType == DataTypeEnum.String)
					cboCompare.Items.AddRange(StringCompare);
				else
					cboCompare.Items.AddRange(ValueCompare);

				cboCompare.Visible = true;
				cboCompare.SelectedIndex = compareIndex;

				cbxChecked.Visible = true;
				cbxChecked.Checked = isChecked;
			}

			cboProperty.SelectedIndex = propertyIndex;

			cboProperty.SelectedIndexChanged += new EventHandler(property_SelectedIndexChanged);
			txtValue.KeyPress += new KeyPressEventHandler(txtCommon_KeyPress);
			txtValue.TextChanged += new EventHandler(txtCommon_TextChanged);
			txtString.TextChanged += new EventHandler(txtCommon_TextChanged);
			btnRemove.Click += btnRemove_Click;

			pnlPanel.ResumeLayout(false);
			pnlPanel.PerformLayout();

			parent.Controls.Add(pnlPanel);
			parent.ResumeLayout(false);
			parent.PerformLayout();
		}

		#endregion

		#region + Events

		#region property_SelectedIndexChanged

		static void property_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox cbxProperty = sender as ComboBox;
			PanelDefinition definition = PanelDefinitions.ElementAt(cbxProperty.SelectedIndex);
			PropertyEnum property = definition.Property;

			Panel panel = cbxProperty.Parent as Panel;

			CheckBox cbx = null;
			ComboBox compare = null;
			TextBox str = null;
			TextBox value = null;
			SelectDateControl date = null;
			Label label = null;

			foreach (Control c in panel.Controls)
			{
				if (c.Name == CheckedName)
					cbx = c as CheckBox;
				else if (c.Name == CompareName)
					compare = c as ComboBox;
				else if (c.Name == StringName)
					str = c as TextBox;
				else if (c.Name == ValueName)
					value = c as TextBox;
				else if (c.Name == DateName)
					date = c as SelectDateControl;
				else if (c.Name == LabelName)
					label = c as Label;
			}

			compare.Items.Clear();

			if (definition.DataType == DataTypeEnum.String)
				compare.Items.AddRange(StringCompare);
			else
				compare.Items.AddRange(ValueCompare);

			cbx.Visible = true;

			compare.Visible = true;
			compare.SelectedIndex = definition.InitialCompareIndex;

			str.Visible = definition.StringVisible;
			value.Visible = definition.ValueVisible;
			value.Tag = definition?.Validator;
			date.Visible = definition.DateVisible;
			label.Text = definition.LabelText;
		}

		#endregion

		#region txtCommon_KeyPress

		static void txtCommon_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = ValNumManager.HandleKeyPress(sender, e);
		}

		#endregion

		#region txtCommon_TextChanged

		static void txtCommon_TextChanged(object sender, EventArgs e)
		{
			TextBox txt = sender as TextBox;
			txt.Parent.Controls.OfType<CheckBox>().First().Checked = txt.Text.Trim().Length > 0;

			FormDatabase owner = txt.FindForm() as FormDatabase;
			if (owner != null)
				owner.ApplyFilters(owner.CometsInitial);
		}

		#endregion

		#region sdc_OnDateChanged

		private static void sdc_OnSelectedDatetimeChanged(object sender, DateTime dt)
		{
			Control control = sender as Control;
			SelectDateControl sdc = control.Parent as SelectDateControl ?? control as SelectDateControl;
			if (sdc != null)
			{
				sdc.SelectedDateTime = dt;
				sdc.Parent.Controls.OfType<CheckBox>().First().Checked = true;

				FormDatabase owner = sdc.FindForm() as FormDatabase;
				if (owner != null)
					owner.ApplyFilters(owner.CometsInitial);
			}
		}

		#endregion

		#region btnRemove_Click

		static void btnRemove_Click(object sender, EventArgs e)
		{
			Button btn = sender as Button;
			Panel panel = btn.Parent as Panel;
			ContainerControl container = panel.Parent as ContainerControl;

			int panelId = panel.Name.Int();

			Panel toRemove = null;
			Button addNew = null;

			foreach (Control c in container.Controls)
			{
				if (c is Panel && c.Name.Int() == panelId)
				{
					toRemove = c as Panel;
					if (addNew != null)
						break;
				}
				else if (c is Button && c.Name == "btnAddNew")
				{
					addNew = c as Button;
					if (toRemove != null)
						break;
				}
			}

			int offset = toRemove.Height + 6; //margin
			container.Controls.Remove(toRemove);

			int count = 0;
			//move other panels up
			foreach (Control c in container.Controls.OfType<Panel>())
			{
				count++;

				if (c.Name.Int() > panelId)
					c.Location = new Point(c.Location.X, c.Location.Y - offset);
			}

			addNew.Visible = count < 10;
			addNew.Location = new Point(addNew.Location.X, addNew.Location.Y - offset);
		}

		#endregion

		#endregion
	}
}
