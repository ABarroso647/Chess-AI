/*
 Name: Adam Barroso
 Date: 10/29/18
 Program Name: Chess Game
 Purpose: To create a small represantation of chess using 2d arrays multiple methods and classes
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class ChessBoard
    {
        //initialize variables that are variously used throughout my program
        public static bool CastleCheck = false;
        public static bool JustChecking = false;
        public static bool WhiteLRookMove = false;
        public static bool WhiteRRookMove = false;
        public static bool BlackLRookMove = false;
        public static bool BlackRRookMove = false;
        public static bool BlackKingMove = false;
        public static bool WhiteKingMove = false;
        public static int[,] BlackCoords = new int[16, 2];
        public static int[,] WhiteCoords = new int[16, 2];
        public static bool WhiteKingAlive = true;
        public static bool BlackKingAlive = true;
        public static bool PvP = true;
        public static int WhiteKingX = 0;
        public static int WhiteKingY = 0;
        public static int BlackKingX = 0;
        public static int BlackKingY = 0;
        public static bool Tie = false;
        public static char currentPiece = ' ';
        public static bool Attack = false;
        public static int CpuMove = 0;
        //this is the array that will contain both the white and the black pieces
        public static char[,] Board = new char[8, 8];
        //more initialization
        public static char[,] TeamWhite = new char[,]
        {
          /*
           {' ',' ',' ',' ',' ',' ',' ',' '},
           {' ',' ',' ',' ',' ',' ',' ',' '},
           {' ',' ',' ',' ',' ',' ',' ',' '},
           {' ',' ',' ',' ',' ',' ',' ',' '},
           {' ',' ',' ',' ',' ',' ',' ',' '},
           {' ',' ',' ',' ',' ',' ',' ',' '},
           {'P','P','P','P','P','P','P','P'},
           {'R','H','B','Q','K','B','H','R'},
           */
           {' ',' ',' ',' ',' ',' ',' ',' '},
           {' ',' ',' ',' ',' ',' ',' ',' '},
           {' ',' ',' ',' ',' ',' ',' ',' '},
           {' ',' ',' ',' ',' ',' ',' ',' '},
           {' ',' ',' ',' ',' ',' ',' ',' '},
           {' ',' ',' ',' ',' ',' ',' ',' '},
           {'P','P','P','P','P','P','P','P'},
           {'R','H','B','Q','K','B','H','R'},
        };
        public static char[,] TeamBlack = new char[,]
        {
         /* Orig Setup:
           {'R','H','B','Q','K','B','H','R'},
           {'P','P','P','P','P','P','P','P'},
           {' ',' ',' ',' ',' ',' ',' ',' '},
           {' ',' ',' ',' ',' ',' ',' ',' '},
           {' ',' ',' ',' ',' ',' ',' ',' '},
           {' ',' ',' ',' ',' ',' ',' ',' '},
           {' ',' ',' ',' ',' ',' ',' ',' '},
           {' ',' ',' ',' ',' ',' ',' ',' '},
         */
           {'R','H','B','Q','K','B','H','R'},
           {'P','P','P','P','P','P','P','P'},
           {' ',' ',' ',' ',' ',' ',' ',' '},
           {' ',' ',' ',' ',' ',' ',' ',' '},
           {' ',' ',' ',' ',' ',' ',' ',' '},
           {' ',' ',' ',' ',' ',' ',' ',' '},
           {' ',' ',' ',' ',' ',' ',' ',' '},
           {' ',' ',' ',' ',' ',' ',' ',' '},
        };
        //this method combines team white and team black to the board by first adding team black, then adding team white if theres no piece already there
        public static void combineArray()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int inc = 0; inc < 8; inc++)
                {
                    Board[i, inc] = TeamBlack[i, inc];
                }
            }
            for (int i = 0; i < 8; i++)
            {
                for (int inc = 0; inc < 8; inc++)
                {
                    if (Board[i, inc] == ' ')
                    {
                        Board[i, inc] = TeamWhite[i, inc];
                    }

                }
            }
        }

    }
    public abstract class AllMoves//This is an abstract classs, that I use for my inheritance of all the possible moves used by the chess pieces 
    {
        //any virtual method is one that is going to be overrriden by one of the subs of Allmoves but not all of them, so the body declaration is needed
        public char Piece = 'P';//set the inherited char to pawn
        public virtual bool forwardMove(int XCoord, int YCoord, int TargetX, int TargetY, bool white)//generates forward  move which accepts white due to ovverides of the method needing it, this checks if the coords are up the board and there is no piece inbetween the piece and its destination and then returns if there is or not
        {
            if ((XCoord > TargetX && YCoord == TargetY) && forwardCheck(XCoord, YCoord, TargetX, TargetY))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool backwardMove(int XCoord, int YCoord, int TargetX, int TargetY)//same as forward except it doesnt accept white and its for backwards now
        {
            if ((XCoord < TargetX && YCoord == TargetY) && backwardCheck(XCoord, YCoord, TargetX, TargetY))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool sideMove(int XCoord, int YCoord, int TargetX, int TargetY, bool white)//same as forward with this one accepting white, and checking if you can move to both sides now
        {
            if ((XCoord == TargetX && (YCoord > TargetY || YCoord < TargetY)) && sideCheck(XCoord, YCoord, TargetX, TargetY))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool diagonalMove(int XCoord, int YCoord, int TargetX, int TargetY, bool white)//same as forward move, except this checks if you can move in all four diagonals, then calls a method that checks if anythings in between 
        {
            if ((YCoord - TargetY == XCoord - TargetX || YCoord - TargetY == TargetX - XCoord || TargetY - YCoord == XCoord - TargetX || TargetY - YCoord == TargetX - XCoord) && diagonalCheck(XCoord, YCoord, TargetX, TargetY))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool lMove(int XCoord, int YCoord, int TargetX, int TargetY)//same as forward move, accept we are checking the 8 possible combinations of x,y coords that will make an l to see if the move proposed will fit into that, and not checking if theres a piece inbetween
        {

            if ((TargetX == XCoord + 2 && (TargetY == YCoord + 1 || TargetY == YCoord - 1)) || (TargetX == XCoord - 2 && (TargetY == YCoord + 1 || TargetY == YCoord - 1)) || (TargetY == YCoord + 2 && (TargetX == XCoord + 1 || TargetX == XCoord - 1)) || (TargetY == YCoord - 2 && (TargetX == XCoord + 1 || TargetX == XCoord - 1)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool forwardCheck(int XCoord, int YCoord, int TargetX, int TargetY)//This method runs through all the spaces inbetween the originally and its target on a forward to check if theres a piece inbetween, if there is return false
        {
            for (int i = (TargetX + 1); i < XCoord; i++)
            {
                if (ChessBoard.Board[i, YCoord] != ' ')
                {
                    return false;
                }
            }
            return true;
        }
        public bool backwardCheck(int XCoord, int YCoord, int TargetX, int TargetY)//same as forward check except it checks all the spaces behind it
        {

            for (int i = (XCoord + 1); i < TargetX; i++)
            {
                if (ChessBoard.Board[i, YCoord] != ' ')
                {
                    return false;
                }
            }
            return true;
        }
        public bool sideCheck(int XCoord, int YCoord, int TargetX, int TargetY)//same as forward accept it checks first if you can move to the left, then if you can move to the right without pieces beiing inbetween
        {
            if ((XCoord == TargetX) && YCoord > TargetY)
            {
                for (int i = (TargetY + 1); i < YCoord; i++)
                {
                    if (ChessBoard.Board[XCoord, i] != ' ')
                    {
                        return false;
                    }

                }
                return true;
            }
            else if ((XCoord == TargetX) && YCoord < TargetY)
            {
                for (int i = (YCoord + 1); i < TargetY; i++)
                {
                    if (ChessBoard.Board[XCoord, i] != ' ')
                    {
                        return false;
                    }

                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool diagonalCheck(int XCoord, int YCoord, int TargetX, int TargetY)//this one checks all four diagonals and if theres piedces in between on any of them top left, top right, bottom left, and bottom right
        {

            if (((YCoord - TargetY) == (XCoord - TargetX)) && (((YCoord - TargetY) >= 0) || ((XCoord - TargetX) >= 0)))
            {
                int inc = (TargetY + 1);
                for (int i = (TargetX + 1); i < XCoord; i++)//loop used in this way that way inc loops at the samespeed as i
                {
                        if (ChessBoard.Board[i, inc] != ' ')
                        {
                            return false;
                        }
                    inc++;
                }
                return true;
            }
            else if (((YCoord - TargetY) == (TargetX - XCoord)) && (((YCoord - TargetY) >= 0) || ((TargetX - XCoord) >= 0)))
            {
                int inc = (YCoord - 1);
                for (int i = (XCoord + 1); i < TargetX; i++)
                {
                   
                   
                        if (ChessBoard.Board[i, inc] != ' ')
                        {
                            return false;
                        }
                    inc--;
                }
                return true;
            }
            else if ((TargetY - YCoord) == (XCoord - TargetX))
            {
                int inc = (TargetY-1);
                for (int i = (TargetX + 1); i < XCoord; i++)
                {
                    
                        if (ChessBoard.Board[i, inc] != ' ')
                        {
                            return false;
                        }
                    inc--;
                }
                return true;
            }
            else if ((TargetY - YCoord) == (TargetX - XCoord))
            {
                int inc = YCoord + 1;
                for (int i = (XCoord + 1); i < TargetX; i++)
                {
                        if (ChessBoard.Board[i, inc] != ' ')
                        {
                            return false;
                        }
                    inc++;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public abstract bool canMove(int CurrentX, int CurrentY, int Newx, int Newy, bool white);//sets an abstract public bool method that will be overriden by all the subclasses of allmoves in order for them to put in their combination of moves that are possible
    }
    public class Queen : AllMoves
    {

        new public char Piece = 'Q';//within the queen the piece is q
        public override bool canMove(int CurrentX, int CurrentY, int NewX, int NewY, bool white)
        {
            Queen queen = new Queen();//the queen is able to move in either fwd, bkwd, diag, or side and so it checks if any of the 4 are true using the entered coords // Also object of queen created within the queen class that way the queen can use her own methods, this is eminated on all the other canmoves
            if (queen.forwardMove(CurrentX, CurrentY, NewX, NewY, white) || queen.backwardMove(CurrentX, CurrentY, NewX, NewY) || queen.diagonalMove(CurrentX, CurrentY, NewX, NewY, white) || queen.sideMove(CurrentX, CurrentY, NewX, NewY, white))
            {
                return true;

            }
            else
            {
                return false;
            }
        }
    }
    public class King : AllMoves
    {

        public override bool backwardMove(int XCoord, int YCoord, int TargetX, int TargetY)// the king ovverides backwards as the kigns backwards only cna move one space
        {
            if ((XCoord + 1) == TargetX && YCoord == TargetY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool forwardMove(int XCoord, int YCoord, int TargetX, int TargetY, bool white)//same as backwards but with forwards
        {
          
            if ((XCoord - 1) == TargetX && YCoord == TargetY)
            {
                return true;
            }
           
            else
            {
                return false;
            }
        }
        public override bool diagonalMove(int XCoord, int YCoord, int TargetX, int TargetY, bool white)//same as backwards but with diagonal
        {
            if ((TargetX == XCoord + 1 && TargetY == YCoord + 1) || (TargetX == XCoord - 1 && TargetY == YCoord - 1) || (TargetX == XCoord + 1 && TargetY == YCoord - 1) || (TargetX == XCoord - 1 && TargetY == YCoord + 1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool sideMove(int XCoord, int YCoord, int TargetX, int TargetY, bool white)//ovveride is the same however there are some xtra steps
        {
            if (((YCoord - 1) == TargetY && XCoord == TargetX) || ((YCoord + 1) == TargetY && XCoord == TargetX))//in addition to checking the usual one movce to each side,
            {
                return true;
            }
           else if (((YCoord - 2) == TargetY && XCoord == TargetX) && ChessBoard.WhiteKingMove == false && ChessBoard.WhiteLRookMove == false&&sideCheck(XCoord,YCoord,TargetX,(TargetY-2))&&white&&YCoord==4&&!ChessBoard.JustChecking&&!ChessBoard.CastleCheck)
            {
                ChessBoard.TeamWhite[7, 3] = 'R';//it checks if the white left  rook or king has moved, if there are any pieces between it and the king, and if the king moved two spaces to the left, then it performs a castle where it places the rook on the inside of the king
                ChessBoard.TeamWhite[7, 0] = ' ';
                ChessBoard.Board[7, 3] = 'R';
                ChessBoard.Board[7, 0] = ' ';
                return true;
            }
            else if (((YCoord + 2) == TargetY && XCoord == TargetX) && ChessBoard.WhiteKingMove == false && ChessBoard.WhiteRRookMove == false && sideCheck(XCoord, YCoord, TargetX, TargetY)&&white&&YCoord==4 && !ChessBoard.JustChecking && !ChessBoard.CastleCheck)
            {
                ChessBoard.TeamWhite[7, 5] = 'R';
                ChessBoard.TeamWhite[7, 7] = ' ';//same as left but on the right side now
                ChessBoard.Board[7, 5] = 'R';
                ChessBoard.Board[7, 7] = ' ';
                return true;
            }
            else if (((YCoord - 2) == TargetY && XCoord == TargetX) && ChessBoard.BlackKingMove == false && ChessBoard.BlackLRookMove == false && sideCheck(XCoord, YCoord, TargetX, (TargetY-2))&&!white&&YCoord==4 && !ChessBoard.JustChecking && !ChessBoard.CastleCheck)
            {
                ChessBoard.TeamBlack[0, 3] = 'R';//same but for black piece
                ChessBoard.TeamBlack[0, 0] = ' ';
                ChessBoard.Board[0, 3] = 'R';
                ChessBoard.Board[0, 0] = ' ';
                return true;
            }
            else if (((YCoord + 2) == TargetY && XCoord == TargetX) && ChessBoard.BlackKingMove == false && ChessBoard.BlackRRookMove == false && sideCheck(XCoord, YCoord, TargetX, TargetY)&&!white&&YCoord==4 && !ChessBoard.JustChecking && !ChessBoard.CastleCheck)
            {
                ChessBoard.TeamBlack[0, 5] = 'R';//same but for black right piece
                ChessBoard.TeamBlack[0, 7] = ' ';
                ChessBoard.Board[0, 5] = 'R';
                ChessBoard.Board[0, 7] = ' ';
                return true;
            }
            else
            {
                return false;
            }
        }
     

        public override bool canMove(int CurrentX, int CurrentY, int NewX, int NewY, bool white)//The king canmove checks if the king is able to move iether forwards, bnackwards, diagonal and sideways
        {
            King king = new King();
            if (king.forwardMove(CurrentX, CurrentY, NewX, NewY, white) || king.backwardMove(CurrentX, CurrentY, NewX, NewY) || king.diagonalMove(CurrentX, CurrentY, NewX, NewY, white) || king.sideMove(CurrentX, CurrentY, NewX, NewY, white))
            {
                return true;
               
            }
            else
            {
                return false;
            }
        }


        new public char Piece = 'K';//its char is ofcourse 'K'(king is very dry)
    }
    public class Bishop : AllMoves
    {
        new public char Piece = 'B';
        public override bool canMove(int CurrentX, int CurrentY, int NewX, int NewY, bool white)// bishop canmove only calls upon the diagonal
        {
            Bishop bishop = new Bishop();
            if (bishop.diagonalMove(CurrentX, CurrentY, NewX, NewY, white))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class Rook : AllMoves
    {
        new public char Piece = 'R';//chars are self explanatory
        public override bool canMove(int CurrentX, int CurrentY, int NewX, int NewY, bool white)//rook checks if the coordinates allow him to move forwards, backwards, or sideways
        {
            Rook rook = new Rook();
            if (rook.forwardMove(CurrentX, CurrentY, NewX, NewY, white) || rook.backwardMove(CurrentX, CurrentY, NewX, NewY) || rook.sideMove(CurrentX, CurrentY, NewX, NewY, white))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class Knight : AllMoves
    {
        new public char Piece = 'H';//H is way better then N for Knight, horse is a second name for the piece
        public override bool canMove(int CurrentX, int CurrentY, int NewX, int NewY, bool white)// this checks if the knight can move in the l shape
        {
            Knight knight = new Knight();
            if (knight.lMove(CurrentX, CurrentY, NewX, NewY))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class Pawn : AllMoves
    {
        public override bool diagonalMove(int XCoord, int YCoord, int TargetX, int TargetY,bool white)//this is the diagonal override for the pawn where the pawn checks if the coord are only 1 movement in the forward 2 diagonals if it is white and the backwards 2 diagonals if it is black
        {
            if (white && ((TargetX == XCoord - 1 && TargetY == YCoord - 1) || (TargetX == XCoord - 1 && TargetY == YCoord + 1)))
            {
                return true;
            }
            else if (!white && ((TargetX == XCoord + 1 && TargetY == YCoord + 1) || (TargetX == XCoord + 1 && TargetY == YCoord - 1)))
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        public override bool forwardMove(int XCoord, int YCoord, int TargetX, int TargetY, bool white)//this is forward ovveride for the pawn, since the pawn can never move back I just check based on the team if the pawn is in its starting x it can move one or two spaces forwards
        {
            if ((white && XCoord == 6))
            {
                if ((((XCoord - 2) == TargetX && YCoord == TargetY) || ((XCoord - 1) == TargetX && YCoord == TargetY))&& forwardCheck(XCoord, YCoord, TargetX, TargetY))//also checks the one space in between so it cant magically jump pieces
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (!white &&XCoord==1)//technically down i know
            {
                if ((((XCoord + 2) == TargetX && YCoord == TargetY) || ((XCoord + 1) == TargetX && YCoord == TargetY))&&forwardCheck(XCoord,YCoord,TargetX,TargetY))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (white)//this just checks if its just white and not in the starting position then it only allows the pawn to move one space, so the x coord to be one less in this case
            {
                if ((XCoord - 1) == TargetX && YCoord == TargetY)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else//technically down i know this just checks if its not white and not in the starting position, in which case the pawn can move one space, tehnically its down, but it works better in this case to still use forwardspace
            {
                if ((XCoord + 1) == TargetX && YCoord == TargetY)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public override bool canMove(int CurrentX, int CurrentY, int NewX, int NewY, bool white)//this is the pawn canmove
        {
            Pawn pawn = new Pawn();
            if(ChessBoard.Attack)// if the piece is set to attack it checks if the pawn can you its diagonal move
            {
                if (pawn.diagonalMove(CurrentX, CurrentY, NewX, NewY,white))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else//if the pawn is not set to attack it just checks if the pawn can do the forward move
            {
                if (pawn.forwardMove(CurrentX, CurrentY, NewX, NewY, white))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        
       

    }
}
