The application works as follows:
1.	There is a placement indicator which is blue in color when detected on planes.
2.	The color of the placement marker changes to red when furniture is detected at the location, indicating that a collision may occur, and the user cannot place the object there. Placement, relocation, and collision detection is implemented using Raycasts within the application.
3.	The user can move around the device to select where to place the furniture.
4.	Placing furniture: When the user finds the desired location, they can click on the furniture option that they wish to deploy from the options on the right of the screen.
    -	Upon clicking, the application will check 
        o	If the placement pose is valid 
        o	If a collision is detected (i.e., if there is another piece of furniture at the location)
    -	If the placement pose is valid and no collision is detected, the furniture will appear at the placement mark on the screen.
5.	Deleting furniture: To delete a piece of furniture, the user must click on the furniture on the screen and then click on the delete button. 
6.	Relocating furniture: To relocate a piece of furniture, the user must aim the placement marker at the furniture to be relocated, then click on the relocate button and then navigate to the new desired location of the furniture and click on the relocate button again.
