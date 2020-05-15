#include "PC_FileIO.c"


task main()
{
	TFileHandle fout;
  openWritePC(fout,"fileWrite.txt");
	writeTextPC(fout,"Hello");
	writeDebugStream("Hello");
	closeFilePC(fout);

}

