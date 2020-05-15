#include "PC_FileIO.c"
task main()
{
	clearDebugStream();
	writeDebugStreamLine("Hello");
	wait1Msec(500);
	string s = "";
	while(s!=""||s=="")
	{
			TFileHandle fin;
			bool fileOkay = openReadPC(fin, "chessmove.txt");
			readTextPC(fin, s);
			displayString(1,s);
			closeFilePC(fin);
	}
	displayString(1,s);
	  }
