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
//---------------------------------------------------------------------------
class TFrame3 : public TFrame
{
__published:	// IDE-managed Components
	TLabel *Label1;
	TCheckBox *CheckBox1;
	TComboBox *ComboBox1;
	TCheckBox *CheckBox2;
	TComboBox *ComboBox2;
	TLabel *Label2;
	TCheckBox *CheckBox3;
	TComboBox *ComboBox3;
	TCheckBox *CheckBox4;
	TComboBox *ComboBox4;
	TLabel *Label4;
	TCheckBox *CheckBox5;
	TComboBox *ComboBox5;
	TLabel *Label5;
	TCheckBox *CheckBox6;
	TComboBox *ComboBox6;
	TLabel *Label6;
	TCheckBox *CheckBox7;
	TComboBox *ComboBox7;
	TLabel *Label7;
	TButton *Button2;
	TButton *Button1;
	TEdit *Edit2;
	TEdit *Edit3;
	TEdit *Edit4;
	TEdit *Edit5;
	TEdit *Edit6;
	TEdit *Edit7;
	TMaskEdit *MaskEdit1;
	TImage *Image1;
	void __fastcall Button1Click(TObject *Sender);
	void __fastcall Button2Click(TObject *Sender);
	void __fastcall CheckBox1Click(TObject *Sender);
	void __fastcall CheckBox2Click(TObject *Sender);
	void __fastcall CheckBox3Click(TObject *Sender);
	void __fastcall CheckBox4Click(TObject *Sender);
	void __fastcall CheckBox5Click(TObject *Sender);
	void __fastcall CheckBox6Click(TObject *Sender);
	void __fastcall CheckBox7Click(TObject *Sender);
private:	// User declarations
public:		// User declarations
	__fastcall TFrame3(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TFrame3 *Frame3;
//---------------------------------------------------------------------------
#endif
