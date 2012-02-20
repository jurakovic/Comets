//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "Unit11.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm11 *Form11;
//---------------------------------------------------------------------------
__fastcall TForm11::TForm11(TComponent* Owner)
	: TForm(Owner)
{
}
//---------------------------------------------------------------------------
void __fastcall TForm11::Button2Click(TObject *Sender)
{
	this->Close();
}
//---------------------------------------------------------------------------
void __fastcall TForm11::ComboBox1CloseUp(TObject *Sender)
{
	if(ComboBox1->ItemIndex == -1) return;

	RichEdit1->Clear();

	UnicodeString tempFile = _wgetenv(L"temp");
	tempFile += "\\CoewTempPreview.txt";

	int type = ComboBox1->ItemIndex;

	Form1->export_main(10, type, AnsiString(tempFile).c_str());
    Form1->Frame61->ProgressBar1->Visible = false;
	RichEdit1->Lines->LoadFromFile(tempFile);

	remove(AnsiString(tempFile).c_str());
}
//---------------------------------------------------------------------------
void __fastcall TForm11::Button3Click(TObject *Sender)
{
	ComboBox1CloseUp(Sender);
}
void __fastcall TForm11::Button1Click(TObject *Sender)
{
	Form1->Frame61->ComboBox1->ItemIndex = ComboBox1->ItemIndex;
	Form1->Frame61->Button1Click(Sender);
	Form1->Frame61->clearFrame();
}
//---------------------------------------------------------------------------
