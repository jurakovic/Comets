#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <string.h>
#define MAX 1000

float izracunaj_period (float q, float e);
int izracunaj_epoch (int y, int m, int d);
char* uredi_ime (char *name);

int main () {

	int i, N;
	char izvorisna [80+1], odredisna [80+1];
	FILE *f1, *f2;

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

	unos: printf("Unesite ime izvorisne datoteke: ");
	scanf("%s", izvorisna);

	f1=fopen(izvorisna, "r");
	if (f1==NULL) {
		printf("\nGreska pri otvaranju datoteke %s\n\n", izvorisna);
		goto unos;
	}

	else
		printf("\nDatoteka %s uspjesno otvorena\n", izvorisna);

	printf("\nUnesite ime odredisne datoteke: ");
	scanf("%s", odredisna);

	printf("\nKoliko kometa zelite obraditi: ");
	scanf("%d", &N);

	f2=fopen(odredisna, "a");

	for (i=1; i<=N; i++) {

		fscanf(f1, "name=%40[^\n]%*c\
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

		comet[i].period = izracunaj_period (comet[i].peri, comet[i].ecc);
		comet[i].epoch_JD = izracunaj_epoch (comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d);
		uredi_ime (comet[i].name);

		if (comet[i].ecc >= 1.000000)
			comet[i].ecc = 0.999999;

		fprintf(f2,"\"%s\" \"Sol\"\n \
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
} \n\n\n", 	comet[i].name,
		comet[i].period,
		comet[i].peri,
		comet[i].ecc,
		comet[i].incl,
		comet[i].asc_node,
		comet[i].peri_node,
		comet[i].epoch_JD, comet[i].epoch_h,
		comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d, comet[i].epoch_h);
	}

	fclose(f1);
	fclose(f2);

	printf("\nDatoteka %s je uspjesno kreirana\n", odredisna);
	getch ();
	return 0;
}

float izracunaj_period (float q, float e) {

	float period;

	if (e >= 0 && e < 1)
		period = pow((q/(1-e)),1.5);

	if (e > 1)
		period = pow((q/(e-1)),1.5);

	if (e == 1)
		period = pow((q/(1-0.999999)),1.5);

	return period;
}

int izracunaj_epoch (int y, int m, int d) {

	int JD;

	JD = 367*y - (7*(y + (m + 9)/12))/4 -
		((3*(y + (m - 9)/7))/100 + 1)/4 +
		(275*m)/9 + d + 1721029;

	return JD;
}

char* uredi_ime (char *name) {

	int i;

	for (i=0; name[i+1]!='\0'; i++) {
		if (name[i]==' ' && name[i+1]==' ') {
			name[i]='\0';
			break;
		}

		if (name[i]=='/')
			name[i]=' ';
	}
}
