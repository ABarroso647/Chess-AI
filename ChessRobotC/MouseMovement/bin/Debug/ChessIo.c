#include "PC_FileIO.c"
task main()
{
	//setting up files to be handled
	TFileHandle FIn;
	TFileHandle FOut;
	openReadPC(FIn,"chessmove.txt");
	int WhiteKing = true;
	int BlackKing = true;
	bool StaleMate = false;
	int Attack = 0;
	string Input = "";
	//keep checking will the file is empty
	while(Input == "")
	{
		readTextPC(FIn, Input);
	}
	//display
	displayString(1,"Success Exit");
	//conversion as stalemate is a bool
	StaleMate = atoi(Input);
	readIntPC(FIn,WhiteKing);
	readIntPC(FIn,BlackKing);
	readIntPC(FIn,Attack);
	//make sure all values are saved properly
	displayString(2,"%d",StaleMate);
	displayString(3,"%d",WhiteKing);
	displayString(4,"%d",BlackKing);
	displayString(5,"%d",Attack);
	int CurX = 0,CurY = 0, NewX = 0, NewY = 0;
	readIntPC(FIn,CurX);
	readIntPC(FIn,CurY);
	readIntPC(FIn,NewX);
	readIntPC(FIn,NewY);
	//make sure all values are saved properly
	displayString(6,"%d",CurX);
	displayString(7,"%d",CurY);
	displayString(8,"%d",NewX);
	displayString(9,"%d",NewY);
	//this is where movement woudl take place
	closeFilePC(FIn);
	///clear contents of chessmove
	openWritePC(FOut,"chessmove.txt");
	writeTextPC(FOut,"");
	closeFilePC(FOut);
	//clear contents of aistuff
	openWritePC(FOut,"aiStuff.txt");
	writeTextPC(FOut,"");
	closeFilePC(FOut);
	if(Attack==1)
	{
		//this is where the pc would wait for the piece to be moved
		displayString(1,"Attack Needed");
	}
	//so data stays on screen until button is pressed
	while(!getButtonPress(buttonAny))
	{}
}
