#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>
#include <ctype.h>


//	funkcije za ispisivanje na ekran
void start_screen ();
void screen_imp ();
void screen_exp1 ();
void screen_exp2 ();
void screen_exp3 ();
void exit_screen ();

//	funkcije za citanje iz datoteka
int  import_menu (int Ty);				//glavna fja

void import_mpc (int N);
void import_skymap (int N);
void import_guide (int N);
void import_xephem (int N);
void import_voyager (int N);
void import_home_planet (int N);
void import_mystars (int N);			//comet[i].pn zapravo nije .pn !!!
void import_thesky (int N);
void import_starry_night (int N);
void import_deep_space (int N);
void import_pc_tcs (int N);
void import_skytools (int N);
void import_skychart (int N);
void import_ecu (int N);
void import_dance (int N);
void import_megastar (int N);
void import_nasa1 (int N);			//ELEMENTS.COMET
void import_nasa2 (int N);			//sbdb query
void import_cfw (int N);				//comet for windows

//	funkcije za racunanje...
double compute_period (float q, float e);
long int compute_JD (int y, int m, int d);
char *edit_name (char *name);

//	funkcije za pisanje u datoteke
void output_ssc (int N, int Ty);
void output_stell (int N, int Ty);
