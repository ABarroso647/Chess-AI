#include<windows.h>
#include<stdio.h>
#include <cmath> 
#include <fstream>
#include <iostream>
using namespace std; 
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
	cin.get();
   click(14,40); 
	

	click(39,56);
	Sleep(20);
	keybd_event(0X49,0, 0,0);//I
	Sleep(20);
	keybd_event(0X49,0, KEYEVENTF_KEYUP,0);
	Sleep(20);
	keybd_event(0X4E,0, 0,0);//N
	Sleep(20);
	keybd_event(0X4E,0, KEYEVENTF_KEYUP,0);
	Sleep(20);
	keybd_event(0X50,0, 0,0);//P
	Sleep(20);
	keybd_event(0X50,0, KEYEVENTF_KEYUP,0);
	Sleep(20);
	keybd_event(0X55,0, 0,0);//U
	Sleep(20);
	keybd_event(0X55,0, KEYEVENTF_KEYUP,0);
	Sleep(20);
	keybd_event(0X54,0, 0,0);//T
	Sleep(20);
	keybd_event(0X54,0, KEYEVENTF_KEYUP,0);
	Sleep(20);
	keybd_event(VK_RETURN,0, 0,0);//ENTER
	Sleep(20);
	keybd_event(VK_RETURN,0, KEYEVENTF_KEYUP,0);
	Sleep(600);	
	ifstream fin("input.txt");
	if(!is_empty(fin))
	{
		fin.close();
		ShellExecute(NULL, "open", "MouseMovement.exe", NULL, NULL, SW_SHOWDEFAULT);
		bool loop = false;
		while(!loop)
		{
			try
			{
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
		remove("input.txt");
		click(1908,10);	
		Sleep(200);
		click(139,36);
		Sleep(200);
		click(157,237);
		Sleep(200);
		click(443,260);
		Sleep(3000);
		click(366,375);
		mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);	
		Sleep(5000);
		click(873,277);
	/*	Sleep(5000);
		Sleep(20);
		keybd_event(0X43,0,0,0);//c
		Sleep(20);
		keybd_event(0X43,0, KEYEVENTF_KEYUP,0);//c
		Sleep(20);
		keybd_event(0X48,0,0,0);//h
		Sleep(20);
		keybd_event(0X48,0, KEYEVENTF_KEYUP,0);//h
		Sleep(20);
		keybd_event(0X45,0,0,0);//e
		Sleep(20);
		keybd_event(0X45,0, KEYEVENTF_KEYUP,0);//e
		Sleep(20);
		keybd_event(0X53,0,0,0);//s
		Sleep(20);
		keybd_event(0X53,0, KEYEVENTF_KEYUP,0);//s
		Sleep(20);
		keybd_event(0X53,0,0,0);//s
		Sleep(20);
		keybd_event(0X53,0, KEYEVENTF_KEYUP,0);//s
		Sleep(20);
		keybd_event(0X4D,0,0,0);//m
		Sleep(20);
		keybd_event(0X4D,0, KEYEVENTF_KEYUP,0);//m
		Sleep(20);
		keybd_event(0X4F,0,0,0);//o
		Sleep(20);
		keybd_event(0X4F,0, KEYEVENTF_KEYUP,0);//o
		Sleep(20);
		keybd_event(0X56,0,0,0);//v
		Sleep(20);
		keybd_event(0X56,0, KEYEVENTF_KEYUP,0);//v
		Sleep(20);
		keybd_event(0X45,0,0,0);//e
		Sleep(20);
		keybd_event(0X45,0, KEYEVENTF_KEYUP,0);//e
		Sleep(20);
		keybd_event(VK_OEM_PERIOD,0,0,0);//.
		Sleep(20);
		keybd_event(VK_OEM_PERIOD,0, KEYEVENTF_KEYUP,0);//.
		Sleep(20);
		keybd_event(0X54,0,0,0);//t
		Sleep(20);
		keybd_event(0X54,0, KEYEVENTF_KEYUP,0);//t
		Sleep(20);
		keybd_event(0X58,0,0,0);//x
		Sleep(20);
		keybd_event(0X58,0, KEYEVENTF_KEYUP,0);//x
		Sleep(20);
		keybd_event(0X54,0,0,0);//t
		Sleep(20);
		keybd_event(0X54,0, KEYEVENTF_KEYUP,0);//t
		Sleep(5000);
	*/	click(689,334);
		Sleep(5000);
		//mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);	
		click(1125,702);
		Sleep(5000);
		click(895,189);
		Sleep(500);
		click(140,35);
		Sleep(300);
		click(203,180);
		Sleep(5000);
		click(441,447);
		remove("chessmove.txt");
		cout<< "Success now wait for roughly 1 min the turn time and run again";
	
		
		
	}
	else
	{
		cout<<"No data in file";
		Sleep(10000);
	}
	return 0;

	//https://stackoverflow.com/questions/15435994/how-do-i-open-an-exe-from-another-c-exe
}
