
task main()
{
	bool loopy = false;
	while(loopy ==false)
	{
		int red=0, green =0, blue = 0;
		getColorRGB(S3,red,green,blue);
		displayString(1,"Blue: %d",blue);
		displayString(2,"Red: %d",red);
		displayString(3,"Green: %d",green);
	}




}
