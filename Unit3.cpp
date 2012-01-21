//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "Unit3.h"
#include "Unit7.h"
#include "Unit8.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TFrame3 *Frame3;
//---------------------------------------------------------------------------
__fastcall TFrame3::TFrame3(TComponent* Owner)
	: TFrame(Owner)
{
}
//---------------------------------------------------------------------------
void __fastcall TFrame3::Button1Click(TObject *Sender)
{
	if ((CheckBox1->Checked && ComboBox1->ItemIndex == -1) ||
		(CheckBox2->Checked && ComboBox2->ItemIndex == -1) ||
		(CheckBox3->Checked && ComboBox3->ItemIndex == -1) ||
		(CheckBox4->Checked && ComboBox4->ItemIndex == -1) ||
		(CheckBox5->Checked && ComboBox5->ItemIndex == -1) ||
		(CheckBox6->Checked && ComboBox6->ItemIndex == -1) ||
		(CheckBox7->Checked && ComboBox7->ItemIndex == -1)){

		Application->MessageBox(L"Please select < or >",
			L"Error",
			MB_OK | MB_ICONERROR);
		return;
	}

	if(!Form1->define_exclude()) return;

	Form1->Frame41->Visible = true;
	Form1->Frame31->Visible = false;

	Form1->Frame41->ProgressBar1->Visible = false;
	Form1->Frame41->Label2->Visible = false;
	Form1->Frame41->Button1->Enabled = false;
}
//---------------------------------------------------------------------------

void __fastcall TFrame3::Button2Click(TObject *Sender)
{
	Form1->Frame21->Visible = true;
	Form1->Frame31->Visible = false;
}


//---------------------------------------------------------------------------

void __fastcall TFrame3::CheckBox1Click(TObject *Sender)
{
		ComboBox1->Enabled = CheckBox1->Checked;
		EditD->Enabled = CheckBox1->Checked;
		EditM->Enabled = CheckBox1->Checked;
		EditY->Enabled = CheckBox1->Checked;
}
//---------------------------------------------------------------------------

void __fastcall TFrame3::CheckBox2Click(TObject *Sender)
{
		ComboBox2->Enabled = CheckBox2->Checked;
		Edit2->Enabled = CheckBox2->Checked;
		Label2->Enabled = CheckBox2->Checked;
}
//---------------------------------------------------------------------------

void __fastcall TFrame3::CheckBox3Click(TObject *Sender)
{
		ComboBox3->Enabled = CheckBox3->Checked;
		Edit3->Enabled = CheckBox3->Checked;
}
//---------------------------------------------------------------------------

void __fastcall TFrame3::CheckBox4Click(TObject *Sender)
{
		ComboBox4->Enabled = CheckBox4->Checked;
		Edit4->Enabled = CheckBox4->Checked;
		Label4->Enabled = CheckBox4->Checked;
}
//---------------------------------------------------------------------------

void __fastcall TFrame3::CheckBox5Click(TObject *Sender)
{
		ComboBox5->Enabled = CheckBox5->Checked;
		Edit5->Enabled = CheckBox5->Checked;
		Label5->Enabled = CheckBox5->Checked;
}
//---------------------------------------------------------------------------

void __fastcall TFrame3::CheckBox6Click(TObject *Sender)
{
		ComboBox6->Enabled = CheckBox6->Checked;
		Edit6->Enabled = CheckBox6->Checked;
		Label6->Enabled = CheckBox6->Checked;
}
//---------------------------------------------------------------------------

void __fastcall TFrame3::CheckBox7Click(TObject *Sender)
{
		ComboBox7->Enabled = CheckBox7->Checked;
		Edit7->Enabled = CheckBox7->Checked;
		Label7->Enabled = CheckBox7->Checked;
}

//---------------------------------------------------------------------------


void __fastcall TFrame3::BAboutClick(TObject *Sender)
{
	Form8->ShowModal();
}
//---------------------------------------------------------------------------

void __fastcall TFrame3::BSettingsClick(TObject *Sender)
{
	Form7->ShowModal();
}
//---------------------------------------------------------------------------

void __fastcall TFrame3::BExitClick(TObject *Sender)
{
	Form1->Close();
}
//---------------------------------------------------------------------------
bool TFrame3::provjeriTocku(char *str){

	for(int i=0; i<strlen(str); i++){

		if(str[i]==',' || str[i]=='.')
			return false;
			// ne moze tocka
	}

	return true;
	// moze tocka
}

//---------------------------------------------------------------------------

bool TFrame3::provjeriZnak(char c){

	if(c==',' || c=='.'){

		char *editText = AnsiString(Edit2->Text).c_str();

		if(provjeriTocku(editText)) return true;
	}

	const char *string = "0123456789";

	for(int i=0; i<strlen(string); i++){

		if(c==string[i]) return true;
	}

	return false;
}
//---------------------------------------------------------------------------
void __fastcall TFrame3::Edit2KeyPress(TObject *Sender, System::WideChar &Key)
{
	if (provjeriZnak(Key)==false && Key!=VK_BACK) {

		Beep();
		Key = false;
	}
}
//---------------------------------------------------------------------------

