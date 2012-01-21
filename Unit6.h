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
	TLabel *Label2;
	TLabel *Label3;
	TPanel *Panel2;
	TLabel *Label1;
	TButton *BAbout;
	TButton *BSettings;
	TButton *Button3;
	TButton *Button2;
	TButton *BExit;
	TComboBox *ComboBox1;
	TButton *Button4;
	TEdit *Edit1;
	TSaveDialog *SaveDialog1;
	TLabel *Label4;
	TButton *Button1;
	TProgressBar *ProgressBar1;
	TBevel *Bevel1;
	void __fastcall Button2Click(TObject *Sender);
	void __fastcall Button3Click(TObject *Sender);
	void __fastcall Button4Click(TObject *Sender);
	void __fastcall ComboBox1Change(TObject *Sender);
	void __fastcall Edit1Change(TObject *Sender);
	void __fastcall Button1Click(TObject *Sender);
	void __fastcall BAboutClick(TObject *Sender);
	void __fastcall BSettingsClick(TObject *Sender);
	void __fastcall BExitClick(TObject *Sender);
private:	// User declarations
public:		// User declarations
	__fastcall TFrame6(TComponent* Owner);

};
//---------------------------------------------------------------------------
extern PACKAGE TFrame6 *Frame6;
//---------------------------------------------------------------------------
#endif
