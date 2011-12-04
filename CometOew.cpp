//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop
#include <tchar.h>
//---------------------------------------------------------------------------
USEFORM("Unit3.cpp", Frame3); /* TFrame: File Type */
USEFORM("Unit5.cpp", Frame5); /* TFrame: File Type */
USEFORM("Unit2.cpp", Frame2); /* TFrame: File Type */
USEFORM("Unit1.cpp", Form1);
USEFORM("Unit4.cpp", Frame4); /* TFrame: File Type */
USEFORM("Unit6.cpp", Frame6); /* TFrame: File Type */
//---------------------------------------------------------------------------
WINAPI _tWinMain(HINSTANCE, HINSTANCE, LPTSTR, int)
{
	try
	{
		Application->Initialize();
		Application->MainFormOnTaskBar = true;
		Application->Title = "Comet Orbital Elements Workshop";
		Application->CreateForm(__classid(TForm1), &Form1);
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
