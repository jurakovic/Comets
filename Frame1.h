//---------------------------------------------------------------------------

#ifndef Frame01H
#define Frame01H
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>

#include "CometMain.hpp"

#include "IdBaseComponent.hpp"
#include "IdComponent.hpp"
#include "IdHTTP.hpp"
#include "IdTCPClient.hpp"
#include "IdTCPConnection.hpp"
#include <Dialogs.hpp>
#include <ComCtrls.hpp>
#include <ExtCtrls.hpp>
#include <pngimage.hpp>
#include <Vcl.Menus.hpp>

//---------------------------------------------------------------------------
class TFrame01 : public TFrame
{
__published:	// IDE-managed Components
	TCheckBox *CheckBox1;
	TPanel *Panel2;
	TLabel *Label1;
	TIdHTTP *H1;
	TOpenDialog *OpenDialog1;
	TBevel *Bevel1;
	TButton *Button1;
	TGroupBox *GroupBox1;
	TComboBox *ComboBox1;
	TRadioButton *RadioButton1;
	TButton *Button3;
	TProgressBar *ProgressBar1;
	TRadioButton *RadioButton2;
	TButton *Button4;
	TEdit *Edit1;
	TLabel *Label4;
	TGroupBox *GroupBox2;
	TButton *Button2;
	TProgressBar *ProgressBar2;
	TLabel *Label2;
	void __fastcall Button1Click(TObject *Sender);
	void __fastcall RadioButton1Click(TObject *Sender);
	void __fastcall RadioButton2Click(TObject *Sender);
	void __fastcall Button3Click(TObject *Sender);
	void __fastcall Button4Click(TObject *Sender);
	void __fastcall ComboBox1Change(TObject *Sender);
	void __fastcall H1WorkBegin(TObject *ASender, TWorkMode AWorkMode, __int64 AWorkCountMax);
	void __fastcall H1Work(TObject *ASender, TWorkMode AWorkMode, __int64 AWorkCount);
	void __fastcall Button2Click(TObject *Sender);

private:	// User declarations
public:		// User declarations
	__fastcall TFrame01(TComponent* Owner);
	void setDetectedComets();
	int getImportType(FILE *);
	bool checkImportType();
	bool isFileDownloaded;
	UnicodeString downloadedFile;
	FILE *fin;
	int detectedComets;
};
//---------------------------------------------------------------------------
extern PACKAGE TFrame01 *Frame01;
//---------------------------------------------------------------------------
#endif
