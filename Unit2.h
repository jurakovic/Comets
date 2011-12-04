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
	TButton *Button1;
	TLabel *Label1;
	TLabel *Label2;
	TComboBox *ComboBox1;
	TRadioButton *RadioButton1;
	TButton *Button3;
	TRadioButton *RadioButton2;
	TButton *Button4;
	TEdit *Edit1;
	TOpenDialog *OpenDialog1;
	TIdHTTP *H1;
	TProgressBar *ProgressBar1;
	TLabel *Label3;
	TImage *Image1;
	TLabel *Label4;
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
	bool fileIsDownloaded;
	AnsiString downloadedFile;
	FILE *fin;
	int detectedComets;


void setDetectedComets(){

	char c, *importFile;

	if (RadioButton1->Checked)
		importFile = downloadedFile.c_str();

	else{
		AnsiString str = Edit1->Text;
		importFile = str.c_str();
	}

	fin = fopen(importFile, "r");

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
}

};
//---------------------------------------------------------------------------
extern PACKAGE TFrame2 *Frame2;
//---------------------------------------------------------------------------
#endif
