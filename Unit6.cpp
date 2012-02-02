//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "Unit6.h"
#include "Unit7.h"
#include "Unit8.h"
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

void __fastcall TFrame6::Button4Click(TObject *Sender)
{
	if(ComboBox1->ItemIndex < 17)
		SaveDialog1->Filter = "Text files (*.txt)|*.TXT|All files (*.*)|*.*";;
	if(ComboBox1->ItemIndex == 17)
		SaveDialog1->Filter = "SSC files (*.ssc)|*.SSC|All files (*.*)|*.*";
	if(ComboBox1->ItemIndex == 18)
		SaveDialog1->Filter = "INI files (*.ini)|*.INI|All files (*.*)|*.*";

	SaveDialog1->Execute();
	Edit1->Text =  SaveDialog1->FileName;

	//if(SaveDialog1->FilterIndex==1) Edit1->Text += ".txt";
	//if(SaveDialog1->FilterIndex==2) Edit1->Text = Edit1->Text + ".ini";
	//if(SaveDialog1->FilterIndex==3) Edit1->Text = Edit1->Text + ".dat";
	//if(SaveDialog1->FilterIndex==4) Edit1->Text = Edit1->Text + ".ssc";
}
//---------------------------------------------------------------------------

void __fastcall TFrame6::ComboBox1Change(TObject *Sender)
{
	if(ComboBox1->ItemIndex > -1 && Edit1->GetTextLen() > 0)
		Button1->Enabled = true;
	else Button1->Enabled = false;

	ProgressBar1->Visible = false;
	ProgressBar1->Position = 0;
	Label4->Visible = false;
}
//---------------------------------------------------------------------------

void __fastcall TFrame6::Edit1Change(TObject *Sender)
{
	if(ComboBox1->ItemIndex > -1 && Edit1->GetTextLen() > 0){

		Button1->Enabled = true;
		ProgressBar1->Visible = false;
		ProgressBar1->Position = 0;
		Label4->Visible = false;
	}
	else Button1->Enabled = false;
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

	int exportType = Form1->Frame61->ComboBox1->ItemIndex;

	UnicodeString exportFile = Edit1->Text;

	Form1->export_main(Ncmt, exportType, exportFile);
}

