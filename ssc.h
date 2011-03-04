#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>
#include <ctype.h>
#include <time.h>


//	funkcije za ispisivanje na ekran
void screen_imp();
void screen_exp1();
void screen_exp2();
void screen_exp3();
void exit_screen();

//	funkcije za citanje iz datoteka
int  import_menu();				//glavna fja
void import_mpc();
void import_skymap();
void import_guide();
void import_xephem();
void import_voyager();
void import_home_planet();
void import_mystars();			//comet[i].pn zapravo nije .pn !!!
void import_thesky();
void import_starry_night();
void import_deep_space();
void import_pc_tcs();
void import_skytools();
void import_skychart();
void import_ecu();
void import_dance();
void import_megastar();
void import_nasa1();			//ELEMENTS.COMET
void import_nasa2();			//sbdb query
void import_cfw();				//comet for windows

//	funkcije za racunanje...
float compute_period (float q, float e);
int   compute_JD (int y, int m, int d);
char *edit_name (char *name);

//	funkcije za pisanje u datoteke
void output_ssc ();
void output_stell ();
