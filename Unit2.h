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
	TButton *BAbout;
	TButton *BSettings;
	TButton *BBack;
	TButton *Button1;
	TButton *BExit;
	void __fastcall Button1Click(TObject *Sender);
	void __fastcall RadioButton1Click(TObject *Sender);
	void __fastcall RadioButton2Click(TObject *Sender);
	void __fastcall Button3Click(TObject *Sender);
	void __fastcall Button4Click(TObject *Sender);
	void __fastcall ComboBox1Change(TObject *Sender);
	void __fastcall H1WorkBegin(TObject *ASender, TWorkMode AWorkMode, __int64 AWorkCountMax);
	void __fastcall H1Work(TObject *ASender, TWorkMode AWorkMode, __int64 AWorkCount);
	void __fastcall BAboutClick(TObject *Sender);
	void __fastcall BSettingsClick(TObject *Sender);
	void __fastcall BExitClick(TObject *Sender);

private:	// User declarations
public:		// User declarations
	__fastcall TFrame2(TComponent* Owner);
	bool isFileDownloaded;
	UnicodeString downloadedFile;
	FILE *fin;
	int detectedComets;


void setDetectedComets(){

	UnicodeString importFile;
	char c;

	if (RadioButton1->Checked)
		importFile = downloadedFile;
	else
		importFile = Edit1->Text;

	fin = fopen(AnsiString(importFile).c_str(), "r");

	if(!fin){
		Application->MessageBox(L"Unable to open input file",
			L"Error",
			MB_OK | MB_ICONERROR);
		return;
	}

	detectedComets = 0;
	while ((c=fgetc(fin)) != EOF){
		if (c=='\n') detectedComets++;
	}
	rewind(fin);

	int importType = ComboBox1->ItemIndex;

	if (importType==3 || importType==8 || importType==10) detectedComets/=2;
	if (importType==8 || importType==4 || importType==5) --detectedComets;
	if (importType==7) detectedComets-=15;
	if (importType==11) detectedComets-=5;
	if (importType==14) detectedComets-=23;
	if (importType==17) detectedComets/=13;
	if (importType==18) detectedComets-=2;

	Label4->Caption = "Detected comets: " + IntToStr(detectedComets);
	Label4->Visible = true;
}

};
//---------------------------------------------------------------------------
extern PACKAGE TFrame2 *Frame2;
//---------------------------------------------------------------------------
#endif
