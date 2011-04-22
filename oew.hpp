#include <iostream>
#include <fstream>
#include <cstdio>
#include <cstdlib>
#include <string>
#include <string.h>
#include <cmath>
#include <cctype>
#include <conio.h>

#define MAX_CMT 4000
#define equinox 2000
#define ep_y 2011
#define ep_m 2
#define ep_d 8
#define eq_JD compute_T(ep_y, ep_m, ep_d)-1

//	funkcije za ispisivanje na ekran
void start_screen ();
void screen_imp (char *import_format, char *soft);
void screen_exp1 (char *import_format);
void screen_exp2 (char *import_format, char *export_format);
void screen_exp3 (char *import_format, char *export_format);
void exit_screen ();
void help_screen ();
void excl_screen ();

//	funkcije za citanje iz datoteka
int import_main (int Ty);			//glavna fja

int import_mpc (int N, char *fin_name);
int import_skymap (int N, char *fin_name);
int import_guide (int N, char *fin_name);
int import_xephem (int N, char *fin_name);
int import_voyager (int N, char *fin_name);
int import_home_planet (int N, char *fin_name);
int import_mystars (int N, char *fin_name);			//comet[i].pn zapravo nije .pn !!!
int import_thesky (int N, char *fin_name);
int import_starry_night (int N, char *fin_name);
int import_deep_space (int N, char *fin_name);
int import_pc_tcs (int N, char *fin_name);
int import_skytools (int N, char *fin_name);
int import_skychart (int N, char *fin_name);
int import_ecu (int N, char *fin_name);
int import_dance (int N, char *fin_name);
int import_megastar (int N, char *fin_name);
int import_cfw (int N, char *fin_name);		//comet for windows
int import_nasa1 (int N, char *fin_name);			//ELEMENTS.COMET
int import_nasa2 (int N, char *fin_name);			//sbdb query

//	funkcije za pisanje u datoteke
void export_mpc (int N, char *fout_name);
void export_skymap (int N, char *fout_name);
void export_guide (int N, char *fout_name);
void export_xephem (int N, char *fout_name);
void export_home_planet (int N, char *fout_name);
void export_mystars (int N, char *fout_name);
void export_thesky (int N, char *fout_name);
void export_starry_night (int N, char *fout_name);
void export_deep_space (int N, char *fout_name);
void export_pc_tcs (int N, char *fout_name);
void export_ecu (int N, char *fout_name);
void export_dance (int N, char *fout_name);
void export_megastar (int N, char *fout_name);
void export_skychart (int N, char *fout_name);
void export_voyager (int N, char *fout_name);
void export_skytools (int N, char *fout_name);
void export_ssc (int N, int Ty, char *fout_name);
void export_stell (int N, int Ty, char *fout_name);

//	funkcije za racunanje...
double compute_period (float q, float e);
long int compute_T (int y, int m, int d);
char *edit_name (char *name);

int sort_data(int N);

int define_exclude();
int do_exclude(int i);
