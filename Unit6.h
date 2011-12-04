//---------------------------------------------------------------------------

#ifndef Unit6H
#define Unit6H

#include "Unit1.h"
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <Dialogs.hpp>
#include <ExtCtrls.hpp>
#include <pngimage.hpp>
#include <ComCtrls.hpp>
//---------------------------------------------------------------------------
class TFrame6 : public TFrame
{
__published:	// IDE-managed Components
	TButton *Button2;
	TLabel *Label1;
	TLabel *Label2;
	TComboBox *ComboBox1;
	TLabel *Label3;
	TButton *Button4;
	TEdit *Edit1;
	TButton *Button1;
	TButton *Button3;
	TSaveDialog *SaveDialog1;
	TImage *Image1;
	TLabel *Label4;
	TProgressBar *ProgressBar1;
	void __fastcall Button2Click(TObject *Sender);
	void __fastcall Button3Click(TObject *Sender);
	void __fastcall Button4Click(TObject *Sender);
	void __fastcall ComboBox1Change(TObject *Sender);
	void __fastcall Edit1Change(TObject *Sender);
	void __fastcall Button1Click(TObject *Sender);
private:	// User declarations
public:		// User declarations
	__fastcall TFrame6(TComponent* Owner);

};
//---------------------------------------------------------------------------
extern PACKAGE TFrame6 *Frame6;
//---------------------------------------------------------------------------
#endif
