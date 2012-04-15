//---------------------------------------------------------------------------

#ifndef MainFormH
#define MainFormH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include "Frame1.h"
#include "Frame2.h"
#include "Frame3.h"
#include "Frame4.h"
#include "FormExit.h"

#include "CometMain.hpp"
#include "FrameSplash.h"
#include <Vcl.Menus.hpp>
#include "Frame3.h"

//---------------------------------------------------------------------------
class TForm1 : public TForm
{
__published:	// IDE-managed Components
	TMainMenu *MainMenu1;
	TMenuItem *File1;
	TMenuItem *Exit1;
	TMenuItem *Help1;
	TMenuItem *About1;
	TMenuItem *N1;
	TMenuItem *Checkfornewversion1;
	TMenuItem *ools1;
	TMenuItem *Settings2;
	TFrame04 *Frame4;
	TFrame03 *Frame3;
	TFrame02 *Frame2;
	TFrame01 *Frame1;
	void __fastcall FormClose(TObject *Sender, TCloseAction &Action);
	void __fastcall Settings2Click(TObject *Sender);
	void __fastcall Exit1Click(TObject *Sender);
	void __fastcall About1Click(TObject *Sender);
	void __fastcall FormCreate(TObject *Sender);
private:	// User declarations
public:		// User declarations
	__fastcall TForm1(TComponent* Owner);
	UnicodeString dataFolder;
	UnicodeString settingsFile;
	struct Excludings excl;
	struct Settings sett;

	Comet *cmt;

	bool exitFunction();
	void spremiPostavke();

	bool define_exclude();
	bool do_exclude(Comet *);
	bool check_name(Comet *);

	void updateListbox(Comet *);

	int import_main (int , UnicodeString);
	void export_main (int , int , UnicodeString);
	void export_semi(int , Comet *, FILE *);
	void writeNecessaryText(int, FILE *);

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

	void export_mpc (Comet *, FILE *);
	void export_skymap (Comet *, FILE *);
	void export_guide (Comet *, FILE *);
	void export_xephem (Comet *, FILE *);
	void export_home_planet (Comet *, FILE *);
	void export_mystars (Comet *, FILE *);
	void export_thesky (Comet *, FILE *);
	void export_starry_night (Comet *, FILE *);
	void export_deep_space (Comet *, FILE *);
	void export_pc_tcs (Comet *, FILE *);
	void export_ecu (Comet *, FILE *);
	void export_dance (Comet *, FILE *);
	void export_megastar (Comet *, FILE *);
	void export_skychart (Comet *, FILE *);
	void export_voyager (Comet *, FILE *);
	void export_skytools (Comet *, FILE *);
	void export_ssc (Comet *, FILE *);
	void export_stell (Comet *, FILE *);

};
//---------------------------------------------------------------------------
extern PACKAGE TForm1 *Form1;
//---------------------------------------------------------------------------
#endif
