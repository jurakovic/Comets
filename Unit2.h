//---------------------------------------------------------------------------

#ifndef Unit2H
#define Unit2H
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
class TFrame2 : public TFrame
{
__published:	// IDE-managed Components
	TLabel *Label2;
	TLabel *Label4;
	TComboBox *ComboBox1;
	TRadioButton *RadioButton1;
	TButton *Button3;
	TRadioButton *RadioButton2;
	TButton *Button4;
	TEdit *Edit1;
	TProgressBar *ProgressBar1;
	TCheckBox *CheckBox1;
	TPanel *Panel2;
	TLabel *Label1;
	TIdHTTP *H1;
	TOpenDialog *OpenDialog1;
	TBevel *Bevel1;
	TButton *Button1;
	void __fastcall Button1Click(TObject *Sender);
	void __fastcall RadioButton1Click(TObject *Sender);
	void __fastcall RadioButton2Click(TObject *Sender);
	void __fastcall Button3Click(TObject *Sender);
	void __fastcall Button4Click(TObject *Sender);
	void __fastcall ComboBox1Change(TObject *Sender);
	void __fastcall H1WorkBegin(TObject *ASender, TWorkMode AWorkMode, __int64 AWorkCountMax);
	void __fastcall H1Work(TObject *ASender, TWorkMode AWorkMode, __int64 AWorkCount);

private:	// User declarations
public:		// User declarations
	__fastcall TFrame2(TComponent* Owner);
	void setDetectedComets();
	int getImportType(FILE *);
	bool checkImportType();
	bool isFileDownloaded;
	UnicodeString downloadedFile;
	FILE *fin;
	int detectedComets;
};
//---------------------------------------------------------------------------
extern PACKAGE TFrame2 *Frame2;
//---------------------------------------------------------------------------
#endif
