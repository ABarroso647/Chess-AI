/*
 Name: Adam Barroso
 Date: 10/29/18
 Program Name: Chess Game
 Purpose: To create a small represantation of chess using 2d arrays multiple methods and classes
 */
 //Enchancements all of them to a fairly high degree of success(hopefully)
using System;
using System.Collections.Generic;

namespace Chess
{
    class Program
    {
        //Declare my opjects so I can call upon my methods  in my classeslater
        public static Rook RookPiece = new Rook();
        public static Pawn PawnPiece = new Pawn();
        public static Knight KnightPiece = new Knight();
        public static Bishop BishopPiece = new Bishop();
        public static King KingPiece = new King();
        public static Queen QueenPiece = new Queen();
        public static bool PvcCheck = false;
        //Delcare many public variables that will be used all throughout the namespace "Chess"
        public static int SideCheck = 0;
        public static char CurrPiece = ' ';
        public static string currTeam = "white";
        public static int TotalPiece = 0;
        public static bool WhiteQueenAlive = true;
        public static bool BlackQueenAlive = true;
        public static int TieCounter = 0;
        public static int MovesNum = 0;
        public static bool AiTurn = false;
        static void Main(string[] args)
        {
            //This just tells the user what game this is and accepts only their choice of pvp or pvc
            Console.WriteLine("Welcome to Adam's Chess Game, type \"PVP\" for pvp, type \"PVC\" to play my computer");
            bool FirstLoop = true;
            while (FirstLoop)
            {
                string Response = Console.ReadLine();
                if (Response.ToUpper() == "PVP")
                {
                    Console.Clear();
                    Console.WriteLine("PVP selected");
                    FirstLoop = false;
                }
                else if (Response.ToUpper() == "PVC")
                {
                    Console.Clear();
                    Console.WriteLine("PVC selected");
                    ChessBoard.PvP = false;
                    FirstLoop = false;
                }
                else
                {
                    Console.WriteLine("Invalid entry, please try again");
                }
            }
            ChessBoard.combineArray();// This is used on my first run to combine my white team and black team arrays into one, this is used when I first start the program to generate my display array
            //initial methods that will be explained when there
            coordCheck();
            DrawBoard();
            kingLocation();
            SideCheck = 0;//Sidecheck is set to zero So that way any coord check I do will automatically set my currentpiece variable to the variable stored at that point in the array
            if (ChessBoard.PvP)//if pvp chosen
            {
                while (ChessBoard.WhiteKingAlive == true && ChessBoard.BlackKingAlive == true && ChessBoard.Tie == false)//This makes it so the turns loop if its not a tie, win, or loss
                {
                    //initialzes some variables to be used later including loop ones, and sets the team to white
                    int CurrentX = 0;
                    int CurrentY = 0;
                    int FutureX = 0;
                    int FutureY = 0;
                    bool mediumloopy = false;
                    bool bigloopy = false;
                    bool loopy = false;
                    currTeam = "white";


                    Console.WriteLine("Player One Start:");
                    while (bigloopy == false)
                    {
                        SideCheck = 0;
                        while (mediumloopy == false)
                        {

                            Console.Write("Enter X Co-ord of the piece you want to move: ");                                  //This litte block of code checks if the number the user entered for the quardinants is within the range of the board, and if its actually a number
                            while (loopy == false)
                            {

                                loopy = int.TryParse(Console.ReadLine(), out CurrentY);//X becomes y that way its easier for the user to input X,Y but it makes sense for the coder seeing it like a 2d array
                                if (loopy == false)
                                {
                                    Console.Write("The value entered is not a number please enter a number: ");
                                }
                                else if (CurrentY > 7 || CurrentY < 0)
                                {
                                    Console.Write("That x is outside the board, try a number  <=7 and >=0: ");
                                    loopy = false;
                                }

                            }
                            loopy = false;
                            Console.Write("Enter Y Co-ord of the piece you want to move: ");    //Any code blocks such as this for future reference just do what is stated above
                            while (loopy == false)
                            {

                                loopy = int.TryParse(Console.ReadLine(), out CurrentX);//y becomes x that way its easier for the user to input X,Y but it makes sense for the coder seeing it like a 2d array
                                if (loopy == false)
                                {
                                    Console.Write("The value entered is not a number please enter a number: ");
                                }
                                else if (CurrentX > 7 || CurrentX < 0)
                                {
                                    Console.Write("That Y is outside the board, try a number  <=7 and >=0: ");
                                    loopy = false;
                                }

                            }
                            loopy = false;
                            mediumloopy = PieceCheck(CurrentX, CurrentY);//Explained more in the method but this takes the points and checks if theres actually a piece there if theres not itll keep looping
                            if (!mediumloopy)
                            {
                                Console.WriteLine("There is no piece there for you");
                            }
                        }
                        mediumloopy = false;
                        //same as above but for coord that the user wants to move to
                        Console.Write("Enter X Co-ord of where you want to move: ");
                        while (loopy == false)
                        {

                            loopy = int.TryParse(Console.ReadLine(), out FutureY);
                            if (loopy == false)
                            {
                                Console.Write("The value entered is not a number please enter a number: ");
                            }
                            else if (FutureY > 7 || FutureY < 0)
                            {
                                Console.Write("That x is outside the board, try a number  <=7 and >=0: ");
                                loopy = false;
                            }

                        }
                        loopy = false;
                        Console.Write("Enter Y Co-ord of where you want to move: ");
                        while (loopy == false)
                        {

                            loopy = int.TryParse(Console.ReadLine(), out FutureX);
                            if (loopy == false)
                            {
                                Console.Write("The value entered is not a number please enter a number: ");
                            }
                            else if (FutureX > 7 || FutureX < 0)
                            {
                                Console.Write("That Y is outside the board, try a number  <=7 and >=0: ");
                                loopy = false;
                            }
                        }
                        if (MoveCheck(CurrentX, CurrentY, FutureX, FutureY))//This checks if the move is possible based on the given rules
                        {
                            Console.Clear();//Clears the board
                            BoardChanges(CurrentX, CurrentY, FutureX, FutureY);//Makes the changes in the arrrays
                            coordCheck();//Rechecks where all the coordinates are now
                            DrawBoard();//draws the new board
                            bigloopy = true;//then exits the loop

                        }
                        else
                        {
                            bigloopy = false;//if not possible move keep looping
                            Console.WriteLine("That was an invalid move please check your move and try again");
                        }
                        loopy = false;
                    }
                    if (ChessBoard.BlackKingAlive == false)//Checks if after white move, black king is dead, therefore meaning white won
                    {
                        Console.WriteLine("Player one has won");
                        break;
                    }
                    kingLocation();//calls upon this method

                    if (kingCheck(ChessBoard.BlackKingX, ChessBoard.BlackKingY))//calls upon this method using the black king location to see if the black king is in check
                    {
                        Console.WriteLine("Black King is currently in check, attempt to remove it from check or on your next turn you will lose");
                    }
                    else
                    {
                        currTeam = "black";
                        if (staleMate2Check())//checks for a stalemate after the turn, if the black king is not in check, and stalemate conditions are met
                        {

                            Console.WriteLine("This game is now a stalemate");
                            ChessBoard.Tie = true;
                            break;
                        }
                        currTeam = "white";
                        ChessBoard.JustChecking = false;//this is used so that way castling isnt accidently done whilst the user is checking moves
                    }
                    if (staleMate1Check())//checks if stalemate 1 parameters are true
                    {
                        Console.WriteLine("I predict this will end in a stalemate type 'y' if you accept, otherwise press any other key to continue");
                        if (Console.ReadKey().KeyChar.ToString().ToLower() == "y")//if the user enters y continue
                        {

                            Console.Write("\b");
                            Console.WriteLine("I declare this a tie");
                            ChessBoard.Tie = true;                          //call it a tie and break out of the loop
                            break;
                        }
                        else
                        {
                            Console.Write("\b");//if user doesnt enter y just remove their entered key and continue
                        }

                    }

                    currTeam = "black";//switch team to black

                    mediumloopy = false;//reset bools
                    bigloopy = false;
                    Console.WriteLine("Player two start:");
                    while (bigloopy == false)
                    {
                        SideCheck = 2;//this works so that way whatever black piece is chosen is stored as the current piece
                        while (mediumloopy == false)//Rest of code plays out the same as the white side but switched for the black side now
                        {

                            Console.Write("Enter X Co-ord of the piece you want to move: ");
                            while (loopy == false)
                            {

                                loopy = int.TryParse(Console.ReadLine(), out CurrentY);
                                if (loopy == false)
                                {
                                    Console.Write("The value entered is not a number please enter a number: ");
                                }
                                else if (CurrentY > 7 || CurrentY < 0)
                                {
                                    Console.Write("That x is outside the board, try a number  <=7 and >=0: ");
                                    loopy = false;
                                }

                            }
                            loopy = false;
                            Console.Write("Enter Y Co-ord of the piece you want to move: ");
                            while (loopy == false)
                            {

                                loopy = int.TryParse(Console.ReadLine(), out CurrentX);
                                if (loopy == false)
                                {
                                    Console.Write("The value entered is not a number please enter a number: ");
                                }
                                else if (CurrentX > 7 || CurrentX < 0)
                                {
                                    Console.Write("That Y is outside the board, try a number  <=7 and >=0: ");
                                    loopy = false;
                                }

                            }
                            loopy = false;
                            mediumloopy = PieceCheck(CurrentX, CurrentY);
                            if (!mediumloopy)
                            {
                                Console.WriteLine("There is no piece there for you");
                            }
                        }
                        mediumloopy = false;

                        Console.Write("Enter X Co-ord of where you want to move: ");
                        while (loopy == false)
                        {

                            loopy = int.TryParse(Console.ReadLine(), out FutureY);
                            if (loopy == false)
                            {
                                Console.Write("The value entered is not a number please enter a number: ");
                            }
                            else if (FutureY > 7 || FutureY < 0)
                            {
                                Console.Write("That x is outside the board, try a number  <=7 and >=0: ");
                                loopy = false;
                            }

                        }
                        loopy = false;
                        Console.Write("Enter Y Co-ord of where you want to move: ");
                        while (loopy == false)
                        {

                            loopy = int.TryParse(Console.ReadLine(), out FutureX);
                            if (loopy == false)
                            {
                                Console.Write("The value entered is not a number please enter a number: ");
                            }
                            else if (FutureX > 7 || FutureX < 0)
                            {
                                Console.Write("That Y is outside the board, try a number  <=7 and >=0: ");
                                loopy = false;
                            }
                        }
                        if (MoveCheck(CurrentX, CurrentY, FutureX, FutureY))
                        {
                            Console.Clear();
                            BoardChanges(CurrentX, CurrentY, FutureX, FutureY);
                            coordCheck();
                            DrawBoard();
                            bigloopy = true;

                        }
                        else
                        {
                            bigloopy = false;
                            Console.WriteLine("That was an invalid move please check your move and try again");
                        }
                        loopy = false;
                    }
                    if (ChessBoard.WhiteKingAlive == false)
                    {
                        Console.WriteLine("Player two has won");
                        break;
                    }
                    kingLocation();
                    if (kingCheck(ChessBoard.WhiteKingX, ChessBoard.WhiteKingY))
                    {
                        Console.WriteLine("White King is currently in check, attempt to remove it from check or on your next turn you will lose");
                    }
                    else
                    {
                        currTeam = "white";//I make it white, because the system checks if the white pieces can hit the black king, therefore I briefly set the team to white
                        if (staleMate2Check())
                        {
                            Console.WriteLine("This game is now a stalemate");
                            ChessBoard.Tie = true;
                            break;
                        }
                        currTeam = "black";//revert the team back to black
                        ChessBoard.JustChecking = false;
                    }
                    if (staleMate1Check())
                    {
                        Console.WriteLine("I predict this will end in a stalemate type 'y' if you accept, otherwise press any other key to continue");
                        if (Console.ReadKey().KeyChar.ToString().ToLower() == "y")
                        {

                            Console.Write("\b");
                            Console.WriteLine("I declare this a tie");
                            ChessBoard.Tie = true;
                            break;
                        }
                        else
                        {
                            Console.Write("\b");
                        }

                    }


                }

                Console.WriteLine("Press a key to Exit");//If ever I had a break, then it ends up here, and pressing a key causes the game to exit
                Console.ReadKey();

            }
            if (!ChessBoard.PvP)//If PVC is selected
            {
                while (ChessBoard.WhiteKingAlive == true && ChessBoard.BlackKingAlive == true && ChessBoard.Tie == false)
                {
                    //player one is the exact same
                    int CurrentX = 0;
                    int CurrentY = 0;
                    int FutureX = 0;
                    int FutureY = 0;
                    bool mediumloopy = false;
                    bool bigloopy = false;
                    bool loopy = false;
                    currTeam = "white";


                    Console.WriteLine("Player One Start:");
                    while (bigloopy == false)
                    {
                        SideCheck = 0;
                        while (mediumloopy == false)
                        {

                            Console.Write("Enter X Co-ord of the piece you want to move: ");
                            while (loopy == false)
                            {

                                loopy = int.TryParse(Console.ReadLine(), out CurrentY);
                                if (loopy == false)
                                {
                                    Console.Write("The value entered is not a number please enter a number: ");
                                }
                                else if (CurrentY > 7 || CurrentY < 0)
                                {
                                    Console.Write("That x is outside the board, try a number  <=7 and >=0: ");
                                    loopy = false;
                                }

                            }
                            loopy = false;
                            Console.Write("Enter Y Co-ord of the piece you want to move: ");
                            while (loopy == false)
                            {

                                loopy = int.TryParse(Console.ReadLine(), out CurrentX);
                                if (loopy == false)
                                {
                                    Console.Write("The value entered is not a number please enter a number: ");
                                }
                                else if (CurrentX > 7 || CurrentX < 0)
                                {
                                    Console.Write("That Y is outside the board, try a number  <=7 and >=0: ");
                                    loopy = false;
                                }

                            }
                            loopy = false;
                            mediumloopy = PieceCheck(CurrentX, CurrentY);
                            if (!mediumloopy)
                            {
                                Console.WriteLine("There is no piece there for you");
                            }
                        }
                        mediumloopy = false;

                        Console.Write("Enter X Co-ord of where you want to move: ");
                        while (loopy == false)
                        {

                            loopy = int.TryParse(Console.ReadLine(), out FutureY);
                            if (loopy == false)
                            {
                                Console.Write("The value entered is not a number please enter a number: ");
                            }
                            else if (FutureY > 7 || FutureY < 0)
                            {
                                Console.Write("That x is outside the board, try a number  <=7 and >=0: ");
                                loopy = false;
                            }

                        }
                        loopy = false;
                        Console.Write("Enter Y Co-ord of where you want to move: ");
                        while (loopy == false)
                        {

                            loopy = int.TryParse(Console.ReadLine(), out FutureX);
                            if (loopy == false)
                            {
                                Console.Write("The value entered is not a number please enter a number: ");
                            }
                            else if (FutureX > 7 || FutureX < 0)
                            {
                                Console.Write("That Y is outside the board, try a number  <=7 and >=0: ");
                                loopy = false;
                            }
                        }
                        if (MoveCheck(CurrentX, CurrentY, FutureX, FutureY))
                        {
                            Console.Clear();
                            BoardChanges(CurrentX, CurrentY, FutureX, FutureY);
                            coordCheck();
                            DrawBoard();
                            bigloopy = true;

                        }
                        else
                        {
                            bigloopy = false;
                            Console.WriteLine("That was an invalid move please check your move and try again");
                        }
                        loopy = false;
                    }
                    if (ChessBoard.BlackKingAlive == false)
                    {
                        Console.WriteLine("Player one has won");
                        break;
                    }
                    kingLocation();
                    if (!kingCheck(ChessBoard.BlackKingX, ChessBoard.BlackKingY))//slight difference since it makes no sense to display when the pc is in check we instead just use our stalemate check when its not in check
                    {
                        currTeam = "black";
                        if (staleMate2Check())
                        {
                            Console.WriteLine("This game is now a stalemate");
                            ChessBoard.Tie = true;
                            break;
                        }
                        currTeam = "white";
                        ChessBoard.JustChecking = false;
                    }
                    if (staleMate1Check())
                    {
                        Console.WriteLine("I predict this will end in a stalemate type 'y' if you accept, otherwise press any other key to continue");
                        if (Console.ReadKey().KeyChar.ToString().ToLower() == "y")
                        {

                            Console.Write("\b");
                            Console.WriteLine("I declare this a tie");
                            ChessBoard.Tie = true;
                            break;
                        }
                        else
                        {
                            Console.Write("\b");
                        }

                    }
                    currTeam = "black";
                    AiTurn = true;//This is used so that way if the ai pawn hits the end it automatically changes to queen, instead of waiting for user input
                    SideCheck = 2;
                    Random rnd = new Random();//gneerates a random
                    int[,] CurrentMove = computerMove();//calls up the 2d int method and calls upon all the possible x,y moves that could be made
                    int Selection = rnd.Next(0, (CurrentMove.GetLength(0)));//randomly chooses between all of them
                    Console.Clear();
                    CurrPiece = ChessBoard.Board[CurrentMove[Selection, 0], CurrentMove[Selection, 1]];//Sets the current piece to the char of the x,y chosen
                    if (CurrPiece == 'P' && CurrentMove[Selection, 2] == 7)//this automatically upgrades any pawns that reach the end to the queen
                    {
                        CurrPiece = 'Q';
                    }
                    BoardChanges(CurrentMove[Selection, 0], CurrentMove[Selection, 1], CurrentMove[Selection, 2], CurrentMove[Selection, 3]);//This performs the piece move on the arrays
                    coordCheck();//checks the new coords
                    DrawBoard();//draws the board
                    AiTurn = false;//reverted back to false now that hte move is over, this successfulyl stops the ai pawn from asking the user what they want to upgrade to
                    if (ChessBoard.WhiteKingAlive == false)//if white king is dead black has won
                    {
                        Console.WriteLine("Player two has won");
                        break;
                    }
                    kingLocation();//This end bit is the same as player one
                    if (kingCheck(ChessBoard.WhiteKingX, ChessBoard.WhiteKingY))
                    {
                        Console.WriteLine("White King is currently in check, attempt to remove it from check or on your next turn you will lose");
                    }
                    else
                    {
                        currTeam = "white";
                        if (staleMate2Check())
                        {
                            Console.WriteLine("This game is now a stalemate");
                            ChessBoard.Tie = true;
                            break;
                        }
                        currTeam = "black";
                        ChessBoard.JustChecking = false;
                    }
                    if (staleMate1Check())
                    {
                        Console.WriteLine("I predict this will end in a stalemate type 'y' if you accept, otherwise press any other key to continue");
                        if (Console.ReadKey().KeyChar.ToString().ToLower() == "y")
                        {

                            Console.Write("\b");
                            Console.WriteLine("I declare this a tie");
                            ChessBoard.Tie = true;
                            break;
                        }
                        else
                        {
                            Console.Write("\b");
                        }

                    }
                }
                Console.WriteLine("Press a key to Exit");
                Console.ReadKey();
            }
        }


        public static void DrawBoard()
        {
            //Board Design Credit goes to Coding Homework on youtube the linK to the video where I saw the design is https://www.youtube.com/watch?v=xVdNAVePXjo&t=70s 
            for (int i = 0; i < 8; i++)//This forloop just draws out my array and board
            {

                if (i == 0)
                {
                    Console.Write("     0   1   2   3   4   5   6   7");
                }

                Console.WriteLine("\n   +---+---+---+---+---+---+---+---+");
                for (int inc = 0; inc < 8; inc++)
                {
                    if (inc == 0)
                    {
                        Console.Write(i + "  |");
                    }
                    SideCheck = 3;
                    if (PieceCheck(i, inc))//checks if the piece is on the black coords, if so colour it red
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    SideCheck = 1;
                    if (PieceCheck(i, inc))//checks if the piece is on the whtie coord if so colour it blue
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }

                    Console.Write(" " + ChessBoard.Board[i, inc]);//writs the current element
                    Console.ResetColor();
                    Console.Write(" |");
                }
            }
            Console.WriteLine("\n   +---+---+---+---+---+---+---+---+");
        }




        public static void coordCheck()//This method runs through the two char 2d arrays and determines whats within them aswell as counting the total pieces
        {
            TotalPiece = 0;
            ChessBoard.WhiteKingAlive = false;
            ChessBoard.BlackKingAlive = false;
            WhiteQueenAlive = false;
            BlackQueenAlive = false;
            int ArrayCounter = 0;
            int[,] WhiteCoordTemp = new int[16, 2];
            int[,] BlackCoordTemp = new int[16, 2];
            int ThirdCounter = 0;//loops and sets the coords to a temp array
            for (int i = 0; i < 8; i++)
            {
                for (int inc = 0; inc < 8; inc++)
                {
                    if (ChessBoard.TeamWhite[i, inc] != ' ')//iif there is an actual chess piece there
                    {

                        WhiteCoordTemp[ThirdCounter, 0] = i;//add it to the temparray
                        WhiteCoordTemp[ThirdCounter, 1] = inc;
                        ThirdCounter++;
                        ArrayCounter++;//add to array counter and piece counter aswell as third array counter which works with the for loops
                        TotalPiece++;
                    }
                    if (ChessBoard.TeamWhite[i, inc] == 'K')
                    {
                        ChessBoard.WhiteKingAlive = true;//if king is in the array, king is allive
                    }
                    if (ChessBoard.TeamWhite[i, inc] == 'Q')
                    {
                        WhiteQueenAlive = true;//if queen is in the array, queen is alive
                    }
                }
            }
            ChessBoard.WhiteCoords = new int[ArrayCounter, 2];//based on the number of pieces generate my coord array copying the temporary array to my actual one after
            for (int i = 0; i < ArrayCounter; i++)
            {
                for (int inc = 0; inc < 2; inc++)
                {
                    ChessBoard.WhiteCoords[i, inc] = WhiteCoordTemp[i, inc];
                }
            }
            //does the same thing as the white array, but for the black one
            ThirdCounter = 0;
            ArrayCounter = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int inc = 0; inc < 8; inc++)
                {
                    if (ChessBoard.TeamBlack[i, inc] != ' ')
                    {

                        BlackCoordTemp[ThirdCounter, 0] = i;
                        BlackCoordTemp[ThirdCounter, 1] = inc;
                        ThirdCounter++;
                        ArrayCounter++;
                        TotalPiece++;
                    }
                    if (ChessBoard.TeamBlack[i, inc] == 'K')
                    {
                        ChessBoard.BlackKingAlive = true;
                    }
                    if (ChessBoard.TeamBlack[i, inc] == 'Q')
                    {
                        BlackQueenAlive = true;
                    }
                }
            }
            ChessBoard.BlackCoords = new int[ArrayCounter, 2];
            for (int i = 0; i < ArrayCounter; i++)
            {
                for (int inc = 0; inc < 2; inc++)
                {
                    ChessBoard.BlackCoords[i, inc] = BlackCoordTemp[i, inc];
                }
            }
        }
        public static bool PieceCheck(int x, int y)
        {
            bool PieceThere = false;
            //if turntype= white this checks if
            if (SideCheck == 0 || SideCheck == 1 || SideCheck == 4)//if sidecheck is sed to 0/1/4 then determine what piece is thee
            {
                for (int i = 0; i < ChessBoard.WhiteCoords.GetLength(0); i++)
                {
                    for (int inc = 0; inc < 2; inc++)
                    {
                        if (ChessBoard.WhiteCoords[i, 0] == x && ChessBoard.WhiteCoords[i, 1] == y)
                        {
                            if (SideCheck == 0)
                            {
                                CurrPiece = ChessBoard.TeamWhite[x, y];//if sidecheck is zero, make that location on the array your current piece
                            }
                            PieceThere = true;
                        }
                    }
                }
            }
            if (SideCheck == 2 || SideCheck == 3 || SideCheck == 4)//same as the earlier one but for 2,3,4
            {
                for (int i = 0; i < ChessBoard.BlackCoords.GetLength(0); i++)
                {
                    for (int inc = 0; inc < 2; inc++)
                    {
                        if (ChessBoard.BlackCoords[i, 0] == x && ChessBoard.BlackCoords[i, 1] == y)
                        {
                            if (SideCheck == 2)
                            {
                                CurrPiece = ChessBoard.TeamBlack[x, y];
                            }
                            PieceThere = true;
                        }
                    }
                }
            }
            if (PieceThere)//if the piecethere bool was present return true, or else return false, this was used so that way it always checks booth coords
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public static void BoardChanges(int x, int y, int NewX, int NewY)//if any array changes are occuring then it calls this method to do them
        {
            if (currTeam == "white")
            {
                if (CurrPiece == 'K')//if the king is the one thats going to move, set his move to true, that way he can no longer castle
                {
                    ChessBoard.WhiteKingMove = true;
                }

                ChessBoard.TeamWhite[x, y] = ' ';//set the old spot on the array to null
                ChessBoard.TeamWhite[NewX, NewY] = CurrPiece;//set the new spot to whatever the char is
                ChessBoard.Board[x, y] = ' ';//do the same for board
                ChessBoard.Board[NewX, NewY] = CurrPiece;
                ChessBoard.TeamBlack[NewX, NewY] = ' ';//if an enemy was there, theyre now gone
            }
            else if (currTeam == "black")//same as white but for black
            {
                if (CurrPiece == 'K')
                {
                    ChessBoard.BlackKingMove = true;
                }


                ChessBoard.TeamBlack[x, y] = ' ';
                ChessBoard.TeamBlack[NewX, NewY] = CurrPiece;
                ChessBoard.Board[x, y] = ' ';
                ChessBoard.Board[NewX, NewY] = CurrPiece;
                ChessBoard.TeamWhite[NewX, NewY] = ' ';
            }


        }

        public static bool MoveCheck(int x, int y, int NewX, int NewY)//This checks if the move is possible based on the rules
        {
            ChessBoard.Attack = false;
            bool white = false;
            if (currTeam == "white")//if team is white, set bool to that, and check if theres a white piece at the new spot, if so the move is impossible, if theres not, and theres instead a enemy piece, set attack to true
            {
                white = true;
                SideCheck = 1;
                if (PieceCheck(NewX, NewY))
                {
                    return false;
                }
                SideCheck = 3;

                if (PieceCheck(NewX, NewY))
                {
                    ChessBoard.Attack = true;
                }
            }
            else if (currTeam == "black")//same as white but for black, so the bool is oposite etc
            {
                white = false;
                SideCheck = 3;
                if (PieceCheck(NewX, NewY))
                {
                    return false;
                }
                SideCheck = 1;
                if (PieceCheck(NewX, NewY))
                {
                    ChessBoard.Attack = true;
                }
            }

            if (CurrPiece == KingPiece.Piece)//if the piece is king
            {
                if(CastleCheck(x,y,NewX,NewY))
                {
                    ChessBoard.CastleCheck = true;
                }
                else
                {
                    ChessBoard.CastleCheck = false;
                }
                if (KingPiece.canMove(x, y, NewX, NewY, white))//check if the kign can move due to the rules, this is within the Kinpiece class
                {
                    if (white)//if its white, make sure the king isnt moving into attack range of the black king
                    {
                        if (KingPiece.canMove(ChessBoard.BlackKingX, ChessBoard.BlackKingY, NewX, NewY, white))
                        {
                            return false;
                        }
                        else
                        {

                            return true;
                        }
                    }
                    else//vise versas if black
                    {
                        if (KingPiece.canMove(ChessBoard.WhiteKingX, ChessBoard.WhiteKingY, NewX, NewY, white))
                        {
                            return false;
                        }
                        else
                        {

                            return true;
                        }
                    }
                }
                else//if move is mathematically impossible return false
                {
                    return false;
                }
            }
            if (CurrPiece == QueenPiece.Piece)
            {
                if (QueenPiece.canMove(x, y, NewX, NewY, white))//if queen cna move based on rules in class, return true
                {

                    return true;
                }
                else
                {
                    return false;
                }

            }
            if (CurrPiece == RookPiece.Piece)
            {
                if (RookPiece.canMove(x, y, NewX, NewY, white))//if rook can move based on rules in class return true
                {
                    if (white && x == 7 && y == 0 && ChessBoard.WhiteLRookMove == false)//depending on if this is the rooks first move and where it is, set its corresponding move bool to true, so it can no longer castle
                    {
                        ChessBoard.WhiteLRookMove = true;
                    }
                    else if (white && x == 7 && y == 7 && ChessBoard.WhiteRRookMove == false)
                    {
                        ChessBoard.WhiteRRookMove = true;
                    }
                    else if (!white && x == 0 && y == 0 && ChessBoard.BlackLRookMove == false)
                    {
                        ChessBoard.BlackLRookMove = true;
                    }
                    else if (!white && x == 0 && y == 7 && ChessBoard.BlackRRookMove == false)
                    {
                        ChessBoard.BlackLRookMove = true;
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (CurrPiece == BishopPiece.Piece)//same as queen but for bishop
            {
                if (BishopPiece.canMove(x, y, NewX, NewY, white))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (CurrPiece == PawnPiece.Piece)
            {
                if (PawnPiece.canMove(x, y, NewX, NewY, white))//checks mathematically if move is possible
                {
                    if (!ChessBoard.JustChecking&&(((white && NewX == 0) || (!white && NewX == 7)) && !AiTurn))//if it is  has reached the other end of the board and it is not the ai checking, then allow the user to select which piece they wnat ot upgrade it to changing the curpiece to that char
                    {
                        bool loopy = false;
                        while (loopy == false)
                        {
                            Console.WriteLine("You can upgrade your pawn congratulations, enter what piece you want to upgrade it to:");
                            string UpgradeName = Console.ReadLine().ToLower();
                            if (UpgradeName == "queen")
                            {
                                CurrPiece = 'Q';
                                loopy = true;
                            }
                            else if (UpgradeName == "bishop")
                            {
                                CurrPiece = 'B';
                                loopy = true;
                            }
                            else if (UpgradeName == "rook")
                            {
                                CurrPiece = 'R';
                                loopy = true;
                            }
                            else if (UpgradeName == "knight")
                            {
                                CurrPiece = 'H';
                                loopy = true;
                            }
                            else
                            {
                                Console.WriteLine("You entered an invalid piece type");// I didnt put pawn as an option because theres no point in chess when someone would want to upgrade the pawn to a pawn, it holds no vlaue
                            }
                        }
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (CurrPiece == KnightPiece.Piece)//same as queen but for knight
            {
                if (KnightPiece.canMove(x, y, NewX, NewY, white))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        public static bool staleMate1Check()//THis checks if one form of stalemate is true, needless moving
        {
            if (ChessBoard.Attack)//if ever there is an attack, return the stalemate counter back to 0
            {
                TieCounter = 0;
            }
            if (TotalPiece <= 14 && WhiteQueenAlive == false && BlackQueenAlive == false && TieCounter == 6)//if stalemate counter is 6, there is less than 14 pieces left, and no queens for either team return true
            {
                return true;
            }
            else if (TotalPiece <= 14 && WhiteQueenAlive == false && BlackQueenAlive == false)//same parameters above, but if the counter isnt there yet add one to it and return false
            {
                TieCounter++;
                return false;
            }
            else
            {
                return false;
            }
        }
        public static bool staleMate2Check()//this checks if the second type of stalemate is true
        {


            ChessBoard.JustChecking = true;//sets justchecking to true, so a castle is impossible while checking
            if (currTeam == "white")
            {

                for (int i = 0; i < ChessBoard.WhiteCoords.GetLength(0); i++)//loop through all the white coords
                {
                    //set variables that will be used
                    char OldChar = ' ';
                    int CurrentX = ChessBoard.WhiteCoords[i, 0];//set the current x to the value of the current white coord,0
                    int CurrentY = ChessBoard.WhiteCoords[i, 1];
                    char CurrentPieceType = ChessBoard.TeamWhite[CurrentX, CurrentY];//grabs the piece at that coord
                    for (int inc = 0; inc < 8; inc++)//loop through all posisble spaces on the baord
                    {
                        for (int incr = 0; incr < 8; incr++)
                        {
                            if (CurrentPieceType == 'Q')//if it is queen, check if the queen can move to the given inc,incr on the board, then, set the team to black so that way the check method checks if the black team can get the whtie kign, then set the new x and y to the current x and y char, store the new char, and call upon check to see if this move puts the white king into check, if it does keep going if it doesnt return false as there is a possible move to make and so it is not a stalemate
                            {                           //then after that reset the pieces in the array back to what they were
                                CurrPiece = 'Q';
                                if (MoveCheck(CurrentX, CurrentY, inc, incr))
                                {

                                    currTeam = "black";
                                    OldChar = ChessBoard.Board[inc, incr];
                                    ChessBoard.Board[inc, incr] = ChessBoard.Board[CurrentX, CurrentY];
                                    ChessBoard.Board[CurrentX, CurrentY] = ' ';
                                    if (!kingCheck(ChessBoard.WhiteKingX, ChessBoard.WhiteKingY))
                                    {
                                        currTeam = "white";
                                        ChessBoard.Board[CurrentX, CurrentY] = ChessBoard.Board[inc, incr];
                                        ChessBoard.Board[inc, incr] = OldChar;
                                        return false;
                                    }
                                    else
                                    {
                                        currTeam = "white";
                                        ChessBoard.Board[CurrentX, CurrentY] = ChessBoard.Board[inc, incr];
                                        ChessBoard.Board[inc, incr] = OldChar;
                                    }

                                }
                            }
                            else if (CurrentPieceType == 'R')//same as queen but for rook
                            {
                                CurrPiece = 'R';
                                if (MoveCheck(CurrentX, CurrentY, inc, incr))
                                {
                                    currTeam = "black";
                                    OldChar = ChessBoard.Board[inc, incr];
                                    ChessBoard.Board[inc, incr] = ChessBoard.Board[CurrentX, CurrentY];
                                    ChessBoard.Board[CurrentX, CurrentY] = ' ';
                                    if (!kingCheck(ChessBoard.WhiteKingX, ChessBoard.WhiteKingY))
                                    {
                                        currTeam = "white";
                                        ChessBoard.Board[CurrentX, CurrentY] = ChessBoard.Board[inc, incr];
                                        ChessBoard.Board[inc, incr] = OldChar;
                                        return false;
                                    }
                                    else
                                    {
                                        currTeam = "white";
                                        ChessBoard.Board[CurrentX, CurrentY] = ChessBoard.Board[inc, incr];
                                        ChessBoard.Board[inc, incr] = OldChar;
                                    }

                                }
                            }
                            else if (CurrentPieceType == 'B')//same as queen but for bishop
                            {
                                CurrPiece = 'B';
                                if (MoveCheck(CurrentX, CurrentY, inc, incr))
                                {
                                    currTeam = "black";
                                    OldChar = ChessBoard.Board[inc, incr];
                                    ChessBoard.Board[inc, incr] = ChessBoard.Board[CurrentX, CurrentY];
                                    ChessBoard.Board[CurrentX, CurrentY] = ' ';
                                    if (!kingCheck(ChessBoard.WhiteKingX, ChessBoard.WhiteKingY))
                                    {
                                        currTeam = "white";
                                        ChessBoard.Board[CurrentX, CurrentY] = ChessBoard.Board[inc, incr];
                                        ChessBoard.Board[inc, incr] = OldChar;
                                        return false;
                                    }
                                    else
                                    {
                                        currTeam = "white";
                                        ChessBoard.Board[CurrentX, CurrentY] = ChessBoard.Board[inc, incr];
                                        ChessBoard.Board[inc, incr] = OldChar;
                                    }

                                }
                            }
                            else if (CurrentPieceType == 'H')//same as queen but for horse or knight
                            {
                                CurrPiece = 'H';
                                if (MoveCheck(CurrentX, CurrentY, inc, incr))
                                {
                                    OldChar = ChessBoard.Board[inc, incr];
                                    currTeam = "black";
                                    ChessBoard.Board[inc, incr] = ChessBoard.Board[CurrentX, CurrentY];
                                    ChessBoard.Board[CurrentX, CurrentY] = ' ';
                                    if (!kingCheck(ChessBoard.WhiteKingX, ChessBoard.WhiteKingY))
                                    {
                                        currTeam = "white";
                                        ChessBoard.Board[CurrentX, CurrentY] = ChessBoard.Board[inc, incr];
                                        ChessBoard.Board[inc, incr] = OldChar;
                                        return false;
                                    }
                                    else
                                    {
                                        currTeam = "white";
                                        ChessBoard.Board[CurrentX, CurrentY] = ChessBoard.Board[inc, incr];
                                        ChessBoard.Board[inc, incr] = OldChar;
                                    }

                                }
                            }
                            else if (CurrentPieceType == 'P')//same as queen but for pawn
                            {
                                CurrPiece = 'P';
                                if (MoveCheck(CurrentX, CurrentY, inc, incr))
                                {
                                    currTeam = "black";
                                    OldChar = ChessBoard.Board[inc, incr];
                                    ChessBoard.Board[inc, incr] = ChessBoard.Board[CurrentX, CurrentY];
                                    ChessBoard.Board[CurrentX, CurrentY] = ' ';
                                    if (!kingCheck(ChessBoard.WhiteKingX, ChessBoard.WhiteKingY))
                                    {
                                        currTeam = "white";
                                        ChessBoard.Board[CurrentX, CurrentY] = ChessBoard.Board[inc, incr];
                                        ChessBoard.Board[inc, incr] = OldChar;
                                        return false;
                                    }
                                    else
                                    {
                                        currTeam = "white";
                                        ChessBoard.Board[CurrentX, CurrentY] = ChessBoard.Board[inc, incr];
                                        ChessBoard.Board[inc, incr] = OldChar;
                                    }

                                }
                                CurrPiece = ' ';//I added this in because pawn was giving issues removing itself from current piece


                            }//same as the others except instead of seeing if moving another piece will hit the kign, change the king location and see if the king is in check based on the new king location
                            else if (CurrentPieceType == 'K')
                            {
                                CurrPiece = 'K';
                                if (MoveCheck(CurrentX, CurrentY, inc, incr))
                                {
                                    currTeam = "black";
                                    OldChar = ChessBoard.Board[inc, incr];
                                    ChessBoard.Board[inc, incr] = ChessBoard.Board[CurrentX, CurrentY];
                                    ChessBoard.Board[CurrentX, CurrentY] = ' ';
                                    kingLocation();
                                    if (!kingCheck(inc, incr))
                                    {

                                        currTeam = "white";
                                        ChessBoard.Board[CurrentX, CurrentY] = ChessBoard.Board[inc, incr];
                                        ChessBoard.Board[inc, incr] = OldChar;
                                        kingLocation();
                                        return false;
                                    }
                                    else
                                    {

                                        currTeam = "white";
                                        ChessBoard.Board[CurrentX, CurrentY] = ChessBoard.Board[inc, incr];
                                        ChessBoard.Board[inc, incr] = OldChar;
                                        kingLocation();
                                    }
                                }
                            }

                        }

                    }

                }
            }
            //same as white but for black now
            else if (currTeam == "black")
            {
                for (int i = 0; i < ChessBoard.BlackCoords.GetLength(0); i++)
                {
                    char OldChar = ' ';
                    int CurrentX = ChessBoard.BlackCoords[i, 0];
                    int CurrentY = ChessBoard.BlackCoords[i, 1];
                    char CurrentPieceType = ChessBoard.TeamBlack[CurrentX, CurrentY];
                    for (int inc = 0; inc < 8; inc++)
                    {
                        for (int incr = 0; incr < 8; incr++)
                        {

                            if (CurrentPieceType == 'Q')
                            {
                                CurrPiece = 'Q';
                                if (MoveCheck(CurrentX, CurrentY, inc, incr))
                                {

                                    currTeam = "white";
                                    OldChar = ChessBoard.Board[inc, incr];
                                    ChessBoard.Board[inc, incr] = ChessBoard.Board[CurrentX, CurrentY];
                                    ChessBoard.Board[CurrentX, CurrentY] = ' ';
                                    if (!kingCheck(ChessBoard.BlackKingX, ChessBoard.BlackKingY))
                                    {
                                        currTeam = "black";
                                        ChessBoard.Board[CurrentX, CurrentY] = ChessBoard.Board[inc, incr];
                                        ChessBoard.Board[inc, incr] = OldChar;
                                        return false;
                                    }
                                    else
                                    {
                                        currTeam = "black";
                                        ChessBoard.Board[CurrentX, CurrentY] = ChessBoard.Board[inc, incr];
                                        ChessBoard.Board[inc, incr] = OldChar;
                                    }

                                }
                            }
                            else if (CurrentPieceType == 'R')
                            {
                                CurrPiece = 'R';
                                if (MoveCheck(CurrentX, CurrentY, inc, incr))
                                {
                                    currTeam = "white";
                                    OldChar = ChessBoard.Board[inc, incr];
                                    ChessBoard.Board[inc, incr] = ChessBoard.Board[CurrentX, CurrentY];
                                    ChessBoard.Board[CurrentX, CurrentY] = ' ';
                                    if (!kingCheck(ChessBoard.BlackKingX, ChessBoard.BlackKingY))
                                    {
                                        currTeam = "black";
                                        ChessBoard.Board[CurrentX, CurrentY] = ChessBoard.Board[inc, incr];
                                        ChessBoard.Board[inc, incr] = OldChar;
                                        return false;
                                    }
                                    else
                                    {
                                        currTeam = "black";
                                        ChessBoard.Board[CurrentX, CurrentY] = ChessBoard.Board[inc, incr];
                                        ChessBoard.Board[inc, incr] = OldChar;
                                    }

                                }
                            }
                            else if (CurrentPieceType == 'B')
                            {
                                CurrPiece = 'B';
                                if ((MoveCheck(CurrentX, CurrentY, inc, incr)))
                                {
                                    currTeam = "white";
                                    OldChar = ChessBoard.Board[inc, incr];
                                    ChessBoard.Board[inc, incr] = ChessBoard.Board[CurrentX, CurrentY];
                                    ChessBoard.Board[CurrentX, CurrentY] = ' ';
                                    if (!kingCheck(ChessBoard.BlackKingX, ChessBoard.BlackKingY))
                                    {
                                        currTeam = "black";
                                        ChessBoard.Board[CurrentX, CurrentY] = ChessBoard.Board[inc, incr];
                                        ChessBoard.Board[inc, incr] = OldChar;
                                        return false;
                                    }
                                    else
                                    {
                                        currTeam = "black";
                                        ChessBoard.Board[CurrentX, CurrentY] = ChessBoard.Board[inc, incr];
                                        ChessBoard.Board[inc, incr] = OldChar;
                                    }

                                }
                            }
                            else if (CurrentPieceType == 'H')
                            {
                                CurrPiece = 'H';
                                if (MoveCheck(CurrentX, CurrentY, inc, incr))
                                {
                                    OldChar = ChessBoard.Board[inc, incr];
                                    currTeam = "white";
                                    ChessBoard.Board[inc, incr] = ChessBoard.Board[CurrentX, CurrentY];
                                    ChessBoard.Board[CurrentX, CurrentY] = ' ';
                                    if (!kingCheck(ChessBoard.BlackKingX, ChessBoard.BlackKingY))
                                    {
                                        currTeam = "black";
                                        ChessBoard.Board[CurrentX, CurrentY] = ChessBoard.Board[inc, incr];
                                        ChessBoard.Board[inc, incr] = OldChar;
                                        return false;
                                    }
                                    else
                                    {
                                        currTeam = "black";
                                        ChessBoard.Board[CurrentX, CurrentY] = ChessBoard.Board[inc, incr];
                                        ChessBoard.Board[inc, incr] = OldChar;
                                    }

                                }
                            }
                            else if (CurrentPieceType == 'P')
                            {
                                CurrPiece = 'P';
                                if (MoveCheck(CurrentX, CurrentY, inc, incr))
                                {
                                    currTeam = "white";
                                    OldChar = ChessBoard.Board[inc, incr];
                                    ChessBoard.Board[inc, incr] = ChessBoard.Board[CurrentX, CurrentY];
                                    ChessBoard.Board[CurrentX, CurrentY] = ' ';
                                    if (!kingCheck(ChessBoard.BlackKingX, ChessBoard.BlackKingY))
                                    {
                                        currTeam = "black";
                                        ChessBoard.Board[CurrentX, CurrentY] = ChessBoard.Board[inc, incr];
                                        ChessBoard.Board[inc, incr] = OldChar;
                                        return false;
                                    }
                                    else
                                    {
                                        currTeam = "black";
                                        ChessBoard.Board[CurrentX, CurrentY] = ChessBoard.Board[inc, incr];
                                        ChessBoard.Board[inc, incr] = OldChar;
                                    }

                                }


                            }
                            else if (CurrentPieceType == 'K')
                            {
                                CurrPiece = 'K';
                                if (MoveCheck(CurrentX, CurrentY, inc, incr))
                                {
                                    currTeam = "white";
                                    OldChar = ChessBoard.Board[inc, incr];
                                    ChessBoard.Board[inc, incr] = ChessBoard.Board[CurrentX, CurrentY];
                                    ChessBoard.Board[CurrentX, CurrentY] = ' ';
                                    kingLocation();
                                    if (!kingCheck(inc, incr))
                                    {

                                        currTeam = "black";
                                        ChessBoard.Board[CurrentX, CurrentY] = ChessBoard.Board[inc, incr];
                                        ChessBoard.Board[inc, incr] = OldChar;
                                        kingLocation();
                                        return false;
                                    }
                                    else
                                    {

                                        currTeam = "black";
                                        ChessBoard.Board[CurrentX, CurrentY] = ChessBoard.Board[inc, incr];
                                        ChessBoard.Board[inc, incr] = OldChar;
                                        kingLocation();
                                    }
                                }
                            }

                        }

                    }

                }





            }
            return true;
        }

        public static bool kingCheck(int KingX, int KingY)
        {//determine what team is checking
            bool white = false;
            if (currTeam == "white")
            {
                white = true;
            }
            if (currTeam == "white")//if trying to see if white pieces cna hit blakc kign
            {
                for (int i = 0; i < ChessBoard.WhiteCoords.GetLength(0); i++)//sets the loop sfor the length of the  coords int eh array
                {
                    int CurrentX = ChessBoard.WhiteCoords[i, 0];
                    int CurrentY = ChessBoard.WhiteCoords[i, 1];
                    if (ChessBoard.Board[CurrentX, CurrentY] == 'Q')//if the current coord in the loop is ueen check if the queen cna move to hit the black kig
                    { 
                        if (QueenPiece.canMove(CurrentX, CurrentY, KingX, KingY,white))
                        {
                            return true;
                        }
                       
                    }
                    else if (ChessBoard.Board[CurrentX, CurrentY] == 'B')//same as queen bbut for bishop
                    {
                        if (BishopPiece.canMove(CurrentX, CurrentY, KingX, KingY, white))
                        {
                            return true;
                        }
                    }
                    else if (ChessBoard.Board[CurrentX, CurrentY] == 'R')
                    {
                        if (RookPiece.canMove(CurrentX, CurrentY, KingX, KingY, white))
                        {
                            return true;
                        }
                    }
                    else if (ChessBoard.Board[CurrentX, CurrentY] == 'H')
                    {
                        if (KnightPiece.canMove(CurrentX, CurrentY, KingX, KingY, white))
                        {
                            return true;
                        }

                    }
                    else if (ChessBoard.Board[CurrentX, CurrentY] == 'P')
                    {
                        if (PawnPiece.diagonalMove(CurrentX, CurrentY, KingX, KingY, white))
                        {
                            return true;
                        }

                    }
                    else if (ChessBoard.Board[CurrentX,CurrentY]=='K')
                    {
                        if(KingPiece.canMove(CurrentX,CurrentY,KingX,KingY,white))
                        {
                            return true;
                        }
                    }

                }
                return false;
            }
            //same as white but in the case of black now triyng to attack white king
            else if (currTeam == "black")
            {
                for (int i = 0; i < ChessBoard.BlackCoords.GetLength(0); i++)
                {
                    int CurrentX = ChessBoard.BlackCoords[i, 0];
                    int CurrentY = ChessBoard.BlackCoords[i, 1];
                    if (ChessBoard.Board[CurrentX, CurrentY] == 'Q')
                    {
                        if (QueenPiece.canMove(CurrentX, CurrentY, KingX, KingY, white))
                        {
                            return true;
                        }

                    }
                    else if (ChessBoard.Board[CurrentX, CurrentY] == 'B')
                    {
                        if (BishopPiece.canMove(CurrentX, CurrentY, KingX, KingY, white))
                        {
                            return true;
                        }
                    }
                    else if (ChessBoard.Board[CurrentX, CurrentY] == 'R')
                    {
                        if (RookPiece.canMove(CurrentX, CurrentY, KingX, KingY, white))
                        {
                            return true;
                        }
                    }
                    else if (ChessBoard.Board[CurrentX, CurrentY] == 'H')
                    {
                        if (KnightPiece.canMove(CurrentX, CurrentY, KingX, KingY, white))
                        {
                            return true;
                        }

                    }
                    else if (ChessBoard.Board[CurrentX, CurrentY] == 'P')
                    {
                        if (PawnPiece.diagonalMove(CurrentX, CurrentY, KingX, KingY, white))
                        {
                            return true;
                        }

                    }
                    else if (ChessBoard.Board[CurrentX, CurrentY] == 'K')
                    {
                        if (KingPiece.canMove(CurrentX, CurrentY, KingX, KingY, white))
                        {
                            return true;
                        }
                    }


                }
                return false;
            }
            else
            {

                return false;
            }

        }
        public static void kingLocation()//This loops through the coord array and determines where both kings are located
        {
            for (int i = 0; i < ChessBoard.WhiteCoords.GetLength(0); i++)
            {
                if (ChessBoard.Board[ChessBoard.WhiteCoords[i, 0], ChessBoard.WhiteCoords[i, 1]] == 'K')
                {
                    ChessBoard.WhiteKingX = ChessBoard.WhiteCoords[i, 0];
                    ChessBoard.WhiteKingY = ChessBoard.WhiteCoords[i, 1];
                }

            }
            for (int i = 0; i < ChessBoard.BlackCoords.GetLength(0); i++)
            {
                if (ChessBoard.Board[ChessBoard.BlackCoords[i, 0], ChessBoard.BlackCoords[i, 1]] == 'K')
                {
                    ChessBoard.BlackKingX = ChessBoard.BlackCoords[i, 0];
                    ChessBoard.BlackKingY = ChessBoard.BlackCoords[i, 1];
                }

            }
        }
        public static int[,] computerMove()//this is used to determine where the computer will move
        {
            MovesNum = 0;//sets number of moves in the bestmove array to 0
            int[,] BestMove = new int[1, 4];
            int TurnPoint = -1000000;//starts at negative one so a move
            for (int i = 0; i < ChessBoard.BlackCoords.GetLength(0); i++)//loops through all the black coord and sets x to the currne tblack coord
            {
                int CurrentX = ChessBoard.BlackCoords[i, 0];
                int CurrentY = ChessBoard.BlackCoords[i, 1];
                char CurrentPieceType = ChessBoard.TeamBlack[CurrentX, CurrentY];
                for (int inc = 0; inc < 8; inc++)
                {
                    for (int incr = 0; incr < 8; incr++)//loops through all the possible spaces on the board for the black pieces to move to 
                    {
                        if (CurrentPieceType == 'Q')//if it is queen, set the current piece to queen and check if the move cna be made to the new x location as defined by inc, incr
                        {
                            CurrPiece = 'Q';
                            if (MoveCheck(CurrentX, CurrentY, inc, incr))
                            {
                                    if ((pointGain(inc, incr) - pointLoss(CurrentX, CurrentY, inc, incr)) > TurnPoint)//checks to see if the delta points gained by this turn is greater than the current highest delta point gain by a move
                                    {
                                        MovesNum = 0;//if the move will you more points than the current highest point, then reset the moves in the array to zero and reset the moves array to empty
                                        BestMove = new int[1, 4];
                                    }
                                    if ((pointGain(inc, incr) - pointLoss(CurrentX, CurrentY, inc, incr)) >= TurnPoint)// if the points is greater or equal to the current bar,
                                    {
                                        TurnPoint = pointGain(inc, incr)-pointLoss(CurrentX, CurrentY, inc, incr);//set the new bar to points gained- the points lost for the move that was made
                                        BestMove[MovesNum, 0] = CurrentX;//add the orig x and y and the move to the move array
                                        BestMove[MovesNum, 1] = CurrentY;
                                        BestMove[MovesNum, 2] = inc;
                                        BestMove[MovesNum, 3] = incr;
                                        MovesNum++;//add one to the move array counter

                                        BestMove = arrayResize(BestMove);//calll upon the resize method to resize the array one bigger

                                    }
                            }
                            else
                            {
                                CurrPiece = ' ';//reset current piece when done
                            }
                        }
                        else if (CurrentPieceType == 'R')//same as queen but for rook, this continues for all the other pieces aswell
                        {
                            CurrPiece = 'R';
                            if (MoveCheck(CurrentX, CurrentY, inc, incr))
                            {
                                if ((pointGain(inc, incr) - pointLoss(CurrentX, CurrentY, inc, incr)) > TurnPoint)
                                {
                                    MovesNum = 0;
                                    BestMove = new int[1, 4];
                                }
                                if ((pointGain(inc, incr) - pointLoss(CurrentX, CurrentY, inc, incr)) >= TurnPoint)
                                {
                                    TurnPoint = pointGain(inc, incr)- pointLoss(CurrentX, CurrentY, inc, incr);
                                    BestMove[MovesNum, 0] = CurrentX;
                                    BestMove[MovesNum, 1] = CurrentY;
                                    BestMove[MovesNum, 2] = inc;
                                    BestMove[MovesNum, 3] = incr;
                                    MovesNum++;
                                    BestMove = arrayResize(BestMove);

                                }
                            }
                            else
                            {
                                CurrPiece = ' ';
                            }
                        }
                        else if (CurrentPieceType == 'B')
                        {
                            CurrPiece = 'B';
                            if (MoveCheck(CurrentX, CurrentY, inc, incr))
                            {
                                if ((pointGain(inc, incr) - pointLoss(CurrentX, CurrentY, inc, incr)) > TurnPoint)
                                {
                                    MovesNum = 0;
                                    BestMove = new int[1, 4];
                                }
                                if ((pointGain(inc, incr) - pointLoss(CurrentX, CurrentY, inc, incr)) >= TurnPoint)
                                {
                                    TurnPoint = pointGain(inc, incr)-pointLoss(CurrentX, CurrentY, inc, incr);
                                    BestMove[MovesNum, 0] = CurrentX;
                                    BestMove[MovesNum, 1] = CurrentY;
                                    BestMove[MovesNum, 2] = inc;
                                    BestMove[MovesNum, 3] = incr;
                                    MovesNum++;
                                    BestMove = arrayResize(BestMove);

                                }
                            }
                            else
                            {
                                CurrPiece = ' ';
                            }
                        }
                        else if (CurrentPieceType == 'H')
                        {
                            CurrPiece = 'H';
                            if (MoveCheck(CurrentX, CurrentY, inc, incr))
                            {
                                if ((pointGain(inc, incr) - pointLoss(CurrentX, CurrentY, inc, incr)) > TurnPoint)
                                {
                                    MovesNum = 0;
                                    BestMove = new int[1, 4];
                                }
                                if ((pointGain(inc, incr) - pointLoss(CurrentX, CurrentY, inc, incr)) >= TurnPoint)
                                {
                                    TurnPoint = pointGain(inc, incr)-pointLoss(CurrentX, CurrentY, inc, incr);
                                    BestMove[MovesNum, 0] = CurrentX;
                                    BestMove[MovesNum, 1] = CurrentY;
                                    BestMove[MovesNum, 2] = inc;
                                    BestMove[MovesNum, 3] = incr;
                                    MovesNum++;
                                    BestMove = arrayResize(BestMove);

                                }
                            }
                            else
                            {
                                CurrPiece = ' ';
                            }
                        }
                        else if (CurrentPieceType == 'P')
                        {
                            CurrPiece = 'P';
                            if (MoveCheck(CurrentX, CurrentY, inc, incr))
                            {
                                if ((pointGain(inc, incr) - pointLoss(CurrentX, CurrentY, inc, incr)) > TurnPoint)
                                {
                                    MovesNum = 0;
                                    BestMove = new int[1, 4];
                                }
                                if ((pointGain(inc, incr) - pointLoss(CurrentX, CurrentY, inc, incr)) >= TurnPoint)
                                {
                                    TurnPoint = pointGain(inc, incr)-pointLoss(CurrentX, CurrentY, inc, incr);
                                    BestMove[MovesNum, 0] = CurrentX;
                                    BestMove[MovesNum, 1] = CurrentY;
                                    BestMove[MovesNum, 2] = inc;
                                    BestMove[MovesNum, 3] = incr;
                                    MovesNum++;
                                    BestMove = arrayResize(BestMove);

                                }
                            }
                            else
                            {
                                CurrPiece = ' ';
                            }
                        }

                        else if (CurrentPieceType == 'K')
                        {
                            CurrPiece = 'K';
                            if (MoveCheck(CurrentX, CurrentY, inc, incr))
                            {
                                if ((pointGain(inc, incr)- pointLoss(CurrentX, CurrentY, inc, incr) > TurnPoint))
                                {
                                    MovesNum = 0;
                                    BestMove = new int[1, 4];
                                }
                                if ((pointGain(inc, incr) - pointLoss(CurrentX, CurrentY, inc, incr)) >= TurnPoint)
                                {
                                    TurnPoint = pointGain(inc, incr)-pointLoss(CurrentX,CurrentY,inc,incr);
                                    BestMove[MovesNum, 0] = CurrentX;
                                    BestMove[MovesNum, 1] = CurrentY;
                                    BestMove[MovesNum, 2] = inc;
                                    BestMove[MovesNum, 3] = incr;
                                    MovesNum++;
                                    BestMove = arrayResize(BestMove);

                                }

                            }
                            else
                            {
                                CurrPiece = ' ';
                            }
                        }

                    }

                }

            }
            int[,] TempMove = BestMove;//once done, due to the way resizing worked, we have one extra space, so we shrink our array to remove that extra empty space
            int NewLength = BestMove.GetLength(0) - 1;
            BestMove = new int[NewLength, 4];
            for (int i = 0; i < BestMove.GetLength(0); i++)
            {
                for (int inc = 0; inc < 4; inc++)
                {
                    BestMove[i, inc] = TempMove[i, inc];
                }
            }
            return BestMove;//retunr the gathered moves that will give the computer the most points
        }
        public static int pointGain(int NewX, int NewY)
        {
            //sets what each piece upon kill is work
            int PawnPoint = 1;
            int HorsePoint = 10;
            int RoyaltyPoint = 9;
            int QueenPoint = 40;
            int KingPoint = 1000;
            //checks what piece is at the space the computer is attemtping to move to
            char PieceDie = ChessBoard.TeamWhite[NewX, NewY];
            if (PieceDie == 'K')
            {
                return KingPoint;
            }
            if (PieceDie == 'Q')
            {
                return QueenPoint;
            }
            if (PieceDie == 'H' || PieceDie == 'R' || PieceDie == 'B')
            {
                return RoyaltyPoint;
            }
            if (PieceDie == 'H')
            {
                return HorsePoint;
            }
            if (PieceDie == 'P')
            {
                return PawnPoint;
            }
            else
            {
                return 0;
            }
        }
        public static int pointLoss(int OldX, int OldY, int NewX, int NewY)
        {
            //declares the variables
            int TotalLoss = 0;
            int PawnPoint = 1;
            int HorsePoint = 10;
            int RoyaltyPoint = 9;
            int QueenPoint = 40;
            int KingPoint = 900;//losing your king is seen as better than killing their king because killing their king will imediatley win you the game
            char OldPiece = ' ';
            char PieceSelect = ChessBoard.TeamBlack[OldX, OldY];
            //this changes the coordinates of the piece that is wanted to move to where it is wanted to be moved, in thus way we can run the scenario of what will happen if this piece is moved to that coords
            for (int i=0;i<ChessBoard.BlackCoords.GetLength(0);i++)
            {
                if(ChessBoard.BlackCoords[i,0]==OldX&& ChessBoard.BlackCoords[i,1]==OldY)//if the blackcoords is = to the coords of the piece you're looking for
                {
                    ChessBoard.BlackCoords[i, 0] = NewX;//set the coords to the new coords where you want the piece to move towards
                    ChessBoard.BlackCoords[i, 1] = NewY;
                    ChessBoard.Board[OldX, OldY] = ' ';
                    OldPiece = ChessBoard.Board[NewX, NewY];
                   ChessBoard.Board[NewX, NewY] = PieceSelect;
                    ChessBoard.TeamBlack[OldX, OldY] = ' ';
                  
                    ChessBoard.TeamBlack[NewX, NewY] = PieceSelect;
                }
            }
            //loops for each of the blackcorods in other words the location of each piece on the black team
            for(int i=0;i< ChessBoard.BlackCoords.GetLength(0);i++)
            {
                //sets the team to white so that way kingcheck uses the white pieces to check if the black peices are under check
                currTeam = "white";
                if(kingCheck(ChessBoard.BlackCoords[i,0], ChessBoard.BlackCoords[i,1]))//checks if the current piece in the black coords array can be killed on the next turn
                {
                    char PieceToDie = ChessBoard.TeamBlack[ChessBoard.BlackCoords[i, 0], ChessBoard.BlackCoords[i, 1]];//grabs the char of the piece that will be killed
                   
                    //basic if statements that add to the points lost based on what piece it is willl die
                    if (PieceToDie == 'K')
                    {
                        TotalLoss += KingPoint;
                    }
                    else if (PieceToDie == 'Q')
                    {
                        TotalLoss += QueenPoint;
                    }
                    else if (PieceToDie == 'R' || PieceToDie == 'B')
                    {
                        TotalLoss += RoyaltyPoint;
                    }
                    else if (PieceToDie == 'H')
                    {
                        TotalLoss += HorsePoint;
                    }
                    else if (PieceToDie == 'P')
                    {
                        TotalLoss += PawnPoint;
                    }
                    //I choose to do an adding points system as even though technically in the next turn only one of these pieces can be killed, i think its important to notice the gamestate with a large number of pieces able to be killed instead of just looking at the highest value of the piece that can be killed
                }
            }
            //reset the current team to black
            currTeam = "black";
            //resets my black coordingates array back to what it originally was
            for (int i = 0; i < ChessBoard.BlackCoords.GetLength(0); i++)
            {
                if (ChessBoard.BlackCoords[i, 0] == NewX && ChessBoard.BlackCoords[i, 1] == NewY)
                {
                    ChessBoard.BlackCoords[i, 0] = OldX;
                    ChessBoard.BlackCoords[i, 1] = OldY;
                    ChessBoard.Board[OldX, OldY] = PieceSelect;
                    ChessBoard.Board[NewX, NewY] = OldPiece;
                    ChessBoard.TeamBlack[OldX, OldY] = PieceSelect;
                    ChessBoard.TeamBlack[NewX, NewY] = ' ';
                }
            }
            return TotalLoss;//return the lost score



        }
        public static int[,] arrayResize(int[,] CurrentMoves)//simple resize method, sends the arrya to temporary array, increases the length by 1 and then re initializes it, then adds all the elements to it in a for loop
        {
            int[,] Moves = CurrentMoves;
            int NewLength = CurrentMoves.GetLength(0) + 1;
            CurrentMoves = new int[NewLength, 4];
            for (int i = 0; i < Moves.GetLength(0); i++)
            {
                for (int inc = 0; inc < 4; inc++)
                {
                    CurrentMoves[i, inc] = Moves[i, inc];
                }
            }
            return CurrentMoves;

        }
        public static bool CastleCheck(int OldX, int OldY, int NewX, int NewY)//this program loops can checks the spaces where the king would move when casteling, to see if hes moving through check
        {
            if(currTeam=="white")
            {
                currTeam = "black";
                if(OldY>NewY)
                {
                    for(int i=NewY;i<OldY;i++)//calls upon my king check method wiht the given y coords where the king is and where hes moving to, first going on to the left 
                    {
                       if (kingCheck(OldX,i))
                        {
                            currTeam = "white";//reset team to white after briefly chanign it to black to do kincheck
                            return true;
                        }
                       
                    }
                   
                }
                else if (NewY>OldY)// if its movwing to the right then we have oldy at the bottom
                {
                    for (int i = OldY; i < NewY; i++)
                    {
                        if (kingCheck(OldX, i))
                        {
                            currTeam = "white";//resets the team
                            return true;
                        }

                    }
                }
                currTeam = "white";//
            }
            if (currTeam == "black")
            {
                currTeam = "white";
                if (OldY > NewY)
                {
                    for (int i = NewY; i < OldY; i++)
                    {
                        if (kingCheck(OldX, i))
                        {
                            currTeam = "black";
                            return true;
                        }

                    }

                }
                else if (NewY > OldY)
                {
                    for (int i = OldY; i < NewY; i++)
                    {
                        if (kingCheck(OldX, i))
                        {
                            currTeam = "black";
                            return true;
                        }

                    }
                }
                currTeam = "black";
            }
            return false;
        }
      
       
   
      

    }
    

}
    

