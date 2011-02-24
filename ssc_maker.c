// Celestia and Stellarium Format Maker

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>
#define MAX 5000

void initscreen1();
void initscreen2();
void mainscreen();
void printscreen (char *format);

int import_mpc(char *format);
int import_skymap(char *format);
int import_guide (char *format);
int import_home_planet (char *format);
int import_mystars (char *format);
int import_thesky (char *format);

float compute_period (float q, float e);
int compute_JD (int y, int m, int d);
char *edit_name (char *name);
void output_ssc (char *fout_, int N);

struct data {
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

int main (){

	int a, b;
	char *format;

	initscreen2();
	getch();
	system("CLS");

	start: do {
		mainscreen();

		printf("  Import format: \n\n");
		printf("	0. MPC			10. Earth Centered Universe\n");
		printf("	1. SkyMap		11. Dance of the Planets\n");
		printf("	2. Guide		12. MegaStar V4.x\n");
		printf("	3. xephem -		13. SkyChart III\n");
		printf("	4. Home Planet		14. Voyager II\n");
		printf("	5. MyStars!		15. SkyTools\n");
		printf("	6. TheSky		16. Autostar\n");
		printf("	7. Starry Night		17. Comet for Windows\n");
		printf("	8. Deep Space		18. NASA (ELEMENTS.COMET)\n");
		printf("	9. PC-TCS		19. NASA (CSV format)\n\n");
		printf("  Choice: ");

		scanf("%d", &a);
	} while (a<0 || a>20);

	switch (a) {

		case 0: {
			format = "MPC";
			b=import_mpc(format);
			if (b==1) goto start;
			if (b==2) goto end;
		}

		case 1: {
			format = "SkyMap";
			b=import_skymap(format);
			if (b==1) goto start;
			if (b==2) goto end;
		}

		case 2: {
			format = "Guide";
			b=import_guide(format);
			if (b==1) goto start;
			if (b==2) goto end;
		}

		case 4: {
			format = "Home Planet";
			b=import_home_planet(format);
			if (b==1) goto start;
			if (b==2) goto end;
		}

		case 5: {
			format = "MyStars!";
			b=import_mystars(format);
			if (b==1) goto start;
			if (b==2) goto end;
		}

		case 6: {
			format = "TheSky!";
			b=import_thesky(format);
			if (b==1) goto start;
			if (b==2) goto end;
		}

	}

	end:
	initscreen2();
	printf("Press any key to exit...                                        (c) jurluk 2011");
	getch();
	return 0;
}

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

void mainscreen(){

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	printf("\n");
	printf("                     CELESTIA AND STELLARIUM FORMAT MAKER\n\n");
	printf(" =============================================================================\n\n");
}

void printscreen (char *format){

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	printf("\n");
	printf("  Importing \"%s\" format...\n\n", format);
	printf(" =============================================================================\n");
	printf("     1.   Main Menu   |   2.   Exit   \n");
	printf(" =============================================================================\n\n");
}

int import_mpc (char *format){

	int b, N=0, i=0;
	char c, fin_name[80+1], fout_name[80+1];
	FILE *fin;

	printscreen(format);

	unos:
	printf("  Enter input filename: ");
	scanf("%s", fin_name);

	b=atoi(fin_name);
	if (b==1 || b==2) return b;

	fin=fopen(fin_name, "r");
	if (fin==NULL) {
		printf("\n  Error opening file %s\n\n", fin_name);
		goto unos;
	}

	else printf("\n  File %s is successfully opened\n", fin_name);

	printf("\n  Enter output file name: ");
	scanf("%s", fout_name);

	b=atoi(fout_name);
	if (b==1 || b==2) return b;

	while ((c=fgetc(fin)) != EOF ){
		if (c=='\n') N++;
	}

	rewind(fin);

	for (i=0; i<N; i++) {

		fscanf(fin, "%14c %d %d %d.%d %f %f %f %f %f %d %f %f %70[^\n]%*c",
			comet[i].x, &comet[i].y, &comet[i].m, &comet[i].d, &comet[i].h,
			&comet[i].q, &comet[i].e, &comet[i].pn, &comet[i].an,
			&comet[i].i, &comet[i].eq, &comet[i].H, &comet[i].G, comet[i].name);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].name);

		if (comet[i].e >= 1.000000)
			comet[i].e = 0.999999;
	}

	fclose(fin);

	output_ssc (fout_name, N);

	do {
		printscreen(format);
		printf("  %d comets successfully saved in file %s\n\n", N, fout_name);
		printf("  Choice: ");
		scanf("%d", &b);
	} while (b!=1 && b!=2);

	return b;
}

int import_skymap (char *format){

	int b, N=0, i;
	char c, fin_name[80+1], fout_name[80+1];
	FILE *fin;

	printscreen(format);

	unos:
	printf("  Enter input filename: ");
	scanf("%s", fin_name);

	b=atoi(fin_name);
	if (b==1 || b==2) return b;

	fin=fopen(fin_name, "r");
	if (fin==NULL) {
		printf("\n  Error opening file %s\n\n", fin_name);
		goto unos;
	}

	else printf("\n  File %s is successfully opened\n", fin_name);

	printf("\n  Enter output file name: ");
	scanf("%s", fout_name);

	b=atoi(fout_name);
	if (b==1 || b==2) return b;

	while ((c=fgetc(fin)) != EOF ){
		if (c=='\n') N++;
	}

	rewind(fin);

	for (i=0; i<N; i++) {

		fscanf(fin, "%47c %4d %2d %2d.%4d %f %f %f %f %f %f %10[^\n]%*c",
			comet[i].name, &comet[i].y, &comet[i].m, &comet[i].d,
			&comet[i].h, &comet[i].q, &comet[i].e, &comet[i].pn,
			&comet[i].an, &comet[i].i, &comet[i].H, &comet[i].x);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].name);

		if (comet[i].e >= 1.000000)
			comet[i].e = 0.999999;
	}

	fclose(fin);

	output_ssc (fout_name, N);

	do {
		printscreen(format);
		printf("  %d comets successfully saved in file %s\n\n", N, fout_name);
		printf("  Choice: ");
		scanf("%d", &b);
	} while (b!=1 && b!=2);

	return b;
}

int import_guide (char *format){

	int b, N=0, i;
	char c, fin_name[80+1], fout_name[80+1];
	FILE *fin;

	printscreen(format);

	unos:
	printf("  Enter input filename: ");
	scanf("%s", fin_name);

	b=atoi(fin_name);
	if (b==1 || b==2) return b;

	fin=fopen(fin_name, "r");
	if (fin==NULL) {
		printf("\n  Error opening file %s\n\n", fin_name);
		goto unos;
	}

	else printf("\n  File %s is successfully opened\n", fin_name);

	printf("\n  Enter output file name: ");
	scanf("%s", fout_name);

	b=atoi(fout_name);
	if (b==1 || b==2) return b;

	while ((c=fgetc(fin)) != EOF ){
		if (c=='\n') N++;
	}

	rewind(fin);

	for (i=0; i<N; i++) {

		fscanf(fin, "%43c %d.%d %d %d %10c %f %f %f %f %f %10c %f %f %15[^\n]%*c",
			comet[i].name, &comet[i].d, &comet[i].h, &comet[i].m, &comet[i].y, comet[i].x,
			&comet[i].q, &comet[i].e, &comet[i].i, &comet[i].pn, &comet[i].an,
			comet[i].x, &comet[i].H, &comet[i].G, comet[i].x);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].name);

		if (comet[i].e >= 1.000000)
			comet[i].e = 0.999999;
	}

	fclose(fin);

	output_ssc (fout_name, N);

	do {
		printscreen(format);
		printf("  %d comets successfully saved in file %s\n\n", N, fout_name);
		printf("  Choice: ");
		scanf("%d", &b);
	} while (b!=1 && b!=2);

	return b;
}

int import_home_planet (char *format){

	int b, N=0, i, j;
	char c, fin_name[80+1], fout_name[80+1], line[115+1];
	FILE *fin;

	printscreen(format);

	unos:
	printf("  Enter input filename: ");
	scanf("%s", fin_name);

	b=atoi(fin_name);
	if (b==1 || b==2) return b;

	fin=fopen(fin_name, "r");
	if (fin==NULL) {
		printf("\n  Error opening file %s\n\n", fin_name);
		goto unos;
	}

	else printf("\n  File %s is successfully opened\n", fin_name);

	printf("\n  Enter output file name: ");
	scanf("%s", fout_name);

	b=atoi(fout_name);
	if (b==1 || b==2) return b;

	while ((c=fgetc(fin)) != EOF ){
		if (c=='\n') N++;
	}

	N = N-1;

	rewind(fin);

	fscanf(fin, "%115[^\n]%*c", line);

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

		if (comet[i].e >= 1.000000)
			comet[i].e = 0.999999;
	}

	fclose(fin);

	output_ssc (fout_name, N);

	do {
		printscreen(format);
		printf("  %d comets successfully saved in file %s\n\n", N, fout_name);
		printf("  Choice: ");
		scanf("%d", &b);
	} while (b!=1 && b!=2);

	return b;
}

int import_mystars (char *format){

	int b, N=0, i, j;
	char c, fin_name[80+1], fout_name[80+1], line[20+1];
	FILE *fin;

	// varijable za izracun gregorijanskog datuma iz julijanskog dana
	int v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13;

	printscreen(format);

	unos:
	printf("  Enter input filename: ");
	scanf("%s", fin_name);

	b=atoi(fin_name);
	if (b==1 || b==2) return b;

	fin=fopen(fin_name, "r");
	if (fin==NULL) {
		printf("\n  Error opening file %s\n\n", fin_name);
		goto unos;
	}

	else printf("\n  File %s is successfully opened\n", fin_name);

	printf("\n  Enter output file name: ");
	scanf("%s", fout_name);

	b=atoi(fout_name);
	if (b==1 || b==2) return b;

	while ((c=fgetc(fin)) != EOF ){
		if (c=='\n') N++;
	}

	N = N-1;

	rewind(fin);

	fscanf(fin, "%20[^\n]%*c", line);

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

		if (comet[i].e >= 1.000000)
			comet[i].e = 0.999999;
	}

	fclose(fin);

	output_ssc (fout_name, N);

	do {
		printscreen(format);
		printf("  %d comets successfully saved in file %s\n\n", N, fout_name);
		printf("  Choice: ");
		scanf("%d", &b);
	} while (b!=1 && b!=2);

	return b;
}

int import_thesky (char *format){

	int b, N=0, i=0;
	char c, fin_name[80+1], fout_name[80+1];
	FILE *fin;

	printscreen(format);

	unos:
	printf("  Enter input filename: ");
	scanf("%s", fin_name);

	b=atoi(fin_name);
	if (b==1 || b==2) return b;

	fin=fopen(fin_name, "r");
	if (fin==NULL) {
		printf("\n  Error opening file %s\n\n", fin_name);
		goto unos;
	}

	else printf("\n  File %s is successfully opened\n", fin_name);

	printf("\n  Enter output file name: ");
	scanf("%s", fout_name);

	b=atoi(fout_name);
	if (b==1 || b==2) return b;

	while ((c=fgetc(fin)) != EOF ){
		if (c=='\n') N++;
	}

	rewind(fin);

	for (i=0; i<N; i++) {

		fscanf(fin, "%40c %*c %d %*c %4d %2d %2d %*c %d %*c %f %*c %f %*c %f %*c %f %*c %f %40[^\n]%*c",
				comet[i].name, &comet[i].eq, &comet[i].y, &comet[i].m,
				&comet[i].d, &comet[i].h, &comet[i].q, &comet[i].e,
				&comet[i].pn, &comet[i].an, &comet[i].i, &comet[i].x);

		comet[i].P = compute_period (comet[i].q, comet[i].e);
		comet[i].JD = compute_JD (comet[i].y, comet[i].m, comet[i].d);
		edit_name (comet[i].name);

		if (comet[i].e >= 1.000000)
			comet[i].e = 0.999999;
	}

	fclose(fin);

	output_ssc (fout_name, N);

	do {
		printscreen(format);
		printf("  %d comets successfully saved in file %s\n\n", N, fout_name);
		printf("  Choice: ");
		scanf("%d", &b);
	} while (b!=1 && b!=2);

	return b;
}

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

void output_ssc (char *fout_, int N){

	int i;
	FILE *fout;

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

		fout=fopen(fout_, "a");

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
