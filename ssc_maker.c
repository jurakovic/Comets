// Celestia and Stellarium Format Maker

#include "ssc.h"

char *soft;
char *input_format;
char *output_format;
char  fin_name[80+1];
char  fout_name[80+1];

struct Comet{
	char name [80+1];
	char ID [16+1];
	long int JD;
	int y;
	int m;
	int d;
	int h;
	int eq;
	double P;
	float q;
	float e;
	float pn;
	float an;
	float i;
	float H;
	float G;
} comet[5000];


int main (){

	int type, a;

	struct Menu {
		char format[25];
		char soft[15];
	} menu[20] = {
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
		{ format: "NASA", soft: "CSV format" }
	};

	system("COLOR 9");

	do {
		start_screen();
		scanf("%d", &type);
        if (type >= 0 && type < 20) {
        	input_format = menu[type].format;
        	soft = menu[type].soft;
        	a = import_menu(type);
		}
	} while (type!=21 && a!=2);   // vrti se sve dok na prvom izborniku nije upisan broj 21 ili na drugom broj 2

	exit_screen();
	getch();
	return 0;
}


void start_screen (){

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	printf("\n");
	printf("                      CELESTIA AND STELLARIUM FORMAT MAKER\n\n");
	printf(" ==============================================================================\n\n");
	printf("  Supported input formats: \n\n");
	printf("        0. MPC                         12. MegaStar V4.x\n");
	printf("        1. SkyMap                      13. SkyChart III\n");
	printf("        2. Guide                       14. Voyager II\n");
	printf("        3. xephem                      15. SkyTools\n");
	printf("        4. Home Planet                 16. Autostar\n");
	printf("        5. MyStars!\n");
	printf("        6. TheSky                      17. Comet for Windows\n");
	printf("        7. Starry Night                18. NASA (ELEMENTS.COMET)\n");
	printf("        8. Deep Space                  19. NASA (CSV format)\n");
	printf("        9. PC-TCS\n");
	printf("       10. Earth Centered Universe     20. Help\n");
	printf("       11. Dance of the Planets        21. Exit\n\n");
	printf("  Select option [0-21]: ");
}

void screen_imp (){

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	printf("\n");
	printf("  Importing %s (%s) format...\n\n", input_format, soft);
	printf(" =============================================================================\n");
	printf("     1.   Main Menu   |   2.   Exit   \n");
	printf(" =============================================================================\n\n");
}

void screen_exp1 (){

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	printf("\n");
	printf("  Exporting %s format...\n\n", input_format);
	printf(" =============================================================================\n");
	printf("     1.   Main Menu   |   2.   Exit   \n");
	printf(" =============================================================================\n\n");
}

void screen_exp2 (){

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	printf("\n");
	printf("  Exporting %s as %s format...\n\n", input_format, output_format);
	printf(" =============================================================================\n");
	printf("     1.   Main Menu   |   2.   Exit   \n");
	printf(" =============================================================================\n\n");
}

void screen_exp3 (){

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	printf("\n");
	printf("  %s exported as %s format...\n\n", input_format, output_format);
	printf(" =============================================================================\n");
	printf("     1.   Main Menu   |   2.   Exit   \n");
	printf(" =============================================================================\n\n");
}

void exit_screen(){

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	printf("\n\n");
	printf("             ____     _          _   _                          _ \n");
	printf("            / ___|___| | ___ ___| |_(_) __ _     __ _ _ __   __| |\n");
	printf("           | |   / _ \\ |/ _ \\ __| __| |/ _` |   / _` | '_ \\ / _` |\n");
	printf("           | |___  __/ |  __\\__ \\ |_| | (_| |  | (_| | | | | (_| |\n");
	printf("            \\____\\___|_|\\___|___/\\__|_|\\__,_|   \\__,_|_| |_|\\__,_|\n\n");
	printf("                ____  _       _ _            _                 \n");
	printf("               / ___|| |_ ___| | | __ _ _ __(_)_   _ _ __ ___  \n");
	printf("               \\___ \\| __/ _ \\ | |/ _` | '__| | | | | '_ ` _ \\ \n");
	printf("                ___) | |_  __/ | | (_| | |  | | |_| | | | | | |\n");
	printf("               |____/ \\__\\___|_|_|\\__,_|_|  |_|\\__,_|_| |_| |_|\n\n");
	printf("       _____                          _      __  __       _             \n");
	printf("      |  ___|__  _ __ _ __ ___   __ _| |_   |  \\/  | __ _| | _____ _ __ \n");
	printf("      | |_ / _ \\| '__| '_ ` _ \\ / _` | __|  | |\\/| |/ _` | |/ / _ \\ '__|\n");
	printf("      |  _| (_) | |  | | | | | | (_| | |_   | |  | | (_| |   <  __/ |   \n");
	printf("      |_|  \\___/|_|  |_| |_| |_|\\__,_|\\__|  |_|  |_|\\__,_|_|\\_\\___|_|");
	printf("\n\n\n\n\n\n");
	printf("Press any key to exit...                             Copyright (c) 2011, jurluk");
}


int import_menu (int Ty){

	int Ncmt=0, b;
	char a, c;
	FILE *fin;

	screen_imp();

    do {
        printf("  Enter input filename: ");
        scanf("%s", fin_name);

        if (fin_name[0]=='1' && fin_name[1]=='\0') return 1;
        if (fin_name[0]=='2' && fin_name[1]=='\0') return 2;

        fin=fopen(fin_name, "r");
        if (fin==NULL) printf("\n  Error opening file %s\n\n", fin_name);
    } while (fin==NULL);

    printf("\n  File %s is successfully opened\n", fin_name);

	while ((c=fgetc(fin)) != EOF){
		if (c=='\n') Ncmt++;
	}

	if (Ty==17) Ncmt=Ncmt/13;						// jer je 17. format cfw, a jedan komet je definiran kroz 13 redova
	if (Ty==3 || Ty==8 || Ty==10) Ncmt=Ncmt/2;		// kao gore, samo što je 1 komet kroz 2 reda

	printf("\n  Total detected comets: %d\n  ", Ncmt);
	printf("\n  Press any key to continue... ");
	getch();

	if (Ty==4 || Ty==11 || Ty==14 || Ty==18 || Ty==19){
		screen_exp1();
		output_format="Celestia (SSC)";
		printf("  %s format can be exported only as %s format\n  ", input_format, output_format);
		printf("\n  Press any key to continue... ");
		getch();
	}

	else {
		do {
			screen_exp1();
			printf("  Export as:\n\n");
			printf("	    a. Celestia (SSC)\n");
			printf("	    b. Stellarium\n\n");
			printf("  Select option:   ");
			scanf("%c", &a);
		} while (a!='a' && a!='b' && a!='1' && a!='2');

		if (a=='1') return 1;
		if (a=='2') return 2;

		if (a=='a') output_format="Celestia (SSC)";
		if (a=='b') output_format="Stellarium";
	}

	screen_exp2();
	printf("  Enter output filename: ");
	scanf("%s", fout_name);

	if (fout_name[0]=='1' && fout_name[1]=='\0') return 1;
	if (fout_name[0]=='2' && fout_name[1]=='\0') return 2;

	if (Ty==0) import_mpc (Ncmt);
	if (Ty==1) import_skymap (Ncmt);
	if (Ty==2) import_guide (Ncmt);
	if (Ty==3) import_xephem (Ncmt);
	if (Ty==4) import_home_planet (Ncmt);
	if (Ty==5) import_mystars (Ncmt);
	if (Ty==6 || Ty==16) import_thesky (Ncmt);		//jer imaju isti format
	if (Ty==7) import_starry_night (Ncmt);
	if (Ty==8) import_deep_space (Ncmt);
	if (Ty==9) import_pc_tcs (Ncmt);
	if (Ty==10) import_ecu (Ncmt);
	if (Ty==11) import_dance (Ncmt);
	if (Ty==12) import_megastar (Ncmt);
	if (Ty==13) import_skychart (Ncmt);
	if (Ty==14) import_voyager (Ncmt);
	if (Ty==15) import_skytools (Ncmt);
	if (Ty==17) import_cfw (Ncmt);
	if (Ty==18) import_nasa1 (Ncmt);
	if (Ty==19) import_nasa2 (Ncmt);

	fclose(fin);

	if (Ty==4 || Ty==11 || Ty==14 || Ty==18 || Ty==19) output_ssc (Ncmt, Ty);

	else {
		if (a=='a') output_ssc  (Ncmt, Ty);
		if (a=='b') output_stell  (Ncmt, Ty);
	}

	do {
		screen_exp3();
		printf("  Done\n\n  %d comets successfully saved in file %s\n\n", Ncmt, fout_name);
		printf("  Select option: ");
		scanf("%d", &b);
	} while (b!=1 && b!=2);

	return b;
}


void import_mpc (int N){

	int i;
	char c, x[14+1];
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {

		fscanf(fin, "%14c %d %d %d.%d %f %f %f %f %f%12c%f %f %75[^\n]%*c",		// %f%12c%f mora bit tako zajedno
			x, &comet[i].y, &comet[i].m, &comet[i].d, &comet[i].h,
			&comet[i].q, &comet[i].e, &comet[i].pn, &comet[i].an,
			&comet[i].i, x, &comet[i].H, &comet[i].G, comet[i].name);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name(comet[i].name);
	}
}

void import_skymap (int N){

	int i;
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {

		fscanf(fin, "%47c %4d %2d %2d.%4d %f %f %f %f %f %f %f\n",
			comet[i].name, &comet[i].y, &comet[i].m, &comet[i].d,
			&comet[i].h, &comet[i].q, &comet[i].e, &comet[i].pn,
			&comet[i].an, &comet[i].i, &comet[i].H, &comet[i].G);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].name);
	}
}

void import_guide (int N){

	int i, j;
	char c, x[20];
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {
		j=0;
		while ((c=fgetc(fin)) != '(' ){
			comet[i].name[j++]=c;
		}
		comet[i].name[j-1]='\0';

		if (comet[i].name[0]=='P' && comet[i].name[1]=='/')
			for (j=0; j<strlen(comet[i].name); j++)
				comet[i].name[j]=comet[i].name[j+2];		//k+2 jer je na mjestu 0='P', 1='/'

		j=0;
		while ((c=fgetc(fin)) != ')' ){
			comet[i].ID[j++]=c;
		}
		comet[i].ID[j]='\0';

		fscanf(fin, "%d.%d %d %d 0.0 %f %f %f %f %f %d.0 %f %f %15[^\n]%*c",
			&comet[i].d, &comet[i].h, &comet[i].m, &comet[i].y,
			&comet[i].q, &comet[i].e, &comet[i].i, &comet[i].pn, &comet[i].an,
			&comet[i].eq, &comet[i].H, &comet[i].G, x);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].ID);
	}
}

void import_xephem (int N){

	int i, j, JD, z;
	float a, n;
	char c, x[25+1];
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {

		fscanf(fin, "%25[^\n]%*c", x);

		j=0;
		while ((c=fgetc(fin)) != ',' ){
			comet[i].name[j++]=c;
		}

		c=fgetc(fin);

		if(c == 'e'){
			fscanf(fin, ",%f,%f,%f,%f,%f,%f,%d.%d,%d/%d.%d/%d,%d,g %f,%f\n",
				&comet[i].i, &comet[i].an, &comet[i].pn, &a,
				&n, &comet[i].e, &JD, &comet[i].h, &comet[i].m, &comet[i].d,
				&z, &comet[i].y, &comet[i].eq, &comet[i].H, &comet[i].G);

			comet[i].q = a*(1-comet[i].e);
			comet[i].JD = JD + compute_JD (comet[i].y, comet[i].m, comet[i].d);
		}

		if(c == 'p'){
			fscanf(fin, ",%d/%d.%d/%d,%f,%f,%f,%f,%d,%f,%f\n",
				&comet[i].m, &comet[i].d, &comet[i].h, &comet[i].y,
				&comet[i].i, &comet[i].pn, &comet[i].q, &comet[i].an,
				&comet[i].eq, &comet[i].H, &comet[i].G);

			comet[i].e = 1.000000;
			comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		}

		if(c == 'h'){
			fscanf(fin, ",%d/%d.%d/%d,%f,%f,%f,%f,%f,%d,%f,%f\n",
				&comet[i].m, &comet[i].d, &comet[i].h, &comet[i].y,
				&comet[i].i, &comet[i].an, &comet[i].pn, &comet[i].e,
				&comet[i].q, &comet[i].eq, &comet[i].H, &comet[i].G);

			comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		}

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		edit_name (comet[i].name);
	}
}

void import_home_planet (int N){

	int i, j;
	char c, x[50+1];
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {

		j=0;
		while ((c=fgetc(fin)) != ',' ){
			comet[i].name[j++]=c;
		}

		fscanf(fin, "%d-%d-%d.%d,%f,%f,%f,%f,%f,%50[^\n]%*c",
			&comet[i].y, &comet[i].m, &comet[i].d, &comet[i].h,
			&comet[i].q, &comet[i].e, &comet[i].pn, &comet[i].an,
			&comet[i].i, x);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].name);
	}
}

void import_mystars (int N){

	int i, j;
	char c, x[30+1];
	FILE *fin = fopen(fin_name, "r");

// 	varijable za izracun gregorijanskog datuma iz julijanskog dana
	int v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13;

	for (i=0; i<N; i++) {

		j=0;
		while ((c=fgetc(fin)) != ';' ){
			comet[i].name[j++]=c;
		}

		fscanf(fin, "%d.%d %f %f %f %f %f %f %f %30[^\n]%*c",
			&comet[i].JD, &comet[i].h, &comet[i].pn, &comet[i].e,
			&comet[i].q, &comet[i].i, &comet[i].an, &comet[i].H,
			&comet[i].G, x);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD += 2400000;
		edit_name (comet[i].name);

//		izracuvanje gregorijanskog datuma iz julijanskog dana
//		izvor: http://en.wikipedia.org/wiki/Julian_day#Gregorian_calendar_from_Julian_day_number

		v1 = comet[i].JD + 0.5;
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
		comet[i].d = v13 + 2;
	}
}

void import_thesky (int N){

	int i;
	char x[20+1];
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {
//		fscanf(fin, "%40c %*c %d %*c %4d %2d %2d %*c %d %*c %f %*c %f %*c %f %*c %f %*c %f %*c %f %25[^\n]%*c",     stari nacin
		fscanf(fin, "%45c %4d%2d%2d.%d | %f | %f | %f | %f | %f | %f | %f %20[^\n]%*c",
			comet[i].name, &comet[i].y, &comet[i].m,
			&comet[i].d, &comet[i].h, &comet[i].q, &comet[i].e,
			&comet[i].pn, &comet[i].an, &comet[i].i, &comet[i].H,
			&comet[i].G, x);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].name);
	}
}

void import_starry_night (int N){

	int i, j, k;
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
			j++;
			k++;
		}

		fscanf(fin, "%f 0.0 %f %f %f %f %f %d.%d %d.5 %f %16c %20[^\n]%*c",
			&comet[i].H, &comet[i].e, &comet[i].q, &comet[i].an,
			&comet[i].pn, &comet[i].i, &comet[i].JD, &comet[i].h,
			&y, &comet[i].G, comet[i].ID, x);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		edit_name (comet[i].name);
		edit_name (comet[i].ID);

//		izracuvanje gregorijanskog datuma iz julijanskog dana
//		izvor: http://en.wikipedia.org/wiki/Julian_day#Gregorian_calendar_from_Julian_day_number

		v1 = comet[i].JD + 0.5;
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
		comet[i].d = v13 + 2;
	}
}

void import_deep_space (int N){

	int i, j;
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

		fscanf(fin, "\n%8c %d %d %d.%d %f %f %f %f %f %f %f\n",
			x, &comet[i].y, &comet[i].m, &comet[i].d, &comet[i].h,
			&comet[i].q, &comet[i].e, &comet[i].pn, &comet[i].an,
			&comet[i].i, &comet[i].H, &comet[i].G);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].ID);
	}
}

void import_pc_tcs (int N){

	int i;
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {

		fscanf(fin, "%s %f %f %f %f %f %d %d %d.%d %f %f %60[^\n]%*c",
			comet[i].ID, &comet[i].q, &comet[i].e, &comet[i].i,
			&comet[i].pn, &comet[i].an, &comet[i].y, &comet[i].m,
			&comet[i].d, &comet[i].h, &comet[i].H, &comet[i].G, comet[i].name);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].ID);
		edit_name (comet[i].name);
	}
}

void import_ecu (int N){

	int i;
	char x[8+1];
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {

		fscanf(fin, "%45[^\n]%*c %8c %d %d %d.%d %f %f %f %f %f %f %f\n",
			comet[i].name, x, &comet[i].y, &comet[i].m, &comet[i].d,
			&comet[i].h, &comet[i].q, &comet[i].e, &comet[i].pn, &comet[i].an,
			&comet[i].i, &comet[i].H, &comet[i].G);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].name);
	}
}

void import_dance (int N){

	int i;
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {

		fscanf(fin, "%11c %f %f %f %f %f %d.%2d%2d%4d %30[^\n]%*c",
			comet[i].ID, &comet[i].q, &comet[i].e, &comet[i].i,
			&comet[i].an, &comet[i].pn, &comet[i].y, &comet[i].m,
			&comet[i].d, &comet[i].h, comet[i].name);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].ID);
		edit_name (comet[i].name);
	}
}

void import_megastar (int N){

	int i;
	char x[25+1];
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {

		fscanf(fin, "%30c %12c %d %d %d.%d %f %f %f %f %f %f %f %25[^\n]%*c",
			comet[i].name, comet[i].ID, &comet[i].y, &comet[i].m, &comet[i].d,
			&comet[i].h, &comet[i].q, &comet[i].e, &comet[i].pn,
			&comet[i].an, &comet[i].i, &comet[i].H, &comet[i].G, x);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].ID);
		edit_name (comet[i].name);
	}
}

void import_skychart (int N){

	int i, j;
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {

		fscanf(fin, "P11 2000.0 -%f %f %f %f %f %d %d/%d/%d.%d %f %f 0 0 %75[^\n]%*c",
			&comet[i].q, &comet[i].e, &comet[i].i, &comet[i].pn,
			&comet[i].an, &comet[i].eq, &comet[i].y, &comet[i].m, &comet[i].d,
			&comet[i].h, &comet[i].H, &comet[i].G, comet[i].name);

		for(j=0; j<strlen(comet[i].name); j++)
			if(comet[i].name[j]==';') comet[i].name[j]='\0';

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].name);
	}
}

void import_voyager (int N){

	int i;
	char mj[3+1];
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {

		fscanf(fin, "%28c %f %f %f %f %f %f %4d %3c %d.%d %d.0\n",
			comet[i].name, &comet[i].q, &comet[i].e, &comet[i].i,
			&comet[i].an, &comet[i].pn, &comet[i].G, &comet[i].y,
			mj, &comet[i].d, &comet[i].h, &comet[i].eq);

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
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].name);
	}
}

void import_skytools (int N){

	int i;
	char x[15+1];
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {

		fscanf(fin, "C %52c %d %d %d.%d %f %f %f %f %f %f %f 0.00%d %15[^\n]%*c",
			comet[i].name, &comet[i].y, &comet[i].m, &comet[i].d, &comet[i].h,
			&comet[i].q, &comet[i].e, &comet[i].pn, &comet[i].an, &comet[i].i,
			&comet[i].H, &comet[i].G, &comet[i].eq, x);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].name);
	}
}

void import_cfw (int N){

	int i;
	char x[20+1];
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {
		fscanf(fin, "name=%40[^\n]%*c\
					%20[^\n]%*c\
					type=orbit\n\
					T=%d %d %d.%d\n\
					q=%f\n\
					e=%f\n\
					peri=%f\n\
					node=%f\n\
					i=%f\n\
					prec=2000.0\n\
					%20[^\n]%*c\
					mageq=%f %10[^\n]%*c\
					\n",
					comet[i].name,
					x,
					&comet[i].y, &comet[i].m, &comet[i].d, &comet[i].h,
					&comet[i].q,
					&comet[i].e,
					&comet[i].pn,
					&comet[i].an,
					&comet[i].i,
					x,
					&comet[i].H,
					x);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].name);
	}
}

void import_nasa1 (int N){

	int i, j, k;
	char c, x[20+1];
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {

		k=0; j=0;
		while (k<44){
			c=fgetc(fin);
			comet[i].name[j]=c;
			if (c==' ' && j==0) --j;
			j++;
			k++;
		}

		fscanf(fin, "%d %f %f %f %f %f %4d%2d%2d.%4d %20[^\n]%*c",
			&comet[i].eq, &comet[i].q, &comet[i].e, &comet[i].i,
			&comet[i].pn, &comet[i].an, &comet[i].y, &comet[i].m,
			&comet[i].d, &comet[i].h, x);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name(comet[i].name);

		for (j=0; j<strlen(comet[i].name); j++){
			if ((comet[i].name[j]  =='S' && comet[i].name[j+1]=='O' &&
				 comet[i].name[j+2]=='H' && comet[i].name[j+3]=='O')
	//			|| (comet[i].P > 300)
				){
				--i;
				--N;
			}
		}
	}
}

void import_nasa2 (int N){

	int i, j;
	char c, x[10+1];
	FILE *fin = fopen(fin_name, "r");

	for (i=0; i<N; i++) {

		j=0;
		fgetc(fin);		// da uzme prve navodnike
		while ((c=fgetc(fin)) != '"' ){
			comet[i].name[j]=c;
			if (c==' ' && j==0) --j;
			j++;
		}

		comet[i].name[j]='\0';

		fscanf(fin, ",%f,%f,%f,%f,%f,%4d%2d%2d.%4d%10[^\n]%*c",
			&comet[i].q, &comet[i].e, &comet[i].pn, &comet[i].an,
			&comet[i].i, &comet[i].y, &comet[i].m, &comet[i].d,
			&comet[i].h, x);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name(comet[i].name);
	}
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

long int compute_JD (int y, int m, int d){

	long int JD;

	JD = 367*y - (7*(y + (m + 9)/12))/4 -
		((3*(y + (m - 9)/7))/100 + 1)/4 +
		(275*m)/9 + d + 1721029;

	return JD;
}

char *edit_name (char *name){

	int i;

	for (i=0; i<strlen(name)-1; i++)
		if (name[i]==' ' && name[i+1]==' ') name[i]='\0';
}


void output_ssc (int N, int Ty){

	int i, j;
	char *mon;
	FILE *fout;

	for (i=0; i<N; i++) {

		if (comet[i].e == 1) comet[i].e = 1.000001;

		for (j=0; j<strlen(comet[i].name); j++)
			if (comet[i].name[j]=='/') comet[i].name[j]=' ';

		if (Ty==2 || Ty==7 || Ty==8 || Ty==9 || Ty==11 || Ty==12)
			for (j=0; j<strlen(comet[i].ID); j++)
				if (comet[i].ID[j]=='/') comet[i].ID[j]=' ';

		if (comet[i].m==1) mon="Jan";
		if (comet[i].m==2) mon="Feb";
		if (comet[i].m==3) mon="Mar";
		if (comet[i].m==4) mon="Apr";
		if (comet[i].m==5) mon="May";
		if (comet[i].m==6) mon="Jun";
		if (comet[i].m==7) mon="Jul";
		if (comet[i].m==8) mon="Aug";
		if (comet[i].m==9) mon="Sep";
		if (comet[i].m==10) mon="Oct";
		if (comet[i].m==11) mon="Nov";
		if (comet[i].m==12) mon="Dec";

		fout=fopen(fout_name, "a");

	if (Ty==2 || Ty==7 || Ty==8 || Ty==9 || Ty==11 || Ty==12)
		fprintf(fout,"\"%s %s\" \"Sol\"\n", comet[i].ID, comet[i].name);
	else
		fprintf(fout,"\"%s\" \"Sol\"\n", comet[i].name);
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
				comet[i].JD, comet[i].h, comet[i].y, mon, comet[i].d, comet[i].h);
	else
		fprintf(fout,"\tEpoch \t\t\t %d.%.4d\t# %d %s %.2d.%.4d \n",
				comet[i].JD, comet[i].h, comet[i].y, mon, comet[i].d, comet[i].h);		// a ovdje je %.4d, jer kod ty 15 .h ima 3 oznake,
		fprintf(fout,"\t} \n");															// pa bi npr 304 ispisao kao 0304 sto nije dobro
		fprintf(fout,"}\n\n\n");

		fclose(fout);
	}
}

void output_stell (int N, int Ty){

	int i;
	FILE *fout;

	for (i=0; i<N; i++) {

		fout=fopen(fout_name, "a");

	if (Ty==2 || Ty==7 || Ty==8 || Ty==9 || Ty==11 || Ty==12)
		fprintf(fout,"[%s %s]\n", comet[i].ID, comet[i].name);
	else
		fprintf(fout,"[%s]\n", comet[i].name);
		fprintf(fout,"parent=Sun\n");
		fprintf(fout,"orbit_Inclination=%f\n", comet[i].i);
		fprintf(fout,"coord_func=comet_orbit\n");
		fprintf(fout,"orbit_Eccentricity=%f\n", comet[i].e);
		fprintf(fout,"orbit_ArgOfPericenter=%f\n", comet[i].pn);
		fprintf(fout,"absolute_magnitude=%.1f\n", comet[i].H);
		fprintf(fout,"name=%s\n", comet[i].name);
		fprintf(fout,"slope_parameter=4\n");
		fprintf(fout,"lighting=false\n");
		fprintf(fout,"tex_map=nomap.png\n");
		fprintf(fout,"color=1.0, 1.0, 1.0\n");
		fprintf(fout,"orbit_AscendingNode=%f\n", comet[i].an);
		fprintf(fout,"albedo=1\n");
		fprintf(fout,"radius=5\n");
		fprintf(fout,"orbit_PericenterDistance=%f\n", comet[i].q);
		fprintf(fout,"Ty=comet\n");
	if (Ty==15)
		fprintf(fout,"orbit_TimeAtPericenter=%d.%.3d\n\n", comet[i].JD, comet[i].h);
	else
		fprintf(fout,"orbit_TimeAtPericenter=%d.%.4d\n\n", comet[i].JD, comet[i].h);

		fclose(fout);
	}
}
