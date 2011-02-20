#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <string.h>
#define MAX 5000

void  input_cfw ();
void  input_ciel ();
void  input_nasa ();
float compute_period (float q, float e);
int   compute_epoch (int y, int m, int d);
char *edit_name (char *name);
void  output (char *fout_, char *name, float P, float q, float e, float i,
			  float node, float peri, int eJD, int ey, int em, int ed, int eh);

int main () {
	int a;

	do {
		system("CLS");
		printf("\n          SSC MAKER\n");
		printf(" ===========================\n\n");
		printf("  - 1 -   Comet for Windows\n");
		printf("  - 2 -   Cartes du Ciel\n");
		printf("  - 3 -   NASA\n");
		printf("  - 4 -   Kraj programa\n\n");
		printf(" Izbor: ");
		scanf("%d", &a);
	} while (a<1 || a>4);

	if (a==1) input_cfw ();
	else if (a==2) input_ciel ();
	else if (a==3) input_nasa ();
	else if (a==4) printf("\n Kraj programa...");

	getch();
	return 0;
}

void input_cfw () {

	int i, N=0;
	char fin_name [80+1], fout_name [80+1];
	FILE *fin;

	struct data {
		char name [39+1];		// 39 znakova + \0
		char code [14+1];
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
		char book [14+1];
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
	printf("\n Kraj programa...");
}

void input_ciel () {

	int i, N=0;
	char fin_name [80+1], fout_name [80+1];
	FILE *fin;

	struct data {
		char name [39+1];		// 39 znakova + \0
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
		char nepotrebno [29+1];
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

		fscanf(fin, "%40c %*c %d %*c %4d %2d \
				%2d %*c %d %*c %f %*c %f %*c \
				%f %*c %f %*c %f %40[^\n]%*c",
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
	printf("\n Kraj programa...");
}

void  input_nasa (){

	int i, N=0;
	char fin_name [80+1], fout_name [80+1];
	FILE *fin;

	struct data {
		char name [39+1];		// 39 znakova + \0
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
		char nepotrebno [29+1];
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

		fscanf(fin, "%42c %d %f %f %f %f %f %4d%2d%2d.%4d %15[^\n]%*c",
			comet[i].name, &comet[i].equinox, &comet[i].peri, &comet[i].ecc,
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
	printf("\n Kraj programa...");
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

	int i, j;

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

void output (char *fout_, char *name, float P, float q, float e, float i,
			float node, float peri, int eJD, int ey, int em, int ed, int eh){

	FILE *fout;

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
\tEpoch \t\t\t %d.%d\t# %d %d %d.%d \n \
\t} \n \
} \n\n\n", name, P, q, e, i, node, peri, eJD, eh, ey, em, ed, eh);

	fclose(fout);
}
