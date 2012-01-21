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
	TLabel *Label4;
	TLabel *Label5;
	TComboBox *ComboBox2;
	TComboBox *ComboBox3;
	TPanel *Panel2;
	TLabel *Label1;
	TButton *BAbout;
	TButton *BSettings;
	TButton *Button2;
	TButton *Button1;
	TButton *BExit;
	TButton *Button3;
	TProgressBar *ProgressBar1;
	TLabel *Label2;
	TBevel *Bevel1;
	void __fastcall Button2Click(TObject *Sender);
	void __fastcall Button1Click(TObject *Sender);
	void __fastcall Button3Click(TObject *Sender);
	void __fastcall ComboBox2Change(TObject *Sender);
	void __fastcall ComboBox3Change(TObject *Sender);
	void __fastcall BAboutClick(TObject *Sender);
	void __fastcall BSettingsClick(TObject *Sender);
	void __fastcall BExitClick(TObject *Sender);
private:	// User declarations
public:		// User declarations
	__fastcall TFrame4(TComponent* Owner);

	int Ncmt;
};
//---------------------------------------------------------------------------
extern PACKAGE TFrame4 *Frame4;
//---------------------------------------------------------------------------
#endif
