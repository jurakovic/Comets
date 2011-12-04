//---------------------------------------------------------------------------

#ifndef Unit2H
#define Unit2H
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <Mask.hpp>

#include "File2.h"
#include "IdBaseComponent.hpp"
#include "IdComponent.hpp"
#include "IdHTTP.hpp"
#include "IdTCPClient.hpp"
#include "IdTCPConnection.hpp"
#include <Dialogs.hpp>

#include "Unit5.h"
//include "Unit2.h"
#include <ExtCtrls.hpp>
#include <jpeg.hpp>
#include <ComCtrls.hpp>
#include <Dialogs.hpp>
#include <Menus.hpp>
#include "IdBaseComponent.hpp"
#include "IdComponent.hpp"
#include "IdHTTP.hpp"
#include "IdTCPClient.hpp"
#include "IdTCPConnection.hpp"
#include <DBCtrls.hpp>

#include "File2.h"



//---------------------------------------------------------------------------
class TForm2 : public TForm
{
__published:	// IDE-managed Components
	TComboBox *import_combo;
	TCheckBox *ch1;
	TCheckBox *ch2;
	TCheckBox *ch3;
	TCheckBox *ch4;
	TCheckBox *ch5;
	TCheckBox *ch6;
	TCheckBox *ch7;
	TComboBox *combo1;
	TComboBox *combo2;
	TComboBox *combo3;
	TComboBox *combo4;
	TComboBox *combo5;
	TComboBox *combo6;
	TComboBox *combo7;
	TMaskEdit *t1;
	TEdit *t2;
	TEdit *t3;
	TEdit *t4;
	TEdit *t5;
	TEdit *t6;
	TEdit *t7;
	TLabel *lab5;
	TLabel *lab6;
	TLabel *lab4;
	TLabel *lab7;
	TLabel *lab2;
	TLabel *Label11;
	TLabel *Label12;
	TComboBox *sort_combo1;
	TComboBox *sort_combo2;
	TButton *start_button;
	TButton *help_button;
	TButton *about_button;
	TButton *reset_button;
	TRadioButton *rad1;
	TRadioButton *rad2;
	TOpenDialog *openfile;
	TSaveDialog *savefile;
	TIdHTTP *H1;
	TLabel *Label3;
	TButton *brbt;
	TEdit *brtxt;
	TGroupBox *GroupBox1;
	TLabel *Label1;
	TGroupBox *GroupBox2;
	TGroupBox *GroupBox3;
	TProgressBar *pBar1;
	void __fastcall ch1Click(TObject *Sender);
	void __fastcall ch2Click(TObject *Sender);
	void __fastcall ch3Click(TObject *Sender);
	void __fastcall ch4Click(TObject *Sender);
	void __fastcall ch5Click(TObject *Sender);
	void __fastcall ch6Click(TObject *Sender);
	void __fastcall ch7Click(TObject *Sender);
	void __fastcall reset_buttonClick(TObject *Sender);
	void __fastcall start_buttonClick(TObject *Sender);
	void __fastcall about_buttonClick(TObject *Sender);
	void __fastcall sort_combo1Change(TObject *Sender);
	void __fastcall t2KeyPress(TObject *Sender, wchar_t &Key);
	void __fastcall help_buttonClick(TObject *Sender);
	void __fastcall rad1Click(TObject *Sender);
	void __fastcall rad2Click(TObject *Sender);
	void __fastcall brbtClick(TObject *Sender);
	void __fastcall brtxtDblClick(TObject *Sender);
	void __fastcall savbtClick(TObject *Sender);
	void __fastcall savtxtDblClick(TObject *Sender);
	void __fastcall savefileClose(TObject *Sender);
private:	// User declarations
public:		// User declarations
	__fastcall TForm2(TComponent* Owner);

	struct Comet cmt[MAX_CMT];
	struct Excludings excl;

int import_main (int Ty, int exp_ty, const char *fin_name, const char *fout_name)
{

	using namespace std;

	char c;
	FILE *fin;

	if(!define_exclude()) return  0;

	fin=fopen(fin_name, "r");

	if(!fin)
	{
		Application->MessageBox(L"Unable to open input file",
			L"Error",
			MB_OK | MB_ICONERROR);
		return 0;
	}

	int Ncmt=0;
	while ((c=fgetc(fin)) != EOF){
		if (c=='\n') Ncmt++;
	}

	rewind(fin);

	if (Ty==3 || Ty==8 || Ty==10) Ncmt/=2;		// kao gore, samo što je 1 komet kroz 2 reda
	if (Ty==8 || Ty==4 || Ty==5) --Ncmt;
	if (Ty==7) Ncmt-=15;

	if (Ty==17) {
		//Ncmt-=7; 
		Ncmt/=13;
	}

	if (Ty== 0) Ncmt = import_mpc (Ncmt, fin);
	if (Ty== 1) Ncmt = import_skymap (Ncmt, fin);
	if (Ty== 2) Ncmt = import_guide (Ncmt, fin);
	if (Ty== 3) Ncmt = import_xephem (Ncmt, fin);
	if (Ty== 4) Ncmt = import_home_planet (Ncmt, fin);
	if (Ty== 5) Ncmt = import_mystars (Ncmt, fin);
	if (Ty== 6) Ncmt = import_thesky (Ncmt, fin);
	if (Ty== 7) Ncmt = import_starry_night (Ncmt, fin);
	if (Ty== 8) Ncmt = import_deep_space (Ncmt, fin);
	if (Ty== 9) Ncmt = import_pc_tcs (Ncmt, fin);
	if (Ty==10) Ncmt = import_ecu (Ncmt, fin);
	if (Ty==11) Ncmt = import_dance (Ncmt, fin);
	if (Ty==12) Ncmt = import_megastar (Ncmt, fin);
	if (Ty==13) Ncmt = import_skychart (Ncmt, fin);
	if (Ty==14) Ncmt = import_voyager (Ncmt, fin);
	if (Ty==15) Ncmt = import_skytools (Ncmt, fin);
	if (Ty==16) Ncmt = import_thesky (Ncmt, fin);
	if (Ty==17) Ncmt = import_cfw (Ncmt, fin);
	//if (Ty==456) Ncmt = import_nasa1 (Ncmt, fin);
	//if (Ty==789) Ncmt = import_nasa2 (Ncmt, fin);



	fclose(fin);

	if(Ncmt == 0)
	{
		Application->MessageBox(L"There are no imported comets\n\n"
			L"Two possible reasons:\n\n"
			L" - You selected wrong import format\n"
			L" - Excluding rules are too high",
			L"Error",
			MB_OK | MB_ICONERROR);
		return 0;
	}


	pBar1->Position = 0;
	pBar1->Max = 3*(Ncmt/4);

	if(!sort_data(Ncmt)) return 0;

	pBar1->Position = 0;
	return Ncmt;
}


void export_main (int Ncmt, int exp_ty, const char *fout_name)
{
	FILE *fout;

	fout=fopen(fout_name, "w");

	if (exp_ty== 0) export_mpc (Ncmt, fout);
	if (exp_ty== 1) export_skymap (Ncmt, fout);
	if (exp_ty== 2) export_guide (Ncmt, fout);
	if (exp_ty== 3) export_xephem (Ncmt, fout);
	if (exp_ty== 4) export_home_planet (Ncmt, fout);
	if (exp_ty== 5) export_mystars (Ncmt, fout);
	if (exp_ty== 6) export_thesky (Ncmt, fout);
	if (exp_ty== 7) export_starry_night (Ncmt, fout);
	if (exp_ty== 8) export_deep_space (Ncmt, fout);
	if (exp_ty== 9) export_pc_tcs (Ncmt, fout);
	if (exp_ty==10) export_ecu (Ncmt, fout);
	if (exp_ty==11) export_dance (Ncmt, fout);
	if (exp_ty==12) export_megastar (Ncmt, fout);
	if (exp_ty==13) export_skychart (Ncmt, fout);
	if (exp_ty==14) export_voyager (Ncmt, fout);
	if (exp_ty==15) export_skytools (Ncmt, fout);
	if (exp_ty==16) export_thesky (Ncmt, fout);
	if (exp_ty==17) export_ssc (Ncmt, fout);
	if (exp_ty==18) export_stell (Ncmt, fout);

	fclose(fout);
	Application->MessageBox(L"File is successfully saved",
			L"OK",
			MB_OK | MB_ICONINFORMATION);
	return;
}

int import_mpc (int N, FILE *fin)
{

	int j, k, l;
	int m, line=1;
	char x[30+1];

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "%14c %d %02d %02d.%04d %f %f %f %f %f%12c%f %f %55c %30[^\n]%*c",		// %f%12c%f mora bit tako zajedno
			x, &cmt[i].y, &cmt[i].m, &cmt[i].d, &cmt[i].h,
			&cmt[i].q, &cmt[i].e, &cmt[i].pn, &cmt[i].an,
			&cmt[i].i, x, &cmt[i].H, &cmt[i].G, cmt[i].full, x);

		if (m < 15){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		remove_spaces(cmt[i].full);

		for (j=0; cmt[i].full[j]!='\0'; j++){
			if ((isdigit(cmt[i].full[j]) && cmt[i].full[j+1]=='P' && cmt[i].full[j+2]=='/') ||
				(isdigit(cmt[i].full[j]) && cmt[i].full[j+1]=='D' && cmt[i].full[j+2]=='/')){

				for(k=0; cmt[i].full[k]!='/'; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k]='\0';
				++k;
				for(l=0; cmt[i].full[k]!='\0'; l++, k++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
			}

			if (cmt[i].full[j]=='('){
				for(k=0; cmt[i].full[k]!='('; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k-1]='\0';

				++k;
				for(l=0; cmt[i].full[k]!=')'; k++, l++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
			}
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;

		if(do_exclude(i)){N--; i--;}
	}

	return N;
}
int import_skymap (int N, FILE *fin)
{

	int j, k, l, u, t=0, space;
	int m, line=1, len;

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "%47c %4d %2d %2d.%4d %f %f %f %f %f %f %f\n",
			cmt[i].full, &cmt[i].y, &cmt[i].m, &cmt[i].d,
			&cmt[i].h, &cmt[i].q, &cmt[i].e, &cmt[i].pn,
			&cmt[i].an, &cmt[i].i, &cmt[i].H, &cmt[i].G);

		if (m < 12){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		remove_spaces(cmt[i].full);

		for (j=0; cmt[i].full[j]!='\0'; j++){
			if ((isdigit(cmt[i].full[j]) && cmt[i].full[j+1]=='P' && cmt[i].full[j+2]==' ') ||
				(isdigit(cmt[i].full[j]) && cmt[i].full[j+1]=='D' && cmt[i].full[j+2]==' ')){

				for(k=0; cmt[i].full[k]!=' '; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k]='\0';
				++k;
				for(l=0; cmt[i].full[k]!='\0'; l++, k++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
			}

		if ((cmt[i].full[0]=='C' && cmt[i].full[1]=='/') ||
			(cmt[i].full[0]=='P' && cmt[i].full[1]=='/') ||
			(cmt[i].full[0]=='D' && cmt[i].full[1]=='/')){
				space=0;
				len = strlen(cmt[i].full);
				for(u=0; u<len; u++){
					if (cmt[i].full[u]==' ' && space==1) {
						t=u;
						break;
					}
					else if(cmt[i].full[u]==' ') space++;
				}

				for(k=0; k<t; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k]='\0';

				++k;
				for(l=0; cmt[i].full[k]!='\0'; k++, l++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
			}
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;

		if(do_exclude(i)){N--; i--;}
	}

	return N;
}
int import_guide (int N, FILE *fin)
{

	int j, m, line=1, len;
	char c, x[20];

	for (int i=0; i<N; i++) {

		j=0;
		while ((c=fgetc(fin)) != '(' ){
			cmt[i].name[j++]=c;
		}
		cmt[i].name[j-1]='\0';

		j=0;
		while ((c=fgetc(fin)) != ')' ){
			cmt[i].ID[j++]=c;
		}
		cmt[i].ID[j]='\0';


		if ((cmt[i].name[0]=='P' && cmt[i].name[1]=='/') ||
			(cmt[i].name[0]=='D' && cmt[i].name[1]=='/')){
			len = strlen(cmt[i].name);
			for (j=0; j<len; j++)
				cmt[i].name[j]=cmt[i].name[j+2];		//j+2 jer je na mjestu 0='P', 1='/'

			strcpy(cmt[i].full, cmt[i].ID);
			strcat(cmt[i].full, "/");
			strcat(cmt[i].full, cmt[i].name);
		}

		else {
			strcpy(cmt[i].full, cmt[i].ID);
			strcat(cmt[i].full, " (");
			strcat(cmt[i].full, cmt[i].name);
			strcat(cmt[i].full, ")");
		}

		m = fscanf(fin, "%d.%d %d %d 0.0 %f %f %f %f %f 2000.0 %f %f %15[^\n]%*c",
			&cmt[i].d, &cmt[i].h, &cmt[i].m, &cmt[i].y,
			&cmt[i].q,  &cmt[i].e, &cmt[i].i, &cmt[i].pn,
			&cmt[i].an, &cmt[i].H, &cmt[i].G, x);

		if (m < 12){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;

		if(do_exclude(i)){N--; i--;}
	}

	return N;
}
int import_xephem (int N, FILE *fin)
{

	//info: http://www.clearskyinstitute.com/xephem/help/xephem.html#mozTocId215848

	int j, k, l;
	int mm, dd, yy;
	long int T;
	int nula, m, line=2;
	float smAxis, mdMotion, mAnomaly;
	char c, x[25+1];

	for (int i=0; i<N; i++) {

		fscanf(fin, "# From %25[^\n]%*c", x);

		j=0;
		while ((c=fgetc(fin)) != ',' ){
			cmt[i].full[j++]=c;
		}

		for (j=0; cmt[i].full[j]!='\0'; j++){
			if (isdigit(cmt[i].full[j]) &&
				cmt[i].full[j+1]=='P' &&
				cmt[i].full[j+2]=='/'){

				for(k=0; cmt[i].full[k]!='/'; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k]='\0';
				++k;
				for(l=0; cmt[i].full[k]!='\0'; l++, k++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
			}

			if (cmt[i].full[j]=='('){
				for(k=0; cmt[i].full[k]!='('; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k-1]='\0';

				++k;
				for(l=0; cmt[i].full[k]!=')'; k++, l++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
			}
		}

		c=fgetc(fin);

		if(c == 'e'){
			m = fscanf(fin, ",%f,%f,%f,%f,%f,%f,%f,%d/%d.%d/%d,2000,g %f,%f\n",
				&cmt[i].i, &cmt[i].an, &cmt[i].pn, &smAxis,
				&mdMotion, &cmt[i].e, &mAnomaly, &mm, &dd,
				&nula, &yy, &cmt[i].H, &cmt[i].G);

			if (m < 13){
				//cout << "\n\n  Unable to read data in line " << line;
				fscanf(fin, "%*[^\n]\n");
				N--; i--; line++;
				continue;
			}

			cmt[i].q = smAxis*(1-cmt[i].e);
			T = greg_to_jul (yy, mm, dd);
			cmt[i].T = T - mAnomaly/mdMotion;

			jul_to_greg(cmt[i].T, cmt[i].y, cmt[i].m, cmt[i].d);

			line+=2;

			if(do_exclude(i)){N--; i--;}
		}

		if(c == 'p'){
			m = fscanf(fin, ",%d/%d.%d/%d,%f,%f,%f,%f,2000,%f,%f\n",
				&cmt[i].m, &cmt[i].d, &cmt[i].h, &cmt[i].y,
				&cmt[i].i, &cmt[i].pn, &cmt[i].q, &cmt[i].an,
				&cmt[i].H, &cmt[i].G);

			if (m < 10){
				//cout << "\n\n  Unable to read data in line " << line;
				fscanf(fin, "%*[^\n]\n");
				N--; i--; line++;
				continue;
			}

			cmt[i].e = 1.000000;
			cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, cmt[i].d);
			line+=2;

			if(do_exclude(i)){N--; i--;}
		}

		if(c == 'h'){
			m = fscanf(fin, ",%d/%d.%d/%d,%f,%f,%f,%f,%f,2000,%f,%f\n",
				&cmt[i].m, &cmt[i].d, &cmt[i].h, &cmt[i].y,
				&cmt[i].i, &cmt[i].an, &cmt[i].pn, &cmt[i].e,
				&cmt[i].q, &cmt[i].H, &cmt[i].G);

			if (m < 11){
				//cout << "\n\n  Unable to read data in line " << line;
				fscanf(fin, "%*[^\n]\n");
				N--; i--; line++;
				continue;
			}

			cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, cmt[i].d);
			line+=2;

			if(do_exclude(i)){N--; i--;}
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].sort = get_sort_key(cmt[i].ID);
	}

	return N;
}
int import_home_planet (int N, FILE *fin)
{

	int j, k, l;
	int m, line=1;
	char c, x[50+1];

	fscanf(fin, "%*[^\n]\n");

	for (int i=0; i<N; i++) {

		j=0;
		while ((c=fgetc(fin)) != ',' ){
			cmt[i].full[j++]=c;
		}

		m = fscanf(fin, "%d-%d-%d.%d,%f,%f,%f,%f,%f,%50[^\n]%*c",
			&cmt[i].y, &cmt[i].m, &cmt[i].d, &cmt[i].h,
			&cmt[i].q, &cmt[i].e, &cmt[i].pn, &cmt[i].an,
			&cmt[i].i, x);

		if (m < 10){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		for (j=0; cmt[i].full[j]!='\0'; j++){
			if (isdigit(cmt[i].full[j]) &&
				cmt[i].full[j+1]=='P' &&
				cmt[i].full[j+2]=='/'){

				for(k=0; cmt[i].full[k]!='/'; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k]='\0';
				++k;
				for(l=0; cmt[i].full[k]!='\0'; l++, k++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
			}

			if (cmt[i].full[j]=='('){
				for(k=0; cmt[i].full[k]!='('; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k-1]='\0';

				++k;
				for(l=0; cmt[i].full[k]!=')'; k++, l++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
			}
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;

		if(do_exclude(i)){N--; i--;}
	}

	return N;
}
int import_mystars (int N, FILE *fin)
{

	int j, k, l;
	int m, line=1;
	char c, x[30+1];

	fscanf(fin, "%*[^\n]\n");

	for (int i=0; i<N; i++) {

		j=0;
		while ((c=fgetc(fin)) != ';' ){
			cmt[i].full[j++]=c;
		}

		m = fscanf(fin, "%ld.%d %f %f %f %f %f %f %f %30[^\n]%*c",
			&cmt[i].T, &cmt[i].h, &cmt[i].pn, &cmt[i].e,
			&cmt[i].q, &cmt[i].i, &cmt[i].an, &cmt[i].H,
			&cmt[i].G, x);

		if (m < 10){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		for (j=0; cmt[i].full[j]!='\0'; j++){
			if (isdigit(cmt[i].full[j]) &&
				cmt[i].full[j+1]=='P' &&
				cmt[i].full[j+2]=='/'){

				for(k=0; cmt[i].full[k]!='/'; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k]='\0';
				++k;
				for(l=0; cmt[i].full[k]!='\0'; l++, k++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
			}

			if (cmt[i].full[j]=='('){
				for(k=0; cmt[i].full[k]!='('; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k-1]='\0';

				++k;
				for(l=0; cmt[i].full[k]!=')'; k++, l++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
			}
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T += 2400000;
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;

		jul_to_greg(cmt[i].T, cmt[i].y, cmt[i].m, cmt[i].d);

		if(do_exclude(i)){N--; i--;}
	}

	return N;
}
int import_thesky (int N, FILE *fin)
{

	int j, k, l;
	int m, line=1;
	float G;
	char x[20+1];

	for (int i=0; i<N; i++) {
//		fscanf(fin, "%40c %*c %d %*c %4d %2d %2d %*c %d %*c %f %*c %f %*c %f %*c %f %*c %f %*c %f %25[^\n]%*c",     stari nacin
		m = fscanf(fin, "%45c %4d%2d%2d.%d | %f | %f | %f | %f | %f | %f | %f %20[^\n]%*c",
			cmt[i].full, &cmt[i].y, &cmt[i].m,
			&cmt[i].d, &cmt[i].h, &cmt[i].q, &cmt[i].e,
			&cmt[i].pn, &cmt[i].an, &cmt[i].i, &cmt[i].H,
			&G, x);

		cmt[i].G = G/2.5;

		if (m < 13){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		remove_spaces(cmt[i].full);

		for (j=0; cmt[i].full[j]!='\0'; j++){
			if (isdigit(cmt[i].full[j]) &&
				cmt[i].full[j+1]=='P' &&
				cmt[i].full[j+2]=='/'){

				for(k=0; cmt[i].full[k]!='/'; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k]='\0';
				++k;
				for(l=0; cmt[i].full[k]!='\0'; l++, k++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
			}

			if (cmt[i].full[j]=='('){
				for(k=0; cmt[i].full[k]!='('; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k-1]='\0';

				++k;
				for(l=0; cmt[i].full[k]!=')'; k++, l++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
			}
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, cmt[i].d);
		remove_spaces (cmt[i].name);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;

		if(do_exclude(i)){N--; i--;}
	}

	return N;
}
int import_starry_night (int N, FILE *fin)
{

	int j, k;
	int m, line=1, nula;
	char nulanula[11], praznina[6];
	float G;
	long int y;
	char c, x[20+1];

	for (int i=0; i<16; i++) fscanf(fin, "%*[^\n]\n");

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "%5c%29c %f %8c %f %f %f %f %f %ld.%d %ld.5 %f  %13c %20[^\n]%*c",
			praznina, cmt[i].name, &cmt[i].H, nulanula, &cmt[i].e, &cmt[i].q, &cmt[i].an,
			&cmt[i].pn, &cmt[i].i, &cmt[i].T, &cmt[i].h,
			&y, &G, cmt[i].ID, x);

		cmt[i].G = G/2.5;

		if (m < 15){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		remove_spaces(cmt[i].name);
		remove_spaces(cmt[i].ID);

		if ((cmt[i].ID[0]=='C' && cmt[i].ID[1]=='/') ||
			(cmt[i].ID[0]=='P' && cmt[i].ID[1]=='/')){

			strcpy(cmt[i].full, cmt[i].ID);
			strcat(cmt[i].full, " (");
			strcat(cmt[i].full, cmt[i].name);
			strcat(cmt[i].full, ")");
		}

		else {
			strcpy(cmt[i].full, cmt[i].ID);
			strcat(cmt[i].full, "/");
			strcat(cmt[i].full, cmt[i].name);
		}


		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;

		jul_to_greg(cmt[i].T, cmt[i].y, cmt[i].m, cmt[i].d);

		if(do_exclude(i)){N--; i--;}
	}

	return N;
}
int import_deep_space (int N, FILE *fin)
{

	int j, m, line=2;
	float G;
	char c, x[8+1];

	//fscanf(fin, "Type C: Equinox Year Month Day q e Peri Node i Mag k\n");
	//fscanf(fin, "Type A: Equinox Year Month Day a M e Peri Node i H G\n");
	fscanf(fin, "%*[^\n]\n");
	fscanf(fin, "%*[^\n]\n");

	for (int i=0; i<N; i++) {

		j=0;
		while ((c=fgetc(fin)) != '(' ){
			cmt[i].name[j++]=c;
		}
		cmt[i].name[j-1]='\0';

		j=0;
		while ((c=fgetc(fin)) != ')' ){
			cmt[i].ID[j++]=c;
		}
		cmt[i].ID[j]='\0';

		m = fscanf(fin, "\n%8c %d %d %d.%d %f %f %f %f %f %f %f\n",
			x, &cmt[i].y, &cmt[i].m, &cmt[i].d, &cmt[i].h,
			&cmt[i].q, &cmt[i].e, &cmt[i].pn, &cmt[i].an,
			&cmt[i].i, &cmt[i].H, &G);

		cmt[i].G = G/2.5;

		if (m < 12){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		if ((cmt[i].ID[0]=='C' && cmt[i].ID[1]=='/') ||
			(cmt[i].ID[0]=='P' && cmt[i].ID[1]=='/')){

			strcpy(cmt[i].full, cmt[i].ID);
			strcat(cmt[i].full, " (");
			strcat(cmt[i].full, cmt[i].name);
			strcat(cmt[i].full, ")");
		}

		else {
			strcpy(cmt[i].full, cmt[i].ID);
			strcat(cmt[i].full, "/");
			strcat(cmt[i].full, cmt[i].name);
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line+=2;

		if(do_exclude(i)){N--; i--;}
	}

	return N;
}
int import_pc_tcs (int N, FILE *fin)
{

	int j, k;
	int m, line=1, len;
	float G;
	char tempID[20];

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "%s %f %f %f %f %f %d %d %d.%d %f %f %60[^\n]%*c",
			cmt[i].ID, &cmt[i].q, &cmt[i].e, &cmt[i].i,
			&cmt[i].pn, &cmt[i].an, &cmt[i].y, &cmt[i].m,
			&cmt[i].d, &cmt[i].h, &cmt[i].H, &G, cmt[i].name);

		cmt[i].G = G/2.5;

		if (m < 13){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		for (j=0; j<20; j++) tempID[j]='\0';

		if ((cmt[i].ID[0]=='C' && cmt[i].ID[1]=='/') ||
			(cmt[i].ID[0]=='P' && cmt[i].ID[1]=='/')){

			for(j=6, k=0; cmt[i].ID[j]!='\0'; j++, k++)
				tempID[k]=cmt[i].ID[j];

			len = strlen(cmt[i].ID);
			for(j=6; j<len; j++)
				cmt[i].ID[j]=' ';

			remove_spaces(cmt[i].ID);

			strcat(cmt[i].ID, " ");
			strcat(cmt[i].ID, tempID);
		}

		remove_spaces (cmt[i].name);

		if ((cmt[i].ID[0]=='C' && cmt[i].ID[1]=='/') ||
			(cmt[i].ID[0]=='P' && cmt[i].ID[1]=='/')){

			strcpy(cmt[i].full, cmt[i].ID);
			strcat(cmt[i].full, " (");
			strcat(cmt[i].full, cmt[i].name);
			strcat(cmt[i].full, ")");
		}

		else {
			strcpy(cmt[i].full, cmt[i].ID);
			strcat(cmt[i].full, "/");
			strcat(cmt[i].full, cmt[i].name);
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;

		if(do_exclude(i)){N--; i--;}
	}

	return N;
}
int import_ecu (int N, FILE *fin)
{

	int j, k, l;
	float G;
	int m, line=2;

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "%45[^\n]%*cE C 2000 %d %d %d.%d %f %f %f %f %f %f %f\n",
			cmt[i].full, &cmt[i].y, &cmt[i].m, &cmt[i].d,
			&cmt[i].h, &cmt[i].q, &cmt[i].e, &cmt[i].pn, &cmt[i].an,
			&cmt[i].i, &cmt[i].H, &G);

		cmt[i].G = G/2.5;

		if (m < 12){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		remove_spaces(cmt[i].full);

		for (j=0; cmt[i].full[j]!='\0'; j++){
			if (isdigit(cmt[i].full[j]) &&
				cmt[i].full[j+1]=='P' &&
				cmt[i].full[j+2]=='/'){

				for(k=0; cmt[i].full[k]!='/'; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k]='\0';
				++k;
				for(l=0; cmt[i].full[k]!='\0'; l++, k++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
			}

			if (cmt[i].full[j]=='('){
				for(k=0; cmt[i].full[k]!='('; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k-1]='\0';

				++k;
				for(l=0; cmt[i].full[k]!=')'; k++, l++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
			}
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line+=2;

		if(do_exclude(i)){N--; i--;}
	}

	return N;
}
int import_dance (int N, FILE *fin)
{

	int j, k;
	int m, line=1, len;
	char tempID[20];

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "%11c %f %f %f %f %f %d.%2d%2d%4d %30[^\n]%*c",
			cmt[i].ID, &cmt[i].q, &cmt[i].e, &cmt[i].i,
			&cmt[i].an, &cmt[i].pn, &cmt[i].y, &cmt[i].m,
			&cmt[i].d, &cmt[i].h, cmt[i].name);

		if (m < 11){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		remove_spaces (cmt[i].ID);
		remove_spaces (cmt[i].name);

		for (j=0; j<20; j++) tempID[j]='\0';

		if ((cmt[i].ID[0]=='C' && cmt[i].ID[1]=='/') ||
			(cmt[i].ID[0]=='P' && cmt[i].ID[1]=='/')){

			for(j=6, k=0; cmt[i].ID[j]!='\0'; j++, k++)
				tempID[k]=cmt[i].ID[j];

			len = strlen(cmt[i].ID);
			for(j=6; j<len; j++) cmt[i].ID[j]=' ';

			remove_spaces(cmt[i].ID);

			strcat(cmt[i].ID, " ");
			strcat(cmt[i].ID, tempID);
		}

		if ((cmt[i].ID[0]=='C' && cmt[i].ID[1]=='/') ||
			(cmt[i].ID[0]=='P' && cmt[i].ID[1]=='/')){

			strcpy(cmt[i].full, cmt[i].ID);
			strcat(cmt[i].full, " (");
			strcat(cmt[i].full, cmt[i].name);
			strcat(cmt[i].full, ")");
		}

		else {
			strcpy(cmt[i].full, cmt[i].ID);
			strcat(cmt[i].full, "/");
			strcat(cmt[i].full, cmt[i].name);
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;

		if(do_exclude(i)){N--; i--;}
	}

	return N;
}
int import_megastar (int N, FILE *fin)
{

	int m, line=1;
	float G;
	char x[25+1];

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "%30c %12c %d %d %d.%d %f %f %f %f %f %f %f %25[^\n]%*c",
			cmt[i].name, cmt[i].ID, &cmt[i].y, &cmt[i].m, &cmt[i].d,
			&cmt[i].h, &cmt[i].q, &cmt[i].e, &cmt[i].pn,
			&cmt[i].an, &cmt[i].i, &cmt[i].H, &G, x);

		cmt[i].G = G/2.5;

		if (m < 14){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		remove_spaces (cmt[i].ID);
		remove_spaces (cmt[i].name);

		if ((cmt[i].ID[0]=='C' && cmt[i].ID[1]=='/') ||
			(cmt[i].ID[0]=='P' && cmt[i].ID[1]=='/')){

			strcpy(cmt[i].full, cmt[i].ID);
			strcat(cmt[i].full, " (");
			strcat(cmt[i].full, cmt[i].name);
			strcat(cmt[i].full, ")");
		}

		else {
			strcpy(cmt[i].full, cmt[i].ID);
			strcat(cmt[i].full, "/");
			strcat(cmt[i].full, cmt[i].name);
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;

		if(do_exclude(i)){N--; i--;}
	}

	return N;
}
int import_skychart (int N, FILE *fin)
{

	int j, k, l;
	int m, line=1;
	char c;

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "P11 2000.0 -%f %f %f %f %f 0 %d/%d/%d.%d %f %f 0 0 ",
			&cmt[i].q, &cmt[i].e, &cmt[i].i, &cmt[i].pn,
			&cmt[i].an, &cmt[i].y, &cmt[i].m, &cmt[i].d,
			&cmt[i].h, &cmt[i].H, &cmt[i].G);

		if (m < 11){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		j=0;
		while ((c=fgetc(fin)) != ';' ){
			cmt[i].full[j++]=c;
		}
		cmt[i].full[j]='\0';

		fscanf(fin, "%*[^\n]\n");		//za izostavi ono na kraju

		for (j=0; cmt[i].full[j]!='\0'; j++){
			if (isdigit(cmt[i].full[j]) &&
				cmt[i].full[j+1]=='P' &&
				cmt[i].full[j+2]=='/'){

				for(k=0; cmt[i].full[k]!='/'; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k]='\0';
				++k;
				for(l=0; cmt[i].full[k]!='\0'; l++, k++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
			}

			if (cmt[i].full[j]=='('){
				for(k=0; cmt[i].full[k]!='('; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k-1]='\0';

				++k;
				for(l=0; cmt[i].full[k]!=')'; k++, l++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
			}
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;

		if(do_exclude(i)){N--; i--;}
	}

	return N;
}
int import_voyager (int N, FILE *fin)
{

	int m, line=1;
	char mj[3+1];

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "%27c %f %f %f %f %f %f %4d %3c %d.%d 2000.0\n",
			cmt[i].name, &cmt[i].q, &cmt[i].e, &cmt[i].i,
			&cmt[i].an, &cmt[i].pn, &cmt[i].G, &cmt[i].y,
			mj, &cmt[i].d, &cmt[i].h);

		if (m < 11){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		strcpy(cmt[i].full, cmt[i].name); 		//posto nema pravog full-a, name ce bit kao full

		if (mj[0]=='J' && mj[1]=='a' && mj[2]=='n') cmt[i].m=1;
		if (mj[0]=='F' && mj[1]=='e' && mj[2]=='b') cmt[i].m=2;
		if (mj[0]=='M' && mj[1]=='a' && mj[2]=='r') cmt[i].m=3;
		if (mj[0]=='A' && mj[1]=='p' && mj[2]=='r') cmt[i].m=4;
		if (mj[0]=='M' && mj[1]=='a' && mj[2]=='y') cmt[i].m=5;
		if (mj[0]=='J' && mj[1]=='u' && mj[2]=='n') cmt[i].m=6;
		if (mj[0]=='J' && mj[1]=='u' && mj[2]=='l') cmt[i].m=7;
		if (mj[0]=='A' && mj[1]=='u' && mj[2]=='g') cmt[i].m=8;
		if (mj[0]=='S' && mj[1]=='e' && mj[2]=='p') cmt[i].m=9;
		if (mj[0]=='O' && mj[1]=='c' && mj[2]=='t') cmt[i].m=10;
		if (mj[0]=='N' && mj[1]=='o' && mj[2]=='v') cmt[i].m=11;
		if (mj[0]=='D' && mj[1]=='e' && mj[2]=='c') cmt[i].m=12;

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, cmt[i].d);
		remove_spaces (cmt[i].name);
//		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;

		if(do_exclude(i)){N--; i--;}
	}

	return N;
}
int import_skytools (int N, FILE *fin)
{

	int j, k, l, u, t=0, space;
	int yy, mm, dd;
	int m, line=1, len;
	char x[15+1];

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "C %40c %d %d %d %d %d %d.%d %f %f %f %f %f %f %f 0.002000 %15[^\n]%*c",
			cmt[i].full, &yy, &mm, &dd, &cmt[i].y, &cmt[i].m, &cmt[i].d, &cmt[i].h,
			&cmt[i].q, &cmt[i].e, &cmt[i].pn, &cmt[i].an, &cmt[i].i,
			&cmt[i].H, &cmt[i].G, x);

		cmt[i].h*=10;

		if (m < 16){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		remove_spaces(cmt[i].full);

		for (j=0; cmt[i].full[j]!='\0'; j++){
			if (isdigit(cmt[i].full[j]) && cmt[i].full[j+1]=='P' && cmt[i].full[j+2]=='/'){

				for(k=0; cmt[i].full[k]!='/'; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k]='\0';
				++k;
				for(l=0; cmt[i].full[k]!='\0'; l++, k++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
			}

		if ((cmt[i].full[0]=='C' && cmt[i].full[1]=='/') ||
			(cmt[i].full[0]=='P' && cmt[i].full[1]=='/')){
				space=0;
				len = strlen(cmt[i].full);
				for(u=0; u<len; u++){
					if (cmt[i].full[u]==' ' && space==1) {
						t=u;
						break;
					}
					else if(cmt[i].full[u]==' ') space++;
				}

				for(k=0; k<t; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k]='\0';

				++k;
				for(l=0; cmt[i].full[k]!='\0'; k++, l++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
			}
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;

		if(do_exclude(i)){N--; i--;}
	}

	return N;
}

int import_cfw (int N, FILE *fin)
{

	int j, k, l;
	float G;

	//for (int i=0; i<7; i++) fscanf(fin, "%*[^\n]\n");

	for (int i=0; i<N; i++){

		fscanf(fin, "name=%40[^\n]%*c\
					%*[^\n]\n\
					type=orbit\n\
					T=%d %d %d.%d\n\
					q=%f\n\
					e=%f\n\
					peri=%f\n\
					node=%f\n\
					i=%f\n\
					prec=2000.0\n\
					%*[^\n]\n\
					mageq=%f %f\
					\n",
					cmt[i].full,
					&cmt[i].y, &cmt[i].m, &cmt[i].d, &cmt[i].h,
					&cmt[i].q,
					&cmt[i].e,
					&cmt[i].pn,
					&cmt[i].an,
					&cmt[i].i,
					&cmt[i].H, &G);

		cmt[i].G = G/2.5;

		remove_spaces(cmt[i].full);

		for (j=0; cmt[i].full[j]!='\0'; j++){

			if (isdigit(cmt[i].full[j]) && cmt[i].full[j+1]=='P' && cmt[i].full[j+2]=='/'){

				for(k=0; cmt[i].full[k]!='/'; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k]='\0';

				++k;

				for(l=0; cmt[i].full[k]!='\0'; l++, k++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
			}

			if (cmt[i].full[j]=='('){
				for(k=0; cmt[i].full[k]!='('; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k-1]='\0';

				++k;

				for(l=0; cmt[i].full[k]!=')'; k++, l++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
			}
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);

		if(do_exclude(i)){N--; i--;}
	}

	return N;
}

void export_mpc (int N, FILE *fout)
{

	for (int i=0; i<N; i++) {

		fprintf(fout,"              %4d %02d %02d.%04d %9f  %.6f  %8.4f  %8.4f  %8.4f  %4d%02d%02d  %4.1f %4.1f  %-56s MPC 00000\n",
				cmt[i].y, cmt[i].m, cmt[i].d, cmt[i].h, cmt[i].q, cmt[i].e,
				cmt[i].pn, cmt[i].an, cmt[i].i, ep_y, ep_m, ep_d, cmt[i].H, cmt[i].G, cmt[i].full);
	}
}
void export_skymap (int N, FILE *fout)
{

	int j, k, len;

	for (int i=0; i<N; i++) {

		k=0;
		len = strlen(cmt[i].ID);
		for(j=0; j<len; j++){
			fputc(cmt[i].ID[j], fout);
			k++;
		}
		fputc(' ', fout); k++;
		len = strlen(cmt[i].name);
		for(j=0; j<len; j++){
			fputc(cmt[i].name[j], fout);
			k++;
		}
		while(k!=47){
			fputc(' ', fout); k++;
		}

		fprintf(fout,"%4d %02d %02d.%04d %9f       %.6f %8.4f %8.4f %8.4f  %4.1f  %4.1f\n",
				cmt[i].y, cmt[i].m, cmt[i].d, cmt[i].h, cmt[i].q,
				cmt[i].e, cmt[i].pn, cmt[i].an, cmt[i].i, cmt[i].H, cmt[i].G);
	}
}
void export_guide (int N, FILE *fout)
{

	int j, k, len;

	for (int i=0; i<N; i++) {

		k=0;
		len = strlen(cmt[i].ID);
		if (cmt[i].ID[len-1]=='P' && isdigit(cmt[i].ID[len-2])){
			fputc('P', fout); k++;
			fputc('/', fout); k++;
		}

		if (cmt[i].ID[len-1]=='D' && isdigit(cmt[i].ID[len-2])){
			fputc('D', fout); k++;
			fputc('/', fout); k++;
		}

		len = strlen(cmt[i].name);
		for(j=0; j<len; j++){
			fputc(cmt[i].name[j], fout);
			k++;
		}
		fputc(' ', fout); k++;
		fputc('(', fout); k++;

		len = strlen(cmt[i].ID);
		for(j=0; j<len; j++){
			fputc(cmt[i].ID[j], fout);
			k++;
		}
		fputc(')', fout); k++;
		k++;

		while(k!=44){
			fputc(' ', fout); k++;
		}

		fprintf(fout,"%2d.%04d  %2d  %4d  0.0        %9.6f    %.6f  %8.4f    %8.4f    %8.4f    %d.0   %4.1f %4.1f    MPC 00000\n",
				cmt[i].d, cmt[i].h, cmt[i].m, cmt[i].y, cmt[i].q, cmt[i].e,
				cmt[i].i, cmt[i].pn, cmt[i].an, equinox, cmt[i].H, cmt[i].G);
	}
}
void export_xephem (int N, FILE *fout)
{

	//info: http://www.clearskyinstitute.com/xephem/help/xephem.html#mozTocId215848

	for (int i=0; i<N; i++) {

		fprintf(fout,"# From MPC 00000\n%s,", cmt[i].full);

		if(cmt[i].e < 1){


			double smAxis = cmt[i].q/(1-cmt[i].e);
			double mdMotion = 0.9856076686/cmt[i].P;
			double mAnomaly = -(mdMotion * (cmt[i].T - greg_to_jul(ep_y, ep_m, ep_d)));

			if (mAnomaly <   0) mAnomaly+=360;
			if (mAnomaly > 360) mAnomaly-=360;

			fprintf(fout, "e,%.4f,%.4f,%.4f,%.6f,%.7f,%.8f,%.4f,%02d/%02d.0/%d,%d,g %4.1f,%.1f\n",
				cmt[i].i, cmt[i].an, cmt[i].pn, smAxis, mdMotion, cmt[i].e,
				mAnomaly, ep_m, ep_d, ep_y, equinox, cmt[i].H, cmt[i].G);
		}

		if(cmt[i].e == 1.0){

			fprintf(fout, "p,%02d/%02d.%03d/%4d,%.3f,%.3f,%.5f,%.3f,2000,%.1f,%.1f\n",
				cmt[i].m, cmt[i].d, cmt[i].h, cmt[i].y,
				cmt[i].i, cmt[i].pn, cmt[i].q, cmt[i].an,
				cmt[i].H, cmt[i].G);
		}

		if(cmt[i].e > 1.0){

			fprintf(fout, "h,%02d/%02d.%04d/%4d,%.4f,%.4f,%.4f,%.6f,%.6f,2000,%.1f,%.1f\n",
				cmt[i].m, cmt[i].d, cmt[i].h, cmt[i].y,
				cmt[i].i, cmt[i].an, cmt[i].pn, cmt[i].e,
				cmt[i].q, cmt[i].H, cmt[i].G);
		}
	}
}
void export_home_planet (int N, FILE *fout)
{

	for (int i=0; i<N; i++) {

		double smAxis = cmt[i].q/(1-cmt[i].e);

		fprintf(fout,"%s,%d-%d-%d.%04d,%.6f,%.6f,%.4f,%.4f,%.4f,%.5f,%.5f years, MPC      \n",
				cmt[i].full, cmt[i].y, cmt[i].m, cmt[i].d, cmt[i].h, cmt[i].q,
				cmt[i].e, cmt[i].pn, cmt[i].an, cmt[i].i, smAxis, cmt[i].P);
	}
}
void export_mystars (int N, FILE *fout)
{

	for (int i=0; i<N; i++) {


		fprintf(fout,"%s;\t%ld.%04d\t%.4f\t%.6f\t%.6f\t%.4f\t%.4f\t%.1f\t%.1f\tMPC00000\t%ld.0\n",
				cmt[i].full, cmt[i].T-2400000, cmt[i].h, cmt[i].pn, cmt[i].e, cmt[i].q,
				cmt[i].i, cmt[i].an, cmt[i].H, cmt[i].G, eq_JD-2400000);
	}
}
void export_thesky (int N, FILE *fout)
{

	for (int i=0; i<N; i++) {

		fprintf(fout,"%-39s|%d|%4d%02d%02d.%04d |%9f |%.6f |%8.4f |%8.4f |%8.4f |%4.1f |%4.1f | MPC 00000\n",
				cmt[i].full, equinox, cmt[i].y, cmt[i].m, cmt[i].d, cmt[i].h, cmt[i].q,
				cmt[i].e, cmt[i].pn, cmt[i].an, cmt[i].i, cmt[i].H, cmt[i].G*2.5);
	}
}
void export_starry_night (int N, FILE *fout)
{

	for (int i=0; i<N; i++) {

		fprintf(fout,"     %-29s %4.1f    0.0   %.6f   %9.6f    %8.4f  %8.4f  %8.4f  %ld.%04d    %ld.5  %4.1f  %-13s MPC 00000\n",
				cmt[i].name, cmt[i].H, cmt[i].e, cmt[i].q, cmt[i].an, cmt[i].pn,
				cmt[i].i, cmt[i].T, cmt[i].h, eq_JD, cmt[i].G*2.5, cmt[i].ID);
	}
}
void export_deep_space (int N, FILE *fout)
{

	for (int i=0; i<N; i++) {

		fprintf(fout,"%s (%s)\nC J%d %4d %02d %02d.%04d %.6f %.6f %.4f %.4f %.4f %.1f %.1f\n",
				cmt[i].name, cmt[i].ID, equinox, cmt[i].y, cmt[i].m, cmt[i].d, cmt[i].h,
				cmt[i].q, cmt[i].e, cmt[i].pn, cmt[i].an, cmt[i].i, cmt[i].H, cmt[i].G*2.5);
	}
}
void export_pc_tcs (int N, FILE *fout)
{

	int j, k, len;

	for (int i=0; i<N; i++) {

		len = strlen(cmt[i].ID);
		for (j=0; j<len; j++){
			if (cmt[i].ID[j]==' '){
				k=j;
				for( ; cmt[i].ID[k]!='\0'; k++)
					cmt[i].ID[k]=cmt[i].ID[k+1];
			}
		}

		fprintf(fout,"%s %.6f %.6f %.4f %.4f %.4f %4d %02d %02d.%04d %.1f %.1f %s\n",
				cmt[i].ID, cmt[i].q, cmt[i].e, cmt[i].i, cmt[i].pn, cmt[i].an,
				cmt[i].y, cmt[i].m, cmt[i].d, cmt[i].h, cmt[i].H, cmt[i].G*2.5, cmt[i].name);
	}
}
void export_ecu (int N, FILE *fout)
{

	for (int i=0; i<N; i++) {

		fprintf(fout,"%s\nE C %d %4d %02d %02d.%04d %.6f %.6f %.4f %.4f %.4f %.1f %.1f\n",
				cmt[i].full, equinox, cmt[i].y, cmt[i].m, cmt[i].d, cmt[i].h, cmt[i].q,
				cmt[i].e, cmt[i].pn, cmt[i].an, cmt[i].i, cmt[i].H, cmt[i].G*2.5);
	}
}
void export_dance (int N, FILE *fout)
{

	for (int i=0; i<N; i++) {

		fprintf(fout,"%s\nE C %d %4d %02d %02d.%04d %.6f %.6f %.4f %.4f %.4f %.1f %.1f\n",
				cmt[i].full, equinox, cmt[i].y, cmt[i].m, cmt[i].d, cmt[i].h,
				cmt[i].q, cmt[i].e, cmt[i].pn, cmt[i].an, cmt[i].i, cmt[i].H, cmt[i].G);
	}
}
void export_megastar (int N, FILE *fout)
{

	for (int i=0; i<N; i++) {

		fprintf(fout,"%-30s%-12s%4d %02d  %02d.%04d   %9.6f   %.6f    %8.4f    %8.4f    %8.4f   %4.1f   %4.1f    %d MPC 00000\n",
				cmt[i].name, cmt[i].ID, cmt[i].y, cmt[i].m, cmt[i].d,
				cmt[i].h, cmt[i].q, cmt[i].e, cmt[i].pn, cmt[i].an,
				cmt[i].i, cmt[i].H, cmt[i].G*2.5, equinox);
	}
}
void export_skychart (int N, FILE *fout)
{

	for (int i=0; i<N; i++) {

		fprintf(fout,"P11	%d.0	-%.6f\t%.6f\t%.3f\t%.4f\t%.4f\t0\t%4d/%02d/%02d.%04d\t%.1f %.1f\t0\t0\t%s; MPC 00000\t\n",
				equinox, cmt[i].q, cmt[i].e, cmt[i].i, cmt[i].pn, cmt[i].an, cmt[i].y,
				cmt[i].m, cmt[i].d, cmt[i].h, cmt[i].H, cmt[i].G, cmt[i].full);
	}
}
void export_voyager (int N, FILE *fout)
{

	char *mon;

	for (int i=0; i<N; i++) {

		if (cmt[i].m== 1) mon = "Jan";
		if (cmt[i].m== 2) mon = "Feb";
		if (cmt[i].m== 3) mon = "Mar";
		if (cmt[i].m== 4) mon = "Apr";
		if (cmt[i].m== 5) mon = "May";
		if (cmt[i].m== 6) mon = "Jun";
		if (cmt[i].m== 7) mon = "Jul";
		if (cmt[i].m== 8) mon = "Aug";
		if (cmt[i].m== 9) mon = "Sep";
		if (cmt[i].m==10) mon = "Oct";
		if (cmt[i].m==11) mon = "Nov";
		if (cmt[i].m==12) mon = "Dec";

		fprintf(fout,"%-26s %9.6f   %.6f  %8.4f   %8.4f   %8.4f   0.0  %4d%s",
				cmt[i].name, cmt[i].q, cmt[i].e, cmt[i].i, cmt[i].an,
				cmt[i].pn, cmt[i].y, mon);

		if (cmt[i].d<10) fprintf(fout, "%d.%04d  %d.0\n", cmt[i].d, cmt[i].h, equinox);
		else fprintf(fout, "%d.%04d %d.0\n", cmt[i].d, cmt[i].h, equinox);
	}
}
void export_skytools (int N, FILE *fout)
{

	int j, k, len;

	for (int i=0; i<N; i++) {

		if(cmt[i].h>999) cmt[i].h/=10;

		k=0;
		fputc('C', fout); k++;
		fputc(' ', fout); k++;
		len = strlen(cmt[i].ID);
		for(j=0; j<len; j++){
			fputc(cmt[i].ID[j], fout);
			k++;
		}

		len = strlen(cmt[i].ID);
		if ((cmt[i].ID[len-1]=='P' && isdigit(cmt[i].ID[len-2])) ||
			(cmt[i].ID[len-1]=='D' && isdigit(cmt[i].ID[len-2]))){
			fputc('/', fout); k++;
		}
		else {
			fputc(' ', fout);
			k++;
		}
		len = strlen(cmt[i].name);
		for(j=0; j<len; j++){
			fputc(cmt[i].name[j], fout);
			k++;
		}
		while(k<43){
			fputc(' ', fout); k++;
		}

		fprintf(fout,"2011 02 08 %4d %02d %02d.%-.03d  %9.6f   %.6f %7.3f %7.3f %7.3f  %4.1f  %4.1f 0.00%d MPC 00000\n",
				cmt[i].y, cmt[i].m, cmt[i].d, cmt[i].h, cmt[i].q, cmt[i].e,
				cmt[i].pn, cmt[i].an, cmt[i].i, cmt[i].H, cmt[i].G, equinox);
	}
}
void export_ssc (int N, FILE *fout)
{

	char *mon;

	for (int i=0; i<N; i++) {

		if (cmt[i].e == 1) cmt[i].e = 1.000001;

		int len = strlen(cmt[i].full);
		for (int j=0; j<len; j++) if (cmt[i].full[j]=='/') cmt[i].full[j]=' ';

		if (cmt[i].m== 1) mon = "Jan";
		if (cmt[i].m== 2) mon = "Feb";
		if (cmt[i].m== 3) mon = "Mar";
		if (cmt[i].m== 4) mon = "Apr";
		if (cmt[i].m== 5) mon = "May";
		if (cmt[i].m== 6) mon = "Jun";
		if (cmt[i].m== 7) mon = "Jul";
		if (cmt[i].m== 8) mon = "Aug";
		if (cmt[i].m== 9) mon = "Sep";
		if (cmt[i].m==10) mon = "Oct";
		if (cmt[i].m==11) mon = "Nov";
		if (cmt[i].m==12) mon = "Dec";

		fprintf(fout,"\"%s\" \"Sol\"\n", cmt[i].full);
		fprintf(fout,"{\n");
		fprintf(fout,"Class \"comet\" \n");
		fprintf(fout,"Mesh \"asteroid.cms\" \n");
		fprintf(fout,"Texture \"asteroid.jpg\" \n");
		fprintf(fout,"Radius 5 \n");
		fprintf(fout,"Albedo 0.1 \n");
		fprintf(fout,"EllipticalOrbit \n");
		fprintf(fout,"\t{ \n");
		fprintf(fout,"\tPeriod \t\t\t %f \n", cmt[i].P);
		fprintf(fout,"\tPericenterDistance \t %f \n", cmt[i].q);
		fprintf(fout,"\tEccentricity \t\t %f \n", cmt[i].e);
		fprintf(fout,"\tInclination \t\t %.4f \n", cmt[i].i);
		fprintf(fout,"\tAscendingNode \t\t %.4f \n", cmt[i].an);
		fprintf(fout,"\tArgOfPericenter \t %.4f \n", cmt[i].pn);
		fprintf(fout,"\tMeanAnomaly \t\t 0  \n");
		fprintf(fout,"\tEpoch \t\t\t %ld.%.4d\t# %d %s %.2d.%.4d \n",
				cmt[i].T, cmt[i].h, cmt[i].y, mon, cmt[i].d, cmt[i].h);
		fprintf(fout,"\t} \n");
		fprintf(fout,"}\n\n\n");
	}
}
void export_stell (int N, FILE *fout)
{

	for (int i=0; i<N; i++) {

		int len = strlen(cmt[i].name);
		for (int j=0; j<len; j++) if (isupper(cmt[i].name[j])) cmt[i].name[j] = tolower(cmt[i].name[j]);

		fprintf(fout,"[%s]\n", cmt[i].name);
		fprintf(fout,"parent = Sun\n");
		fprintf(fout,"orbit_Inclination = %f\n", cmt[i].i);
		fprintf(fout,"coord_func = comet_orbit\n");
		fprintf(fout,"orbit_Eccentricity = %f\n", cmt[i].e);
		fprintf(fout,"orbit_ArgOfPericenter = %f\n", cmt[i].pn);
		fprintf(fout,"absolute_magnitude=%.1f\n", cmt[i].H);
		fprintf(fout,"name = %s\n", cmt[i].full);
		fprintf(fout,"slope_parameter = %.1f\n", cmt[i].G);
		fprintf(fout,"lighting = false\n");
		fprintf(fout,"tex_map = nomap.png\n");
		fprintf(fout,"color = 1.0, 1.0, 1.0\n");
		fprintf(fout,"orbit_AscendingNode = %f\n", cmt[i].an);
		fprintf(fout,"albedo = 1\n");
		fprintf(fout,"radius = 5\n");
		fprintf(fout,"orbit_PericenterDistance = %f\n", cmt[i].q);
		fprintf(fout,"type = comet\n");
		fprintf(fout,"orbit_TimeAtPericenter = %ld.%.4d\n\n", cmt[i].T, cmt[i].h);
	}
}

double compute_period (double q, double e)
{

	double P=0;

	if (e <  1) P = pow((q/(1-e)),1.5);
	if (e >  1) P = pow((q/(e-1)),1.5);
	if (e == 1) P = pow((q/(1-0.999999)),1.5);

	return P;
}
double get_sort_key(char *ID)
{

	int k;
	double sort, v=0.0;
	char temp[4+1], tempp[2+1], temppp[3+1];

	if(isdigit(ID[0])){
		k=0;
		do{
			temp[k]=ID[k];
		}while(isdigit(ID[k++]));

		sort = atof(temp);
	}

	else {
		temp[0]=ID[2];
		temp[1]=ID[3];
		temp[2]=ID[4];
		temp[3]=ID[5];

		sort = atof(temp);

		k=7;

		if (isalpha(ID[k]) && isalpha(ID[k+1]) && ID[k+2]=='\0'){

				v = (ID[k]-64)/(double)100;
				v += (ID[k]-64)/(double)10000;
		}

		if (isalpha(ID[k]) && isdigit(ID[k+1]) && ID[k+2]=='\0'){

			v = (ID[k]-64)/(double)100;
			v += (double)(ID[k+1]-48)/100000;
		}

		if (isalpha(ID[k]) && isalpha(ID[k+1]) && isdigit(ID[k+2]) && ID[k+3]=='\0'){

			v = (ID[k]-64)/(double)100;
			v += (ID[k+1]-64)/(double)10000;
			v += (ID[k+2]-48)/(double)100000;
		}

		if (isalpha(ID[k]) && isalpha(ID[k+1]) && isdigit(ID[k+2]) && isdigit(ID[k+3])  && ID[k+4]=='\0'){

			v = (ID[k]-64)/(double)100;
			v += (ID[k+1]-64)/(double)10000;

			tempp[0]='\0';
			tempp[1]='\0';

			tempp[0]=ID[k+2];
			tempp[1]=ID[k+3];

			v += atof(tempp)/(double)100000;
		}

		if (isalpha(ID[k]) && isalpha(ID[k+1]) && isdigit(ID[k+2]) && isdigit(ID[k+3]) && isdigit(ID[k+4])){

			v = (ID[k]-64)/(double)100;
			v += (ID[k+1]-64)/(double)10000;

			temppp[0]='\0';
			temppp[1]='\0';
			temppp[2]='\0';
//			temppp[3]='\0';

			temppp[0]=ID[k+2];
			temppp[1]=ID[k+3];
			temppp[2]=ID[k+4];

			v += atof(tempp)/(double)10000000;
		}

		sort+=v;
	}

	return sort;
}
long int greg_to_jul (int y, int m, int d)
{

	return 367*y - (7*(y + (m + 9)/12))/4 - ((3*(y + (m - 9)/7))/100 + 1)/4 + (275*m)/9 + d + 1721029;
}
void jul_to_greg (long int T, int &y, int &m, int &d)
{

//	izracuvanje gregorijanskog datuma iz julijanskog dana
//	izvor: http://en.wikipedia.org/wiki/Julian_day#Gregorian_calendar_from_Julian_day_number

	int v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13;

	v1 = T + 0.5;
	v2 = v1 + 32044;
	v3 = v2 / 146097;
	v4 = v2 % 146097;
	v5 = (v4 / 36524 + 1) * 3 / 4;
	v6 = v4 - v5 * 36524;
	v7 = v6 / 1461;
	v8 = v6 % 1461;
	v9 = (v8 / 365 + 1) * 3 / 4;
	v10 = v8 - v9 * 365;
	v12 = v3 * 400 + v5 * 100 + v7 * 4 + v9;
	v11 = (v10 * 5 + 308) / 153 - 2;
	v13 = v10 - (v11 + 4) * 153 / 5 + 122;
	y = v12 - 4800 + (v11 + 2) / 12;
	m = (v11 + 2) % 12 + 1;
	d = v13 + 1;
}
void remove_spaces (char *name)
{

	int len = strlen(name);

	for (int i=0; i<len-1; i++) if (name[i]==' ' && name[i+1]==' ') name[i]='\0';
}

bool sort_data (int N)
{
	for (int i=0; i<N-1; i++){
		for (int j=i+1; j<N; j++){
			if (sort_combo1->ItemIndex == 1 && sort_combo2->ItemIndex == 0 && cmt[i].sort > cmt[j].sort) do_swap(i, j);
			if (sort_combo1->ItemIndex == 1 && sort_combo2->ItemIndex == 1 && cmt[i].sort < cmt[j].sort) do_swap(i, j);
			if (sort_combo1->ItemIndex == 2 && sort_combo2->ItemIndex == 0 && cmt[i].T > cmt[j].T) do_swap(i, j);
			if (sort_combo1->ItemIndex == 2 && sort_combo2->ItemIndex == 1 && cmt[i].T < cmt[j].T) do_swap(i, j);
			if (sort_combo1->ItemIndex == 3 && sort_combo2->ItemIndex == 0 && cmt[i].q > cmt[j].q) do_swap(i, j);
			if (sort_combo1->ItemIndex == 3 && sort_combo2->ItemIndex == 1 && cmt[i].q < cmt[j].q) do_swap(i, j);
			if (sort_combo1->ItemIndex == 4 && sort_combo2->ItemIndex == 0 && cmt[i].e > cmt[j].e) do_swap(i, j);
			if (sort_combo1->ItemIndex == 4 && sort_combo2->ItemIndex == 1 && cmt[i].e < cmt[j].e) do_swap(i, j);
			if (sort_combo1->ItemIndex == 5 && sort_combo2->ItemIndex == 0 && cmt[i].an > cmt[j].an) do_swap(i, j);
			if (sort_combo1->ItemIndex == 5 && sort_combo2->ItemIndex == 1 && cmt[i].an < cmt[j].an) do_swap(i, j);
			if (sort_combo1->ItemIndex == 6 && sort_combo2->ItemIndex == 0 && cmt[i].pn > cmt[j].pn) do_swap(i, j);
			if (sort_combo1->ItemIndex == 6 && sort_combo2->ItemIndex == 1 && cmt[i].pn < cmt[j].pn) do_swap(i, j);
			if (sort_combo1->ItemIndex == 7 && sort_combo2->ItemIndex == 0 && cmt[i].i > cmt[j].i) do_swap(i, j);
			if (sort_combo1->ItemIndex == 7 && sort_combo2->ItemIndex == 1 && cmt[i].i < cmt[j].i) do_swap(i, j);
			if (sort_combo1->ItemIndex == 8 && sort_combo2->ItemIndex == 0 && cmt[i].P > cmt[j].P) do_swap(i, j);
			if (sort_combo1->ItemIndex == 8 && sort_combo2->ItemIndex == 1 && cmt[i].P < cmt[j].P) do_swap(i, j);
		}
		pBar1->Position = i;
	}
	return true;
}

void do_swap(int i, int j)
{

	Comet temp;
	int k;

	for(k=0;k<81;k++) {
		temp.full[k]=cmt[i].full[k];
		cmt[i].full[k]=cmt[j].full[k];
		cmt[j].full[k]=temp.full[k];
	}

	for(k=0;k<56;k++) {
		temp.name[k]=cmt[i].name[k];
		cmt[i].name[k]=cmt[j].name[k];
		cmt[j].name[k]=temp.name[k];
	}

	for(k=0;k<26;k++) {
		temp.ID[k]=cmt[i].ID[k];
		cmt[i].ID[k]=cmt[j].ID[k];
		cmt[j].ID[k]=temp.ID[k];
	}

	temp.T = cmt[i].T;
	cmt[i].T = cmt[j].T;
	cmt[j].T = temp.T;

	temp.y = cmt[i].y;
	cmt[i].y = cmt[j].y;
	cmt[j].y = temp.y;

	temp.m = cmt[i].m;
	cmt[i].m = cmt[j].m;
	cmt[j].m = temp.m;

	temp.d = cmt[i].d;
	cmt[i].d = cmt[j].d;
	cmt[j].d = temp.d;

	temp.h = cmt[i].h;
	cmt[i].h = cmt[j].h;
	cmt[j].h = temp.h;

	temp.P = cmt[i].P;
	cmt[i].P = cmt[j].P;
	cmt[j].P = temp.P;

	temp.q = cmt[i].q;
	cmt[i].q = cmt[j].q;
	cmt[j].q = temp.q;

	temp.e = cmt[i].e;
	cmt[i].e = cmt[j].e;
	cmt[j].e = temp.e;

	temp.i = cmt[i].i;
	cmt[i].i = cmt[j].i;
	cmt[j].i = temp.i;

	temp.an = cmt[i].an;
	cmt[i].an = cmt[j].an;
	cmt[j].an = temp.an;

	temp.pn = cmt[i].pn;
	cmt[i].pn = cmt[j].pn;
	cmt[j].pn = temp.pn;

	temp.H = cmt[i].H;
	cmt[i].H = cmt[j].H;
	cmt[j].H = temp.H;

	temp.G = cmt[i].G;
	cmt[i].G = cmt[j].G;
	cmt[j].G = temp.G;

	temp.sort=cmt[i].sort;
	cmt[i].sort=cmt[j].sort;
	cmt[j].sort=temp.sort;

	for(k=0;k<21;k++) {
		temp.book[k]=cmt[i].book[k];
		cmt[i].book[k]=cmt[j].book[k];
		cmt[j].book[k]=temp.book[k];
	}
}

bool define_exclude()
{
	for (int i=0; i<14; i++) excl.key[i]=false;

	if(ch1->Checked){

		char dd[3], mm[3], yy[5];

		AnsiString str =  t1->Text;
		const char *date = str.c_str();

		dd[0]=date[0];
		dd[1]=date[1];
		mm[0]=date[3];
		mm[1]=date[4];
		yy[0]=date[6];
		yy[1]=date[7];
		yy[2]=date[8];
		yy[3]=date[9];

		int d = atoi(dd);
		int m = atoi(mm);
		int y = atoi(yy);


		if (d<1 || d>31 || m<1 || m>12)
		{
			Application->MessageBox(L"Wrong date",
				L"Error",
				MB_OK | MB_ICONERROR);
			return false;
		}

		excl.T = greg_to_jul(y, m, d);

		if(combo1->ItemIndex==0) excl.key[0]=true;
		if(combo1->ItemIndex==1) excl.key[1]=true;
	}

	if(ch2->Checked){

		AnsiString str =  t2->Text;
		const char *qq = str.c_str();

		float q = atof(qq);

		if (q <= 0)
		{
			Application->MessageBox(L"Value must be greather than zero",
				L"Error",
				MB_OK | MB_ICONERROR);
			return false;
		}

		excl.q = q;

		if(combo2->ItemIndex==0) excl.key[2]=true;
		if(combo2->ItemIndex==1) excl.key[3]=true;
	}

	if(ch3->Checked){

		AnsiString str =  t3->Text;
		const char *ee = str.c_str();

		float e = atof(ee);

		if (e<0 || e>1)
		{
			Application->MessageBox(L"Value must be between 0 and 1",
				L"Error",
				MB_OK | MB_ICONERROR);
			return false;
		}

		excl.e = e;

		if(combo3->ItemIndex==0) excl.key[4]=true;
		if(combo3->ItemIndex==1) excl.key[5]=true;
	}

	if(ch4->Checked){

		AnsiString str =  t4->Text;
		const char *ann = str.c_str();

		float an = atof(ann);

		if (an<0 || an>=360)
		{
			Application->MessageBox(L"Value must be between 0 and 360",
				L"Error",
				MB_OK | MB_ICONERROR);
			return false;
		}

		excl.an = an;

		if(combo4->ItemIndex==0) excl.key[6]=true;
		if(combo4->ItemIndex==1) excl.key[7]=true;
	}

	if(ch5->Checked){

		AnsiString str =  t5->Text;
		const char *pnn = str.c_str();

		float pn = atof(pnn);

		if (pn<0 || pn>=360)
		{
			Application->MessageBox(L"Value must be between 0 and 360",
				L"Error",
				MB_OK | MB_ICONERROR);
			return false;
		}

		excl.pn = pn;

		if(combo5->ItemIndex==0) excl.key[8]=true;
		if(combo5->ItemIndex==1) excl.key[9]=true;
	}

	if(ch6->Checked){

		AnsiString str =  t6->Text;
		const char *ii = str.c_str();

		float i = atof(ii);

		if (i<0 || i>=180)
		{
			Application->MessageBox(L"Value must be between 0 and 180",
				L"Error",
				MB_OK | MB_ICONERROR);
			return false;
		}

		excl.i = i;

		if(combo6->ItemIndex==0) excl.key[10]=true;
		if(combo6->ItemIndex==1) excl.key[11]=true;
	}

	if(ch7->Checked){

		AnsiString str =  t7->Text;
		const char *PP = str.c_str();

		float P = atof(PP);

		if (P <= 0)
		{
			Application->MessageBox(L"Value must be greather than zero",
				L"Error",
				MB_OK | MB_ICONERROR);
			return false;
		}

		excl.P = P;

		if(combo7->ItemIndex==0) excl.key[12]=true;
		if(combo7->ItemIndex==1) excl.key[13]=true;
	}

	return true;
}
bool do_exclude(int i){

	if (excl.key[ 0] && cmt[i].T > excl.T) return true;
	if (excl.key[ 1] && cmt[i].T < excl.T) return true;
	if (excl.key[ 2] && cmt[i].q > excl.q) return true;
	if (excl.key[ 3] && cmt[i].q < excl.q) return true;
	if (excl.key[ 4] && cmt[i].e > excl.e) return true;
	if (excl.key[ 5] && cmt[i].e < excl.e) return true;
	if (excl.key[ 6] && cmt[i].an > excl.an) return true;
	if (excl.key[ 7] && cmt[i].an < excl.an) return true;
	if (excl.key[ 8] && cmt[i].pn > excl.pn) return true;
	if (excl.key[ 9] && cmt[i].pn < excl.pn) return true;
	if (excl.key[10] && cmt[i].i > excl.i) return true;
	if (excl.key[11] && cmt[i].i < excl.i) return true;
	if (excl.key[12] && cmt[i].P > excl.P) return true;
	if (excl.key[13] && cmt[i].P < excl.P) return true;

	return false;
}
};
//---------------------------------------------------------------------------
extern PACKAGE TForm2 *Form2;
//---------------------------------------------------------------------------



#endif
