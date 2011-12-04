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
	TButton *Button2;
	TButton *Button1;
	TLabel *Label1;
	TListBox *ListBox1;
	TLabel *Label2;
	TEdit *Edit1;
	TLabel *Label3;
	TEdit *Edit2;
	TLabel *Label4;
	TEdit *Edit3;
	TLabel *Label5;
	TEdit *Edit4;
	TEdit *Edit5;
	TEdit *Edit6;
	TLabel *Label6;
	TEdit *Edit7;
	TLabel *Label7;
	TEdit *Edit8;
	TLabel *Label8;
	TEdit *Edit9;
	TLabel *Label9;
	TEdit *Edit10;
	TLabel *Label10;
	TEdit *Edit11;
	TLabel *Label11;
	TEdit *Edit12;
	TLabel *Label12;
	TEdit *Edit13;
	TEdit *Edit14;
	TLabel *Label13;
	TLabel *Label14;
	TLabel *Label16;
	TLabel *Label17;
	TLabel *Label18;
	TLabel *Label19;
	TImage *Image1;
	void __fastcall Button1Click(TObject *Sender);
	void __fastcall Button2Click(TObject *Sender);
	void __fastcall ListBox1Click(TObject *Sender);
private:	// User declarations
public:		// User declarations
	__fastcall TFrame5(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TFrame5 *Frame5;
//---------------------------------------------------------------------------
#endif
