//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "Unit10.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TFrame10 *Frame10;
//---------------------------------------------------------------------------
__fastcall TFrame10::TFrame10(TComponent* Owner)
	: TFrame(Owner)
{
}
//---------------------------------------------------------------------------
void __fastcall TFrame10::Image1Click(TObject *Sender)
{
	Form1->Frame21->Visible = true;
	//Form1->Frame101->Visible = false;

	if(CheckBox1->Checked) Form1->sett.showSplash = 0;
}
//---------------------------------------------------------------------------
