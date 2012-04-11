//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "MainForm.h"
#include "Frame1.h"
#include "FormSettings.h"
#include "CometMain.hpp"
#include "FileCtrl.hpp"
#include <dir.h>
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm7 *Form7;
//---------------------------------------------------------------------------
__fastcall TForm7::TForm7(TComponent* Owner)
	: TForm(Owner)
{
}
//---------------------------------------------------------------------------

void __fastcall TForm7::Button2Click(TObject *Sender)
{
	Form1->sett.checkNewVersion = CheckBox1->Checked;
	Form1->sett.advancedMode = CheckBox3->Checked;
	Form1->sett.exitConfirm = CheckBox2->Checked;

	Form1->spremiPostavke();

	if(Form1->sett.advancedMode){
		Form1->Frame1->CheckBox1->Checked = true;
		Form1->Frame1->CheckBox1->Visible = false;
	}
	else{
		Form1->Frame1->CheckBox1->Checked = false;
		Form1->Frame1->CheckBox1->Visible = true;
    }

	this->Close();
}
//---------------------------------------------------------------------------

void __fastcall TForm7::Button1Click(TObject *Sender)
{
	this->Close();
}
//---------------------------------------------------------------------------

void __fastcall TForm7::FormShow(TObject *Sender)
{
	CheckBox1->Checked = Form1->sett.checkNewVersion;
	CheckBox3->Checked = Form1->sett.advancedMode;
	CheckBox2->Checked = Form1->sett.exitConfirm;
	Edit1->Text = Form1->dataFolder;
}
//---------------------------------------------------------------------------

void __fastcall TForm7::Button3Click(TObject *Sender)
{
	UnicodeString Directory = Edit1->Text;

	TSelectDirExtOpts Opts;
	Opts << sdNewUI << sdNewFolder;
	SelectDirectory("Select a folder where you would like to save generated file", "", Directory, Opts);

	Edit1->Text = Directory;
}
//---------------------------------------------------------------------------

void __fastcall TForm7::Button4Click(TObject *Sender)
{
	Form1->dataFolder = _wgetenv(L"appdata");
	Form1->dataFolder += "\\Comet OEW";
	Edit1->Text = Form1->dataFolder;
}
//---------------------------------------------------------------------------

void __fastcall TForm7::Button5Click(TObject *Sender)
{
	_wrmdir(Form1->dataFolder.w_str());
    Form1->spremiPostavke();
}
//---------------------------------------------------------------------------

