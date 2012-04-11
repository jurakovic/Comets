//---------------------------------------------------------------------------

#ifndef FrameSplashH
#define FrameSplashH
//---------------------------------------------------------------------------
#include <System.Classes.hpp>
#include <Vcl.Controls.hpp>
#include <Vcl.StdCtrls.hpp>
#include <Vcl.Forms.hpp>
#include <Vcl.ExtCtrls.hpp>
#include <Vcl.Imaging.pngimage.hpp>
//---------------------------------------------------------------------------
class TFrameSplash1 : public TFrame
{
__published:	// IDE-managed Components
	TImage *Image1;
	TCheckBox *CheckBox1;
	void __fastcall Image1Click(TObject *Sender);
private:	// User declarations
public:		// User declarations
	__fastcall TFrameSplash1(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TFrameSplash1 *FrameSplash1;
//---------------------------------------------------------------------------
#endif
