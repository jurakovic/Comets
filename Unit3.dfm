object Frame3: TFrame3
  Left = 0
  Top = 0
  Width = 570
  Height = 400
  Align = alCustom
  Color = clBtnFace
  ParentBackground = False
  ParentColor = False
  TabOrder = 0
  DesignSize = (
    570
    400)
  object Bevel1: TBevel
    Left = 7
    Top = 50
    Width = 556
    Height = 303
    Align = alCustom
    Anchors = [akLeft, akTop, akRight, akBottom]
    Shape = bsBottomLine
    ExplicitWidth = 586
  end
  object Label2: TLabel
    Left = 476
    Top = 94
    Width = 16
    Height = 16
    Anchors = [akTop, akRight]
    Caption = 'AU'
    Enabled = False
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label4: TLabel
    Left = 476
    Top = 155
    Width = 6
    Height = 16
    Anchors = [akTop, akRight]
    Caption = #176
    Enabled = False
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label5: TLabel
    Left = 476
    Top = 185
    Width = 6
    Height = 16
    Anchors = [akTop, akRight]
    Caption = #176
    Enabled = False
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label6: TLabel
    Left = 476
    Top = 215
    Width = 6
    Height = 16
    Anchors = [akTop, akRight]
    Caption = #176
    Enabled = False
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label7: TLabel
    Left = 476
    Top = 245
    Width = 31
    Height = 16
    Anchors = [akTop, akRight]
    Caption = 'years'
    Enabled = False
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object CheckBox1: TCheckBox
    Left = 16
    Top = 66
    Width = 109
    Height = 17
    Caption = 'Perihelion Date'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 0
    OnClick = CheckBox1Click
  end
  object ComboBox1: TComboBox
    Left = 269
    Top = 63
    Width = 121
    Height = 21
    Style = csDropDownList
    Anchors = [akTop, akRight]
    Enabled = False
    TabOrder = 1
    Items.Strings = (
      'Greather than (>)'
      'Less than (<)')
  end
  object EditD: TEdit
    Left = 404
    Top = 62
    Width = 26
    Height = 22
    Hint = 'Day'
    Anchors = [akTop, akRight]
    Enabled = False
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Tahoma'
    Font.Style = []
    MaxLength = 2
    NumbersOnly = True
    ParentFont = False
    ParentShowHint = False
    ShowHint = True
    TabOrder = 2
  end
  object EditM: TEdit
    Left = 436
    Top = 62
    Width = 26
    Height = 22
    Hint = 'Month'
    Anchors = [akTop, akRight]
    Enabled = False
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Tahoma'
    Font.Style = []
    MaxLength = 2
    NumbersOnly = True
    ParentFont = False
    ParentShowHint = False
    ShowHint = True
    TabOrder = 3
  end
  object EditY: TEdit
    Left = 468
    Top = 62
    Width = 39
    Height = 22
    Hint = 'Year'
    Anchors = [akTop, akRight]
    Enabled = False
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Tahoma'
    Font.Style = []
    MaxLength = 4
    NumbersOnly = True
    ParentFont = False
    ParentShowHint = False
    ShowHint = True
    TabOrder = 4
  end
  object CheckBox2: TCheckBox
    Left = 16
    Top = 96
    Width = 137
    Height = 17
    Caption = 'Pericenter Distance'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 5
    OnClick = CheckBox2Click
  end
  object ComboBox2: TComboBox
    Left = 269
    Top = 93
    Width = 121
    Height = 21
    Style = csDropDownList
    Anchors = [akTop, akRight]
    Enabled = False
    TabOrder = 6
    Items.Strings = (
      'Greather than (>)'
      'Less than (<)')
  end
  object Edit2: TEdit
    Left = 404
    Top = 93
    Width = 66
    Height = 22
    Anchors = [akTop, akRight]
    Enabled = False
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Tahoma'
    Font.Style = []
    MaxLength = 5
    ParentFont = False
    ParentShowHint = False
    ShowHint = True
    TabOrder = 7
    OnKeyPress = Edit2KeyPress
  end
  object CheckBox3: TCheckBox
    Left = 16
    Top = 126
    Width = 81
    Height = 17
    Caption = 'Eccentricity'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 8
    OnClick = CheckBox3Click
  end
  object ComboBox3: TComboBox
    Left = 269
    Top = 123
    Width = 121
    Height = 21
    Style = csDropDownList
    Anchors = [akTop, akRight]
    Enabled = False
    TabOrder = 9
    Items.Strings = (
      'Greather than (>)'
      'Less than (<)')
  end
  object Edit3: TEdit
    Left = 404
    Top = 123
    Width = 66
    Height = 22
    Anchors = [akTop, akRight]
    Enabled = False
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Tahoma'
    Font.Style = []
    MaxLength = 5
    ParentFont = False
    ParentShowHint = False
    ShowHint = True
    TabOrder = 10
  end
  object CheckBox4: TCheckBox
    Left = 16
    Top = 156
    Width = 209
    Height = 17
    Caption = 'Longitude of the Ascending Node'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 11
    OnClick = CheckBox4Click
  end
  object ComboBox4: TComboBox
    Left = 269
    Top = 153
    Width = 121
    Height = 21
    Style = csDropDownList
    Anchors = [akTop, akRight]
    Enabled = False
    TabOrder = 12
    Items.Strings = (
      'Greather than (>)'
      'Less than (<)')
  end
  object Edit4: TEdit
    Left = 404
    Top = 153
    Width = 66
    Height = 22
    Anchors = [akTop, akRight]
    Enabled = False
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Tahoma'
    Font.Style = []
    MaxLength = 5
    ParentFont = False
    ParentShowHint = False
    ShowHint = True
    TabOrder = 13
  end
  object CheckBox5: TCheckBox
    Left = 16
    Top = 186
    Width = 153
    Height = 17
    Caption = 'Longitude of Pericenter'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 14
    OnClick = CheckBox5Click
  end
  object ComboBox5: TComboBox
    Left = 269
    Top = 183
    Width = 121
    Height = 21
    Style = csDropDownList
    Anchors = [akTop, akRight]
    Enabled = False
    TabOrder = 15
    Items.Strings = (
      'Greather than (>)'
      'Less than (<)')
  end
  object Edit5: TEdit
    Left = 404
    Top = 183
    Width = 66
    Height = 22
    Anchors = [akTop, akRight]
    Enabled = False
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Tahoma'
    Font.Style = []
    MaxLength = 5
    ParentFont = False
    ParentShowHint = False
    ShowHint = True
    TabOrder = 16
  end
  object CheckBox6: TCheckBox
    Left = 16
    Top = 216
    Width = 81
    Height = 17
    Caption = 'Inclination'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 17
    OnClick = CheckBox6Click
  end
  object ComboBox6: TComboBox
    Left = 269
    Top = 213
    Width = 121
    Height = 21
    Style = csDropDownList
    Anchors = [akTop, akRight]
    Enabled = False
    TabOrder = 18
    Items.Strings = (
      'Greather than (>)'
      'Less than (<)')
  end
  object Edit6: TEdit
    Left = 404
    Top = 213
    Width = 66
    Height = 22
    Anchors = [akTop, akRight]
    Enabled = False
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Tahoma'
    Font.Style = []
    MaxLength = 5
    ParentFont = False
    ParentShowHint = False
    ShowHint = True
    TabOrder = 19
  end
  object CheckBox7: TCheckBox
    Left = 16
    Top = 246
    Width = 57
    Height = 17
    Caption = 'Period'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 20
    OnClick = CheckBox7Click
  end
  object ComboBox7: TComboBox
    Left = 269
    Top = 243
    Width = 121
    Height = 21
    Style = csDropDownList
    Anchors = [akTop, akRight]
    Enabled = False
    TabOrder = 21
    Items.Strings = (
      'Greather than (>)'
      'Less than (<)')
  end
  object Edit7: TEdit
    Left = 404
    Top = 243
    Width = 66
    Height = 22
    Anchors = [akTop, akRight]
    Enabled = False
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Tahoma'
    Font.Style = []
    MaxLength = 5
    ParentFont = False
    ParentShowHint = False
    ShowHint = True
    TabOrder = 22
  end
  object Panel2: TPanel
    Left = 0
    Top = 0
    Width = 570
    Height = 50
    Align = alTop
    Color = cl3DLight
    ParentBackground = False
    TabOrder = 23
    ExplicitWidth = 600
    DesignSize = (
      570
      50)
    object Label1: TLabel
      Left = 16
      Top = 12
      Width = 211
      Height = 21
      Caption = 'Exclude data and Import'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -17
      Font.Name = 'Tahoma'
      Font.Style = [fsBold]
      ParentFont = False
    end
    object Panel1: TPanel
      Left = 32
      Top = 56
      Width = 554
      Height = 1
      Anchors = [akLeft, akRight, akBottom]
      TabOrder = 0
      ExplicitWidth = 584
    end
  end
  object Button2: TButton
    Left = 404
    Top = 365
    Width = 75
    Height = 25
    Anchors = [akRight, akBottom]
    Caption = '< Back'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 24
    OnClick = Button2Click
    ExplicitLeft = 434
  end
  object Button1: TButton
    Left = 485
    Top = 365
    Width = 75
    Height = 25
    Anchors = [akRight, akBottom]
    Caption = 'Next >'
    Enabled = False
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 25
    OnClick = Button1Click
    ExplicitLeft = 515
  end
  object CheckBox8: TCheckBox
    Left = 16
    Top = 275
    Width = 153
    Height = 17
    Caption = 'Exclude SOHO comets'
    Checked = True
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    State = cbChecked
    TabOrder = 26
  end
  object Button3: TButton
    Left = 513
    Top = 64
    Width = 17
    Height = 17
    Anchors = [akTop, akRight]
    Enabled = False
    TabOrder = 27
    OnClick = Button3Click
  end
  object Button4: TButton
    Left = 11
    Top = 314
    Width = 107
    Height = 25
    Caption = 'Import'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 28
    OnClick = Button4Click
  end
  object ProgressBar1: TProgressBar
    Left = 124
    Top = 314
    Width = 435
    Height = 24
    Anchors = [akLeft, akTop, akRight]
    TabOrder = 29
    Visible = False
    ExplicitWidth = 465
  end
end
