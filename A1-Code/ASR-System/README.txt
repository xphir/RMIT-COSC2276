Name: Elliot Schot
RMIT Email: s3530160@student.rmit.edu.au
Group: No Group
GIT LINK: https://gitlab.com/ElliotSchotRMIT/COSC2276 [Currently Private]

Singleton: Used in SQLConnectionSingleton.cs
	Analytical justification: Allows a single instance of the SQL connection, with global access. and if needed lock protection ect.
	What advantages do they offer: Allows future enhancement of the SQLConnectionSingleton to allow things like a pool of SQLConection strings (ie. different SQL server connections to allow load balancing ect.)
	Alternative: Use a "public static string" - you lose the advantages of the singleton pattern (ie cant do more manipulation/thinking behind the singletons getter)
	
Observer: Used in [x]Manager.cs classes
	Analytical justification: It allows the Engine to view SQL data [x]Manager.[x]List and to create/delete/update the data in the SQL backend by [x]List.Update
	What advantages do they offer: Allows a one stop shop to create/delete/edit/read data from the SQL server
	Alternative: Doing SQL data pulls/pushes in each data manipulation method (this would honestly remove the need for OOP, so it would be pretty stupid and get confusing fast)