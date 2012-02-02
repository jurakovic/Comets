//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop
#include <tchar.h>
//---------------------------------------------------------------------------


























USEFORM("Unit5.cpp", Frame5); /* TFrame: File Type */
USEFORM("Unit2.cpp", Frame2); /* TFrame: File Type */
USEFORM("Unit3.cpp", Frame3); /* TFrame: File Type */
USEFORM("Unit8.cpp", Form8);
USEFORM("Unit9.cpp", Form9);
USEFORM("Unit6.cpp", Frame6); /* TFrame: File Type */
USEFORM("Unit7.cpp", Form7);
USEFORM("Unit1.cpp", Form1);
USEFORM("Unit10.cpp", Frame10); /* TFrame: File Type */
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
