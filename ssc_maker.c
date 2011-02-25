// Celestia and Stellarium Format Maker

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>
#include <ctype.h>
#define MAX 5000

 /*
  * funkcije za ispisivanje na ekran
  */
void initscreen1();
void initscreen2();
void screen_imp();
void screen_exp1();
void screen_exp2();
void screen_exp3();

 /*
  * funkcije za citanje iz datoteka
  */
int  import_menu();				//glavna fja
void import_mpc();				//soft00
void import_skymap();			//soft01
void import_guide ();			//soft02
void import_home_planet();		//soft04
void import_mystars();			//soft05
void import_thesky();			//soft06
void import_nasa1();			//ELEMENTS.COMET
void import_nasa2();			//sbdb query
void import_cfw();				//comet for windows

 /*
  * funkcije za racunanje...
  */
float compute_period (float q, float e);
int   compute_JD (int y, int m, int d);
char *edit_name (char *name);

 /*
  * funkcije za pisanje u datoteke
  */
void output_ssc ();
void output_stell ();

struct data{
	int ty;
	char *informat;
	char *outformat;
	int N;
	char fin_name[80+1];
	char fout_name[80+1];
} f;

struct cmt{
	char name [80+1];
	int JD;
	int y;
	int m;
	int d;
	int h;
	int eq;
	float q;
	float e;
	float pn;
	float an;
	float i;
	float H;
	float G;
	float P;
	char x [80+1];
} comet[MAX];

 /*
  * glavna funkcija
  */
int main (){

	system("COLOR 9");
	int a;

	initscreen2();
	getch();
	system("CLS");

	start: do {
		fflush(stdin);
		fflush(stdout);
		system("CLS");
		printf("\n");
		printf("                      CELESTIA AND STELLARIUM FORMAT MAKER\n\n");
		printf(" ==============================================================================\n\n");
		printf("  Supported input formats: \n\n");
		printf("        0. MPC                  13. Earth Centered Universe\n");
		printf("        1. SkyMap               14. Dance of the Planets\n");
		printf("        2. Guide                15. MegaStar V4.x\n");
		printf("        3. Voyager II\n");
		printf("        4. Home Planet          16. Celestia (SSC)\n");
		printf("        5. MyStars!             17. Comet for Windows\n");
		printf("        6. TheSky               18. NASA (ELEMENTS.COMET)\n");
		printf("        7. Starry Night         19. NASA (CSV format)\n");
		printf("        8. Deep Space\n");
		printf("        9. PC-TCS               20. Online Resources\n");
		printf("       10. Autostar             21. Help\n");
		printf("       11. SkyTools             22. Exit\n");
		printf("       12. SkyChart III\n\n");
		printf("  Select option [0-22]: ");

		scanf("%d", &a);
	} while (a<0 || a>22);

	f.ty=a;

	if(a == 0) {
		f.informat = "MPC (Soft00Cmt)";
		a=import_menu();
		if (a==1) goto start;
		if (a==2) goto end;
	}

	if(a == 1) {
		f.informat = "SkyMap (Soft01Cmt)";
		a=import_menu();
		if (a==1) goto start;
		if (a==2) goto end;
	}

	if(a == 2) {
		f.informat = "Guide (Soft02Cmt)";
		a=import_menu();
		if (a==1) goto start;
		if (a==2) goto end;
	}

	if(a == 4) {
		f.informat = "Home Planet (Soft04Cmt)";
		a=import_menu();
		if (a==1) goto start;
		if (a==2) goto end;
	}

	if(a == 5) {
		f.informat = "MyStars! (Soft05Cmt)";
		a=import_menu();
		if (a==1) goto start;
		if (a==2) goto end;
	}

	if(a == 6) {
		f.informat = "TheSky! (Soft06Cmt)";
		a=import_menu();
		if (a==1) goto start;
		if (a==2) goto end;
	}

	if(a == 17) {
		f.informat = "Comet for Windows";
		a=import_menu();
		if (a==1) goto start;
		if (a==2) goto end;
	}

	if(a == 18) {
		f.informat = "NASA (ELEMENTS.COMET)";
		a=import_menu();
		if (a==1) goto start;
		if (a==2) goto end;
	}

	if(a == 19) {
		f.informat = "NASA (CSV format)";
		a=import_menu();
		if (a==1) goto start;
		if (a==2) goto end;
	}

	if(a == 22) {
		goto end;
	}

	else goto start;

	end:
	initscreen2();
	printf("Press any key to exit...                                        (c) jurluk 2011");
	getch();
	return 0;
}

 /*
  * funkcije za ispisivanje na ekran
  */
void initscreen1(){

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	printf("\n\n\n\n");
	printf("                _   _   _   _   _   _   _   _       _   _   _  \n");
	printf("               / \\ / \\ / \\ / \\ / \\ / \\ / \\ / \\     / \\ / \\ / \\ \n");
	printf("              ( C ) e ) l ) e ) s ) t ) i ) a )   ( a ) n ) d )\n");
	printf("               \\_/ \\_/ \\_/ \\_/ \\_/ \\_/ \\_/ \\_/     \\_/ \\_/ \\_/ \n");
	printf("                    _   _   _   _   _   _   _   _   _   _  \n");
	printf("                   / \\ / \\ / \\ / \\ / \\ / \\ / \\ / \\ / \\ / \\ \n");
	printf("                  ( S ) t ) e ) l ) l ) a ) r ) i ) u ) m )\n");
	printf("                   \\_/ \\_/ \\_/ \\_/ \\_/ \\_/ \\_/ \\_/ \\_/ \\_/ \n");
	printf("                _   _   _   _   _   _       _   _   _   _   _  \n");
	printf("               / \\ / \\ / \\ / \\ / \\ / \\     / \\ / \\ / \\ / \\ / \\ \n");
	printf("              ( F ) o ) r ) m ) a ) t )   ( M ) a ) k ) e ) r )\n");
	printf("               \\_/ \\_/ \\_/ \\_/ \\_/ \\_/     \\_/ \\_/ \\_/ \\_/ \\_/ \n\n\n\n\n\n\n\n\n");
}

void initscreen2(){

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
	printf("      |_|  \\___/|_|  |_| |_| |_|\\__,_|\\__|  |_|  |_|\\__,_|_|\\_\\___|_|\n\n\n\n\n\n");
}

void screen_imp (){

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	printf("\n");
	printf("  Importing %s format...\n\n", f.informat);
	printf(" =============================================================================\n");
	printf("     1.   Main Menu   |   2.   Exit   \n");
	printf(" =============================================================================\n\n");
}

void screen_exp1 (){

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	printf("\n");
	printf("  Exporting %s format...\n\n", f.informat);
	printf(" =============================================================================\n");
	printf("     1.   Main Menu   |   2.   Exit   \n");
	printf(" =============================================================================\n\n");
}

void screen_exp2 (){

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	printf("\n");
	printf("  Exporting %s as %s format...\n\n", f.informat, f.outformat);
	printf(" =============================================================================\n");
	printf("     1.   Main Menu   |   2.   Exit   \n");
	printf(" =============================================================================\n\n");
}

void screen_exp3 (){

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	printf("\n");
	printf("  %s exported as %s format...\n\n", f.informat, f.outformat);
	printf(" =============================================================================\n");
	printf("     1.   Main Menu   |   2.   Exit   \n");
	printf(" =============================================================================\n\n");
}


int import_menu (){

	int b;
	char a, c;
	FILE *fin;

	f.N=0;

	screen_imp();
	unos:
	printf("  Enter input filename: ");
	scanf("%s", f.fin_name);

	b=atoi(f.fin_name);
	if ((b==1 && !isalnum(f.fin_name[1])) ||
		(b==2 && !isalnum(f.fin_name[1]))) return b;

	fin=fopen(f.fin_name, "r");
	if (fin==NULL) {
		printf("\n  Error opening file %s\n\n", f.fin_name);
		goto unos;
	}

	else printf("\n  File %s is successfully opened\n", f.fin_name);

	while ((c=fgetc(fin)) != EOF){
		if (c=='\n') f.N++;
	}

	if(f.ty==17) f.N=f.N/13;		// jer je 17. format cfw, a jedan komet je definiran kroz 13 redova

	printf("\n  Total detected comets: %d\n  ", f.N);
	printf("\n  Press any key to continue... ");
	getch();

	do {
		screen_exp1();
		printf("  Export as: ");
		printf("\n\n	    a. Celestia (SSC)");
		printf("\n	    b. Stellarium");
		printf("\n\n  Select option:   ");
		scanf("%c", &a);
	} while (a!='a' && a!='b' && a!='1' && a!='2');

	if (a==49) return 1;
	if (a==50) return 2;

	if (a=='a') f.outformat="Celestia (SSC)";
	if (a=='b') f.outformat="Stellarium";

	screen_exp2();
	printf("  Enter output filename: ");
	scanf("%s", f.fout_name);

	b=atoi(f.fout_name);
	if ((b==1 && !isalnum(f.fout_name[1])) ||
		(b==2 && !isalnum(f.fout_name[1]))) return b;

	if(f.ty==0) import_mpc();
	if(f.ty==1) import_skymap();
	if(f.ty==2) import_guide ();
	if(f.ty==4) import_home_planet ();
	if(f.ty==5) import_mystars ();
	if(f.ty==6) import_thesky ();
	if(f.ty==18) import_nasa1();
	if(f.ty==19) import_nasa2();
	if(f.ty==17) import_cfw();

	fclose(fin);

	if (a=='a') output_ssc ();
	if (a=='b') output_stell ();

	do {
		screen_exp3();
		printf("  Done\n\n  %d comets successfully saved in file %s\n\n", f.N, f.fout_name);
		printf("  Select option: ");
		scanf("%d", &b);
	} while (b!=1 && b!=2);

	return b;
}

 /*
  * funkcije za citanje iz datoteka
  */
void import_mpc(){

	int i, N;
	FILE *fin;

	N=f.N;
	fin=fopen(f.fin_name, "r");

	for (i=0; i<N; i++) {

		fscanf(fin, "%14c %d %d %d.%d %f %f %f %f %f %d %f %f %70[^\n]%*c",
			comet[i].x, &comet[i].y, &comet[i].m, &comet[i].d, &comet[i].h,
			&comet[i].q, &comet[i].e, &comet[i].pn, &comet[i].an,
			&comet[i].i, &comet[i].eq, &comet[i].H, &comet[i].G, comet[i].name);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].name);
	}
}

void import_skymap(){

	int i, N;
	FILE *fin;

	N=f.N;
	fin=fopen(f.fin_name, "r");

	for (i=0; i<N; i++) {

			fscanf(fin, "%47c %4d %2d %2d.%4d %f %f %f %f %f %f %10[^\n]%*c",
				comet[i].name, &comet[i].y, &comet[i].m, &comet[i].d,
				&comet[i].h, &comet[i].q, &comet[i].e, &comet[i].pn,
				&comet[i].an, &comet[i].i, &comet[i].H, &comet[i].x);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].name);
	}
}

void import_guide(){

	int i, N;
	FILE *fin;

	N=f.N;
	fin=fopen(f.fin_name, "r");

	for (i=0; i<N; i++) {

			fscanf(fin, "%43c %d.%d %d %d %10c %f %f %f %f %f %10c %f %f %15[^\n]%*c",
				comet[i].name, &comet[i].d, &comet[i].h, &comet[i].m, &comet[i].y, comet[i].x,
				&comet[i].q, &comet[i].e, &comet[i].i, &comet[i].pn, &comet[i].an,
				comet[i].x, &comet[i].H, &comet[i].G, comet[i].x);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].name);
	}
}

void import_home_planet(){

	int i, j, N;
	char c;
	FILE *fin;

	N=f.N;
	fin=fopen(f.fin_name, "r");

	for (i=0; i<N; i++) {

		j=0;
		while ((c=fgetc(fin)) != ',' ){
			comet[i].name[j++]=c;
		}

		fscanf(fin, "%d-%d-%d.%d,%f,%f,%f,%f,%f %50[^\n]%*c",
			&comet[i].y, &comet[i].m, &comet[i].d, &comet[i].h,
			&comet[i].q, &comet[i].e, &comet[i].pn, &comet[i].an,
			&comet[i].i, comet[i].x);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].name);
	}
}

void import_mystars(){

	int i, j, N;
	char c;
	FILE *fin;

// 	varijable za izracun gregorijanskog datuma iz julijanskog dana
	int v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13;

	N=f.N;
	fin=fopen(f.fin_name, "r");

	for (i=0; i<N; i++) {

		j=0;
		while ((c=fgetc(fin)) != ';' ){
			comet[i].name[j++]=c;
		}

		fscanf(fin, "%d.%d %f %f %f %f %f %f %f %30[^\n]%*c",
				&comet[i].JD, &comet[i].h, &comet[i].pn, &comet[i].e,
				&comet[i].q, &comet[i].i, &comet[i].an, &comet[i].H,
				&comet[i].G, comet[i].x);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = comet[i].JD + 2400000;
		edit_name (comet[i].name);
/*
 * 		izracuvanje gregorijanskog datuma iz julijanskog dana
 * 		izvor: http://en.wikipedia.org/wiki/Julian_day#Gregorian_calendar_from_Julian_day_number
 */
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

void import_thesky(){

	int i, N;
	FILE *fin;

	N=f.N;
	fin=fopen(f.fin_name, "r");

	for (i=0; i<N; i++) {

		fscanf(fin, "%40c %*c %d %*c %4d %2d %2d %*c %d %*c %f %*c %f %*c %f %*c %f %*c %f %40[^\n]%*c",
				comet[i].name, &comet[i].eq, &comet[i].y, &comet[i].m,
				&comet[i].d, &comet[i].h, &comet[i].q, &comet[i].e,
				&comet[i].pn, &comet[i].an, &comet[i].i, &comet[i].x);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].name);
	}
}

void import_cfw(){

	int i, N;
	FILE *fin;

	N=f.N/13;
	fin=fopen(f.fin_name, "r");

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
							comet[i].x,
							&comet[i].y, &comet[i].m, &comet[i].d, &comet[i].h,
							&comet[i].q,
							&comet[i].e,
							&comet[i].pn,
							&comet[i].an,
							&comet[i].i,
							comet[i].x,
							&comet[i].H,
							comet[i].x);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].name);
	}
}

void import_nasa1(){

	int i, N;
	FILE *fin;

	N=f.N;
	fin=fopen(f.fin_name, "r");

	for (i=0; i<N; i++) {

		fscanf(fin, "%48c %f %f %f %f %f %4d%2d%2d.%4d %15[^\n]%*c",
				comet[i].name, &comet[i].q, &comet[i].e,
				&comet[i].i, &comet[i].pn, &comet[i].an, &comet[i].y,
				&comet[i].m, &comet[i].d, &comet[i].h, comet[i].x);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].name);
	}
}

void import_nasa2(){

	int i, j, t, N;
	char c;
	FILE *fin;

	N=f.N;
	fin=fopen(f.fin_name, "r");

	for (i=0; i<N; i++) {

		j=0;
		while ((c=fgetc(fin)) != ',' ){
			comet[i].name[j]=c;
			if (c=='"' && j>2) t=j;			// j>2 zato što se " pojavljuje i na prvom mjestu u stringu
			j++;
		}

		comet[i].name[0]=' ';
		comet[i].name[t+1]='\0';

		fscanf(fin, "%f,%f,%f,%f,%f,%4d%2d%2d.%4d%10[^\n]%*c",
				&comet[i].q, &comet[i].e, &comet[i].pn, &comet[i].an,
				&comet[i].i, &comet[i].y, &comet[i].m,
				&comet[i].d, &comet[i].h, comet[i].x);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].name);
	}
}

 /*
  * funkcije za racunanje...
  */
float compute_period (float q, float e){

	float P;

	if (e >= 0 && e < 1)
		P = pow((q/(1-e)),1.5);

	if (e > 1)
		P = pow((q/(e-1)),1.5);

	if (e == 1)
		P = pow((q/(1-0.999999)),1.5);

	return P;
}

int compute_JD (int y, int m, int d){

	int JD;

	JD = 367*y - (7*(y + (m + 9)/12))/4 -
		((3*(y + (m - 9)/7))/100 + 1)/4 +
		(275*m)/9 + d + 1721029;

	return JD;
}

char *edit_name (char *name){

	int i, j, k;

	if (name[0]==' '){          		// ovaj "if" je samo za "nasa_sbdb format" jer ima razmaka ispred imena kometa
		for (i=0; i<4; i++) {
			if (name[i]==' '){
				for (j=0; name[j+1]!='\0'; j++){
					name[j]=name[j+1];
				}
				--i;
			}
		}

		for (i=0; name[i+1]!='\0'; i++) {
			if ((name[i]==' ' && name[i+1]==' ') || (name[i]=='"' && name[i+1]=='"')) {
				name[i]='\0';
				break;
			}
			if (name[i]=='/')
				name[i]=' ';
		}
	}

	else {
		for (i=0; name[i+1]!='\0'; i++) {
			if (name[i]==' ' && name[i+1]==' ') {
				name[i]='\0';
				break;
			}
			if (name[i]=='/')
				name[i]=' ';
		}
	}
}

 /*
  * funkcije za pisanje u datoteke
  */
void output_ssc (){

	int i, N;
	FILE *fout;

	if (f.ty==17) N=f.N/13;
	else N=f.N;

	char *mon;

	for (i=0; i<N; i++) {

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

		fout=fopen(f.fout_name, "a");

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
		fprintf(fout,"\tEpoch \t\t\t %d.%.4d\t# %d %s %.2d.%.4d \n", comet[i].JD, comet[i].h, comet[i].y, mon, comet[i].d, comet[i].h);
		fprintf(fout,"\t} \n");
		fprintf(fout,"}\n\n\n");

		fclose(fout);
	}
}

void output_stell (){

	int i, N;
	FILE *fout;

	if (f.ty==17) N=f.N/13;
	else N=f.N;

	char *mon;

	for (i=0; i<N; i++) {

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

		fout=fopen(f.fout_name, "a");

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
		fprintf(fout,"type=comet\n");
		fprintf(fout,"orbit_TimeAtPericenter=%d.%.d\n\n", comet[i].JD, comet[i].h);

		fclose(fout);
	}
}
