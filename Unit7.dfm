object Form8: TForm8
  Left = 0
  Top = 0
  Caption = 'Form8'
  ClientHeight = 150
  ClientWidth = 353
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object GroupBox1: TGroupBox
    Left = 8
    Top = 8
    Width = 337
    Height = 89
    Caption = 'Export Format'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 0
    object Label1: TLabel
      Left = 16
      Top = 32
      Width = 82
      Height = 14
      Caption = 'Export Format:'
    end
    object Label2: TLabel
      Left = 16
      Top = 60
      Width = 47
      Height = 14
      Caption = 'Save As:'
    end
    object import_combo: TComboBox
      Left = 120
      Top = 29
      Width = 206
      Height = 22
      Hint = 'brb'
      Style = csDropDownList
      DropDownCount = 18
      ParentShowHint = False
      ShowHint = True
      TabOrder = 0
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
        'Autostar (Soft16Cmt)')
    end
    object brbt: TButton
      Left = 120
      Top = 57
      Width = 75
      Height = 23
      Caption = 'Browse'
      Enabled = False
      TabOrder = 1
    end
  end
  object Button1: TButton
    Left = 112
    Top = 103
    Width = 121
    Height = 40
    Caption = 'Export'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 1
  end
end
