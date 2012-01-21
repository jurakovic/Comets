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
	TButton *BAbout;
	TButton *BSettings;
	TButton *Button2;
	TButton *Button1;
	TButton *BExit;
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
	TBevel *Bevel1;
	TEdit *Edit15;
	TLabel *Label19;
	void __fastcall Button1Click(TObject *Sender);
	void __fastcall Button2Click(TObject *Sender);
	void __fastcall ListBox1Click(TObject *Sender);
	void __fastcall BAboutClick(TObject *Sender);
	void __fastcall BSettingsClick(TObject *Sender);
	void __fastcall BExitClick(TObject *Sender);
private:	// User declarations
public:		// User declarations
	__fastcall TFrame5(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TFrame5 *Frame5;
//---------------------------------------------------------------------------
#endif
