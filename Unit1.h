//---------------------------------------------------------------------------

#ifndef Unit1H
#define Unit1H
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include "Unit2.h"
#include "Unit3.h"
#include "Unit4.h"
#include "Unit5.h"
#include "Unit6.h"

#include "CometMain.hpp"
#include "Unit6.h"

//---------------------------------------------------------------------------
class TForm1 : public TForm
{
__published:	// IDE-managed Components
	TFrame2 *Frame21;
	TFrame3 *Frame31;
	TFrame4 *Frame41;
	TFrame5 *Frame51;
	TFrame6 *Frame61;
	void __fastcall FormShow(TObject *Sender);
private:	// User declarations
public:		// User declarations
	__fastcall TForm1(TComponent* Owner);
	AnsiString coewFolder;
	struct Excludings excl;
	Comet *cmt;


int import_main (int importType, const char *importFile){

	FILE *fin = Frame21->fin;
	int Ncmt = Frame21->detectedComets;

	cmt = new Comet [Ncmt];

	Frame41->ProgressBar1->Visible = true;
	Frame41->ProgressBar1->Position = 0;
	Frame41->ProgressBar1->Max = Ncmt;

	if (importType== 0) Ncmt = import_mpc (Ncmt, fin);
	if (importType== 1) Ncmt = import_skymap (Ncmt, fin);
	if (importType== 2) Ncmt = import_guide (Ncmt, fin);
	if (importType== 3) Ncmt = import_xephem (Ncmt, fin);
	if (importType== 4) Ncmt = import_home_planet (Ncmt, fin);
	if (importType== 5) Ncmt = import_mystars (Ncmt, fin);
	if (importType== 6) Ncmt = import_thesky (Ncmt, fin);
	if (importType== 7) Ncmt = import_starry_night (Ncmt, fin);
	if (importType== 8) Ncmt = import_deep_space (Ncmt, fin);
	if (importType== 9) Ncmt = import_pc_tcs (Ncmt, fin);
	if (importType==10) Ncmt = import_ecu (Ncmt, fin);
	if (importType==11) Ncmt = import_dance (Ncmt, fin);
	if (importType==12) Ncmt = import_megastar (Ncmt, fin);
	if (importType==13) Ncmt = import_skychart (Ncmt, fin);
	if (importType==14) Ncmt = import_voyager (Ncmt, fin);
	if (importType==15) Ncmt = import_skytools (Ncmt, fin);
	if (importType==16) Ncmt = import_thesky (Ncmt, fin);
	if (importType==17) Ncmt = import_cfw (Ncmt, fin);
	if (importType==18) Ncmt = import_nasa (Ncmt, fin);

	rewind(fin);

	if(Ncmt == 0){
		Application->MessageBox(L"There are no imported comets\n\n"
			L"Two possible reasons:\n\n"
			L" - You selected wrong import format\n"
			L" - Excluding rules are too high",
			L"Error",
			MB_OK | MB_ICONERROR);
		Frame41->ProgressBar1->Visible = false;
		Frame41->Label3->Visible = false;
		Frame41->Label2->Visible = false;
		return 0;
	}

	if(Frame41->ComboBox2->ItemIndex > 0) sort_data (Ncmt);

	Frame41->Label3->Visible = true;

	if(Ncmt == Frame21->detectedComets){
		Frame41->Label2->Caption = "All comets successfully imported.";
	}
	else{
		Frame41->Label2->Caption = IntToStr(Ncmt) + " of " + IntToStr(Frame21->detectedComets) + " comets imported.";
	}

	Frame41->Label2->Visible = true;

	return Ncmt;
}

void export_main (int Ncmt, int exportFormat, const char* exportFile){

	FILE *fout;

	fout=fopen(exportFile, "w");

	if(!fout){
		Application->MessageBox(L"Unable to create input file",
			L"Error",
			MB_OK | MB_ICONERROR);
		return;
	}

	Frame61->ProgressBar1->Visible = true;
	Frame61->ProgressBar1->Position = 0;
	Frame61->ProgressBar1->Max = Ncmt;

	if (exportFormat== 0) export_mpc (Ncmt, fout);
	if (exportFormat== 1) export_skymap (Ncmt, fout);
	if (exportFormat== 2) export_guide (Ncmt, fout);
	if (exportFormat== 3) export_xephem (Ncmt, fout);
	if (exportFormat== 4) export_home_planet (Ncmt, fout);
	if (exportFormat== 5) export_mystars (Ncmt, fout);
	if (exportFormat== 6) export_thesky (Ncmt, fout);
	if (exportFormat== 7) export_starry_night (Ncmt, fout);
	if (exportFormat== 8) export_deep_space (Ncmt, fout);
	if (exportFormat== 9) export_pc_tcs (Ncmt, fout);
	if (exportFormat==10) export_ecu (Ncmt, fout);
	if (exportFormat==11) export_dance (Ncmt, fout);
	if (exportFormat==12) export_megastar (Ncmt, fout);
	if (exportFormat==13) export_skychart (Ncmt, fout);
	if (exportFormat==14) export_voyager (Ncmt, fout);
	if (exportFormat==15) export_skytools (Ncmt, fout);
	if (exportFormat==16) export_thesky (Ncmt, fout);
	if (exportFormat==17) export_ssc (Ncmt, fout);
	if (exportFormat==18) export_stell (Ncmt, fout);

	fclose(fout);

	Frame61->Label4->Visible = true;

	//free(cmt);
	delete [] cmt;
}


int import_mpc (int N, FILE *fin){

	int j, k, l;
	int m, line=1;
	char x[30+1];

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "%14c %d %02d %f %f %f %f %f %f%12c%f %f %55c %30[^\n]%*c",		// %f%12c%f mora bit tako zajedno
			x, &cmt[i].y, &cmt[i].m, &cmt[i].d,
			&cmt[i].q, &cmt[i].e, &cmt[i].pn, &cmt[i].an,
			&cmt[i].i, x, &cmt[i].H, &cmt[i].G, cmt[i].full, x);

		if (m < 14){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame41->ProgressBar1->Max--;
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
				break;
			}

			if (cmt[i].full[j]=='('){
				for(k=0; cmt[i].full[k]!='('; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k-1]='\0';

				++k;
				for(l=0; cmt[i].full[k]!=')'; k++, l++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
				break;
			}
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, (int)cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;
		Frame41->ProgressBar1->Position = i+1;

		if(do_exclude(i)){
			N--;
			i--;
			Frame41->ProgressBar1->Max--;
		}
	}

	return N;
}

int import_skymap (int N, FILE *fin){

	int j, k, l, u, t=0, space;
	int m, line=1, len;

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "%47c %4d %2d %f %f %f %f %f %f %f %f\n",
			cmt[i].full, &cmt[i].y, &cmt[i].m, &cmt[i].d,
			&cmt[i].q, &cmt[i].e, &cmt[i].pn,
			&cmt[i].an, &cmt[i].i, &cmt[i].H, &cmt[i].G);

		if (m < 11){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame41->ProgressBar1->Max--;
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
				break;
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
				break;
			}
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, (int)cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;
		Frame41->ProgressBar1->Position = i+1;

		if(do_exclude(i)){
			N--;
			i--;
			Frame41->ProgressBar1->Max--;
		}
	}

	return N;
}

int import_guide (int N, FILE *fin){

	int j, k, m, line=1, len;
	char c, x[21], full[42+1];

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "%42c %f %d %d 0.0 %f %f %f %f %f 2000.0 %f %f %20[^\n]%*c",
			full, &cmt[i].d, &cmt[i].m, &cmt[i].y,
			&cmt[i].q,  &cmt[i].e, &cmt[i].i, &cmt[i].pn,
			&cmt[i].an, &cmt[i].H, &cmt[i].G, x);

		if (m < 12){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame41->ProgressBar1->Max--;
			continue;
		}

		j=0;
		while ((full[j]) != '(' ){
			cmt[i].name[j]=full[j];
			j++;
		}
		cmt[i].name[j-1]='\0';

		j++; k=0;
		while ((full[j]) != ')' ){
			cmt[i].ID[k]=full[j];
			j++; k++;
		}
		cmt[i].ID[k]='\0';


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



		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, (int)cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;
		Frame41->ProgressBar1->Position = i+1;

		if(do_exclude(i)){
			N--;
			i--;
			Frame41->ProgressBar1->Max--;
		}
	}

	return N;
}

int import_xephem (int N, FILE *fin){

	//info: http://www.clearskyinstitute.com/xephem/help/xephem.html#mozTocId215848

	int j, k, l;
	int mm, dd, hh, yy;
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
        cmt[i].full[j]='\0';

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
				break;
			}

			if (cmt[i].full[j]=='('){
				for(k=0; cmt[i].full[k]!='('; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k-1]='\0';

				++k;
				for(l=0; cmt[i].full[k]!=')'; k++, l++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
				break;
			}
		}

		c=fgetc(fin);

		if(c == 'e'){
			m = fscanf(fin, ",%f,%f,%f,%f,%f,%f,%f,%d/%d.%d/%d,2000,g %f,%f\n",
				&cmt[i].i, &cmt[i].an, &cmt[i].pn, &smAxis,
				&mdMotion, &cmt[i].e, &mAnomaly, &mm, &dd,
				&hh, &yy, &cmt[i].H, &cmt[i].G);

			if (m < 13){
				//cout << "\n\n  Unable to read data in line " << line;
				fscanf(fin, "%*[^\n]\n");
				N--; i--; line++;
				Frame41->ProgressBar1->Max--;
				continue;
			}

			cmt[i].q = smAxis*(1-cmt[i].e);
			T = greg_to_jul (yy, mm, dd);
			long double TT = (long double)T - mAnomaly/mdMotion;
			cmt[i].T = (long int)TT;

			jul_to_greg(cmt[i].T, cmt[i].y, cmt[i].m, cmt[i].d);
			cmt[i].d += (float)hh/10000;
			cmt[i].d += TT-(long int)TT;

			line+=2;
			Frame41->ProgressBar1->Position = i+1;

			if(do_exclude(i)){
				N--;
				i--;
				Frame41->ProgressBar1->Max--;
			}
		}

		if(c == 'p'){
			m = fscanf(fin, ",%d/%f/%d,%f,%f,%f,%f,2000,%f,%f\n",
				&cmt[i].m, &cmt[i].d, &cmt[i].y,
				&cmt[i].i, &cmt[i].pn, &cmt[i].q, &cmt[i].an,
				&cmt[i].H, &cmt[i].G);

			if (m < 9){
				//cout << "\n\n  Unable to read data in line " << line;
				fscanf(fin, "%*[^\n]\n");
				N--; i--; line++;
				Frame41->ProgressBar1->Max--;
				continue;
			}

			cmt[i].e = 1.000000;
			cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, (int)cmt[i].d);
			line+=2;
			Frame41->ProgressBar1->Position = i+1;

			if(do_exclude(i)){
				N--;
				i--;
				Frame41->ProgressBar1->Max--;
			}
		}

		if(c == 'h'){
			m = fscanf(fin, ",%d/%f/%d,%f,%f,%f,%f,%f,2000,%f,%f\n",
				&cmt[i].m, &cmt[i].d, &cmt[i].y,
				&cmt[i].i, &cmt[i].an, &cmt[i].pn, &cmt[i].e,
				&cmt[i].q, &cmt[i].H, &cmt[i].G);

			if (m < 10){
				//cout << "\n\n  Unable to read data in line " << line;
				fscanf(fin, "%*[^\n]\n");
				N--; i--; line++;
				Frame41->ProgressBar1->Max--;
				continue;
			}

			cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, (int)cmt[i].d);
			line+=2;
			Frame41->ProgressBar1->Position = i+1;

			if(do_exclude(i)){
				N--;
				i--;
				Frame41->ProgressBar1->Max--;
			}
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].sort = get_sort_key(cmt[i].ID);
	}

	return N;
}

int import_home_planet (int N, FILE *fin){

	int j, k, l;
	int m, line=1;
	char c, x[50+1];

	fscanf(fin, "%*[^\n]\n");

	for (int i=0; i<N; i++) {

		j=0;
		while ((c=fgetc(fin)) != ',' ){
			cmt[i].full[j++]=c;
		}
		cmt[i].full[j]='\0';

		m = fscanf(fin, "%d-%d-%f,%f,%f,%f,%f,%f,%50[^\n]%*c",
			&cmt[i].y, &cmt[i].m, &cmt[i].d,
			&cmt[i].q, &cmt[i].e, &cmt[i].pn, &cmt[i].an,
			&cmt[i].i, x);

		if (m < 9){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame41->ProgressBar1->Max--;
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
				break;
			}

			if (cmt[i].full[j]=='('){
				for(k=0; cmt[i].full[k]!='('; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k-1]='\0';

				++k;
				for(l=0; cmt[i].full[k]!=')'; k++, l++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
				break;
			}
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, (int)cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;
		Frame41->ProgressBar1->Position = i+1;

		if(do_exclude(i)){
			N--;
			i--;
			Frame41->ProgressBar1->Max--;
		}
	}

	return N;
}

int import_mystars (int N, FILE *fin){

	int j, k, l;
	int m, line=1;
	char c, x[30+1];
	float TT;

	fscanf(fin, "%*[^\n]\n");

	for (int i=0; i<N; i++) {

		j=0;
		while ((c=fgetc(fin)) != ';' ){
			cmt[i].full[j++]=c;
		}

		m = fscanf(fin, "%f %f %f %f %f %f %f %f %30[^\n]%*c",
			&TT, &cmt[i].pn, &cmt[i].e,
			&cmt[i].q, &cmt[i].i, &cmt[i].an, &cmt[i].H,
			&cmt[i].G, x);

		if (m < 9){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame41->ProgressBar1->Max--;
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
				break;
			}

			if (cmt[i].full[j]=='('){
				for(k=0; cmt[i].full[k]!='('; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k-1]='\0';

				++k;
				for(l=0; cmt[i].full[k]!=')'; k++, l++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
				break;
			}
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = (int)TT + 2400000;
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;
		Frame41->ProgressBar1->Position = i+1;

		jul_to_greg(cmt[i].T, cmt[i].y, cmt[i].m, cmt[i].d);
		cmt[i].d += TT - (int)TT;

		if(do_exclude(i)){
			N--;
			i--;
			Frame41->ProgressBar1->Max--;
		}
	}

	return N;
}

int import_thesky (int N, FILE *fin){

	int j, k, l;
	int m, line=1;
	float G;
	char x[20+1];

	for (int i=0; i<N; i++) {
//		fscanf(fin, "%40c %*c %d %*c %4d %2d %2d %*c %d %*c %f %*c %f %*c %f %*c %f %*c %f %*c %f %25[^\n]%*c",     stari nacin
		m = fscanf(fin, "%45c %4d%2d%f | %f | %f | %f | %f | %f | %f | %f %20[^\n]%*c",
			cmt[i].full, &cmt[i].y, &cmt[i].m,
			&cmt[i].d, &cmt[i].q, &cmt[i].e,
			&cmt[i].pn, &cmt[i].an, &cmt[i].i, &cmt[i].H,
			&G, x);

		cmt[i].G = G/2.5;

		if (m < 12){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame41->ProgressBar1->Max--;
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
				break;
			}

			if (cmt[i].full[j]=='('){
				for(k=0; cmt[i].full[k]!='('; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k-1]='\0';

				++k;
				for(l=0; cmt[i].full[k]!=')'; k++, l++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
				break;
			}
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, (int)cmt[i].d);
		remove_spaces (cmt[i].name);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;
		Frame41->ProgressBar1->Position = i+1;

		if(do_exclude(i)){
			N--;
			i--;
			Frame41->ProgressBar1->Max--;
		}
	}

	return N;
}

int import_starry_night (int N, FILE *fin){

	int j, k, h;
	int m, line=1;
	char c;
	float G;
	long int y;

	for (int i=0; i<10; i++) fscanf(fin, "%*[^\n]\n");
	// u prvih 15 redova nema nikakvih podataka
	// ali je u samo 10 redova tekst, a 5 redova je prazno

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "     %29c %f 0.0 %f %f %f %f %f %ld.%d %ld.5 %f",
			cmt[i].name, &cmt[i].H, &cmt[i].e, &cmt[i].q, &cmt[i].an,
			&cmt[i].pn, &cmt[i].i, &cmt[i].T, &h,
			&y, &G);

		if (m < 11){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame41->ProgressBar1->Max--;
			continue;
		}

		cmt[i].G = G/2.5;

		j=0;
		fgetc(fin);
		fgetc(fin);
		while(j<13){
			c = fgetc(fin);
			cmt[i].ID[j++]=c;
		}
		cmt[i].ID[13] = '\0';

		fscanf(fin, "%*[^\n]\n");

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
		Frame41->ProgressBar1->Position = i+1;

		jul_to_greg(cmt[i].T, cmt[i].y, cmt[i].m, cmt[i].d);
		cmt[i].d += (float)h/10000;
		//cmt[i].d += TT - (int)TT;

		if(do_exclude(i)){
			N--;
			i--;
			Frame41->ProgressBar1->Max--;
		}
	}

	return N;
}

int import_deep_space (int N, FILE *fin){

	int j, m, line=2;
	float G;
	char c, x[8+1];

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

		m = fscanf(fin, "\n%8c %d %d %f %f %f %f %f %f %f %f\n",
			x, &cmt[i].y, &cmt[i].m, &cmt[i].d,
			&cmt[i].q, &cmt[i].e, &cmt[i].pn, &cmt[i].an,
			&cmt[i].i, &cmt[i].H, &G);

		cmt[i].G = G/2.5;

		if (m < 11){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame41->ProgressBar1->Max--;
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
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, (int)cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line+=2;
		Frame41->ProgressBar1->Position = i+1;

		if(do_exclude(i)){
			N--;
			i--;
			Frame41->ProgressBar1->Max--;
		}
	}

	return N;
}

int import_pc_tcs (int N, FILE *fin){

	int j, k;
	int m, line=1, len;
	float G;
	char tempID[20];

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "%s %f %f %f %f %f %d %d %f %f %f %60[^\n]%*c",
			cmt[i].ID, &cmt[i].q, &cmt[i].e, &cmt[i].i,
			&cmt[i].pn, &cmt[i].an, &cmt[i].y, &cmt[i].m,
			&cmt[i].d, &cmt[i].H, &G, cmt[i].name);

		cmt[i].G = G/2.5;

		if (m < 12){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame41->ProgressBar1->Max--;
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
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, (int)cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;
		Frame41->ProgressBar1->Position = i+1;

		if(do_exclude(i)){
			N--;
			i--;
			Frame41->ProgressBar1->Max--;
		}
	}

	return N;
}

int import_ecu (int N, FILE *fin){

	int j, k, l;
	float G;
	int m, line=2;

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "%45[^\n]%*cE C 2000 %d %d %f %f %f %f %f %f %f %f\n",
			cmt[i].full, &cmt[i].y, &cmt[i].m, &cmt[i].d,
			&cmt[i].q, &cmt[i].e, &cmt[i].pn, &cmt[i].an,
			&cmt[i].i, &cmt[i].H, &G);

		cmt[i].G = G/2.5;

		if (m < 11){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame41->ProgressBar1->Max--;
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
				break;
			}

			if (cmt[i].full[j]=='('){
				for(k=0; cmt[i].full[k]!='('; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k-1]='\0';

				++k;
				for(l=0; cmt[i].full[k]!=')'; k++, l++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
				break;
			}
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, (int)cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line+=2;
		Frame41->ProgressBar1->Position = i+1;

		if(do_exclude(i)){
			N--;
			i--;
			Frame41->ProgressBar1->Max--;
		}
	}

	return N;
}

int import_dance (int N, FILE *fin){

	int j, k;
	int m, line=1, len;
	char tempID[20];

	for (int i=0; i<5; i++) fscanf(fin, "%*[^\n]\n");

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "%11c %f %f %f %f %f %d.%2d%6f %30[^\n]%*c",
			cmt[i].ID, &cmt[i].q, &cmt[i].e, &cmt[i].i,
			&cmt[i].an, &cmt[i].pn, &cmt[i].y, &cmt[i].m,
			&cmt[i].d, cmt[i].name);

		if (m < 10){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame41->ProgressBar1->Max--;
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
		cmt[i].d /= 10000;
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, (int)cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;
		Frame41->ProgressBar1->Position = i+1;

		if(do_exclude(i)){
			N--;
			i--;
			Frame41->ProgressBar1->Max--;
		}
	}

	return N;
}

int import_megastar (int N, FILE *fin){

	int m, line=1;
	float G;
	char x[25+1];

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "%30c %12c %d %d %f %f %f %f %f %f %f %f %25[^\n]%*c",
			cmt[i].name, cmt[i].ID, &cmt[i].y, &cmt[i].m, &cmt[i].d,
			&cmt[i].q, &cmt[i].e, &cmt[i].pn,
			&cmt[i].an, &cmt[i].i, &cmt[i].H, &G, x);

		cmt[i].G = G/2.5;

		if (m < 13){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame41->ProgressBar1->Max--;
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
		Frame41->ProgressBar1->Position = i+1;

		if(do_exclude(i)){
			N--;
			i--;
			Frame41->ProgressBar1->Max--;
		}
	}

	return N;
}

int import_skychart (int N, FILE *fin){

	int j, k, l;
	int m, line=1;
	char c;

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "P11 2000.0 -%f %f %f %f %f 0 %d/%d/%f %f %f 0 0 ",
			&cmt[i].q, &cmt[i].e, &cmt[i].i, &cmt[i].pn,
			&cmt[i].an, &cmt[i].y, &cmt[i].m, &cmt[i].d,
			&cmt[i].H, &cmt[i].G);

		if (m < 10){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame41->ProgressBar1->Max--;
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
				break;
			}

			if (cmt[i].full[j]=='('){
				for(k=0; cmt[i].full[k]!='('; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k-1]='\0';

				++k;
				for(l=0; cmt[i].full[k]!=')'; k++, l++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
				break;
			}
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, (int)cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;
		Frame41->ProgressBar1->Position = i+1;

		if(do_exclude(i)){
			N--;
			i--;
			Frame41->ProgressBar1->Max--;
		}
	}

	return N;
}

int import_voyager (int N, FILE *fin){

	int m, line=1;
	char mj[3+1];

	for (int i=0; i<18; i++) fscanf(fin, "%*[^\n]\n");

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "%27c %f %f %f %f %f %f %4d %3c %f 2000.0\n",
			cmt[i].name, &cmt[i].q, &cmt[i].e, &cmt[i].i,
			&cmt[i].an, &cmt[i].pn, &cmt[i].G, &cmt[i].y,
			mj, &cmt[i].d);

		if (m < 10){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame41->ProgressBar1->Max--;
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
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, (int)cmt[i].d);
		remove_spaces (cmt[i].name);
//		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;
		Frame41->ProgressBar1->Position = i+1;

		if(do_exclude(i)){
			N--;
			i--;
			Frame41->ProgressBar1->Max--;
		}
	}

	return N;
}

int import_skytools (int N, FILE *fin){

	int j, k, l, u, t=0, space;
	int yy, mm, dd;
	int m, line=1, len;
	char x[15+1];

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "C %40c %d %d %d %d %d %f %f %f %f %f %f %f %f 0.002000 %15[^\n]%*c",
			cmt[i].full, &yy, &mm, &dd, &cmt[i].y, &cmt[i].m, &cmt[i].d,
			&cmt[i].q, &cmt[i].e, &cmt[i].pn, &cmt[i].an, &cmt[i].i,
			&cmt[i].H, &cmt[i].G, x);

		//cmt[i].h*=10;

		if (m < 15){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame41->ProgressBar1->Max--;
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
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, (int)cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;
		Frame41->ProgressBar1->Position = i+1;

		if(do_exclude(i)){
			N--;
			i--;
			Frame41->ProgressBar1->Max--;
		}
	}

	return N;
}

int import_cfw (int N, FILE *fin){

	int j, k, l;
	float G;

	//for (int i=0; i<7; i++) fscanf(fin, "%*[^\n]\n");

	for (int i=0; i<N; i++){

		fscanf(fin, "name=%40[^\n]%*c\
					%*[^\n]\n\
					type=orbit\n\
					T=%d %d %f\n\
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
					&cmt[i].y, &cmt[i].m, &cmt[i].d,
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
				break;
			}

			if (cmt[i].full[j]=='('){
				for(k=0; cmt[i].full[k]!='('; k++)
					cmt[i].ID[k]=cmt[i].full[k];

				cmt[i].ID[k-1]='\0';

				++k;

				for(l=0; cmt[i].full[k]!=')'; k++, l++)
					cmt[i].name[l]=cmt[i].full[k];

				cmt[i].name[l]='\0';
				break;
			}
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, (int)cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		Frame41->ProgressBar1->Position = i+1;

		if(do_exclude(i)){
			N--;
			i--;
			Frame41->ProgressBar1->Max--;
		}
	}

	return N;
}

int import_nasa (int N, FILE *fin){

	int j, k, l;
	int m, line=1, len, trash;
	char c, q, x[20+1];

	fscanf(fin, "%*[^\n]\n");
	fscanf(fin, "%*[^\n]\n");

	for (int i=0; i<N; i++) {

		k=0; j=0;
		while (k<44){
			c=fgetc(fin);
			cmt[i].full[j]=c;
			if (c==' ' && j==0) --j;
			j++;
			k++;
		}

		remove_spaces(cmt[i].full);

		if (Frame41->CheckBox1->Checked){
			len = strlen(cmt[i].full);
			for (j=0; j<len; j++){
				if (cmt[i].full[j  ]=='S' && cmt[i].full[j+1]=='O' &&
					cmt[i].full[j+2]=='H' && cmt[i].full[j+3]=='O'){

					N--; i--;
					Frame41->ProgressBar1->Max--;
					continue;
				}
			}
		}

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

		m = fscanf(fin, "%d %f %f %f %f %f %4d%2d%f %20[^\n]%*c",
			&trash, &cmt[i].q, &cmt[i].e, &cmt[i].i,
			&cmt[i].pn, &cmt[i].an, &cmt[i].y, &cmt[i].m,
			&cmt[i].d, x);

		if (m < 10){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame41->ProgressBar1->Max--;
			continue;
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		Frame41->ProgressBar1->Position = i+1;
		line++;

		if(do_exclude(i)){
			N--;
			i--;
			Frame41->ProgressBar1->Max--;
		}
	}

	return N;
}

void export_mpc (int N, FILE *fout){

	for (int i=0; i<N; i++) {

		fprintf(fout,"              %4d %02d %07.4f %9f  %.6f  %8.4f  %8.4f  %8.4f  %4d%02d%02d  %4.1f %4.1f  %-56s MPC 00000\n",
				cmt[i].y, cmt[i].m, cmt[i].d, cmt[i].q, cmt[i].e,
				cmt[i].pn, cmt[i].an, cmt[i].i, ep_y, ep_m, ep_d, cmt[i].H, cmt[i].G, cmt[i].full);

		Frame61->ProgressBar1->Position = i+1;
	}
}

void export_skymap (int N, FILE *fout){

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

		fprintf(fout,"%4d %02d %07.4f %9f       %.6f %8.4f %8.4f %8.4f  %4.1f  %4.1f\n",
				cmt[i].y, cmt[i].m, cmt[i].d, cmt[i].q,
				cmt[i].e, cmt[i].pn, cmt[i].an, cmt[i].i, cmt[i].H, cmt[i].G);

		Frame61->ProgressBar1->Position = i+1;
	}
}

void export_guide (int N, FILE *fout){

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

		fprintf(fout,"%7.4f  %2d  %4d  0.0        %9.6f    %.6f  %8.4f    %8.4f    %8.4f    %d.0   %4.1f %4.1f    MPC 00000\n",
				cmt[i].d, cmt[i].m, cmt[i].y, cmt[i].q, cmt[i].e,
				cmt[i].i, cmt[i].pn, cmt[i].an, equinox, cmt[i].H, cmt[i].G);

		Frame61->ProgressBar1->Position = i+1;
	}
}

void export_xephem (int N, FILE *fout){

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

			fprintf(fout, "p,%02d/%06.3f/%4d,%.3f,%.3f,%.5f,%.3f,2000,%.1f,%.1f\n",
				cmt[i].m, cmt[i].d, cmt[i].y,
				cmt[i].i, cmt[i].pn, cmt[i].q, cmt[i].an,
				cmt[i].H, cmt[i].G);
		}

		if(cmt[i].e > 1.0){

			fprintf(fout, "h,%02d/%07.4f/%4d,%.4f,%.4f,%.4f,%.6f,%.6f,2000,%.1f,%.1f\n",
				cmt[i].m, cmt[i].d, cmt[i].y,
				cmt[i].i, cmt[i].an, cmt[i].pn, cmt[i].e,
				cmt[i].q, cmt[i].H, cmt[i].G);
		}

		Frame61->ProgressBar1->Position = i+1;
	}
}

void export_home_planet (int N, FILE *fout){

	for (int i=0; i<N; i++) {

		double smAxis = cmt[i].q/((1-cmt[i].e)+0.000001);

		fprintf(fout,"%s,%d-%d-%7.4f,%.6f,%.6f,%.4f,%.4f,%.4f,%.5f,%.5f years, MPC      \n",
				cmt[i].full, cmt[i].y, cmt[i].m, cmt[i].d, cmt[i].q,
				cmt[i].e, cmt[i].pn, cmt[i].an, cmt[i].i, smAxis, cmt[i].P);

		Frame61->ProgressBar1->Position = i+1;
	}
}

void export_mystars (int N, FILE *fout){

	for (int i=0; i<N; i++) {

		int h = (cmt[i].d - (int)cmt[i].d)*10000;

		fprintf(fout,"%s;\t%ld.%04d\t%.4f\t%.6f\t%.6f\t%.4f\t%.4f\t%.1f\t%.1f\tMPC00000\t%ld.0\n",
				cmt[i].full, cmt[i].T-2400000, h, cmt[i].pn, cmt[i].e, cmt[i].q,
				cmt[i].i, cmt[i].an, cmt[i].H, cmt[i].G, eq_JD-2400000);

		Frame61->ProgressBar1->Position = i+1;
	}
}

void export_thesky (int N, FILE *fout){

	for (int i=0; i<N; i++) {

		fprintf(fout,"%-39s|%d|%4d%02d%07.4f |%9f |%.6f |%8.4f |%8.4f |%8.4f |%4.1f |%4.1f | MPC 00000\n",
				cmt[i].full, equinox, cmt[i].y, cmt[i].m, cmt[i].d, cmt[i].q,
				cmt[i].e, cmt[i].pn, cmt[i].an, cmt[i].i, cmt[i].H, cmt[i].G*2.5);

		Frame61->ProgressBar1->Position = i+1;
	}
}

void export_starry_night (int N, FILE *fout){

	for (int i=0; i<N; i++) {

		int h = (cmt[i].d - (int)cmt[i].d)*10000;

		fprintf(fout,"     %-29s %4.1f    0.0   %.6f   %9.6f    %8.4f  %8.4f  %8.4f  %ld.%04d    %ld.5  %4.1f  %-13s MPC 00000\n",
				cmt[i].name, cmt[i].H, cmt[i].e, cmt[i].q, cmt[i].an, cmt[i].pn,
				cmt[i].i, cmt[i].T, h, eq_JD, cmt[i].G*2.5, cmt[i].ID);

		Frame61->ProgressBar1->Position = i+1;
	}
}

void export_deep_space (int N, FILE *fout){

	for (int i=0; i<N; i++) {

		fprintf(fout,"%s (%s)\nC J%d %4d %02d %07.4f %.6f %.6f %.4f %.4f %.4f %.1f %.1f\n",
				cmt[i].name, cmt[i].ID, equinox, cmt[i].y, cmt[i].m, cmt[i].d,
				cmt[i].q, cmt[i].e, cmt[i].pn, cmt[i].an, cmt[i].i, cmt[i].H, cmt[i].G*2.5);

		Frame61->ProgressBar1->Position = i+1;
	}
}

void export_pc_tcs (int N, FILE *fout){

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

		fprintf(fout,"%s %.6f %.6f %.4f %.4f %.4f %4d %02d %07.4f %.1f %.1f %s\n",
				cmt[i].ID, cmt[i].q, cmt[i].e, cmt[i].i, cmt[i].pn, cmt[i].an,
				cmt[i].y, cmt[i].m, cmt[i].d, cmt[i].H, cmt[i].G*2.5, cmt[i].name);

		Frame61->ProgressBar1->Position = i+1;
	}
}

void export_ecu (int N, FILE *fout){

	for (int i=0; i<N; i++) {

		fprintf(fout,"%s\nE C %d %4d %02d %07.4f %.6f %.6f %.4f %.4f %.4f %.1f %.1f\n",
				cmt[i].full, equinox, cmt[i].y, cmt[i].m, cmt[i].d, cmt[i].q,
				cmt[i].e, cmt[i].pn, cmt[i].an, cmt[i].i, cmt[i].H, cmt[i].G*2.5);

		Frame61->ProgressBar1->Position = i+1;
	}
}

void export_dance (int N, FILE *fout){

	for (int i=0; i<N; i++) {

		fprintf(fout,"%s\nE C %d %4d %02d %07.4f %.6f %.6f %.4f %.4f %.4f %.1f %.1f\n",
				cmt[i].full, equinox, cmt[i].y, cmt[i].m, cmt[i].d,
				cmt[i].q, cmt[i].e, cmt[i].pn, cmt[i].an, cmt[i].i, cmt[i].H, cmt[i].G);

		Frame61->ProgressBar1->Position = i+1;
	}
}

void export_megastar (int N, FILE *fout){

	for (int i=0; i<N; i++) {

		fprintf(fout,"%-30s%-12s%4d %02d  %07.4f   %9.6f   %.6f    %8.4f    %8.4f    %8.4f   %4.1f   %4.1f    %d MPC 00000\n",
				cmt[i].name, cmt[i].ID, cmt[i].y, cmt[i].m, cmt[i].d,
				cmt[i].q, cmt[i].e, cmt[i].pn, cmt[i].an,
				cmt[i].i, cmt[i].H, cmt[i].G*2.5, equinox);


		Frame61->ProgressBar1->Position = i+1;
	}
}

void export_skychart (int N, FILE *fout){

	for (int i=0; i<N; i++) {

		fprintf(fout,"P11	%d.0	-%.6f\t%.6f\t%.3f\t%.4f\t%.4f\t0\t%4d/%02d/%07.4f\t%.1f %.1f\t0\t0\t%s; MPC 00000\t\n",
				equinox, cmt[i].q, cmt[i].e, cmt[i].i, cmt[i].pn, cmt[i].an, cmt[i].y,
				cmt[i].m, cmt[i].d, cmt[i].H, cmt[i].G, cmt[i].full);

		Frame61->ProgressBar1->Position = i+1;
	}
}

void export_voyager (int N, FILE *fout){

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

		if ((int)cmt[i].d<10) fprintf(fout, "%7.4f  %d.0\n", cmt[i].d, equinox);
		else fprintf(fout, "%7.4f %d.0\n", cmt[i].d, equinox);

		Frame61->ProgressBar1->Position = i+1;
	}
}

void export_skytools (int N, FILE *fout){

	int j, k, len;

	for (int i=0; i<N; i++) {

		int h = (cmt[i].d - (int)cmt[i].d)*10000;

		if(h>999) h/=10;

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
				cmt[i].y, cmt[i].m, (int)cmt[i].d, h, cmt[i].q, cmt[i].e,
				cmt[i].pn, cmt[i].an, cmt[i].i, cmt[i].H, cmt[i].G, equinox);

		Frame61->ProgressBar1->Position = i+1;
	}
}

void export_ssc (int N, FILE *fout){

	char *mon;

	for (int i=0; i<N; i++) {

		if (cmt[i].e == 1) cmt[i].e = 1.000001;

		int h = (cmt[i].d - (int)cmt[i].d)*10000;

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
				cmt[i].T, h, cmt[i].y, mon, (int)cmt[i].d, h);
		fprintf(fout,"\t} \n");
		fprintf(fout,"}\n\n\n");

		Frame61->ProgressBar1->Position = i+1;
	}
}

void export_stell (int N, FILE *fout){

	for (int i=0; i<N; i++) {

		int len = strlen(cmt[i].name);
		for (int j=0; j<len; j++) if (isupper(cmt[i].name[j])) cmt[i].name[j] = tolower(cmt[i].name[j]);

		int h = (cmt[i].d - (int)cmt[i].d)*10000;

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
		fprintf(fout,"orbit_TimeAtPericenter = %ld.%.4d\n\n", cmt[i].T, h);

		Frame61->ProgressBar1->Position = i+1;
	}
}

void sort_data (int N) {

	Frame41->ProgressBar1->Visible = true;
	Frame41->ProgressBar1->Position = 0;
	Frame41->ProgressBar1->Max = N-1;

	for (int i=0; i<N-1; i++){
		for (int j=i+1; j<N; j++){
			if (Frame41->ComboBox2->ItemIndex == 1 && Frame41->ComboBox3->ItemIndex == 0 && cmt[i].sort > cmt[j].sort) do_swap(i, j);
			if (Frame41->ComboBox2->ItemIndex == 1 && Frame41->ComboBox3->ItemIndex == 1 && cmt[i].sort < cmt[j].sort) do_swap(i, j);
			if (Frame41->ComboBox2->ItemIndex == 2 && Frame41->ComboBox3->ItemIndex == 0 && cmt[i].T > cmt[j].T) do_swap(i, j);
			if (Frame41->ComboBox2->ItemIndex == 2 && Frame41->ComboBox3->ItemIndex == 1 && cmt[i].T < cmt[j].T) do_swap(i, j);
			if (Frame41->ComboBox2->ItemIndex == 3 && Frame41->ComboBox3->ItemIndex == 0 && cmt[i].q > cmt[j].q) do_swap(i, j);
			if (Frame41->ComboBox2->ItemIndex == 3 && Frame41->ComboBox3->ItemIndex == 1 && cmt[i].q < cmt[j].q) do_swap(i, j);
			if (Frame41->ComboBox2->ItemIndex == 4 && Frame41->ComboBox3->ItemIndex == 0 && cmt[i].e > cmt[j].e) do_swap(i, j);
			if (Frame41->ComboBox2->ItemIndex == 4 && Frame41->ComboBox3->ItemIndex == 1 && cmt[i].e < cmt[j].e) do_swap(i, j);
			if (Frame41->ComboBox2->ItemIndex == 5 && Frame41->ComboBox3->ItemIndex == 0 && cmt[i].an > cmt[j].an) do_swap(i, j);
			if (Frame41->ComboBox2->ItemIndex == 5 && Frame41->ComboBox3->ItemIndex == 1 && cmt[i].an < cmt[j].an) do_swap(i, j);
			if (Frame41->ComboBox2->ItemIndex == 6 && Frame41->ComboBox3->ItemIndex == 0 && cmt[i].pn > cmt[j].pn) do_swap(i, j);
			if (Frame41->ComboBox2->ItemIndex == 6 && Frame41->ComboBox3->ItemIndex == 1 && cmt[i].pn < cmt[j].pn) do_swap(i, j);
			if (Frame41->ComboBox2->ItemIndex == 7 && Frame41->ComboBox3->ItemIndex == 0 && cmt[i].i > cmt[j].i) do_swap(i, j);
			if (Frame41->ComboBox2->ItemIndex == 7 && Frame41->ComboBox3->ItemIndex == 1 && cmt[i].i < cmt[j].i) do_swap(i, j);
			if (Frame41->ComboBox2->ItemIndex == 8 && Frame41->ComboBox3->ItemIndex == 0 && cmt[i].P > cmt[j].P) do_swap(i, j);
			if (Frame41->ComboBox2->ItemIndex == 8 && Frame41->ComboBox3->ItemIndex == 1 && cmt[i].P < cmt[j].P) do_swap(i, j);
		}

		Frame41->ProgressBar1->Position = i+1;
	}
}

void do_swap(int i, int j){

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
}

bool define_exclude(){

	for (int i=0; i<14; i++) excl.key[i]=false;

	if(Frame31->CheckBox1->Checked){

		char dd[3], mm[3], yy[5];
		int d, m, y;

		AnsiString str =  Frame31->MaskEdit1->Text;
		const char *date = str.c_str();

		dd[0]=date[0];
		dd[1]=date[1];
		dd[2]='\0';
		mm[0]=date[3];
		mm[1]=date[4];
		mm[2]='\0';
		yy[0]=date[6];
		yy[1]=date[7];
		yy[2]=date[8];
		yy[3]=date[9];
		yy[4]='\0';

		d = atoi(dd);
		m = atoi(mm);
		y = atoi(yy);

		if (d<1 || d>31 || m<1 || m>12 || y<1000 || y>3000) throw(Exception("Invalid date"));

		excl.T = greg_to_jul(y, m, d);

		if(Frame31->ComboBox1->ItemIndex==0) excl.key[0]=true;
		if(Frame31->ComboBox1->ItemIndex==1) excl.key[1]=true;
	}

	if(Frame31->CheckBox2->Checked){

		if (Frame31->Edit2->GetTextLen() == 0) throw(Exception("Please enter value"));

		AnsiString str = Frame31->Edit2->Text;

		float q = atof(str.c_str());

		if (q <= 0) throw(Exception("Value must be greather than zero"));

		excl.q = q;

		if(Frame31->ComboBox2->ItemIndex==0) excl.key[2]=true;
		if(Frame31->ComboBox2->ItemIndex==1) excl.key[3]=true;
	}

	if(Frame31->CheckBox3->Checked){

		if (Frame31->Edit3->GetTextLen() == 0) throw(Exception("Please enter value"));

		AnsiString str =  Frame31->Edit3->Text;

		float e = atof(str.c_str());

		if (e<0 || e>1) throw(Exception("Value must be between 0 and 1"));

		excl.e = e;

		if(Frame31->ComboBox3->ItemIndex==0) excl.key[4]=true;
		if(Frame31->ComboBox3->ItemIndex==1) excl.key[5]=true;
	}

	if(Frame31->CheckBox4->Checked){

		if (Frame31->Edit4->GetTextLen() == 0) throw(Exception("Please enter value"));

		AnsiString str =  Frame31->Edit4->Text;

		float an = atof(str.c_str());

		if (an<0 || an>=360) throw(Exception("Value must be between 0 and 360"));

		excl.an = an;

		if(Frame31->ComboBox4->ItemIndex==0) excl.key[6]=true;
		if(Frame31->ComboBox4->ItemIndex==1) excl.key[7]=true;
	}

	if(Frame31->CheckBox5->Checked){

		if (Frame31->Edit5->GetTextLen() == 0) throw(Exception("Please enter value"));

		AnsiString str =  Frame31->Edit5->Text;

		float pn = atof(str.c_str());

		if (pn<0 || pn>=360) throw(Exception("Value must be between 0 and 360"));

		excl.pn = pn;

		if(Frame31->ComboBox5->ItemIndex==0) excl.key[8]=true;
		if(Frame31->ComboBox5->ItemIndex==1) excl.key[9]=true;
	}

	if(Frame31->CheckBox6->Checked){

		if (Frame31->Edit6->GetTextLen() == 0) throw(Exception("Please enter value"));

		AnsiString str =  Frame31->Edit6->Text;

		float i = atof(str.c_str());

		if (i<0 || i>=180) throw(Exception("Value must be between 0 and 180"));

		excl.i = i;

		if(Frame31->ComboBox6->ItemIndex==0) excl.key[10]=true;
		if(Frame31->ComboBox6->ItemIndex==1) excl.key[11]=true;
	}

	if(Frame31->CheckBox7->Checked){

		if (Frame31->Edit7->GetTextLen() == 0) throw(Exception("Please enter value"));

		AnsiString str =  Frame31->Edit7->Text;

		float P = atof(str.c_str());

		if (P <= 0) throw(Exception("Value must be greather than zero"));

		excl.P = P;

		if(Frame31->ComboBox7->ItemIndex==0) excl.key[12]=true;
		if(Frame31->ComboBox7->ItemIndex==1) excl.key[13]=true;
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
extern PACKAGE TForm1 *Form1;
//---------------------------------------------------------------------------
#endif
