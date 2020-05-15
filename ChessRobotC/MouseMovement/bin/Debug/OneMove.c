#include "PC_FileIO.c"
const int BoardSize = 8;
typedef struct
{
	//decided to use arrays that way we didnt have 3 seperate ints for each type
	int RookCol[3];
	int PawnCol[3];
	int KingCol[3];
	int QueenCol[3];
	int BishopCol[3];
	int KnightCol[3];
	int WhitePiece;
	char whitecoords[BoardSize][BoardSize];
}ColourValue;

const int Tol = 5;
const float SquareSize = 6.35;
const float xMovementconst = SquareSize*(360/(3.97*PI));
const float yMovementconst = SquareSize*(360/(3.55*PI));
const int XYPower = 15;


int checkButtonPress(bool difficulty)
{
	while(!getButtonPress(buttonAny))
	{}
	int Check = 0;
	if(getButtonPress(buttonLeft))
	{
		Check = 1;
	}

	else if(getButtonPress(buttonUp)&&!difficulty)
	{
		Check = 0;
	}

	else if(getButtonPress(buttonRight))
	{
		Check = 2;

	}
	//invalid input
	else
	{
		Check = 4;
	}
	while(getButtonPress(buttonAny))
	{}
	return Check;
}

void countdown()
{
	time1[T2] = 0;
	while(time1[T2]<5000)
	{
		displayBigTextLine(1, "%d SECONDS",(5000-time1[T2])/1000);
	}
}

void xMovement(float oldX, float newX, bool reset)
{
	int Negative = 1;
	float XChange = newX-oldX;
	//the only purpose of XChange is to determine if the movement is negative
	if(XChange<0)
	{
		Negative = -1;
		XChange *= Negative;
	}
	if(reset)
	{
		//this reset part basically happens only if the move being made isnt a consecutive one
		nMotorEncoder[motorD] = 0;
	}
	motor[motorD] = XYPower*Negative;
	//because newx is always positive  it made sense to seperate our movement into
	//two seperate while loops which handles movement approaching zero from both ends
	if(Negative>0)
	{
		//as a general note we use newX as we take all our calculations from 0 to the X value
		//as opposed to the last x value to the new X value
		while(nMotorEncoder[motorD]<newX*xMovementconst)
		{
			//displayBigTextLine(1,"%d",nMotorEncoder[motorD]); testing code
			//displayBigTextLine(3,"%f",newX*xMovementconst);
		}
	}
	else
	{
		while(nMotorEncoder[motorD]>newX*xMovementconst)
		{
			//displayBigTextLine(1,"%d",nMotorEncoder[motorD]); testing code
			//displayBigTextLine(3,"%f",newX*xMovementconst);
		}
	}
	motor[motorD] = 0;

}
//same comments as the Y value, just differnt constant and motor
void yMovement(float oldY, float newY, bool reset)
{
	int Negative = 1;
	float YChange = newY-oldY;
	if(YChange<0)
	{
		Negative = -1;
		YChange *= Negative;
	}
	if(reset)
	{
		nMotorEncoder[motorA] = 0;
	}
	motor[motorA] = XYPower*Negative;
	if(Negative>0)
	{

		while(nMotorEncoder[motorA]<newY*yMovementconst)
		{}
	}
	else
	{
		while(nMotorEncoder[motorA]>newY*yMovementconst)
		{}
	}
	motor[motorA] = 0;
}

void placePiece(bool pickup)
{
	const int ClawPower = 100;
	const int ClawDown = 35;
	const float Height = 9.8;
	const int ClawLiftPower = 5;
	const int OpenDegree = 40;
	const int CloseDegree = -30;
	//this opens the claw
	if(pickup)
	{
		nMotorEncoder[motorB] = 0;
		motor[motorB] = ClawPower;
		/*this wait here is mainly because we saw that otherwise our motors would not open the claw the full way
		as the motor would spin in its holding container and so this pause dealt with that spin if needed
		however its not so much that if the motor doesnt spin in place itll open it over 40 degrees
		*/
		wait1Msec(250);
		while(nMotorEncoder[motorB] < OpenDegree)
		{}
		motor[motorB] = 0;
	}
	//this drops the claw down
	wait1Msec(500);
	nMotorEncoder[motorC] = 0;
	motor[motorC] = -ClawDown;
	while(abs(nMotorEncoder[motorC]) < (Height)*(yMovementconst/SquareSize))
	{}
	motor[motorC] = 0;
	wait1Msec(1000);
	//this closes the claw
	if(pickup)
	{
		motor[motorB] = -ClawDown;
		//this was our experimentally determined value we needed to close the claw
		while(nMotorEncoder[motorB] > CloseDegree)
		{}
		motor[motorB] = 0;
	}
	//this opens the claw
	else
	{
		nMotorEncoder[motorB] = 0;
		motor[motorB] = ClawDown;
		wait1Msec(250);
		while(nMotorEncoder[motorB] < OpenDegree)
		{}
		motor[motorB] = 0;
	}
	//this lifts the claw up
	wait1Msec(1000);
	nMotorEncoder[motorC] = 0;
	motor[motorC] = ClawLiftPower;
	while(nMotorEncoder[motorC] < (Height*(yMovementconst/SquareSize)))
	{}
}
void movement(int oldX, int oldY, int newX, int newY)
{
	//pause times are just to allow everything to be more accurate
	//move to starting location
	//bool of one as we are beginning a consecutive moveset and so we need to reset the motor encoders
	xMovement(0,oldX,1);
	wait1Msec(250);
	yMovement(0,oldY,1);
	wait1Msec(500);
	//pickup the piece
	placePiece(true);
	//move to new location
	wait1Msec(250);
	xMovement(oldX,newX,0);
	wait1Msec(250);
	yMovement(oldY,newY,0);
	wait1Msec(500);
	//drop the piece
	placePiece(false);
	//move back to the origin
	wait1Msec(250);
	xMovement(newX,0,0);
	wait1Msec(250);
	yMovement(newY,0,0);
	wait1Msec(250);
}
bool isColor(int Chosen, ColourValue & Piece, int Red, int Green, int Blue)
{
	// Pawn = 1, Rook = 2, Knight = 3, Bishop = 4, King = 5 , Queen = 6
	//simple check if the given values are within the tolerance values for each of our systems
	if(Chosen == 1)
	{
		return (abs(Piece.PawnCol[0]-Red)<Tol&&abs(Piece.PawnCol[1]-Green)<Tol && abs(Piece.PawnCol[2]-Blue)<Tol);
	}
	else if(Chosen == 2)
	{
		return (abs(Piece.RookCol[0]-Red)<Tol&&abs(Piece.RookCol[1]-Green)<Tol && abs(Piece.RookCol[2]-Blue)<Tol);
	}
	else if(Chosen == 3)
	{
		return (abs(Piece.KnightCol[0]-Red)<Tol&&abs(Piece.KnightCol[1]-Green)<Tol && abs(Piece.KnightCol[2]-Blue)<Tol);
	}
	else if(Chosen == 4)
	{
		return (abs(Piece.BishopCol[0]-Red)<Tol&&abs(Piece.BishopCol[1]-Green)<Tol && abs(Piece.BishopCol[2]-Blue)<Tol);
	}
	else if(Chosen == 5)
	{
		return (abs(Piece.KingCol[0]-Red)<Tol&&abs(Piece.KingCol[1]-Green)<Tol && abs(Piece.KingCol[2]-Blue)<Tol);
	}
	else if(Chosen == 6)
	{
		return (abs(Piece.QueenCol[0]-Red)<Tol&&abs(Piece.QueenCol[1]-Green)<Tol && abs(Piece.QueenCol[2]-Blue)<Tol);
	}
	return false;

}

void scanBoard(bool initial, ColourValue & Piece)
{
	Piece.WhitePiece = 16;
	//these constants are basically the precise distances that we need to move to place the colour sensor over the centre of the square
	const int ColourX = 6.8;
	const int ColourY = -3;
	//divide by Square Size to cancel it out within the X and Y movement constant
	//this moves the colour sensor to the centre of the square
	yMovement(0,ColourX/SquareSize,0);
	xMovement(0,ColourY/SquareSize,0);

	if(initial)
	{
		//fill up all the empty parts of the array
		for(int XValue = 0; XValue< BoardSize-2; XValue++)
		{
			for(int YValue = 0; YValue<BoardSize; YValue++)
			{
				Piece.whitecoords[XValue][YValue] = ' ';
			}
		}
		//fill in the pawn row in the array
		for(int YValue = 0; YValue<BoardSize; YValue++)
		{
			Piece.whitecoords[6][YValue] = 'P';
		}
		Piece.whitecoords[7][0] = 'R';
		Piece.whitecoords[7][1] = 'H';
		Piece.whitecoords[7][2] = 'B';
		Piece.whitecoords[7][3] = 'Q';
		Piece.whitecoords[7][4] = 'K';
		Piece.whitecoords[7][5] = 'B';
		Piece.whitecoords[7][6] = 'H';
		Piece.whitecoords[7][7] = 'R';
		Piece.WhitePiece = 16;

		//initial rook sense
		xMovement(0,7,1);//bol of 1 signifies start of consecutive moves
		wait1Msec(1000);//pause time is just for consistent moving and sensing
		getColorRGB(S3,Piece.RookCol[0],Piece.RookCol[1],Piece.RookCol[2]);
		displayBigTextLine(1,"%d",Piece.RookCol[0]);
		displayBigTextLine(3,"%d",Piece.RookCol[1]);
		displayBigTextLine(5,"%d",Piece.RookCol[2]);
		wait1Msec(1000);

		//inital knight sense
		yMovement(0,1,1);
		wait1Msec(1000);
		getColorRGB(S3,Piece.KnightCol[0], Piece.KnightCol[1], Piece.KnightCol[2]);
		displayBigTextLine(1,"%d",Piece.KnightCol[0]);
		displayBigTextLine(3,"%d",Piece.KnightCol[1]);
		displayBigTextLine(5,"%d",Piece.KnightCol[2]);
		wait1Msec(1000);

		//initial bishop sense
		yMovement(1,2,0);
		wait1Msec(1000);
		getColorRGB(S3,Piece.BishopCol[0],Piece.BishopCol[1],Piece.BishopCol[2]);
		displayBigTextLine(1,"%d",Piece.BishopCol[0]);
		displayBigTextLine(3,"%d",Piece.BishopCol[1]);
		displayBigTextLine(5,"%d",Piece.BishopCol[2]);
		wait1Msec(1000);

		//initial queen sense
		yMovement(2,3,0);
		wait1Msec(1000);
		getColorRGB(S3,Piece.QueenCol[0],Piece.QueenCol[1],Piece.QueenCol[2]);
		displayBigTextLine(1,"%d",Piece.QueenCol[0]);
		displayBigTextLine(3,"%d",Piece.QueenCol[1]);
		displayBigTextLine(5,"%d",Piece.QueenCol[2]);
		wait1Msec(1000);
		//initial king sense
		yMovement(3,4,0);
		wait1Msec(1000);
		getColorRGB(S3,Piece.KingCol[0],Piece.KingCol[1],Piece.KingCol[2]);
		displayBigTextLine(1,"%d",Piece.KingCol[0]);
		displayBigTextLine(3,"%d",Piece.KingCol[1]);
		displayBigTextLine(5,"%d",Piece.KingCol[2]);
		wait1Msec(1000);
		//inital pawn sense
		xMovement(7,6,0);
		wait1Msec(1000);
		getColorRGB(S3,Piece.PawnCol[0],Piece.PawnCol[1],Piece.PawnCol[2]);
		displayBigTextLine(1,"%d",Piece.PawnCol[0]);
		displayBigTextLine(3,"%d",Piece.PawnCol[1]);
		displayBigTextLine(5,"%d",Piece.PawnCol[2]);
		wait1Msec(1000);

		//reset the arm back to zero
		xMovement(6,0,0);
		yMovement(4,0,0);
	}
	else
	{
		//this saves us an if statement to start with a bool of 1 for our movement on the very first x and y move
		nMotorEncoder[motorA] = 0;
		nMotorEncoder[motorD] = 0;
		int TotalPiece = 0;
		int EndY = 0, EndX = 0;
		for(int XValue = 0; XValue< BoardSize&&TotalPiece<Piece.WhitePiece; XValue++)
		{
			//moves the system up one in the x value
			if(XValue > 0)
			{
				xMovement(XValue-1, XValue,0);
			}
			for(int YValue = 0; YValue< BoardSize&&TotalPiece<Piece.WhitePiece; YValue++)
			{
				//this basically covers the snaking so if we end on the right end of the board we move to the left and vise versa
				if(YValue > 0)
				{
					if(XValue%2==0)
					{
						yMovement(YValue-1, YValue, 0);
					}
					else
					{
						yMovement(BoardSize-YValue,7-YValue, 0);
					}

				}
				//This deals with determining what the pieces are
				int Red = 0, Green = 0, Blue = 0;
				wait1Msec(300);
				getColorRGB(S3,Red,Green,Blue);
				if(isColor(2,Piece,Red,Green,Blue))
				{
					TotalPiece++;
					Piece.whitecoords[XValue][YValue] = 'R';
				}
				else if(isColor(1,Piece,Red,Green,Blue))
				{
					TotalPiece++;
					Piece.whitecoords[XValue][YValue] = 'P';
				}
				else if(isColor(3,Piece,Red,Green,Blue))
				{
					TotalPiece++;
					Piece.whitecoords[XValue][YValue] = 'H';
				}
				else if(isColor(4,Piece,Red,Green,Blue))
				{
					TotalPiece++;
					Piece.whitecoords[XValue][YValue] = 'B';
				}
				else if(isColor(6,Piece,Red,Green,Blue))
				{
					TotalPiece++;
					Piece.whitecoords[XValue][YValue] = 'Q';
				}
				else if(isColor(5,Piece,Red,Green,Blue))
				{
					TotalPiece++;
					Piece.whitecoords[XValue][YValue] = 'K';
				}
				else
				{
					//we set all non pieces to '  ' that way if a piece has moved we arent still storing its value
					Piece.whitecoords[XValue][YValue] = ' ';
				}
				//this just stores where the arm ended sensing
				if(TotalPiece == Piece.WhitePiece)
				{
					EndY = YValue;
				}
			}
			if(TotalPiece == Piece.WhitePiece)
			{
				EndX = XValue;
			}

		}
		//reset the claw back to zero from wherever it left off
		xMovement(EndX,0,0);
		yMovement(EndY,0,0);
	}
	//moves the claw back to the centre of the claw
	yMovement(ColourX/SquareSize,0,0);
	xMovement(ColourY/SquareSize,0,0);
}

void fileIO(ColourValue & Values,int & Difficulty, bool & StaleMate, bool & WhiteKing, bool & BlackKing, int & Attack,int & CurX, int & CurY, int & NewX, int & NewY)
{
	TFileHandle FOut;
	TFileHandle FIn;
	openWritePC(FOut,"aistuff.txt");

	writeLongPC(FOut,Difficulty);
	writeEndlPC(FOut);
	//write out the white boardstate to the file
	for(int XValue = 0; XValue<BoardSize;XValue++)
	{
		for(int YValue = 0;YValue<BoardSize;YValue++)
		{
			//this basically writes out a * for an empty location because it makes it easy for the c# code to read it in this way
			if(Values.whitecoords[XValue][YValue] == ' ')
			{
				writeCharPC(FOut,'*');
			}
			else
			{
				writeCharPC(FOut,Values.whitecoords[XValue][YValue]);
			}
		}
		//formatting of the file
		writeEndlPC(FOut);
	}

	closeFilePC(FOut);
	openReadPC(FIn,"chessmove.txt");
	//these valuesa are here due to there not being a proper read for bools
	//so we first read into integers and then convert them to bools
	int WhiteKingI = 0;
	int BlackKingI = 0;
	//we use a string of input because our first value is a bool so a 1 or a 0
	//so when we read itll be unable to tell if we have no value there at all or if the bool is just zero
	//works the same way if we had an integer and set it to -1
	string Input = "";
	//loops while the file is empty
	while(Input == "")
	{
		readTextPC(FIn, Input);
	}
	//displayString(1,"Success Exit"); testing code
	//converting our string to integer which can then be implicitly converted to bool
	StaleMate = atoi(Input);

	readIntPC(FIn,WhiteKingI);
	readIntPC(FIn,BlackKingI);
	readIntPC(FIn,Attack);
	readIntPC(FIn,CurX);
	readIntPC(FIn,CurY);
	readIntPC(FIn,NewX);
	readIntPC(FIn,NewY);
	closeFilePC(FIn);
	//empties the chessmove.txt
	openWritePC(FOut,"chessmove.txt");
	writeTextPC(FOut,"");
	closeFilePC(FOut);
	//empties the aistuff.txt
	openWritePC(FOut,"aistuff.txt");
	writeTextPC(FOut,"");
	closeFilePC(FOut);

	WhiteKing = WhiteKingI;
	BlackKing = BlackKingI;
}

task main()
{
	SensorType[S1] = sensorEV3_Touch;
	ColourValue CurrentStuff;
	//we dont actually need to setup our colour sensor because we use the getRGB values
	wait1Msec(50);
	const int Time = 15*60*1000;
	//they both start as 4 because the checkbuttonpress will return invalid as 4
	//as such we automatically account for invalid button presses
	int Timer = 4;
	int Difficulty  = 4;
	eraseDisplay();
	countdown();
	while(Timer == 4)
	{
		displayTextLine(1,"Enter the Timer Setting:");
		displayTextLine(2,"Up for no Limit");
		displayTextLine(3,"Left for 15 minute");
		displayTextLine(4,"Right for 30 minutes");
		Timer = checkButtonPress(false);
	}
	eraseDisplay();
	Timer *= Time;

	while (Difficulty == 4)
	{
		displayTextLine(2,"Enter the Difficulty");
		displayTextLine(3,"Left for beginner");
		displayTextLine(4,"Right for Regular");
		Difficulty = checkButtonPress(true);
	}
	eraseDisplay();
	countdown();
	eraseDisplay();
	//initial scan
	scanBoard(true, CurrentStuff);

	bool WhiteKing = true,BlackKing = true,StaleMate = false;
	int LastTime = 0;
	//main while loop, checks whether or not an end game state is reached
	while(WhiteKing&&BlackKing&&!StaleMate)
	{
		displayTextLine(1,"Please make your move");
		displayTextLine(3,"Tap touch sensor when done");
		wait1Msec(5000);
		eraseDisplay();
		countdown();
		time1[T1] = 0;
		eraseDisplay();
		//if our timer is set to infinite
		if(Timer == 0)
		{
			while(SensorValue[S1] == 0)
			{}
		}
		else
		{
			//we add on last time as that way we can start where the timer last left off
			while(SensorValue[S1]==0&&(time1[T1]+LastTime)<Timer)
			{
				displayBigTextLine(1,"%d minutes",((Timer-(time1[T1]+LastTime))/60000));//displaying exact number of minutes
				//seconds calculations
				displayBigTextLine(3,"%d seconds",( ( ( (Timer-(time1[T1]+LastTime) )/60000.0) - ( (Timer-(time1[T1]+LastTime) )/60000) ) *60));
			}
			//add on wherever the person ended their turn
			LastTime +=time1[T1];
		}
		eraseDisplay();
		//the greater then accounts for perhaps if it takes the program 1 milisecond to add to LastTime we handle that case
		//!=0 is because it will not take the person less than a milisecond to make a move and so we ignore all the infinite time setups
		if(LastTime>=Timer&&LastTime!=0)
		{
			//the player loses if their timer runs out so theres no point even doing the ai move
			WhiteKing = false;
		}
		else
		{
			//does a regular scan
			scanBoard(false,CurrentStuff);
			eraseDisplay();
			int Attack = 0, CurX = 0, CurY = 0, NewX = 0, NewY = 0;
			fileIO(CurrentStuff,Difficulty,StaleMate,WhiteKing,BlackKing,Attack,CurX,CurY,NewX,NewY);
			//the arm wont move the pieces out of the way so we make the user do so
			if(Attack==1)
			{
				displayTextLine(1,"Please remove pieces");
				displayTextLine(2,"At %d,%d",NewX,NewY);
				displayTextLine(3,"Then touch touch");
				while(SensorValue[S1]==0)
				{}
				//because attack = 1 we know a white piece will die so we subtract one from their total pieces
				CurrentStuff.WhitePiece--;
				eraseDisplay();
			}
			//make the move
			movement(CurX,CurY,NewX,NewY);
		}
	}
	//little end statements before the end of the code
	if(!WhiteKing)
	{
		displayBigTextLine(1,"You");
		displayBigTextLine(3,"Win");
		displayBigTextLine(5,":(");
	}
	else if(!BlackKing)
	{
		displayBigTextLine(1,"I");
		displayBigTextLine(3,"Win");
		displayBigTextLine(3,":P");
	}
	else
	{
		displayBigTextLine(1,"Tie :|");
		displayBigTextLine(3,"GG");
	}
}
