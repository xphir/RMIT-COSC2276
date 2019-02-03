# Web Development Technologies Assignment 2 [COSC2276-A2 WDT]

*Brief*
>A school is in need for an Appointment Scheduling and Reservation (ASR) system. They
use a manual system currently, where the reservations are made by an admin and
maintained on papers. This system is difficult to use and has various flaws. The school
has set aside a budget in the New Year for this software. However, they are cautious as
they have never dealt with a software firm before and are afraid that the software may not
meet their needs.
>After much deliberation, the school committee has approached your firm. After a series
of meetings, the firm has been asked to submit a working prototype of ASR.
>The Project Manager briefs you with a set of requirements and makes it very clear that
the Web ASR system is to be created using ASP.<span></span>NET Core 2.1/2.2 written in C# with
a Microsoft SQL Server backend. 

For more detailed information please see  [Assignment 1 Brief](/Other/WDT_Assignment_1.pdf) &  [Assignment 2 Brief](/Other/WDT_Assignment_2.pdf)

## Tech Used
Main Website [ASR-WEB] 
- ASP.<span></span>NET
- EF Core to Azure SQL Database 
- Razer/HTML Front End 
- API For an Angular Front End

Admin Site [AngularTest] **CURRENTLY NOT WORKING**
- Angular 5 Front End

## TODO
Main Website
- Add custom bootstrap stylesheet
- Clean up razor code
- Look into Staff/Student class inheritance

Angular Site
- Fix external API communication bug (It seems there is something blocking the Angular App from communicating with an external/different port API)

## Other Information
-	Inside /Other/ is a PDF of the current [Database Entity Relationship Diagram](/Other/ASR-Database-ER-Diagram.pdf)
-	Inside /Other/ is a [API Postman Config file](/Other/WDT A2 API.postman_collection.json) for each of the API connections, along with the relevant data for said calls 

## API

This project produces and uses an API, the following is the details on said API
>Note: (Message Body X Object) means the object is expected to be sent in the body of the request eg. form POST object, or raw JSON of Object and so on (see Postman file for examples)

<br><br>
**Staff API**


| API Link | Information | Type | Variable |
|----------|----------|----------|----------|
| /api/Staff/ |	Get all staff |	GET	| None
| /api/Staff/&lt;StaffID&gt; |	Get specific staff details |	GET |	&lt;StaffID&gt;
| /api/Staff/Create |	Create a new staff member |	POST |	(Message Body Staff Object)
| /api/Staff/Update/&lt;StaffID&gt; | Update a specific staff member |	PUT |	&lt;StaffID&gt; & (Message Body Staff Object)
| /api/Staff/Delete |	Delete a specific staff member |	DELETE |	(Message Body Staff Object)


<br><br>
**Student API**


| API Link | Information | Type | Variable |
|----------|----------|----------|----------|
| /api/Student |	Get all students |	GET |	None
| /api/Student/&lt;StudentID&gt; |	Get specific student details |	GET | &lt;StudentID&gt;
| /api/Student/Create |	Create a new student |	POST |	(Message Body Student Object)
| /api/Student/Update/&lt;StudentID&gt; |	Update a specific student |	PUT |	&lt;StudentID&gt; & (Message Body Student Object)
| /api/Student/Delete |	Delete a specific student |	DELETE |	(Message Body Student Object)


<br><br>
**Room API**


| API Link | Information | Type | Variable |
|----------|----------|----------|----------|
| /api/Room |	Get all rooms |	GET |	None
| /api/Room/Create |	Create a new room |	POST |	(Message Body Room Object)
| /api/Room/Edit/&lt;RoomID&gt; |	DISABLED: Update a specific Room |	PUT |	&lt;RoomID&gt; & (Message Body Room Object)
| /api/Room/Delete |	Delete a specific room |	DELETE |	(Message Body Room Object) 


<br><br>
**Slots API**


| API Link | Information | Type | Variable |
|----------|----------|----------|----------|
| /api/Slot/ |	Get all slots |	GET |	None
| /api/Slot/StaffBooking/&lt;StaffID&gt; |	Get all slots for specified staff |	GET |	&lt;StaffID&gt;
| /api/Slot/StudentBooking/&lt;StudentID&gt; |	Get all slots for a specifided student |	GET |	&lt;StudentID&gt;
| /api/Slot/Create/ |	Create a new slot |	POST |	(Message Body Slot Object)
| /api/Slot/Book/&lt;StudentID&gt; |	Book a specific slot |	PUT |	&lt;StudentID&gt; & (Message Body Slot Object)
| /api/Slot/Unbook/ |	Unbook a specific slot |	PUT |	(Message Body Slot Object)
| /api/Slot/Details/ |	Get specific slot details |	POST |	(Message Body Slot Object)
| /api/Slot/Delete |	Delete a specific slot |	DELETE |	(Message Body Slot Object)