//---------------------------------------------------------------------------

#ifndef Unit4H
#define Unit4H
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <ComCtrls.hpp>

#include "CometMain.hpp"
#include <ExtCtrls.hpp>
#include <pngimage.hpp>
//---------------------------------------------------------------------------
class TFrame4 : public TFrame
{
__published:	// IDE-managed Components
	TButton *Button2;
	TButton *Button1;
	TLabel *Label1;
	TLabel *Label4;
	TComboBox *ComboBox2;
	TLabel *Label5;
	TComboBox *ComboBox3;
	TProgressBar *ProgressBar1;
	TLabel *Label3;
	TButton *Button3;
	TImage *Image1;
	TLabel *Label2;
	TCheckBox *CheckBox1;
	void __fastcall Button2Click(TObject *Sender);
	void __fastcall Button1Click(TObject *Sender);
	void __fastcall Button3Click(TObject *Sender);
	void __fastcall ComboBox2Change(TObject *Sender);
	void __fastcall ComboBox3Change(TObject *Sender);
private:	// User declarations
public:		// User declarations
	__fastcall TFrame4(TComponent* Owner);

	int Ncmt;
};
//---------------------------------------------------------------------------
extern PACKAGE TFrame4 *Frame4;
//---------------------------------------------------------------------------
#endif
