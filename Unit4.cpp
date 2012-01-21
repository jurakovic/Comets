//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "Unit4.h"
#include "Unit7.h"
#include "Unit8.h"

#include "CometMain.hpp"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TFrame4 *Frame4;
//---------------------------------------------------------------------------
__fastcall TFrame4::TFrame4(TComponent* Owner)
	: TFrame(Owner)
{
}
//---------------------------------------------------------------------------
void __fastcall TFrame4::Button2Click(TObject *Sender)
{
	Form1->Frame31->Visible = true;
	Form1->Frame41->Visible = false;
}
//---------------------------------------------------------------------------
void __fastcall TFrame4::Button1Click(TObject *Sender)
{
	Form1->Frame51->Visible = true;
	Form1->Frame41->Visible = false;
}
//---------------------------------------------------------------------------

void __fastcall TFrame4::Button3Click(TObject *Sender)
{
	int importType = Form1->Frame21->ComboBox1->ItemIndex;

	UnicodeString importFile;

	if(Form1->Frame21->RadioButton1->Checked)
		importFile = Form1->Frame21->downloadedFile;

	else
		importFile = Form1->Frame21->Edit1->Text;

	Ncmt = Form1->import_main(importType, importFile);

	if(Ncmt) {

		Button1->Enabled = true;
		Form1->Frame51->ListBox1->Clear();

		for(int i=0; i<Ncmt; i++)
			Form1->Frame51->ListBox1->Items->Add(Form1->cmt[i].full);
	}
}
//---------------------------------------------------------------------------

void __fastcall TFrame4::ComboBox2Change(TObject *Sender)
{
	ProgressBar1->Visible = false;
	Label2->Visible = false;
	Button1->Enabled = false;

	Form1->Frame51->Edit1->Text = "";
	Form1->Frame51->Edit2->Text = "";
	Form1->Frame51->Edit3->Text = "";
	Form1->Frame51->Edit4->Text = "";
	Form1->Frame51->Edit5->Text = "";
	Form1->Frame51->Edit6->Text = "";
	Form1->Frame51->Edit7->Text = "";
	Form1->Frame51->Edit8->Text = "";
	Form1->Frame51->Edit9->Text = "";
	Form1->Frame51->Edit10->Text = "";
	Form1->Frame51->Edit11->Text = "";
	Form1->Frame51->Edit12->Text = "";
	Form1->Frame51->Edit13->Text = "";
	Form1->Frame51->Edit14->Text = "";

	if(Form1->Frame21->ComboBox1->ItemIndex == 14 && ComboBox2->ItemIndex == 1){
		ShowMessage("Format Voyager II (Soft14Cmt) can not be sorted by name.\n\
					Please choose another criterion.");
		ComboBox2->ItemIndex = 0;
		ComboBox3->Enabled = false;
		ComboBox3->ItemIndex = -1;
		return;
	}

	if(ComboBox2->ItemIndex > 0){
		ComboBox3->Enabled = true;
		ComboBox3->ItemIndex = 0;
	}

	if(ComboBox2->ItemIndex == 0){
		ComboBox3->Enabled = false;
		ComboBox3->ItemIndex = -1;
	}
}
//---------------------------------------------------------------------------

void __fastcall TFrame4::ComboBox3Change(TObject *Sender)
{
	ProgressBar1->Visible = false;
	Label2->Visible = false;
	Button1->Enabled = false;

	Form1->Frame51->ListBox1->Clear();
	Form1->Frame51->Edit1->Text = "";
	Form1->Frame51->Edit2->Text = "";
	Form1->Frame51->Edit3->Text = "";
	Form1->Frame51->Edit4->Text = "";
	Form1->Frame51->Edit5->Text = "";
	Form1->Frame51->Edit6->Text = "";
	Form1->Frame51->Edit7->Text = "";
	Form1->Frame51->Edit8->Text = "";
	Form1->Frame51->Edit9->Text = "";
	Form1->Frame51->Edit10->Text = "";
	Form1->Frame51->Edit11->Text = "";
	Form1->Frame51->Edit12->Text = "";
	Form1->Frame51->Edit13->Text = "";
	Form1->Frame51->Edit14->Text = "";
}
//---------------------------------------------------------------------------



void __fastcall TFrame4::BAboutClick(TObject *Sender)
{
	Form8->ShowModal();
}
//---------------------------------------------------------------------------

void __fastcall TFrame4::BSettingsClick(TObject *Sender)
{
	Form7->ShowModal();
}
//---------------------------------------------------------------------------

void __fastcall TFrame4::BExitClick(TObject *Sender)
{
	Form1->Close();
}
//---------------------------------------------------------------------------

