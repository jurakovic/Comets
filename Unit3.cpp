//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "Unit3.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TFrame3 *Frame3;
//---------------------------------------------------------------------------
__fastcall TFrame3::TFrame3(TComponent* Owner)
	: TFrame(Owner)
{
}
//---------------------------------------------------------------------------
void __fastcall TFrame3::Button1Click(TObject *Sender)
{
    if(!Form1->define_exclude()) return;

	Form1->Frame41->Visible = true;
	Form1->Frame31->Visible = false;

	if(Form1->Frame21->ComboBox1->ItemIndex == 18)
		Form1->Frame41->CheckBox1->Visible = true;
	else
	   Form1->Frame41->CheckBox1->Visible = false;

	Form1->Frame41->ProgressBar1->Visible = false;
	Form1->Frame41->Label2->Visible = false;
	Form1->Frame41->Label3->Visible = false;
	Form1->Frame41->Button1->Enabled = false;
}
//---------------------------------------------------------------------------

void __fastcall TFrame3::Button2Click(TObject *Sender)
{
	Form1->Frame21->Visible = true;
	Form1->Frame31->Visible = false;
}


//---------------------------------------------------------------------------

void __fastcall TFrame3::CheckBox1Click(TObject *Sender)
{
	if(CheckBox1->Checked){
		ComboBox1->Enabled = true;
		MaskEdit1->Enabled = true;
	}
	else{
		ComboBox1->Enabled = false;
		MaskEdit1->Enabled = false;
    }
}
//---------------------------------------------------------------------------

void __fastcall TFrame3::CheckBox2Click(TObject *Sender)
{
	if(CheckBox2->Checked){
		ComboBox2->Enabled = true;
		Edit2->Enabled = true;
		Label2->Enabled = true;
	}
	else{
		ComboBox2->Enabled = false;
		Edit2->Enabled = false;
		Label2->Enabled = false;
    }
}
//---------------------------------------------------------------------------

void __fastcall TFrame3::CheckBox3Click(TObject *Sender)
{
	if(CheckBox3->Checked){
		ComboBox3->Enabled = true;
		Edit3->Enabled = true;
	}
	else{
		ComboBox3->Enabled = false;
		Edit3->Enabled = false;
    }
}
//---------------------------------------------------------------------------

void __fastcall TFrame3::CheckBox4Click(TObject *Sender)
{
	if(CheckBox4->Checked){
		ComboBox4->Enabled = true;
		Edit4->Enabled = true;
		Label4->Enabled = true;
	}
	else{
		ComboBox4->Enabled = false;
		Edit4->Enabled = false;
		Label4->Enabled = false;
    }
}
//---------------------------------------------------------------------------

void __fastcall TFrame3::CheckBox5Click(TObject *Sender)
{
	if(CheckBox5->Checked){
		ComboBox5->Enabled = true;
		Edit5->Enabled = true;
		Label5->Enabled = true;
	}
	else{
		ComboBox5->Enabled = false;
		Edit5->Enabled = false;
		Label5->Enabled = false;
    }
}
//---------------------------------------------------------------------------

void __fastcall TFrame3::CheckBox6Click(TObject *Sender)
{
	if(CheckBox6->Checked){
		ComboBox6->Enabled = true;
		Edit6->Enabled = true;
		Label6->Enabled = true;
	}
	else{
		ComboBox6->Enabled = false;
		Edit6->Enabled = false;
		Label6->Enabled = false;
    }
}
//---------------------------------------------------------------------------

void __fastcall TFrame3::CheckBox7Click(TObject *Sender)
{
	if(CheckBox7->Checked){
		ComboBox7->Enabled = true;
		Edit7->Enabled = true;
		Label7->Enabled = true;
	}
	else{
		ComboBox7->Enabled = false;
		Edit7->Enabled = false;
		Label7->Enabled = false;
    }

}

//---------------------------------------------------------------------------

