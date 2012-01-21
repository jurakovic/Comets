//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "Unit2.h"
#include "Unit7.h"
#include "CometMain.hpp"
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
	Form1->sett.showSplash = CheckBox4->Checked;
	Form1->sett.exitConfirm = CheckBox2->Checked;

	Form1->spremiPostavke();

	if(Form1->sett.advancedMode){
		Form1->Frame21->CheckBox1->Checked = true;
		Form1->Frame21->CheckBox1->Visible = false;
	}
	else{
		Form1->Frame21->CheckBox1->Checked = false;
		Form1->Frame21->CheckBox1->Visible = true;
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
	CheckBox4->Checked = Form1->sett.showSplash;
	CheckBox2->Checked = Form1->sett.exitConfirm;
}
//---------------------------------------------------------------------------

