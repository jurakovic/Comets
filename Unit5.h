//---------------------------------------------------------------------------

#ifndef Unit5H
#define Unit5H
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
//---------------------------------------------------------------------------
class TForm5 : public TForm
{
__published:	// IDE-managed Components
	TListBox *ListBox1;
	TLabel *Label1;
	TButton *Button1;
	TButton *Button3;
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
	TButton *Button4;
	TButton *Button5;
	TButton *Button2;
	TGroupBox *GroupBox1;
	TGroupBox *GroupBox2;
	void __fastcall FormClose(TObject *Sender, TCloseAction &Action);
	void __fastcall Button1Click(TObject *Sender);
	void __fastcall Button6Click(TObject *Sender);
	void __fastcall ListBox1Click(TObject *Sender);
private:	// User declarations
public:		// User declarations
	__fastcall TForm5(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TForm5 *Form5;
//---------------------------------------------------------------------------
#endif
