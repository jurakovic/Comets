//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop
#include <tchar.h>
//---------------------------------------------------------------------------




















































USEFORM("Frame1.cpp", Frame01); /* TFrame: File Type */
USEFORM("Frame2.cpp", Frame02); /* TFrame: File Type */
USEFORM("FormPreview.cpp", Form11);
USEFORM("FormSettings.cpp", Form7);
USEFORM("FrameSplash.cpp", FrameSplash1); /* TFrame: File Type */
USEFORM("MainForm.cpp", Form1);
USEFORM("Frame3.cpp", Frame03); /* TFrame: File Type */
USEFORM("Frame4.cpp", Frame04); /* TFrame: File Type */
USEFORM("FormExit.cpp", Form9);
USEFORM("FormAbout.cpp", Form8);
USEFORM("FormDetails.cpp", Form12);
USEFORM("FormFilters.cpp", Form2);
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
		Application->CreateForm(__classid(TForm2), &Form2);
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
