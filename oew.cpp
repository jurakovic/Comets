// Orbital Elements Workshop

// TO DO:
// - promjenit import za elements.comet, rastav na ID i name
// - u get_sort_key namjestit da funkcionira i sa godinama ispod 1000

#include "oew.hpp"
using namespace std;

Comet cmt[MAX_CMT];
Excludings excl;

int main (){

	int type, end;

	system("COLOR 9");

/*	//stari nacin gdje je sve svima dostupno
	do {
		type=-1;
		start_screen();
		scanf("%d", &type);
		if (type >= 0 && type < 20) end = import_main(type, 0);
		if (type == 20) end = tools();
	} while (type!=21 && end!=EXIT_PROG);
*/
	//novi nacin gdje ona 3 tipa nisu dostupni ostalima
	do {
		type=-1;
		start_screen();
		scanf("%d", &type);
		if ((type >= 0 && type < 17) || type==123 || type==456 || type==789) end = import_main(type, 0);
		if (type == 17) end = tools();
	} while (type!=18 && end!=EXIT_PROG);

	exit_screen();
	getch();

	return SUCCESS;
}

int import_main (int Ty, int parent){

	int Ncmt, total_cmt, exp_ty, end;
	char c, *soft, *import_format, *export_format;
	char fin_name[80+1], fout_name[80+1];
	FILE *fin, *fout;

	// ta 3 su zapravo skriveni formati koji "nisu dostupni" obicnom korisniku
	if(Ty==123){
		import_format = "Comet for Windows";
		soft = "Comet.dat";
	}
	else if(Ty==456){
		import_format = "NASA";
		soft = "ELEMENTS.COMET";
	}
	else if(Ty==789){
		import_format = "NASA";
		soft = "CSV format";
	}
	else{
		import_format = menu[Ty].format;
		soft = menu[Ty].soft;
	}

	screen_imp(import_format, soft);

    do {
        cout << "  Enter input filename: ";
        cin.getline(fin_name, 81);

        if (fin_name[0]=='1' && fin_name[1]=='\0') return MAIN_MENU;
        if (fin_name[0]=='2' && fin_name[1]=='\0') return EXIT_PROG;

        fin=fopen(fin_name, "r");
        if (fin==NULL) cout << "\n  Unable to open file " << fin_name << "\n\n";
    } while (fin==NULL);

	Ncmt=0;
	while ((c=fgetc(fin)) != EOF){
		if (c=='\n') Ncmt++;
	}

	rewind(fin);

	if (Ty==123) Ncmt/=13;						// jer je 17. format cfw, a jedan komet je definiran kroz 13 redova
	if (Ty==3 || Ty==8 || Ty==10) Ncmt/=2;		// kao gore, samo što je 1 komet kroz 2 reda

	total_cmt = Ncmt;

    cout << "\n  File " << fin_name << " is successfully opened\n";
	cout << "\n  Total detected comets: " << total_cmt << "\n";
	cout << "\n  Press any key to continue... ";
	getch();

	end = define_exclude();
	if (end==MAIN_MENU) return MAIN_MENU;
	if (end==EXIT_PROG) return EXIT_PROG;

	if (Ty== 0) Ncmt = import_mpc (Ncmt, fin);
	if (Ty== 1) Ncmt = import_skymap (Ncmt, fin);
	if (Ty== 2) Ncmt = import_guide (Ncmt, fin);
	if (Ty== 3) Ncmt = import_xephem (Ncmt, fin);
	if (Ty== 4) Ncmt = import_home_planet (Ncmt, fin);
	if (Ty== 5) Ncmt = import_mystars (Ncmt, fin);
	if (Ty== 6 || Ty==16) Ncmt = import_thesky (Ncmt, fin); 	//jer imaju isti format
	if (Ty== 7) Ncmt = import_starry_night (Ncmt, fin);
	if (Ty== 8) Ncmt = import_deep_space (Ncmt, fin);
	if (Ty== 9) Ncmt = import_pc_tcs (Ncmt, fin);
	if (Ty==10) Ncmt = import_ecu (Ncmt, fin);
	if (Ty==11) Ncmt = import_dance (Ncmt, fin);
	if (Ty==12) Ncmt = import_megastar (Ncmt, fin);
	if (Ty==13) Ncmt = import_skychart (Ncmt, fin);
	if (Ty==14) Ncmt = import_voyager (Ncmt, fin);
	if (Ty==15) Ncmt = import_skytools (Ncmt, fin);
	if (Ty==123) Ncmt = import_cfw (Ncmt, fin);
	if (Ty==456) Ncmt = import_nasa1 (Ncmt, fin);
	if (Ty==789) Ncmt = import_nasa2 (Ncmt, fin);

	fclose(fin);

	cout << "\n\n  Total:    " << setw(4)  << total_cmt;
	cout << "\n  Excl/Err: " << setw(4) << total_cmt-Ncmt;
	cout << "\n  Imported: " << setw(4) << Ncmt;
	cout << "\n\n  Press any key to continue... ";
	getch();

	if (Ncmt==0){
		do {
			screen_imp(import_format, soft);
			cout << "  No data to work with! (0 imported comets)";
			cout << "\n\n  Select option (1/2): ";
			scanf("%d", &end);
		} while (end!=MAIN_MENU && end!=EXIT_PROG);
		return end;
	}

	if (parent == 1) return Ncmt;		// ovo je potrebno samo zbog funkcije tools_c()

	do {
		screen_exp1 (import_format);
		scanf("%d", &exp_ty);
	} while (exp_ty<0 || exp_ty>18);

	export_format = menu[exp_ty].format;				//koristi se u screen_exp2 i screen_exp3

	end = sort_data(Ncmt);
	if (end==MAIN_MENU) return MAIN_MENU;
	if (end==EXIT_PROG) return EXIT_PROG;

	screen_exp2(import_format, export_format);
	do {
		cout << "  Enter output filename: ";
		cin.getline(fout_name, 80);

		if (fout_name[0]=='1' && fout_name[1]=='\0') return MAIN_MENU;
		if (fout_name[0]=='2' && fout_name[1]=='\0') return EXIT_PROG;

		fout=fopen(fout_name, "a");
		if (fout==NULL) cout << "\n  Unable to create file " << fout_name << "\n\n";
	} while (fout==NULL);

	if (exp_ty== 0) export_mpc (Ncmt, fout);
	if (exp_ty== 1) export_skymap (Ncmt, fout);
	if (exp_ty== 2) export_guide (Ncmt, fout);
	if (exp_ty== 3) export_xephem (Ncmt, fout);
	if (exp_ty== 4) export_home_planet (Ncmt, fout);
	if (exp_ty== 5) export_mystars (Ncmt, fout);
	if (exp_ty== 6 || exp_ty==16) export_thesky (Ncmt, fout);
	if (exp_ty== 7) export_starry_night (Ncmt, fout);
	if (exp_ty== 8) export_deep_space (Ncmt, fout);
	if (exp_ty== 9) export_pc_tcs (Ncmt, fout);
	if (exp_ty==10) export_ecu (Ncmt, fout);
	if (exp_ty==11) export_dance (Ncmt, fout);
	if (exp_ty==12) export_megastar (Ncmt, fout);
	if (exp_ty==13) export_skychart (Ncmt, fout);
	if (exp_ty==14) export_voyager (Ncmt, fout);
	if (exp_ty==15) export_skytools (Ncmt, fout);
	if (exp_ty==17) export_ssc  (Ncmt, fout);
	if (exp_ty==18) export_stell  (Ncmt, fout);

	fclose(fout);

	do {
		screen_exp3(import_format, export_format);
		cout << "\n  Done\n\n  " <<  Ncmt  << " comets successfully saved in file " << fout_name;
		cout << "\n\n  Select option (1/2): ";
		scanf("%d", &end);
	} while (end!=MAIN_MENU && end!=EXIT_PROG);

//	clear_data();

	return end;
}


int define_exclude(){

	int y, m, d;
	char exclKey;

	for (int i=0; i<14; i++) excl.key[i]=0;

	do {

		do {
			system("CLS");
			excl_screen ();
			cout << "     1.   Main Menu   |   2.   Exit\n";
			cout << " =============================================================================\n\n";
			cout << "  Exclude by: \n\n";
			cout << "        Perihelion Date                  a. Greather than    b. Less than\n";
			cout << "        Pericenter Distance              c. Greather than    d. Less than\n";
			cout << "        Eccentricity                     e. Greather than    f. Less than\n";
			cout << "        Long. of the Ascending Node      g. Greather than    h. Less than\n";
			cout << "        Long. of Pericenter              i. Greather than    j. Less than\n";
			cout << "        Inclination                      k. Greather than    l. Less than\n";
			cout << "        Period                           m. Greather than    n. Less than\n\n";
			cout << "        Type \"x\" to continue\n\n";
			cout << "  Select option [a-n]: ";
			cin >> exclKey;

			if(isupper(exclKey)) exclKey = tolower(exclKey);

		} while ((exclKey < 'a' || exclKey > 'n') && exclKey!='x' && exclKey!='1' && exclKey!='2');

		if (exclKey=='1') return MAIN_MENU;
		if (exclKey=='2') return EXIT_PROG;

		if (exclKey=='a'){
			do{
				excl_screen ();
				cout << "\n  Exclude comet if Perihelion Date is greather than (DD MM YYYY):  ";
				scanf("%d %d %d", &d, &m, &y);
				if ((d<1 || d>31) || (m<1 || m>12)){
					cout << "\n  Invalid date! Try again... ";
					getch();
				}
			} while ((d<1 || d>31) || (m<1 || m>12));

			excl.T = greg_to_jul(y, m, d);
			excl.key[0]=1;
			if(excl.key[1]==1) excl.key[1]=0;
		}

		if (exclKey=='b'){
			do{
				excl_screen ();
				cout << "\n  Exclude comet if Perihelion Date is less than (DD MM YYYY):  ";
				scanf("%d %d %d", &d, &m, &y);
				if ((d<1 || d>31) || (m<1 || m>12)){
					cout << "\n  Invalid date! Try again... ";
					getch();
				}
			} while ((d<1 || d>31) || (m<1 || m>12));

			excl.T = greg_to_jul(y, m, d);
			excl.key[1]=1;
			if(excl.key[0]==1) excl.key[0]=0;
		}

		if (exclKey=='c'){
			do{
				excl_screen ();
				cout << "\n  Exclude comet if Perihelion distance is greather than:       AU\b\b\b\b\b\b\b\b";
				cin >> excl.q;
				if (excl.q<=0){
					cout << "\n  Value must be greather than zero! Try again... ";
					getch();
				}
			} while (excl.q<=0);

			excl.key[2]=1;
			if(excl.key[3]==1) excl.key[3]=0;
		}

		if (exclKey=='d'){
			do{
				excl_screen ();
				cout << "\n  Exclude comet if Perihelion distance is less than:       AU\b\b\b\b\b\b\b\b";
				cin >> excl.q;
				if (excl.q<=0){
					cout << "\n  Value must be greather than zero! Try again... ";
					getch();
				}
			} while (excl.q<=0);

			excl.key[3]=1;
			if(excl.key[2]==1) excl.key[2]=0;
		}

		if (exclKey=='e'){
			do{
				excl_screen ();
				cout << "\n  Exclude comet if Eccentricity is greather than: ";
				cin >> excl.e;
				if (excl.e<0 || excl.e>1){
					cout << "\n  Value must be between 0 and 1! Try again... ";
					getch();
				}
			} while (excl.e<0 || excl.e>1);

			excl.key[4]=1;
			if(excl.key[5]==1) excl.key[5]=0;
		}

		if (exclKey=='f'){
			do{
				excl_screen ();
				cout << "\n  Exclude comet if Eccentricity is less than: ";
				cin >> excl.e;
				if (excl.e<0 || excl.e>1){
					cout << "\n  Value must be between 0 and 1! Try again... ";
					getch();
				}
			} while (excl.e<0 || excl.e>1);

			excl.key[5]=1;
			if(excl.key[4]==1) excl.key[4]=0;
		}

		if (exclKey=='g'){
			do{
				excl_screen ();
				cout << "\n  Exclude comet if Long. of the Ascending Node is greather than:      degrees\b\b\b\b\b\b\b\b\b\b\b\b";
				cin >> excl.an;
				if (excl.an<0 || excl.an>=360){
					cout << "\n  Value must be between 0 and 360! Try again... ";
					getch();
				}
			} while (excl.an<0 || excl.an>=360);

			excl.key[6]=1;
			if(excl.key[7]==1) excl.key[7]=0;
		}

		if (exclKey=='h'){
			do{
				excl_screen ();
				cout << "\n  Exclude comet if Long. of the Ascending Node is less than:      degrees\b\b\b\b\b\b\b\b\b\b\b\b";
				cin >> excl.an;
				if (excl.an<0 || excl.an>=360){
					cout << "\n  Value must be between 0 and 360! Try again... ";
					getch();
				}
			} while (excl.an<0 || excl.an>=360);

			excl.key[7]=1;
			if(excl.key[6]==1) excl.key[6]=0;
		}

		if (exclKey=='i'){
			do{
				excl_screen ();
				cout << "\n  Exclude comet if Long. of Pericenter is greather than:      degrees\b\b\b\b\b\b\b\b\b\b\b\b";
				cin >> excl.pn;
				if (excl.pn<0 || excl.pn>=360){
					cout << "\n  Value must be between 0 and 360! Try again... ";
					getch();
				}
			} while (excl.pn<0 || excl.pn>=360);

			excl.key[8]=1;
			if(excl.key[9]==1) excl.key[9]=0;
		}

		if (exclKey=='j'){
			do{
				excl_screen ();
				cout << "\n  Exclude comet if Long. of Pericenter is less than:      degrees\b\b\b\b\b\b\b\b\b\b\b\b";
				cin >> excl.pn;
				if (excl.pn<0 || excl.pn>=360){
					cout << "\n  Value must be between 0 and 360! Try again... ";
					getch();
				}
			} while (excl.pn<0 || excl.pn>=360);

			excl.key[9]=1;
			if(excl.key[8]==1) excl.key[8]=0;
		}

		if (exclKey=='k'){
			do{
				excl_screen ();
				cout << "\n  Exclude comet if Inclination is greather than:      degrees\b\b\b\b\b\b\b\b\b\b\b\b";
				cin >> excl.i;
				if (excl.i<0 || excl.i>=180){
					cout << "\n  Value must be between 0 and 180! Try again... ";
					getch();
				}
			} while (excl.i<0 || excl.i>=180);

			excl.key[10]=1;
			if(excl.key[11]==1) excl.key[11]=0;
		}

		if (exclKey=='l'){
			do{
				excl_screen ();
				cout << "\n  Exclude comet if Inclination is less than:      degrees\b\b\b\b\b\b\b\b\b\b\b\b";
				cin >> excl.i;
				if (excl.i<0 || excl.i>=180){
					cout << "\n  Value must be between 0 and 180! Try again... ";
					getch();
				}
			} while (excl.i<0 || excl.i>=180);

			excl.key[11]=1;
			if(excl.key[10]==1) excl.key[10]=0;
		}

		if (exclKey=='m'){
			do{
				excl_screen ();
				cout << "\n  Exclude comet if Period is greather than:      years\b\b\b\b\b\b\b\b\b\b";
				cin >> excl.P;
				if (excl.P<=0){
					cout << "\n  Value must be greather than zero! Try again... ";
					getch();
				}
			} while (excl.P<=0);

			excl.key[12]=1;
			if(excl.key[13]==1) excl.key[13]=0;
		}

		if (exclKey=='n'){
			do{
				excl_screen ();
				cout << "\n  Exclude comet if Period is less than:      years\b\b\b\b\b\b\b\b\b\b";
				cin >> excl.P;
				if (excl.P<=0){
					cout << "\n  Value must be greather than zero! Try again... ";
					getch();
				}
			} while (excl.P<=0);

			excl.key[13]=1;
			if(excl.key[12]==1) excl.key[12]=0;
		}

	} while (exclKey!='x');

	system("CLS");
	cout << "\n  Excludings...\n\n";
	cout << " =============================================================================\n\n";

	if (excl.key[ 0]==0 && excl.key[ 1]==0 && excl.key[ 2]==0 &&
		excl.key[ 3]==0 && excl.key[ 4]==0 && excl.key[ 5]==0 &&
		excl.key[ 6]==0 && excl.key[ 7]==0 && excl.key[ 8]==0 &&
		excl.key[ 9]==0 && excl.key[10]==0 && excl.key[11]==0 &&
		excl.key[12]==0 && excl.key[13]==0) printf("  None\n");
	if (excl.key[ 0]==1) printf("  Exclude if Perihelion Date is greather than %02d. %02d. %d.\n", d, m, y);
	if (excl.key[ 1]==1) printf("  Exclude if Perihelion Date is less than %02d. %02d. %d.\n", d, m, y);
	if (excl.key[ 2]==1) printf("  Exclude if Pericenter Distance is greather than %.2f AU\n", excl.q);
	if (excl.key[ 3]==1) printf("  Exclude if Pericenter Distance is less than %.2f AU\n", excl.q);
	if (excl.key[ 4]==1) printf("  Exclude if Eccentricity is greather than %.2f\n", excl.e);
	if (excl.key[ 5]==1) printf("  Exclude if Eccentricity is less than %.2f\n", excl.e);
	if (excl.key[ 6]==1) printf("  Exclude if Long. of the Ascending Node is greather than %.1f degrees\n", excl.an);
	if (excl.key[ 7]==1) printf("  Exclude if Long. of the Ascending Node is less than %.1f degrees\n", excl.an);
	if (excl.key[ 8]==1) printf("  Exclude if Long. of Pericenter is greather than %.1f degrees\n", excl.pn);
	if (excl.key[ 9]==1) printf("  Exclude if Long. of Pericenter is less than %.1f degrees\n", excl.pn);
	if (excl.key[10]==1) printf("  Exclude if Inclination is greather than %.1f degrees\n", excl.i);
	if (excl.key[11]==1) printf("  Exclude if Inclination is less than %.1f degrees\n", excl.i);
	if (excl.key[12]==1) printf("  Exclude if Period is greather than %.1f years\n", excl.P);
	if (excl.key[13]==1) printf("  Exclude if Period is less than %.1f years\n", excl.P);
	printf("\n =============================================================================");

	return SUCCESS;
}

int do_exclude(int i){

	if (excl.key[ 0]==1 && cmt[i].T > excl.T) return 1;
	if (excl.key[ 1]==1 && cmt[i].T < excl.T) return 1;
	if (excl.key[ 2]==1 && cmt[i].q > excl.q) return 1;
	if (excl.key[ 3]==1 && cmt[i].q < excl.q) return 1;
	if (excl.key[ 4]==1 && cmt[i].e > excl.e) return 1;
	if (excl.key[ 5]==1 && cmt[i].e < excl.e) return 1;
	if (excl.key[ 6]==1 && cmt[i].an > excl.an) return 1;
	if (excl.key[ 7]==1 && cmt[i].an < excl.an) return 1;
	if (excl.key[ 8]==1 && cmt[i].pn > excl.pn) return 1;
	if (excl.key[ 9]==1 && cmt[i].pn < excl.pn) return 1;
	if (excl.key[10]==1 && cmt[i].i > excl.i) return 1;
	if (excl.key[11]==1 && cmt[i].i < excl.i) return 1;
	if (excl.key[12]==1 && cmt[i].P > excl.P) return 1;
	if (excl.key[13]==1 && cmt[i].P < excl.P) return 1;

	return SUCCESS;
}


int import_mpc (int N, FILE *fin){

	int j, k, l;
	int m, line=1;
	char x[30+1];

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "%14c %d %02d %02d.%04d %f %f %f %f %f%12c%f %f %55c %30[^\n]%*c",		// %f%12c%f mora bit tako zajedno
			x, &cmt[i].y, &cmt[i].m, &cmt[i].d, &cmt[i].h,
			&cmt[i].q, &cmt[i].e, &cmt[i].pn, &cmt[i].an,
			&cmt[i].i, x, &cmt[i].H, &cmt[i].G, cmt[i].full, x);

		if (m < 15){
			cout << "\n\n  Unable to read data in line " << line;
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

		if(do_exclude(i)==1){N--; i--;}
	}

	return N;
}

int import_skymap (int N, FILE *fin){

	int j, k, l, u, t, space;
	int m, line=1, len;

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "%47c %4d %2d %2d.%4d %f %f %f %f %f %f %f\n",
			cmt[i].full, &cmt[i].y, &cmt[i].m, &cmt[i].d,
			&cmt[i].h, &cmt[i].q, &cmt[i].e, &cmt[i].pn,
			&cmt[i].an, &cmt[i].i, &cmt[i].H, &cmt[i].G);

		if (m < 12){
			cout << "\n\n  Unable to read data in line " << line;
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

		if(do_exclude(i)==1){N--; i--;}
	}

	return N;
}

int import_guide (int N, FILE *fin){

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
			cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;

		if(do_exclude(i)==1){N--; i--;}
	}

	return N;
}

int import_xephem (int N, FILE *fin){

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
				cout << "\n\n  Unable to read data in line " << line;
				fscanf(fin, "%*[^\n]\n");
				N--; i--; line++;
				continue;
			}

			cmt[i].q = smAxis*(1-cmt[i].e);
			T = greg_to_jul (yy, mm, dd);
			cmt[i].T = T - mAnomaly/mdMotion;

			jul_to_greg(cmt[i].T, cmt[i].y, cmt[i].m, cmt[i].d);

			line+=2;

			if(do_exclude(i)==1){N--; i--;}
		}

		if(c == 'p'){
			m = fscanf(fin, ",%d/%d.%d/%d,%f,%f,%f,%f,2000,%f,%f\n",
				&cmt[i].m, &cmt[i].d, &cmt[i].h, &cmt[i].y,
				&cmt[i].i, &cmt[i].pn, &cmt[i].q, &cmt[i].an,
				&cmt[i].H, &cmt[i].G);

			if (m < 10){
				cout << "\n\n  Unable to read data in line " << line;
				fscanf(fin, "%*[^\n]\n");
				N--; i--; line++;
				continue;
			}

			cmt[i].e = 1.000000;
			cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, cmt[i].d);
			line+=2;

			if(do_exclude(i)==1){N--; i--;}
		}

		if(c == 'h'){
			m = fscanf(fin, ",%d/%d.%d/%d,%f,%f,%f,%f,%f,2000,%f,%f\n",
				&cmt[i].m, &cmt[i].d, &cmt[i].h, &cmt[i].y,
				&cmt[i].i, &cmt[i].an, &cmt[i].pn, &cmt[i].e,
				&cmt[i].q, &cmt[i].H, &cmt[i].G);

			if (m < 11){
				cout << "\n\n  Unable to read data in line " << line;
				fscanf(fin, "%*[^\n]\n");
				N--; i--; line++;
				continue;
			}

			cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, cmt[i].d);
			line+=2;

			if(do_exclude(i)==1){N--; i--;}
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
			cout << "\n\n  Unable to read data in line " << line;
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

		if(do_exclude(i)==1){N--; i--;}
	}

	return N;
}

int import_mystars (int N, FILE *fin){

	int j, k, l;
	int m, line=1;
	char c, x[30+1];

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
			cout << "\n\n  Unable to read data in line " << line;
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

		if(do_exclude(i)==1){N--; i--;}
	}

	return N;
}

int import_thesky (int N, FILE *fin){

	int j, k, l;
	int m, line=1;
	char x[20+1];

	for (int i=0; i<N; i++) {
//		fscanf(fin, "%40c %*c %d %*c %4d %2d %2d %*c %d %*c %f %*c %f %*c %f %*c %f %*c %f %*c %f %25[^\n]%*c",     stari nacin
		m = fscanf(fin, "%45c %4d%2d%2d.%d | %f | %f | %f | %f | %f | %f | %f %20[^\n]%*c",
			cmt[i].full, &cmt[i].y, &cmt[i].m,
			&cmt[i].d, &cmt[i].h, &cmt[i].q, &cmt[i].e,
			&cmt[i].pn, &cmt[i].an, &cmt[i].i, &cmt[i].H,
			&cmt[i].G, x);

		if (m < 13){
			cout << "\n\n  Unable to read data in line " << line;
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

		if(do_exclude(i)==1){N--; i--;}
	}

	return N;
}

int import_starry_night (int N, FILE *fin){

	int j, k;
	int m, line=1;
	long int y;
	char c, x[20+1];

	for (int i=0; i<N; i++) {

		j=0; k=0;
		while (k<34){
			c=fgetc(fin);
			cmt[i].name[j]=c;
			if (c==' ' && j==0) --j;
			j++; k++;
		}

		remove_spaces(cmt[i].name);

		m = fscanf(fin, "%f 0.0 %f %f %f %f %f %ld.%d %ld.5 %f",
			&cmt[i].H, &cmt[i].e, &cmt[i].q, &cmt[i].an,
			&cmt[i].pn, &cmt[i].i, &cmt[i].T, &cmt[i].h,
			&y, &cmt[i].G);

		if (m < 10){
			cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		j=0; k=0;
		while (k<16){
			c=fgetc(fin);
			cmt[i].ID[j]=c;
			if (c==' ' && j==0) --j;
			j++; k++;
		}

		remove_spaces(cmt[i].ID);

		fscanf(fin, "%20[^\n]%*c", x);

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

		if(do_exclude(i)==1){N--; i--;}
	}

	return N;
}

int import_deep_space (int N, FILE *fin){

	int j, m, line=2;
	char c, x[8+1];

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
			&cmt[i].i, &cmt[i].H, &cmt[i].G);

		if (m < 12){
			cout << "\n\n  Unable to read data in line " << line;
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

		if(do_exclude(i)==1){N--; i--;}
	}

	return N;
}

int import_pc_tcs (int N, FILE *fin){

	int j, k;
	int m, line=1, len;
	char tempID[20];

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "%s %f %f %f %f %f %d %d %d.%d %f %f %60[^\n]%*c",
			cmt[i].ID, &cmt[i].q, &cmt[i].e, &cmt[i].i,
			&cmt[i].pn, &cmt[i].an, &cmt[i].y, &cmt[i].m,
			&cmt[i].d, &cmt[i].h, &cmt[i].H, &cmt[i].G, cmt[i].name);

		if (m < 13){
			cout << "\n\n  Unable to read data in line " << line;
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

		if(do_exclude(i)==1){N--; i--;}
	}

	return N;
}

int import_ecu (int N, FILE *fin){

	int j, k, l;
	int m, line=2;

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "%45[^\n]%*cE C 2000 %d %d %d.%d %f %f %f %f %f %f %f\n",
			cmt[i].full, &cmt[i].y, &cmt[i].m, &cmt[i].d,
			&cmt[i].h, &cmt[i].q, &cmt[i].e, &cmt[i].pn, &cmt[i].an,
			&cmt[i].i, &cmt[i].H, &cmt[i].G);

		if (m < 12){
			cout << "\n\n  Unable to read data in line " << line;
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

		if(do_exclude(i)==1){N--; i--;}
	}

	return N;
}

int import_dance (int N, FILE *fin){

	int j, k;
	int m, line=1, len;
	char tempID[20];

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "%11c %f %f %f %f %f %d.%2d%2d%4d %30[^\n]%*c",
			cmt[i].ID, &cmt[i].q, &cmt[i].e, &cmt[i].i,
			&cmt[i].an, &cmt[i].pn, &cmt[i].y, &cmt[i].m,
			&cmt[i].d, &cmt[i].h, cmt[i].name);

		if (m < 11){
			cout << "\n\n  Unable to read data in line " << line;
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

		if(do_exclude(i)==1){N--; i--;}
	}

	return N;
}

int import_megastar (int N, FILE *fin){

	int m, line=1;
	char x[25+1];

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "%30c %12c %d %d %d.%d %f %f %f %f %f %f %f %25[^\n]%*c",
			cmt[i].name, cmt[i].ID, &cmt[i].y, &cmt[i].m, &cmt[i].d,
			&cmt[i].h, &cmt[i].q, &cmt[i].e, &cmt[i].pn,
			&cmt[i].an, &cmt[i].i, &cmt[i].H, &cmt[i].G, x);

		if (m < 14){
			cout << "\n\n  Unable to read data in line " << line;
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

		if(do_exclude(i)==1){N--; i--;}
	}

	return N;
}

int import_skychart (int N, FILE *fin){

	int j, k, l;
	int m, line=1;
	char c;

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "P11 2000.0 -%f %f %f %f %f 0 %d/%d/%d.%d %f %f 0 0 ",
			&cmt[i].q, &cmt[i].e, &cmt[i].i, &cmt[i].pn,
			&cmt[i].an, &cmt[i].y, &cmt[i].m, &cmt[i].d,
			&cmt[i].h, &cmt[i].H, &cmt[i].G);

		if (m < 12){
			cout << "\n\n  Unable to read data in line " << line;
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

		if(do_exclude(i)==1){N--; i--;}
	}

	return N;
}

int import_voyager (int N, FILE *fin){

	int m, line=1;
	char mj[3+1];

	for (int i=0; i<N; i++) {

		m = fscanf(fin, "%27c %f %f %f %f %f %f %4d %3c %d.%d 2000.0\n",
			cmt[i].name, &cmt[i].q, &cmt[i].e, &cmt[i].i,
			&cmt[i].an, &cmt[i].pn, &cmt[i].G, &cmt[i].y,
			mj, &cmt[i].d, &cmt[i].h);

		if (m < 11){
			cout << "\n\n  Unable to read data in line " << line;
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

		if(do_exclude(i)==1){N--; i--;}
	}

	return N;
}

int import_skytools (int N, FILE *fin){

	int j, k, l, u, t, space;
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
			cout << "\n\n  Unable to read data in line " << line;
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

		if(do_exclude(i)==1){N--; i--;}
	}

	return N;
}

int import_cfw (int N, FILE *fin){

	int j, k, l;

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
					&cmt[i].H, &cmt[i].G);

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

		if(do_exclude(i)==1){N--; i--;}
	}

	return N;
}

int import_nasa1 (int N, FILE *fin){

	int j, k, l;
	int m, line=1, len, trash;
	char c, q, x[20+1];

	cout << "\n\n  Press any key to continue.... ";
	getch();

	do{
		clear_all();
		cout << "\n  Exclude SOHO...\n\n";
		cout << " =============================================================================\n\n";
		cout << "  Do you want exclude SOHO comets? (y/n)\n\n";
		cout << "  Select option: ";
		cin >> q;
		if(isupper(q)) q = tolower(q);
	} while (q!='y' && q!='n');

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

		m = fscanf(fin, "%d %f %f %f %f %f %4d%2d%2d.%4d %20[^\n]%*c",
			&trash, &cmt[i].q, &cmt[i].e, &cmt[i].i,
			&cmt[i].pn, &cmt[i].an, &cmt[i].y, &cmt[i].m,
			&cmt[i].d, &cmt[i].h, x);

		if (m < 11){
			cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;


		if (q=='y'){
			len = strlen(cmt[i].name);
			for (j=0; j<len; j++){
				if (cmt[i].name[j  ]=='S' && cmt[i].name[j+1]=='O' &&
					cmt[i].name[j+2]=='H' && cmt[i].name[j+3]=='O'){
					N--; i--;
				}
			}
		}

		if(do_exclude(i)==1){N--; i--;}
	}

	cout << "\n =============================================================================";

	return N;
}

int import_nasa2 (int N, FILE *fin){

	int j, k, l;
	int m, line=1, len;
	char c, q, x[10+1];

	cout << "\n\n  Press any key to continue.... ";
	getch();

	do{
		clear_all();
		cout << "\n  Exclude SOHO...\n\n";
		cout << " =============================================================================\n\n";
		cout << "  Do you want exclude SOHO comets? (y/n)\n\n";
		cout << "  Select option: ";
		cin >> q;
		if(isupper(q)) q = tolower(q);
	} while (q!='y' && q!='n');

	for (int i=0; i<N; i++) {

		j=0;
		fgetc(fin);		// da uzme prve navodnike
		while ((c=fgetc(fin)) != '"' ){
			cmt[i].full[j]=c;
			if (c==' ' && j==0) --j;
			j++;
		}

		cmt[i].name[j]='\0';

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

		if (strlen(cmt[i].name)==0){
			for(k=0; cmt[i].full[k]!='\0'; k++)
				cmt[i].name[k]=cmt[i].full[k];
			cmt[i].name[k]='\0';

			strcat(cmt[i].full, " (");
			strcat(cmt[i].full, cmt[i].name);
			strcat(cmt[i].full, ")");
		}

		m = fscanf(fin, ",%f,%f,%f,%f,%f,%4d%2d%2d.%4d%10[^\n]%*c",
			&cmt[i].q, &cmt[i].e, &cmt[i].pn, &cmt[i].an,
			&cmt[i].i, &cmt[i].y, &cmt[i].m, &cmt[i].d,
			&cmt[i].h, x);

		if (m < 10){
			cout << "\n\n  Unable to read data in line " << line;
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		if (q=='y'){
			len = strlen(cmt[i].name);
			for (j=0; j<len; j++){
				if ((cmt[i].name[j]  =='S' && cmt[i].name[j+1]=='O' &&
					cmt[i].name[j+2]=='H' && cmt[i].name[j+3]=='O')){
					N--; i--;
				}
			}
		}

		cmt[i].P = compute_period (cmt[i].q, cmt[i].e);
		cmt[i].T = greg_to_jul (cmt[i].y, cmt[i].m, cmt[i].d);
		cmt[i].sort = get_sort_key(cmt[i].ID);
		line++;

		if(do_exclude(i)==1){N--; i--;}
	}

	cout << "\n =============================================================================";

	return N;
}


void export_mpc (int N, FILE *fout){

	for (int i=0; i<N; i++) {

		fprintf(fout,"              %4d %02d %02d.%04d %9f  %.6f  %8.4f  %8.4f  %8.4f  %4d%02d%02d  %4.1f %4.1f  %-56s MPC 00000\n",
				cmt[i].y, cmt[i].m, cmt[i].d, cmt[i].h, cmt[i].q, cmt[i].e,
				cmt[i].pn, cmt[i].an, cmt[i].i, ep_y, ep_m, ep_d, cmt[i].H, cmt[i].G, cmt[i].full);
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

		fprintf(fout,"%4d %02d %02d.%04d %9f       %.6f %8.4f %8.4f %8.4f  %4.1f  %4.1f\n",
				cmt[i].y, cmt[i].m, cmt[i].d, cmt[i].h, cmt[i].q,
				cmt[i].e, cmt[i].pn, cmt[i].an, cmt[i].i, cmt[i].H, cmt[i].G);
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

		fprintf(fout,"%2d.%04d  %2d  %4d  0.0        %9.6f    %.6f  %8.4f    %8.4f    %8.4f    %d.0   %4.1f %4.1f    MPC 00000\n",
				cmt[i].d, cmt[i].h, cmt[i].m, cmt[i].y, cmt[i].q, cmt[i].e,
				cmt[i].i, cmt[i].pn, cmt[i].an, equinox, cmt[i].H, cmt[i].G);
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

void export_home_planet (int N, FILE *fout){

	for (int i=0; i<N; i++) {

		double smAxis = cmt[i].q/(1-cmt[i].e);

		fprintf(fout,"%s,%d-%d-%d.%04d,%.6f,%.6f,%.4f,%.4f,%.4f,%.5f,%.5f years, MPC      \n",
				cmt[i].full, cmt[i].y, cmt[i].m, cmt[i].d, cmt[i].h, cmt[i].q,
				cmt[i].e, cmt[i].pn, cmt[i].an, cmt[i].i, smAxis, cmt[i].P);
	}
}

void export_mystars (int N, FILE *fout){

	for (int i=0; i<N; i++) {


		fprintf(fout,"%s;\t%ld.%04d\t%.4f\t%.6f\t%.6f\t%.4f\t%.4f\t%.1f\t%.1f\tMPC00000\t%ld.0\n",
				cmt[i].full, cmt[i].T-2400000, cmt[i].h, cmt[i].pn, cmt[i].e, cmt[i].q,
				cmt[i].i, cmt[i].an, cmt[i].H, cmt[i].G, eq_JD-2400000);
	}
}

void export_thesky (int N, FILE *fout){

	for (int i=0; i<N; i++) {

		fprintf(fout,"%-39s|%d|%4d%02d%02d.%04d |%9f |%.6f |%8.4f |%8.4f |%8.4f |%4.1f |%4.1f | MPC 00000\n",
				cmt[i].full, equinox, cmt[i].y, cmt[i].m, cmt[i].d, cmt[i].h, cmt[i].q,
				cmt[i].e, cmt[i].pn, cmt[i].an, cmt[i].i, cmt[i].H, cmt[i].G);
	}
}

void export_starry_night (int N, FILE *fout){

	for (int i=0; i<N; i++) {

		fprintf(fout,"     %-29s %4.1f    0.0   %.6f   %9.6f    %8.4f  %8.4f  %8.4f  %ld.%04d    %ld.5  %4.1f  %-13s MPC 00000\n",
				cmt[i].name, cmt[i].H, cmt[i].e, cmt[i].q, cmt[i].an, cmt[i].pn,
				cmt[i].i, cmt[i].T, cmt[i].h, eq_JD, cmt[i].G, cmt[i].ID);
	}
}

void export_deep_space (int N, FILE *fout){

	for (int i=0; i<N; i++) {

		fprintf(fout,"%s (%s)\nC J%d %4d %02d %02d.%04d %.6f %.6f %.4f %.4f %.4f %.1f %.1f\n",
				cmt[i].name, cmt[i].ID, equinox, cmt[i].y, cmt[i].m, cmt[i].d, cmt[i].h,
				cmt[i].q, cmt[i].e, cmt[i].pn, cmt[i].an, cmt[i].i, cmt[i].H, cmt[i].G);
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

		fprintf(fout,"%s %.6f %.6f %.4f %.4f %.4f %4d %02d %02d.%04d %.1f %.1f %s\n",
				cmt[i].ID, cmt[i].q, cmt[i].e, cmt[i].i, cmt[i].pn, cmt[i].an,
				cmt[i].y, cmt[i].m, cmt[i].d, cmt[i].h, cmt[i].H, cmt[i].G, cmt[i].name);
	}
}

void export_ecu (int N, FILE *fout){

	for (int i=0; i<N; i++) {

		fprintf(fout,"%s\nE C %d %4d %02d %02d.%04d %.6f %.6f %.4f %.4f %.4f %.1f %.1f\n",
				cmt[i].full, equinox, cmt[i].y, cmt[i].m, cmt[i].d, cmt[i].h, cmt[i].q,
				cmt[i].e, cmt[i].pn, cmt[i].an, cmt[i].i, cmt[i].H, cmt[i].G);
	}
}

void export_dance (int N, FILE *fout){

	for (int i=0; i<N; i++) {

		fprintf(fout,"%s\nE C %d %4d %02d %02d.%04d %.6f %.6f %.4f %.4f %.4f %.1f %.1f\n",
				cmt[i].full, equinox, cmt[i].y, cmt[i].m, cmt[i].d, cmt[i].h,
				cmt[i].q, cmt[i].e, cmt[i].pn, cmt[i].an, cmt[i].i, cmt[i].H, cmt[i].G);
	}
}

void export_megastar (int N, FILE *fout){

	for (int i=0; i<N; i++) {

		fprintf(fout,"%-30s%-12s%4d %02d  %02d.%04d   %9.6f   %.6f    %8.4f    %8.4f    %8.4f   %4.1f   %4.1f    %d MPC 00000\n",
				cmt[i].name, cmt[i].ID, cmt[i].y, cmt[i].m, cmt[i].d,
				cmt[i].h, cmt[i].q, cmt[i].e, cmt[i].pn, cmt[i].an,
				cmt[i].i, cmt[i].H, cmt[i].G, equinox);
	}
}

void export_skychart (int N, FILE *fout){

	for (int i=0; i<N; i++) {

		fprintf(fout,"P11	%d.0	-%.6f\t%.6f\t%.3f\t%.4f\t%.4f\t0\t%4d/%02d/%02d.%04d\t%.1f %.1f\t0\t0\t%s; MPC 00000\t\n",
				equinox, cmt[i].q, cmt[i].e, cmt[i].i, cmt[i].pn, cmt[i].an, cmt[i].y,
				cmt[i].m, cmt[i].d, cmt[i].h, cmt[i].H, cmt[i].G, cmt[i].full);
	}
}

void export_voyager (int N, FILE *fout){

	string mon;

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
				cmt[i].pn, cmt[i].y, mon.c_str());

		if (cmt[i].d<10) fprintf(fout, "%d.%04d  %d.0\n", cmt[i].d, cmt[i].h, equinox);
		else fprintf(fout, "%d.%04d %d.0\n", cmt[i].d, cmt[i].h, equinox);
	}
}

void export_skytools (int N, FILE *fout){

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

void export_ssc (int N, FILE *fout){

	string mon;

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
				cmt[i].T, cmt[i].h, cmt[i].y, mon.c_str(), cmt[i].d, cmt[i].h);
		fprintf(fout,"\t} \n");
		fprintf(fout,"}\n\n\n");
	}
}

void export_stell (int N, FILE *fout){

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


double compute_period (float q, float e){

	double P;

	if (e <  1) P = pow((q/(1-e)),1.5);
	if (e >  1) P = pow((q/(e-1)),1.5);
	if (e == 1) P = pow((q/(1-0.999999)),1.5);

	return P;
}

double get_sort_key(char *ID){

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
			temppp[3]='\0';

			temppp[0]=ID[k+2];
			temppp[1]=ID[k+3];
			temppp[2]=ID[k+4];

			v += atof(tempp)/(double)10000000;
		}

		sort+=v;
	}

	return sort;
}

long int greg_to_jul (int y, int m, int d){

	return 367*y - (7*(y + (m + 9)/12))/4 - ((3*(y + (m - 9)/7))/100 + 1)/4 + (275*m)/9 + d + 1721029;
}

void jul_to_greg (long int T, int &y, int &m, int &d){

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

void remove_spaces (char *name){

	int len = strlen(name);

	for (int i=0; i<len-1; i++) if (name[i]==' ' && name[i+1]==' ') name[i]='\0';
}


void clear_all(){
	system("CLS");
	fflush(stdin);
	fflush(stdout);
}

void clear_data(){

	for (int i=0; i<MAX_CMT; i++){

		for (int j=0; j<81; j++) cmt[i].full[j]='\0';
		for (int j=0; j<56; j++) cmt[i].name[j]='\0';
		for (int j=0; j<26; j++) cmt[i].ID[j]='\0';
		cmt[i].T=0;
		cmt[i].y=0;
		cmt[i].m=0;
		cmt[i].d=0;
		cmt[i].h=0;
		cmt[i].P=0.0;
		cmt[i].q=0.0;
		cmt[i].e=0.0;
		cmt[i].i=0.0;
		cmt[i].an=0.0;
		cmt[i].pn=0.0;
		cmt[i].H=0.0;
		cmt[i].G=0.0;
		cmt[i].sort=0.0;
		for (int j=0; j<21; j++) cmt[i].book[j]='\0';
	}
}


int tools (){

	int end;
	char sel;

	while (1){
		do {
			clear_all();
			cout << "\n  Tools\n\n";
			cout << " =============================================================================\n";
			cout << "     1.   Main Menu   |   2.   Exit\n";
			cout << " =============================================================================\n\n";
			cout << "    a. Gregorian date to Julian day converter\n";
			cout << "    b. Julian day to Gregorian date converter\n";
			cout << "    c. Perihelion passages calculator\n";
			cout << "    d. Kepler orbit elements to Cartesian coordinates converter\n";
			cout << "    e. Cartesian coordinates to Kepler orbit elements converter\n\n";
			cout << "  Select option [a-e]: ";
			cin >> sel;

			if(isupper(sel)) sel = tolower(sel);

		} while ((sel < 'a' || sel > 'e') && sel!='1' && sel!='2');

		if (sel=='1') return MAIN_MENU;
		if (sel=='2') return EXIT_PROG;

		if (sel=='a') end = tools_a();
		if (sel=='b') end = tools_b();
		if (sel=='c') end = tools_d();
		if (sel=='d') end = tools_d();
		if (sel=='e') end = tools_e();

		if (end==MAIN_MENU) return MAIN_MENU;
		if (end==EXIT_PROG) return EXIT_PROG;
	}

	return SUCCESS;
}

int tools_a(){

	int d, m, y, i, j, razmaka;
	long int jd;
	char date_str[50+1];	//stavio sam dosta mjesta za svaki sluèaj
	char dd[10+1], mm[10+1], yy[30+1];

	while(1){
		do{
			//sa ovim petljama brisem prethodne unose
			for(i=0; i<51; i++) date_str[i]='\0';
			for(i=0; i<11; i++) dd[i]='\0';
			for(i=0; i<11; i++) mm[i]='\0';
			for(i=0; i<31; i++) yy[i]='\0';

			clear_all();
			cout << "\n  Gregorian date to Julian day converter\n\n";
			cout << " =============================================================================\n";
			cout << "     1.   Main Menu   |   2.   Exit\n";
			cout << " =============================================================================\n\n";
			cout << "  Enter Gregorian date in format DD MM YYYY:  ";
			cin.getline(date_str, 51);

			if (date_str[0]=='1' && date_str[1]=='\0') return MAIN_MENU;
			if (date_str[0]=='2' && date_str[1]=='\0') return EXIT_PROG;

			razmaka=0;
			for(i=0; date_str[i]!='\0'; i++){
				if (date_str[i]==' ') razmaka++;
			}

			if (razmaka!=2){
				cout << "\n  Invalid date! Try again... ";
				getch();
			}

			else {

				i=0; j=0;
				while (date_str[i]!=' '){
					if(isdigit(date_str[i]))
					dd[j]=date_str[i];
					j++; i++;
				}
				j=0; i++;
				while (date_str[i]!=' '){
					if(isdigit(date_str[i]))
					mm[j]=date_str[i];
					j++; i++;
				}
				j=0; i++;
				while (date_str[i]!='\0'){
					if(isdigit(date_str[i]))
					yy[j]=date_str[i];
					j++; i++;
				}

				d=atoi(dd);
				m=atoi(mm);
				y=atoi(yy);

				if (d<1 || d>31 || m<1 || m>12){
					cout << "\n  Invalid date! Try again... ";
					getch();
				}
			}
		} while (d<1 || d>31 || m<1 || m>12 || razmaka!=2);

		if (razmaka==2){
			// jer ako se dvaput zaredom raèuna datum i ako se prvi put dobro unese, a drugi put ne,
			// ostat ce datum od prvog unosa, pa ce prikazat taj datum, a ne bi trebao

			jd = greg_to_jul(y, m, d);
			cout << "\n\n  Julian day: " << jd;
			getch();
		}
	}

	return SUCCESS;
}

int tools_b(){

	int d, m, y;
	long int jd;
	char date_str[50+1];

	while(1){

		for(int i=0; i<51; i++) date_str[i]='\0';

		clear_all();
		cout << "\n  Julian day to Gregorian date converter\n\n";
		cout << " =============================================================================\n";
		cout << "     1.   Main Menu   |   2.   Exit\n";
		cout << " =============================================================================\n\n";
		cout << "  Enter Julian day:  ";
		cin.getline(date_str, 51);

		if (date_str[0]=='1' && date_str[1]=='\0') return MAIN_MENU;
		if (date_str[0]=='2' && date_str[1]=='\0') return EXIT_PROG;

		jd=atoi(date_str);

		jul_to_greg(jd, y, m, d);
		cout << "\n\n  Day:   " << setw (4) << d;
		cout << "\n  Month: " << setw (4) << m;
		cout << "\n  Year:  " << setw (4) << y;
		getch();
	}

	return SUCCESS;
}

int tools_c(){

	int type, N, end, pass, d, m, y, h, h2;
	float v1, v2, v3, v4;
	long int today_jd;
	char fout_name[80+1];
	time_t rawtime;
    struct tm *timeinfo;
	FILE *fout;

	// tu staviti neko upozorenje da se iskljuci sto vise kometa

	do {
		type = -1;
		tools_soft_screen();
		scanf("%d", &type);
	} while (type<0 || type>19);

	N = import_main(type, 1);
	if (N==MAIN_MENU) return MAIN_MENU;
	if (N==EXIT_PROG) return EXIT_PROG;

	end = sort_data(N);
	if (end==MAIN_MENU) return MAIN_MENU;
	if (end==EXIT_PROG) return EXIT_PROG;

	do{
		system("CLS");
		cout << "\n  Perihelion passages calculator\n\n";
		cout << " =============================================================================\n\n";
		cout << "  How many passages do you want to be calculated? (1-20) ";
		scanf("%d", &pass);
	} while (pass<1 || pass>20);

	do {
		fflush (stdin);
		cout << "\n  Enter output filename: ";
		cin.getline(fout_name, 80);

		if (fout_name[0]=='1' && fout_name[1]=='\0') return MAIN_MENU;
		if (fout_name[0]=='2' && fout_name[1]=='\0') return EXIT_PROG;

		fout=fopen(fout_name, "a");
		if (fout==NULL) cout << "\n  Unable to create file " << fout_name;
	} while (fout==NULL);

	time (&rawtime);
	timeinfo = localtime(&rawtime);
	d = timeinfo->tm_mday;
	m = 1+timeinfo->tm_mon;
	y = 1900+timeinfo->tm_year;
	today_jd = greg_to_jul(y, m, d);

	for (int i=0; i<N; i++){

		while(cmt[i].T<//(
			today_jd// - ((int) (cmt[i].P * 365.256363)))
			){
			//tako ga vracamo na "priblizno" zadnji prolaz

			v1 = ((int) cmt[i].P) * 365.256363;
			v2 = (cmt[i].P - (int) cmt[i].P) * 365.256363;
			v3 = v1 + v2;
			cmt[i].T += (int) v3;
			v4 = v3 - (int) v3;
			h = v4 * 10000;
			h2 = cmt[i].h + h;
			if(h2>9999){
				h2-=10000;
				cmt[i].T += 1;
			}
			cmt[i].h = h2;
		}

		fprintf(fout, "%s\nP: %.6f\nq: %.6f\n----------\n", cmt[i].full, cmt[i].P, cmt[i].q);
		jul_to_greg(cmt[i].T, y, m, d);
		fprintf(fout, "%02d %02d %d\n", d, m, y);

		for (int j=1; j<=pass; j++){

			v1 = ((int) cmt[i].P) * 365.256363;
			// npr 2p encke ima period od ~3.28 g
			// 3 * 365 da dobijemo broj dana (3 = punih godina u jednom periodu)

			v2 = (cmt[i].P - (int) cmt[i].P) * 365.256363;
			// (3.28 - 3) = 0.28 * 365 da dobijemo onaj ostatak dana

			v3 = v1 + v2;
			// tu zbrojimo one dane, npr = 1299.4

			cmt[i].T += (int) v3;
			// i nadodamo ih na pocetni T

			v4 = v3 - (int) v3;
			// 1299.4 - 1299 = 0.4 da dobijemo sate

			h = v4 * 10000;
			// 0.4 * 10000 = 4000

			h2 = cmt[i].h + h;
			// 4951 + 4000 = 8951 i to je ok

			if(h2>9999){ // npr 7756 + 4000 = 11756

				h2-=10000;
				// 11756 - 10000 = 1756

				cmt[i].T += 1;
				// i T povecamo za 1
			}

			cmt[i].h = h2;
			// treba nam za svaki sljedeæi prolaz


			jul_to_greg(cmt[i].T, y, m, d);
			fprintf(fout, "%02d %02d %d\n", d, m, y);

		}
		fprintf(fout, "\n\n");
	}

	cout << "\n  Done\n\n  Press any key to continue... ";
	getch();

	return SUCCESS;
}

int tools_d(){

	int type, end, N;
	char dir, fout_name[80+1];
	double eAnomaly = 77;		//moram zadati neku pocetnu vrijednost
	double smAxis, mdMotion, mAnomaly, tAnomaly;
	double mic, w, v, r, l, x, y, z, vx, vy, vz;
	const double PI = 3.14159265;
	FILE *fout;

	do {
		clear_all();
		cout << "\n  Kepler orbit elements to Cartesian coordinates converter\n\n";
		cout << " =============================================================================\n";
		cout << "     1.   Main Menu   |   2.   Exit\n";
		cout << " =============================================================================\n\n";
		cout << "        a. Manually (One by one)\n";
		cout << "        b. Bulk (Read from file)\n\n";
		cout << "  Select option: ";
		cin >> dir;
		if(isupper(dir)) dir = tolower(dir);
	} while (dir!='a' && dir!='b' && dir!='1' && dir!='2');

	if (dir=='1') return MAIN_MENU;
	if (dir=='2') return EXIT_PROG;

	if (dir=='a'){

	}

	if (dir=='b'){
		do {
			type = -1;
			tools_soft_screen();
			scanf("%d", &type);
		} while (type<0 || type>19);

		N = import_main(type, 1);
		if (N==MAIN_MENU) return MAIN_MENU;
		if (N==EXIT_PROG) return EXIT_PROG;

		end = sort_data(N);
		if (end==MAIN_MENU) return MAIN_MENU;
		if (end==EXIT_PROG) return EXIT_PROG;

		clear_all();
		cout << "\n  Kepler orbit elements to Cartesian coordinates converter\n\n";
		cout << " =============================================================================\n";
		cout << "     1.   Main Menu   |   2.   Exit\n";
		cout << " =============================================================================\n\n";

		do {
			cout << "  Enter output filename: ";
			cin.getline(fout_name, 80);

			if (fout_name[0]=='1' && fout_name[1]=='\0') return MAIN_MENU;
			if (fout_name[0]=='2' && fout_name[1]=='\0') return EXIT_PROG;

			fout=fopen(fout_name, "a");
			if (fout==NULL) cout << "\n  Unable to create file " << fout_name << "\n\n";
		} while (fout==NULL);

		for (int i=0; i<N; i++){

			smAxis = cmt[i].q/(1-cmt[i].e);
			mdMotion = 0.9856076686/cmt[i].P;
			mAnomaly = -(mdMotion * (cmt[i].T - greg_to_jul(ep_y, ep_m, ep_d)));

			if (mAnomaly <   0) mAnomaly+=360;
			if (mAnomaly >=360) mAnomaly-=360;

			if (cmt[i].e>=1) cmt[i].e=0.999999;

			while (fabs(mAnomaly+(cmt[i].e*sin(eAnomaly))-eAnomaly) > 0.000001 ){
				eAnomaly =  eAnomaly + (mAnomaly+(cmt[i].e*sin(eAnomaly))-eAnomaly)/(1-(cmt[i].e*cos(eAnomaly)));
			}

			v = pow((1+cmt[i].e)/(1-cmt[i].e),0.5)*tan(eAnomaly/2);
			tAnomaly = 2*atan(v)*180/PI;

			r = smAxis*(1-(cmt[i].e*cos(eAnomaly)));

			w = (2*PI)/cmt[i].P;
			mic = pow(r,3)*pow(w,2);
//			mic = (4*pow(PI,2)*pow(smAxis,3))/pow(cmt[i].P,2);

			l = pow(mic*smAxis*(1-cmt[i].e*cmt[i].e),0.5);

			x = (r*(cos(cmt[i].an)*cos(cmt[i].pn+tAnomaly)))-(sin(cmt[i].an)*sin(cmt[i].pn+tAnomaly)*cos(cmt[i].i));
			y = (r*(sin(cmt[i].an)*cos(cmt[i].pn+tAnomaly)))+(cos(cmt[i].an)*sin(cmt[i].pn+tAnomaly)*cos(cmt[i].i));
			z = r*(sin(cmt[i].i)*sin(cmt[i].pn+tAnomaly));

			vx= (((x*l*cmt[i].e)/(r*cmt[i].P))*sin(tAnomaly))-((l/r)*((cos(cmt[i].an)*sin(cmt[i].pn+tAnomaly))+(sin(cmt[i].an)*cos(cmt[i].pn+tAnomaly)*cos(cmt[i].i))));
			vy= (((y*l*cmt[i].e)/(r*cmt[i].P))*sin(tAnomaly))-((l/r)*((sin(cmt[i].an)*sin(cmt[i].pn+tAnomaly))-(cos(cmt[i].an)*cos(cmt[i].pn+tAnomaly)*cos(cmt[i].i))));
			vz= (((z*l*cmt[i].e)/(r*cmt[i].P))*sin(tAnomaly))+((l/r)*(sin(cmt[i].i)*cos(cmt[i].pn+tAnomaly)));

			fprintf(fout, "%s\n%f\n%f\n%f\n%f\n%f\n%f\n%f\n\n", cmt[i].full, smAxis, cmt[i].e,
			cmt[i].i, cmt[i].an, cmt[i].pn, mAnomaly, tAnomaly);

			fprintf(fout, "x= %f\ny= %f\nz= %f\nvx=%f\nvy=%f\nvz=%f\n\n", x, y, z, vx, vy, vz);
		}
		cout << "\n  Done\n\n  Press any key to continue... ";
		getch();
	}

	return SUCCESS;
}

int tools_e(){

	char dir;

	do {
		clear_all();
		cout << "\n  Cartesian coordinates to Kepler orbit elements converter\n\n";
		cout << " =============================================================================\n";
		cout << "     1.   Main Menu   |   2.   Exit\n";
		cout << " =============================================================================\n\n";
		cout << "        a. Manually (One by one)\n";
		cout << "        b. Bulk (Read from file)\n\n";
		cout << "  Select option: ";
		cin >> dir;
		if(isupper(dir)) dir = tolower(dir);
	} while (dir!='a' && dir!='b' && dir!='1' && dir!='2');

	if (dir=='1') return MAIN_MENU;
	if (dir=='2') return EXIT_PROG;

	if (dir=='a'){

	}

	if (dir=='b'){

	}

	return SUCCESS;
}


void start_screen (){

	clear_all();

/*	// sve svima dostupno
	cout << "\n                            ORBITAL ELEMENTS WORKSHOP\n\n";
	cout << " =============================================================================\n\n";
	cout << "  Input formats:\n\n";
	cout << "        0. MPC                         12. MegaStar V4.x\n";
	cout << "        1. SkyMap                      13. SkyChart III\n";
	cout << "        2. Guide                       14. Voyager II\n";
	cout << "        3. xephem                      15. SkyTools\n";
	cout << "        4. Home Planet                 16. Autostar\n";
	cout << "        5. MyStars!\n";
	cout << "        6. TheSky                      17. Comet for Windows\n";
	cout << "        7. Starry Night                18. NASA (ELEMENTS.COMET)\n";
	cout << "        8. Deep Space                  19. NASA (CSV format)\n";
	cout << "        9. PC-TCS\n";
	cout << "       10. Earth Centered Universe     20. Tools\n";
	cout << "       11. Dance of the Planets        21. Exit\n\n";
	cout << "  Select option [0-21]: ";
*/
	// bez ona 3 tipa
	cout << "\n                            ORBITAL ELEMENTS WORKSHOP\n\n";
	cout << " =============================================================================\n\n";
	cout << "  Input formats:\n\n\n";
	cout << "        0. MPC                         10. Earth Centered Universe\n";
	cout << "        1. SkyMap                      11. Dance of the Planets\n";
	cout << "        2. Guide                       12. MegaStar V4.x\n";
	cout << "        3. xephem                      13. SkyChart III\n";
	cout << "        4. Home Planet                 14. Voyager II\n";
	cout << "        5. MyStars!                    15. SkyTools\n";
	cout << "        6. TheSky                      16. Autostar\n";
	cout << "        7. Starry Night\n";
	cout << "        8. Deep Space                  17. Tools\n";
	cout << "        9. PC-TCS                      18. Exit\n\n\n";
	cout << "  Select option [0-21]: ";
}

void screen_imp (char *import_format, char *soft){

	clear_all();
	cout << "\n  Importing " << import_format << " (" << soft << ") format...\n\n";
	cout << " =============================================================================\n";
	cout << "     1.   Main Menu   |   2.   Exit\n";
	cout << " =============================================================================\n\n";
}

void screen_exp1 (char *import_format){

	clear_all();
	cout << "\n  Exporting " << import_format << " format...\n\n";
	cout << " =============================================================================\n\n";
	cout << "  Output formats:\n\n";
	cout << "        0. MPC                         12. MegaStar V4.x\n";
	cout << "        1. SkyMap                      13. SkyChart III\n";
	cout << "        2. Guide                       14. Voyager II\n";
	cout << "        3. xephem                      15. SkyTools\n";
	cout << "        4. Home Planet                 16. Autostar\n";
	cout << "        5. MyStars!\n";
	cout << "        6. TheSky\n";
	cout << "        7. Starry Night                17. Celestia(SSC)\n";
	cout << "        8. Deep Space                  18. Stellarium\n";
	cout << "        9. PC-TCS\n";
	cout << "       10. Earth Centered Universe\n";
	cout << "       11. Dance of the Planets\n\n";
	cout << "  Select option [0-18]: ";
}

void screen_exp2 (char *import_format, char *export_format){

	clear_all();
	cout << "\n  Exporting " << import_format << " as " << export_format << " format...\n\n";
	cout << " =============================================================================\n";
	cout << "     1.   Main Menu   |   2.   Exit\n";
	cout << " =============================================================================\n\n";
}

void screen_exp3 (char *import_format, char *export_format){

	clear_all();
	cout << "\n  " << import_format << " exported as " << export_format << " format...\n\n";
	cout << " =============================================================================\n";
	cout << "     1.   Main Menu   |   2.   Exit\n";
	cout << " =============================================================================\n";
}

void exit_screen (){

	// http://patorjk.com/software/taag/
	// Font: Big

	clear_all();
	cout << "\n";
	cout << "                   ____           _       _   _             _ \n";
	cout << "                  / __ \\         | |     (_) | |           | |\n";
	cout << "                 | |  | |  _ __  | |__    _  | |_    __ _  | |\n";
	cout << "                 | |  | | | '__| | '_ \\  | | | __|  / _` | | |\n";
	cout << "                 | |__| | | |    | |_) | | | | |_  | (_| | | |\n";
	cout << "                  \\____/  |_|    |_.__/  |_|  \\__|  \\__,_| |_|\n\n";
	cout << "            ______   _                                     _         \n";
	cout << "           |  ____| | |                                   | |        \n";
	cout << "           | |__    | |   ___   _ __ ___     ___   _ __   | |_   ___ \n";
	cout << "           |  __|   | |  / _ \\ | '_ ` _ \\   / _ \\ | '_ \\  | __| / __|\n";
	cout << "           | |____  | | |  __/ | | | | | | |  __/ | | | | | |_  \\__ \\\n";
	cout << "           |______| |_|  \\___| |_| |_| |_|  \\___| |_| |_|  \\__| |___/\n\n";
	cout << "       __          __                _            _                     \n";
	cout << "       \\ \\        / /               | |          | |                    \n";
	cout << "        \\ \\  /\\  / /   ___    _ __  | | __  ___  | |__     ___    _ __  \n";
	cout << "         \\ \\/  \\/ /   / _ \\  | '__| | |/ / / __| | '_ \\   / _ \\  | '_ \\ \n";
	cout << "          \\  /\\  /   | (_) | | |    |   <  \\__ \\ | | | | | (_) | | |_) |\n";
	cout << "           \\/  \\/     \\___/  |_|    |_|\\_\\ |___/ |_| |_|  \\___/  | .__/\n";
	cout << "                                                                 | |\n";
    cout << "                                                                 |_|\n\n";
	cout << "Press any key to exit...                             Copyright (c) 2011, jurluk";
}

void excl_screen (){

	clear_all();
	cout << "\n";
	cout << "  Excluding comets...\n\n";
	cout << " =============================================================================\n";
}

void tools_soft_screen(){

	clear_all();
	cout << "\n  Perihelion passages calculator\n\n";
	cout << " =============================================================================\n\n";
	cout << "  Input formats:\n\n";
	cout << "        0. MPC                         12. MegaStar V4.x\n";
	cout << "        1. SkyMap                      13. SkyChart III\n";
	cout << "        2. Guide                       14. Voyager II\n";
	cout << "        3. xephem                      15. SkyTools\n";
	cout << "        4. Home Planet                 16. Autostar\n";
	cout << "        5. MyStars!\n";
	cout << "        6. TheSky                      17. Comet for Windows\n";
	cout << "        7. Starry Night                18. NASA (ELEMENTS.COMET)\n";
	cout << "        8. Deep Space                  19. NASA (CSV format)\n";
	cout << "        9. PC-TCS\n";
	cout << "       10. Earth Centered Universe\n";
	cout << "       11. Dance of the Planets\n\n";
	cout << "  Select option [0-19]: ";
}


int sort_data (int N){

	char dir, sortKey;

	do {
		clear_all();
		cout << "\n  Sorting data...\n\n";
		cout << " =============================================================================\n";
		cout << "     1.   Main Menu   |   2.   Exit\n";
		cout << " =============================================================================\n\n";
		cout << "  Sort by: \n\n";
		cout << "        a. Default\n";
		cout << "        b. Name\n";
		cout << "        c. Perihelion Date\n";
		cout << "        d. Pericenter Distance\n";
		cout << "        e. Eccentricity\n";
		cout << "        f. Longitude of the Ascending Node\n";
		cout << "        g. Longitude of Pericenter\n";
		cout << "        h. Inclination\n";
		cout << "        i. Period\n\n";
		cout << "  Select option [a-h]: ";
		cin >> sortKey;

		if(isupper(sortKey)) sortKey = tolower(sortKey);

	} while ((sortKey < 'a' || sortKey > 'i') && sortKey!='1' && sortKey!='2');

	if (sortKey=='1') return MAIN_MENU;
	if (sortKey=='2') return EXIT_PROG;

	if(sortKey > 'a'){
		do {
			clear_all();
			cout << "\n  Sorting data...\n\n";
			cout << " =============================================================================\n";
			cout << "     1.   Main Menu   |   2.   Exit   \n";
			cout << " =============================================================================\n\n";
			cout << "        a. Ascending\n";
			cout << "        b. Descending\n\n";
			cout << "  Select option: ";
			cin >> dir;
			if(isupper(dir)) dir = tolower(dir);
		} while (dir!='a' && dir!='b' && dir!='1' && dir!='2');
	}

	if (dir=='1') return MAIN_MENU;
	if (dir=='2') return EXIT_PROG;

	for (int i=0; i<N-1; i++){

		for (int j=i+1; j<N; j++){

			if (sortKey=='b' && dir=='a' && cmt[i].sort > cmt[j].sort) do_swap(i, j);
			if (sortKey=='b' && dir=='b' && cmt[i].sort < cmt[j].sort) do_swap(i, j);
			if (sortKey=='c' && dir=='a' && cmt[i].T > cmt[j].T) do_swap(i, j);
			if (sortKey=='c' && dir=='b' && cmt[i].T < cmt[j].T) do_swap(i, j);
			if (sortKey=='d' && dir=='a' && cmt[i].q > cmt[j].q) do_swap(i, j);
			if (sortKey=='d' && dir=='b' && cmt[i].q < cmt[j].q) do_swap(i, j);
			if (sortKey=='e' && dir=='a' && cmt[i].e > cmt[j].e) do_swap(i, j);
			if (sortKey=='e' && dir=='b' && cmt[i].e < cmt[j].e) do_swap(i, j);
			if (sortKey=='f' && dir=='a' && cmt[i].an > cmt[j].an) do_swap(i, j);
			if (sortKey=='f' && dir=='b' && cmt[i].an < cmt[j].an) do_swap(i, j);
			if (sortKey=='g' && dir=='a' && cmt[i].pn > cmt[j].pn) do_swap(i, j);
			if (sortKey=='g' && dir=='b' && cmt[i].pn < cmt[j].pn) do_swap(i, j);
			if (sortKey=='h' && dir=='a' && cmt[i].i > cmt[j].i) do_swap(i, j);
			if (sortKey=='h' && dir=='b' && cmt[i].i < cmt[j].i) do_swap(i, j);
			if (sortKey=='i' && dir=='a' && cmt[i].P > cmt[j].P) do_swap(i, j);
			if (sortKey=='i' && dir=='b' && cmt[i].P < cmt[j].P) do_swap(i, j);
		}
	}

	return SUCCESS;
}

void do_swap(int i, int j){

	Comet temp;
	int k;

	for(k=0;k<81;k++) {
		temp.full[k]=cmt[i].full[k];
	}
	for(k=0;k<81;k++) {
		cmt[i].full[k]=cmt[j].full[k];
	}
	for(k=0;k<81;k++) {
		cmt[j].full[k]=temp.full[k];
	}

	for(k=0;k<56;k++) {
		temp.name[k]=cmt[i].name[k];
	}
	for(k=0;k<56;k++) {
		cmt[i].name[k]=cmt[j].name[k];
	}
	for(k=0;k<56;k++) {
		cmt[j].name[k]=temp.name[k];
	}

	for(k=0;k<26;k++) {
		temp.ID[k]=cmt[i].ID[k];
	}
	for(k=0;k<26;k++) {
		cmt[i].ID[k]=cmt[j].ID[k];
	}
	for(k=0;k<26;k++) {
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
	}
	for(k=0;k<21;k++) {
		cmt[i].book[k]=cmt[j].book[k];
	}
	for(k=0;k<21;k++) {
		cmt[j].book[k]=temp.book[k];
	}
}
