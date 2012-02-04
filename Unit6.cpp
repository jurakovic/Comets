//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "Unit6.h"
#include "Unit7.h"
#include "Unit8.h"
#include "Unit11.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TFrame6 *Frame6;
//---------------------------------------------------------------------------
__fastcall TFrame6::TFrame6(TComponent* Owner)
	: TFrame(Owner)
{
}
//---------------------------------------------------------------------------
void __fastcall TFrame6::Button2Click(TObject *Sender)
{
	Form1->Frame61->Visible = false;
	Form1->Frame51->Visible = true;
}
//---------------------------------------------------------------------------

void __fastcall TFrame6::Button3Click(TObject *Sender)
{
	if(Form1->Frame21->CheckBox1->Checked)
		Form1->Frame51->Visible = true;
	else
		Form1->Frame21->Visible = true;

	Form1->Frame61->Visible = false;
}
//---------------------------------------------------------------------------


void __fastcall TFrame6::ComboBox1Change(TObject *Sender)
{
	clearFrame();
}
//---------------------------------------------------------------------------



void __fastcall TFrame6::Button1Click(TObject *Sender)
{

	int Ncmt = Form1->Frame51->Ncmt;

	if(Form1->Frame21->CheckBox1->Checked == false){

		int importType = Form1->Frame21->ComboBox1->ItemIndex;

		UnicodeString importFile;

		if(Form1->Frame21->RadioButton1->Checked)
			importFile = Form1->Frame21->downloadedFile;

		else importFile = Form1->Frame21->Edit1->Text;

		Ncmt = Form1->import_main(importType, importFile);
	}

	int exportType = ComboBox1->ItemIndex;

	if(ComboBox1->ItemIndex < 17)
		SaveDialog1->Filter = "Text files (*.txt)|*.TXT|All files (*.*)|*.*";;
	if(ComboBox1->ItemIndex == 17)
		SaveDialog1->Filter = "SSC files (*.ssc)|*.SSC|All files (*.*)|*.*";
	if(ComboBox1->ItemIndex == 18)
		SaveDialog1->Filter = "INI files (*.ini)|*.INI|All files (*.*)|*.*";

	if(SaveDialog1->Execute()){
		expFileName = SaveDialog1->FileName;

		if(ComboBox1->ItemIndex < 17 && SaveDialog1->FilterIndex==1){

			if((expFileName.AnsiPos(".txt") == 0 && expFileName.AnsiPos(".TXT") > 0) ||
				(expFileName.AnsiPos(".txt") > 0 && expFileName.AnsiPos(".TXT") == 0)){

				// do nothing
			}

			else expFileName += ".txt";
		}

		if(ComboBox1->ItemIndex == 17 && SaveDialog1->FilterIndex==1){

			if((expFileName.AnsiPos(".ssc") == 0 && expFileName.AnsiPos(".SSC") > 0) ||
				(expFileName.AnsiPos(".ssc") > 0 && expFileName.AnsiPos(".SSC") == 0)){

				// do nothing
			}

			else expFileName += ".ssc";
		}

		if(ComboBox1->ItemIndex == 18 && SaveDialog1->FilterIndex==1){

			if((expFileName.AnsiPos(".ini") == 0 && expFileName.AnsiPos(".INI") > 0) ||
				(expFileName.AnsiPos(".ini") > 0 && expFileName.AnsiPos(".INI") == 0)){

				// do nothing
			}

			else expFileName += ".ini";
		}
	}
	else return;

	Form1->export_main(Ncmt, exportType, expFileName);
	Form1->Frame61->Label3->Visible = true;

	Application->MessageBox(L"All data successfully exported",
		L"Export completed!",
		MB_OK | MB_ICONASTERISK);
}

void TFrame6::clearFrame(){

	Form1->Frame61->ProgressBar1->Visible = false;
	Form1->Frame61->ProgressBar1->Position = 0;
	Form1->Frame61->Label3->Visible = false;
}
