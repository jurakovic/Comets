//---------------------------------------------------------------------------

#ifndef Unit1H
#define Unit1H
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include "Unit2.h"
#include "Unit3.h"
#include "Unit4.h"
#include "Unit5.h"
#include "Unit6.h"
#include "Unit9.h"

#include "CometMain.hpp"
#include "Unit10.h"

//---------------------------------------------------------------------------
class TForm1 : public TForm
{
__published:	// IDE-managed Components
	TFrame2 *Frame21;
	TFrame3 *Frame31;
	TFrame4 *Frame41;
	TFrame5 *Frame51;
	TFrame6 *Frame61;
	TFrame10 *Frame101;
	void __fastcall FormShow(TObject *Sender);
	void __fastcall FormClose(TObject *Sender, TCloseAction &Action);
private:	// User declarations
public:		// User declarations
	__fastcall TForm1(TComponent* Owner);
	UnicodeString defaultDataFolder;
	UnicodeString settingsFile;
	struct Excludings excl;
	struct Settings sett;
	Comet *cmt;

	bool exitFunction();
	void spremiPostavke();

	void sort_data(int);
	void do_swap(int , int);
	bool define_exclude();
	bool do_exclude(int);

	int import_main (int , UnicodeString);
	void export_main (int , int , UnicodeString);
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
	int import_nasa (int, FILE *);		//ELEMENTS.COMET
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

};
//---------------------------------------------------------------------------
extern PACKAGE TForm1 *Form1;
//---------------------------------------------------------------------------
#endif
