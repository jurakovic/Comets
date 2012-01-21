//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma link "Unit2"
#pragma link "Unit3"
#pragma link "Unit4"
#pragma link "Unit5"
#pragma link "Unit6"
#pragma resource "*.dfm"
TForm1 *Form1;
//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
	: TForm(Owner)
{
}
//---------------------------------------------------------------------------

void __fastcall TForm1::FormShow(TObject *Sender)
{
	using namespace std;

	coewFolder = std::getenv("appdata");
	coewFolder += "\\Comet OEW";

	AnsiString versionFile = coewFolder + "\\version.ini";

	char *file = versionFile.c_str();

	FILE *fin = fopen(file, "r");

	if(!fin){

		mkdir(coewFolder.c_str());

		fin = fopen(versionFile.c_str(), "w");
		fprintf(fin, "%.1f", PROGRAM_VERSION);
		fclose(fin);
		return;
	}

	else{
		fclose(fin);
		return;
	}
}

//---------------------------------------------------------------------------

