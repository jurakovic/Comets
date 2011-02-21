#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <string.h>
#define MAX 5000

void  input_mpc ();
void  input_skymap ();
void  input_cfw ();
void  input_nasa ();
void  input_ciel ();
float compute_period (float q, float e);
int   compute_epoch (int y, int m, int d);
char *edit_name (char *name);
void  output (char *fout_, char *name, float P, float q, float e, float i,
			  float node, float peri, int eJD, int ey, int em, int ed, int eh);

int main (){
	int a;

	pocetak: do {
		fflush(stdin);
		fflush(stdout);
		system("CLS");
		printf("\n          SSC MAKER\n");
		printf(" ===========================\n\n");
		printf("  Odaberite ulazni format:\n\n");
		printf("    1.  MPC\n");
		printf("    2.	SkyMap\n");
		printf("    3.  Comet for Windows\n");
		printf("    4.  NASA\n");
		printf("    5.  Cartes du Ciel/TheSky\n");
		printf("    6.  Izlaz\n\n");
		printf("  Izbor: ");
		scanf("%d", &a);
	} while (a<1 || a>6);

	if (a==1){
		input_mpc ();
		goto pocetak;
	}
	else if (a==2){
		input_skymap ();
		goto pocetak;
	}
	else if (a==3){
		input_cfw ();
		goto pocetak;
	}
	else if (a==4){
		input_nasa ();
		goto pocetak;
	}
	else if (a==5){
		input_ciel ();
		goto pocetak;
	}
	else if (a==6)
		printf("\n Kraj programa...");

	getch();
	return 0;
}

void input_mpc (){

	int i, N=0;
	char fin_name [80+1], fout_name [80+1];
	FILE *fin;

	struct data {
		char name [70+1];
		int equinox;
		int epoch_JD;
		int epoch_y;
		int epoch_m;
		int epoch_d;
		int epoch_h;
		float peri;
		float ecc;
		float peri_node;
		float asc_node;
		float incl;
		float G;
		float H;
		float period;
		char nepotrebno [14+1];
	} comet[MAX];

	system("CLS");
	printf("\n     Pretvaram \"MPC format\"\n");
	printf(" ==============================\n\n");

	unos2: printf(" Unesite ime izvorisne datoteke: ");
	scanf("%s", fin_name);

	fin=fopen(fin_name, "r");
	if (fin==NULL) {
		printf("\n Greska pri otvaranju datoteke %s\n\n", fin_name);
		goto unos2;
	}

	else printf("\n Datoteka %s je uspjesno otvorena\n", fin_name);

	printf("\n Unesite ime odredisne datoteke: ");
	scanf("%s", fout_name);

	for (i=0; i<MAX; i++) {

		fscanf(fin, "%14c %d %d %d.%d %f %f %f %f %f %d %f %f %70[^\n]%*c",
			comet[i].nepotrebno, &comet[i].epoch_y, &comet[i].epoch_m, &comet[i].epoch_d, &comet[i].epoch_h,
			&comet[i].peri, &comet[i].ecc, &comet[i].peri_node, &comet[i].asc_node,
			&comet[i].incl, &comet[i].equinox, &comet[i].G, &comet[i].H, comet[i].name);

		comet[i].period = compute_period (comet[i].peri, comet[i].ecc);
		comet[i].epoch_JD = compute_epoch (comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d);
		edit_name (comet[i].name);

		if (comet[i].ecc >= 1.000000)
			comet[i].ecc = 0.999999;

		if (comet[i].epoch_m == 0 && comet[i].epoch_d == 0 && comet[i].epoch_h == 0)
			break;

		N++;
	}

	fclose(fin);

	for (i=0; i<N; i++)
		output (fout_name, comet[i].name, comet[i].period, comet[i].peri, comet[i].ecc,
			   comet[i].incl, comet[i].asc_node, comet[i].peri_node, comet[i].epoch_JD,
			   comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d, comet[i].epoch_h);

	printf("\n Uspjesno je spremljeno %d kometa u datoteci %s\n", N, fout_name);
	printf("\n Vracam se na glavni izbornik...");
	getch();
}

void input_skymap (){

	int i, N=0;
	char fin_name [80+1], fout_name [80+1];
	FILE *fin;

	struct data {
		char name [48+1];
		int epoch_JD;
		int epoch_y;
		int epoch_m;
		int epoch_d;
		int epoch_h;
		float peri;
		float ecc;
		float peri_node;
		float asc_node;
		float incl;
		float period;
		char nepotrebno [14+1];
	} comet[MAX];

	system("CLS");
	printf("\n     Pretvaram \"SkyMap format\"\n");
	printf(" ==============================\n\n");

	unos2: printf(" Unesite ime izvorisne datoteke: ");
	scanf("%s", fin_name);

	fin=fopen(fin_name, "r");
	if (fin==NULL) {
		printf("\n Greska pri otvaranju datoteke %s\n\n", fin_name);
		goto unos2;
	}

	else printf("\n Datoteka %s je uspjesno otvorena\n", fin_name);

	printf("\n Unesite ime odredisne datoteke: ");
	scanf("%s", fout_name);

	for (i=0; i<MAX; i++) {

		fscanf(fin, "%47c %4d %2d %2d.%4d %f %f %f %f %f %15[^\n]%*c",
			comet[i].name, &comet[i].epoch_y, &comet[i].epoch_m, &comet[i].epoch_d,
			&comet[i].epoch_h, &comet[i].peri, &comet[i].ecc, &comet[i].peri_node,
			&comet[i].asc_node, &comet[i].incl, comet[i].nepotrebno);

		comet[i].period = compute_period (comet[i].peri, comet[i].ecc);
		comet[i].epoch_JD = compute_epoch (comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d);
		edit_name(comet[i].name);

		if (comet[i].ecc >= 1.000000)
			comet[i].ecc = 0.999999;

		if (comet[i].epoch_m == 0 && comet[i].epoch_d == 0 && comet[i].epoch_h == 0)
			break;

		N++;
	}

	fclose(fin);

	for (i=0; i<N; i++)
		output (fout_name, comet[i].name, comet[i].period, comet[i].peri, comet[i].ecc,
			   comet[i].incl, comet[i].asc_node, comet[i].peri_node, comet[i].epoch_JD,
			   comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d, comet[i].epoch_h);

	printf("\n Uspjesno je spremljeno %d kometa u datoteci %s\n", N, fout_name);
	printf("\n Vracam se na glavni izbornik...");
	getch();
}

void input_cfw (){

	int i, N=0;
	char fin_name [80+1], fout_name [80+1];
	FILE *fin;

	struct data {
		char name [40+1];
		char code [15+1];
		int epoch_y;
		int epoch_m;
		int epoch_d;
		int epoch_h;
		int epoch_JD;
		float peri;
		float ecc;
		float peri_node;
		float asc_node;
		float incl;
		char book [15+1];
		float period;
		float mag1;
		float mag2;
	} comet[MAX];

	system("CLS");
	printf("\n     Pretvaram \"cfw format\"\n");
	printf(" ==============================\n\n");

	unos1: printf(" Unesite ime izvorisne datoteke: ");
	scanf("%s", fin_name);

	fin=fopen(fin_name, "r");
	if (fin==NULL) {
		printf("\n Greska pri otvaranju datoteke %s\n\n", fin_name);
		goto unos1;
	}

	else printf("\n Datoteka %s je uspjesno otvorena\n", fin_name);

	printf("\n Unesite ime odredisne datoteke: ");
	scanf("%s", fout_name);

	for (i=0; i<MAX; i++) {
		fscanf(fin, "name=%40[^\n]%*c\
					code=%15[^\n]%*c\
					type=orbit\n\
					T=%d %d %d.%d\n\
					q=%f\n\
					e=%f\n\
					peri=%f\n\
					node=%f\n\
					i=%f\n\
					prec=2000.0\n\
					book=%15[^\n]%*c\
					mageq=%f %f\n\
					\n",
				comet[i].name,
				comet[i].code,
				&comet[i].epoch_y,
				&comet[i].epoch_m,
				&comet[i].epoch_d,
				&comet[i].epoch_h,
				&comet[i].peri,
				&comet[i].ecc,
				&comet[i].peri_node,
				&comet[i].asc_node,
				&comet[i].incl,
				comet[i].book,
				&comet[i].mag1,
				&comet[i].mag2);

		comet[i].period = compute_period (comet[i].peri, comet[i].ecc);
		comet[i].epoch_JD = compute_epoch (comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d);
		edit_name (comet[i].name);

		if (comet[i].ecc >= 1.000000)
			comet[i].ecc = 0.999999;

		if (comet[i].epoch_m == 0 && comet[i].epoch_d == 0 && comet[i].epoch_h == 0)
			break;

		N++;
	}

	for (i=0; i<N; i++)
		output (fout_name, comet[i].name, comet[i].period, comet[i].peri, comet[i].ecc,
			   comet[i].incl, comet[i].asc_node, comet[i].peri_node, comet[i].epoch_JD,
			   comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d, comet[i].epoch_h);

	fclose(fin);

	printf("\n Uspjesno je spremljeno %d kometa u datoteci %s\n", N, fout_name);
	printf("\n Vracam se na glavni izbornik...");
	getch();
}

void input_nasa (){

	int i, N=0;
	char fin_name [80+1], fout_name [80+1];
	FILE *fin;

	struct data {
		char name [48+1];
		int equinox;
		int epoch_JD;
		int epoch_y;
		int epoch_m;
		int epoch_d;
		int epoch_h;
		float peri;
		float ecc;
		float peri_node;
		float asc_node;
		float incl;
		float period;
		char nepotrebno [15+1];
	} comet[MAX];

	system("CLS");
	printf("\n     Pretvaram \"NASA format\"\n");
	printf(" ==============================\n\n");

	unos2: printf(" Unesite ime izvorisne datoteke: ");
	scanf("%s", fin_name);

	fin=fopen(fin_name, "r");
	if (fin==NULL) {
		printf("\n Greska pri otvaranju datoteke %s\n\n", fin_name);
		goto unos2;
	}

	else printf("\n Datoteka %s je uspjesno otvorena\n", fin_name);

	printf("\n Unesite ime odredisne datoteke: ");
	scanf("%s", fout_name);

	for (i=0; i<MAX; i++) {

		fscanf(fin, "%48c %f %f %f %f %f %4d%2d%2d.%4d %15[^\n]%*c",
			comet[i].name, &comet[i].peri, &comet[i].ecc,
			&comet[i].incl, &comet[i].peri_node, &comet[i].asc_node, &comet[i].epoch_y,
			&comet[i].epoch_m, &comet[i].epoch_d, &comet[i].epoch_h, &comet[i].nepotrebno);

		comet[i].period = compute_period (comet[i].peri, comet[i].ecc);
		comet[i].epoch_JD = compute_epoch (comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d);
		edit_name (comet[i].name);

		if (comet[i].ecc >= 1.000000)
			comet[i].ecc = 0.999999;

		if (comet[i].epoch_m == 0 && comet[i].epoch_d == 0 && comet[i].epoch_h == 0)
			break;

		N++;
	}

	fclose(fin);

	for (i=0; i<N; i++)
		output (fout_name, comet[i].name, comet[i].period, comet[i].peri, comet[i].ecc,
			   comet[i].incl, comet[i].asc_node, comet[i].peri_node, comet[i].epoch_JD,
			   comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d, comet[i].epoch_h);

	printf("\n Uspjesno je spremljeno %d kometa u datoteci %s\n", N, fout_name);
	printf("\n Vracam se na glavni izbornik...");
	getch();
}

void input_ciel (){

	int i, N=0;
	char fin_name [80+1], fout_name [80+1];
	FILE *fin;

	struct data {
		char name [40+1];
		int equinox;
		int epoch_JD;
		int epoch_y;
		int epoch_m;
		int epoch_d;
		int epoch_h;
		float peri;
		float ecc;
		float peri_node;
		float asc_node;
		float incl;
		float period;
		char nepotrebno [40+1];
	} comet[MAX];

	system("CLS");
	printf("\n     Pretvaram \"ciel format\"\n");
	printf(" ==============================\n\n");

	unos2: printf(" Unesite ime izvorisne datoteke: ");
	scanf("%s", fin_name);

	fin=fopen(fin_name, "r");
	if (fin==NULL) {
		printf("\n Greska pri otvaranju datoteke %s\n\n", fin_name);
		goto unos2;
	}

	else printf("\n Datoteka %s je uspjesno otvorena\n", fin_name);

	printf("\n Unesite ime odredisne datoteke: ");
	scanf("%s", fout_name);

	for (i=0; i<MAX; i++) {

		fscanf(fin, "%40c %*c %d %*c %4d %2d %2d %*c %d %*c %f %*c %f %*c %f %*c %f %*c %f %40[^\n]%*c",
				comet[i].name, &comet[i].equinox, &comet[i].epoch_y, &comet[i].epoch_m,
				&comet[i].epoch_d, &comet[i].epoch_h, &comet[i].peri, &comet[i].ecc,
				&comet[i].peri_node, &comet[i].asc_node, &comet[i].incl, &comet[i].nepotrebno);

		comet[i].period = compute_period (comet[i].peri, comet[i].ecc);
		comet[i].epoch_JD = compute_epoch (comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d);
		edit_name (comet[i].name);

		if (comet[i].ecc >= 1.000000)
			comet[i].ecc = 0.999999;

		if (comet[i].epoch_m == 0 && comet[i].epoch_d == 0 && comet[i].epoch_h == 0)
			break;

		N++;
	}

	fclose(fin);

	for (i=0; i<N; i++)
		output (fout_name, comet[i].name, comet[i].period, comet[i].peri, comet[i].ecc,
			   comet[i].incl, comet[i].asc_node, comet[i].peri_node, comet[i].epoch_JD,
			   comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d, comet[i].epoch_h);

	printf("\n Uspjesno je spremljeno %d kometa u datoteci %s\n", N, fout_name);
	printf("\n Vracam se na glavni izbornik...");
	getch();
}

float compute_period (float q, float e) {

	float period;

	if (e >= 0 && e < 1)
		period = pow((q/(1-e)),1.5);

	if (e > 1)
		period = pow((q/(e-1)),1.5);

	if (e == 1)
		period = pow((q/(1-0.999999)),1.5);

	return period;
}

int compute_epoch (int y, int m, int d) {

	int JD;

	JD = 367*y - (7*(y + (m + 9)/12))/4 -
		((3*(y + (m - 9)/7))/100 + 1)/4 +
		(275*m)/9 + d + 1721029;

	return JD;
}

char *edit_name (char *name) {

	int i, j, k;

	if (name[0]==' '){
		for (i=0; i<4; i++) {
			if (name[i]==' '){
				for (j=0; name[j+1]!='\0'; j++){
					name[j]=name[j+1];
				}
				--i;
			}
		}

		for (i=0; name[i+1]!='\0'; i++) {
			if (name[i]==' ' && name[i+1]==' ') {
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

void output (char *fout_, char *name, float P, float q, float e, float i,
			float node, float peri, int eJD, int ey, int em, int ed, int eh){

	FILE *fout;

	char *mon;
	if (em==1) mon="Jan";
	if (em==2) mon="Feb";
	if (em==3) mon="Mar";
	if (em==4) mon="Apr";
	if (em==5) mon="May";
	if (em==6) mon="Jun";
	if (em==7) mon="Jul";
	if (em==8) mon="Aug";
	if (em==9) mon="Sep";
	if (em==10) mon="Oct";
	if (em==11) mon="Nov";
	if (em==12) mon="Dec";

	fout=fopen(fout_, "a");

	fprintf(fout,"\"%s\" \"Sol\"\n \
{\n \
Class \"comet\" \n \
Mesh \"asteroid.cms\" \n \
Texture \"asteroid.jpg\" \n \
Radius 5 \n \
Albedo 0.1 \n \
EllipticalOrbit \n \
\t{ \n \
\tPeriod \t\t\t %f \n \
\tPericenterDistance \t %f \n \
\tEccentricity \t\t %f \n \
\tInclination \t\t %.4f \n \
\tAscendingNode \t\t %.4f \n \
\tArgOfPericenter \t %.4f \n \
\tMeanAnomaly \t\t 0  \n \
\tEpoch \t\t\t %d.%.4d\t# %d %s %.2d.%.4d \n \
\t} \n \
} \n\n\n", name, P, q, e, i, node, peri, eJD, eh, ey, mon, ed, eh);

	fclose(fout);
}
