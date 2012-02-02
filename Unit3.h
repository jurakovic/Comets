//---------------------------------------------------------------------------

#ifndef Unit3H
#define Unit3H
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <Mask.hpp>
#include <ExtCtrls.hpp>
#include <pngimage.hpp>
#include <Vcl.ComCtrls.hpp>
//---------------------------------------------------------------------------
class TFrame3 : public TFrame
{
__published:	// IDE-managed Components
	TBevel *Bevel1;
	TLabel *Label2;
	TLabel *Label4;
	TLabel *Label5;
	TLabel *Label6;
	TLabel *Label7;
	TCheckBox *CheckBox1;
	TComboBox *ComboBox1;
	TEdit *EditD;
	TEdit *EditM;
	TEdit *EditY;
	TCheckBox *CheckBox2;
	TComboBox *ComboBox2;
	TEdit *Edit2;
	TCheckBox *CheckBox3;
	TComboBox *ComboBox3;
	TEdit *Edit3;
	TCheckBox *CheckBox4;
	TComboBox *ComboBox4;
	TEdit *Edit4;
	TCheckBox *CheckBox5;
	TComboBox *ComboBox5;
	TEdit *Edit5;
	TCheckBox *CheckBox6;
	TComboBox *ComboBox6;
	TEdit *Edit6;
	TCheckBox *CheckBox7;
	TComboBox *ComboBox7;
	TEdit *Edit7;
	TPanel *Panel2;
	TLabel *Label1;
	TPanel *Panel1;
	TButton *Button2;
	TButton *Button1;
	TCheckBox *CheckBox8;
	TButton *Button3;
	TButton *Button4;
	TProgressBar *ProgressBar1;
	void __fastcall Button1Click(TObject *Sender);
	void __fastcall Button2Click(TObject *Sender);
	void __fastcall CheckBox1Click(TObject *Sender);
	void __fastcall CheckBox2Click(TObject *Sender);
	void __fastcall CheckBox3Click(TObject *Sender);
	void __fastcall CheckBox4Click(TObject *Sender);
	void __fastcall CheckBox5Click(TObject *Sender);
	void __fastcall CheckBox6Click(TObject *Sender);
	void __fastcall CheckBox7Click(TObject *Sender);
	void __fastcall Edit2KeyPress(TObject *Sender, System::WideChar &Key);
	void __fastcall Button3Click(TObject *Sender);
	void __fastcall Button4Click(TObject *Sender);
private:	// User declarations
public:		// User declarations
	__fastcall TFrame3(TComponent* Owner);
	bool provjeriZnak(char);
	bool provjeriTocku(char *);
};
//---------------------------------------------------------------------------
extern PACKAGE TFrame3 *Frame3;
//---------------------------------------------------------------------------
#endif
