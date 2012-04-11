//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "MainForm.h"
#include "FormPreview.h"
#include <IniFiles.hpp>
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
    Form1->Frame4->ProgressBar1->Visible = false;
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
	Form1->Frame4->ComboBox1->ItemIndex = ComboBox1->ItemIndex;
	Form1->Frame4->Button1Click(Sender);
	Form1->Frame4->clearFrame();
}
//---------------------------------------------------------------------------

void __fastcall TForm11::FormCreate(TObject *Sender)
{
	TIniFile *iniSett = new TIniFile(Form1->settingsFile);

	Left = iniSett->ReadInteger("PreviewForm", "Left", 0);
	Top  = iniSett->ReadInteger("PreviewForm", "Top", 0);
	Width  = iniSett->ReadInteger("PreviewForm", "Width", 0);
	Height  = iniSett->ReadInteger("PreviewForm", "Height", 0);
	WindowState = iniSett->ReadInteger("PreviewForm", "WindowState", 0);

	delete iniSett;
}
//---------------------------------------------------------------------------


