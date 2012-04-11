//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "MainForm.h"
#include "FrameSplash.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TFrameSplash1 *FrameSplash1;
//---------------------------------------------------------------------------
__fastcall TFrameSplash1::TFrameSplash1(TComponent* Owner)
	: TFrame(Owner)
{
}
//---------------------------------------------------------------------------
void __fastcall TFrameSplash1::Image1Click(TObject *Sender)
{
	Form1->Frame1->Visible = true;
	//Form1->Frame101->Visible = false;

	//if(CheckBox1->Checked) Form1->sett.showSplash = 0;
}
//---------------------------------------------------------------------------
