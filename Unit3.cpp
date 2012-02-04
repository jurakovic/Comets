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

	Form1->Frame51->Visible = true;
	Form1->Frame31->Visible = false;

	Form1->Frame51->canDoChange = false;
}
//---------------------------------------------------------------------------

void __fastcall TFrame3::Button2Click(TObject *Sender)
{
	Form1->Frame21->Visible = true;
	Form1->Frame31->Visible = false;

	ProgressBar1->Visible = false;
	Button1->Enabled = false;
	ocistiMemoriju(&Form1->cmt);
}


//---------------------------------------------------------------------------

void __fastcall TFrame3::CheckBox1Click(TObject *Sender)
{
		ComboBox1->Enabled = CheckBox1->Checked;
		EditD->Enabled = CheckBox1->Checked;
		EditM->Enabled = CheckBox1->Checked;
		EditY->Enabled = CheckBox1->Checked;
		Button3->Enabled = CheckBox1->Checked;
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

void __fastcall TFrame3::Button3Click(TObject *Sender)
{
	time_t rawtime;
	struct tm *timeinfo;

	time (&rawtime);
	timeinfo = localtime(&rawtime);
	int d = timeinfo->tm_mday;
	int m = 1+timeinfo->tm_mon;
	int y = 1900+timeinfo->tm_year;

	EditD->Text = AnsiString(d);
	EditM->Text = AnsiString(m);
	EditY->Text = AnsiString(y);
}
//---------------------------------------------------------------------------

void __fastcall TFrame3::Button4Click(TObject *Sender)
{
	if ((CheckBox1->Checked && ComboBox1->ItemIndex == -1) ||
		(CheckBox2->Checked && ComboBox2->ItemIndex == -1) ||
		(CheckBox3->Checked && ComboBox3->ItemIndex == -1) ||
		(CheckBox4->Checked && ComboBox4->ItemIndex == -1) ||
		(CheckBox5->Checked && ComboBox5->ItemIndex == -1) ||
		(CheckBox6->Checked && ComboBox6->ItemIndex == -1) ||
		(CheckBox7->Checked && ComboBox7->ItemIndex == -1)){

		Application->MessageBox(L"Please select Greather than (>) or Less than (<)",
			L"Error",
			MB_OK | MB_ICONERROR);
		return;
	}

	if(Form1->define_exclude() == false) return;

	ocistiMemoriju(&Form1->cmt);

	int importType = Form1->Frame21->ComboBox1->ItemIndex;

	UnicodeString importFile;

	if(Form1->Frame21->RadioButton1->Checked)
		importFile = Form1->Frame21->downloadedFile;

	else
		importFile = Form1->Frame21->Edit1->Text;

	int Ncmt = Form1->import_main(importType, importFile);

	if(Ncmt==0) return;

	Application->MessageBox(String(String(Ncmt) + " of " + String(Form1->Frame21->detectedComets) + " comets imported").w_str(),
			L"Import completed!",
			MB_OK | MB_ICONASTERISK);

	Form1->Frame51->ListBox1->Clear();
	Form1->updateListbox(Form1->cmt);

	Form1->Frame51->Ncmt = Ncmt;
	Form1->Frame51->Label20->Caption = "Comets: " + String(Ncmt);

	Form1->Frame51->Button3->Enabled = true;
	Form1->Frame51->Button4->Enabled = true;
	Form1->Frame51->Button5->Enabled = true;
	Form1->Frame51->Button6->Enabled = true;
	Form1->Frame51->Button7->Enabled = true;

	Form1->Frame51->nosort2->Checked = true;
	Form1->Frame51->Ascending1->Checked = true;
	Form1->Frame51->Button1->Enabled = true;
	Button1->Enabled = true;
}
//---------------------------------------------------------------------------

