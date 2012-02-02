#ifndef _COMET_OEW_
#define _COMET_OEW_

#include <cstdio>
#include <cstdlib>
#include <cstring>
#include <cmath>
#include <ctime>
#include <cctype>

#include <direct.h>

#define PROGRAM_VERSION 0.5

#define MAX_CMT 3500
#define equinox 2000
#define ep_y 2011
#define ep_m 2
#define ep_d 8
#define eq_JD greg_to_jul(ep_y, ep_m, ep_d)-1

// eq_JD se nakon nekog vremena povecava za 200 dana
// saznati kada, i to implementirati u program

using namespace std;

struct Settings{
	int checkNewVersion;
	int advancedMode;
	int showSplash;
	int exitConfirm;
	int importConfirm;
};

class Comet{

public:
	char full [80+1];
	char name [55+1];
	char ID [25+1];
	long int T;
	int y;
	int m;
	float d;
	double P;
	float q;
	float e;
	float i;
	float an;
	float pn;
	float H;
	float G;
	double sort;
	Comet *next;

	Comet(){

		for(int i=0; i<81; i++) full[i] = '\0';
		for(int i=0; i<56; i++) name[i] = '\0';
		for(int i=0; i<26; i++) ID[i] = '\0';
		T = 0;
		y = 0;
		m = 0;
		d = 0.0;
		P = 0.0;
		q = 0.0;
		e = 0.0;
		i = 0.0;
		an = 0.0;
		pn = 0.0;
		H = 0.0;
		G = 0.0;
		sort = 0.0;
		next = NULL;
	}

	Comet(Comet *cmt){

		for(int a=0; a<81; a++) full[a] = '\0';
		for(int a=0; a<56; a++) name[a] = '\0';
		for(int a=0; a<26; a++) ID[a] = '\0';

		strcpy(full, cmt->full);
		strcpy(name, cmt->name);
		strcpy(ID, cmt->ID);

		T = cmt->T;
		y = cmt->y;
		m = cmt->m;
		d = cmt->d;
		P = cmt->P;
		q = cmt->q;
		e = cmt->e;
		i = cmt->i;
		an = cmt->an;
		pn = cmt->pn;
		H = cmt->H;
		G = cmt->G;
		sort = cmt->sort;
		next = NULL;
	}

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

void addCmt(Comet **, Comet *);
Comet *getCmt(Comet *, int);
void ocistiMemoriju(Comet **);
Comet *sortList(Comet *, int);
void deleteFromMiddle(Comet *);
void deleteFirst(Comet **);
void deleteLast(Comet **);
int totalComets(Comet *);

//	funkcije za racunanje...
double get_sort_key(char *);
double compute_period (double, double);
long int greg_to_jul (int, int, int);
void jul_to_greg (long int, int &, int &, float &);
void remove_spaces (char *);

void editFullIdName(char *, char *, char *, int);

bool define_exclude();
bool do_exclude(int);

void wait(int);

#endif

