//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit5.h"
#include "Unit7.h"
#include "Unit2.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm5 *Form5;
//---------------------------------------------------------------------------
__fastcall TForm5::TForm5(TComponent* Owner)
	: TForm(Owner)
{
}
//---------------------------------------------------------------------------
void __fastcall TForm5::FormClose(TObject *Sender, TCloseAction &Action)
{
	ListBox1->Items->Clear();
}
//---------------------------------------------------------------------------
void __fastcall TForm5::Button1Click(TObject *Sender)
{
	if(Form5->Width == 424){
		Form5->Width = 837;
		Button1->Caption = "<-- Details";
	}
	else if(Form5->Width == 837) {
		Form5->Width = 424;
		Button1->Caption = "Details -->";
	}
}

//---------------------------------------------------------------------------

void __fastcall TForm5::Button6Click(TObject *Sender)
{
	//Edit1->Text = cmt[ListBox1->ItemIndex].full;
}
//---------------------------------------------------------------------------

void __fastcall TForm5::ListBox1Click(TObject *Sender)
{
	int i = ListBox1->ItemIndex;

	Edit1->Text = Form2->cmt[i].full;
	Edit2->Text = Form2->cmt[i].ID;
	Edit3->Text = Form2->cmt[i].name;
	Edit4->Text = Form2->cmt[i].y;

	if(Form2->cmt[i].m<10)
		Edit5->Text = "0" + Form2->cmt[i].m;
	else
		Edit5->Text = Form2->cmt[i].m;

	if(Form2->cmt[i].d<10)
		Edit6->Text = "0" + Form2->cmt[i].d;
	else
		Edit6->Text = Form2->cmt[i].d;

	Edit6->Text = Edit6->Text + "." + Form2->cmt[i].h;

	Edit7->Text = Form2->cmt[i].q;
	Edit8->Text = Form2->cmt[i].e;
	Edit9->Text = Form2->cmt[i].i;
	Edit10->Text = Form2->cmt[i].an;
	Edit11->Text = Form2->cmt[i].pn;

	if (Form2->cmt[i].e>=1)
		Edit12->Text = "";
	else
		Edit12->Text = Form2->cmt[i].P;
}
//---------------------------------------------------------------------------

