//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "Unit2.h"

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
	Form1->Frame21->Visible = false;
	Form1->Frame31->Visible = true;
}
//---------------------------------------------------------------------------

void __fastcall TFrame2::RadioButton1Click(TObject *Sender)
{
	if(RadioButton1->Checked){
		Button3->Enabled = true;
		Edit1->Enabled = false;
		Button4->Enabled = false;
		Label4->Visible = false;
	}
	if(!fileIsDownloaded) Button1->Enabled  = false;
}

//---------------------------------------------------------------------------

void __fastcall TFrame2::RadioButton2Click(TObject *Sender)
{
	if(RadioButton2->Checked){
		if(Edit1->GetTextLen() > 0) Button1->Enabled = true;
		Edit1->Enabled = true;
		Button4->Enabled = true;
		Button3->Enabled = false;
		if(Edit1->GetTextLen() > 0) Label4->Visible = true;
	}
}

//---------------------------------------------------------------------------

void __fastcall TFrame2::Button3Click(TObject *Sender)
{
	char *fileToDownload;

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
		downloadedFile = Form1->coewFolder + "\\Soft" +
					FormatFloat("00", ComboBox1->ItemIndex) +
					"Cmt_" + year + "-" +
					FormatFloat("00", mon) + "-" +
					FormatFloat("00", day) + "_" +
					FormatFloat("00", hour) + "-" +
					FormatFloat("00", min) + "-" +
					FormatFloat("00", sec) + ".txt";

	else downloadedFile = Form1->coewFolder + "\\ELEMENTS.COMET";

	try{
		TFileStream *fStr = new TFileStream(downloadedFile, fmCreate);
		H1->Get(fileToDownload, fStr);
		delete fStr;
	}

	catch (...){
		Application->MessageBox(L"Unable to download orbital elements",
			L"Error",
			MB_OK | MB_ICONERROR);

		//remove(downloadedFile.c_str());
		ProgressBar1->Position = 0;
		ProgressBar1->Visible = false;
		return;
	}

	setDetectedComets();

	ProgressBar1->Position = ProgressBar1->Max;

	fileIsDownloaded = true;
	Button1->Enabled = true;
	Label3->Visible = true;

	Label4->Caption = "Total " + IntToStr(detectedComets) + " comets detected";
	Label4->Visible = true;
}
//---------------------------------------------------------------------------

void __fastcall TFrame2::Button4Click(TObject *Sender)
{
	OpenDialog1->Execute();
	Edit1->Text =  OpenDialog1->FileName;

	if(Edit1->GetTextLen() > 0){

		ComboBox1Change(Sender);
		setDetectedComets();

		Label4->Caption = "Total " + IntToStr(detectedComets) + " detected comets.";
		Label4->Visible = true;
		Button1->Enabled = true;
	}
}
//---------------------------------------------------------------------------

void __fastcall TFrame2::ComboBox1Change(TObject *Sender)
{
	if(Edit1->GetTextLen() == 0) Button1->Enabled = false;

	fileIsDownloaded = false;
	RadioButton1->Enabled = true;
	if(RadioButton1->Checked) Button3->Enabled = true;
	RadioButton2->Enabled = true;
	ProgressBar1->Position = 0;
	ProgressBar1->Visible = false;
	Label3->Visible = false;
	Label4->Visible = false;

	if(RadioButton2->Checked && Edit1->GetTextLen() > 0) {
		setDetectedComets();
		Label4->Caption = "Total " + IntToStr(detectedComets) + " detected comets.";
		Label4->Visible = true;
	}

	Form1->Frame41->ProgressBar1->Visible = false;
	Form1->Frame41->Label3->Visible = false;
	Form1->Frame41->Label2->Visible = false;
	Form1->Frame41->Button1->Enabled = false;

	Form1->Frame51->ListBox1->Clear();
	Form1->Frame51->Edit1->Text = "";
	Form1->Frame51->Edit2->Text = "";
	Form1->Frame51->Edit3->Text = "";
	Form1->Frame51->Edit4->Text = "";
	Form1->Frame51->Edit5->Text = "";
	Form1->Frame51->Edit6->Text = "";
	Form1->Frame51->Edit7->Text = "";
	Form1->Frame51->Edit8->Text = "";
	Form1->Frame51->Edit9->Text = "";
	Form1->Frame51->Edit10->Text = "";
	Form1->Frame51->Edit11->Text = "";
	Form1->Frame51->Edit12->Text = "";
	Form1->Frame51->Edit13->Text = "";
	Form1->Frame51->Edit14->Text = "";

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
}
//---------------------------------------------------------------------------
