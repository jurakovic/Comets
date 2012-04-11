//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "MainForm.h"
#include "FormSettings.h"
#include "FormPreview.h"
#include "FormAbout.h"
#include <Registry.hpp>
#include <System.IOUtils.hpp>
#include <IniFiles.hpp>
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma link "Frame1"
#pragma link "Frame2"
#pragma link "Frame4"
#pragma link "Frame3"
#pragma link "FrameSplash"
#pragma resource "*.dfm"
TForm1 *Form1;
//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
	: TForm(Owner)
{
}
//---------------------------------------------------------------------------
void __fastcall TForm1::FormCreate(TObject *Sender)
{
	TRegistry *reg = new TRegistry;
	reg->RootKey = HKEY_CURRENT_USER;

	String regKey = String("Software\\Comet OEW\\");
	if(reg->KeyExists(regKey)){

		reg->OpenKey(regKey, true);
		dataFolder = reg->ReadString("Data Folder");
	}
	else{
		dataFolder = _wgetenv(L"appdata");
		dataFolder += "\\Comet OEW";

		reg->OpenKey(regKey, true);
		reg->WriteString("Data Folder", dataFolder);
	}
	reg->CloseKey();
	delete reg;

	if(DirectoryExists(dataFolder) == false){

		_wmkdir(dataFolder.w_str());
	}

	settingsFile = dataFolder + "\\settings.ini";

	TIniFile *iniSett = new TIniFile(settingsFile);

	if(FileExists(settingsFile)){

		Left = iniSett->ReadInteger("MainForm", "Left", 0);
		Top  = iniSett->ReadInteger("MainForm", "Top", 0);

		sett.checkNewVersion = iniSett->ReadInteger("Preferences", "CheckNewVersion", 0);
		sett.advancedMode = iniSett->ReadInteger("Preferences", "AdvancedMode", 0);
		sett.exitConfirm = iniSett->ReadInteger("Preferences", "ExitConfirm", 0);
	}
	else {
		Position = poDesktopCenter;

		int resx = GetSystemMetrics(SM_CXSCREEN);
		int resy = GetSystemMetrics(SM_CYSCREEN);

		iniSett->WriteInteger("MainForm", "Left", (resx/2)-(Width/2));
		iniSett->WriteInteger("MainForm", "Top", (resy/2)-(Height/2));

		int ww = resx - 100;
		int hh = resy - 100;

		iniSett->WriteInteger("PreviewForm", "Left", 50);
		iniSett->WriteInteger("PreviewForm", "Top", 50);
		iniSett->WriteInteger("PreviewForm", "Width", ww);
		iniSett->WriteInteger("PreviewForm", "Height", hh);
		iniSett->WriteInteger("PreviewForm", "WindowState", wsNormal);

		sett.checkNewVersion = 1;
		iniSett->WriteInteger("Preferences", "CheckNewVersion", 1);
		sett.advancedMode = 0;
		iniSett->WriteInteger("Preferences", "AdvancedMode", 0);
		sett.exitConfirm  = 0;
		iniSett->WriteInteger("Preferences", "ExitConfirm", 0);
	}

	delete iniSett;

	if(sett.advancedMode){
		Frame1->CheckBox1->Checked = true;
		Frame1->CheckBox1->Visible = false;
	}

	Frame2->Visible = false;
	Frame3->Visible = false;
	Frame4->Visible = false;
}

//---------------------------------------------------------------------------

void __fastcall TForm1::FormClose(TObject *Sender, TCloseAction &Action)
{
	if(Form1->sett.exitConfirm){
		Action = true;
	}

	else{
		if(exitFunction())
			Action = true;
		else
			Action = false;
	}

	ocistiMemoriju(&Form1->cmt);
	spremiPostavke();
}
//---------------------------------------------------------------------------


int TForm1::import_main (int importType, UnicodeString importFile){

	cmt = NULL;

	FILE *fin = Frame1->fin;
	//FILE *fin = fopen(AnsiString(importFile).c_str(), "r");

	int Ncmt = Frame1->detectedComets;

	Frame2->ProgressBar1->Visible = true;
	Frame2->ProgressBar1->Position = 0;
	Frame2->ProgressBar1->Max = Ncmt;

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

	//fclose(fin);
	rewind(fin);

	if(Ncmt == 0){
		Application->MessageBox(L"There are no imported comets\n\n"
			L"Two possible reasons:\n\n"
			L" - You selected wrong import format\n"
			L" - Excluding rules are too high",
			L"Error",
			MB_OK | MB_ICONERROR);
		Frame2->ProgressBar1->Visible = false;
		Frame2->Button1->Enabled = false;
		return 0;
	}

	return Ncmt;
}

void TForm1::export_main (int Ncmt, int exportFormat, UnicodeString exportFile){

	FILE *fout = fopen(AnsiString(exportFile).c_str(), "w");

	if(!fout){
		Application->MessageBox(L"Unable to create input file",
			L"Error",
			MB_OK | MB_ICONERROR);
		return;
	}

	Frame4->ProgressBar1->Visible = true;
	Frame4->ProgressBar1->Position = 0;
	Frame4->ProgressBar1->Max = Ncmt;

	writeNecessaryText(exportFormat, fout);

	export_semi(exportFormat, cmt, fout);

	fclose(fout);
}

void TForm1::writeNecessaryText(int exp_ty, FILE *fout){

	if(exp_ty == 4){
		fprintf(fout, "Name,Perihelion time,Perihelion AU,Eccentricity,Long. perihelion,Long. node,Inclination,Semimajor axis,Period\n");
	}

	if(exp_ty == 5){
		fprintf(fout, "RDPC	%d\n", Form1->Frame3->Ncmt);
	}

	if(exp_ty == 7){
		fprintf(fout, "NOTE: If viewing this file and it appears confused, make the window very wide!\n\n");
		fprintf(fout, "   The numbers are all in the proper format for easy use in Starry Night's\n");
		fprintf(fout, "orbit editor. Just click on the word Sun in the planet floater and then\n");
		fprintf(fout, "click on add. In the first window that appears select the comet as the type\n");
		fprintf(fout, "of object you want to add. Please see the manual for more information.\n\n");
		fprintf(fout, "   The orbital information should have the reference plane set at Ecliptic\n");
		fprintf(fout, " 2000 and the Style should be pericentric. Don't forget to use copy and\n");
		fprintf(fout, " paste to ease the input of the orbital data into Starry Night.\n\n");
		fprintf(fout, "This file kindly prepared by the IAU Minor Planet Center & Central Bureau for Astronomical Telegrams.\n\n");
        fprintf(fout, "Num  Name                          Mag.   Diam      e            q        Node         w         i         Tp           Epoch       k   Desig         Reference\n\n");
	}

	if(exp_ty == 8){
		fprintf(fout, "Type C: Equinox Year Month Day q e Peri Node i Mag k\n");
		fprintf(fout, "Type A: Equinox Year Month Day a M e Peri Node i H G\n");
	}

	if(exp_ty == 11){
		fprintf(fout, "Comet      peri(au)   e         iø       êø       wø     peridate     name\n");
		fprintf(fout, "(In order to be recognised by Dance of the Planets, this file)\n");
		fprintf(fout, "(must have a .cmt extension.)\n");
		fprintf(fout, "(File prepared by IAU Minor Planet Center/Central Bureau)\n");
		fprintf(fout, "(for Astronomical Telegrams.)\n");

	}

	if(exp_ty == 14){
		fprintf(fout, "NOTE TO VOYAGER II USERS:\n\n");
		fprintf(fout, "   The following table will link the symbols below with the names used in\n");
		fprintf(fout, "the Voyager II \"Define New Orbit...\" dialog for comets.\n\n");
		fprintf(fout, "     q        perihelion distance (astronomical units)\n");
		fprintf(fout, "     e        eccentricity (no units)\n");
		fprintf(fout, "     i        inclination of orbit to ecliptic (degrees)\n");
		fprintf(fout, "     Node     longitude of ascending node (degrees)\n");
		fprintf(fout, "     w        argument of perihelion (degrees)\n");
		fprintf(fout, "     L        mean anomaly (this is 0 at perihelion) (degrees)\n");
		fprintf(fout, "     Date     epoch of orbit\n");
		fprintf(fout, "     Equinox  reference equinox (usually 2000.0)\n\n");
		fprintf(fout, "Save this page as plain text from your browser and use the table to input\n");
		fprintf(fout, "the orbital elements for the comets that you would like to plot and\n");
		fprintf(fout, "follow.  If you have any question, consult your software manual or the\n");
		fprintf(fout, "Carina web site: <a href=\"http://www.carinasoft.com\">http://www.carinasoft.com</a>\n\n");
		fprintf(fout, "Thanks to the IAU Minor Planet Center & Central Bureau for Astronomical\n");
		fprintf(fout, "Telegrams for providing this information.\n\n");
		fprintf(fout, "Name                            q          e         i        Node         w       L      T(Date)    Equinox\n");
	}
}


void TForm1::export_semi(int exp_ty, Comet *head, FILE *fout){

	while(head!=NULL){

		if (exp_ty== 0) export_mpc (head, fout);
		if (exp_ty== 1) export_skymap (head, fout);
		if (exp_ty== 2) export_guide (head, fout);
		if (exp_ty== 3) export_xephem (head, fout);
		if (exp_ty== 4) export_home_planet (head, fout);
		if (exp_ty== 5) export_mystars (head, fout);
		if (exp_ty== 6) export_thesky (head, fout);
		if (exp_ty== 7) export_starry_night (head, fout);
		if (exp_ty== 8) export_deep_space (head, fout);
		if (exp_ty== 9) export_pc_tcs (head, fout);
		if (exp_ty==10) export_ecu (head, fout);
		if (exp_ty==11) export_dance (head, fout);
		if (exp_ty==12) export_megastar (head, fout);
		if (exp_ty==13) export_skychart (head, fout);
		if (exp_ty==14) export_voyager (head, fout);
		if (exp_ty==15) export_skytools (head, fout);
		if (exp_ty==16) export_thesky (head, fout);
		if (exp_ty==17) export_ssc (head, fout);
		if (exp_ty==18) export_stell (head, fout);

		head = head->next;
		Frame4->ProgressBar1->Position += 1;
	}

	Application->ProcessMessages();
}

int TForm1::import_mpc (int N, FILE *fin){

	int j, k, l;
	int m, line=1;
	char x[30+1];

	for (int i=0; i<N; i++) {

		Comet *com = new Comet;

		m = fscanf(fin, "%14c %d %02d %f %f %f %f %f %f%12c%f %f %55c %30[^\n]%*c",		// %f%12c%f mora bit tako zajedno
			x, &com->y, &com->m, &com->d,
			&com->q, &com->e, &com->pn, &com->an,
			&com->i, x, &com->H, &com->G, com->full, x);

		if (m < 14){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame2->ProgressBar1->Max--;
			delete com;
			continue;
		}

		remove_spaces(com->full);

		editFullIdName(com->full, com->ID, com->name, 1);

		com->P = compute_period (com->q, com->e);
		com->T = greg_to_jul (com->y, com->m, (int)com->d);
		com->sort = get_sort_key(com->ID);
		line++;

		Frame2->ProgressBar1->Position = i+1;


		if(Frame1->CheckBox1->Checked){
			if(do_exclude(com) || check_name(com)){
				N--;
				i--;
				Frame2->ProgressBar1->Max--;
				delete com;
				continue;
			}
		}

		addCmt(&cmt, com);
		delete com;
	}

	return N;
}

int TForm1::import_skymap (int N, FILE *fin){

	int j, k, l, u, t=0, space;
	int m, line=1, len;

	for (int i=0; i<N; i++) {

		Comet *com = new Comet;

		m = fscanf(fin, "%47c %4d %2d %f %f %f %f %f %f %f %f\n",
			com->full, &com->y, &com->m, &com->d,
			&com->q, &com->e, &com->pn,
			&com->an, &com->i, &com->H, &com->G);

		if (m < 11){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame2->ProgressBar1->Max--;
			delete com;
			continue;
		}

		remove_spaces(com->full);

		if ((com->full[0]=='C' && com->full[1]=='/') ||
			(com->full[0]=='P' && com->full[1]=='/') ||
			(com->full[0]=='D' && com->full[1]=='/')){

			space=0;
			len = strlen(com->full);
			for(u=0; u<len; u++){
				if (com->full[u]==' ' && space==1) {
					t=u;
					break;
				}
				else if(com->full[u]==' ') space++;
			}

			for(k=0; k<t; k++)
				com->ID[k]=com->full[k];

			com->ID[k]='\0';

			++k;
			for(l=0; com->full[k]!='\0'; k++, l++)
				com->name[l]=com->full[k];

			com->name[l]='\0';

			AnsiString s1 = String(com->ID) + " (" + String(com->name) + ")";
			strcpy(com->full, s1.c_str());
		}
		else{

			strcpy(com->ID, com->full);

			for(k=0; ; k++) {

				if(com->ID[k]==' ') break;
			}

			com->ID[k]='\0';
			com->full[k]='/';

			++k;
			for(l=0; com->full[k]!='\0'; l++, k++)
				com->name[l]=com->full[k];

			com->name[l]='\0';
		}


		com->P = compute_period (com->q, com->e);
		com->T = greg_to_jul (com->y, com->m, (int)com->d);
		com->sort = get_sort_key(com->ID);
		line++;

		if(Frame1->CheckBox1->Checked)
			Frame2->ProgressBar1->Position = i+1;
		else
			Frame4->ProgressBar1->Position = i+1;

		if(Frame1->CheckBox1->Checked){
			if(do_exclude(com) || check_name(com)){
				N--;
				i--;
				Frame2->ProgressBar1->Max--;
				delete com;
				continue;
			}
		}

		addCmt(&cmt, com);
		delete com;
	}

	return N;
}

int TForm1::import_guide (int N, FILE *fin){

	int j, k, m, line=1, len;
	char c, x[21], full[42+1];

	for (int i=0; i<N; i++) {

		Comet *com = new Comet;

		m = fscanf(fin, "%42c %f %d %d 0.0 %f %f %f %f %f 2000.0 %f %f %20[^\n]%*c",
			full, &com->d, &com->m, &com->y,
			&com->q,  &com->e, &com->i, &com->pn,
			&com->an, &com->H, &com->G, x);

		if (m < 12){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame2->ProgressBar1->Max--;
			delete com;
			continue;
		}

		j=0;
		while ((full[j]) != '(' ){
			com->name[j]=full[j];
			j++;
		}
		com->name[j-1]='\0';

		j++; k=0;
		while ((full[j]) != ')' ){
			com->ID[k]=full[j];
			j++; k++;
		}
		com->ID[k]='\0';

		if ((com->name[0]=='P' && com->name[1]=='/') ||
			(com->name[0]=='D' && com->name[1]=='/')){
			len = strlen(com->name);
			for (j=0; j<len; j++)
				com->name[j]=com->name[j+2];		//j+2 jer je na mjestu 0='P', 1='/'

			strcpy(com->full, com->ID);
			strcat(com->full, "/");
			strcat(com->full, com->name);
		}

		else {
			strcpy(com->full, com->ID);
			strcat(com->full, " (");
			strcat(com->full, com->name);
			strcat(com->full, ")");
		}

		com->P = compute_period (com->q, com->e);
		com->T = greg_to_jul (com->y, com->m, (int)com->d);
		com->sort = get_sort_key(com->ID);
		line++;
		Frame2->ProgressBar1->Position = i+1;

		if(Frame1->CheckBox1->Checked){
			if(do_exclude(com) || check_name(com)){
				N--;
				i--;
				Frame2->ProgressBar1->Max--;
				delete com;
				continue;
			}
		}

		addCmt(&cmt, com);
		delete com;
	}

	return N;
}

int TForm1::import_xephem (int N, FILE *fin){

	//info: http://www.clearskyinstitute.com/xephem/help/xephem.html#mozTocId215848

	int j, k, l;
	int mm, dd, hh, yy;
	long int T;
	int nula, m, line=2;
	float smAxis, mdMotion, mAnomaly;
	char c, x[25+1];

	for (int i=0; i<N; i++) {

		Comet *com = new Comet;

		try{
			fscanf(fin, "# From %25[^\n]%*c", x);
		}
		catch(...){
			return 0;
		}

		j=0;
		while ((c=fgetc(fin)) != ',' ){
			com->full[j++]=c;
		}
        com->full[j]='\0';

		editFullIdName(com->full, com->ID, com->name, 1);

		c=fgetc(fin);

		if(c == 'e'){
			m = fscanf(fin, ",%f,%f,%f,%f,%f,%f,%f,%d/%d.%d/%d,2000,g %f,%f\n",
				&com->i, &com->an, &com->pn, &smAxis,
				&mdMotion, &com->e, &mAnomaly, &mm, &dd,
				&hh, &yy, &com->H, &com->G);

			if (m < 13){
				//cout << "\n\n  Unable to read data in line " << line;
				fscanf(fin, "%*[^\n]\n");
				N--; i--; line++;
				Frame2->ProgressBar1->Max--;
				delete com;
				continue;
			}

			com->q = smAxis*(1-com->e);
			T = greg_to_jul (yy, mm, dd);

			if(mAnomaly==0) mAnomaly = 0.00000001;
			if(mdMotion==0) mdMotion = 0.00000001;

			long double TT = (long double)T - mAnomaly/mdMotion;
			com->T = (long int)TT;

			jul_to_greg(com->T, com->y, com->m, com->d);
			com->d += (float)hh/10000;
			com->d += TT-(long int)TT;

			line+=2;
			Frame2->ProgressBar1->Position = i+1;

			if(Frame1->CheckBox1->Checked){
				if(do_exclude(com) || check_name(com)){
					N--;
					i--;
					Frame2->ProgressBar1->Max--;
					delete com;
					continue;
				}
			}
		}

		if(c == 'p'){
			m = fscanf(fin, ",%d/%f/%d,%f,%f,%f,%f,2000,%f,%f\n",
				&com->m, &com->d, &com->y,
				&com->i, &com->pn, &com->q, &com->an,
				&com->H, &com->G);

			if (m < 9){
				//cout << "\n\n  Unable to read data in line " << line;
				fscanf(fin, "%*[^\n]\n");
				N--; i--; line++;
				Frame2->ProgressBar1->Max--;
				delete com;
				continue;
			}

			com->e = 1.000000;
			com->T = greg_to_jul (com->y, com->m, (int)com->d);
			line+=2;
			Frame2->ProgressBar1->Position = i+1;

			if(Frame1->CheckBox1->Checked){
				if(do_exclude(com) || check_name(com)){
					N--;
					i--;
					Frame2->ProgressBar1->Max--;
					delete com;
					continue;
				}
			}
		}

		if(c == 'h'){
			m = fscanf(fin, ",%d/%f/%d,%f,%f,%f,%f,%f,2000,%f,%f\n",
				&com->m, &com->d, &com->y,
				&com->i, &com->an, &com->pn, &com->e,
				&com->q, &com->H, &com->G);

			if (m < 10){
				//cout << "\n\n  Unable to read data in line " << line;
				fscanf(fin, "%*[^\n]\n");
				N--; i--; line++;
				Frame2->ProgressBar1->Max--;
				delete com;
				continue;
			}

			com->T = greg_to_jul (com->y, com->m, (int)com->d);
			line+=2;
			Frame2->ProgressBar1->Position = i+1;

			if(Frame1->CheckBox1->Checked){
				if(do_exclude(com) || check_name(com)){
					N--;
					i--;
					Frame2->ProgressBar1->Max--;
					delete com;
					continue;
				}
			}
		}

		com->P = compute_period (com->q, com->e);
		com->sort = get_sort_key(com->ID);

		addCmt(&cmt, com);
		delete com;
	}

	return N;
}

int TForm1::import_home_planet (int N, FILE *fin){

	int j, k, l;
	int m, line=1;
	char c, x[50+1];

	fscanf(fin, "%*[^\n]\n");

	for (int i=0; i<N; i++) {

		Comet *com = new Comet;

		try{
			j=0;
			while ((c=fgetc(fin)) != ',' ){
				com->full[j++]=c;
			}
			com->full[j]='\0';
		}
		catch(...){
			return 0;
		}

		m = fscanf(fin, "%d-%d-%f,%f,%f,%f,%f,%f,%50[^\n]%*c",
			&com->y, &com->m, &com->d,
			&com->q, &com->e, &com->pn, &com->an,
			&com->i, x);

		if (m < 9){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame2->ProgressBar1->Max--;
			delete com;
			continue;
		}

		editFullIdName(com->full, com->ID, com->name, 1);

		com->P = compute_period (com->q, com->e);
		com->T = greg_to_jul (com->y, com->m, (int)com->d);
		com->sort = get_sort_key(com->ID);
		line++;
		Frame2->ProgressBar1->Position = i+1;

		if(Frame1->CheckBox1->Checked){
			if(do_exclude(com) || check_name(com)){
				N--;
				i--;
				Frame2->ProgressBar1->Max--;
				delete com;
				continue;
			}
		}

		addCmt(&cmt, com);
		delete com;
	}

	return N;
}

int TForm1::import_mystars (int N, FILE *fin){

	int j, k, l;
	int m, line=1;
	char c, x[30+1];
	float TT;

	fscanf(fin, "%*[^\n]\n");

	for (int i=0; i<N; i++) {

		Comet *com = new Comet;

		j=0;
		while ((c=fgetc(fin)) != ';' ){
			com->full[j++]=c;
		}

		m = fscanf(fin, "%f %f %f %f %f %f %f %f %30[^\n]%*c",
			&TT, &com->pn, &com->e,
			&com->q, &com->i, &com->an, &com->H,
			&com->G, x);

		if (m < 9){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame2->ProgressBar1->Max--;
			delete com;
			continue;
		}

		editFullIdName(com->full, com->ID, com->name, 1);

		com->P = compute_period (com->q, com->e);
		com->T = (int)TT + 2400000;
		com->sort = get_sort_key(com->ID);
		line++;
		Frame2->ProgressBar1->Position = i+1;

		jul_to_greg(com->T, com->y, com->m, com->d);
		com->d += TT - (int)TT;

		if(Frame1->CheckBox1->Checked){
			if(do_exclude(com) || check_name(com)){
				N--;
				i--;
				Frame2->ProgressBar1->Max--;
				delete com;
				continue;
			}
		}

		addCmt(&cmt, com);
		delete com;
	}

	return N;
}

int TForm1::import_thesky (int N, FILE *fin){

	int j, k, l;
	int m, line=1;
	float G;
	char x[20+1];

	for (int i=0; i<N; i++) {

		Comet *com = new Comet;

//		fscanf(fin, "%40c %*c %d %*c %4d %2d %2d %*c %d %*c %f %*c %f %*c %f %*c %f %*c %f %*c %f %25[^\n]%*c",     stari nacin
		m = fscanf(fin, "%45c %4d%2d%f | %f | %f | %f | %f | %f | %f | %f %20[^\n]%*c",
			com->full, &com->y, &com->m,
			&com->d, &com->q, &com->e,
			&com->pn, &com->an, &com->i, &com->H,
			&G, x);

		com->G = G/2.5;

		if (m < 12){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame2->ProgressBar1->Max--;
			delete com;
			continue;
		}

		remove_spaces(com->full);

		editFullIdName(com->full, com->ID, com->name, 1);

		com->P = compute_period (com->q, com->e);
		com->T = greg_to_jul (com->y, com->m, (int)com->d);
		remove_spaces (com->name);
		com->sort = get_sort_key(com->ID);
		line++;
		Frame2->ProgressBar1->Position = i+1;

		if(Frame1->CheckBox1->Checked){
			if(do_exclude(com) || check_name(com)){
				N--;
				i--;
				Frame2->ProgressBar1->Max--;
				delete com;
				continue;
			}
		}

		addCmt(&cmt, com);
		delete com;
	}

	return N;
}

int TForm1::import_starry_night (int N, FILE *fin){

	int j, k, h;
	int m, line=1;
	char c;
	float G;
	long int y;

	for (int i=0; i<10; i++) fscanf(fin, "%*[^\n]\n");
	// u prvih 15 redova nema nikakvih podataka
	// ali je u samo 10 redova tekst, a 5 redova je prazno

	for (int i=0; i<N; i++) {

		Comet *com = new Comet;

		m = fscanf(fin, "     %29c %f 0.0 %f %f %f %f %f %ld.%d %ld.5 %f",
			com->name, &com->H, &com->e, &com->q, &com->an,
			&com->pn, &com->i, &com->T, &h,
			&y, &G);

		if (m < 11){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame2->ProgressBar1->Max--;
			delete com;
			continue;
		}

		com->G = G/2.5;

		j=0;
		fgetc(fin);
		fgetc(fin);
		while(j<13){
			c = fgetc(fin);
			com->ID[j++]=c;
		}
		com->ID[13] = '\0';

		fscanf(fin, "%*[^\n]\n");

		remove_spaces(com->name);
		remove_spaces(com->ID);

		if ((com->ID[0]=='C' && com->ID[1]=='/') ||
			(com->ID[0]=='P' && com->ID[1]=='/')){

			strcpy(com->full, com->ID);
			strcat(com->full, " (");
			strcat(com->full, com->name);
			strcat(com->full, ")");
		}

		else {
			strcpy(com->full, com->ID);
			strcat(com->full, "/");
			strcat(com->full, com->name);
		}

		com->P = compute_period (com->q, com->e);
		com->sort = get_sort_key(com->ID);
		line++;
		Frame2->ProgressBar1->Position = i+1;

		jul_to_greg(com->T, com->y, com->m, com->d);
		com->d += (float)h/10000;
		//com->d += TT - (int)TT;

		if(Frame1->CheckBox1->Checked){
			if(do_exclude(com) || check_name(com)){
				N--;
				i--;
				Frame2->ProgressBar1->Max--;
				delete com;
				continue;
			}
		}

		addCmt(&cmt, com);
		delete com;
	}

	return N;
}

int TForm1::import_deep_space (int N, FILE *fin){

	int j, m, line=2;
	float G;
	char c, x[8+1];

	fscanf(fin, "%*[^\n]\n");
	fscanf(fin, "%*[^\n]\n");

	for (int i=0; i<N; i++) {

		Comet *com = new Comet;

		j=0;
		while ((c=fgetc(fin)) != '(' ){
			com->name[j++]=c;
		}
		com->name[j-1]='\0';

		j=0;
		while ((c=fgetc(fin)) != ')' ){
			com->ID[j++]=c;
		}
		com->ID[j]='\0';

		m = fscanf(fin, "\n%8c %d %d %f %f %f %f %f %f %f %f\n",
			x, &com->y, &com->m, &com->d,
			&com->q, &com->e, &com->pn, &com->an,
			&com->i, &com->H, &G);

		com->G = G/2.5;

		if (m < 11){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame2->ProgressBar1->Max--;
			delete com;
			continue;
		}

		if ((com->ID[0]=='C' && com->ID[1]=='/') ||
			(com->ID[0]=='P' && com->ID[1]=='/')){

			strcpy(com->full, com->ID);
			strcat(com->full, " (");
			strcat(com->full, com->name);
			strcat(com->full, ")");
		}

		else {
			strcpy(com->full, com->ID);
			strcat(com->full, "/");
			strcat(com->full, com->name);
		}

		com->P = compute_period (com->q, com->e);
		com->T = greg_to_jul (com->y, com->m, (int)com->d);
		com->sort = get_sort_key(com->ID);
		line+=2;
		Frame2->ProgressBar1->Position = i+1;

		if(Frame1->CheckBox1->Checked){
			if(do_exclude(com) || check_name(com)){
				N--;
				i--;
				Frame2->ProgressBar1->Max--;
				delete com;
				continue;
			}
		}

		addCmt(&cmt, com);
		delete com;
	}

	return N;
}

int TForm1::import_pc_tcs (int N, FILE *fin){

	int j, k;
	int m, line=1, len;
	float G;
	char tempID[20];

	for (int i=0; i<N; i++) {

		Comet *com = new Comet;

		m = fscanf(fin, "%s %f %f %f %f %f %d %d %f %f %f %55[^\n]%*c",
			com->ID, &com->q, &com->e, &com->i,
			&com->pn, &com->an, &com->y, &com->m,
			&com->d, &com->H, &G, com->name);

		com->G = G/2.5;

		if (m < 12){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame2->ProgressBar1->Max--;
			delete com;
			continue;
		}

		for (j=0; j<20; j++) tempID[j]='\0';

		if ((com->ID[0]=='C' && com->ID[1]=='/') ||
			(com->ID[0]=='P' && com->ID[1]=='/')){

			int p=2;
			while(isdigit(com->ID[p])) p++;
			//da pronade prvo mjesto gdje je slovo
			//pa da od tog mjesta krene kopirati
			for(j=p, k=0; com->ID[j]!='\0'; j++, k++)
				tempID[k]=com->ID[j];

			len = strlen(com->ID);
			for(j=6; j<len; j++)
				com->ID[j]=' ';

			remove_spaces(com->ID);

			strcat(com->ID, " ");
			strcat(com->ID, tempID);
		}

		remove_spaces (com->name);

		if ((com->ID[0]=='C' && com->ID[1]=='/') ||
			(com->ID[0]=='P' && com->ID[1]=='/')){

			strcpy(com->full, com->ID);
			strcat(com->full, " (");
			strcat(com->full, com->name);
			strcat(com->full, ")");
		}

		else {
			strcpy(com->full, com->ID);
			strcat(com->full, "/");
			strcat(com->full, com->name);
		}

		com->P = compute_period (com->q, com->e);
		com->T = greg_to_jul (com->y, com->m, (int)com->d);
		com->sort = get_sort_key(com->ID);
		line++;
		Frame2->ProgressBar1->Position = i+1;

		if(Frame1->CheckBox1->Checked){
			if(do_exclude(com) || check_name(com)){
				N--;
				i--;
				Frame2->ProgressBar1->Max--;
				delete com;
				continue;
			}
		}

		addCmt(&cmt, com);
		delete com;
	}

	return N;
}

int TForm1::import_ecu (int N, FILE *fin){

	int j, k, l;
	float G;
	int m, line=2;

	for (int i=0; i<N; i++) {

		Comet *com = new Comet;

		m = fscanf(fin, "%45[^\n]%*cE C 2000 %d %d %f %f %f %f %f %f %f %f\n",
			com->full, &com->y, &com->m, &com->d,
			&com->q, &com->e, &com->pn, &com->an,
			&com->i, &com->H, &G);

		com->G = G/2.5;

		if (m < 11){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame2->ProgressBar1->Max--;
			delete com;
			continue;
		}

		remove_spaces(com->full);

		editFullIdName(com->full, com->ID, com->name, 1);

		com->P = compute_period (com->q, com->e);
		com->T = greg_to_jul (com->y, com->m, (int)com->d);
		com->sort = get_sort_key(com->ID);
		line+=2;
		Frame2->ProgressBar1->Position = i+1;

		if(Frame1->CheckBox1->Checked){
			if(do_exclude(com) || check_name(com)){
				N--;
				i--;
				Frame2->ProgressBar1->Max--;
				delete com;
				continue;
			}
		}

		addCmt(&cmt, com);
		delete com;
	}

	return N;
}

int TForm1::import_dance (int N, FILE *fin){

	int j, k;
	int m, line=1, len;
	char tempID[20];

	for (int i=0; i<5; i++) fscanf(fin, "%*[^\n]\n");

	for (int i=0; i<N; i++) {

		Comet *com = new Comet;

		m = fscanf(fin, "%11c %f %f %f %f %f %d.%2d%6f %30[^\n]%*c",
			com->ID, &com->q, &com->e, &com->i,
			&com->an, &com->pn, &com->y, &com->m,
			&com->d, com->name);

		if (m < 10){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame2->ProgressBar1->Max--;
			delete com;
			continue;
		}

		remove_spaces (com->ID);
		remove_spaces (com->name);

		for (j=0; j<20; j++) tempID[j]='\0';

		if ((com->ID[0]=='C' && com->ID[1]=='/') ||
			(com->ID[0]=='P' && com->ID[1]=='/')){

			for(j=6, k=0; com->ID[j]!='\0'; j++, k++)
				tempID[k]=com->ID[j];

			len = strlen(com->ID);
			for(j=6; j<len; j++) com->ID[j]=' ';

			remove_spaces(com->ID);

			strcat(com->ID, " ");
			strcat(com->ID, tempID);
		}

		if ((com->ID[0]=='C' && com->ID[1]=='/') ||
			(com->ID[0]=='P' && com->ID[1]=='/')){

			strcpy(com->full, com->ID);
			strcat(com->full, " (");
			strcat(com->full, com->name);
			strcat(com->full, ")");
		}

		else {
			strcpy(com->full, com->ID);
			strcat(com->full, "/");
			strcat(com->full, com->name);
		}

		com->P = compute_period (com->q, com->e);
		com->d /= 10000;
		com->T = greg_to_jul (com->y, com->m, (int)com->d);
		com->sort = get_sort_key(com->ID);
		line++;
		Frame2->ProgressBar1->Position = i+1;

		if(Frame1->CheckBox1->Checked){
			if(do_exclude(com) || check_name(com)){
				N--;
				i--;
				Frame2->ProgressBar1->Max--;
				delete com;
				continue;
			}
		}

		addCmt(&cmt, com);
		delete com;
	}

	return N;
}

int TForm1::import_megastar (int N, FILE *fin){

	int m, line=1;
	float G;
	char x[25+1];

	for (int i=0; i<N; i++) {

		Comet *com = new Comet;

		m = fscanf(fin, "%30c %12c %d %d %f %f %f %f %f %f %f %f %25[^\n]%*c",
			com->name, com->ID, &com->y, &com->m, &com->d,
			&com->q, &com->e, &com->pn,
			&com->an, &com->i, &com->H, &G, x);

		com->G = G/2.5;

		if (m < 13){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame2->ProgressBar1->Max--;
			delete com;
			continue;
		}

		remove_spaces (com->ID);
		remove_spaces (com->name);

		if ((com->ID[0]=='C' && com->ID[1]=='/') ||
			(com->ID[0]=='P' && com->ID[1]=='/')){

			strcpy(com->full, com->ID);
			strcat(com->full, " (");
			strcat(com->full, com->name);
			strcat(com->full, ")");
		}

		else {
			strcpy(com->full, com->ID);
			strcat(com->full, "/");
			strcat(com->full, com->name);
		}

		com->P = compute_period (com->q, com->e);
		com->T = greg_to_jul (com->y, com->m, com->d);
		com->sort = get_sort_key(com->ID);
		line++;
		Frame2->ProgressBar1->Position = i+1;

		if(Frame1->CheckBox1->Checked){
			if(do_exclude(com) || check_name(com)){
				N--;
				i--;
				Frame2->ProgressBar1->Max--;
				delete com;
				continue;
			}
		}

		addCmt(&cmt, com);
		delete com;
	}

	return N;
}

int TForm1::import_skychart (int N, FILE *fin){

	int j, k, l;
	int m, line=1;
	char c;

	for (int i=0; i<N; i++) {

		Comet *com = new Comet;

		m = fscanf(fin, "P11 2000.0 -%f %f %f %f %f 0 %d/%d/%f %f %f 0 0 ",
			&com->q, &com->e, &com->i, &com->pn,
			&com->an, &com->y, &com->m, &com->d,
			&com->H, &com->G);

		if (m < 10){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame2->ProgressBar1->Max--;
			delete com;
			continue;
		}

		j=0;
		while ((c=fgetc(fin)) != ';' ){
			com->full[j++]=c;
		}
		com->full[j]='\0';

		fscanf(fin, "%*[^\n]\n");		//za izostavi ono na kraju

		editFullIdName(com->full, com->ID, com->name, 1);

		com->P = compute_period (com->q, com->e);
		com->T = greg_to_jul (com->y, com->m, (int)com->d);
		com->sort = get_sort_key(com->ID);
		line++;
		Frame2->ProgressBar1->Position = i+1;

		if(Frame1->CheckBox1->Checked){
			if(do_exclude(com) || check_name(com)){
				N--;
				i--;
				Frame2->ProgressBar1->Max--;
				delete com;
				continue;
			}
		}

		addCmt(&cmt, com);
		delete com;
	}

	return N;
}

int TForm1::import_voyager (int N, FILE *fin){

	int m, line=1;
	char mj[3+1];

	for (int i=0; i<18; i++) fscanf(fin, "%*[^\n]\n");

	for (int i=0; i<N; i++) {

		Comet *com = new Comet;

		m = fscanf(fin, "%27c %f %f %f %f %f %f %4d %3c %f 2000.0\n",
			com->name, &com->q, &com->e, &com->i,
			&com->an, &com->pn, &com->G, &com->y,
			mj, &com->d);

		if (m < 10){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame2->ProgressBar1->Max--;
			delete com;
			continue;
		}

		strcpy(com->full, com->name); 		//posto nema pravog full-a, name ce bit kao full

		if (mj[0]=='J' && mj[1]=='a' && mj[2]=='n') com->m=1;
		if (mj[0]=='F' && mj[1]=='e' && mj[2]=='b') com->m=2;
		if (mj[0]=='M' && mj[1]=='a' && mj[2]=='r') com->m=3;
		if (mj[0]=='A' && mj[1]=='p' && mj[2]=='r') com->m=4;
		if (mj[0]=='M' && mj[1]=='a' && mj[2]=='y') com->m=5;
		if (mj[0]=='J' && mj[1]=='u' && mj[2]=='n') com->m=6;
		if (mj[0]=='J' && mj[1]=='u' && mj[2]=='l') com->m=7;
		if (mj[0]=='A' && mj[1]=='u' && mj[2]=='g') com->m=8;
		if (mj[0]=='S' && mj[1]=='e' && mj[2]=='p') com->m=9;
		if (mj[0]=='O' && mj[1]=='c' && mj[2]=='t') com->m=10;
		if (mj[0]=='N' && mj[1]=='o' && mj[2]=='v') com->m=11;
		if (mj[0]=='D' && mj[1]=='e' && mj[2]=='c') com->m=12;

		com->P = compute_period (com->q, com->e);
		com->T = greg_to_jul (com->y, com->m, (int)com->d);
		remove_spaces (com->name);
//		com->sort = get_sort_key(com->ID);
		line++;
		Frame2->ProgressBar1->Position = i+1;

		if(Frame1->CheckBox1->Checked){
			if(do_exclude(com) || check_name(com)){
				N--;
				i--;
				Frame2->ProgressBar1->Max--;
				delete com;
				continue;
			}
		}

		addCmt(&cmt, com);
		delete com;
	}

	return N;
}

int TForm1::import_skytools (int N, FILE *fin){

	int j, k, l, u, t=0, space;
	int yy, mm, dd;
	int m, line=1, len;
	char x[15+1];

	for (int i=0; i<N; i++) {

		Comet *com = new Comet;

		m = fscanf(fin, "C %40c %d %d %d %d %d %f %f %f %f %f %f %f %f 0.002000 %15[^\n]%*c",
			com->full, &yy, &mm, &dd, &com->y, &com->m, &com->d,
			&com->q, &com->e, &com->pn, &com->an, &com->i,
			&com->H, &com->G, x);

		//com->h*=10;

		if (m < 15){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame2->ProgressBar1->Max--;
			delete com;
			continue;
		}

		remove_spaces(com->full);

		for (j=0; com->full[j]!='\0'; j++){
			if (isdigit(com->full[j]) && com->full[j+1]=='P' && com->full[j+2]=='/'){

				for(k=0; com->full[k]!='/'; k++)
					com->ID[k]=com->full[k];

				com->ID[k]='\0';
				++k;
				for(l=0; com->full[k]!='\0'; l++, k++)
					com->name[l]=com->full[k];

				com->name[l]='\0';
			}

		if ((com->full[0]=='C' && com->full[1]=='/') ||
			(com->full[0]=='P' && com->full[1]=='/')){
				space=0;
				len = strlen(com->full);
				for(u=0; u<len; u++){
					if (com->full[u]==' ' && space==1) {
						t=u;
						break;
					}
					else if(com->full[u]==' ') space++;
				}

				for(k=0; k<t; k++)
					com->ID[k]=com->full[k];

				com->ID[k]='\0';

				++k;
				for(l=0; com->full[k]!='\0'; k++, l++)
					com->name[l]=com->full[k];

				com->name[l]='\0';
			}
		}

		com->P = compute_period (com->q, com->e);
		com->T = greg_to_jul (com->y, com->m, (int)com->d);
		com->sort = get_sort_key(com->ID);
		line++;
		Frame2->ProgressBar1->Position = i+1;

		if(Frame1->CheckBox1->Checked){
			if(do_exclude(com) || check_name(com)){
				N--;
				i--;
				Frame2->ProgressBar1->Max--;
				delete com;
				continue;
			}
		}

		addCmt(&cmt, com);
		delete com;
	}

	return N;
}

int TForm1::import_cfw (int N, FILE *fin){

	int j, k, l;
	float G;

	//for (int i=0; i<7; i++) fscanf(fin, "%*[^\n]\n");

	fscanf(fin, "\n");
	fscanf(fin, "[File]\n");
	fscanf(fin, "group=Comets\n");
	fscanf(fin, "\n");
	fscanf(fin, "\n");
	fscanf(fin, "[Data]\n");
	fscanf(fin, "\n");

	for (int i=0; i<N; i++){

		Comet *com = new Comet;

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
					com->full,
					&com->y, &com->m, &com->d,
					&com->q,
					&com->e,
					&com->pn,
					&com->an,
					&com->i,
					&com->H, &G);

		com->G = G/2.5;

		remove_spaces(com->full);

		editFullIdName(com->full, com->ID, com->name, 1);

		com->P = compute_period (com->q, com->e);
		com->T = greg_to_jul (com->y, com->m, (int)com->d);
		com->sort = get_sort_key(com->ID);
		Frame2->ProgressBar1->Position = i+1;

		if(Frame1->CheckBox1->Checked){
			if(do_exclude(com) || check_name(com)){
				N--;
				i--;
				Frame2->ProgressBar1->Max--;
				delete com;
				continue;
			}
		}

		addCmt(&cmt, com);
		delete com;
	}

	return N;
}

int TForm1::import_nasa (int N, FILE *fin){

	int j, k, l;
	int m, line=1, len, trash;
	char c, q, x[20+1];

	fscanf(fin, "%*[^\n]\n");
	fscanf(fin, "%*[^\n]\n");

	for (int i=0; i<N; i++) {

		Comet *com = new Comet;

		k=0; j=0;
		while (k<44){
			c=fgetc(fin);
			com->full[j]=c;
			if (c==' ' && j==0) --j;
			j++;
			k++;
		}

		remove_spaces(com->full);

		if (Frame2->CheckBox8->Checked){
			bool skip = false;
			len = strlen(com->full);
			for (j=0; j<len; j++){
				if (com->full[j  ]=='S' && com->full[j+1]=='O' &&
					com->full[j+2]=='H' && com->full[j+3]=='O'
				 && com->full[0  ]=='C'
					){

					N--; i--;
					Frame2->ProgressBar1->Max--;
					skip = true;
					break;
				}
			}
			if(skip){
				fscanf(fin, "%*[^\n]\n");
				delete com;
				continue;
			}
		}

		for (j=0; com->full[j]!='\0'; j++){
			if ((isdigit(com->full[j]) && com->full[j+1]=='P' && com->full[j+2]=='/') ||
				(isdigit(com->full[j]) && com->full[j+1]=='D' && com->full[j+2]=='/')){

				for(k=0; com->full[k]!='/'; k++)
					com->ID[k]=com->full[k];

				com->ID[k]='\0';
				++k;
				for(l=0; com->full[k]!='\0'; l++, k++)
					com->name[l]=com->full[k];

				com->name[l]='\0';
			}

			if (com->full[j]=='('){
				for(k=0; com->full[k]!='('; k++)
					com->ID[k]=com->full[k];

				com->ID[k-1]='\0';

				++k;
				for(l=0; com->full[k]!=')'; k++, l++)
					com->name[l]=com->full[k];

				com->name[l]='\0';
			}
		}

		m = fscanf(fin, "%d %f %f %f %f %f %4d%2d%f %20[^\n]%*c",
			&trash, &com->q, &com->e, &com->i,
			&com->pn, &com->an, &com->y, &com->m,
			&com->d, x);

		if (m < 10){
			//cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			Frame2->ProgressBar1->Max--;
			delete com;
			continue;
		}

		com->P = compute_period (com->q, com->e);
		com->T = greg_to_jul (com->y, com->m, com->d);
		com->sort = get_sort_key(com->ID);
		Frame2->ProgressBar1->Position = i+1;
		//Application->ProcessMessages();
		line++;

		if(Frame1->CheckBox1->Checked){
			if(do_exclude(com) || check_name(com)){
				N--;
				i--;
				Frame2->ProgressBar1->Max--;
				delete com;
				continue;
			}
		}

		addCmt(&cmt, com);
		delete com;
	}

	return N;
}


void TForm1::export_mpc (Comet *cmt, FILE *fout){

	fprintf(fout,"              %4d %02d %07.4f %9f  %.6f  %8.4f  %8.4f  %8.4f  %4d%02d%02d  %4.1f %4.1f  %-56s MPC 00000\n",
			cmt->y, cmt->m, cmt->d, cmt->q, cmt->e,
			cmt->pn, cmt->an, cmt->i, ep_y, ep_m, ep_d, cmt->H, cmt->G, cmt->full);

	Frame4->ProgressBar1->Position += 1;
}

void TForm1::export_skymap (Comet *cmt, FILE *fout){

	int j, k, len;

	k=0;
	len = strlen(cmt->ID);
	for(j=0; j<len; j++){
		fputc(cmt->ID[j], fout);
		k++;
	}
	fputc(' ', fout); k++;
	len = strlen(cmt->name);
	for(j=0; j<len; j++){
		fputc(cmt->name[j], fout);
		k++;
	}
	while(k!=47){
		fputc(' ', fout); k++;
	}

	fprintf(fout,"%4d %02d %07.4f %9f       %.6f %8.4f %8.4f %8.4f  %4.1f  %4.1f\n",
			cmt->y, cmt->m, cmt->d, cmt->q,
			cmt->e, cmt->pn, cmt->an, cmt->i, cmt->H, cmt->G);

	Frame4->ProgressBar1->Position += 1;
}

void TForm1::export_guide (Comet *cmt, FILE *fout){

	int j, k, len;

	k=0;
	len = strlen(cmt->ID);
	if (cmt->ID[len-1]=='P' && isdigit(cmt->ID[len-2])){
		fputc('P', fout); k++;
		fputc('/', fout); k++;
	}

	if (cmt->ID[len-1]=='D' && isdigit(cmt->ID[len-2])){
		fputc('D', fout); k++;
		fputc('/', fout); k++;
	}

	len = strlen(cmt->name);
	for(j=0; j<len; j++){
		fputc(cmt->name[j], fout);
		k++;
	}
	fputc(' ', fout); k++;
	fputc('(', fout); k++;

	len = strlen(cmt->ID);
	for(j=0; j<len; j++){
		fputc(cmt->ID[j], fout);
		k++;
	}
	fputc(')', fout); k++;
	k++;

	while(k!=44){
		fputc(' ', fout); k++;
	}

	fprintf(fout,"%7.4f  %2d  %4d  0.0        %9.6f    %.6f  %8.4f    %8.4f    %8.4f    %d.0   %4.1f %4.1f    MPC 00000\n",
			cmt->d, cmt->m, cmt->y, cmt->q, cmt->e,
			cmt->i, cmt->pn, cmt->an, equinox, cmt->H, cmt->G);

	Frame4->ProgressBar1->Position += 1;
}

void TForm1::export_xephem (Comet *cmt, FILE *fout){

	//info: http://www.clearskyinstitute.com/xephem/help/xephem.html#mozTocId215848

	fprintf(fout,"# From MPC 00000\n%s,", cmt->full);

	if(cmt->e < 1.0){

		double smAxis = cmt->q/(1-cmt->e);
		double mdMotion = 0.9856076686/cmt->P;
		double mAnomaly = -(mdMotion * (cmt->T - greg_to_jul(ep_y, ep_m, ep_d)));

		if (mAnomaly <   0) mAnomaly+=360;
		if (mAnomaly > 360) mAnomaly-=360;

		fprintf(fout, "e,%.4f,%.4f,%.4f,%.6f,%.7f,%.8f,%.4f,%02d/%02d.0/%d,%d,g %4.1f,%.1f\n",
			cmt->i, cmt->an, cmt->pn, smAxis, mdMotion, cmt->e,
			mAnomaly, ep_m, ep_d, ep_y, equinox, cmt->H, cmt->G);
	}

	if(cmt->e == 1.0){

		fprintf(fout, "p,%02d/%06.3f/%4d,%.3f,%.3f,%.5f,%.3f,2000,%.1f,%.1f\n",
			cmt->m, cmt->d, cmt->y,
			cmt->i, cmt->pn, cmt->q, cmt->an,
			cmt->H, cmt->G);
	}

	if(cmt->e > 1.0){

		fprintf(fout, "h,%02d/%07.4f/%4d,%.4f,%.4f,%.4f,%.6f,%.6f,2000,%.1f,%.1f\n",
			cmt->m, cmt->d, cmt->y,
			cmt->i, cmt->an, cmt->pn, cmt->e,
			cmt->q, cmt->H, cmt->G);
	}

	Frame4->ProgressBar1->Position += 1;
}

void TForm1::export_home_planet (Comet *cmt, FILE *fout){

	double smAxis = cmt->q/((1-cmt->e)+0.000001);

	fprintf(fout,"%s,%d-%d-%7.4f,%.6f,%.6f,%.4f,%.4f,%.4f,%.5f,%.5f years, MPC      \n",
			cmt->full, cmt->y, cmt->m, cmt->d, cmt->q,
			cmt->e, cmt->pn, cmt->an, cmt->i, smAxis, cmt->P);

	Frame4->ProgressBar1->Position += 1;
}

void TForm1::export_mystars (Comet *cmt, FILE *fout){

	int h = (cmt->d - (int)cmt->d)*10000;

	fprintf(fout,"%s;\t%ld.%04d\t%.4f\t%.6f\t%.6f\t%.4f\t%.4f\t%.1f\t%.1f\tMPC00000\t%ld.0\n",
			cmt->full, cmt->T-2400000, h, cmt->pn, cmt->e, cmt->q,
			cmt->i, cmt->an, cmt->H, cmt->G, eq_JD-2400000);

	Frame4->ProgressBar1->Position += 1;
}

void TForm1::export_thesky (Comet *cmt, FILE *fout){

	fprintf(fout,"%-39s|%d|%4d%02d%07.4f |%9f |%.6f |%8.4f |%8.4f |%8.4f |%4.1f |%4.1f | MPC 00000\n",
			cmt->full, equinox, cmt->y, cmt->m, cmt->d, cmt->q,
			cmt->e, cmt->pn, cmt->an, cmt->i, cmt->H, cmt->G*2.5);

	Frame4->ProgressBar1->Position += 1;
}

void TForm1::export_starry_night (Comet *cmt, FILE *fout){

	int h = (cmt->d - (int)cmt->d)*10000;

	fprintf(fout,"     %-29s %4.1f    0.0   %.6f   %9.6f    %8.4f  %8.4f  %8.4f  %ld.%04d    %ld.5  %4.1f  %-13s MPC 00000\n",
			cmt->name, cmt->H, cmt->e, cmt->q, cmt->an, cmt->pn,
			cmt->i, cmt->T, h, eq_JD, cmt->G*2.5, cmt->ID);

	Frame4->ProgressBar1->Position += 1;
}

void TForm1::export_deep_space (Comet *cmt, FILE *fout){

	fprintf(fout,"%s (%s)\nC J%d %4d %02d %07.4f %.6f %.6f %.4f %.4f %.4f %.1f %.1f\n",
			cmt->name, cmt->ID, equinox, cmt->y, cmt->m, cmt->d,
			cmt->q, cmt->e, cmt->pn, cmt->an, cmt->i, cmt->H, cmt->G*2.5);

	Frame4->ProgressBar1->Position += 1;
}

void TForm1::export_pc_tcs (Comet *cmt, FILE *fout){

	int j, k, len;

	len = strlen(cmt->ID);
	char *ID;
	ID = (char*) malloc ((len+1) * sizeof(char));
	strcpy(ID, cmt->ID);

	for (j=0; j<len; j++){
		if (ID[j]==' '){
			k=j;
			for( ; ID[k]!='\0'; k++)
				ID[k]=ID[k+1];
		}
	}

	fprintf(fout,"%s %.6f %.6f %.4f %.4f %.4f %4d %02d %07.4f %.1f %.1f %s\n",
			ID, cmt->q, cmt->e, cmt->i, cmt->pn, cmt->an,
			cmt->y, cmt->m, cmt->d, cmt->H, cmt->G*2.5, cmt->name);

	Frame4->ProgressBar1->Position += 1;

	free (ID);
}

void TForm1::export_ecu (Comet *cmt, FILE *fout){

	fprintf(fout,"%s\nE C %d %4d %02d %07.4f %.6f %.6f %.4f %.4f %.4f %.1f %.1f\n",
			cmt->full, equinox, cmt->y, cmt->m, cmt->d, cmt->q,
			cmt->e, cmt->pn, cmt->an, cmt->i, cmt->H, cmt->G*2.5);

	Frame4->ProgressBar1->Position += 1;
}

void TForm1::export_dance (Comet *cmt, FILE *fout){

	int j, k, len;

	len = strlen(cmt->ID);
	char *ID;
	ID = (char*) malloc ((len+1) * sizeof(char));
	strcpy(ID, cmt->ID);

	for (j=0; j<len; j++){
		if (ID[j]==' '){
			k=j;
			for( ; ID[k]!='\0'; k++)
				ID[k]=ID[k+1];
		}
	}

	fprintf(fout,"%-11s",ID);

	if(cmt->q >= 10.0){
		fprintf(fout, "******** ");
	}
	else{
		fprintf(fout, "%.6f ", cmt->q);
	}

	fprintf(fout,"%.6f %8.4f %8.4f %8.4f %4d.%02d%06d %s\n",
			cmt->e, cmt->i, cmt->pn, cmt->an, cmt->y,
			cmt->m, (int)(cmt->d*10000), cmt->name);


	Frame4->ProgressBar1->Position += 1;

	free (ID);
}

void TForm1::export_megastar (Comet *cmt, FILE *fout){

	fprintf(fout,"%-30s%-12s%4d %02d  %07.4f   %9.6f   %.6f    %8.4f    %8.4f    %8.4f   %4.1f   %4.1f    %d MPC 00000\n",
			cmt->name, cmt->ID, cmt->y, cmt->m, cmt->d,
			cmt->q, cmt->e, cmt->pn, cmt->an,
			cmt->i, cmt->H, cmt->G*2.5, equinox);

	Frame4->ProgressBar1->Position += 1;
}

void TForm1::export_skychart (Comet *cmt, FILE *fout){

	fprintf(fout,"P11	%d.0	-%.6f\t%.6f\t%.3f\t%.4f\t%.4f\t0\t%4d/%02d/%07.4f\t%.1f %.1f\t0\t0\t%s; MPC 00000\t\n",
			equinox, cmt->q, cmt->e, cmt->i, cmt->pn, cmt->an, cmt->y,
			cmt->m, cmt->d, cmt->H, cmt->G, cmt->full);

	Frame4->ProgressBar1->Position += 1;
}

void TForm1::export_voyager (Comet *cmt, FILE *fout){

	char *mon;

	if (cmt->m== 1) mon = "Jan";
	if (cmt->m== 2) mon = "Feb";
	if (cmt->m== 3) mon = "Mar";
	if (cmt->m== 4) mon = "Apr";
	if (cmt->m== 5) mon = "May";
	if (cmt->m== 6) mon = "Jun";
	if (cmt->m== 7) mon = "Jul";
	if (cmt->m== 8) mon = "Aug";
	if (cmt->m== 9) mon = "Sep";
	if (cmt->m==10) mon = "Oct";
	if (cmt->m==11) mon = "Nov";
	if (cmt->m==12) mon = "Dec";

	fprintf(fout,"%-26s %9.6f   %.6f  %8.4f   %8.4f   %8.4f   0.0  %4d%s",
			cmt->name, cmt->q, cmt->e, cmt->i, cmt->an,
			cmt->pn, cmt->y, mon);

	if ((int)cmt->d<10)
		fprintf(fout, "%7.4f  %d.0\n", cmt->d, equinox);
	else
		fprintf(fout, "%7.4f %d.0\n", cmt->d, equinox);

	Frame4->ProgressBar1->Position += 1;
}

void TForm1::export_skytools (Comet *cmt, FILE *fout){

	int j, k, len;

	int h = (cmt->d - (int)cmt->d)*10000;

	if(h>999) h/=10;

	k=0;
	fputc('C', fout); k++;
	fputc(' ', fout); k++;
	len = strlen(cmt->ID);
	for(j=0; j<len; j++){
		fputc(cmt->ID[j], fout);
		k++;
	}

	len = strlen(cmt->ID);
	if ((cmt->ID[len-1]=='P' && isdigit(cmt->ID[len-2])) ||
		(cmt->ID[len-1]=='D' && isdigit(cmt->ID[len-2]))){
		fputc('/', fout); k++;
	}
	else {
		fputc(' ', fout);
		k++;
	}
	len = strlen(cmt->name);
	for(j=0; j<len; j++){
		fputc(cmt->name[j], fout);
		k++;
	}
	while(k<43){
		fputc(' ', fout); k++;
	}

	fprintf(fout,"2011 02 08 %4d %02d %02d.%-.03d  %9.6f   %.6f %7.3f %7.3f %7.3f  %4.1f  %4.1f 0.00%d MPC 00000\n",
			cmt->y, cmt->m, (int)cmt->d, h, cmt->q, cmt->e,
			cmt->pn, cmt->an, cmt->i, cmt->H, cmt->G, equinox);

	Frame4->ProgressBar1->Position += 1;
}

void TForm1::export_ssc (Comet *cmt, FILE *fout){

	char *mon;

	if (cmt->e == 1) cmt->e = 1.000001;
	int h = (cmt->d - (int)cmt->d)*10000;

	int len = strlen(cmt->full);
	char *full;
	full = (char*) malloc ((len+1) * sizeof(char));
	strcpy(full, cmt->full);

	for (int j=0; j<len; j++)
		if (full[j]=='/') full[j]=' ';

	if (cmt->m== 1) mon = "Jan";
	if (cmt->m== 2) mon = "Feb";
	if (cmt->m== 3) mon = "Mar";
	if (cmt->m== 4) mon = "Apr";
	if (cmt->m== 5) mon = "May";
	if (cmt->m== 6) mon = "Jun";
	if (cmt->m== 7) mon = "Jul";
	if (cmt->m== 8) mon = "Aug";
	if (cmt->m== 9) mon = "Sep";
	if (cmt->m==10) mon = "Oct";
	if (cmt->m==11) mon = "Nov";
	if (cmt->m==12) mon = "Dec";

	fprintf(fout,"\"%s\" \"Sol\"\n", full);
	fprintf(fout,"{\n");
	fprintf(fout,"Class \"comet\" \n");
	fprintf(fout,"Mesh \"asteroid.cms\" \n");
	fprintf(fout,"Texture \"asteroid.jpg\" \n");
	fprintf(fout,"Radius 5 \n");
	fprintf(fout,"Albedo 0.1 \n");
	fprintf(fout,"EllipticalOrbit \n");
	fprintf(fout,"\t{ \n");
	fprintf(fout,"\tPeriod \t\t\t %f \n", cmt->P);
	fprintf(fout,"\tPericenterDistance \t %f \n", cmt->q);
	fprintf(fout,"\tEccentricity \t\t %f \n", cmt->e);
	fprintf(fout,"\tInclination \t\t %.4f \n", cmt->i);
	fprintf(fout,"\tAscendingNode \t\t %.4f \n", cmt->an);
	fprintf(fout,"\tArgOfPericenter \t %.4f \n", cmt->pn);
	fprintf(fout,"\tMeanAnomaly \t\t 0  \n");
	fprintf(fout,"\tEpoch \t\t\t %ld.%.4d\t# %d %s %.2d.%.4d \n",
			cmt->T, h, cmt->y, mon, (int)cmt->d, h);
	fprintf(fout,"\t} \n");
	fprintf(fout,"}\n\n\n");

	Frame4->ProgressBar1->Position += 1;

	free (full);
}

void TForm1::export_stell (Comet *cmt, FILE *fout){

	int len = strlen(cmt->name);

	char *name;
	name = (char*) malloc ((len+1) * sizeof(char));
	strcpy(name, cmt->name);


	for (int j=0; j<len; j++)
		if (isupper(name[j])) name[j] = tolower(name[j]);

	int h = (cmt->d - (int)cmt->d)*10000;

	fprintf(fout,"[%s]\n", name);
	fprintf(fout,"parent = Sun\n");
	fprintf(fout,"orbit_Inclination = %f\n", cmt->i);
	fprintf(fout,"coord_func = comet_orbit\n");
	fprintf(fout,"orbit_Eccentricity = %f\n", cmt->e);
	fprintf(fout,"orbit_ArgOfPericenter = %f\n", cmt->pn);
	fprintf(fout,"absolute_magnitude=%.1f\n", cmt->H);
	fprintf(fout,"name = %s\n", cmt->full);
	fprintf(fout,"slope_parameter = %.1f\n", cmt->G);
	fprintf(fout,"lighting = false\n");
	fprintf(fout,"tex_map = nomap.png\n");
	fprintf(fout,"color = 1.0, 1.0, 1.0\n");
	fprintf(fout,"orbit_AscendingNode = %f\n", cmt->an);
	fprintf(fout,"albedo = 1\n");
	fprintf(fout,"radius = 5\n");
	fprintf(fout,"orbit_PericenterDistance = %f\n", cmt->q);
	fprintf(fout,"type = comet\n");
	fprintf(fout,"orbit_TimeAtPericenter = %ld.%.4d\n\n", cmt->T, h);

	Frame4->ProgressBar1->Position += 1;

	free (name);
}

bool TForm1::define_exclude(){

	for (int i=0; i<14; i++) excl.key[i]=false;

	if(Frame2->CheckBox1->Checked){

		if (Frame2->EditD->GetTextLen() == 0 ||
			Frame2->EditM->GetTextLen() == 0 ||
			Frame2->EditY->GetTextLen() == 0) {

			 Application->MessageBox(L"Please enter value",
				L"Error",
				MB_OK | MB_ICONERROR);
			return false;
		}

		int d = atoi(AnsiString(Frame2->EditD->Text).c_str());
		int m = atoi(AnsiString(Frame2->EditM->Text).c_str());
		int y = atoi(AnsiString(Frame2->EditY->Text).c_str());

		if (d<1 || d>31 || m<1 || m>12 || y<1000 || y>3000){

			 Application->MessageBox(L"Invalid date",
				L"Error",
				MB_OK | MB_ICONERROR);
			return false;
		}

		excl.T = greg_to_jul(y, m, d);

		if(Frame2->ComboBox1->ItemIndex==0) excl.key[0]=true;
		if(Frame2->ComboBox1->ItemIndex==1) excl.key[1]=true;
	}

	if(Frame2->CheckBox2->Checked){

		if (Frame2->Edit2->GetTextLen() == 0){

			Application->MessageBox(L"Please enter value",
				L"Error",
				MB_OK | MB_ICONERROR);
			return false;
		}

		float q = atof(AnsiString(Frame2->Edit2->Text).c_str());

		if (q <= 0){

			 Application->MessageBox(L"Value must be greather than zero",
				L"Error",
				MB_OK | MB_ICONERROR);
			return false;
		}

		excl.q = q;

		if(Frame2->ComboBox2->ItemIndex==0) excl.key[2]=true;
		if(Frame2->ComboBox2->ItemIndex==1) excl.key[3]=true;
	}

	if(Frame2->CheckBox3->Checked){

		if (Frame2->Edit3->GetTextLen() == 0){

			Application->MessageBox(L"Please enter value",
				L"Error",
				MB_OK | MB_ICONERROR);
			return false;
		}

		float e = atof(AnsiString(Frame2->Edit3->Text).c_str());

		if (e<0 || e>1){

			Application->MessageBox(L"Value must be between 0 and 1",
				L"Error",
				MB_OK | MB_ICONERROR);
			return false;
		}

		excl.e = e;

		if(Frame2->ComboBox3->ItemIndex==0) excl.key[4]=true;
		if(Frame2->ComboBox3->ItemIndex==1) excl.key[5]=true;
	}

	if(Frame2->CheckBox4->Checked){

		if (Frame2->Edit4->GetTextLen() == 0){

			Application->MessageBox(L"Please enter value",
				L"Error",
				MB_OK | MB_ICONERROR);
			return false;
		}

		float an = atof(AnsiString(Frame2->Edit4->Text).c_str());

		if (an<0 || an>=360){

			Application->MessageBox(L"Value must be between 0 and 360",
				L"Error",
				MB_OK | MB_ICONERROR);
			return false;
		}

		excl.an = an;

		if(Frame2->ComboBox4->ItemIndex==0) excl.key[6]=true;
		if(Frame2->ComboBox4->ItemIndex==1) excl.key[7]=true;
	}

	if(Frame2->CheckBox5->Checked){

		if (Frame2->Edit5->GetTextLen() == 0){

			Application->MessageBox(L"Please enter value",
				L"Error",
				MB_OK | MB_ICONERROR);
			return false;
		}

		float pn = atof(AnsiString(Frame2->Edit5->Text).c_str());

		if (pn<0 || pn>=360){

			Application->MessageBox(L"Value must be between 0 and 360",
				L"Error",
				MB_OK | MB_ICONERROR);
			return false;
		}

		excl.pn = pn;

		if(Frame2->ComboBox5->ItemIndex==0) excl.key[8]=true;
		if(Frame2->ComboBox5->ItemIndex==1) excl.key[9]=true;
	}

	if(Frame2->CheckBox6->Checked){

		if (Frame2->Edit6->GetTextLen() == 0){

			Application->MessageBox(L"Please enter value",
				L"Error",
				MB_OK | MB_ICONERROR);
			return false;
		}

		float i = atof(AnsiString(Frame2->Edit6->Text).c_str());

		if (i<0 || i>=180){

			Application->MessageBox(L"Value must be between 0 and 180",
				L"Error",
				MB_OK | MB_ICONERROR);
			return false;
		}

		excl.i = i;

		if(Frame2->ComboBox6->ItemIndex==0) excl.key[10]=true;
		if(Frame2->ComboBox6->ItemIndex==1) excl.key[11]=true;
	}

	if(Frame2->CheckBox7->Checked){

		if (Frame2->Edit7->GetTextLen() == 0){

        	Application->MessageBox(L"Please enter value",
				L"Error",
				MB_OK | MB_ICONERROR);
			return false;
		}

		float P = atof(AnsiString(Frame2->Edit7->Text).c_str());

		if (P <= 0){

			Application->MessageBox(L"Value must be greather than zero",
				L"Error",
				MB_OK | MB_ICONERROR);
			return false;
		}

		excl.P = P;

		if(Frame2->ComboBox7->ItemIndex==0) excl.key[12]=true;
		if(Frame2->ComboBox7->ItemIndex==1) excl.key[13]=true;
	}

	return true;
}

bool TForm1::do_exclude(Comet *cmt){

	if (excl.key[ 0] && cmt->T > excl.T) return true;
	if (excl.key[ 1] && cmt->T < excl.T) return true;
	if (excl.key[ 2] && cmt->q > excl.q) return true;
	if (excl.key[ 3] && cmt->q < excl.q) return true;
	if (excl.key[ 4] && cmt->e > excl.e) return true;
	if (excl.key[ 5] && cmt->e < excl.e) return true;
	if (excl.key[ 6] && cmt->an > excl.an) return true;
	if (excl.key[ 7] && cmt->an < excl.an) return true;
	if (excl.key[ 8] && cmt->pn > excl.pn) return true;
	if (excl.key[ 9] && cmt->pn < excl.pn) return true;
	if (excl.key[10] && cmt->i > excl.i) return true;
	if (excl.key[11] && cmt->i < excl.i) return true;
	if (excl.key[12] && cmt->P > excl.P) return true;
	if (excl.key[13] && cmt->P < excl.P) return true;

	return false;
}

bool TForm1::check_name(Comet *cmt){

	if(Form1->Frame2->CheckBox9->Checked){

		int len1 = Form1->Frame2->Edit1->Text.Length();
		int len2 = strlen(cmt->full);

		int passes = len2 - len1 + 1;

		AnsiString full = AnsiString(cmt->full);
		AnsiString str = Form1->Frame2->Edit1->Text;
		AnsiString sub;

		for(int i=0; i<=passes; i++){

			sub = full.SubString(i, len1);
			if(str.AnsiCompareIC(sub) == 0) return false;
		}
	}

	else return false;

	return true;
}

bool TForm1::exitFunction(){

	Form9->ShowModal();
	if(Form9->exit) return true;
	else return false;
}

void TForm1::spremiPostavke(){

	if(Form7->Edit1->GetTextLen() > 0 && DirectoryExists(Form7->Edit1->Text)){

		TRegistry *reg = new TRegistry;
		reg->RootKey = HKEY_CURRENT_USER;
		String regKey = String("Software\\Comet OEW\\");
		reg->OpenKey(regKey, true);
		reg->WriteString("Data Folder", Form7->Edit1->Text);
		dataFolder = Form7->Edit1->Text;
		settingsFile = dataFolder + "\\settings.ini";
		reg->CloseKey();
		delete reg;
	}

	TIniFile *iniSett = new TIniFile(settingsFile);
	iniSett->WriteInteger("MainForm", "Left", Form1->Left);
	iniSett->WriteInteger("MainForm", "Top", Form1->Top);

	iniSett->WriteInteger("PreviewForm", "Left", Form11->Left);
	iniSett->WriteInteger("PreviewForm", "Top", Form11->Top);
	iniSett->WriteInteger("PreviewForm", "Width", Form11->Width);
	iniSett->WriteInteger("PreviewForm", "Height", Form11->Height);

	iniSett->WriteInteger("Preferences", "CheckNewVersion", sett.checkNewVersion);
	iniSett->WriteInteger("Preferences", "AdvancedMode", sett.advancedMode);
	iniSett->WriteInteger("Preferences", "ExitConfirm", sett.exitConfirm);

	delete iniSett;
}

void TForm1::updateListbox(Comet *head){

	Form1->Frame3->ListBox1->Clear();

  	while(head!=NULL){
		Form1->Frame3->ListBox1->Items->Add(head->full);
		head = head->next;
	}

	Form1->Frame4->clearFrame();
}

//---------------------------------------------------------------------------

void __fastcall TForm1::Settings2Click(TObject *Sender)
{
	Form7->ShowModal();
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Exit1Click(TObject *Sender)
{
	Form1->Close();
}
//---------------------------------------------------------------------------

void __fastcall TForm1::About1Click(TObject *Sender)
{
	Form8->Position = poOwnerFormCenter;
	Form8->ShowModal();
}
//---------------------------------------------------------------------------




