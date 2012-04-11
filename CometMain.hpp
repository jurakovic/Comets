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

using namespace std;

struct Settings{
	int checkNewVersion;
	int advancedMode;
	int exitConfirm;
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

template <class T> void addCmt(T **head, T *cmt){

	if(*head==NULL){

		T *temp = new T(cmt);
		*head = temp;
	}

	else{
		T *temp = *head;

		while(temp->next != NULL){
			temp = temp->next;
		}

		T *r = new T(cmt);
		temp->next = r;
	}
}

template <class T> T *getCmt(T *head, int index){

	int i=0;

	while(head!=NULL){

		if(index == i) return head;

		i++;
		head = head->next;
	}
}

template <class T> void deleteFirst(T **head){

	T *temp;

	if(*head == NULL) return;

	else{

		temp = *head;
		*head = (*head)->next;
		delete temp;
	}
}

template <class T> void deleteFromMiddle(T *cmt){

//	Pseudocode:
//	void delete_node(Node* pNode) {
//		pNode->Data = pNode->Next->Data;  // Assume that SData::operator=(SData&) exists.
//		Node* pTemp = pNode->Next->Next;
//		delete(pNode->Next);
//		pNode->Next = pTemp;
//	}

	strcpy(cmt->full, cmt->next->full);
	strcpy(cmt->name, cmt->next->name);
	strcpy(cmt->ID, cmt->next->ID);
	cmt->T = cmt->next->T;
	cmt->y = cmt->next->y;
	cmt->m = cmt->next->m;
	cmt->d = cmt->next->d;
	cmt->P = cmt->next->P;
	cmt->q = cmt->next->q;
	cmt->e = cmt->next->e;
	cmt->i = cmt->next->i;
	cmt->an = cmt->next->an;
	cmt->pn = cmt->next->pn;
	cmt->H = cmt->next->H;
	cmt->G = cmt->next->G;
	cmt->sort = cmt->next->sort;

	T *temp = cmt->next->next;
	delete cmt->next;
	cmt->next = temp;
}

template <class T> void deleteLast(T **head){

	T *temp, *prev;

	if (*head == NULL) return;

	else if ((*head)->next == NULL){

		temp = *head;
		*head = NULL;
		delete temp;
    }

	else{

		prev = *head;
		temp = (*head)->next;
		while (temp->next != NULL){

			prev = temp;
			temp = temp->next;
		}

		prev->next = NULL;
		delete temp;
	}
}

template <class T> void ocistiMemoriju(T **head){

	T *current = NULL;
	while ((*head) != NULL){
		current = *head;
		*head = current->next;
		delete current;
	}
}

template <class T> int totalComets(T *head){

	int counter = 0;
	while(head!=NULL){
		counter++;
		head = head->next;
	}
	return counter;
}

template <class T> T *sortList(T *h, int ty){

	T *sorted = NULL;

	while(h!=NULL){

		T *head = h;
		T **trail = &sorted;

		h = h->next;

		while(1){

			if(*trail == NULL ||
				(ty== 0 && head->sort < (*trail)->sort) ||
				(ty== 1 && head->sort > (*trail)->sort) ||
				(ty== 2 && head->T < (*trail)->T) ||
				(ty== 3 && head->T > (*trail)->T) ||
				(ty== 4 && head->q < (*trail)->q) ||
				(ty== 5 && head->q > (*trail)->q) ||
				(ty== 6 && head->e < (*trail)->e) ||
				(ty== 7 && head->e > (*trail)->e) ||
				(ty== 8 && head->an < (*trail)->an) ||
				(ty== 9 && head->an > (*trail)->an) ||
				(ty==10 && head->pn < (*trail)->pn) ||
				(ty==11 && head->pn > (*trail)->pn) ||
				(ty==12 && head->i < (*trail)->i) ||
				(ty==13 && head->i > (*trail)->i) ||
				(ty==14 && head->P < (*trail)->P) ||
				(ty==15 && head->P > (*trail)->P)){

				head->next = *trail;
				*trail = head;
				break;
			}
			else{
				trail = &(*trail)->next;
			}
		}
	}

	return sorted;
}

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

