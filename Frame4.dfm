object Frame04: TFrame04
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
    Top = 168
    Width = 160
    Height = 14
    Caption = 'All data successfully exported'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
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
    Left = 115
    Top = 65
    Width = 209
    Height = 21
    Style = csDropDownList
    DropDownCount = 19
    ItemIndex = 0
    TabOrder = 2
    Text = 'MPC (Soft00Cmt)'
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
  object Button1: TButton
    Left = 11
    Top = 116
    Width = 98
    Height = 24
    Caption = 'Export'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 3
    OnClick = Button1Click
  end
  object ProgressBar1: TProgressBar
    Left = 115
    Top = 116
    Width = 445
    Height = 24
    Anchors = [akLeft, akRight, akBottom]
    TabOrder = 4
    Visible = False
  end
  object SaveDialog1: TSaveDialog
    Left = 456
    Top = 56
  end
end
