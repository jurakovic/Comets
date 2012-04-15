object Frame01: TFrame01
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
  object CheckBox1: TCheckBox
    Left = 16
    Top = 369
    Width = 113
    Height = 17
    Anchors = [akLeft, akBottom]
    Caption = 'Advanced mode'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 0
  end
  object Panel2: TPanel
    Left = 0
    Top = 0
    Width = 570
    Height = 50
    Align = alTop
    Color = cl3DLight
    ParentBackground = False
    TabOrder = 1
    object Label1: TLabel
      Left = 16
      Top = 12
      Width = 123
      Height = 21
      Caption = 'Import format'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -17
      Font.Name = 'Tahoma'
      Font.Style = [fsBold]
      ParentFont = False
    end
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
    TabOrder = 2
    OnClick = Button1Click
  end
  object GroupBox1: TGroupBox
    Left = 10
    Top = 56
    Width = 550
    Height = 193
    Caption = 'Select Import Format'
    TabOrder = 3
    DesignSize = (
      550
      193)
    object Label4: TLabel
      Left = 16
      Top = 168
      Width = 104
      Height = 16
      Caption = 'Detected comets: '
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -13
      Font.Name = 'Tahoma'
      Font.Style = []
      ParentFont = False
      Visible = False
    end
    object ComboBox1: TComboBox
      Left = 16
      Top = 24
      Width = 209
      Height = 21
      Style = csDropDownList
      DropDownCount = 20
      ItemIndex = 0
      TabOrder = 0
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
        'Comet for Windows (Comet.dat)'
        'NASA (ELEMENTS.COMET)')
    end
    object RadioButton1: TRadioButton
      Left = 16
      Top = 54
      Width = 225
      Height = 17
      Caption = 'Download the latest orbital elements'
      Checked = True
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -13
      Font.Name = 'Tahoma'
      Font.Style = []
      ParentFont = False
      TabOrder = 1
      TabStop = True
      OnClick = RadioButton1Click
    end
    object Button3: TButton
      Left = 40
      Top = 77
      Width = 98
      Height = 25
      Caption = 'Download'
      TabOrder = 2
      OnClick = Button3Click
    end
    object ProgressBar1: TProgressBar
      Left = 144
      Top = 77
      Width = 391
      Height = 25
      Anchors = [akLeft, akTop, akRight]
      TabOrder = 3
      Visible = False
    end
    object RadioButton2: TRadioButton
      Left = 16
      Top = 111
      Width = 102
      Height = 17
      Caption = 'Use a local file'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -13
      Font.Name = 'Tahoma'
      Font.Style = []
      ParentFont = False
      TabOrder = 4
      OnClick = RadioButton2Click
    end
    object Button4: TButton
      Left = 40
      Top = 134
      Width = 98
      Height = 25
      Caption = 'Browse'
      Enabled = False
      TabOrder = 5
      OnClick = Button4Click
    end
    object Edit1: TEdit
      Left = 144
      Top = 134
      Width = 391
      Height = 24
      Anchors = [akLeft, akTop, akRight]
      Enabled = False
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -13
      Font.Name = 'Tahoma'
      Font.Style = []
      ParentFont = False
      ReadOnly = True
      TabOrder = 6
    end
  end
  object GroupBox2: TGroupBox
    Left = 10
    Top = 255
    Width = 550
    Height = 82
    Caption = 'Import Data'
    TabOrder = 4
    DesignSize = (
      550
      82)
    object Label2: TLabel
      Left = 16
      Top = 56
      Width = 121
      Height = 16
      Caption = 'N/N imported comets'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -13
      Font.Name = 'Tahoma'
      Font.Style = []
      ParentFont = False
      Visible = False
    end
    object Button2: TButton
      Left = 40
      Top = 22
      Width = 98
      Height = 25
      Caption = 'Import'
      Enabled = False
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -12
      Font.Name = 'Tahoma'
      Font.Style = [fsBold]
      ParentFont = False
      TabOrder = 0
      OnClick = Button2Click
    end
    object ProgressBar2: TProgressBar
      Left = 144
      Top = 22
      Width = 391
      Height = 25
      Anchors = [akLeft, akTop, akRight]
      TabOrder = 1
      Visible = False
    end
  end
  object H1: TIdHTTP
    OnWork = H1Work
    OnWorkBegin = H1WorkBegin
    AllowCookies = True
    ProxyParams.BasicAuthentication = False
    ProxyParams.ProxyPort = 0
    Request.ContentLength = -1
    Request.ContentRangeEnd = -1
    Request.ContentRangeStart = -1
    Request.ContentRangeInstanceLength = -1
    Request.Accept = 'text/html, */*'
    Request.BasicAuthentication = False
    Request.UserAgent = 'Mozilla/3.0 (compatible; Indy Library)'
    Request.Ranges.Units = 'bytes'
    Request.Ranges = <>
    HTTPOptions = [hoForceEncodeParams]
    Left = 480
    Top = 96
  end
  object OpenDialog1: TOpenDialog
    Left = 448
    Top = 184
  end
end
