//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "Unit12.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm12 *Form12;
//---------------------------------------------------------------------------
__fastcall TForm12::TForm12(TComponent* Owner)
	: TForm(Owner)
{
}
//---------------------------------------------------------------------------

void TForm12::updateEditFields(Comet *cmt){

	Edit1->Text = cmt->full;
	Edit2->Text = cmt->ID;
	Edit3->Text = cmt->name;

	Edit4->Text = cmt->y;
	Edit5->Text = FormatFloat("00",cmt->m);
	Edit6->Text = FormatFloat("00.0000",cmt->d);
	Edit6->Text = StringReplace(Edit6->Text, ",", ".", TReplaceFlags());

	Edit7->Text = FormatFloat("0.000000",cmt->q);
	Edit7->Text = StringReplace(Edit7->Text, ",", ".", TReplaceFlags());

	Edit8->Text = FormatFloat("0.000000",cmt->e);
	Edit8->Text = StringReplace(Edit8->Text, ",", ".", TReplaceFlags());

	Edit9->Text = FormatFloat("0.0000",cmt->i);
	Edit9->Text = StringReplace(Edit9->Text, ",", ".", TReplaceFlags());

	Edit10->Text = FormatFloat("0.0000",cmt->an);
	Edit10->Text = StringReplace(Edit10->Text, ",", ".", TReplaceFlags());

	Edit11->Text = FormatFloat("0.0000",cmt->pn);
	Edit11->Text = StringReplace(Edit11->Text, ",", ".", TReplaceFlags());


	if (cmt->e >= 1 ||cmt->P >= 99999)
		Edit12->Text = "";
	else {
		Edit12->Text = FormatFloat("0.0000",cmt->P);
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
		Edit13->Text = FormatFloat("0.00",cmt->H);
		Edit13->Text = StringReplace(Edit13->Text, ",", ".", TReplaceFlags());

		Edit14->Text = FormatFloat("0.00",cmt->G);
		Edit14->Text = StringReplace(Edit14->Text, ",", ".", TReplaceFlags());
	}

	Edit15->Text = FormatFloat("0.000000",cmt->sort);
	Edit15->Text = StringReplace(Edit15->Text, ",", ".", TReplaceFlags());
}
//---------------------------------------------------------------------------
void __fastcall TForm12::FormShow(TObject *Sender)
{
	Form12->Top = Form1->Top;
	Form12->Left = Form1->Left + Form1->Width + 10;
}

//---------------------------------------------------------------------------

void __fastcall TForm12::FormClose(TObject *Sender, TCloseAction &Action)
{
	Form1->Frame51->Button7->Caption = "Details -->";
}
//---------------------------------------------------------------------------

