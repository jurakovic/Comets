object Frame4: TFrame4
  Left = 0
  Top = 0
  Width = 600
  Height = 400
  TabOrder = 0
  DesignSize = (
    600
    400)
  object Bevel1: TBevel
    Left = 7
    Top = 50
    Width = 586
    Height = 303
    Align = alCustom
    Anchors = [akLeft, akTop, akRight, akBottom]
    Shape = bsBottomLine
  end
  object Label4: TLabel
    Left = 16
    Top = 66
    Width = 46
    Height = 16
    Anchors = [akTop]
    Caption = 'Sort by:'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label5: TLabel
    Left = 16
    Top = 101
    Width = 55
    Height = 16
    Anchors = [akTop]
    Caption = 'Direction:'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label2: TLabel
    Left = 16
    Top = 322
    Width = 248
    Height = 16
    Anchors = [akLeft, akBottom]
    Caption = 'Import completed! N of N comets imported.'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    Visible = False
  end
  object ComboBox2: TComboBox
    Left = 168
    Top = 65
    Width = 161
    Height = 21
    Style = csDropDownList
    DropDownCount = 9
    ItemIndex = 0
    TabOrder = 0
    Text = 'Default'
    OnChange = ComboBox2Change
    Items.Strings = (
      'Default'
      'Name'
      'Perihelion Date'
      'Pericenter Distance'
      'Eccentricity'
      'Long. of the Asc. Node'
      'Long. of Pericenter'
      'Inclination'
      'Period')
  end
  object ComboBox3: TComboBox
    Left = 168
    Top = 100
    Width = 161
    Height = 21
    Style = csDropDownList
    Enabled = False
    TabOrder = 1
    OnChange = ComboBox3Change
    Items.Strings = (
      'Ascending'
      'Descending')
  end
  object Panel2: TPanel
    Left = 0
    Top = 0
    Width = 600
    Height = 50
    Align = alTop
    Color = cl3DLight
    ParentBackground = False
    TabOrder = 2
    object Label1: TLabel
      Left = 16
      Top = 12
      Width = 136
      Height = 21
      Caption = 'Import and sort'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -17
      Font.Name = 'Tahoma'
      Font.Style = [fsBold]
      ParentFont = False
    end
  end
  object BAbout: TButton
    Left = 11
    Top = 364
    Width = 25
    Height = 25
    Anchors = [akLeft, akBottom]
    Caption = '?'
    TabOrder = 3
    OnClick = BAboutClick
  end
  object BSettings: TButton
    Left = 42
    Top = 364
    Width = 75
    Height = 25
    Anchors = [akLeft, akBottom]
    Caption = 'Settings'
    TabOrder = 4
    OnClick = BSettingsClick
  end
  object Button2: TButton
    Left = 304
    Top = 364
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
    TabOrder = 5
    OnClick = Button2Click
  end
  object Button1: TButton
    Left = 385
    Top = 364
    Width = 75
    Height = 25
    Anchors = [akRight, akBottom]
    Caption = 'Next >'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 6
    OnClick = Button1Click
  end
  object BExit: TButton
    Left = 514
    Top = 364
    Width = 75
    Height = 25
    Anchors = [akRight, akBottom]
    Caption = 'Exit'
    TabOrder = 7
    OnClick = BExitClick
  end
  object Button3: TButton
    Left = 168
    Top = 184
    Width = 98
    Height = 33
    Caption = 'Import'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 8
    OnClick = Button3Click
  end
  object ProgressBar1: TProgressBar
    Left = 16
    Top = 287
    Width = 568
    Height = 24
    Anchors = [akLeft, akRight, akBottom]
    TabOrder = 9
    Visible = False
  end
end
