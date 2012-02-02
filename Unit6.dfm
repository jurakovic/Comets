object Frame6: TFrame6
  Left = 0
  Top = 0
  Width = 570
  Height = 400
  Align = alCustom
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
    Top = 194
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
    Width = 570
    Height = 50
    Align = alTop
    Color = cl3DLight
    ParentBackground = False
    TabOrder = 0
    ExplicitWidth = 600
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
  object Button3: TButton
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
    TabOrder = 1
    OnClick = Button3Click
  end
  object ComboBox1: TComboBox
    Left = 168
    Top = 65
    Width = 209
    Height = 21
    Style = csDropDownList
    DropDownCount = 19
    TabOrder = 2
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
    Left = 65
    Top = 146
    Width = 25
    Height = 25
    Anchors = [akTop, akRight]
    Caption = '...'
    TabOrder = 3
    OnClick = Button4Click
  end
  object Edit1: TEdit
    Left = 168
    Top = 107
    Width = 392
    Height = 24
    Anchors = [akLeft, akTop, akRight]
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 4
    OnChange = Edit1Change
  end
  object Button1: TButton
    Left = 11
    Top = 292
    Width = 98
    Height = 24
    Caption = 'Export'
    Enabled = False
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 5
    OnClick = Button1Click
  end
  object ProgressBar1: TProgressBar
    Left = 115
    Top = 292
    Width = 445
    Height = 24
    Anchors = [akLeft, akRight, akBottom]
    TabOrder = 6
    Visible = False
  end
  object Button2: TButton
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
    TabOrder = 7
    OnClick = Button2Click
  end
  object SaveDialog1: TSaveDialog
    Left = 456
    Top = 56
  end
end
