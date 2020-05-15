
#include<windows.h>
#include<stdio.h>
#include <cmath> 
#include <fstream>
#include <iostream>
using namespace std; 
//This cheekcs if the file is empty in a consistent manner
bool is_empty(ifstream& pFile)
{
    return pFile.peek() == ifstream::traits_type::eof();
}
 
void click(int x,int y)
{
	Sleep(20);
	SetCursorPos(x,y);
	Sleep(40);
	mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);	
                                                                                                                                                                                       	
}
int main(int argc, char** argv)
{
	//initial opening of the file maangement directory a
	cin.ge	click(139,36);
	Sleep(200);
	click(157,237);
	Sleep(200);
	click(443,260);
	Sleep(3000);
	//double click to go to rc_data
	click(366,375);
	mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);	
	Sleep(200);
	//click on the proper file to download
	click(375,300);
	//program runs until the user clicks the space key
	//which theyll do at the end of the game
	while(!(GetAsyncKeyState(VK_SPACE) & 0x80000000))
	{
		//download the file to the computer
		Sleep(200);
		click(772,285);
		Sleep(500);
		click(983,683);
		Sleep(600);	
		//check if the file is empty
		ifstream fin("aistuff.txt");
		if(!is_empty(fin))
		{	
			fin.close();
			ShellExecute(NULL, "open", "chessai.exe", NULL, NULL, SW_SHOWDEFAULT);
			bool loop = false;
			while(!loop)
			{
				try
				{
					//wait for c# generated file to contain values
					fin.open("chessmove.txt");
					if(!is_empty(fin))
					{
						loop = true;
					}
					cout << "empty"<<endl;
					fin.close();
				}	
				catch (exception& e)
				{
					fin.close();
					cout << "no file yet"<<endl;
				}
			}
				//deletes the robotc file from pc so we dont need to deal 
				// with overwriting the file and extra clicks
				remove("aistuff.txt");	
				//uploads the c# file to the robot
				Sleep(200);
				click(873,277);
				Sleep(2000);
				click(550,345);
				Sleep(2000);		
				click(1125,702);
				Sleep(5000);
				//remnove the c# file from the pc as 
				//to not deal with overrwrite
				remove("chessmove.txt");
				//a turn will not take less than 2 minute 
				//so wait atleast 2 minute, as per constraints
				Sleep(120000);	
		}
		else
		{
			//wait 5 seconds then try opening the file again
			fin.close();
			remove("aistuff.txt");
			Sleep(5000);
		}
	}
	return 0;

	
}
