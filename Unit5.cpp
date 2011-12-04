//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "Unit5.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TFrame5 *Frame5;
//---------------------------------------------------------------------------
__fastcall TFrame5::TFrame5(TComponent* Owner)
	: TFrame(Owner)
{
}
//---------------------------------------------------------------------------
void __fastcall TFrame5::Button1Click(TObject *Sender)
{
	Form1->Frame61->Visible = true;
	Form1->Frame51->Visible = false;
}
//---------------------------------------------------------------------------

void __fastcall TFrame5::Button2Click(TObject *Sender)
{
    Form1->Frame41->Visible = true;
	Form1->Frame51->Visible = false;
}
//---------------------------------------------------------------------------

void __fastcall TFrame5::ListBox1Click(TObject *Sender)
{
	int i = Form1->Frame51->ListBox1->ItemIndex;

	Edit1->Text = Form1->cmt[i].full;
	Edit2->Text = Form1->cmt[i].ID;
	Edit3->Text = Form1->cmt[i].name;

	Edit4->Text = Form1->cmt[i].y;
	Edit5->Text = FormatFloat("00", Form1->cmt[i].m);
	Edit6->Text = FormatFloat("00.0000", Form1->cmt[i].d);
	Edit6->Text = StringReplace(Edit6->Text, ",", ".", TReplaceFlags());

	Edit7->Text = FormatFloat("0.000000", Form1->cmt[i].q);
	Edit7->Text = StringReplace(Edit7->Text, ",", ".", TReplaceFlags());

	Edit8->Text = FormatFloat("0.000000", Form1->cmt[i].e);
	Edit8->Text = StringReplace(Edit8->Text, ",", ".", TReplaceFlags());

	Edit9->Text = FormatFloat("0.0000", Form1->cmt[i].i);
	Edit9->Text = StringReplace(Edit9->Text, ",", ".", TReplaceFlags());

	Edit10->Text = FormatFloat("0.0000", Form1->cmt[i].an);
	Edit10->Text = StringReplace(Edit10->Text, ",", ".", TReplaceFlags());

	Edit11->Text = FormatFloat("0.0000", Form1->cmt[i].pn);
	Edit11->Text = StringReplace(Edit11->Text, ",", ".", TReplaceFlags());


	if (Form1->cmt[i].e>=1 || Form1->cmt[i].P>=99999)
		Edit12->Text = "";
	else {
		Edit12->Text = FormatFloat("0.0000", Form1->cmt[i].P);
		Edit12->Text = StringReplace(Edit12->Text, ",", ".", TReplaceFlags());
	}

	if (Form1->Frame21->ComboBox1->ItemIndex ==  4 ||
		Form1->Frame21->ComboBox1->ItemIndex == 11 ||
		Form1->Frame21->ComboBox1->ItemIndex == 14 ||
		Form1->Frame21->ComboBox1->ItemIndex == 18 ){
		Edit13->Text = "";
		Edit14->Text = "";
	}

	else{
		Edit13->Text = FormatFloat("0.00", Form1->cmt[i].H);
		Edit13->Text = StringReplace(Edit13->Text, ",", ".", TReplaceFlags());

		Edit14->Text = FormatFloat("0.00", Form1->cmt[i].G);
		Edit14->Text = StringReplace(Edit14->Text, ",", ".", TReplaceFlags());
	}
}
//---------------------------------------------------------------------------

