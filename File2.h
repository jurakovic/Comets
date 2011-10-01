#ifndef __COMET_DATA
#define __COMET_DATA

#include <cstdio>
#include <cstdlib>
#include <cstring>
#include <cmath>
#include <ctime>
#include <cctype>

#define MAX_CMT 5000
#define equinox 2000
#define ep_y 2011
#define ep_m 2
#define ep_d 8
#define eq_JD greg_to_jul(ep_y, ep_m, ep_d)-1

using namespace std;

struct Comet{
	char full [80+1];
	char name [55+1];
	char ID [25+1];
	long int T;
	int y;
	int m;
	int d;
	int h;
	double P;
	float q;
	float e;
	float i;
	float an;
	float pn;
	float H;
	float G;
	double sort;
	char book [20+1];
};

struct Excludings{
	bool key[14];
	long int T;
	float q;
	float e;
	float an;
	float pn;
	float i;
	float P;
};


//	funkcije za citanje iz datoteka
void import_main (int, const char*);			//glavna fja
void export_main (int, int, const char*);

int import_mpc (int, FILE *);
int import_skymap (int, FILE *);
int import_guide (int, FILE *);
int import_xephem (int, FILE *);
int import_voyager (int, FILE *);
int import_home_planet (int, FILE *);
int import_mystars (int, FILE *);			//comet[i].pn zapravo nije .pn !!!
int import_thesky (int, FILE *);
int import_starry_night (int, FILE *);
int import_deep_space (int, FILE *);
int import_pc_tcs (int, FILE *);
int import_skytools (int, FILE *);
int import_skychart (int, FILE *);
int import_ecu (int, FILE *);
int import_dance (int, FILE *);
int import_megastar (int, FILE *);
int import_cfw (int, FILE *);		//comet for windows
int import_nasa1 (int, FILE *);		//ELEMENTS.COMET
int import_nasa2 (int, FILE *);		//csv format

//	funkcije za pisanje u datoteke
void export_mpc (int, FILE *);
void export_skymap (int, FILE *);
void export_guide (int, FILE *);
void export_xephem (int, FILE *);
void export_home_planet (int, FILE *);
void export_mystars (int, FILE *);
void export_thesky (int, FILE *);
void export_starry_night (int, FILE *);
void export_deep_space (int, FILE *);
void export_pc_tcs (int, FILE *);
void export_ecu (int, FILE *);
void export_dance (int, FILE *);
void export_megastar (int, FILE *);
void export_skychart (int, FILE *);
void export_voyager (int, FILE *);
void export_skytools (int, FILE *);
void export_ssc (int, FILE *);
void export_stell (int, FILE *);

//	funkcije za racunanje...
double get_sort_key(char *);
double compute_period (double, double);
long int greg_to_jul (int, int, int);
void jul_to_greg (long int, int &, int &, int &);
void remove_spaces (char *);

bool sort_data(int);
void do_swap(int i, int j);

bool define_exclude();
bool do_exclude(int);

#endif
