#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>
#define MAX 5000

int input_mpc ();
int input_skymap ();
int input_cfw ();
int input_nasa_sbdb ();
int input_nasa_elem ();
int input_ciel ();
float compute_period (float q, float e);
int compute_epoch (int y, int m, int d);
char *edit_name (char *name);
void output (char *fout_, char *name, float P, float q, float e, float i,
			  float node, float peri, int eJD, int ey, int em, int ed, int eh);

int main (){
	int a, b;

	start: do {
		fflush(stdin);
		fflush(stdout);
		system("CLS");
		printf("\n                                 SSC MAKER\n\n");
		printf(" =============================================================================\n\n");
		printf("  Choose input format:\n\n");
		printf("    1.  MPC                       (Soft00Cmt)\n");
		printf("    2.	SkyMap                    (Soft01Cmt)\n");
		printf("    3.  TheSky/Cartes du Ciel     (Soft06Cmt)\n");
		printf("    4.  Comet for Windows         (Comets.dat)\n");
		printf("    5.  NASA                      (ELEMENTS.COMET)\n");
		printf("    6.  NASA                      (sbdb query)\n");
		printf("    7.  Exit\n\n");
		printf("  Choice: ");
		scanf("%d", &a);
	} while (a<1 || a>7);

	if (a==1){
		b=input_mpc ();
		if (b==1) goto start;
		if (b==2) goto end;
	}
	else if (a==2){
		b=input_skymap ();
		if (b==1) goto start;
		if (b==2) goto end;
	}
	else if (a==3){
		b=input_ciel ();
		if (b==1) goto start;
		if (b==2) goto end;
	}
	else if (a==4){
		b=input_cfw ();
		if (b==1) goto start;
		if (b==2) goto end;
	}
	else if (a==5){
		b=input_nasa_elem ();
		if (b==1) goto start;
		if (b==2) goto end;
	}
	else if (a==6){
		b=input_nasa_sbdb ();
		if (b==1) goto start;
		if (b==2) goto end;
	}
	else if (a==7)
		end: printf("\n  Press any key to exit...");

	getch();
	return 0;
}

int input_mpc (){

	int b, i=0, N=0;
	char c, fin_name [80+1], fout_name [80+1];
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

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	printf("\n     Converting \"MPC format\"\n\n");
	printf(" =============================================================================\n\n");
	printf("    1.   Main Menu\n");
	printf("    2.   Exit\n\n");
	printf(" =============================================================================\n\n");

	unos2: printf("  Enter input file name: ");
	scanf("%s", fin_name);

	b=atoi(fin_name);
	if (b==1 || b==2) goto end;

	fin=fopen(fin_name, "r");
	if (fin==NULL) {
		printf("\n  Error opening file %s\n\n", fin_name);
		goto unos2;
	}

	else printf("\n  File %s is successfully opened\n", fin_name);

	printf("\n  Enter output file name: ");
	scanf("%s", fout_name);

	b=atoi(fout_name);
	if (b==1 || b==2) goto end;

	while ((c=fgetc(fin)) != EOF ){
		if (c=='\n') N++;
	}

	rewind(fin);

	for (i=0; i<N; i++) {

		fscanf(fin, "%14c %d %d %d.%d %f %f %f %f %f %d %f %f %70[^\n]%*c",
			comet[i].nepotrebno, &comet[i].epoch_y, &comet[i].epoch_m, &comet[i].epoch_d, &comet[i].epoch_h,
			&comet[i].peri, &comet[i].ecc, &comet[i].peri_node, &comet[i].asc_node,
			&comet[i].incl, &comet[i].equinox, &comet[i].G, &comet[i].H, comet[i].name);

		comet[i].period = compute_period (comet[i].peri, comet[i].ecc);
		comet[i].epoch_JD = compute_epoch (comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d);
		edit_name (comet[i].name);

		if (comet[i].ecc >= 1.000000)
			comet[i].ecc = 0.999999;
	}

	fclose(fin);

	for (i=0; i<N; i++)
		output (fout_name, comet[i].name, comet[i].period, comet[i].peri, comet[i].ecc,
			   comet[i].incl, comet[i].asc_node, comet[i].peri_node, comet[i].epoch_JD,
			   comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d, comet[i].epoch_h);

	do {
		system("CLS");
		printf("\n     Converting \"MPC format\"\n\n");
		printf(" =============================================================================\n\n");
		printf("    1.   Main Menu\n");
		printf("    2.	 Exit\n\n");
		printf(" =============================================================================\n\n");
		printf("  %d comets successfully saved in file %s\n\n", N, fout_name);
		printf("  Choice: ");
		scanf("%d", &b);
	} while (b!=1 && b!=2);

	fflush(fin);
	fflush(stdin);
	end: return b;
}

int input_skymap (){

	int b, i, N=0;
	char c, fin_name [80+1], fout_name [80+1];
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

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	printf("\n     Converting \"SkyMap format\"\n\n");
	printf(" =============================================================================\n\n");
	printf("    1.   Main Menu\n");
	printf("    2.   Exit\n\n");
	printf(" =============================================================================\n\n");

	unos2: printf("  Enter input file name: ");
	scanf("%s", fin_name);

	b=atoi(fin_name);
	if (b==1 || b==2) goto end;

	fin=fopen(fin_name, "r");
	if (fin==NULL) {
		printf("\n  Error opening file %s\n\n", fin_name);
		goto unos2;
	}

	else printf("\n  File %s is successfully opened\n", fin_name);

	printf("\n  Enter output file name: ");
	scanf("%s", fout_name);

	b=atoi(fout_name);
	if (b==1 || b==2) goto end;


	while ((c=fgetc(fin)) != EOF ){
		if (c=='\n') N++;
	}

	rewind(fin);

	for (i=0; i<N; i++) {

		fscanf(fin, "%47c %4d %2d %2d.%4d %f %f %f %f %f %15[^\n]%*c",
			comet[i].name, &comet[i].epoch_y, &comet[i].epoch_m, &comet[i].epoch_d,
			&comet[i].epoch_h, &comet[i].peri, &comet[i].ecc, &comet[i].peri_node,
			&comet[i].asc_node, &comet[i].incl, comet[i].nepotrebno);

		comet[i].period = compute_period (comet[i].peri, comet[i].ecc);
		comet[i].epoch_JD = compute_epoch (comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d);
		edit_name(comet[i].name);

		if (comet[i].ecc >= 1.000000)
			comet[i].ecc = 0.999999;
	}

	fclose(fin);

	for (i=0; i<N; i++)
		output (fout_name, comet[i].name, comet[i].period, comet[i].peri, comet[i].ecc,
			   comet[i].incl, comet[i].asc_node, comet[i].peri_node, comet[i].epoch_JD,
			   comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d, comet[i].epoch_h);

	do {
		system("CLS");
		printf("\n     Converting \"SkyMap format\"\n\n");
		printf(" =============================================================================\n\n");
		printf("    1.   Main Menu\n");
		printf("    2.	 Exit\n\n");
		printf(" =============================================================================\n\n");
		printf("  %d comets successfully saved in file %s\n\n", N, fout_name);
		printf("  Choice: ");
		scanf("%d", &b);
	} while (b!=1 && b!=2);

	fflush(fin);
	fflush(stdin);
	end: return b;
}

int input_cfw (){

	int b, i, N=0;
	char c, fin_name [80+1], fout_name [80+1];
	FILE *fin;

	struct data {
		char name [40+1];
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
		char nepotrebno [20+1];
		float period;
	} comet[MAX];

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	printf("\n     Converting \"cfw format\"\n\n");
	printf(" =============================================================================\n\n");
	printf("    1.   Main Menu\n");
	printf("    2.   Exit\n\n");
	printf(" =============================================================================\n\n");

	unos1: printf("  Enter input file name: ");
	scanf("%s", fin_name);

	b=atoi(fin_name);
	if (b==1 || b==2) goto end;

	fin=fopen(fin_name, "r");
	if (fin==NULL) {
		printf("\n  Error opening file %s\n\n", fin_name);
		goto unos1;
	}

	else printf("\n  File %s is successfully opened\n", fin_name);

	printf("\n  Enter output file name: ");
	scanf("%s", fout_name);

	b=atoi(fout_name);
	if (b==1 || b==2) goto end;

	while ((c=fgetc(fin)) != EOF ){
		if (c=='\n') N++;
	}

	N = N/13;    	// jer su podaci za 1 komet zapisani u 13 redaka

	rewind(fin);

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
					%20[^\n]%*c\
					\n",
				comet[i].name,
				comet[i].nepotrebno,
				&comet[i].epoch_y,
				&comet[i].epoch_m,
				&comet[i].epoch_d,
				&comet[i].epoch_h,
				&comet[i].peri,
				&comet[i].ecc,
				&comet[i].peri_node,
				&comet[i].asc_node,
				&comet[i].incl,
				comet[i].nepotrebno,
				comet[i].nepotrebno);

		comet[i].period = compute_period (comet[i].peri, comet[i].ecc);
		comet[i].epoch_JD = compute_epoch (comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d);
		edit_name (comet[i].name);

		if (comet[i].ecc >= 1.000000)
			comet[i].ecc = 0.999999;
	}

	for (i=0; i<N; i++)
		output (fout_name, comet[i].name, comet[i].period, comet[i].peri, comet[i].ecc,
			   comet[i].incl, comet[i].asc_node, comet[i].peri_node, comet[i].epoch_JD,
			   comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d, comet[i].epoch_h);

	fclose(fin);

	do {
		system("CLS");
		printf("\n     Converting \"cfw format\"\n\n");
		printf(" =============================================================================\n\n");
		printf("    1.   Main Menu\n");
		printf("    2.	 Exit\n\n");
		printf(" =============================================================================\n\n");
		printf("  %d comets successfully saved in file %s\n\n", N, fout_name);
		printf("  Choice: ");
		scanf("%d", &b);
	} while (b!=1 && b!=2);

	fflush(fin);
	fflush(stdin);
	end: return b;
}

int input_nasa_elem (){

	int b, i, j, N=0;
	char c, fin_name [80+1], fout_name [80+1];
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
		char nepotrebno [15+1];
	} comet[MAX];

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	printf("\n     Converting \"NASA format\"\n\n");
	printf(" =============================================================================\n\n");
	printf("    1.   Main Menu\n");
	printf("    2.   Exit\n\n");
	printf(" =============================================================================\n\n");

	unos2: printf("  Enter input file name: ");
	scanf("%s", fin_name);

	b=atoi(fin_name);
	if (b==1 || b==2) goto end;

	fin=fopen(fin_name, "r");
	if (fin==NULL) {
		printf("\n  Error opening file %s\n\n", fin_name);
		goto unos2;
	}

	else printf("\n  File %s is successfully opened\n", fin_name);

	printf("\n  Enter output file name: ");
	scanf("%s", fout_name);

	b=atoi(fout_name);
	if (b==1 || b==2) goto end;

	while ((c=fgetc(fin)) != EOF ){
		if (c=='\n') N++;
	}

	rewind(fin);

	for (i=0; i<N; i++) {

		fscanf(fin, "%48c %f %f %f %f %f %4d%2d%2d.%4d %15[^\n]%*c",
			comet[i].name, &comet[i].peri, &comet[i].ecc,
			&comet[i].incl, &comet[i].peri_node, &comet[i].asc_node, &comet[i].epoch_y,
			&comet[i].epoch_m, &comet[i].epoch_d, &comet[i].epoch_h, &comet[i].nepotrebno);

		comet[i].period = compute_period (comet[i].peri, comet[i].ecc);
		comet[i].epoch_JD = compute_epoch (comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d);
		edit_name (comet[i].name);

		if (comet[i].ecc >= 1.000000)
			comet[i].ecc = 0.999999;

/*		// za uklanjanje kometa odredjenih karakteristika

		for (j=0; j<strlen(comet[i].name); j++){
			if ((comet[i].name[j]  =='S' && comet[i].name[j+1]=='O' &&
				 comet[i].name[j+2]=='H' && comet[i].name[j+3]=='O')
				|| (comet[i].period > 300)
				){
				--i;
				--N;
			}
		}
*/
	}

	fclose(fin);

	for (i=0; i<N; i++)
		output (fout_name, comet[i].name, comet[i].period, comet[i].peri, comet[i].ecc,
			   comet[i].incl, comet[i].asc_node, comet[i].peri_node, comet[i].epoch_JD,
			   comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d, comet[i].epoch_h);

	do {
		system("CLS");
		printf("\n     Converting \"NASA format\"\n\n");
		printf(" =============================================================================\n\n");
		printf("    1.   Main Menu\n");
		printf("    2.	 Exit\n\n");
		printf(" =============================================================================\n\n");
		printf("  %d comets successfully saved in file %s\n\n", N, fout_name);
		printf("  Choice: ");
		scanf("%d", &b);
	} while (b!=1 && b!=2);

	fflush(fin);
	fflush(stdin);
	end: return b;
}

int input_nasa_sbdb (){

	int b, i, j, t, N=0;
	char c, z, fin_name [80+1], fout_name [80+1];
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
		char nepotrebno [10+1];
	} comet[MAX];

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	printf("\n     Converting \"NASA format\"\n\n");
	printf(" =============================================================================\n\n");
	printf("    1.   Main Menu\n");
	printf("    2.   Exit\n\n");
	printf(" =============================================================================\n\n");

	unos2: printf("  Enter input file name: ");
	scanf("%s", fin_name);

	b=atoi(fin_name);
	if (b==1 || b==2) goto end;

	fin=fopen(fin_name, "r");
	if (fin==NULL) {
		printf("\n  Error opening file %s\n\n", fin_name);
		goto unos2;
	}

	else printf("\n  File %s is successfully opened\n", fin_name);

	printf("\n  Enter output file name: ");
	scanf("%s", fout_name);

	b=atoi(fout_name);
	if (b==1 || b==2) goto end;

	while ((c=fgetc(fin)) != EOF ){
		if (c=='\n') N++;
	}

	rewind(fin);

	for (i=0; i<N; i++){

		j=0;
		while ((c=fgetc(fin)) != ',' ){
			comet[i].name[j]=c;
			if (c=='"' && j>2) t=j;			// j>2 zato što se " pojavljuje i na prvom mjestu u stringu
			j++;
		}

		comet[i].name[0]=' ';
		comet[i].name[t+1]='\0';

		fscanf(fin, "%f", &comet[i].peri);

		z=fgetc(fin);

		fscanf(fin, "%f", &comet[i].ecc);

		z=fgetc(fin);

		fscanf(fin, "%f", &comet[i].peri_node);

		z=fgetc(fin);

		fscanf(fin, "%f", &comet[i].asc_node);

		z=fgetc(fin);

		fscanf(fin, "%f", &comet[i].incl);

		z=fgetc(fin);

		fscanf(fin, "%4d%2d%2d.%4d", &comet[i].epoch_y, &comet[i].epoch_m,
									 &comet[i].epoch_d, &comet[i].epoch_h);

		fscanf(fin, "%10[^\n]%*c", comet[i].nepotrebno);

		comet[i].period = compute_period (comet[i].peri, comet[i].ecc);
		comet[i].epoch_JD = compute_epoch (comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d);
		edit_name (comet[i].name);

		if (comet[i].ecc >= 1.000000)
			comet[i].ecc = 0.999999;

/*		// za uklanjanje kometa odredjenih karakteristika

		for (j=0; j<strlen(comet[i].name); j++){
			if ((comet[i].name[j]  =='S' && comet[i].name[j+1]=='O' &&
				 comet[i].name[j+2]=='H' && comet[i].name[j+3]=='O')
				|| (comet[i].period > 300)
				){
				--i;
				--N;
			}
		}
*/
	}

	fclose(fin);

	for (i=0; i<N; i++)
		output (fout_name, comet[i].name, comet[i].period, comet[i].peri, comet[i].ecc,
			   comet[i].incl, comet[i].asc_node, comet[i].peri_node, comet[i].epoch_JD,
			   comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d, comet[i].epoch_h);

	do {
		system("CLS");
		printf("\n     Converting \"NASA format\"\n\n");
		printf(" =============================================================================\n\n");
		printf("    1.   Main Menu\n");
		printf("    2.	 Exit\n\n");
		printf(" =============================================================================\n\n");
		printf("  %d comets successfully saved in file %s\n\n", N, fout_name);
		printf("  Choice: ");
		scanf("%d", &b);
	} while (b!=1 && b!=2);

	fflush(fin);
	fflush(stdin);
	end: return b;
}

/* 	STARI NACIN
int input_nasa_sbdb (){

	int b, i, j, k, N=0;
	char c, e, fin_name [80+1], fout_name [80+1];
	FILE *fin;

	int z1, z2, z3, z4, z5, z6;

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
		char buffer [200+1];
		char *buff_peri;
		char *buff_ecc;
		char *buff_peri_node;
		char *buff_asc_node;
		char *buff_incl;
		char *buff_ey, *buff_em, *buff_ed, *buff_eh;
	} comet[MAX];

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	printf("\n     Converting \"NASA format\"\n\n");
	printf(" =============================================================================\n\n");
	printf("    1.   Main Menu\n");
	printf("    2.   Exit\n\n");
	printf(" =============================================================================\n\n");

	unos2: printf("  Enter input file name: ");
	scanf("%s", fin_name);

	b=atoi(fin_name);
	if (b==1 || b==2) goto end;

	fin=fopen(fin_name, "r");
	if (fin==NULL) {
		printf("\n  Error opening file %s\n\n", fin_name);
		goto unos2;
	}

	else printf("\n  File %s is successfully opened\n", fin_name);

	printf("\n  Enter output file name: ");
	scanf("%s", fout_name);

	b=atoi(fout_name);
	if (b==1 || b==2) goto end;

	while ((c=fgetc(fin)) != EOF ){
		if (c=='\n') N++;
	}

	rewind(fin);


	for (i=0; i<N; i++) {

		fscanf(fin, "%200[^\n]%*c", comet[i].buffer);

		{	//ocitavanje zareza u stringu
			for (j=0; j<200; j++){
				if (comet[i].buffer[j]==','){
					z1=j;
					break;
				}
			}

			for (j=z1+1; j<200; j++){
				if (comet[i].buffer[j]==','){
					z2=j;
					break;
				}
			}

			for (j=z2+1; j<200; j++){
				if (comet[i].buffer[j]==','){
					z3=j;
					break;
				}
			}

			for (j=z3+1; j<200; j++){
				if (comet[i].buffer[j]==','){
					z4=j;
					break;
				}
			}

			for (j=z4+1; j<200; j++){
				if (comet[i].buffer[j]==','){
					z5=j;
					break;
				}
			}

			for (j=z5+1; j<200; j++){
				if (comet[i].buffer[j]==','){
					z6=j;
					break;
				}
			}
		}

		for (j=0; j<z1; j++){
			comet[i].name[j]=comet[i].buffer[j];
		}

		comet[i].name[0]=' ';
		comet[i].name[z1]='\0';

		{	// premjestanje znakova iz stringa u nove stringove uz
			// pomoc polozaja zareza i pretvaranje istih u brojeve

			comet[i].buff_peri=malloc(sizeof(char)*(z2-z1));
			for(j=z1+1, k=0; j<z2; j++, k++)
				comet[i].buff_peri[k]=comet[i].buffer[j];
			comet[i].peri=atof(comet[i].buff_peri);
			free(comet[i].buff_peri);

			comet[i].buff_ecc=malloc(sizeof(char)*(z3-z2));
			for(j=z2+1, k=0; j<z3; j++, k++)
				comet[i].buff_ecc[k]=comet[i].buffer[j];
			comet[i].ecc=atof(comet[i].buff_ecc);
			free(comet[i].buff_ecc);

			comet[i].buff_peri_node=malloc(sizeof(char)*(z4-z3));
			for(j=z3+1, k=0; j<z4; j++, k++)
				comet[i].buff_peri_node[k]=comet[i].buffer[j];
			comet[i].peri_node=atof(comet[i].buff_peri_node);
			free(comet[i].buff_peri_node);

			comet[i].buff_asc_node=malloc(sizeof(char)*(z5-z4));
			for(j=z4+1, k=0; j<z5; j++, k++)
				comet[i].buff_asc_node[k]=comet[i].buffer[j];
			comet[i].asc_node=atof(comet[i].buff_asc_node);
			free(comet[i].buff_asc_node);

			comet[i].buff_incl=malloc(sizeof(char)*(z6-z5));
			for(j=z5+1, k=0; j<z6; j++, k++)
				comet[i].buff_incl[k]=comet[i].buffer[j];
			comet[i].incl=atof(comet[i].buff_incl);
			free(comet[i].buff_incl);

			comet[i].buff_ey=malloc(sizeof(char)*4);
			for(j=z6+1, k=0; j<z6+5; j++, k++)
				comet[i].buff_ey[k]=comet[i].buffer[j];
			comet[i].epoch_y=atoi(comet[i].buff_ey);
			free(comet[i].buff_ey);

			comet[i].buff_em=malloc(sizeof(char)*2);
			for(j=z6+5, k=0; j<z6+7; j++, k++)
				comet[i].buff_em[k]=comet[i].buffer[j];
			comet[i].epoch_m=atoi(comet[i].buff_em);
			free(comet[i].buff_em);

			comet[i].buff_ed=malloc(sizeof(char)*2);
			for(j=z6+7, k=0; j<z6+9; j++, k++)
				comet[i].buff_ed[k]=comet[i].buffer[j];
			comet[i].epoch_d=atoi(comet[i].buff_ed);
			free(comet[i].buff_ed);

			comet[i].buff_eh=malloc(sizeof(char)*4);
			for(j=z6+10, k=0; j<z6+14; j++, k++)
				comet[i].buff_eh[k]=comet[i].buffer[j];
			comet[i].epoch_h=atoi(comet[i].buff_eh);
			free(comet[i].buff_eh);
		}

// 		za uklanjanje SOHO kometa
//
//		for (j=0; j<strlen(comet[i].name); j++){
//			if ((comet[i].name[j]  =='S' && comet[i].name[j+1]=='O' &&
//				 comet[i].name[j+2]=='H' && comet[i].name[j+3]=='O')
//				|| (comet[i].peri>=1.5) || (comet[i].ecc >= 1.0)
//				){
//				--i;
//				--N;
//			}
//		}


		comet[i].period = compute_period (comet[i].peri, comet[i].ecc);
		comet[i].epoch_JD = compute_epoch (comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d);
		edit_name (comet[i].name);

// 		za uklanjanje kometa ciji je q veci od 1.5 AU
//
//		if (comet[i].peri>=1.5){
//			--i;
//			--N;
//		}


		if (comet[i].ecc >= 1.000000)
			comet[i].ecc = 0.999999;
	}

	fclose(fin);

	for (i=0; i<N; i++)
		output (fout_name, comet[i].name, comet[i].period, comet[i].peri, comet[i].ecc,
			   comet[i].incl, comet[i].asc_node, comet[i].peri_node, comet[i].epoch_JD,
			   comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d, comet[i].epoch_h);

	do {
		system("CLS");
		printf("\n     Converting \"NASA format\"\n\n");
		printf(" =============================================================================\n\n");
		printf("    1.   Main Menu\n");
		printf("    2.	 Exit\n\n");
		printf(" =============================================================================\n\n");
		printf("  %d comets successfully saved in file %s\n\n", N, fout_name);
		printf("  Choice: ");
		scanf("%d", &b);
	} while (b!=1 && b!=2);

	fflush(fin);
	fflush(stdin);
	end: return b;
}
*/

int input_ciel (){

	int b, i, N=0;
	char c, fin_name [80+1], fout_name [80+1];
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

	fflush(stdin);
	fflush(stdout);
	system("CLS");
	printf("\n     Converting \"ciel format\"\n\n");
	printf(" =============================================================================\n\n");
	printf("    1.   Main Menu\n");
	printf("    2.   Exit\n\n");
	printf(" =============================================================================\n\n");

	unos2: printf("  Enter input file name: ");
	scanf("%s", fin_name);

	b=atoi(fin_name);
	if (b==1 || b==2) goto end;

	fin=fopen(fin_name, "r");
	if (fin==NULL) {
		printf("\n  Error opening file %s\n\n", fin_name);
		goto unos2;
	}

	else printf("\n  File %s is successfully opened\n", fin_name);

	printf("\n  Enter output file name: ");
	scanf("%s", fout_name);

	b=atoi(fout_name);
	if (b==1 || b==2) goto end;

	while ((c=fgetc(fin)) != EOF ){
		if (c=='\n') N++;
	}

	rewind(fin);

	for (i=0; i<N; i++) {

		fscanf(fin, "%40c %*c %d %*c %4d %2d %2d %*c %d %*c %f %*c %f %*c %f %*c %f %*c %f %40[^\n]%*c",
				comet[i].name, &comet[i].equinox, &comet[i].epoch_y, &comet[i].epoch_m,
				&comet[i].epoch_d, &comet[i].epoch_h, &comet[i].peri, &comet[i].ecc,
				&comet[i].peri_node, &comet[i].asc_node, &comet[i].incl, &comet[i].nepotrebno);

		comet[i].period = compute_period (comet[i].peri, comet[i].ecc);
		comet[i].epoch_JD = compute_epoch (comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d);
		edit_name (comet[i].name);

		if (comet[i].ecc >= 1.000000)
			comet[i].ecc = 0.999999;
	}

	fclose(fin);

	for (i=0; i<N; i++)
		output (fout_name, comet[i].name, comet[i].period, comet[i].peri, comet[i].ecc,
			   comet[i].incl, comet[i].asc_node, comet[i].peri_node, comet[i].epoch_JD,
			   comet[i].epoch_y, comet[i].epoch_m, comet[i].epoch_d, comet[i].epoch_h);

	do {
		system("CLS");
		printf("\n     Converting \"ciel format\"\n\n");
		printf(" =============================================================================\n\n");
		printf("    1.   Main Menu\n");
		printf("    2.	 Exit\n\n");
		printf(" =============================================================================\n\n");
		printf("  %d comets successfully saved in file %s\n\n", N, fout_name);
		printf("  Choice: ");
		scanf("%d", &b);
	} while (b!=1 && b!=2);

	fflush(fin);
	fflush(stdin);
	end: return b;
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

	fprintf(fout,"\"%s\" \"Sol\"\n", name);
	fprintf(fout,"{\n");
	fprintf(fout,"Class \"comet\" \n");
	fprintf(fout,"Mesh \"asteroid.cms\" \n");
	fprintf(fout,"Texture \"asteroid.jpg\" \n");
	fprintf(fout,"Radius 5 \n");
	fprintf(fout,"Albedo 0.1 \n");
	fprintf(fout,"EllipticalOrbit \n");
	fprintf(fout,"\t{ \n");
	fprintf(fout,"\tPeriod \t\t\t %f \n", P);
	fprintf(fout,"\tPericenterDistance \t %f \n", q);
	fprintf(fout,"\tEccentricity \t\t %f \n", e);
	fprintf(fout,"\tInclination \t\t %.4f \n", i);
	fprintf(fout,"\tAscendingNode \t\t %.4f \n", node);
	fprintf(fout,"\tArgOfPericenter \t %.4f \n", peri);
	fprintf(fout,"\tMeanAnomaly \t\t 0  \n");
	fprintf(fout,"\tEpoch \t\t\t %d.%.4d\t# %d %s %.2d.%.4d \n", eJD, eh, ey, mon, ed, eh);
	fprintf(fout,"\t} \n");
	fprintf(fout,"}\n\n\n");

	fclose(fout);
}
