object Frame03: TFrame03
  Left = 0
  Top = 0
  Width = 570
  Height = 400
  Align = alCustom
  TabOrder = 0
  DesignSize = (
    570
    400)
  object Label20: TLabel
    Left = 485
    Top = 324
    Width = 67
    Height = 13
    Caption = 'Comets: Ncmt'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
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
      Width = 148
      Height = 21
      Caption = 'Imported comets'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -17
      Font.Name = 'Tahoma'
      Font.Style = [fsBold]
      ParentFont = False
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
    TabOrder = 1
    OnClick = Button2Click
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
  object ListBox1: TListBox
    Left = 11
    Top = 64
    Width = 468
    Height = 273
    Anchors = [akLeft, akTop, akBottom]
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = []
    ItemHeight = 13
    ParentFont = False
    TabOrder = 3
    OnClick = ListBox1Click
    OnDblClick = ListBox1DblClick
    OnKeyPress = ListBox1KeyPress
  end
  object Button3: TButton
    Left = 485
    Top = 64
    Width = 75
    Height = 25
    Caption = 'Sort by'
    TabOrder = 4
    OnClick = Button3Click
  end
  object Button4: TButton
    Left = 485
    Top = 187
    Width = 75
    Height = 25
    Caption = 'Delete'
    TabOrder = 5
    OnClick = Button4Click
  end
  object Button5: TButton
    Left = 485
    Top = 218
    Width = 75
    Height = 25
    Caption = 'Clear'
    TabOrder = 6
    OnClick = Button5Click
  end
  object Button6: TButton
    Left = 485
    Top = 264
    Width = 75
    Height = 25
    Caption = 'Export'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 7
    OnClick = Button6Click
  end
  object Button7: TButton
    Left = 485
    Top = 141
    Width = 75
    Height = 25
    Caption = 'Details >>'
    TabOrder = 8
    OnClick = Button7Click
  end
  object Filters: TButton
    Left = 485
    Top = 95
    Width = 75
    Height = 25
    Caption = 'Filters'
    TabOrder = 9
    OnClick = FiltersClick
  end
  object PopupActionBar1: TPopupActionBar
    OnChange = PopupActionBar1Change
    Left = 208
    Top = 8
    object nosort2: TMenuItem
      Caption = '(no sort)'
      Checked = True
      RadioItem = True
      OnClick = nosort2Click
    end
    object name1: TMenuItem
      Caption = 'Name'
      RadioItem = True
      OnClick = name1Click
    end
    object PerihelionDate1: TMenuItem
      Caption = 'Perihelion Date'
      RadioItem = True
      OnClick = PerihelionDate1Click
    end
    object PericenterDistance1: TMenuItem
      Caption = 'Pericenter Distance'
      RadioItem = True
      OnClick = PericenterDistance1Click
    end
    object Eccentricity1: TMenuItem
      Caption = 'Eccentricity'
      RadioItem = True
      OnClick = Eccentricity1Click
    end
    object LongoftheAscNode1: TMenuItem
      Caption = 'Long. of the Asc. Node'
      RadioItem = True
      OnClick = LongoftheAscNode1Click
    end
    object LongofPericenter1: TMenuItem
      Caption = 'Long. of Pericenter'
      RadioItem = True
      OnClick = LongofPericenter1Click
    end
    object Inclination1: TMenuItem
      Caption = 'Inclination'
      RadioItem = True
      OnClick = Inclination1Click
    end
    object Period1: TMenuItem
      Caption = 'Period'
      RadioItem = True
      OnClick = Period1Click
    end
    object N1: TMenuItem
      Caption = '-'
    end
    object Ascending1: TMenuItem
      Caption = 'Ascending'
      Checked = True
      GroupIndex = 1
      RadioItem = True
      OnClick = Ascending1Click
    end
    object Descending1: TMenuItem
      Caption = 'Descending'
      GroupIndex = 1
      RadioItem = True
      OnClick = Descending1Click
    end
  end
end
