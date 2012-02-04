//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "Unit3.h"
#include "Unit5.h"
#include "Unit7.h"
#include "Unit8.h"
#include "Unit11.h"
#include "Unit12.h"
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

	//Form1->Frame61->ProgressBar1->Visible = false;
	//Form1->Frame61->ProgressBar1->Position = 0;
	//Form1->Frame61->Label3->Visible = false;

	if(Form11->ComboBox1->ItemIndex > -1)
		Form1->Frame61->ComboBox1->ItemIndex = Form11->ComboBox1->ItemIndex;
}
//---------------------------------------------------------------------------

void __fastcall TFrame5::Button2Click(TObject *Sender)
{
	Form1->Frame31->Visible = true;
	Form1->Frame51->Visible = false;

	Form12->Close();
}
//---------------------------------------------------------------------------


void __fastcall TFrame5::Button3Click(TObject *Sender)
{
	//PopupActionBar1->Popup(Mouse->CursorPos.x, Mouse->CursorPos.y);

	PopupActionBar1->Popup (Form1->Left + Button3->Left + 5,
							Form1->Top + Button3->Top + Button3->Height + 45);
}
//---------------------------------------------------------------------------

void __fastcall TFrame5::nosort2Click(TObject *Sender)
{
	if(nosort2->Checked) return;

	else{

		nosort2->Checked=true;

		ocistiMemoriju(&Form1->cmt);
		//Form12->Close();
		int importType = Form1->Frame21->ComboBox1->ItemIndex;

		UnicodeString importFile;

		if(Form1->Frame21->RadioButton1->Checked)
			importFile = Form1->Frame21->downloadedFile;

		else
			importFile = Form1->Frame21->Edit1->Text;

		int Ncmt = Form1->import_main(importType, importFile);

		if(Ncmt) Form1->updateListbox(Form1->cmt);
	}
}
//---------------------------------------------------------------------------

void __fastcall TFrame5::name1Click(TObject *Sender)
{
	if(name1->Checked) return;

	else{

		name1->Checked=true;

		//Form12->Close();
		//Form12->Visible = false;

		if(Ascending1->Checked) Form1->cmt = sortList(Form1->cmt, 0);
		else Form1->cmt = sortList(Form1->cmt, 1);

		Form1->updateListbox(Form1->cmt);
		ListBox1Click(Sender);
	}
}
//---------------------------------------------------------------------------

void __fastcall TFrame5::PerihelionDate1Click(TObject *Sender)
{

	if(PerihelionDate1->Checked) return;

	else{

		PerihelionDate1->Checked=true;

		//Form12->Close();
		//Form12->Visible = false;

		if(Ascending1->Checked) Form1->cmt = sortList(Form1->cmt, 2);
		else Form1->cmt = sortList(Form1->cmt, 3);

		Form1->updateListbox(Form1->cmt);
		ListBox1Click(Sender);
	}
}
//---------------------------------------------------------------------------

void __fastcall TFrame5::PericenterDistance1Click(TObject *Sender)
{
	if(PericenterDistance1->Checked) return;

	else{

		PericenterDistance1->Checked=true;

		//Form12->Close();
		//Form12->Visible = false;

		if(Ascending1->Checked) Form1->cmt = sortList(Form1->cmt, 4);
		else Form1->cmt = sortList(Form1->cmt, 5);

		Form1->updateListbox(Form1->cmt);
	}
}
//---------------------------------------------------------------------------

void __fastcall TFrame5::Eccentricity1Click(TObject *Sender)
{
	if(Eccentricity1->Checked) return;

	else{

		Eccentricity1->Checked=true;

		//Form12->Close();
		//Form12->Visible = false;

		if(Ascending1->Checked) Form1->cmt = sortList(Form1->cmt, 6);
		else Form1->cmt = sortList(Form1->cmt, 7);

		Form1->updateListbox(Form1->cmt);
		ListBox1Click(Sender);
	}
}
//---------------------------------------------------------------------------

void __fastcall TFrame5::LongoftheAscNode1Click(TObject *Sender)
{
	if(LongoftheAscNode1->Checked) return;

	else{

		LongoftheAscNode1->Checked=true;

		//Form12->Close();
		//Form12->Visible = false;

		if(Ascending1->Checked) Form1->cmt = sortList(Form1->cmt, 8);
		else Form1->cmt = sortList(Form1->cmt, 9);

		Form1->updateListbox(Form1->cmt);
		ListBox1Click(Sender);
	}
}
//---------------------------------------------------------------------------

void __fastcall TFrame5::LongofPericenter1Click(TObject *Sender)
{
	if(LongofPericenter1->Checked) return;

	else{

		LongofPericenter1->Checked=true;

		//Form12->Close();
		//Form12->Visible = false;

		if(Ascending1->Checked) Form1->cmt = sortList(Form1->cmt, 10);
		else Form1->cmt = sortList(Form1->cmt, 11);

		Form1->updateListbox(Form1->cmt);
		ListBox1Click(Sender);
	}
}
//---------------------------------------------------------------------------

void __fastcall TFrame5::Inclination1Click(TObject *Sender)
{
	if(Inclination1->Checked) return;

	else{

		Inclination1->Checked=true;

		//Form12->Close();
		//Form12->Visible = false;

		if(Ascending1->Checked) Form1->cmt = sortList(Form1->cmt, 12);
		else Form1->cmt = sortList(Form1->cmt, 13);

		Form1->updateListbox(Form1->cmt);
		ListBox1Click(Sender);
	}
}
//---------------------------------------------------------------------------

void __fastcall TFrame5::Period1Click(TObject *Sender)
{
	if(Period1->Checked) return;

	else{

		Period1->Checked=true;

		//Form12->Close();
		//Form12->Visible = false;

		if(Ascending1->Checked) Form1->cmt = sortList(Form1->cmt, 14);
		else Form1->cmt = sortList(Form1->cmt, 15);

		Form1->updateListbox(Form1->cmt);
		ListBox1Click(Sender);
	}
}
//---------------------------------------------------------------------------

void __fastcall TFrame5::Ascending1Click(TObject *Sender)
{
	if(Ascending1->Checked) return;

	else{
		Ascending1->Checked=true;

		//Form12->Close();
		//Form12->Visible = false;

		if(nosort2->Checked) return;
		if(name1->Checked) Form1->cmt = sortList(Form1->cmt, 0);
		if(PerihelionDate1->Checked) Form1->cmt = sortList(Form1->cmt, 2);
		if(PericenterDistance1->Checked) Form1->cmt = sortList(Form1->cmt, 4);
		if(Eccentricity1->Checked) Form1->cmt = sortList(Form1->cmt, 6);
		if(LongoftheAscNode1->Checked) Form1->cmt = sortList(Form1->cmt, 8);
		if(LongofPericenter1->Checked) Form1->cmt = sortList(Form1->cmt, 10);
		if(Inclination1->Checked) Form1->cmt = sortList(Form1->cmt, 12);
		if(Period1->Checked) Form1->cmt = sortList(Form1->cmt, 14);

		Form1->updateListbox(Form1->cmt);
		ListBox1Click(Sender);
	}
}
//---------------------------------------------------------------------------

void __fastcall TFrame5::Descending1Click(TObject *Sender)
{
	if(Descending1->Checked) return;

	else{
		Descending1->Checked=true;

		//Form12->Close();
		//Form12->Visible = false;

		if(nosort2->Checked) return;
		if(name1->Checked) Form1->cmt = sortList(Form1->cmt, 1);
		if(PerihelionDate1->Checked) Form1->cmt = sortList(Form1->cmt, 3);
		if(PericenterDistance1->Checked) Form1->cmt = sortList(Form1->cmt, 5);
		if(Eccentricity1->Checked) Form1->cmt = sortList(Form1->cmt, 7);
		if(LongoftheAscNode1->Checked) Form1->cmt = sortList(Form1->cmt, 9);
		if(LongofPericenter1->Checked) Form1->cmt = sortList(Form1->cmt, 11);
		if(Inclination1->Checked) Form1->cmt = sortList(Form1->cmt, 13);
		if(Period1->Checked) Form1->cmt = sortList(Form1->cmt, 15);

		Form1->updateListbox(Form1->cmt);
		ListBox1Click(Sender);
	}
}
//---------------------------------------------------------------------------

void __fastcall TFrame5::Button5Click(TObject *Sender)
{
	ListBox1->Clear();
	Button1->Enabled = false;
	Form1->Frame31->ProgressBar1->Visible = false;
	Form1->Frame31->Button1->Enabled = false;
	ocistiMemoriju(&Form1->cmt);

	Form12->Close();
	//Form12->Visible = false;

	Button3->Enabled = false;
	Button7->Caption = "Details -->";
	Button7->Enabled = false;
	Button4->Enabled = false;
	Button5->Enabled = false;
	Button6->Enabled = false;
	Ncmt = 0;
	Label20->Caption = "Comets: 0";
}
//---------------------------------------------------------------------------



void __fastcall TFrame5::PopupActionBar1Change(TObject *Sender, TMenuItem *Source,
          bool Rebuild)
{
	//Form12->Close();

	if(canDoChange == false) {

		//samo da se nakon prvog klika ne napravi update listboxa
		canDoChange = true;
		return;
	}

	//if(canDoChange){

		if(name1->Checked && Ascending1->Checked) Form1->cmt = sortList(Form1->cmt, 0);
		if(name1->Checked && Descending1->Checked) Form1->cmt = sortList(Form1->cmt, 1);
		if(PerihelionDate1->Checked && Ascending1->Checked) Form1->cmt = sortList(Form1->cmt, 2);
		if(PerihelionDate1->Checked && Descending1->Checked) Form1->cmt = sortList(Form1->cmt, 3);
		if(PericenterDistance1->Checked && Ascending1->Checked) Form1->cmt = sortList(Form1->cmt, 4);
		if(PericenterDistance1->Checked && Descending1->Checked) Form1->cmt = sortList(Form1->cmt, 5);
		if(Eccentricity1->Checked && Ascending1->Checked) Form1->cmt = sortList(Form1->cmt, 6);
		if(Eccentricity1->Checked && Descending1->Checked) Form1->cmt = sortList(Form1->cmt, 7);
		if(LongoftheAscNode1->Checked && Ascending1->Checked) Form1->cmt = sortList(Form1->cmt, 8);
		if(LongoftheAscNode1->Checked && Descending1->Checked) Form1->cmt = sortList(Form1->cmt, 9);
		if(LongofPericenter1->Checked && Ascending1->Checked) Form1->cmt = sortList(Form1->cmt, 10);
		if(LongofPericenter1->Checked && Descending1->Checked) Form1->cmt = sortList(Form1->cmt, 11);
		if(Inclination1->Checked && Ascending1->Checked) Form1->cmt = sortList(Form1->cmt, 12);
		if(Inclination1->Checked && Descending1->Checked) Form1->cmt = sortList(Form1->cmt, 13);
		if(Period1->Checked && Ascending1->Checked) Form1->cmt = sortList(Form1->cmt, 14);
		if(Period1->Checked && Descending1->Checked) Form1->cmt = sortList(Form1->cmt, 15);

		Form1->updateListbox(Form1->cmt);
	//}
}
//---------------------------------------------------------------------------

void __fastcall TFrame5::Button4Click(TObject *Sender)
{
	int i = ListBox1->ItemIndex;

	if(i == -1) return;

	int total = totalComets(Form1->cmt);
	bool lastItem = false;

	if(i==0 && total == 1)  {
		//ako je samo jedan element
		ocistiMemoriju(&Form1->cmt);
		lastItem = true;
	}

	else if(i==0 && total > 1)
		//ako je odabran prvi element od vise njih
		deleteFirst(&Form1->cmt);

	else if(i+1 == total && total > 1){
		//ako je odabran zadnji element od vise njih
		deleteLast(&Form1->cmt);
		lastItem = true;
	}

	else //if (lastItem==false && total > 1)
	{
		//ako ima vise elemenata, a odabrani nije ni prvi ni zadnji
		Comet *cmt = getCmt(Form1->cmt, i);
		deleteFromMiddle(cmt);
	}

	ListBox1->Items->Delete(i);

	if(lastItem){

		i--;

		if(i==-1){

			Button1->Enabled = false;
			Form1->Frame31->ProgressBar1->Visible = false;
			Form1->Frame31->Button1->Enabled = false;

			//Form12->Close();
			//Form12->Visible = false;

			Button3->Enabled = false;
			Button4->Enabled = false;
			Button5->Enabled = false;
			return;
		}
	}

	Ncmt--;
	Label20->Caption = "Comets: " + String(Ncmt);

	ListBox1->ItemIndex = i;
	ListBox1Click(Sender);
}
//---------------------------------------------------------------------------

void __fastcall TFrame5::ListBox1KeyPress(TObject *Sender, System::WideChar &Key)
{
	if(Key == VK_DELETE || Key == VK_BACK) Button4Click(Sender);
	else return;
}
//---------------------------------------------------------------------------


void __fastcall TFrame5::Button7Click(TObject *Sender)
{
	if(Form12->Visible) {
		Form12->Close();

		Button7->Caption = "Details -->";
		return;
	}

	int i = ListBox1->ItemIndex;
	if(i==-1){
		ListBox1->ItemIndex = 0;
		i=0;
	}

	Button7->Caption = "<-- Details";
	Form12->Show();
	Form1->SetFocus();

	Comet *cmt = getCmt(Form1->cmt, i);
	Form12->updateEditFields(cmt);
	delete cmt;
}
//---------------------------------------------------------------------------

void __fastcall TFrame5::ListBox1Click(TObject *Sender)
{
	if(Form12->Visible)	{

		Comet *cmt = getCmt(Form1->cmt, ListBox1->ItemIndex);
		Form12->updateEditFields(cmt);
		delete cmt;
	}
}
//---------------------------------------------------------------------------

void __fastcall TFrame5::Button6Click(TObject *Sender)
{
	int resx = GetSystemMetrics(SM_CXSCREEN);
	int resy = GetSystemMetrics(SM_CYSCREEN);

	Form11->Width = resx - 100;
	Form11->Height = resy - 100;
	Form11->Position = poDesktopCenter;

	//Form11->ComboBox1->ItemIndex = -1;
	//Form11->RichEdit1->Clear();

	Form11->ComboBox1CloseUp(Sender);

	Form11->ShowModal();
}
//---------------------------------------------------------------------------

void __fastcall TFrame5::ListBox1DblClick(TObject *Sender)
{
	if(Form12->Visible == false)  {
		Form12->Show();
		Button7->Caption = "<-- Details";
		Form1->SetFocus();
	}

	Comet *cmt = getCmt(Form1->cmt, ListBox1->ItemIndex);
	Form12->updateEditFields(cmt);
	delete cmt;
}
//---------------------------------------------------------------------------

