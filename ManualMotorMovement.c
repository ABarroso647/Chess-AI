
task main()
{
	bool exit = false;
	while(!exit)
	{
		while(!getButtonPress(buttonAny))
		{}

		 if(getButtonPress(buttonLeft))
		{
			displaystring(1,"X movement");
			motor[MotorA] = 10;//x
		}
		else if(getButtonPress(buttonRight))
		{
			displaystring(1,"X movement");
				motor[motorA] = -10;
		}
		else if(getButtonPress(buttonUp))
		{
			displaystring(1,"Y movement");
			motor[MotorD] = 10;//y
		}
		else if(getButtonPress(buttonDown))
		{
			displaystring(1,"Y movement");
			motor[motorD] = -10;
		}
		while(getButtonPress(buttonAny))
		{}
		motor[motorA] = motor[motorD] = 0;
	}



}
