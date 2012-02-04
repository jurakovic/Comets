//---------------------------------------------------------------------------

#ifndef Unit5H
#define Unit5H
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <ExtCtrls.hpp>
#include <pngimage.hpp>
#include <Vcl.ActnPopup.hpp>
#include <Vcl.Menus.hpp>
#include <Vcl.PlatformDefaultStyleActnCtrls.hpp>
#include <Vcl.ComCtrls.hpp>
#include <Vcl.ToolWin.hpp>
//---------------------------------------------------------------------------
class TFrame5 : public TFrame
{
__published:	// IDE-managed Components
	TPanel *Panel2;
	TLabel *Label1;
	TButton *Button2;
	TButton *Button1;
	TListBox *ListBox1;
	TPopupActionBar *PopupActionBar1;
	TMenuItem *nosort2;
	TMenuItem *name1;
	TMenuItem *PerihelionDate1;
	TMenuItem *PericenterDistance1;
	TMenuItem *Eccentricity1;
	TMenuItem *N1;
	TButton *Button3;
	TMenuItem *LongoftheAscNode1;
	TMenuItem *LongofPericenter1;
	TMenuItem *Inclination1;
	TMenuItem *Period1;
	TMenuItem *Ascending1;
	TMenuItem *Descending1;
	TButton *Button4;
	TButton *Button5;
	TBevel *Bevel1;
	TLabel *Label20;
	TButton *Button6;
	TButton *Button7;
	void __fastcall Button1Click(TObject *Sender);
	void __fastcall Button2Click(TObject *Sender);
	void __fastcall Button3Click(TObject *Sender);
	void __fastcall nosort2Click(TObject *Sender);
	void __fastcall name1Click(TObject *Sender);
	void __fastcall PerihelionDate1Click(TObject *Sender);
	void __fastcall PericenterDistance1Click(TObject *Sender);
	void __fastcall Eccentricity1Click(TObject *Sender);
	void __fastcall LongoftheAscNode1Click(TObject *Sender);
	void __fastcall LongofPericenter1Click(TObject *Sender);
	void __fastcall Inclination1Click(TObject *Sender);
	void __fastcall Period1Click(TObject *Sender);
	void __fastcall Ascending1Click(TObject *Sender);
	void __fastcall Descending1Click(TObject *Sender);
	void __fastcall Button5Click(TObject *Sender);
	void __fastcall PopupActionBar1Change(TObject *Sender, TMenuItem *Source, bool Rebuild);
	void __fastcall Button4Click(TObject *Sender);
	void __fastcall ListBox1KeyPress(TObject *Sender, System::WideChar &Key);
	void __fastcall Button7Click(TObject *Sender);
	void __fastcall ListBox1Click(TObject *Sender);
	void __fastcall Button6Click(TObject *Sender);
	void __fastcall ListBox1DblClick(TObject *Sender);

private:	// User declarations
public:		// User declarations
	__fastcall TFrame5(TComponent* Owner);
	int Ncmt;
	bool canDoChange;
};
//---------------------------------------------------------------------------
extern PACKAGE TFrame5 *Frame5;
//---------------------------------------------------------------------------
#endif
