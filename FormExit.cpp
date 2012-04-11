//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "MainForm.h"
#include "FormSettings.h"
#include "FormExit.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm9 *Form9;
//---------------------------------------------------------------------------
__fastcall TForm9::TForm9(TComponent* Owner)
	: TForm(Owner)
{
}
//---------------------------------------------------------------------------
void __fastcall TForm9::Button1Click(TObject *Sender)
{
	exit = true;
	this->Close();
}
//---------------------------------------------------------------------------

void __fastcall TForm9::Button2Click(TObject *Sender)
{
	exit = false;
	this->Close();
}
//---------------------------------------------------------------------------

void __fastcall TForm9::FormClose(TObject *Sender, TCloseAction &Action)
{
	Form1->sett.exitConfirm = CheckBox1->Checked;
}
//---------------------------------------------------------------------------






