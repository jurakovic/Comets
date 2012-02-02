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
	TLabel *Label2;
	TLabel *Label3;
	TLabel *Label4;
	TLabel *Label5;
	TLabel *Label6;
	TLabel *Label7;
	TLabel *Label8;
	TLabel *Label9;
	TLabel *Label10;
	TLabel *Label11;
	TLabel *Label12;
	TLabel *Label13;
	TLabel *Label14;
	TLabel *Label15;
	TLabel *Label16;
	TLabel *Label17;
	TLabel *Label18;
	TPanel *Panel2;
	TLabel *Label1;
	TButton *Button2;
	TButton *Button1;
	TListBox *ListBox1;
	TEdit *Edit1;
	TEdit *Edit2;
	TEdit *Edit3;
	TEdit *Edit4;
	TEdit *Edit7;
	TEdit *Edit8;
	TEdit *Edit9;
	TEdit *Edit10;
	TEdit *Edit11;
	TEdit *Edit12;
	TEdit *Edit6;
	TEdit *Edit5;
	TEdit *Edit13;
	TEdit *Edit14;
	TEdit *Edit15;
	TLabel *Label19;
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
	void __fastcall Button1Click(TObject *Sender);
	void __fastcall Button2Click(TObject *Sender);
	void __fastcall ListBox1Click(TObject *Sender);
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

private:	// User declarations
public:		// User declarations
	__fastcall TFrame5(TComponent* Owner);
	void ocistiEditPolja();
	int Ncmt;
};
//---------------------------------------------------------------------------
extern PACKAGE TFrame5 *Frame5;
//---------------------------------------------------------------------------
#endif
