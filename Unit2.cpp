//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit2.h"
#include "Unit3.h"
#include "Unit4.h"
#include "Unit5.h"
#include <string>

//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma link "IdBaseComponent"
#pragma link "IdComponent"
#pragma link "IdHTTP"
#pragma link "IdTCPClient"
#pragma link "IdTCPConnection"
#pragma link "IdBaseComponent"
#pragma link "IdComponent"
#pragma link "IdHTTP"
#pragma link "IdTCPClient"
#pragma link "IdTCPConnection"
#pragma resource "*.dfm"
TForm2 *Form2;



//---------------------------------------------------------------------------
__fastcall TForm2::TForm2(TComponent* Owner)
	: TForm(Owner)
{
}
//---------------------------------------------------------------------------


void __fastcall TForm2::ch1Click(TObject *Sender)
{
	if(ch1->Checked)
	{
		combo1->Enabled = true;
		t1->Enabled = true;
	}

	else
	{
		combo1->Enabled = false;
		t1->Enabled = false;
	}
}
//---------------------------------------------------------------------------

void __fastcall TForm2::ch2Click(TObject *Sender)
{
	if(ch2->Checked)
	{
		combo2->Enabled = true;
		t2->Enabled = true;
		lab2->Enabled = true;
	}

	else
	{
		combo2->Enabled = false;
		t2->Enabled = false;
		lab2->Enabled = false;
	}
}
//---------------------------------------------------------------------------

void __fastcall TForm2::ch3Click(TObject *Sender)
{
	if(ch3->Checked)
	{
		combo3->Enabled = true;
		t3->Enabled = true;
	}

	else
	{
		combo3->Enabled = false;
		t3->Enabled = false;
	}
}
//---------------------------------------------------------------------------

void __fastcall TForm2::ch4Click(TObject *Sender)
{
	if(ch4->Checked)
	{
		combo4->Enabled = true;
		t4->Enabled = true;
		lab4->Enabled = true;
	}

	else
	{
		combo4->Enabled = false;
		t4->Enabled = false;
		lab4->Enabled = false;
	}
}
//---------------------------------------------------------------------------

void __fastcall TForm2::ch5Click(TObject *Sender)
{
	if(ch5->Checked)
	{
		combo5->Enabled = true;
		t5->Enabled = true;
		lab5->Enabled = true;
	}

	else
	{
		combo5->Enabled = false;
		t5->Enabled = false;
		lab5->Enabled = false;
	}
}
//---------------------------------------------------------------------------

void __fastcall TForm2::ch6Click(TObject *Sender)
{
	if(ch6->Checked)
	{
		combo6->Enabled = true;
		t6->Enabled = true;
		lab6->Enabled = true;
	}

	else
	{
		combo6->Enabled = false;
		t6->Enabled = false;
		lab6->Enabled = false;
	}
}
//---------------------------------------------------------------------------

void __fastcall TForm2::ch7Click(TObject *Sender)
{
	if(ch7->Checked)
	{
		combo7->Enabled = true;
		t7->Enabled = true;
		lab7->Enabled = true;
	}

	else
	{
		combo7->Enabled = false;
		t7->Enabled = false;
		lab7->Enabled = false;
	}
}
//---------------------------------------------------------------------------

void __fastcall TForm2::sort_combo1Change(TObject *Sender)
{
	if(sort_combo1->ItemIndex > 0)
	{
		sort_combo2->Enabled = true;
		sort_combo2->ItemIndex = 0;
	}
	else
	{
		sort_combo2->Enabled = false;
		sort_combo2->ItemIndex = -1;
	}
}
//---------------------------------------------------------------------------

void __fastcall TForm2::reset_buttonClick(TObject *Sender)
{
	import_combo->ItemIndex = -1;
	//export_combo->ItemIndex = -1;
	rad1->Checked = true;
	brtxt->Text = "";
	//savtxt->Text = "";
	ch1->Checked = false;
	ch2->Checked = false;
	ch3->Checked = false;
	ch4->Checked = false;
	ch5->Checked = false;
	ch6->Checked = false;
	ch7->Checked = false;
	combo1->ItemIndex = -1;
	combo2->ItemIndex = -1;
	combo3->ItemIndex = -1;
	combo4->ItemIndex = -1;
	combo5->ItemIndex = -1;
	combo6->ItemIndex = -1;
	combo7->ItemIndex = -1;
	t1->Text="";
	t2->Text="";
	t3->Text="";
	t4->Text="";
	t5->Text="";
	t6->Text="";
	t7->Text="";
	sort_combo1->ItemIndex = 0;
	sort_combo2->ItemIndex = -1;
	sort_combo2->Enabled = false;
}
//---------------------------------------------------------------------------

void __fastcall TForm2::start_buttonClick(TObject *Sender)
{

	int Ncmt;
	
	remove("c:\\cmt_temp.dat");

	if(import_combo->ItemIndex == -1)
	{
		Application->MessageBox(L"Please select Import Format",
			L"Error",
			MB_OK | MB_ICONERROR);
		return;
	}

	if(rad2->Checked && brtxt->GetTextLen() == 0)
	{
		Application->MessageBox(L"Please select import file",
			L"Error",
			MB_OK | MB_ICONERROR);
		return;
	}

	/*if(export_combo->ItemIndex == -1)
	{
		Application->MessageBox(L"Please select Export Format",
			L"Error",
			MB_OK | MB_ICONERROR);
		return;
	}

	if(savtxt->GetTextLen() == 0)
	{
		Application->MessageBox(L"Please select export file",
			L"Error",
			MB_OK | MB_ICONERROR);
		return;
	}  */

	if((ch1->Checked && combo1->ItemIndex == -1) ||
	   (ch2->Checked && combo2->ItemIndex == -1) ||
	   (ch3->Checked && combo3->ItemIndex == -1) ||
	   (ch4->Checked && combo4->ItemIndex == -1) ||
	   (ch5->Checked && combo5->ItemIndex == -1) ||
	   (ch6->Checked && combo6->ItemIndex == -1) ||
	   (ch7->Checked && combo7->ItemIndex == -1))
	{
		Application->MessageBox(L"Please select > or <",
			L"Error",
			MB_OK | MB_ICONERROR);
		return;
	}

	if((ch2->Checked && t2->GetTextLen() == 0) ||
	   (ch3->Checked && t3->GetTextLen() == 0) ||
	   (ch4->Checked && t4->GetTextLen() == 0) ||
	   (ch5->Checked && t5->GetTextLen() == 0) ||
	   (ch6->Checked && t6->GetTextLen() == 0) ||
	   (ch7->Checked && t7->GetTextLen() == 0))
	{
		Application->MessageBox(L"Please enter value",
			L"Error",
			MB_OK | MB_ICONERROR);

		return;
	}

	int inType = import_combo->ItemIndex;
	//int outType = export_combo->ItemIndex;

	if(rad1->Checked)
	{
		char *netName;

		if(import_combo->ItemIndex == 0) netName = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft00Cmt.txt";
		if(import_combo->ItemIndex == 1) netName = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft01Cmt.txt";
		if(import_combo->ItemIndex == 2) netName = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft02Cmt.txt";
		if(import_combo->ItemIndex == 3) netName = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft03Cmt.txt";
		if(import_combo->ItemIndex == 4) netName = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft04Cmt.txt";
		if(import_combo->ItemIndex == 5) netName = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft05Cmt.txt";
		if(import_combo->ItemIndex == 6) netName = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft06Cmt.txt";
		if(import_combo->ItemIndex == 7) netName = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft07Cmt.txt";
		if(import_combo->ItemIndex == 8) netName = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft08Cmt.txt";
		if(import_combo->ItemIndex == 9) netName = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft09Cmt.txt";
		if(import_combo->ItemIndex == 10) netName = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft10Cmt.txt";
		if(import_combo->ItemIndex == 11) netName = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft11Cmt.txt";
		if(import_combo->ItemIndex == 12) netName = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft12Cmt.txt";
		if(import_combo->ItemIndex == 13) netName = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft13Cmt.txt";
		if(import_combo->ItemIndex == 14) netName = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft14Cmt.txt";
		if(import_combo->ItemIndex == 15) netName = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft15Cmt.txt";
		if(import_combo->ItemIndex == 16) netName = "http://www.minorplanetcenter.net/iau/Ephemerides/Comets/Soft16Cmt.txt";

		try
		{
			TFileStream *fStr = new TFileStream("c:\\cmt_temp.dat", fmCreate);
			H1->Get(netName, fStr);
			delete fStr;
		}

		catch (...)
		{
			Application->MessageBox(L"Unable to download orbital elements",
				L"Error",
				MB_OK | MB_ICONERROR);
			remove("c:\\cmt_temp.dat");
			return;
		}

		//AnsiString str =  savtxt->Text;
		//const char *name = str.c_str();

		Ncmt = import_main(inType, 1, "c:\\cmt_temp.dat", "brb.txt");
		remove("c:\\cmt_temp.dat");
	}

	if(rad2->Checked)
	{
		AnsiString str =  brtxt->Text;
		const char *in_name = str.c_str();

		//AnsiString str2 =  savtxt->Text;
		//const char *out_name = str2.c_str();

		Ncmt = import_main(inType, 1, in_name, "brb.txt");
	}

	Form5->Show();

	for(int i=0; i<Ncmt; i++){

		Form5->ListBox1->Items->Add(cmt[i].full);
	}

}
//---------------------------------------------------------------------------

void __fastcall TForm2::about_buttonClick(TObject *Sender)
{
	Form3->ShowModal();
}
//---------------------------------------------------------------------------

void __fastcall TForm2::help_buttonClick(TObject *Sender)
{
	Form4->ShowModal();
}
//---------------------------------------------------------------------------

void __fastcall TForm2::t2KeyPress(TObject *Sender, wchar_t &Key)
{
	/*if(isdigit(Key) || iscntrl(Key) || Key == '.')
	{
	  //	this -> Handle = true;
	}
	else
	{
	  //	this -> Handle = false;
	}
	  */
	std::string str = "1234567890,.";
	if (str.find(Key) == std::string::npos && Key != VK_BACK)
	{
	   // Znak nije dozvoljen
       Key = 0;
	}

	/*try
	{
		double pom = t2->Text.ToDouble();
		ShowMessage("Input ok");
	}
	catch(...)
	{
		ShowMessage("input nije ok");
	}  */
}
//---------------------------------------------------------------------------

void __fastcall TForm2::rad1Click(TObject *Sender)
{
	if(rad1->Checked)
	{
		brbt->Enabled = false;
		brtxt->Enabled = false;
    }
}
//---------------------------------------------------------------------------

void __fastcall TForm2::rad2Click(TObject *Sender)
{
	if(rad2->Checked)
	{
		brbt->Enabled = true;
		brtxt->Enabled = true;
	}
}
//---------------------------------------------------------------------------

void __fastcall TForm2::brbtClick(TObject *Sender)
{
	openfile->Execute();
	brtxt->Text  =  openfile->FileName;
}
//---------------------------------------------------------------------------

void __fastcall TForm2::brtxtDblClick(TObject *Sender)
{
	openfile->Execute();
	brtxt->Text  =  openfile->FileName;
}
//---------------------------------------------------------------------------

void __fastcall TForm2::savbtClick(TObject *Sender)
{
	if(savefile->Execute()){
		//savtxt->Text = savefile->FileName;

		AnsiString str = savefile->FileName;
		char *name = str.c_str();

		int len = strlen(name);

	   /*	if (!((name[len-1]=='t' && name[len-2]=='x' && name[len-3]=='t' && name[len-4]=='.') ||
		   (name[len-1]=='i' && name[len-2]=='n' && name[len-3]=='i' && name[len-4]=='.') ||
		   (name[len-1]=='t' && name[len-2]=='a' && name[len-3]=='d' && name[len-4]=='.') ||
		   (name[len-1]=='c' && name[len-2]=='s' && name[len-3]=='s' && name[len-4]=='.')))
		{
			if(savefile->FilterIndex==1) savtxt->Text = savtxt->Text + ".txt";
			if(savefile->FilterIndex==2) savtxt->Text = savtxt->Text + ".ini";
			if(savefile->FilterIndex==3) savtxt->Text = savtxt->Text + ".dat";
			if(savefile->FilterIndex==4) savtxt->Text = savtxt->Text + ".ssc";
		} */
	}
	else
	{
		//savtxt->Text = savtxt->Text;
    }

}
//---------------------------------------------------------------------------

void __fastcall TForm2::savtxtDblClick(TObject *Sender)
{
	savefile->Execute();
	//savtxt->Text  =  savefile->FileName;
}
//---------------------------------------------------------------------------

void __fastcall TForm2::savefileClose(TObject *Sender)
{
	//savtxt->Text = "";
}
//---------------------------------------------------------------------------




