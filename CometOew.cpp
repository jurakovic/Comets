//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop
#include <tchar.h>
//---------------------------------------------------------------------------


















































USEFORM("Frame1.cpp", Frame01); /* TFrame: File Type */
USEFORM("Frame4.cpp", Frame04); /* TFrame: File Type */
USEFORM("FormPreview.cpp", Form11);
USEFORM("FormSettings.cpp", Form7);
USEFORM("Frame2.cpp", Frame02); /* TFrame: File Type */
USEFORM("Frame3.cpp", Frame03); /* TFrame: File Type */
USEFORM("FrameSplash.cpp", FrameSplash1); /* TFrame: File Type */
USEFORM("MainForm.cpp", Form1);
USEFORM("FormExit.cpp", Form9);
USEFORM("FormAbout.cpp", Form8);
USEFORM("FormDetails.cpp", Form12);
//---------------------------------------------------------------------------
WINAPI _tWinMain(HINSTANCE, HINSTANCE, LPTSTR, int)
{
	try
	{
		Application->Initialize();
		Application->MainFormOnTaskBar = true;
		Application->Title = "Comet Orbital Elements Workshop";
		Application->CreateForm(__classid(TForm1), &Form1);
		Application->CreateForm(__classid(TForm8), &Form8);
		Application->CreateForm(__classid(TForm7), &Form7);
		Application->CreateForm(__classid(TForm9), &Form9);
		Application->CreateForm(__classid(TForm11), &Form11);
		Application->CreateForm(__classid(TForm12), &Form12);
		Application->Run();
	}
	catch (Exception &exception)
	{
		Application->ShowException(&exception);
	}
	catch (...)
	{
		try
		{
			throw Exception("");
		}
		catch (Exception &exception)
		{
			Application->ShowException(&exception);
		}
	}
	return 0;
}
//---------------------------------------------------------------------------
