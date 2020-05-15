
task main()
{
	bool exit = false;
	while(!exit)
	{
		while(!getButtonPress(buttonAny))
		{}

		 if(getButtonPress(buttonLeft))
		{
			displaystring(1,"Claw open/close");
			motor[MotorB] = 5;
		}
		else if(getButtonPress(buttonRight))
		{
			displaystring(1,"Claw open/close");
				motor[motorB] = -5;
		}
		else if(getButtonPress(buttonUp))
		{
			displaystring(1,"Claw Up/down");
			motor[MotorC] = 5
		}
		else if(getButtonPress(buttonDown))
		{
			displaystring(1,"Claw Up/down");
			motor[motorC] = -5;
		}
		while(getButtonPress(buttonAny))
		{}
		motor[motorC] = motor[motorB] = 0;
	}


}
