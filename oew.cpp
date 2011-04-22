// Orbital Elements Workshop

#include "oew.hpp"
using namespace std;

struct Data{
	char full [80+1];
	char name [55+1];
	char ID [25+1];
	long int T;
	int y;
	int m;
	int d;
	int h;
	double P;
	float q;
	float e;
	float i;
	float an;
	float pn;
	float H;
	float G;
	char book [20+1];
} comet[MAX_CMT], temp;

struct Formats {
		char format[25];
		char soft[15];
} menu[22] = {
		{ format: "MPC", soft: "Soft00Cmt" },
		{ format: "SkyMap", soft: "Soft01Cmt" },
		{ format: "Guide", soft: "Soft02Cmt" },
		{ format: "xephem", soft: "Soft03Cmt" },
		{ format: "Home Planet", soft: "Soft04Cmt" },
		{ format: "MyStars!", soft: "Soft05Cmt" },
		{ format: "TheSky", soft: "Soft06Cmt" },
		{ format: "Starry Night", soft: "Soft07Cmt" },
		{ format: "Deep Space", soft: "Soft08Cmt" },
		{ format: "PC-TCS", soft: "Soft09Cmt" },
		{ format: "Earth Centered Universe", soft: "Soft10Cmt" },
		{ format: "Dance of the Planets", soft: "Soft11Cmt" },
		{ format: "MegaStar V4.x", soft: "Soft12Cmt" },
		{ format: "SkyChart III", soft: "Soft13Cmt" },
		{ format: "Voyager II", soft: "Soft14Cmt" },
		{ format: "SkyTools", soft: "Soft15Cmt" },
		{ format: "Autostar", soft: "Soft16Cmt" },
		{ format: "Comet for Windows", soft: "Comet.dat" },
		{ format: "NASA", soft: "ELEMENTS.COMET" },
		{ format: "NASA", soft: "CSV format" },
		{ format: "Celestia (SSC)", soft: "empty" },
		{ format: "Stellarium", soft: "empty" }
	};

struct Excludings{
	int key[14];
	long int T;
	float q;
	float e;
	float an;
	float pn;
	float i;
	float P;
}excl;


int main (){

	int type, end;

	system("COLOR 9");

	do {
		type=-1;
		start_screen();
		cin >> type;
		if (type >= 0 && type < 20) end = import_main(type);
		if (type == 20) help_screen();
	} while (type!=21 && end!=2);   			// vrti se sve dok na prvom izborniku nije upisan broj 21 ili na drugom broj 2

	exit_screen();
	getch();
	return 0;
}

int import_main (int Ty){

	int Ncmt=0, total_cmt, exp_ty, end;
	ifstream fin;

	char *soft, *import_format, *export_format;
	char fin_name[80+1], fout_name[80+1];

	import_format = menu[Ty].format;
	soft = menu[Ty].soft;

	screen_imp(import_format, soft);

    do {
        cout << "  Enter input filename: ";
        cin.getline(fin_name, 80);

        if (fin_name[0]=='1' && fin_name[1]=='\0') return 1;
        if (fin_name[0]=='2' && fin_name[1]=='\0') return 2;

        fin.open(fin_name, ios::in);
        if (fin==NULL) cout << "\n  Unable to open file " << fin_name << endl << endl;
    } while (fin==NULL);

	while ( !fin.eof() ){
		if(fin.get()=='\n') Ncmt++;
	}

	if (Ty==17) Ncmt/=13;						// jer je 17. format cfw, a jedan komet je definiran kroz 13 redova
	if (Ty==3 || Ty==8 || Ty==10) Ncmt/=2;		// kao gore, samo što je 1 komet kroz 2 reda

	total_cmt = Ncmt;

    cout << "\n  File " << fin_name << " is successfully opened" << endl;
	cout << "\n  Total detected comets: " << Ncmt << endl;
	cout << "\n  Press any key to continue... ";
	getch();

	end = define_exclude();
	if (end==1) return 1;
	if (end==2) return 2;

	if (Ty== 0) Ncmt = import_mpc (Ncmt, fin_name);
	if (Ty== 1) Ncmt = import_skymap (Ncmt, fin_name);
	if (Ty== 2) Ncmt = import_guide (Ncmt, fin_name);
	if (Ty== 3) Ncmt = import_xephem (Ncmt, fin_name);
	if (Ty== 4) Ncmt = import_home_planet (Ncmt, fin_name);
	if (Ty== 5) Ncmt = import_mystars (Ncmt, fin_name);
	if (Ty== 6 || Ty==16) Ncmt = import_thesky (Ncmt, fin_name); 	//jer imaju isti format
	if (Ty== 7) Ncmt = import_starry_night (Ncmt, fin_name);
	if (Ty== 8) Ncmt = import_deep_space (Ncmt, fin_name);
	if (Ty== 9) Ncmt = import_pc_tcs (Ncmt, fin_name);
	if (Ty==10) Ncmt = import_ecu (Ncmt, fin_name);
	if (Ty==11) Ncmt = import_dance (Ncmt, fin_name);
	if (Ty==12) Ncmt = import_megastar (Ncmt, fin_name);
	if (Ty==13) Ncmt = import_skychart (Ncmt, fin_name);
	if (Ty==14) Ncmt = import_voyager (Ncmt, fin_name);
	if (Ty==15) Ncmt = import_skytools (Ncmt, fin_name);
	if (Ty==17) Ncmt = import_cfw (Ncmt, fin_name);
	if (Ty==18) Ncmt = import_nasa1 (Ncmt, fin_name);
	if (Ty==19) Ncmt = import_nasa2 (Ncmt, fin_name);


	if (Ty==18 || Ty==19) cout << "\n =============================================================================";
	cout << endl << endl << "  Total imported comets: " << Ncmt << "/" << total_cmt << endl;
	cout << endl << "  Press any key to continue... ";
	getch();

	do {
		screen_exp1 (import_format);
		cin >> exp_ty;
	} while (exp_ty<0 || exp_ty>18);

	if (exp_ty==17) export_format = menu[exp_ty+3].format;
	else if (exp_ty==18) export_format = menu[exp_ty+3].format;
	else export_format = menu[exp_ty].format;				//koristi se u screen_exp2 i screen_exp3

	end = sort_data(Ncmt);
	if (end==1) return 1;
	if (end==2) return 2;

	screen_exp2(import_format, export_format);
	cout << "  Enter output filename: ";
	cin.getline(fout_name, 80);

	if (fout_name[0]=='1' && fout_name[1]=='\0') return 1;
	if (fout_name[0]=='2' && fout_name[1]=='\0') return 2;

	if (exp_ty== 0) export_mpc (Ncmt, fout_name);
	if (exp_ty== 1) export_skymap (Ncmt, fout_name);
	if (exp_ty== 2) export_guide (Ncmt, fout_name);
	if (exp_ty== 3) export_xephem (Ncmt, fout_name);
	if (exp_ty== 4) export_home_planet (Ncmt, fout_name);
	if (exp_ty== 5) export_mystars (Ncmt, fout_name);
	if (exp_ty== 6 || exp_ty==16) export_thesky (Ncmt, fout_name);
	if (exp_ty== 7) export_starry_night (Ncmt, fout_name);
	if (exp_ty== 8) export_deep_space (Ncmt, fout_name);
	if (exp_ty== 9) export_pc_tcs (Ncmt, fout_name);
	if (exp_ty==10) export_ecu (Ncmt, fout_name);
	if (exp_ty==11) export_dance (Ncmt, fout_name);
	if (exp_ty==12) export_megastar (Ncmt, fout_name);
	if (exp_ty==13) export_skychart (Ncmt, fout_name);
	if (exp_ty==14) export_voyager (Ncmt, fout_name);
	if (exp_ty==15) export_skytools (Ncmt, fout_name);
	if (exp_ty==17) export_ssc  (Ncmt, Ty, fout_name);
	if (exp_ty==18) export_stell  (Ncmt, Ty, fout_name);

	do {
		screen_exp3(import_format, export_format);
		cout << "\n  Done" << endl << endl << "  " <<  Ncmt  << " comets successfully saved in file " << fout_name << endl << endl;
		cout << "  Select option (1/2): ";
		cin >> end;
	} while (end!=1 && end!=2);

	fin.close();
	return end;
}


int define_exclude(){

	int i, y, m, d;
	char exclKey;

	for (i=0; i<14; i++) excl.key[i]=0;

	do {

		do {
			system("CLS");
			cout << endl << "  Excluding data..." << endl << endl;
			cout << " =============================================================================" << endl;
			cout << "     1.   Main Menu   |   2.   Exit   " << endl;
			cout << " =============================================================================" << endl << endl;
			cout << "  Exclude by: " << endl << endl;
			cout << "        Perihelion Date                  a. Greather than    b. Less than" << endl;
			cout << "        Pericenter Distance              c. Greather than    d. Less than" << endl;
			cout << "        Eccentricity                     e. Greather than    f. Less than" << endl;
			cout << "        Long. of the Ascending Node      g. Greather than    h. Less than" << endl;
			cout << "        Long. of Pericenter              i. Greather than    j. Less than" << endl;
			cout << "        Inclination                      k. Greather than    l. Less than" << endl;
			cout << "        Period                           m. Greather than    n. Less than" << endl << endl;
			cout << "        Type \"x\" to continue" << endl << endl;
			cout << "  Select option [a-n]: ";
			cin >> exclKey;

			if(isupper(exclKey)) exclKey = tolower(exclKey);

		} while ((exclKey < 'a' || exclKey > 'n') && exclKey!='x' && exclKey!='1' && exclKey!='2');

		if (exclKey=='1') return 1;
		if (exclKey=='2') return 2;

		if (exclKey=='a'){
			excl_screen ();
			printf("  Exclude comet if Perihelion Date is greather than (DD MM YYYY):  ");
			cin >> d >> m >> y;

			excl.T = compute_T(y, m, d);
			excl.key[0]=1;
			if(excl.key[1]==1) excl.key[1]=0;
		}

		if (exclKey=='b'){
			excl_screen ();
			printf("  Exclude comet if Perihelion Date is less than (DD MM YYYY):  ");
			cin >> d >> m >> y;

			excl.T = compute_T(y, m, d);
			excl.key[1]=1;
			if(excl.key[0]==1) excl.key[0]=0;
		}

		if (exclKey=='c'){
			excl_screen ();
			printf("  Exclude comet if Perihelion distance is greather than:       AU\b\b\b\b\b\b\b\b");
			cin >> excl.q;
			excl.key[2]=1;
			if(excl.key[3]==1) excl.key[3]=0;
		}

		if (exclKey=='d'){
			excl_screen ();
			printf("  Exclude comet if Perihelion distance is less than:       AU\b\b\b\b\b\b\b\b");
			cin >> excl.q;
			excl.key[3]=1;
			if(excl.key[2]==1) excl.key[2]=0;
		}

		if (exclKey=='e'){
			excl_screen ();
			printf("  Exclude comet if Eccentricity is greather than: ");
			cin >> excl.e;
			excl.key[4]=1;
			if(excl.key[5]==1) excl.key[5]=0;
		}

		if (exclKey=='f'){
			excl_screen ();
			printf("  Exclude comet if Eccentricity is less than: ");
			cin >> excl.e;
			excl.key[5]=1;
			if(excl.key[4]==1) excl.key[4]=0;
		}

		if (exclKey=='g'){
			excl_screen ();
			printf("  Exclude comet if Long. of the Ascending Node is greather than:      degrees\b\b\b\b\b\b\b\b\b\b\b\b");
			cin >> excl.an;
			excl.key[6]=1;
			if(excl.key[7]==1) excl.key[7]=0;
		}

		if (exclKey=='h'){
			excl_screen ();
			printf("  Exclude comet if Long. of the Ascending Node is less than:      degrees\b\b\b\b\b\b\b\b\b\b\b\b");
			cin >> excl.an;
			excl.key[7]=1;
			if(excl.key[6]==1) excl.key[6]=0;
		}

		if (exclKey=='i'){
			excl_screen ();
			printf("  Exclude comet if Long. of Pericenter is greather than:      degrees\b\b\b\b\b\b\b\b\b\b\b\b");
			cin >> excl.pn;
			excl.key[8]=1;
			if(excl.key[9]==1) excl.key[9]=0;
		}

		if (exclKey=='j'){
			excl_screen ();
			printf("  Exclude comet if Long. of Pericenter is less than:      degrees\b\b\b\b\b\b\b\b\b\b\b\b");
			cin >> excl.pn;
			excl.key[9]=1;
			if(excl.key[8]==1) excl.key[8]=0;
		}

		if (exclKey=='k'){
			excl_screen ();
			printf("  Exclude comet if Inclination is greather than:      degrees\b\b\b\b\b\b\b\b\b\b\b\b");
			cin >> excl.i;
			excl.key[10]=1;
			if(excl.key[11]==1) excl.key[11]=0;
		}

		if (exclKey=='l'){
			excl_screen ();
			printf("  Exclude comet if Inclination is less than:      degrees\b\b\b\b\b\b\b\b\b\b\b\b");
			cin >> excl.i;
			excl.key[11]=1;
			if(excl.key[10]==1) excl.key[10]=0;
		}

		if (exclKey=='m'){
			excl_screen ();
			printf("  Exclude comet if Period is greather than:      years\b\b\b\b\b\b\b\b\b\b");
			cin >> excl.P;
			excl.key[12]=1;
			if(excl.key[13]==1) excl.key[13]=0;
		}

		if (exclKey=='n'){
			excl_screen ();
			printf("  Exclude comet if Period is less than:      years\b\b\b\b\b\b\b\b\b\b");
			cin >> excl.P;
			excl.key[13]=1;
			if(excl.key[12]==1) excl.key[12]=0;
		}

	} while (exclKey!='x');

	system("CLS");
	cout << endl << "  Excludings..." << endl << endl;
	cout << " =============================================================================" << endl << endl;

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
}

int do_exclude(int i){

	if (excl.key[0]==1){
		if (comet[i].T > excl.T) return 1;
	}

	if (excl.key[1]==1){
		if (comet[i].T < excl.T) return 1;
	}

	if (excl.key[2]==1){
		if (comet[i].q > excl.q) return 1;
	}

	if (excl.key[3]==1){
		if (comet[i].q < excl.q) return 1;
	}

	if (excl.key[4]==1){
		if (comet[i].e > excl.e) return 1;
	}

	if (excl.key[5]==1){
		if (comet[i].e < excl.e) return 1;
	}

	if (excl.key[6]==1){
		if (comet[i].an > excl.an) return 1;
	}

	if (excl.key[7]==1){
		if (comet[i].an < excl.an) return 1;
	}

	if (excl.key[8]==1){
		if (comet[i].pn > excl.pn) return 1;
	}

	if (excl.key[9]==1){
		if (comet[i].pn < excl.pn) return 1;
	}

	if (excl.key[10]==1){
		if (comet[i].i > excl.i) return 1;
	}

	if (excl.key[11]==1){
		if (comet[i].i < excl.i) return 1;
	}

	if (excl.key[12]==1){
		if (comet[i].P > excl.P) return 1;
	}

	if (excl.key[13]==1){
		if (comet[i].P < excl.P) return 1;
	}
}


int import_mpc (int N, char *fin_name){

	int i, j, k, l;
	int ex, m, line=1;
	char x[30+1];
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {

		m = fscanf(fin, "%14c %d %02d %02d.%04d %f %f %f %f %f%12c%f %f %55c %30[^\n]%*c",		// %f%12c%f mora bit tako zajedno
			x, &comet[i].y, &comet[i].m, &comet[i].d, &comet[i].h,
			&comet[i].q, &comet[i].e, &comet[i].pn, &comet[i].an,
			&comet[i].i, x, &comet[i].H, &comet[i].G, comet[i].full, x);

		if (m < 15){
			printf("\n\n  Unable to read data in line %d", line);
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		edit_name(comet[i].full);

		for (j=0; comet[i].full[j]!='\0'; j++){
			if ((isdigit(comet[i].full[j]) && comet[i].full[j+1]=='P' && comet[i].full[j+2]=='/') ||
				(isdigit(comet[i].full[j]) && comet[i].full[j+1]=='D' && comet[i].full[j+2]=='/')){

				for(k=0; comet[i].full[k]!='/'; k++)
					comet[i].ID[k]=comet[i].full[k];

				comet[i].ID[k]='\0';
				++k;
				for(l=0; comet[i].full[k]!='\0'; l++, k++)
					comet[i].name[l]=comet[i].full[k];

				comet[i].name[l]='\0';
			}

			if (comet[i].full[j]=='('){
				for(k=0; comet[i].full[k]!='('; k++)
					comet[i].ID[k]=comet[i].full[k];

				comet[i].ID[k-1]='\0';

				++k;
				for(l=0; comet[i].full[k]!=')'; k++, l++)
					comet[i].name[l]=comet[i].full[k];

				comet[i].name[l]='\0';
			}
		}

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].T = compute_T (comet[i].y, comet[i].m, comet[i].d);
		line++;

		ex = do_exclude(i);
		if (ex == 1) { N--; i--; }
	}

	return N;
}

int import_skymap (int N, char *fin_name){

	int i, j, k, l, u, t, space;
	int ex, m, line=1;
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {

		m = fscanf(fin, "%47c %4d %2d %2d.%4d %f %f %f %f %f %f %f\n",
			comet[i].full, &comet[i].y, &comet[i].m, &comet[i].d,
			&comet[i].h, &comet[i].q, &comet[i].e, &comet[i].pn,
			&comet[i].an, &comet[i].i, &comet[i].H, &comet[i].G);

		if (m < 12){
			printf("\n\n  Unable to read data in line %d", line);
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		edit_name(comet[i].full);

		for (j=0; comet[i].full[j]!='\0'; j++){
			if ((isdigit(comet[i].full[j]) && comet[i].full[j+1]=='P' && comet[i].full[j+2]==' ') ||
				(isdigit(comet[i].full[j]) && comet[i].full[j+1]=='D' && comet[i].full[j+2]==' ')){

				for(k=0; comet[i].full[k]!=' '; k++)
					comet[i].ID[k]=comet[i].full[k];

				comet[i].ID[k]='\0';
				++k;
				for(l=0; comet[i].full[k]!='\0'; l++, k++)
					comet[i].name[l]=comet[i].full[k];

				comet[i].name[l]='\0';
			}

		if ((comet[i].full[0]=='C' && comet[i].full[1]=='/') ||
			(comet[i].full[0]=='P' && comet[i].full[1]=='/') ||
			(comet[i].full[0]=='D' && comet[i].full[1]=='/')){
				space=0;
				for(u=0; u<strlen(comet[i].full); u++){
					if (comet[i].full[u]==' ' && space==1) {
						t=u;
						break;
					}
					else if(comet[i].full[u]==' ') space++;
				}

				for(k=0; k<t; k++)
					comet[i].ID[k]=comet[i].full[k];

				comet[i].ID[k]='\0';

				++k;
				for(l=0; comet[i].full[k]!='\0'; k++, l++)
					comet[i].name[l]=comet[i].full[k];

				comet[i].name[l]='\0';
			}
		}

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].T = compute_T (comet[i].y, comet[i].m, comet[i].d);
		line++;

		ex = do_exclude(i);
		if (ex == 1) { N--; i--; }
	}

	return N;
}

int import_guide (int N, char *fin_name){

	int i, j, ex, m, line=1;
	char c, x[20];
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {

		j=0;
		while ((c=fgetc(fin)) != '(' ){
			comet[i].name[j++]=c;
		}
		comet[i].name[j-1]='\0';

		j=0;
		while ((c=fgetc(fin)) != ')' ){
			comet[i].ID[j++]=c;
		}
		comet[i].ID[j]='\0';


		if ((comet[i].name[0]=='P' && comet[i].name[1]=='/') ||
			(comet[i].name[0]=='D' && comet[i].name[1]=='/')){
			for (j=0; j<strlen(comet[i].name); j++)
				comet[i].name[j]=comet[i].name[j+2];		//j+2 jer je na mjestu 0='P', 1='/'

			strcpy(comet[i].full, comet[i].ID);
			strcat(comet[i].full, "/");
//			edit_name(comet[i].name);
			strcat(comet[i].full, comet[i].name);
		}

		else {
			strcpy(comet[i].full, comet[i].ID);
			strcat(comet[i].full, " (");
//			edit_name(comet[i].name);
			strcat(comet[i].full, comet[i].name);
			strcat(comet[i].full, ")");
		}

		m = fscanf(fin, "%d.%d %d %d 0.0 %f %f %f %f %f 2000.0 %f %f %15[^\n]%*c",
			&comet[i].d, &comet[i].h, &comet[i].m, &comet[i].y,
			&comet[i].q,  &comet[i].e, &comet[i].i, &comet[i].pn,
			&comet[i].an, &comet[i].H, &comet[i].G, x);

		if (m < 12){
			printf("\n\n  Unable to read data in line %d", line);
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].T = compute_T (comet[i].y, comet[i].m, comet[i].d);
		line++;

		ex = do_exclude(i);
		if (ex == 1) { N--; i--; }
	}

	return N;
}

int import_xephem (int N, char *fin_name){

	//info: http://www.clearskyinstitute.com/xephem/help/xephem.html#mozTocId215848

	int i, j, k, l, ex;
	long int T;
	int nula, m, line=2;
	float smAxis, mdMotion, mAnomaly;
	char c, x[25+1];
	FILE *fin = fopen(fin_name, "r");

// 	varijable za izracun gregorijanskog datuma iz julijanskog dana
	int mm, dd, yy, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13;

	for (i=0; i<N; i++) {

		fscanf(fin, "# From %25[^\n]%*c", x);

		j=0;
		while ((c=fgetc(fin)) != ',' ){
			comet[i].full[j++]=c;
		}

		for (j=0; comet[i].full[j]!='\0'; j++){
			if (isdigit(comet[i].full[j]) &&
				comet[i].full[j+1]=='P' &&
				comet[i].full[j+2]=='/'){

				for(k=0; comet[i].full[k]!='/'; k++)
					comet[i].ID[k]=comet[i].full[k];

				comet[i].ID[k]='\0';
				++k;
				for(l=0; comet[i].full[k]!='\0'; l++, k++)
					comet[i].name[l]=comet[i].full[k];

				comet[i].name[l]='\0';
			}

			if (comet[i].full[j]=='('){
				for(k=0; comet[i].full[k]!='('; k++)
					comet[i].ID[k]=comet[i].full[k];

				comet[i].ID[k-1]='\0';

				++k;
				for(l=0; comet[i].full[k]!=')'; k++, l++)
					comet[i].name[l]=comet[i].full[k];

				comet[i].name[l]='\0';
			}
		}

		c=fgetc(fin);

		if(c == 'e'){
			m = fscanf(fin, ",%f,%f,%f,%f,%f,%f,%f,%d/%d.%d/%d,2000,g %f,%f\n",
				&comet[i].i, &comet[i].an, &comet[i].pn, &smAxis,
				&mdMotion, &comet[i].e, &mAnomaly, &mm, &dd,
				&nula, &yy, &comet[i].H, &comet[i].G);

			if (m < 13){
				printf("\n\n  Unable to read data in line %d", line);
				fscanf(fin, "%*[^\n]\n");
				N--; i--; line++;
				continue;
			}

			comet[i].q = smAxis*(1-comet[i].e);
			T = compute_T (yy, mm, dd);
			comet[i].T = T - mAnomaly/mdMotion;

			v1 = comet[i].T + 0.5;
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
			comet[i].y = v12 - 4800 + (v11 + 2) / 12;
			comet[i].m = (v11 + 2) % 12 + 1;
			comet[i].d = v13 + 1;

			line+=2;

			ex = do_exclude(i);
			if (ex == 1) { N--; i--; }
		}

		if(c == 'p'){
			m = fscanf(fin, ",%d/%d.%d/%d,%f,%f,%f,%f,2000,%f,%f\n",
				&comet[i].m, &comet[i].d, &comet[i].h, &comet[i].y,
				&comet[i].i, &comet[i].pn, &comet[i].q, &comet[i].an,
				&comet[i].H, &comet[i].G);

			if (m < 10){
				printf("\n\n  Unable to read data in line %d", line);
				fscanf(fin, "%*[^\n]\n");
				N--; i--; line++;
				continue;
			}

			comet[i].e = 1.000000;
			comet[i].T = compute_T (comet[i].y, comet[i].m, comet[i].d);
			line+=2;

			ex = do_exclude(i);
			if (ex == 1) { N--; i--; }
		}

		if(c == 'h'){
			m = fscanf(fin, ",%d/%d.%d/%d,%f,%f,%f,%f,%f,2000,%f,%f\n",
				&comet[i].m, &comet[i].d, &comet[i].h, &comet[i].y,
				&comet[i].i, &comet[i].an, &comet[i].pn, &comet[i].e,
				&comet[i].q, &comet[i].H, &comet[i].G);

			if (m < 11){
				printf("\n\n  Unable to read data in line %d", line);
				fscanf(fin, "%*[^\n]\n");
				N--; i--; line++;
				continue;
			}

			comet[i].T = compute_T (comet[i].y, comet[i].m, comet[i].d);
			line+=2;

			ex = do_exclude(i);
			if (ex == 1) { N--; i--; }
		}

		comet[i].P = compute_period (comet[i].q, comet[i].e);
	}

	return N;
}

int import_home_planet (int N, char *fin_name){

	int i, j, k, l;
	int ex, m, line=1;
	char c, x[50+1];
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {

		j=0;
		while ((c=fgetc(fin)) != ',' ){
			comet[i].full[j++]=c;
		}

		m = fscanf(fin, "%d-%d-%d.%d,%f,%f,%f,%f,%f,%50[^\n]%*c",
			&comet[i].y, &comet[i].m, &comet[i].d, &comet[i].h,
			&comet[i].q, &comet[i].e, &comet[i].pn, &comet[i].an,
			&comet[i].i, x);

		if (m < 10){
			printf("\n\n  Unable to read data in line %d", line);
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		for (j=0; comet[i].full[j]!='\0'; j++){
			if (isdigit(comet[i].full[j]) &&
				comet[i].full[j+1]=='P' &&
				comet[i].full[j+2]=='/'){

				for(k=0; comet[i].full[k]!='/'; k++)
					comet[i].ID[k]=comet[i].full[k];

				comet[i].ID[k]='\0';
				++k;
				for(l=0; comet[i].full[k]!='\0'; l++, k++)
					comet[i].name[l]=comet[i].full[k];

				comet[i].name[l]='\0';
			}

			if (comet[i].full[j]=='('){
				for(k=0; comet[i].full[k]!='('; k++)
					comet[i].ID[k]=comet[i].full[k];

				comet[i].ID[k-1]='\0';

				++k;
				for(l=0; comet[i].full[k]!=')'; k++, l++)
					comet[i].name[l]=comet[i].full[k];

				comet[i].name[l]='\0';
			}
		}

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].T = compute_T (comet[i].y, comet[i].m, comet[i].d);
//		edit_name (comet[i].name);
		line++;

		ex = do_exclude(i);
		if (ex == 1) { N--; i--; }
	}

	return N;
}

int import_mystars (int N, char *fin_name){

	int i, j, k, l;
	int ex, m, line=1;
	char c, x[30+1];
	FILE *fin = fopen(fin_name, "r");

// 	varijable za izracun gregorijanskog datuma iz julijanskog dana
	int v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13;

	for (i=0; i<N; i++) {

		j=0;
		while ((c=fgetc(fin)) != ';' ){
			comet[i].full[j++]=c;
		}

		m = fscanf(fin, "%d.%d %f %f %f %f %f %f %f %30[^\n]%*c",
			&comet[i].T, &comet[i].h, &comet[i].pn, &comet[i].e,
			&comet[i].q, &comet[i].i, &comet[i].an, &comet[i].H,
			&comet[i].G, x);

		if (m < 10){
			printf("\n\n  Unable to read data in line %d", line);
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		for (j=0; comet[i].full[j]!='\0'; j++){
			if (isdigit(comet[i].full[j]) &&
				comet[i].full[j+1]=='P' &&
				comet[i].full[j+2]=='/'){

				for(k=0; comet[i].full[k]!='/'; k++)
					comet[i].ID[k]=comet[i].full[k];

				comet[i].ID[k]='\0';
				++k;
				for(l=0; comet[i].full[k]!='\0'; l++, k++)
					comet[i].name[l]=comet[i].full[k];

				comet[i].name[l]='\0';
			}

			if (comet[i].full[j]=='('){
				for(k=0; comet[i].full[k]!='('; k++)
					comet[i].ID[k]=comet[i].full[k];

				comet[i].ID[k-1]='\0';

				++k;
				for(l=0; comet[i].full[k]!=')'; k++, l++)
					comet[i].name[l]=comet[i].full[k];

				comet[i].name[l]='\0';
			}
		}

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].T += 2400000;
		line++;

//		izracuvanje gregorijanskog datuma iz julijanskog dana
//		izvor: http://en.wikipedia.org/wiki/Julian_day#Gregorian_calendar_from_Julian_day_number

		v1 = comet[i].T + 0.5;
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
        comet[i].y = v12 - 4800 + (v11 + 2) / 12;
		comet[i].m = (v11 + 2) % 12 + 1;
		comet[i].d = v13 + 1;

		ex = do_exclude(i);
		if (ex == 1) { N--; i--; }
	}

	return N;
}

int import_thesky (int N, char *fin_name){

	int i, j, k, l;
	int ex, m, line=1;
	char x[20+1];
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {
//		fscanf(fin, "%40c %*c %d %*c %4d %2d %2d %*c %d %*c %f %*c %f %*c %f %*c %f %*c %f %*c %f %25[^\n]%*c",     stari nacin
		m = fscanf(fin, "%45c %4d%2d%2d.%d | %f | %f | %f | %f | %f | %f | %f %20[^\n]%*c",
			comet[i].full, &comet[i].y, &comet[i].m,
			&comet[i].d, &comet[i].h, &comet[i].q, &comet[i].e,
			&comet[i].pn, &comet[i].an, &comet[i].i, &comet[i].H,
			&comet[i].G, x);

		if (m < 13){
			printf("\n\n  Unable to read data in line %d", line);
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		edit_name(comet[i].full);

		for (j=0; comet[i].full[j]!='\0'; j++){
			if (isdigit(comet[i].full[j]) &&
				comet[i].full[j+1]=='P' &&
				comet[i].full[j+2]=='/'){

				for(k=0; comet[i].full[k]!='/'; k++)
					comet[i].ID[k]=comet[i].full[k];

				comet[i].ID[k]='\0';
				++k;
				for(l=0; comet[i].full[k]!='\0'; l++, k++)
					comet[i].name[l]=comet[i].full[k];

				comet[i].name[l]='\0';
			}

			if (comet[i].full[j]=='('){
				for(k=0; comet[i].full[k]!='('; k++)
					comet[i].ID[k]=comet[i].full[k];

				comet[i].ID[k-1]='\0';

				++k;
				for(l=0; comet[i].full[k]!=')'; k++, l++)
					comet[i].name[l]=comet[i].full[k];

				comet[i].name[l]='\0';
			}
		}

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].T = compute_T (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].name);
		line++;

		ex = do_exclude(i);
		if (ex == 1) { N--; i--; }
	}

	return N;
}

int import_starry_night (int N, char *fin_name){

	int i, j, k;
	int ex, m, line=1;
	long int y;
	char c, x[20+1];
	FILE *fin = fopen(fin_name, "r");

// 	varijable za izracun gregorijanskog datuma iz julijanskog dana
	int v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13;

	for (i=0; i<N; i++) {

		j=0; k=0;
		while (k<34){
			c=fgetc(fin);
			comet[i].name[j]=c;
			if (c==' ' && j==0) --j;
			j++; k++;
		}

		edit_name(comet[i].name);

		m = fscanf(fin, "%f 0.0 %f %f %f %f %f %d.%d %d.5 %f",
			&comet[i].H, &comet[i].e, &comet[i].q, &comet[i].an,
			&comet[i].pn, &comet[i].i, &comet[i].T, &comet[i].h,
			&y, &comet[i].G);

		if (m < 10){
			printf("\n\n  Unable to read data in line %d", line);
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		j=0; k=0;
		while (k<16){
			c=fgetc(fin);
			comet[i].ID[j]=c;
			if (c==' ' && j==0) --j;
			j++; k++;
		}

		edit_name(comet[i].ID);

		fscanf(fin, "%20[^\n]%*c", x);

		if ((comet[i].ID[0]=='C' && comet[i].ID[1]=='/') ||
			(comet[i].ID[0]=='P' && comet[i].ID[1]=='/')){

			strcpy(comet[i].full, comet[i].ID);
			strcat(comet[i].full, " (");
			strcat(comet[i].full, comet[i].name);
			strcat(comet[i].full, ")");
		}

		else {
			strcpy(comet[i].full, comet[i].ID);
			strcat(comet[i].full, "/");
			strcat(comet[i].full, comet[i].name);
		}


		comet[i].P = compute_period (comet[i].q, comet[i].e);
		line++;

//		izracuvanje gregorijanskog datuma iz julijanskog dana
//		izvor: http://en.wikipedia.org/wiki/Julian_day#Gregorian_calendar_from_Julian_day_number

		v1 = comet[i].T + 0.5;
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
        comet[i].y = v12 - 4800 + (v11 + 2) / 12;
		comet[i].m = (v11 + 2) % 12 + 1;
		comet[i].d = v13 + 1;

		ex = do_exclude(i);
		if (ex == 1) { N--; i--; }
	}

	return N;
}

int import_deep_space (int N, char *fin_name){

	int i, j, ex, m, line=2;
	char c, x[8+1];
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {

		j=0;
		while ((c=fgetc(fin)) != '(' ){
			comet[i].name[j++]=c;
		}
		comet[i].name[j-1]='\0';

		j=0;
		while ((c=fgetc(fin)) != ')' ){
			comet[i].ID[j++]=c;
		}
		comet[i].ID[j]='\0';

		m = fscanf(fin, "\n%8c %d %d %d.%d %f %f %f %f %f %f %f\n",
			x, &comet[i].y, &comet[i].m, &comet[i].d, &comet[i].h,
			&comet[i].q, &comet[i].e, &comet[i].pn, &comet[i].an,
			&comet[i].i, &comet[i].H, &comet[i].G);

		if (m < 12){
			printf("\n\n  Unable to read data in line %d", line);
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		if ((comet[i].ID[0]=='C' && comet[i].ID[1]=='/') ||
			(comet[i].ID[0]=='P' && comet[i].ID[1]=='/')){

			strcpy(comet[i].full, comet[i].ID);
			strcat(comet[i].full, " (");
			strcat(comet[i].full, comet[i].name);
			strcat(comet[i].full, ")");
		}

		else {
			strcpy(comet[i].full, comet[i].ID);
			strcat(comet[i].full, "/");
			strcat(comet[i].full, comet[i].name);
		}

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].T = compute_T (comet[i].y, comet[i].m, comet[i].d);
		line+=2;

		ex = do_exclude(i);
		if (ex == 1) { N--; i--; }
	}

	return N;
}

int import_pc_tcs (int N, char *fin_name){

	int i, j, k;
	int ex, m, line=1;
	char tempID[20];
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {

		m = fscanf(fin, "%s %f %f %f %f %f %d %d %d.%d %f %f %60[^\n]%*c",
			comet[i].ID, &comet[i].q, &comet[i].e, &comet[i].i,
			&comet[i].pn, &comet[i].an, &comet[i].y, &comet[i].m,
			&comet[i].d, &comet[i].h, &comet[i].H, &comet[i].G, comet[i].name);

		if (m < 13){
			printf("\n\n  Unable to read data in line %d", line);
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		for (j=0; j<20; j++) tempID[j]='\0';

		if ((comet[i].ID[0]=='C' && comet[i].ID[1]=='/') ||
			(comet[i].ID[0]=='P' && comet[i].ID[1]=='/')){

			for(j=6, k=0; comet[i].ID[j]!='\0'; j++, k++)
				tempID[k]=comet[i].ID[j];

			for(j=6; j<strlen(comet[i].ID); j++)
				comet[i].ID[j]=' ';

			edit_name(comet[i].ID);

			strcat(comet[i].ID, " ");
			strcat(comet[i].ID, tempID);
		}

		edit_name (comet[i].name);

		if ((comet[i].ID[0]=='C' && comet[i].ID[1]=='/') ||
			(comet[i].ID[0]=='P' && comet[i].ID[1]=='/')){

			strcpy(comet[i].full, comet[i].ID);
			strcat(comet[i].full, " (");
			strcat(comet[i].full, comet[i].name);
			strcat(comet[i].full, ")");
		}

		else {
			strcpy(comet[i].full, comet[i].ID);
			strcat(comet[i].full, "/");
			strcat(comet[i].full, comet[i].name);
		}

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].T = compute_T (comet[i].y, comet[i].m, comet[i].d);
		line++;

		ex = do_exclude(i);
		if (ex == 1) { N--; i--; }
	}

	return N;
}

int import_ecu (int N, char *fin_name){

	int i, j, k, l;
	int ex, m, line=2;
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {

		m = fscanf(fin, "%45[^\n]%*cE C 2000 %d %d %d.%d %f %f %f %f %f %f %f\n",
			comet[i].full, &comet[i].y, &comet[i].m, &comet[i].d,
			&comet[i].h, &comet[i].q, &comet[i].e, &comet[i].pn, &comet[i].an,
			&comet[i].i, &comet[i].H, &comet[i].G);

		if (m < 12){
			printf("\n\n  Unable to read data in line %d", line);
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		edit_name(comet[i].full);

		for (j=0; comet[i].full[j]!='\0'; j++){
			if (isdigit(comet[i].full[j]) &&
				comet[i].full[j+1]=='P' &&
				comet[i].full[j+2]=='/'){

				for(k=0; comet[i].full[k]!='/'; k++)
					comet[i].ID[k]=comet[i].full[k];

				comet[i].ID[k]='\0';
				++k;
				for(l=0; comet[i].full[k]!='\0'; l++, k++)
					comet[i].name[l]=comet[i].full[k];

				comet[i].name[l]='\0';
			}

			if (comet[i].full[j]=='('){
				for(k=0; comet[i].full[k]!='('; k++)
					comet[i].ID[k]=comet[i].full[k];

				comet[i].ID[k-1]='\0';

				++k;
				for(l=0; comet[i].full[k]!=')'; k++, l++)
					comet[i].name[l]=comet[i].full[k];

				comet[i].name[l]='\0';
			}
		}

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].T = compute_T (comet[i].y, comet[i].m, comet[i].d);
		line+=2;

		ex = do_exclude(i);
		if (ex == 1) { N--; i--; }
	}

	return N;
}

int import_dance (int N, char *fin_name){

	int i, j, k;
	int ex, m, line=1;
	char tempID[20];
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {

		m = fscanf(fin, "%11c %f %f %f %f %f %d.%2d%2d%4d %30[^\n]%*c",
			comet[i].ID, &comet[i].q, &comet[i].e, &comet[i].i,
			&comet[i].an, &comet[i].pn, &comet[i].y, &comet[i].m,
			&comet[i].d, &comet[i].h, comet[i].name);

		if (m < 11){
			printf("\n\n  Unable to read data in line %d", line);
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		edit_name (comet[i].ID);
		edit_name (comet[i].name);

		for (j=0; j<20; j++) tempID[j]='\0';

		if ((comet[i].ID[0]=='C' && comet[i].ID[1]=='/') ||
			(comet[i].ID[0]=='P' && comet[i].ID[1]=='/')){

			for(j=6, k=0; comet[i].ID[j]!='\0'; j++, k++)
				tempID[k]=comet[i].ID[j];

			for(j=6; j<strlen(comet[i].ID); j++)
				comet[i].ID[j]=' ';

			edit_name(comet[i].ID);

			strcat(comet[i].ID, " ");
			strcat(comet[i].ID, tempID);
		}

		if ((comet[i].ID[0]=='C' && comet[i].ID[1]=='/') ||
			(comet[i].ID[0]=='P' && comet[i].ID[1]=='/')){

			strcpy(comet[i].full, comet[i].ID);
			strcat(comet[i].full, " (");
			strcat(comet[i].full, comet[i].name);
			strcat(comet[i].full, ")");
		}

		else {
			strcpy(comet[i].full, comet[i].ID);
			strcat(comet[i].full, "/");
			strcat(comet[i].full, comet[i].name);
		}

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].T = compute_T (comet[i].y, comet[i].m, comet[i].d);
		line++;

		ex = do_exclude(i);
		if (ex == 1) { N--; i--; }
	}

	return N;
}

int import_megastar (int N, char *fin_name){

	int i, ex, m, line=1;
	char x[25+1];
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {

		m = fscanf(fin, "%30c %12c %d %d %d.%d %f %f %f %f %f %f %f %25[^\n]%*c",
			comet[i].name, comet[i].ID, &comet[i].y, &comet[i].m, &comet[i].d,
			&comet[i].h, &comet[i].q, &comet[i].e, &comet[i].pn,
			&comet[i].an, &comet[i].i, &comet[i].H, &comet[i].G, x);

		if (m < 14){
			printf("\n\n  Unable to read data in line %d", line);
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		edit_name (comet[i].ID);
		edit_name (comet[i].name);

		if ((comet[i].ID[0]=='C' && comet[i].ID[1]=='/') ||
			(comet[i].ID[0]=='P' && comet[i].ID[1]=='/')){

			strcpy(comet[i].full, comet[i].ID);
			strcat(comet[i].full, " (");
			strcat(comet[i].full, comet[i].name);
			strcat(comet[i].full, ")");
		}

		else {
			strcpy(comet[i].full, comet[i].ID);
			strcat(comet[i].full, "/");
			strcat(comet[i].full, comet[i].name);
		}

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].T = compute_T (comet[i].y, comet[i].m, comet[i].d);
		line++;

		ex = do_exclude(i);
		if (ex == 1) { N--; i--; }
	}

	return N;
}

int import_skychart (int N, char *fin_name){

	int i, j, k, l;
	int ex, m, line=1;
	char c;
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {

		m = fscanf(fin, "P11 2000.0 -%f %f %f %f %f 0 %d/%d/%d.%d %f %f 0 0 ",
			&comet[i].q, &comet[i].e, &comet[i].i, &comet[i].pn,
			&comet[i].an, &comet[i].y, &comet[i].m, &comet[i].d,
			&comet[i].h, &comet[i].H, &comet[i].G);

		if (m < 12){
			printf("\n\n  Unable to read data in line %d", line);
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		j=0;
		while ((c=fgetc(fin)) != ';' ){
			comet[i].full[j++]=c;
		}
		comet[i].full[j]='\0';

		fscanf(fin, "%*[^\n]\n");		//za izostavi ono na kraju

		for (j=0; comet[i].full[j]!='\0'; j++){
			if (isdigit(comet[i].full[j]) &&
				comet[i].full[j+1]=='P' &&
				comet[i].full[j+2]=='/'){

				for(k=0; comet[i].full[k]!='/'; k++)
					comet[i].ID[k]=comet[i].full[k];

				comet[i].ID[k]='\0';
				++k;
				for(l=0; comet[i].full[k]!='\0'; l++, k++)
					comet[i].name[l]=comet[i].full[k];

				comet[i].name[l]='\0';
			}

			if (comet[i].full[j]=='('){
				for(k=0; comet[i].full[k]!='('; k++)
					comet[i].ID[k]=comet[i].full[k];

				comet[i].ID[k-1]='\0';

				++k;
				for(l=0; comet[i].full[k]!=')'; k++, l++)
					comet[i].name[l]=comet[i].full[k];

				comet[i].name[l]='\0';
			}
		}

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].T = compute_T (comet[i].y, comet[i].m, comet[i].d);
		line++;

		ex = do_exclude(i);
		if (ex == 1) { N--; i--; }
	}

	return N;
}

int import_voyager (int N, char *fin_name){

	int i, ex, m, line=1;
	char mj[3+1];
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {

		m = fscanf(fin, "%27c %f %f %f %f %f %f %4d %3c %d.%d 2000.0\n",
			comet[i].name, &comet[i].q, &comet[i].e, &comet[i].i,
			&comet[i].an, &comet[i].pn, &comet[i].G, &comet[i].y,
			mj, &comet[i].d, &comet[i].h);

		if (m < 11){
			printf("\n\n  Unable to read data in line %d", line);
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		strcpy(comet[i].full, comet[i].name); 		//posto nema pravog full-a, name ce bit kao full

		if (mj[0]=='J' && mj[1]=='a' && mj[2]=='n') comet[i].m=1;
		if (mj[0]=='F' && mj[1]=='e' && mj[2]=='b') comet[i].m=2;
		if (mj[0]=='M' && mj[1]=='a' && mj[2]=='r') comet[i].m=3;
		if (mj[0]=='A' && mj[1]=='p' && mj[2]=='r') comet[i].m=4;
		if (mj[0]=='M' && mj[1]=='a' && mj[2]=='y') comet[i].m=5;
		if (mj[0]=='J' && mj[1]=='u' && mj[2]=='n') comet[i].m=6;
		if (mj[0]=='J' && mj[1]=='u' && mj[2]=='l') comet[i].m=7;
		if (mj[0]=='A' && mj[1]=='u' && mj[2]=='g') comet[i].m=8;
		if (mj[0]=='S' && mj[1]=='e' && mj[2]=='p') comet[i].m=9;
		if (mj[0]=='O' && mj[1]=='c' && mj[2]=='t') comet[i].m=10;
		if (mj[0]=='N' && mj[1]=='o' && mj[2]=='v') comet[i].m=11;
		if (mj[0]=='D' && mj[1]=='e' && mj[2]=='c') comet[i].m=12;

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].T = compute_T (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].name);
		line++;

		ex = do_exclude(i);
		if (ex == 1) { N--; i--; }
	}

	return N;
}

int import_skytools (int N, char *fin_name){

	int i, j, k, l, u, t, space;
	int yy, mm, dd;
	int ex, m, line=1;
	char x[15+1];
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {

		m = fscanf(fin, "C %40c %d %d %d %d %d %d.%d %f %f %f %f %f %f %f 0.002000 %15[^\n]%*c",
			comet[i].full, &yy, &mm, &dd, &comet[i].y, &comet[i].m, &comet[i].d, &comet[i].h,
			&comet[i].q, &comet[i].e, &comet[i].pn, &comet[i].an, &comet[i].i,
			&comet[i].H, &comet[i].G, x);

		if (m < 16){
			printf("\n\n  Unable to read data in line %d", line);
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		edit_name(comet[i].full);

		for (j=0; comet[i].full[j]!='\0'; j++){
			if (isdigit(comet[i].full[j]) &&
				comet[i].full[j+1]=='P' &&
				comet[i].full[j+2]=='/'){

				for(k=0; comet[i].full[k]!='/'; k++)
					comet[i].ID[k]=comet[i].full[k];

				comet[i].ID[k]='\0';
				++k;
				for(l=0; comet[i].full[k]!='\0'; l++, k++)
					comet[i].name[l]=comet[i].full[k];

				comet[i].name[l]='\0';
			}

		if ((comet[i].full[0]=='C' && comet[i].full[1]=='/') ||
			(comet[i].full[0]=='P' && comet[i].full[1]=='/')){
				space=0;
				for(u=0; u<strlen(comet[i].full); u++){
					if (comet[i].full[u]==' ' && space==1) {
						t=u;
						break;
					}
					else if(comet[i].full[u]==' ') space++;
				}

				for(k=0; k<t; k++)
					comet[i].ID[k]=comet[i].full[k];

				comet[i].ID[k]='\0';

				++k;
				for(l=0; comet[i].full[k]!='\0'; k++, l++)
					comet[i].name[l]=comet[i].full[k];

				comet[i].name[l]='\0';
			}
		}

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].T = compute_T (comet[i].y, comet[i].m, comet[i].d);
		line++;

		ex = do_exclude(i);
		if (ex == 1) { N--; i--; }
	}

	return N;
}

int import_cfw (int N, char *fin_name){

	int i, j, k, l, ex;
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {
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
					comet[i].full,
					&comet[i].y, &comet[i].m, &comet[i].d, &comet[i].h,
					&comet[i].q,
					&comet[i].e,
					&comet[i].pn,
					&comet[i].an,
					&comet[i].i,
					&comet[i].H, &comet[i].G);

		edit_name(comet[i].full);

		for (j=0; comet[i].full[j]!='\0'; j++){
			if (isdigit(comet[i].full[j]) &&
				comet[i].full[j+1]=='P' &&
				comet[i].full[j+2]=='/'){

				for(k=0; comet[i].full[k]!='/'; k++)
					comet[i].ID[k]=comet[i].full[k];

				comet[i].ID[k]='\0';
				++k;
				for(l=0; comet[i].full[k]!='\0'; l++, k++)
					comet[i].name[l]=comet[i].full[k];

				comet[i].name[l]='\0';
			}

			if (comet[i].full[j]=='('){
				for(k=0; comet[i].full[k]!='('; k++)
					comet[i].ID[k]=comet[i].full[k];

				comet[i].ID[k-1]='\0';

				++k;
				for(l=0; comet[i].full[k]!=')'; k++, l++)
					comet[i].name[l]=comet[i].full[k];

				comet[i].name[l]='\0';
			}
		}

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].T = compute_T (comet[i].y, comet[i].m, comet[i].d);

		ex = do_exclude(i);
		if (ex == 1) { N--; i--; }
	}

	return N;
}

int import_nasa1 (int N, char *fin_name){

	int i, j, k, l;
	int ex, m, line=1, trash;
	char c, q, x[20+1];
	FILE *fin = fopen(fin_name, "r");

	printf("\n\n  Press any key to continue.... ");
	getch();

	do{
		fflush(stdin);
		fflush(stdout);
		system("CLS");
		printf("\n");
		printf("  Exclude SOHO...\n\n");
		printf(" =============================================================================\n\n");
		printf("  Do you want exclude SOHO comets? (y/n)\n\n");
		printf("  Select option: ");
		cin >> q;
		if(isupper(q)) q = tolower(q);
	} while (q!='y' && q!='n');

	for (i=0; i<N; i++) {

		k=0; j=0;
		while (k<44){
			c=fgetc(fin);
			comet[i].full[j]=c;
			if (c==' ' && j==0) --j;
			j++;
			k++;
		}

		edit_name(comet[i].full);

		for (j=0; comet[i].full[j]!='\0'; j++){
			if ((isdigit(comet[i].full[j]) && comet[i].full[j+1]=='P' && comet[i].full[j+2]=='/') ||
				(isdigit(comet[i].full[j]) && comet[i].full[j+1]=='D' && comet[i].full[j+2]=='/')){

				for(k=0; comet[i].full[k]!='/'; k++)
					comet[i].ID[k]=comet[i].full[k];

				comet[i].ID[k]='\0';
				++k;
				for(l=0; comet[i].full[k]!='\0'; l++, k++)
					comet[i].name[l]=comet[i].full[k];

				comet[i].name[l]='\0';
			}

			if (comet[i].full[j]=='('){
				for(k=0; comet[i].full[k]!='('; k++)
					comet[i].ID[k]=comet[i].full[k];

				comet[i].ID[k-1]='\0';

				++k;
				for(l=0; comet[i].full[k]!=')'; k++, l++)
					comet[i].name[l]=comet[i].full[k];

				comet[i].name[l]='\0';
			}
		}

		m = fscanf(fin, "%d %f %f %f %f %f %4d%2d%2d.%4d %20[^\n]%*c",
			&trash, &comet[i].q, &comet[i].e, &comet[i].i,
			&comet[i].pn, &comet[i].an, &comet[i].y, &comet[i].m,
			&comet[i].d, &comet[i].h, x);

		if (m < 11){
			printf("\n\n  Unable to read data in line %d", line);
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].T = compute_T (comet[i].y, comet[i].m, comet[i].d);
		line++;

		if (q=='y') for (j=0; j<strlen(comet[i].name); j++){
			if ((comet[i].name[j]  =='S' && comet[i].name[j+1]=='O' &&
				 comet[i].name[j+2]=='H' && comet[i].name[j+3]=='O')){
				N--; i--;
			}
		}

		ex = do_exclude(i);
		if (ex == 1) { N--; i--; }
	}

	return N;
}

int import_nasa2 (int N, char *fin_name){

	int i, j, k, l;
	int ex, m, line=1;
	char c, q, x[10+1];
	FILE *fin = fopen(fin_name, "r");

	printf("\n\n  Press any key to continue.... ");
	getch();

	do{
		fflush(stdin);
		fflush(stdout);
		system("CLS");
		printf("\n");
		printf("  Exclude SOHO...\n\n");
		printf(" =============================================================================\n\n");
		printf("  Do you want exclude SOHO comets? (y/n)\n\n");
		printf("  Select option: ");
		cin >> q;
		if(isupper(q)) q = tolower(q);
	} while (q!='y' && q!='n');

	for (i=0; i<N; i++) {

		j=0;
		fgetc(fin);		// da uzme prve navodnike
		while ((c=fgetc(fin)) != '"' ){
			comet[i].full[j]=c;
			if (c==' ' && j==0) --j;
			j++;
		}

		comet[i].name[j]='\0';

		for (j=0; comet[i].full[j]!='\0'; j++){
			if ((isdigit(comet[i].full[j]) && comet[i].full[j+1]=='P' && comet[i].full[j+2]=='/') ||
				(isdigit(comet[i].full[j]) && comet[i].full[j+1]=='D' && comet[i].full[j+2]=='/')){

				for(k=0; comet[i].full[k]!='/'; k++)
					comet[i].ID[k]=comet[i].full[k];

				comet[i].ID[k]='\0';
				++k;
				for(l=0; comet[i].full[k]!='\0'; l++, k++)
					comet[i].name[l]=comet[i].full[k];

				comet[i].name[l]='\0';
			}

			if (comet[i].full[j]=='('){
				for(k=0; comet[i].full[k]!='('; k++)
					comet[i].ID[k]=comet[i].full[k];

				comet[i].ID[k-1]='\0';

				++k;
				for(l=0; comet[i].full[k]!=')'; k++, l++)
					comet[i].name[l]=comet[i].full[k];

				comet[i].name[l]='\0';
			}
		}

		if (strlen(comet[i].name)==0){
			for(k=0; comet[i].full[k]!='\0'; k++)
				comet[i].name[k]=comet[i].full[k];
			comet[i].name[k]='\0';

			strcat(comet[i].full, " (");
			strcat(comet[i].full, comet[i].name);
			strcat(comet[i].full, ")");
		}

		m = fscanf(fin, ",%f,%f,%f,%f,%f,%4d%2d%2d.%4d%10[^\n]%*c",
			&comet[i].q, &comet[i].e, &comet[i].pn, &comet[i].an,
			&comet[i].i, &comet[i].y, &comet[i].m, &comet[i].d,
			&comet[i].h, x);

		if (m < 10){
			printf("\n\n  Unable to read data in line %d", line);
			fscanf(fin, "%*[^\n]\n");
			N--; i--; line++;
			continue;
		}

		if (q=='y') for (j=0; j<strlen(comet[i].name); j++){
			if ((comet[i].name[j]  =='S' && comet[i].name[j+1]=='O' &&
				 comet[i].name[j+2]=='H' && comet[i].name[j+3]=='O')){
					N--; i--;
			}
		}

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].T = compute_T (comet[i].y, comet[i].m, comet[i].d);
		line++;

		ex = do_exclude(i);
		if (ex == 1) { N--; i--; }
	}

	return N;
}


void export_mpc (int N, char *fout_name){

	int i;
	FILE *fout=fopen(fout_name, "a");

	for (i=0; i<N; i++) {

		fprintf(fout,"              %4d %02d %02d.%04d %9f  %.6f  %8.4f  %8.4f  %8.4f  %4d%02d%02d  %4.1f %4.1f  %-56s MPC 00000\n",
				comet[i].y, comet[i].m, comet[i].d, comet[i].h, comet[i].q, comet[i].e,
				comet[i].pn, comet[i].an, comet[i].i, ep_y, ep_m, ep_d, comet[i].H, comet[i].G, comet[i].full);
	}
	fclose(fout);
}

void export_skymap (int N, char *fout_name){

	int i, j, k;
	FILE *fout=fopen(fout_name, "a");

	for (i=0; i<N; i++) {

		k=0;
		for(j=0; j<strlen(comet[i].ID); j++){
			fputc(comet[i].ID[j], fout);
			k++;
		}
		fputc(' ', fout); k++;
		for(j=0; j<strlen(comet[i].name); j++){
			fputc(comet[i].name[j], fout);
			k++;
		}
		while(k!=47){
			fputc(' ', fout); k++;
		}

		fprintf(fout,"%4d %02d %02d.%04d %9f       %.6f %8.4f %8.4f %8.4f  %4.1f  %4.1f\n",
				comet[i].y, comet[i].m, comet[i].d, comet[i].h, comet[i].q,
				comet[i].e, comet[i].pn, comet[i].an, comet[i].i, comet[i].H, comet[i].G);
	}
	fclose(fout);
}

void export_guide (int N, char *fout_name){

	int i, j, k;
	FILE *fout=fopen(fout_name, "a");

	for (i=0; i<N; i++) {

		k=0;

		if (comet[i].ID[strlen(comet[i].ID)-1]=='P' && isdigit(comet[i].ID[strlen(comet[i].ID)-2])){
			fputc('P', fout); k++;
			fputc('/', fout); k++;
		}

		if (comet[i].ID[strlen(comet[i].ID)-1]=='D' && isdigit(comet[i].ID[strlen(comet[i].ID)-2])){
			fputc('D', fout); k++;
			fputc('/', fout); k++;
		}

		for(j=0; j<strlen(comet[i].name); j++){
			fputc(comet[i].name[j], fout);
			k++;
		}
		fputc(' ', fout); k++;
		fputc('(', fout); k++;

		for(j=0; j<strlen(comet[i].ID); j++){
			fputc(comet[i].ID[j], fout);
			k++;
		}
		fputc(')', fout); k++;
		k++;

		while(k!=44){
			fputc(' ', fout); k++;
		}

		fprintf(fout,"%2d.%04d  %2d  %4d  0.0        %9.6f    %.6f  %8.4f    %8.4f    %8.4f    %d.0   %4.1f %4.1f    MPC 00000\n",
				comet[i].d, comet[i].h, comet[i].m, comet[i].y, comet[i].q, comet[i].e,
				comet[i].i, comet[i].pn, comet[i].an, equinox, comet[i].H, comet[i].G);
	}
	fclose(fout);
}

void export_xephem (int N, char *fout_name){

	//info: http://www.clearskyinstitute.com/xephem/help/xephem.html#mozTocId215848

	int i;
	double smAxis, mdMotion, mAnomaly;
	FILE *fout=fopen(fout_name, "a");

// 	varijable za izracun gregorijanskog datuma iz julijanskog dana
	int mm, dd, yy, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13;

	for (i=0; i<N; i++) {

		fprintf(fout,"# From MPC 00000\n%s,", comet[i].full);

		if(comet[i].e < 1){


			smAxis = comet[i].q/(1-comet[i].e);
			mdMotion = 0.9856076686/comet[i].P;
			mAnomaly = -(mdMotion * (comet[i].T - compute_T(ep_y, ep_m, ep_d)));

			v1 = comet[i].T + 0.5;
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
			yy = v12 - 4800 + (v11 + 2) / 12;
			mm = (v11 + 2) % 12 + 1;
			dd = v13 + 1;

			fprintf(fout, "e,%.4f,%.4f,%.4f,%.6f,%.7f,%.8f,%.4f,%02d/%02d.0/%d,%d,g %4.1f,%.1f\n",
				comet[i].i, comet[i].an, comet[i].pn, smAxis, mdMotion, comet[i].e,
				mAnomaly, mm, dd, yy, equinox, comet[i].H, comet[i].G);
		}

		if(comet[i].e == 1.0){

			fprintf(fout, "p,%02d/%02d.%03d/%4d,%.3f,%.3f,%.5f,%.3f,2000,%.1f,%.1f\n",
				comet[i].m, comet[i].d, comet[i].h, comet[i].y,
				comet[i].i, comet[i].pn, comet[i].q, comet[i].an,
				comet[i].H, comet[i].G);
		}

		if(comet[i].e > 1.0){

			fprintf(fout, "h,%02d/%02d.%04d/%4d,%.4f,%.4f,%.4f,%.6f,%.6f,2000,%.1f,%.1f\n",
				comet[i].m, comet[i].d, comet[i].h, comet[i].y,
				comet[i].i, comet[i].an, comet[i].pn, comet[i].e,
				comet[i].q, comet[i].H, comet[i].G);
		}
	}
	fclose(fout);
}

void export_home_planet (int N, char *fout_name){

	int i;
	FILE *fout=fopen(fout_name, "a");

	for (i=0; i<N; i++) {

		fprintf(fout,"%s,%d-%d-%d.%04d,%.6f,%.6f,%.4f,%.4f,%.4f,%.5f,%.5f years, MPC      \n",
				comet[i].full, comet[i].y, comet[i].m, comet[i].d, comet[i].h, comet[i].q,
				comet[i].e, comet[i].pn, comet[i].an, comet[i].i, comet[i].q/(1-comet[i].e), comet[i].P);
	}
	fclose(fout);
}

void export_mystars (int N, char *fout_name){

	int i;
	FILE *fout=fopen(fout_name, "a");

	for (i=0; i<N; i++) {


		fprintf(fout,"%s;\t%d.%04d\t%.4f\t%.6f\t%.6f\t%.4f\t%.4f\t%.1f\t%.1f\tMPC00000\t%d.0\n",
				comet[i].full, comet[i].T-2400000, comet[i].h, comet[i].pn, comet[i].e, comet[i].q,
				comet[i].i, comet[i].an, comet[i].H, comet[i].G, eq_JD-2400000);
	}
	fclose(fout);
}

void export_thesky (int N, char *fout_name){

	int i;
	FILE *fout=fopen(fout_name, "a");

	for (i=0; i<N; i++) {

		fprintf(fout,"%-39s|%d|%4d%02d%02d.%04d |%9f |%.6f |%8.4f |%8.4f |%8.4f |%4.1f |%4.1f | MPC 00000\n",
				comet[i].full, equinox, comet[i].y, comet[i].m, comet[i].d, comet[i].h, comet[i].q,
				comet[i].e, comet[i].pn, comet[i].an, comet[i].i, comet[i].H, comet[i].G);
	}
	fclose(fout);
}

void export_starry_night (int N, char *fout_name){

	int i;
	FILE *fout=fopen(fout_name, "a");

	for (i=0; i<N; i++) {

		fprintf(fout,"     %-29s %4.1f    0.0   %.6f   %9.6f    %8.4f  %8.4f  %8.4f  %d.%04d    %d.5  %4.1f  %-13s MPC 00000\n",
				comet[i].name, comet[i].H, comet[i].e, comet[i].q, comet[i].an, comet[i].pn,
				comet[i].i, comet[i].T, comet[i].h, eq_JD, comet[i].G, comet[i].ID);
	}
	fclose(fout);
}

void export_deep_space (int N, char *fout_name){

	int i;
	FILE *fout=fopen(fout_name, "a");

	for (i=0; i<N; i++) {

		fprintf(fout,"%s (%s)\nC J2000 %4d %02d %02d.%04d %.6f %.6f %.4f %.4f %.4f %.1f %.1f\n",
				comet[i].name, comet[i].ID, comet[i].y, comet[i].m, comet[i].d, comet[i].h,
				comet[i].q, comet[i].e, comet[i].pn, comet[i].an, comet[i].i, comet[i].H, comet[i].G);
	}
	fclose(fout);
}

void export_pc_tcs (int N, char *fout_name){

	int i, j, k;
	FILE *fout=fopen(fout_name, "a");

	for (i=0; i<N; i++) {

		// ovo se mora napravit jer u .ID-u ne smije bit razmaka
		for (j=0; j<strlen(comet[i].ID); j++){
			if (comet[i].ID[j]==' '){
				k=j;
				for( ; comet[i].ID[k]!='\0'; k++)
					comet[i].ID[k]=comet[i].ID[k+1];
			}
		}

		fprintf(fout,"%s %.6f %.6f %.4f %.4f %.4f %4d %02d %02d.%04d %.1f %.1f %s\n",
				comet[i].ID, comet[i].q, comet[i].e, comet[i].i, comet[i].pn, comet[i].an,
				comet[i].y, comet[i].m, comet[i].d, comet[i].h, comet[i].H, comet[i].G, comet[i].name);
	}
	fclose(fout);
}

void export_ecu (int N, char *fout_name){

	int i;
	FILE *fout=fopen(fout_name, "a");

	for (i=0; i<N; i++) {

		fprintf(fout,"%s\nE C 2000 %4d %02d %02d.%04d %.6f %.6f %.4f %.4f %.4f %.1f %.1f\n",
				comet[i].full, comet[i].y, comet[i].m, comet[i].d, comet[i].h, comet[i].q,
				comet[i].e, comet[i].pn, comet[i].an, comet[i].i, comet[i].H, comet[i].G);
	}
	fclose(fout);
}

void export_dance (int N, char *fout_name){

	int i;
	FILE *fout=fopen(fout_name, "a");

	for (i=0; i<N; i++) {

		fprintf(fout,"%s\nE C 2000 %4d %02d %02d.%04d %.6f %.6f %.4f %.4f %.4f %.1f %.1f\n",
				comet[i].full, comet[i].y, comet[i].m, comet[i].d, comet[i].h,
				comet[i].q, comet[i].e, comet[i].pn, comet[i].an, comet[i].i, comet[i].H, comet[i].G);
	}
	fclose(fout);
}

void export_megastar (int N, char *fout_name){

	int i;
	FILE *fout=fopen(fout_name, "a");

	for (i=0; i<N; i++) {

		fprintf(fout,"%-30s%-12s%4d %02d  %02d.%04d   %9.6f   %.6f    %8.4f    %8.4f    %8.4f   %4.1f   %4.1f    2000 MPC 00000\n",
				comet[i].name, comet[i].ID, comet[i].y, comet[i].m, comet[i].d, comet[i].h,
				comet[i].q, comet[i].e, comet[i].pn, comet[i].an, comet[i].i, comet[i].H, comet[i].G);
	}
	fclose(fout);
}

void export_skychart (int N, char *fout_name){

	int i;
	FILE *fout=fopen(fout_name, "a");

	for (i=0; i<N; i++) {

		fprintf(fout,"P11	2000.0	-%.6f\t%.6f\t%.3f\t%.4f\t%.4f\t0\t%4d/%02d/%02d.%04d\t%.1f %.1f\t0\t0\t%s; MPC 00000\t\n",
				comet[i].q, comet[i].e, comet[i].i, comet[i].pn, comet[i].an, comet[i].y,
				comet[i].m, comet[i].d, comet[i].h, comet[i].H, comet[i].G, comet[i].full);
	}
	fclose(fout);
}

void export_voyager (int N, char *fout_name){

	int i;
	char *mon;
	FILE *fout=fopen(fout_name, "a");

	for (i=0; i<N; i++) {

		if (comet[i].m== 1) mon = "Jan";
		if (comet[i].m== 2) mon = "Feb";
		if (comet[i].m== 3) mon = "Mar";
		if (comet[i].m== 4) mon = "Apr";
		if (comet[i].m== 5) mon = "May";
		if (comet[i].m== 6) mon = "Jun";
		if (comet[i].m== 7) mon = "Jul";
		if (comet[i].m== 8) mon = "Aug";
		if (comet[i].m== 9) mon = "Sep";
		if (comet[i].m==10) mon = "Oct";
		if (comet[i].m==11) mon = "Nov";
		if (comet[i].m==12) mon = "Dec";

		fprintf(fout,"%-26s %9.6f   %.6f  %8.4f   %8.4f   %8.4f   0.0  %4d%s",
				comet[i].name, comet[i].q, comet[i].e, comet[i].i, comet[i].an,
				comet[i].pn, comet[i].y, mon);

		if (comet[i].d<10) fprintf(fout, "%d.%04d  2000.0\n", comet[i].d, comet[i].h);
		else fprintf(fout, "%d.%04d 2000.0\n", comet[i].d, comet[i].h);
	}
	fclose(fout);
}

void export_skytools (int N, char *fout_name){

	int i, j, k;
	FILE *fout=fopen(fout_name, "a");

	for (i=0; i<N; i++) {

		if(comet[i].h>999) comet[i].h/=10;

		k=0;
		fputc('C', fout); k++;
		fputc(' ', fout); k++;
		for(j=0; j<strlen(comet[i].ID); j++){
			fputc(comet[i].ID[j], fout);
			k++;
		}

		if (comet[i].ID[strlen(comet[i].ID)-1]=='P'
			&& isdigit(comet[i].ID[strlen(comet[i].ID)-2])){
			fputc('/', fout); k++;
		}
		else {
			fputc(' ', fout);
			k++;
		}
		for(j=0; j<strlen(comet[i].name); j++){
			fputc(comet[i].name[j], fout);
			k++;
		}
		while(k<43){
			fputc(' ', fout); k++;
		}

		fprintf(fout,"2011 02 08 %4d %02d %02d.%-.03d  %9.6f   %.6f %7.3f %7.3f %7.3f  %4.1f  %4.1f 0.002000 MPC 00000\n",
				comet[i].y, comet[i].m, comet[i].d, comet[i].h, comet[i].q,
				comet[i].e, comet[i].pn, comet[i].an, comet[i].i, comet[i].H, comet[i].G);
	}
	fclose(fout);
}

void export_ssc (int N, int Ty, char *fout_name){

	int i, j;
	char *mon;
	FILE *fout=fopen(fout_name, "a");

	for (i=0; i<N; i++) {

		if (comet[i].e == 1) comet[i].e = 1.000001;

		for (j=0; j<strlen(comet[i].full); j++)
			if (comet[i].full[j]=='/') comet[i].full[j]=' ';

		if (comet[i].m== 1) mon = "Jan";
		if (comet[i].m== 2) mon = "Feb";
		if (comet[i].m== 3) mon = "Mar";
		if (comet[i].m== 4) mon = "Apr";
		if (comet[i].m== 5) mon = "May";
		if (comet[i].m== 6) mon = "Jun";
		if (comet[i].m== 7) mon = "Jul";
		if (comet[i].m== 8) mon = "Aug";
		if (comet[i].m== 9) mon = "Sep";
		if (comet[i].m==10) mon = "Oct";
		if (comet[i].m==11) mon = "Nov";
		if (comet[i].m==12) mon = "Dec";

		fprintf(fout,"\"%s\" \"Sol\"\n", comet[i].full);
		fprintf(fout,"{\n");
		fprintf(fout,"Class \"comet\" \n");
		fprintf(fout,"Mesh \"asteroid.cms\" \n");
		fprintf(fout,"Texture \"asteroid.jpg\" \n");
		fprintf(fout,"Radius 5 \n");
		fprintf(fout,"Albedo 0.1 \n");
		fprintf(fout,"EllipticalOrbit \n");
		fprintf(fout,"\t{ \n");
		fprintf(fout,"\tPeriod \t\t\t %f \n", comet[i].P);
		fprintf(fout,"\tPericenterDistance \t %f \n", comet[i].q);
		fprintf(fout,"\tEccentricity \t\t %f \n", comet[i].e);
		fprintf(fout,"\tInclination \t\t %.4f \n", comet[i].i);
		fprintf(fout,"\tAscendingNode \t\t %.4f \n", comet[i].an);
		fprintf(fout,"\tArgOfPericenter \t %.4f \n", comet[i].pn);
		fprintf(fout,"\tMeanAnomaly \t\t 0  \n");
	if (Ty==15)
		fprintf(fout,"\tEpoch \t\t\t %d.%.3d\t# %d %s %.2d.%.d \n",						// ovdje je %.3d za comet[].h
				comet[i].T, comet[i].h, comet[i].y, mon, comet[i].d, comet[i].h);
	else
		fprintf(fout,"\tEpoch \t\t\t %d.%.4d\t# %d %s %.2d.%.4d \n",
				comet[i].T, comet[i].h, comet[i].y, mon, comet[i].d, comet[i].h);		// a ovdje je %.4d, jer kod ty 15 .h ima 3 oznake,
		fprintf(fout,"\t} \n");															// pa bi npr 304 ispisao kao 0304 sto nije dobro
		fprintf(fout,"}\n\n\n");
	}
	fclose(fout);
}

void export_stell (int N, int Ty, char *fout_name){

	int i, j;
	FILE *fout=fopen(fout_name, "a");

	for (i=0; i<N; i++) {

		for (j=0; j<strlen(comet[i].name); j++){
			if (isupper(comet[i].name[j])) comet[i].name[j] = tolower(comet[i].name[j]);
		}

		fprintf(fout,"[%s]\n", comet[i].name);
		fprintf(fout,"parent = Sun\n");
		fprintf(fout,"orbit_Inclination = %f\n", comet[i].i);
		fprintf(fout,"coord_func = comet_orbit\n");
		fprintf(fout,"orbit_Eccentricity = %f\n", comet[i].e);
		fprintf(fout,"orbit_ArgOfPericenter = %f\n", comet[i].pn);
		fprintf(fout,"absolute_magnitude=%.1f\n", comet[i].H);
		fprintf(fout,"name = %s\n", comet[i].full);
		fprintf(fout,"slope_parameter = %.1f\n", comet[i].G);
		fprintf(fout,"lighting = false\n");
		fprintf(fout,"tex_map = nomap.png\n");
		fprintf(fout,"color = 1.0, 1.0, 1.0\n");
		fprintf(fout,"orbit_AscendingNode = %f\n", comet[i].an);
		fprintf(fout,"albedo = 1\n");
		fprintf(fout,"radius = 5\n");
		fprintf(fout,"orbit_PericenterDistance = %f\n", comet[i].q);
		fprintf(fout,"type = comet\n");
	if (Ty==15)
		fprintf(fout,"orbit_TimeAtPericenter = %d.%.3d\n\n", comet[i].T, comet[i].h);
	else
		fprintf(fout,"orbit_TimeAtPericenter = %d.%.4d\n\n", comet[i].T, comet[i].h);
	}
	fclose(fout);
}


double compute_period (float q, float e){

	double P;

	if (e < 1)
		P = pow((q/(1-e)),1.5);

	if (e > 1)
		P = pow((q/(e-1)),1.5);

	if (e == 1)
		P = pow((q/(1-0.999999)),1.5);

	return P;
}

long int compute_T (int y, int m, int d){

	long int T;

	T = 367*y - (7*(y + (m + 9)/12))/4 -
		((3*(y + (m - 9)/7))/100 + 1)/4 +
		(275*m)/9 + d + 1721029;

	return T;
}

char *edit_name (char *name){

	int i;

	for (i=0; i<strlen(name)-1; i++)
		if (name[i]==' ' && name[i+1]==' ') name[i]='\0';
}


void start_screen (){

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	cout << endl;
	cout << "                            ORBITAL ELEMENTS WORKSHOP" << endl << endl;
	cout << " =============================================================================" << endl << endl;
	cout << "  Supported input formats: " << endl << endl;
	cout << "        0. MPC                         12. MegaStar V4.x" << endl;
	cout << "        1. SkyMap                      13. SkyChart III" << endl;
	cout << "        2. Guide                       14. Voyager II" << endl;
	cout << "        3. xephem                      15. SkyTools" << endl;
	cout << "        4. Home Planet                 16. Autostar" << endl;
	cout << "        5. MyStars!" << endl;
	cout << "        6. TheSky                      17. Comet for Windows" << endl;
	cout << "        7. Starry Night                18. NASA (ELEMENTS.COMET)" << endl;
	cout << "        8. Deep Space                  19. NASA (CSV format)" << endl;
	cout << "        9. PC-TCS" << endl;
	cout << "       10. Earth Centered Universe     20. Help" << endl;
	cout << "       11. Dance of the Planets        21. Exit" << endl << endl;
	cout << "  Select option [0-21]: ";
}

void screen_imp (char *import_format, char *soft){

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	cout << endl << "  Importing " << import_format << " (" << soft << ") format..." << endl << endl;
	cout << " =============================================================================" << endl;
	cout << "     1.   Main Menu   |   2.   Exit" << endl;
	cout << " =============================================================================" << endl << endl;
}

void screen_exp1 (char *import_format){

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	cout << endl;
	cout << "  Exporting " << import_format << " format..." << endl << endl;
	cout << " =============================================================================" << endl << endl;
	cout << "  Supported output formats: " << endl << endl;
	cout << "        0. MPC                         12. MegaStar V4.x" << endl;
	cout << "        1. SkyMap                      13. SkyChart III" << endl;
	cout << "        2. Guide                       14. Voyager II" << endl;
	cout << "        3. xephem                      15. SkyTools" << endl;
	cout << "        4. Home Planet                 16. Autostar" << endl;
	cout << "        5. MyStars!\n";
	cout << "        6. TheSky" << endl;
	cout << "        7. Starry Night                17. Celestia(SSC)" << endl;
	cout << "        8. Deep Space                  18. Stellarium" << endl;
	cout << "        9. PC-TCS" << endl;
	cout << "       10. Earth Centered Universe" << endl;
	cout << "       11. Dance of the Planets" << endl << endl;
	cout << "  Select option [0-18]: ";
}

void screen_exp2 (char *import_format, char *export_format){

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	cout << endl << "  Exporting " << import_format << " as " << export_format << " format..." << endl << endl;
	cout << " =============================================================================" << endl;
	cout << "     1.   Main Menu   |   2.   Exit" << endl;
	cout << " =============================================================================" << endl << endl;
}

void screen_exp3 (char *import_format, char *export_format){

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	cout << endl << "  " << import_format << " exported as " << export_format << " format..." << endl << endl;
	cout << " =============================================================================" << endl;
	cout << "     1.   Main Menu   |   2.   Exit" << endl;
	cout << " ============================================================================="  << endl;
}

void exit_screen (){

	// http://patorjk.com/software/taag/
	// Font: Big

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	cout << endl;
	cout << "                   ____           _       _   _             _ " << endl;
	cout << "                  / __ \\         | |     (_) | |           | |" << endl;
	cout << "                 | |  | |  _ __  | |__    _  | |_    __ _  | |" << endl;
	cout << "                 | |  | | | '__| | '_ \\  | | | __|  / _` | | |" << endl;
	cout << "                 | |__| | | |    | |_) | | | | |_  | (_| | | |" << endl;
	cout << "                  \\____/  |_|    |_.__/  |_|  \\__|  \\__,_| |_|\n" << endl;
	cout << "            ______   _                                     _         " << endl;
	cout << "           |  ____| | |                                   | |        " << endl;
	cout << "           | |__    | |   ___   _ __ ___     ___   _ __   | |_   ___ " << endl;
	cout << "           |  __|   | |  / _ \\ | '_ ` _ \\   / _ \\ | '_ \\  | __| / __|" << endl;
	cout << "           | |____  | | |  __/ | | | | | | |  __/ | | | | | |_  \\__ \\" << endl;
	cout << "           |______| |_|  \\___| |_| |_| |_|  \\___| |_| |_|  \\__| |___/\n" << endl;
	cout << "       __          __                _            _                     " << endl;
	cout << "       \\ \\        / /               | |          | |                    " << endl;
	cout << "        \\ \\  /\\  / /   ___    _ __  | | __  ___  | |__     ___    _ __  " << endl;
	cout << "         \\ \\/  \\/ /   / _ \\  | '__| | |/ / / __| | '_ \\   / _ \\  | '_ \\ " << endl;
	cout << "          \\  /\\  /   | (_) | | |    |   <  \\__ \\ | | | | | (_) | | |_) |" << endl;
	cout << "           \\/  \\/     \\___/  |_|    |_|\\_\\ |___/ |_| |_|  \\___/  | .__/" << endl;
	cout << "                                                                 | |" << endl;
    cout << "                                                                 |_|" << endl << endl;
	cout << "Press any key to exit...                             Copyright (c) 2011, jurluk";
}

void help_screen (){

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	cout << "\n\n   Under construction... :) ";
	getch();
}

void excl_screen (){

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	cout << "\n";
	cout << "  Excluding comets...\n\n";
	cout << " =============================================================================\n\n";
}


int sort_data (int N){

	int i, j, k;
	char dir, sortKey;

	do {
		fflush(stdin);
		fflush(stdout);
		system("CLS");
		printf("\n");
		printf("  Sorting data...\n\n");
		printf(" =============================================================================\n");
		printf("     1.   Main Menu   |   2.   Exit   \n");
		printf(" =============================================================================\n\n");
		printf("  Sort by: \n\n");
		printf("        a. Default\n");
		printf("        b. Perihelion Date\n");
		printf("        c. Pericenter Distance\n");
		printf("        d. Eccentricity\n");
		printf("        e. Longitude of the Ascending Node\n");
		printf("        f. Longitude of Pericenter\n");
		printf("        g. Inclination\n");
		printf("        h. Period\n\n");
		printf("  Select option [a-h]: ");
		cin >> sortKey;

		if(isupper(sortKey)) sortKey = tolower(sortKey);

	} while ((sortKey < 'a' || sortKey > 'h') && sortKey!='1' && sortKey!='2');

	if (sortKey=='1') return 1;
	if (sortKey=='2') return 2;

	if(sortKey > 'a'){
		do {
			fflush(stdin);
			fflush(stdout);
			system("CLS");
			printf("\n");
			printf("  Sorting data...\n\n");
			printf(" =============================================================================\n");
			printf("     1.   Main Menu   |   2.   Exit   \n");
			printf(" =============================================================================\n\n");
			printf("        a. Ascending\n");
			printf("        b. Descending\n\n");
			printf("  Select option: ");
			cin >> dir;
			if(isupper(dir)) dir = tolower(dir);
		} while (dir!='a' && dir!='b' && dir!='1' && dir!='2');
	}

	if (dir=='1') return 1;
	if (dir=='2') return 2;

	for (i=0; i<N-1; i++){

		for (j=i+1; j<N; j++){

			if (sortKey=='b' && dir=='a'){

				if (comet[i].T > comet[j].T){

					for(k=0;k<81;k++) {
						temp.full[k]='\0';
						temp.full[k]=comet[i].full[k];
					}
					for(k=0;k<81;k++) {
						comet[i].full[k]='\0';
						comet[i].full[k]=comet[j].full[k];
					}
					for(k=0;k<81;k++) {
						comet[j].full[k]='\0';
						comet[j].full[k]=temp.full[k];
					}

					for(k=0;k<56;k++) {
						temp.name[k]='\0';
						temp.name[k]=comet[i].name[k];
					}
					for(k=0;k<56;k++) {
						comet[i].name[k]='\0';
						comet[i].name[k]=comet[j].name[k];
					}
					for(k=0;k<56;k++) {
						comet[j].name[k]='\0';
						comet[j].name[k]=temp.name[k];
					}

					for(k=0;k<26;k++) {
						temp.ID[k]='\0';
						temp.ID[k]=comet[i].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[i].ID[k]='\0';
						comet[i].ID[k]=comet[j].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[j].ID[k]='\0';
						comet[j].ID[k]=temp.ID[k];
					}

					temp.T = comet[i].T;
					comet[i].T = comet[j].T;
					comet[j].T = temp.T;

					temp.y = comet[i].y;
					comet[i].y = comet[j].y;
					comet[j].y = temp.y;

					temp.m = comet[i].m;
					comet[i].m = comet[j].m;
					comet[j].m = temp.m;

					temp.d = comet[i].d;
					comet[i].d = comet[j].d;
					comet[j].d = temp.d;

					temp.h = comet[i].h;
					comet[i].h = comet[j].h;
					comet[j].h = temp.h;

					temp.P = comet[i].P;
					comet[i].P = comet[j].P;
					comet[j].P = temp.P;

					temp.q = comet[i].q;
					comet[i].q = comet[j].q;
					comet[j].q = temp.q;

					temp.e = comet[i].e;
					comet[i].e = comet[j].e;
					comet[j].e = temp.e;

					temp.i = comet[i].i;
					comet[i].i = comet[j].i;
					comet[j].i = temp.i;

					temp.an = comet[i].an;
					comet[i].an = comet[j].an;
					comet[j].an = temp.an;

					temp.pn = comet[i].pn;
					comet[i].pn = comet[j].pn;
					comet[j].pn = temp.pn;

					temp.H = comet[i].H;
					comet[i].H = comet[j].H;
					comet[j].H = temp.H;

					temp.G = comet[i].G;
					comet[i].G = comet[j].G;
					comet[j].G = temp.G;

					for(k=0;k<21;k++) {
						temp.book[k]='\0';
						temp.book[k]=comet[i].book[k];
					}
					for(k=0;k<21;k++) {
						comet[i].book[k]='\0';
						comet[i].book[k]=comet[j].book[k];
					}
					for(k=0;k<21;k++) {
						comet[j].book[k]='\0';
						comet[j].book[k]=temp.book[k];
					}
				}
			}

			if (sortKey=='b' && dir=='b'){

				if (comet[i].T < comet[j].T){

					for(k=0;k<81;k++) {
						temp.full[k]='\0';
						temp.full[k]=comet[i].full[k];
					}
					for(k=0;k<81;k++) {
						comet[i].full[k]='\0';
						comet[i].full[k]=comet[j].full[k];
					}
					for(k=0;k<81;k++) {
						comet[j].full[k]='\0';
						comet[j].full[k]=temp.full[k];
					}

					for(k=0;k<56;k++) {
						temp.name[k]='\0';
						temp.name[k]=comet[i].name[k];
					}
					for(k=0;k<56;k++) {
						comet[i].name[k]='\0';
						comet[i].name[k]=comet[j].name[k];
					}
					for(k=0;k<56;k++) {
						comet[j].name[k]='\0';
						comet[j].name[k]=temp.name[k];
					}

					for(k=0;k<26;k++) {
						temp.ID[k]='\0';
						temp.ID[k]=comet[i].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[i].ID[k]='\0';
						comet[i].ID[k]=comet[j].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[j].ID[k]='\0';
						comet[j].ID[k]=temp.ID[k];
					}

					temp.T = comet[i].T;
					comet[i].T = comet[j].T;
					comet[j].T = temp.T;

					temp.y = comet[i].y;
					comet[i].y = comet[j].y;
					comet[j].y = temp.y;

					temp.m = comet[i].m;
					comet[i].m = comet[j].m;
					comet[j].m = temp.m;

					temp.d = comet[i].d;
					comet[i].d = comet[j].d;
					comet[j].d = temp.d;

					temp.h = comet[i].h;
					comet[i].h = comet[j].h;
					comet[j].h = temp.h;

					temp.P = comet[i].P;
					comet[i].P = comet[j].P;
					comet[j].P = temp.P;

					temp.q = comet[i].q;
					comet[i].q = comet[j].q;
					comet[j].q = temp.q;

					temp.e = comet[i].e;
					comet[i].e = comet[j].e;
					comet[j].e = temp.e;

					temp.i = comet[i].i;
					comet[i].i = comet[j].i;
					comet[j].i = temp.i;

					temp.an = comet[i].an;
					comet[i].an = comet[j].an;
					comet[j].an = temp.an;

					temp.pn = comet[i].pn;
					comet[i].pn = comet[j].pn;
					comet[j].pn = temp.pn;

					temp.H = comet[i].H;
					comet[i].H = comet[j].H;
					comet[j].H = temp.H;

					temp.G = comet[i].G;
					comet[i].G = comet[j].G;
					comet[j].G = temp.G;

					for(k=0;k<21;k++) {
						temp.book[k]='\0';
						temp.book[k]=comet[i].book[k];
					}
					for(k=0;k<21;k++) {
						comet[i].book[k]='\0';
						comet[i].book[k]=comet[j].book[k];
					}
					for(k=0;k<21;k++) {
						comet[j].book[k]='\0';
						comet[j].book[k]=temp.book[k];
					}
				}
			}

			if (sortKey=='c' && dir=='a'){

				if (comet[i].q > comet[j].q){

					for(k=0;k<81;k++) {
						temp.full[k]='\0';
						temp.full[k]=comet[i].full[k];
					}
					for(k=0;k<81;k++) {
						comet[i].full[k]='\0';
						comet[i].full[k]=comet[j].full[k];
					}
					for(k=0;k<81;k++) {
						comet[j].full[k]='\0';
						comet[j].full[k]=temp.full[k];
					}

					for(k=0;k<56;k++) {
						temp.name[k]='\0';
						temp.name[k]=comet[i].name[k];
					}
					for(k=0;k<56;k++) {
						comet[i].name[k]='\0';
						comet[i].name[k]=comet[j].name[k];
					}
					for(k=0;k<56;k++) {
						comet[j].name[k]='\0';
						comet[j].name[k]=temp.name[k];
					}

					for(k=0;k<26;k++) {
						temp.ID[k]='\0';
						temp.ID[k]=comet[i].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[i].ID[k]='\0';
						comet[i].ID[k]=comet[j].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[j].ID[k]='\0';
						comet[j].ID[k]=temp.ID[k];
					}

					temp.T = comet[i].T;
					comet[i].T = comet[j].T;
					comet[j].T = temp.T;

					temp.y = comet[i].y;
					comet[i].y = comet[j].y;
					comet[j].y = temp.y;

					temp.m = comet[i].m;
					comet[i].m = comet[j].m;
					comet[j].m = temp.m;

					temp.d = comet[i].d;
					comet[i].d = comet[j].d;
					comet[j].d = temp.d;

					temp.h = comet[i].h;
					comet[i].h = comet[j].h;
					comet[j].h = temp.h;

					temp.P = comet[i].P;
					comet[i].P = comet[j].P;
					comet[j].P = temp.P;

					temp.q = comet[i].q;
					comet[i].q = comet[j].q;
					comet[j].q = temp.q;

					temp.e = comet[i].e;
					comet[i].e = comet[j].e;
					comet[j].e = temp.e;

					temp.i = comet[i].i;
					comet[i].i = comet[j].i;
					comet[j].i = temp.i;

					temp.an = comet[i].an;
					comet[i].an = comet[j].an;
					comet[j].an = temp.an;

					temp.pn = comet[i].pn;
					comet[i].pn = comet[j].pn;
					comet[j].pn = temp.pn;

					temp.H = comet[i].H;
					comet[i].H = comet[j].H;
					comet[j].H = temp.H;

					temp.G = comet[i].G;
					comet[i].G = comet[j].G;
					comet[j].G = temp.G;

					for(k=0;k<21;k++) {
						temp.book[k]='\0';
						temp.book[k]=comet[i].book[k];
					}
					for(k=0;k<21;k++) {
						comet[i].book[k]='\0';
						comet[i].book[k]=comet[j].book[k];
					}
					for(k=0;k<21;k++) {
						comet[j].book[k]='\0';
						comet[j].book[k]=temp.book[k];
					}
				}
			}

			if (sortKey=='c' && dir=='b'){

				if (comet[i].q < comet[j].q){

					for(k=0;k<81;k++) {
						temp.full[k]='\0';
						temp.full[k]=comet[i].full[k];
					}
					for(k=0;k<81;k++) {
						comet[i].full[k]='\0';
						comet[i].full[k]=comet[j].full[k];
					}
					for(k=0;k<81;k++) {
						comet[j].full[k]='\0';
						comet[j].full[k]=temp.full[k];
					}

					for(k=0;k<56;k++) {
						temp.name[k]='\0';
						temp.name[k]=comet[i].name[k];
					}
					for(k=0;k<56;k++) {
						comet[i].name[k]='\0';
						comet[i].name[k]=comet[j].name[k];
					}
					for(k=0;k<56;k++) {
						comet[j].name[k]='\0';
						comet[j].name[k]=temp.name[k];
					}

					for(k=0;k<26;k++) {
						temp.ID[k]='\0';
						temp.ID[k]=comet[i].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[i].ID[k]='\0';
						comet[i].ID[k]=comet[j].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[j].ID[k]='\0';
						comet[j].ID[k]=temp.ID[k];
					}

					temp.T = comet[i].T;
					comet[i].T = comet[j].T;
					comet[j].T = temp.T;

					temp.y = comet[i].y;
					comet[i].y = comet[j].y;
					comet[j].y = temp.y;

					temp.m = comet[i].m;
					comet[i].m = comet[j].m;
					comet[j].m = temp.m;

					temp.d = comet[i].d;
					comet[i].d = comet[j].d;
					comet[j].d = temp.d;

					temp.h = comet[i].h;
					comet[i].h = comet[j].h;
					comet[j].h = temp.h;

					temp.P = comet[i].P;
					comet[i].P = comet[j].P;
					comet[j].P = temp.P;

					temp.q = comet[i].q;
					comet[i].q = comet[j].q;
					comet[j].q = temp.q;

					temp.e = comet[i].e;
					comet[i].e = comet[j].e;
					comet[j].e = temp.e;

					temp.i = comet[i].i;
					comet[i].i = comet[j].i;
					comet[j].i = temp.i;

					temp.an = comet[i].an;
					comet[i].an = comet[j].an;
					comet[j].an = temp.an;

					temp.pn = comet[i].pn;
					comet[i].pn = comet[j].pn;
					comet[j].pn = temp.pn;

					temp.H = comet[i].H;
					comet[i].H = comet[j].H;
					comet[j].H = temp.H;

					temp.G = comet[i].G;
					comet[i].G = comet[j].G;
					comet[j].G = temp.G;

					for(k=0;k<21;k++) {
						temp.book[k]='\0';
						temp.book[k]=comet[i].book[k];
					}
					for(k=0;k<21;k++) {
						comet[i].book[k]='\0';
						comet[i].book[k]=comet[j].book[k];
					}
					for(k=0;k<21;k++) {
						comet[j].book[k]='\0';
						comet[j].book[k]=temp.book[k];
					}
				}
			}

			if (sortKey=='d' && dir=='a'){

				if (comet[i].e > comet[j].e){

					for(k=0;k<81;k++) {
						temp.full[k]='\0';
						temp.full[k]=comet[i].full[k];
					}
					for(k=0;k<81;k++) {
						comet[i].full[k]='\0';
						comet[i].full[k]=comet[j].full[k];
					}
					for(k=0;k<81;k++) {
						comet[j].full[k]='\0';
						comet[j].full[k]=temp.full[k];
					}

					for(k=0;k<56;k++) {
						temp.name[k]='\0';
						temp.name[k]=comet[i].name[k];
					}
					for(k=0;k<56;k++) {
						comet[i].name[k]='\0';
						comet[i].name[k]=comet[j].name[k];
					}
					for(k=0;k<56;k++) {
						comet[j].name[k]='\0';
						comet[j].name[k]=temp.name[k];
					}

					for(k=0;k<26;k++) {
						temp.ID[k]='\0';
						temp.ID[k]=comet[i].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[i].ID[k]='\0';
						comet[i].ID[k]=comet[j].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[j].ID[k]='\0';
						comet[j].ID[k]=temp.ID[k];
					}

					temp.T = comet[i].T;
					comet[i].T = comet[j].T;
					comet[j].T = temp.T;

					temp.y = comet[i].y;
					comet[i].y = comet[j].y;
					comet[j].y = temp.y;

					temp.m = comet[i].m;
					comet[i].m = comet[j].m;
					comet[j].m = temp.m;

					temp.d = comet[i].d;
					comet[i].d = comet[j].d;
					comet[j].d = temp.d;

					temp.h = comet[i].h;
					comet[i].h = comet[j].h;
					comet[j].h = temp.h;

					temp.P = comet[i].P;
					comet[i].P = comet[j].P;
					comet[j].P = temp.P;

					temp.q = comet[i].q;
					comet[i].q = comet[j].q;
					comet[j].q = temp.q;

					temp.e = comet[i].e;
					comet[i].e = comet[j].e;
					comet[j].e = temp.e;

					temp.i = comet[i].i;
					comet[i].i = comet[j].i;
					comet[j].i = temp.i;

					temp.an = comet[i].an;
					comet[i].an = comet[j].an;
					comet[j].an = temp.an;

					temp.pn = comet[i].pn;
					comet[i].pn = comet[j].pn;
					comet[j].pn = temp.pn;

					temp.H = comet[i].H;
					comet[i].H = comet[j].H;
					comet[j].H = temp.H;

					temp.G = comet[i].G;
					comet[i].G = comet[j].G;
					comet[j].G = temp.G;

					for(k=0;k<21;k++) {
						temp.book[k]='\0';
						temp.book[k]=comet[i].book[k];
					}
					for(k=0;k<21;k++) {
						comet[i].book[k]='\0';
						comet[i].book[k]=comet[j].book[k];
					}
					for(k=0;k<21;k++) {
						comet[j].book[k]='\0';
						comet[j].book[k]=temp.book[k];
					}
				}
			}

			if (sortKey=='d' && dir=='b'){

				if (comet[i].e < comet[j].e){

					for(k=0;k<81;k++) {
						temp.full[k]='\0';
						temp.full[k]=comet[i].full[k];
					}
					for(k=0;k<81;k++) {
						comet[i].full[k]='\0';
						comet[i].full[k]=comet[j].full[k];
					}
					for(k=0;k<81;k++) {
						comet[j].full[k]='\0';
						comet[j].full[k]=temp.full[k];
					}

					for(k=0;k<56;k++) {
						temp.name[k]='\0';
						temp.name[k]=comet[i].name[k];
					}
					for(k=0;k<56;k++) {
						comet[i].name[k]='\0';
						comet[i].name[k]=comet[j].name[k];
					}
					for(k=0;k<56;k++) {
						comet[j].name[k]='\0';
						comet[j].name[k]=temp.name[k];
					}

					for(k=0;k<26;k++) {
						temp.ID[k]='\0';
						temp.ID[k]=comet[i].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[i].ID[k]='\0';
						comet[i].ID[k]=comet[j].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[j].ID[k]='\0';
						comet[j].ID[k]=temp.ID[k];
					}

					temp.T = comet[i].T;
					comet[i].T = comet[j].T;
					comet[j].T = temp.T;

					temp.y = comet[i].y;
					comet[i].y = comet[j].y;
					comet[j].y = temp.y;

					temp.m = comet[i].m;
					comet[i].m = comet[j].m;
					comet[j].m = temp.m;

					temp.d = comet[i].d;
					comet[i].d = comet[j].d;
					comet[j].d = temp.d;

					temp.h = comet[i].h;
					comet[i].h = comet[j].h;
					comet[j].h = temp.h;

					temp.P = comet[i].P;
					comet[i].P = comet[j].P;
					comet[j].P = temp.P;

					temp.q = comet[i].q;
					comet[i].q = comet[j].q;
					comet[j].q = temp.q;

					temp.e = comet[i].e;
					comet[i].e = comet[j].e;
					comet[j].e = temp.e;

					temp.i = comet[i].i;
					comet[i].i = comet[j].i;
					comet[j].i = temp.i;

					temp.an = comet[i].an;
					comet[i].an = comet[j].an;
					comet[j].an = temp.an;

					temp.pn = comet[i].pn;
					comet[i].pn = comet[j].pn;
					comet[j].pn = temp.pn;

					temp.H = comet[i].H;
					comet[i].H = comet[j].H;
					comet[j].H = temp.H;

					temp.G = comet[i].G;
					comet[i].G = comet[j].G;
					comet[j].G = temp.G;

					for(k=0;k<21;k++) {
						temp.book[k]='\0';
						temp.book[k]=comet[i].book[k];
					}
					for(k=0;k<21;k++) {
						comet[i].book[k]='\0';
						comet[i].book[k]=comet[j].book[k];
					}
					for(k=0;k<21;k++) {
						comet[j].book[k]='\0';
						comet[j].book[k]=temp.book[k];
					}
				}
			}

			if (sortKey=='e' && dir=='a'){

				if (comet[i].an > comet[j].an){

					for(k=0;k<81;k++) {
						temp.full[k]='\0';
						temp.full[k]=comet[i].full[k];
					}
					for(k=0;k<81;k++) {
						comet[i].full[k]='\0';
						comet[i].full[k]=comet[j].full[k];
					}
					for(k=0;k<81;k++) {
						comet[j].full[k]='\0';
						comet[j].full[k]=temp.full[k];
					}

					for(k=0;k<56;k++) {
						temp.name[k]='\0';
						temp.name[k]=comet[i].name[k];
					}
					for(k=0;k<56;k++) {
						comet[i].name[k]='\0';
						comet[i].name[k]=comet[j].name[k];
					}
					for(k=0;k<56;k++) {
						comet[j].name[k]='\0';
						comet[j].name[k]=temp.name[k];
					}

					for(k=0;k<26;k++) {
						temp.ID[k]='\0';
						temp.ID[k]=comet[i].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[i].ID[k]='\0';
						comet[i].ID[k]=comet[j].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[j].ID[k]='\0';
						comet[j].ID[k]=temp.ID[k];
					}

					temp.T = comet[i].T;
					comet[i].T = comet[j].T;
					comet[j].T = temp.T;

					temp.y = comet[i].y;
					comet[i].y = comet[j].y;
					comet[j].y = temp.y;

					temp.m = comet[i].m;
					comet[i].m = comet[j].m;
					comet[j].m = temp.m;

					temp.d = comet[i].d;
					comet[i].d = comet[j].d;
					comet[j].d = temp.d;

					temp.h = comet[i].h;
					comet[i].h = comet[j].h;
					comet[j].h = temp.h;

					temp.P = comet[i].P;
					comet[i].P = comet[j].P;
					comet[j].P = temp.P;

					temp.q = comet[i].q;
					comet[i].q = comet[j].q;
					comet[j].q = temp.q;

					temp.e = comet[i].e;
					comet[i].e = comet[j].e;
					comet[j].e = temp.e;

					temp.i = comet[i].i;
					comet[i].i = comet[j].i;
					comet[j].i = temp.i;

					temp.an = comet[i].an;
					comet[i].an = comet[j].an;
					comet[j].an = temp.an;

					temp.pn = comet[i].pn;
					comet[i].pn = comet[j].pn;
					comet[j].pn = temp.pn;

					temp.H = comet[i].H;
					comet[i].H = comet[j].H;
					comet[j].H = temp.H;

					temp.G = comet[i].G;
					comet[i].G = comet[j].G;
					comet[j].G = temp.G;

					for(k=0;k<21;k++) {
						temp.book[k]='\0';
						temp.book[k]=comet[i].book[k];
					}
					for(k=0;k<21;k++) {
						comet[i].book[k]='\0';
						comet[i].book[k]=comet[j].book[k];
					}
					for(k=0;k<21;k++) {
						comet[j].book[k]='\0';
						comet[j].book[k]=temp.book[k];
					}
				}
			}

			if (sortKey=='e' && dir=='b'){

				if (comet[i].an < comet[j].an){

					for(k=0;k<81;k++) {
						temp.full[k]='\0';
						temp.full[k]=comet[i].full[k];
					}
					for(k=0;k<81;k++) {
						comet[i].full[k]='\0';
						comet[i].full[k]=comet[j].full[k];
					}
					for(k=0;k<81;k++) {
						comet[j].full[k]='\0';
						comet[j].full[k]=temp.full[k];
					}

					for(k=0;k<56;k++) {
						temp.name[k]='\0';
						temp.name[k]=comet[i].name[k];
					}
					for(k=0;k<56;k++) {
						comet[i].name[k]='\0';
						comet[i].name[k]=comet[j].name[k];
					}
					for(k=0;k<56;k++) {
						comet[j].name[k]='\0';
						comet[j].name[k]=temp.name[k];
					}

					for(k=0;k<26;k++) {
						temp.ID[k]='\0';
						temp.ID[k]=comet[i].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[i].ID[k]='\0';
						comet[i].ID[k]=comet[j].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[j].ID[k]='\0';
						comet[j].ID[k]=temp.ID[k];
					}

					temp.T = comet[i].T;
					comet[i].T = comet[j].T;
					comet[j].T = temp.T;

					temp.y = comet[i].y;
					comet[i].y = comet[j].y;
					comet[j].y = temp.y;

					temp.m = comet[i].m;
					comet[i].m = comet[j].m;
					comet[j].m = temp.m;

					temp.d = comet[i].d;
					comet[i].d = comet[j].d;
					comet[j].d = temp.d;

					temp.h = comet[i].h;
					comet[i].h = comet[j].h;
					comet[j].h = temp.h;

					temp.P = comet[i].P;
					comet[i].P = comet[j].P;
					comet[j].P = temp.P;

					temp.q = comet[i].q;
					comet[i].q = comet[j].q;
					comet[j].q = temp.q;

					temp.e = comet[i].e;
					comet[i].e = comet[j].e;
					comet[j].e = temp.e;

					temp.i = comet[i].i;
					comet[i].i = comet[j].i;
					comet[j].i = temp.i;

					temp.an = comet[i].an;
					comet[i].an = comet[j].an;
					comet[j].an = temp.an;

					temp.pn = comet[i].pn;
					comet[i].pn = comet[j].pn;
					comet[j].pn = temp.pn;

					temp.H = comet[i].H;
					comet[i].H = comet[j].H;
					comet[j].H = temp.H;

					temp.G = comet[i].G;
					comet[i].G = comet[j].G;
					comet[j].G = temp.G;

					for(k=0;k<21;k++) {
						temp.book[k]='\0';
						temp.book[k]=comet[i].book[k];
					}
					for(k=0;k<21;k++) {
						comet[i].book[k]='\0';
						comet[i].book[k]=comet[j].book[k];
					}
					for(k=0;k<21;k++) {
						comet[j].book[k]='\0';
						comet[j].book[k]=temp.book[k];
					}
				}
			}

			if (sortKey=='f' && dir=='a'){

				if (comet[i].pn > comet[j].pn){

					for(k=0;k<81;k++) {
						temp.full[k]='\0';
						temp.full[k]=comet[i].full[k];
					}
					for(k=0;k<81;k++) {
						comet[i].full[k]='\0';
						comet[i].full[k]=comet[j].full[k];
					}
					for(k=0;k<81;k++) {
						comet[j].full[k]='\0';
						comet[j].full[k]=temp.full[k];
					}

					for(k=0;k<56;k++) {
						temp.name[k]='\0';
						temp.name[k]=comet[i].name[k];
					}
					for(k=0;k<56;k++) {
						comet[i].name[k]='\0';
						comet[i].name[k]=comet[j].name[k];
					}
					for(k=0;k<56;k++) {
						comet[j].name[k]='\0';
						comet[j].name[k]=temp.name[k];
					}

					for(k=0;k<26;k++) {
						temp.ID[k]='\0';
						temp.ID[k]=comet[i].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[i].ID[k]='\0';
						comet[i].ID[k]=comet[j].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[j].ID[k]='\0';
						comet[j].ID[k]=temp.ID[k];
					}

					temp.T = comet[i].T;
					comet[i].T = comet[j].T;
					comet[j].T = temp.T;

					temp.y = comet[i].y;
					comet[i].y = comet[j].y;
					comet[j].y = temp.y;

					temp.m = comet[i].m;
					comet[i].m = comet[j].m;
					comet[j].m = temp.m;

					temp.d = comet[i].d;
					comet[i].d = comet[j].d;
					comet[j].d = temp.d;

					temp.h = comet[i].h;
					comet[i].h = comet[j].h;
					comet[j].h = temp.h;

					temp.P = comet[i].P;
					comet[i].P = comet[j].P;
					comet[j].P = temp.P;

					temp.q = comet[i].q;
					comet[i].q = comet[j].q;
					comet[j].q = temp.q;

					temp.e = comet[i].e;
					comet[i].e = comet[j].e;
					comet[j].e = temp.e;

					temp.i = comet[i].i;
					comet[i].i = comet[j].i;
					comet[j].i = temp.i;

					temp.an = comet[i].an;
					comet[i].an = comet[j].an;
					comet[j].an = temp.an;

					temp.pn = comet[i].pn;
					comet[i].pn = comet[j].pn;
					comet[j].pn = temp.pn;

					temp.H = comet[i].H;
					comet[i].H = comet[j].H;
					comet[j].H = temp.H;

					temp.G = comet[i].G;
					comet[i].G = comet[j].G;
					comet[j].G = temp.G;

					for(k=0;k<21;k++) {
						temp.book[k]='\0';
						temp.book[k]=comet[i].book[k];
					}
					for(k=0;k<21;k++) {
						comet[i].book[k]='\0';
						comet[i].book[k]=comet[j].book[k];
					}
					for(k=0;k<21;k++) {
						comet[j].book[k]='\0';
						comet[j].book[k]=temp.book[k];
					}
				}
			}

			if (sortKey=='f' && dir=='b'){

				if (comet[i].pn < comet[j].pn){

					for(k=0;k<81;k++) {
						temp.full[k]='\0';
						temp.full[k]=comet[i].full[k];
					}
					for(k=0;k<81;k++) {
						comet[i].full[k]='\0';
						comet[i].full[k]=comet[j].full[k];
					}
					for(k=0;k<81;k++) {
						comet[j].full[k]='\0';
						comet[j].full[k]=temp.full[k];
					}

					for(k=0;k<56;k++) {
						temp.name[k]='\0';
						temp.name[k]=comet[i].name[k];
					}
					for(k=0;k<56;k++) {
						comet[i].name[k]='\0';
						comet[i].name[k]=comet[j].name[k];
					}
					for(k=0;k<56;k++) {
						comet[j].name[k]='\0';
						comet[j].name[k]=temp.name[k];
					}

					for(k=0;k<26;k++) {
						temp.ID[k]='\0';
						temp.ID[k]=comet[i].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[i].ID[k]='\0';
						comet[i].ID[k]=comet[j].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[j].ID[k]='\0';
						comet[j].ID[k]=temp.ID[k];
					}

					temp.T = comet[i].T;
					comet[i].T = comet[j].T;
					comet[j].T = temp.T;

					temp.y = comet[i].y;
					comet[i].y = comet[j].y;
					comet[j].y = temp.y;

					temp.m = comet[i].m;
					comet[i].m = comet[j].m;
					comet[j].m = temp.m;

					temp.d = comet[i].d;
					comet[i].d = comet[j].d;
					comet[j].d = temp.d;

					temp.h = comet[i].h;
					comet[i].h = comet[j].h;
					comet[j].h = temp.h;

					temp.P = comet[i].P;
					comet[i].P = comet[j].P;
					comet[j].P = temp.P;

					temp.q = comet[i].q;
					comet[i].q = comet[j].q;
					comet[j].q = temp.q;

					temp.e = comet[i].e;
					comet[i].e = comet[j].e;
					comet[j].e = temp.e;

					temp.i = comet[i].i;
					comet[i].i = comet[j].i;
					comet[j].i = temp.i;

					temp.an = comet[i].an;
					comet[i].an = comet[j].an;
					comet[j].an = temp.an;

					temp.pn = comet[i].pn;
					comet[i].pn = comet[j].pn;
					comet[j].pn = temp.pn;

					temp.H = comet[i].H;
					comet[i].H = comet[j].H;
					comet[j].H = temp.H;

					temp.G = comet[i].G;
					comet[i].G = comet[j].G;
					comet[j].G = temp.G;

					for(k=0;k<21;k++) {
						temp.book[k]='\0';
						temp.book[k]=comet[i].book[k];
					}
					for(k=0;k<21;k++) {
						comet[i].book[k]='\0';
						comet[i].book[k]=comet[j].book[k];
					}
					for(k=0;k<21;k++) {
						comet[j].book[k]='\0';
						comet[j].book[k]=temp.book[k];
					}
				}
			}

			if (sortKey=='g' && dir=='a'){

				if (comet[i].i > comet[j].i){

					for(k=0;k<81;k++) {
						temp.full[k]='\0';
						temp.full[k]=comet[i].full[k];
					}
					for(k=0;k<81;k++) {
						comet[i].full[k]='\0';
						comet[i].full[k]=comet[j].full[k];
					}
					for(k=0;k<81;k++) {
						comet[j].full[k]='\0';
						comet[j].full[k]=temp.full[k];
					}

					for(k=0;k<56;k++) {
						temp.name[k]='\0';
						temp.name[k]=comet[i].name[k];
					}
					for(k=0;k<56;k++) {
						comet[i].name[k]='\0';
						comet[i].name[k]=comet[j].name[k];
					}
					for(k=0;k<56;k++) {
						comet[j].name[k]='\0';
						comet[j].name[k]=temp.name[k];
					}

					for(k=0;k<26;k++) {
						temp.ID[k]='\0';
						temp.ID[k]=comet[i].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[i].ID[k]='\0';
						comet[i].ID[k]=comet[j].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[j].ID[k]='\0';
						comet[j].ID[k]=temp.ID[k];
					}

					temp.T = comet[i].T;
					comet[i].T = comet[j].T;
					comet[j].T = temp.T;

					temp.y = comet[i].y;
					comet[i].y = comet[j].y;
					comet[j].y = temp.y;

					temp.m = comet[i].m;
					comet[i].m = comet[j].m;
					comet[j].m = temp.m;

					temp.d = comet[i].d;
					comet[i].d = comet[j].d;
					comet[j].d = temp.d;

					temp.h = comet[i].h;
					comet[i].h = comet[j].h;
					comet[j].h = temp.h;

					temp.P = comet[i].P;
					comet[i].P = comet[j].P;
					comet[j].P = temp.P;

					temp.q = comet[i].q;
					comet[i].q = comet[j].q;
					comet[j].q = temp.q;

					temp.e = comet[i].e;
					comet[i].e = comet[j].e;
					comet[j].e = temp.e;

					temp.i = comet[i].i;
					comet[i].i = comet[j].i;
					comet[j].i = temp.i;

					temp.an = comet[i].an;
					comet[i].an = comet[j].an;
					comet[j].an = temp.an;

					temp.pn = comet[i].pn;
					comet[i].pn = comet[j].pn;
					comet[j].pn = temp.pn;

					temp.H = comet[i].H;
					comet[i].H = comet[j].H;
					comet[j].H = temp.H;

					temp.G = comet[i].G;
					comet[i].G = comet[j].G;
					comet[j].G = temp.G;

					for(k=0;k<21;k++) {
						temp.book[k]='\0';
						temp.book[k]=comet[i].book[k];
					}
					for(k=0;k<21;k++) {
						comet[i].book[k]='\0';
						comet[i].book[k]=comet[j].book[k];
					}
					for(k=0;k<21;k++) {
						comet[j].book[k]='\0';
						comet[j].book[k]=temp.book[k];
					}
				}
			}

			if (sortKey=='g' && dir=='b'){

				if (comet[i].i < comet[j].i){

					for(k=0;k<81;k++) {
						temp.full[k]='\0';
						temp.full[k]=comet[i].full[k];
					}
					for(k=0;k<81;k++) {
						comet[i].full[k]='\0';
						comet[i].full[k]=comet[j].full[k];
					}
					for(k=0;k<81;k++) {
						comet[j].full[k]='\0';
						comet[j].full[k]=temp.full[k];
					}

					for(k=0;k<56;k++) {
						temp.name[k]='\0';
						temp.name[k]=comet[i].name[k];
					}
					for(k=0;k<56;k++) {
						comet[i].name[k]='\0';
						comet[i].name[k]=comet[j].name[k];
					}
					for(k=0;k<56;k++) {
						comet[j].name[k]='\0';
						comet[j].name[k]=temp.name[k];
					}

					for(k=0;k<26;k++) {
						temp.ID[k]='\0';
						temp.ID[k]=comet[i].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[i].ID[k]='\0';
						comet[i].ID[k]=comet[j].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[j].ID[k]='\0';
						comet[j].ID[k]=temp.ID[k];
					}

					temp.T = comet[i].T;
					comet[i].T = comet[j].T;
					comet[j].T = temp.T;

					temp.y = comet[i].y;
					comet[i].y = comet[j].y;
					comet[j].y = temp.y;

					temp.m = comet[i].m;
					comet[i].m = comet[j].m;
					comet[j].m = temp.m;

					temp.d = comet[i].d;
					comet[i].d = comet[j].d;
					comet[j].d = temp.d;

					temp.h = comet[i].h;
					comet[i].h = comet[j].h;
					comet[j].h = temp.h;

					temp.P = comet[i].P;
					comet[i].P = comet[j].P;
					comet[j].P = temp.P;

					temp.q = comet[i].q;
					comet[i].q = comet[j].q;
					comet[j].q = temp.q;

					temp.e = comet[i].e;
					comet[i].e = comet[j].e;
					comet[j].e = temp.e;

					temp.i = comet[i].i;
					comet[i].i = comet[j].i;
					comet[j].i = temp.i;

					temp.an = comet[i].an;
					comet[i].an = comet[j].an;
					comet[j].an = temp.an;

					temp.pn = comet[i].pn;
					comet[i].pn = comet[j].pn;
					comet[j].pn = temp.pn;

					temp.H = comet[i].H;
					comet[i].H = comet[j].H;
					comet[j].H = temp.H;

					temp.G = comet[i].G;
					comet[i].G = comet[j].G;
					comet[j].G = temp.G;

					for(k=0;k<21;k++) {
						temp.book[k]='\0';
						temp.book[k]=comet[i].book[k];
					}
					for(k=0;k<21;k++) {
						comet[i].book[k]='\0';
						comet[i].book[k]=comet[j].book[k];
					}
					for(k=0;k<21;k++) {
						comet[j].book[k]='\0';
						comet[j].book[k]=temp.book[k];
					}
				}
			}

			if (sortKey=='h' && dir=='a'){

				if (comet[i].P > comet[j].P){

					for(k=0;k<81;k++) {
						temp.full[k]='\0';
						temp.full[k]=comet[i].full[k];
					}
					for(k=0;k<81;k++) {
						comet[i].full[k]='\0';
						comet[i].full[k]=comet[j].full[k];
					}
					for(k=0;k<81;k++) {
						comet[j].full[k]='\0';
						comet[j].full[k]=temp.full[k];
					}

					for(k=0;k<56;k++) {
						temp.name[k]='\0';
						temp.name[k]=comet[i].name[k];
					}
					for(k=0;k<56;k++) {
						comet[i].name[k]='\0';
						comet[i].name[k]=comet[j].name[k];
					}
					for(k=0;k<56;k++) {
						comet[j].name[k]='\0';
						comet[j].name[k]=temp.name[k];
					}

					for(k=0;k<26;k++) {
						temp.ID[k]='\0';
						temp.ID[k]=comet[i].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[i].ID[k]='\0';
						comet[i].ID[k]=comet[j].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[j].ID[k]='\0';
						comet[j].ID[k]=temp.ID[k];
					}

					temp.T = comet[i].T;
					comet[i].T = comet[j].T;
					comet[j].T = temp.T;

					temp.y = comet[i].y;
					comet[i].y = comet[j].y;
					comet[j].y = temp.y;

					temp.m = comet[i].m;
					comet[i].m = comet[j].m;
					comet[j].m = temp.m;

					temp.d = comet[i].d;
					comet[i].d = comet[j].d;
					comet[j].d = temp.d;

					temp.h = comet[i].h;
					comet[i].h = comet[j].h;
					comet[j].h = temp.h;

					temp.P = comet[i].P;
					comet[i].P = comet[j].P;
					comet[j].P = temp.P;

					temp.q = comet[i].q;
					comet[i].q = comet[j].q;
					comet[j].q = temp.q;

					temp.e = comet[i].e;
					comet[i].e = comet[j].e;
					comet[j].e = temp.e;

					temp.i = comet[i].i;
					comet[i].i = comet[j].i;
					comet[j].i = temp.i;

					temp.an = comet[i].an;
					comet[i].an = comet[j].an;
					comet[j].an = temp.an;

					temp.pn = comet[i].pn;
					comet[i].pn = comet[j].pn;
					comet[j].pn = temp.pn;

					temp.H = comet[i].H;
					comet[i].H = comet[j].H;
					comet[j].H = temp.H;

					temp.G = comet[i].G;
					comet[i].G = comet[j].G;
					comet[j].G = temp.G;

					for(k=0;k<21;k++) {
						temp.book[k]='\0';
						temp.book[k]=comet[i].book[k];
					}
					for(k=0;k<21;k++) {
						comet[i].book[k]='\0';
						comet[i].book[k]=comet[j].book[k];
					}
					for(k=0;k<21;k++) {
						comet[j].book[k]='\0';
						comet[j].book[k]=temp.book[k];
					}
				}
			}

			if (sortKey=='h' && dir=='b'){

				if (comet[i].P < comet[j].P){

					for(k=0;k<81;k++) {
						temp.full[k]='\0';
						temp.full[k]=comet[i].full[k];
					}
					for(k=0;k<81;k++) {
						comet[i].full[k]='\0';
						comet[i].full[k]=comet[j].full[k];
					}
					for(k=0;k<81;k++) {
						comet[j].full[k]='\0';
						comet[j].full[k]=temp.full[k];
					}

					for(k=0;k<56;k++) {
						temp.name[k]='\0';
						temp.name[k]=comet[i].name[k];
					}
					for(k=0;k<56;k++) {
						comet[i].name[k]='\0';
						comet[i].name[k]=comet[j].name[k];
					}
					for(k=0;k<56;k++) {
						comet[j].name[k]='\0';
						comet[j].name[k]=temp.name[k];
					}

					for(k=0;k<26;k++) {
						temp.ID[k]='\0';
						temp.ID[k]=comet[i].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[i].ID[k]='\0';
						comet[i].ID[k]=comet[j].ID[k];
					}
					for(k=0;k<26;k++) {
						comet[j].ID[k]='\0';
						comet[j].ID[k]=temp.ID[k];
					}

					temp.T = comet[i].T;
					comet[i].T = comet[j].T;
					comet[j].T = temp.T;

					temp.y = comet[i].y;
					comet[i].y = comet[j].y;
					comet[j].y = temp.y;

					temp.m = comet[i].m;
					comet[i].m = comet[j].m;
					comet[j].m = temp.m;

					temp.d = comet[i].d;
					comet[i].d = comet[j].d;
					comet[j].d = temp.d;

					temp.h = comet[i].h;
					comet[i].h = comet[j].h;
					comet[j].h = temp.h;

					temp.P = comet[i].P;
					comet[i].P = comet[j].P;
					comet[j].P = temp.P;

					temp.q = comet[i].q;
					comet[i].q = comet[j].q;
					comet[j].q = temp.q;

					temp.e = comet[i].e;
					comet[i].e = comet[j].e;
					comet[j].e = temp.e;

					temp.i = comet[i].i;
					comet[i].i = comet[j].i;
					comet[j].i = temp.i;

					temp.an = comet[i].an;
					comet[i].an = comet[j].an;
					comet[j].an = temp.an;

					temp.pn = comet[i].pn;
					comet[i].pn = comet[j].pn;
					comet[j].pn = temp.pn;

					temp.H = comet[i].H;
					comet[i].H = comet[j].H;
					comet[j].H = temp.H;

					temp.G = comet[i].G;
					comet[i].G = comet[j].G;
					comet[j].G = temp.G;

					for(k=0;k<21;k++) {
						temp.book[k]='\0';
						temp.book[k]=comet[i].book[k];
					}
					for(k=0;k<21;k++) {
						comet[i].book[k]='\0';
						comet[i].book[k]=comet[j].book[k];
					}
					for(k=0;k<21;k++) {
						comet[j].book[k]='\0';
						comet[j].book[k]=temp.book[k];
					}
				}
			}
		}
	}
}
