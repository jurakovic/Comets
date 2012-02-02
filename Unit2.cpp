//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "Unit2.h"
#include "Unit7.h"
#include "Unit8.h"

#include "CometMain.hpp"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma link "IdBaseComponent"
#pragma link "IdComponent"
#pragma link "IdHTTP"
#pragma link "IdTCPClient"
#pragma link "IdTCPConnection"
#pragma resource "*.dfm"
TFrame2 *Frame2;
//---------------------------------------------------------------------------
__fastcall TFrame2::TFrame2(TComponent* Owner)
	: TFrame(Owner)
{
}
//---------------------------------------------------------------------------
void __fastcall TFrame2::Button1Click(TObject *Sender)
{
	if(CheckBox1->Checked)
		Form1->Frame31->Visible = true;
	else
		Form1->Frame61->Visible = true;

	Form1->Frame21->Visible = false;

	if(Form1->Frame21->ComboBox1->ItemIndex == 18)
		Form1->Frame31->CheckBox8->Visible = true;
	else
	   Form1->Frame31->CheckBox8->Visible = false;
}
//---------------------------------------------------------------------------

void __fastcall TFrame2::RadioButton1Click(TObject *Sender)
{
	if(RadioButton1->Checked){
		if(isFileDownloaded) {
		//ako je datoteka skinuta, prebroji komete i moze next
			setDetectedComets();
			Button1->Enabled = true;
			Label4->Visible = true;
			ProgressBar1->Visible = true;
		}
		else {
		//inace ne
			Button1->Enabled = false;
			Label4->Visible = false;
		}

		Button3->Enabled = true;
		Edit1->Enabled = false;
		Button4->Enabled = false;
	}
	if(!isFileDownloaded) Button1->Enabled  = false;
}

//---------------------------------------------------------------------------

void __fastcall TFrame2::RadioButton2Click(TObject *Sender)
{
	if(RadioButton2->Checked){

		if(Edit1->GetTextLen() > 0) {
		//ako je datoteka ucitana, prebroji komete i moze next
			setDetectedComets();
			Button1->Enabled = true;
			Label4->Visible = true;
		}

		else {
		//inace ne
			Button1->Enabled = false;
			Label4->Visible = false;
		}

		Edit1->Enabled = true;
		Button4->Enabled = true;
		Button3->Enabled = false;
		ProgressBar1->Visible = false;
	}
}

//---------------------------------------------------------------------------

void __fastcall TFrame2::Button3Click(TObject *Sender)
{
	// s ovime se dobije trenutni datum
	time_t rawtime;
	struct tm *timeinfo;

	time (&rawtime);
	timeinfo = localtime(&rawtime);

	int year = 1900+timeinfo->tm_year;
	int mon = 1+timeinfo->tm_mon;
	int day = timeinfo->tm_mday;
	int hour = timeinfo->tm_hour;
	int min = timeinfo->tm_min;
	int sec = timeinfo->tm_sec;

	UnicodeString fileToDownload;

	if(ComboBox1->ItemIndex == 0) fileToDownload = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft00Cmt.txt";
	if(ComboBox1->ItemIndex == 1) fileToDownload = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft01Cmt.txt";
	if(ComboBox1->ItemIndex == 2) fileToDownload = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft02Cmt.txt";
	if(ComboBox1->ItemIndex == 3) fileToDownload = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft03Cmt.txt";
	if(ComboBox1->ItemIndex == 4) fileToDownload = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft04Cmt.txt";
	if(ComboBox1->ItemIndex == 5) fileToDownload = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft05Cmt.txt";
	if(ComboBox1->ItemIndex == 6) fileToDownload = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft06Cmt.txt";
	if(ComboBox1->ItemIndex == 7) fileToDownload = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft07Cmt.txt";
	if(ComboBox1->ItemIndex == 8) fileToDownload = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft08Cmt.txt";
	if(ComboBox1->ItemIndex == 9) fileToDownload = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft09Cmt.txt";
	if(ComboBox1->ItemIndex == 10) fileToDownload = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft10Cmt.txt";
	if(ComboBox1->ItemIndex == 11) fileToDownload = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft11Cmt.txt";
	if(ComboBox1->ItemIndex == 12) fileToDownload = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft12Cmt.txt";
	if(ComboBox1->ItemIndex == 13) fileToDownload = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft13Cmt.txt";
	if(ComboBox1->ItemIndex == 14) fileToDownload = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft14Cmt.txt";
	if(ComboBox1->ItemIndex == 15) fileToDownload = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft15Cmt.txt";
	if(ComboBox1->ItemIndex == 16) fileToDownload = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft16Cmt.txt";
	if(ComboBox1->ItemIndex == 18) fileToDownload = "http://ssd.jpl.nasa.gov/dat/ELEMENTS.COMET";

	if(ComboBox1->ItemIndex != 18)
		downloadedFile = Form1->defaultDataFolder + "\\Soft" +
					FormatFloat("00", ComboBox1->ItemIndex) +
					"Cmt_" + year + "-" +
					FormatFloat("00", mon) + "-" +
					FormatFloat("00", day) + "_" +
					FormatFloat("00", hour) + "-" +
					FormatFloat("00", min) + "-" +
					FormatFloat("00", sec) + ".txt";

	else downloadedFile = Form1->defaultDataFolder + "\\ELEMENTS_"
					+ year + "-" +
					FormatFloat("00", mon) + "-" +
					FormatFloat("00", day) + "_" +
					FormatFloat("00", hour) + "-" +
					FormatFloat("00", min) + "-" +
					FormatFloat("00", sec) + ".COMET";

	TFileStream *fStr = new TFileStream(downloadedFile, fmCreate);
	try{
		H1->Get(AnsiString(fileToDownload).c_str(), fStr);
	}

	catch (...){
		Application->MessageBox(L"Unable to download orbital elements",
			L"Error",
			MB_OK | MB_ICONERROR);

		delete fStr;
		remove(AnsiString(downloadedFile).c_str());
		ProgressBar1->Position = 0;
		ProgressBar1->Visible = false;
		return;
	}

	delete fStr;
	setDetectedComets();

	ProgressBar1->Position = ProgressBar1->Max;

	isFileDownloaded = true;
	Button1->Enabled = true;
}
//---------------------------------------------------------------------------

void __fastcall TFrame2::Button4Click(TObject *Sender)
{
	if(ComboBox1->ItemIndex < 17)
		OpenDialog1->Filter = "Text files (*.txt)|*.TXT|All files (*.*)|*.*";;
	if(ComboBox1->ItemIndex == 17)
		OpenDialog1->Filter = "DAT files (*.dat)|*.DAT|All files (*.*)|*.*";
	if(ComboBox1->ItemIndex == 18)
		OpenDialog1->Filter = "COMET files (*.comet)|*.COMET|All files (*.*)|*.*";

    OpenDialog1->Execute();
	Edit1->Text =  OpenDialog1->FileName;

	if(Edit1->GetTextLen() > 0){

		setDetectedComets();

		Label4->Caption = "Detected comets: " + String(detectedComets);
		Label4->Visible = true;
		Button1->Enabled = true;
	}
}
//---------------------------------------------------------------------------

void __fastcall TFrame2::ComboBox1Change(TObject *Sender)
{
	if(Edit1->GetTextLen() == 0) Button1->Enabled = false;

	isFileDownloaded = false;
	RadioButton1->Enabled = true;
	if(RadioButton1->Checked) Button3->Enabled = true;
	RadioButton2->Enabled = true;
	ProgressBar1->Position = 0;
	ProgressBar1->Visible = false;
	Label4->Visible = false;

	if(RadioButton2->Checked && Edit1->GetTextLen() > 0) setDetectedComets();

	Form1->Frame31->ProgressBar1->Visible = false;
	Form1->Frame31->Button1->Enabled = false;

	Form1->Frame51->ListBox1->Clear();

	Form1->Frame51->ocistiEditPolja();

	Form1->Frame61->Edit1->Text = "";
	Form1->Frame61->ProgressBar1->Visible = false;
	Form1->Frame61->ProgressBar1->Position = 0;
	Form1->Frame61->Label4->Visible = false;
}
//---------------------------------------------------------------------------

void __fastcall TFrame2::H1WorkBegin(TObject *ASender, TWorkMode AWorkMode,
	__int64 AWorkCountMax)
{
	ProgressBar1->Visible = true;
	ProgressBar1->Position = 0;
	ProgressBar1->Max = AWorkCountMax;
}
//---------------------------------------------------------------------------

void __fastcall TFrame2::H1Work(TObject *ASender, TWorkMode AWorkMode, __int64 AWorkCount)
{
	ProgressBar1->Position =  AWorkCount;
	Application->ProcessMessages();
}
//---------------------------------------------------------------------------

