//---------------------------------------------------------------------------

#ifndef Unit10H
#define Unit10H
//---------------------------------------------------------------------------
#include <System.Classes.hpp>
#include <Vcl.Controls.hpp>
#include <Vcl.StdCtrls.hpp>
#include <Vcl.Forms.hpp>
#include <Vcl.ExtCtrls.hpp>
#include <Vcl.Imaging.pngimage.hpp>
//---------------------------------------------------------------------------
class TFrame10 : public TFrame
{
__published:	// IDE-managed Components
	TImage *Image1;
	TCheckBox *CheckBox1;
	void __fastcall Image1Click(TObject *Sender);
private:	// User declarations
public:		// User declarations
	__fastcall TFrame10(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TFrame10 *Frame10;
//---------------------------------------------------------------------------
#endif
