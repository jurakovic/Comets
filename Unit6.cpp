//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "Unit4.h"
#include "Unit6.h"
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
	Form1->Close();
}
//---------------------------------------------------------------------------

void __fastcall TFrame6::Button4Click(TObject *Sender)
{
	SaveDialog1->Execute();
	Edit1->Text =  SaveDialog1->FileName;

	if(SaveDialog1->FilterIndex==1) Edit1->Text = Edit1->Text + ".txt";
	if(SaveDialog1->FilterIndex==2) Edit1->Text = Edit1->Text + ".ini";
	if(SaveDialog1->FilterIndex==3) Edit1->Text = Edit1->Text + ".dat";
	if(SaveDialog1->FilterIndex==4) Edit1->Text = Edit1->Text + ".ssc";
}
//---------------------------------------------------------------------------

void __fastcall TFrame6::ComboBox1Change(TObject *Sender)
{
	if(Edit1->GetTextLen() > 0) Button1->Enabled = true;

	ProgressBar1->Visible = false;
	ProgressBar1->Position = 0;
	Label4->Visible = false;
}
//---------------------------------------------------------------------------

void __fastcall TFrame6::Edit1Change(TObject *Sender)
{
	if(ComboBox1->ItemIndex > -1) Button1->Enabled = true;
}
//---------------------------------------------------------------------------


void __fastcall TFrame6::Button1Click(TObject *Sender)
{
	int Ncmt = Form1->Frame41->Ncmt;

	int exportType = Form1->Frame61->ComboBox1->ItemIndex;

	AnsiString str = Edit1->Text;
	const char *exportFile = str.c_str();

	Form1->export_main(Ncmt, exportType, exportFile);

	fclose(Form1->Frame21->fin);
}
//---------------------------------------------------------------------------

