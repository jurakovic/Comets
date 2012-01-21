object Frame6: TFrame6
  Left = 0
  Top = 0
  Width = 600
  Height = 400
  Align = alCustom
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
  object Label2: TLabel
    Left = 16
    Top = 66
    Width = 85
    Height = 16
    Caption = 'Select Format:'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label3: TLabel
    Left = 16
    Top = 110
    Width = 51
    Height = 16
    Caption = 'Save As:'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label4: TLabel
    Left = 16
    Top = 322
    Width = 103
    Height = 16
    Anchors = [akLeft, akBottom]
    Caption = 'Export completed!'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    Visible = False
  end
  object Panel2: TPanel
    Left = 0
    Top = 0
    Width = 600
    Height = 50
    Align = alTop
    Color = cl3DLight
    ParentBackground = False
    TabOrder = 0
    object Label1: TLabel
      Left = 16
      Top = 12
      Width = 56
      Height = 21
      Caption = 'Export'
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
    TabOrder = 1
    OnClick = BAboutClick
  end
  object BSettings: TButton
    Left = 42
    Top = 364
    Width = 75
    Height = 25
    Anchors = [akLeft, akBottom]
    Caption = 'Settings'
    TabOrder = 2
    OnClick = BSettingsClick
  end
  object Button3: TButton
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
    TabOrder = 3
    OnClick = Button3Click
  end
  object Button2: TButton
    Left = 385
    Top = 364
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
    TabOrder = 4
    OnClick = Button2Click
  end
  object BExit: TButton
    Left = 514
    Top = 364
    Width = 75
    Height = 25
    Anchors = [akRight, akBottom]
    Caption = 'Exit'
    TabOrder = 5
    OnClick = BExitClick
  end
  object ComboBox1: TComboBox
    Left = 168
    Top = 65
    Width = 209
    Height = 21
    Style = csDropDownList
    DropDownCount = 19
    TabOrder = 6
    OnChange = ComboBox1Change
    Items.Strings = (
      'MPC (Soft00Cmt)'
      'SkyMap (Soft01Cmt)'
      'Guide (Soft02Cmt)'
      'xephem (Soft03Cmt)'
      'Home Planet (Soft04Cmt)'
      'MyStars! (Soft05Cmt)'
      'TheSky (Soft06Cmt)'
      'Starry Night (Soft07Cmt)'
      'Deep Space (Soft08Cmt)'
      'PC-TCS (Soft09Cmt)'
      'Earth Centered Universe (Soft10Cmt)'
      'Dance of the Planets (Soft11Cmt)'
      'MegaStar V4.x (Soft12Cmt)'
      'SkyChart III (Soft13Cmt)'
      'Voyager II (Soft14Cmt)'
      'SkyTools (Soft15Cmt)'
      'Autostar (Soft16Cmt)'
      'Celestia (SSC Format)'
      'Stellarium')
  end
  object Button4: TButton
    Left = 543
    Top = 106
    Width = 25
    Height = 25
    Anchors = [akTop, akRight]
    Caption = '...'
    TabOrder = 7
    OnClick = Button4Click
  end
  object Edit1: TEdit
    Left = 168
    Top = 107
    Width = 369
    Height = 24
    Anchors = [akLeft, akTop, akRight]
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 8
    OnChange = Edit1Change
  end
  object Button1: TButton
    Left = 168
    Top = 184
    Width = 98
    Height = 33
    Caption = 'Export'
    Enabled = False
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 9
    OnClick = Button1Click
  end
  object ProgressBar1: TProgressBar
    Left = 16
    Top = 287
    Width = 568
    Height = 24
    Anchors = [akLeft, akRight, akBottom]
    TabOrder = 10
    Visible = False
  end
  object SaveDialog1: TSaveDialog
    Filter = 'Text Document|*.txt|INI File|*.ini|DAT File|*.dat|All Files|*.*'
    Left = 120
    Top = 104
  end
end
