#include <iostream>
#include <iomanip>
#include <fstream>
#include <cstdio>
#include <cstdlib>
#include <cstring>
#include <cmath>
#include <ctime>
#include <cctype>
#include <conio.h>

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

struct Formats{
	string format;
	string soft;
}menu[19] = {
	{ format: "MPC", soft: "Soft00Cmt" },
	{ format: "SkyMap", soft: "Soft01Cmt" },
	{ format: "Guide", soft: "Soft02Cmt" },
	{ format: "xephem", soft: "Soft03Cmt" },
	{ format: "Home Planet", soft: "Soft04Cmt" },
	{ format: "MyStars!", soft: "Soft05Cmt" },
	{ format: "TheSky", soft: "Soft06Cmt" },
	{ format: "Starry Night", soft: "Soft07Cmt" },
	{ format: "Deep Space", soft: "Soft08Cmt" },
	{ format: "PC-TCS", soft: "Soft09Cmt" },
	{ format: "Earth Centered Universe", soft: "Soft10Cmt" },
	{ format: "Dance of the Planets", soft: "Soft11Cmt" },
	{ format: "MegaStar V4.x", soft: "Soft12Cmt" },
	{ format: "SkyChart III", soft: "Soft13Cmt" },
	{ format: "Voyager II", soft: "Soft14Cmt" },
	{ format: "SkyTools", soft: "Soft15Cmt" },
	{ format: "Autostar", soft: "Soft16Cmt" },
	{ format: "Celestia (SSC)", soft: "empty" },
	{ format: "Stellarium", soft: "empty" }
};

struct Excludings{
	int key[14];
	long int T;
	float q;
	float e;
	float an;
	float pn;
	float i;
	float P;
};

enum RETURN_CODE {SUCCESS, MAIN_MENU, EXIT_PROG};/*
 * SUCCESS		==0	 fja je uspjesno izvrsena
 * MAIN_MENU	==1	 povratak na gl izbornik
 * EXIT_PROG	==2	 izlazak iz programa
 */


//	funkcije za ispisivanje na ekran
void start_screen ();
void screen_imp (string, string);
void screen_exp1 (string);
void screen_exp2 (string, string);
void screen_exp3 (string, string);
void exit_screen ();
void excl_screen ();
void tools_soft_screen();

//	funkcije za citanje iz datoteka
int import_main (int, int);			//glavna fja

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
double compute_period (float, float);
long int greg_to_jul (int, int, int);
void jul_to_greg (long int, int &, int &, int &);
void remove_spaces (char *);

void clear_all();
void clear_data();

int tools ();
int tools_a();
int tools_b();
int tools_c();
int tools_d();
int tools_e();

int sort_data(int);
void do_swap(int i, int j);

int define_exclude();
bool do_exclude(int);
