//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "Unit2.h"
#include "Unit7.h"
#include "Unit8.h"
#include "Unit12.h"

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
	Button3->Enabled = true;
	Edit1->Enabled = false;
	Button4->Enabled = false;

	if(isFileDownloaded) {
	//ako je datoteka skinuta, prebroji komete i moze next
		setDetectedComets();
		Button1->Enabled = true;
		Label4->Visible = true;
		ProgressBar1->Visible = true;
	}
	else {
		Button1->Enabled = false;
		Label4->Visible = false;
	}
}

//---------------------------------------------------------------------------

void __fastcall TFrame2::RadioButton2Click(TObject *Sender)
{
	Edit1->Enabled = true;
	Button4->Enabled = true;
	Button3->Enabled = false;
	ProgressBar1->Visible = false;

	if(Edit1->GetTextLen() > 0) {
	//ako je datoteka ucitana, prebroji komete i moze next

		FILE *test = fopen(AnsiString(Edit1->Text).c_str(), "r");
		if(test == NULL) return;

		if(checkImportType() == false) return;
		Button1->Enabled = true;
		Label4->Visible = true;
	}

	else {
	//inace ne
		Button1->Enabled = false;
		Label4->Visible = false;
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

	if(OpenDialog1->Execute()){
		Edit1->Text =  OpenDialog1->FileName;

		checkImportType();
	}
}
//---------------------------------------------------------------------------

void __fastcall TFrame2::ComboBox1Change(TObject *Sender)
{
	if(Edit1->GetTextLen() == 0) Button1->Enabled = false;

	isFileDownloaded = false;
	if(RadioButton1->Checked) Button3->Enabled = true;
	ProgressBar1->Position = 0;
	ProgressBar1->Visible = false;
	Label4->Visible = false;

	if(RadioButton2->Checked && Edit1->GetTextLen() > 0) {

		checkImportType();
	}

	Form1->Frame31->ProgressBar1->Visible = false;
	Form1->Frame31->Button1->Enabled = false;

	Form1->Frame51->ListBox1->Clear();

	Form1->Frame61->ProgressBar1->Visible = false;
	Form1->Frame61->ProgressBar1->Position = 0;
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

void TFrame2::setDetectedComets(){

	UnicodeString importFile;
	char c;

	if (RadioButton1->Checked)
		importFile = downloadedFile;
	else
		importFile = Edit1->Text;

	fin = fopen(AnsiString(importFile).c_str(), "r");

	if(!fin){
		Application->MessageBox(L"Unable to open input file",
			L"Error",
			MB_OK | MB_ICONERROR);
		return;
	}

	detectedComets = 0;
	while ((c=fgetc(fin)) != EOF){
		if (c=='\n') detectedComets++;
	}
	rewind(fin);

	int importType = ComboBox1->ItemIndex;

	if (importType==3 || importType==8 || importType==10) detectedComets/=2;
	if (importType==8 || importType==4 || importType==5) --detectedComets;
	if (importType==7) detectedComets-=15;
	if (importType==11) detectedComets-=5;
	if (importType==14) detectedComets-=23;
	if (importType==17) detectedComets/=13;
	if (importType==18) detectedComets-=2;

	Label4->Caption = "Detected comets: " + String(detectedComets);
	Label4->Visible = true;
	Button1->Enabled = true;
}
//---------------------------------------------------------------------------

bool TFrame2::checkImportType(){

	FILE *f = fopen(AnsiString(Edit1->Text).c_str(), "r");

	int impType = getImportType(f);

	fclose(f);

	if(impType == -1){
		Label4->Visible = false;
		Button1->Enabled = false;	
		ShowMessage("Invalid import type");
		return false;
	}

	if(ComboBox1->ItemIndex != impType){

		UnicodeString detected;

		if(impType== 0) detected = "MPC (Soft00Cmt)";
		if(impType== 1) detected = "SkyMap (Soft01Cmt)";
		if(impType== 2) detected = "Guide (Soft02Cmt)";
		if(impType== 3) detected = "xephem (Soft03Cmt)";
		if(impType== 4) detected = "Home Planet (Soft04Cmt)";
		if(impType== 5) detected = "MyStars! (Soft05Cmt)";
		if(impType== 6) detected = "TheSky (Soft06Cmt)";
		if(impType== 7) detected = "Starry Night (Soft07Cmt)";
		if(impType== 8) detected = "Deep Space (Soft08Cmt)";
		if(impType== 9) detected = "PC-TCS (Soft09Cmt)";
		if(impType==10) detected = "Earth Centered Universe (Soft10Cmt)";
		if(impType==11) detected = "Dance of the Planets (Soft11Cmt)";
		if(impType==12) detected = "MegaStar V4.x (Soft12Cmt)";
		if(impType==13) detected = "SkyChart III (Soft13Cmt)";
		if(impType==14) detected = "Voyager II (Soft14Cmt)";
		if(impType==15) detected = "SkyTools (Soft15Cmt)";
		if(impType==16) detected = "Autostar (Soft16Cmt)";
		if(impType==17) detected = "Comet for Windows";
		if(impType==18) detected = "NASA (ELEMENTS.COMET)";

		/*
		UnicodeString a = "Detected import format: " + detected  +
			"\nSelected import format: " + ComboBox1->Text +
			"\n\nChange it to " + detected + "?";

		int test = Application->MessageBox(
			a.w_str(),
			L"Change selected import format?",
			MB_OKCANCEL | MB_ICONQUESTION);

		if(test == IDOK) ComboBox1->ItemIndex = impType;
		*/

		UnicodeString text = "Import format will be changed to " + detected;
		UnicodeString title = "Detected " + detected + " import format";

		Application->MessageBox(text.w_str(), title.w_str(), MB_OK | MB_ICONQUESTION);

		ComboBox1->ItemIndex = impType;
	}
	setDetectedComets();
	return true;
}
//---------------------------------------------------------------------------

int TFrame2::getImportType(FILE *fin){

	int m;
	Comet *com = new Comet;

	char x[50+1];
	long int y;
	int h;
	float G;

	//pc-tcs
	m = fscanf(fin, "%s %f %f %f %f %f %d %d %f %f %f %55[^\n]%*c",
		com->ID, &com->q, &com->e, &com->i,
		&com->pn, &com->an, &com->y, &com->m,
		&com->d, &com->H, &G, com->name);

	if (m == 12) {
		delete com;
		return 9;
	}

	rewind(fin);

	char full[42+1];
	//guide
	m = fscanf(fin, "%42c %f %d %d 0.0 %f %f %f %f %f 2000.0 %f %f %20[^\n]%*c",
		full, &com->d, &com->m, &com->y,
		&com->q,  &com->e, &com->i, &com->pn,
		&com->an, &com->H, &com->G, x);

	if (m == 12) {
		delete com;
		return 2;
	}

	rewind(fin);

	//skychart
	m = fscanf(fin, "P11 2000.0 -%f %f %f %f %f 0 %d/%d/%f %f %f 0 0 ",
		&com->q, &com->e, &com->i, &com->pn,
		&com->an, &com->y, &com->m, &com->d,
		&com->H, &com->G);

	if (m == 10) {
		delete com;
		return 13;
	}

	rewind(fin);

	//mpc
    m = fscanf(fin, "%14c %d %02d %f %f %f %f %f %f%12c%f %f %55c %30[^\n]%*c",		// %f%12c%f mora bit tako zajedno
			x, &com->y, &com->m, &com->d,
			&com->q, &com->e, &com->pn, &com->an,
			&com->i, x, &com->H, &com->G, com->full, x);

	if (m == 14) {
		delete com;
		return 0;
	}

	rewind(fin);

	int yy, mm, dd;
	char o[15+1];
	//skytools
	m = fscanf(fin, "C %40c %d %d %d %d %d %f %f %f %f %f %f %f %f 0.002000 %15[^\n]%*c",
		com->full, &yy, &mm, &dd, &com->y, &com->m, &com->d,
		&com->q, &com->e, &com->pn, &com->an, &com->i,
		&com->H, &com->G, o);

	if (m == 15) {
		delete com;
		return 15;
	}

	rewind(fin);

	char u[25+1];
	//megastar
	m = fscanf(fin, "%30c %12c %d %d %f %f %f %f %f %f %f %f %25[^\n]%*c",
		com->name, com->ID, &com->y, &com->m, &com->d,
		&com->q, &com->e, &com->pn,
		&com->an, &com->i, &com->H, &com->G, u);

	if (m == 13) {
		delete com;
		return 12;
	}

	rewind(fin);

	//skymap
	m = fscanf(fin, "%47c %4d %2d %f %f %f %f %f %f %f %f\n",
		com->full, &com->y, &com->m, &com->d,
		&com->q, &com->e, &com->pn,
		&com->an, &com->i, &com->H, &com->G);

	if (m == 11) {
		delete com;
		return 1;
	}

	rewind(fin);

	int dmy;
	m = fscanf(fin, "RDPC	%d\n", &dmy);

	if(m == 1){
		delete com;
		return 5;
	}

	rewind(fin);

	//thesky
	m = fscanf(fin, "%45c %4d%2d%f | %f | %f | %f | %f | %f | %f | %f %20[^\n]%*c",
		com->full, &com->y, &com->m,
		&com->d, &com->q, &com->e,
		&com->pn, &com->an, &com->i, &com->H,
		&com->G, x);

	if (m == 12) {
		delete com;
		return 6;
	}

	rewind(fin);

	//starry night
	for (int i=0; i<10; i++) fscanf(fin, "%*[^\n]\n");
	m = fscanf(fin, "     %29c %f 0.0 %f %f %f %f %f %ld.%d %ld.5 %f",
		com->name, &com->H, &com->e, &com->q, &com->an,
		&com->pn, &com->i, &com->T, &h,
		&y, &G);

	if (m == 11) {
		delete com;
		return 7;
	}

	rewind(fin);

	//earth centered universe
	m = fscanf(fin, "%45[^\n]%*cE C 2000 %d %d %f %f %f %f %f %f %f %f\n",
		com->full, &com->y, &com->m, &com->d,
		&com->q, &com->e, &com->pn, &com->an,
		&com->i, &com->H, &G);

	if (m == 11) {
		delete com;
		return 10;
	}

	rewind(fin);

	fscanf(fin, "%*[^\n]\n");
	fscanf(fin, "%*[^\n]\n");
	fscanf(fin, "%*[^\n]\n");

	m = fscanf(fin, "C J2000 %d %d %f %f %f %f %f %f %f %f\n",
		x, &com->y, &com->m, &com->d,
		&com->q, &com->e, &com->pn, &com->an,
		&com->i, &com->H, &G);

	if (m == 10){
		delete com;
		return 8;
	}
	rewind(fin);

	//dance of the planets
	for (int i=0; i<5; i++) fscanf(fin, "%*[^\n]\n");
	m = fscanf(fin, "%11c %f %f %f %f %f %d.%2d%6f %30[^\n]%*c",
		com->ID, &com->q, &com->e, &com->i,
		&com->an, &com->pn, &com->y, &com->m,
		&com->d, com->name);

	if (m == 10) {
		delete com;
		return 11;
	}

	rewind(fin);

	char mj[3+1];
	//voyager
	for (int i=0; i<18; i++) fscanf(fin, "%*[^\n]\n");
	m = fscanf(fin, "%27c %f %f %f %f %f %f %4d %3c %f 2000.0\n",
		com->name, &com->q, &com->e, &com->i,
		&com->an, &com->pn, &com->G, &com->y,
		mj, &com->d);

	if (m == 10) {
		delete com;
		return 14;
	}

	rewind(fin);

	int dummy;
	m = fscanf(fin, "# From MPC %d\n", &dummy);

	if(m == 1){
		delete com;
		return 3;
	}

	rewind(fin);

	try{
		char c;
		int j=0;

		fscanf(fin, "%*[^\n]\n");

		while ((c=fgetc(fin)) != ','){

			if(c == '\n' || c == EOF) break;

			com->full[j++]=c;
		}
		com->full[j]='\0';

		m = fscanf(fin, "%d-%d-%f,%f,%f,%f,%f,%f,%50[^\n]%*c",
			&com->y, &com->m, &com->d,
			&com->q, &com->e, &com->pn, &com->an,
			&com->i, x);

		if (m == 9){
			delete com;
			return 4;
		}
	}
	catch(...){}

	return -1;
}
//---------------------------------------------------------------------------
