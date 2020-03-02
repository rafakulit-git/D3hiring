Setting up your local and running it locally
========

1. Download and install MySql community
	- https://dev.mysql.com/downloads/installer/
2. Setup you MySql Server
3. Download and install .Net Sutdio 2019 Community
	- https://visualstudio.microsoft.com/downloads/
4. Go to GitHub Repo
	- https://github.com/rafakulit-git/D3hiring
5. Click 'Clone or Download'
6. Click 'Download ZIP'
7. Extract the ZIP file
8. Open your .Net Studio 2019 as 'Administrator'
	- Right click on the Visual Studio 2019 icon
	- Locate Visual Studio 2019 in the list of items and right click on it
	- Find 'Run as Administrator' and click on it
9. Open the solution in your extracted folder 'D3hiring\src\d3hiring.sln'
10. Open your MySql Workbench and connect to your DB
	- Restore DB using the files inside the extracted folder 'D3hiring\database\dumps\Dump20200301'
11. Inside VS 2019, you can see the 'Solution Explorer' on the right side panel.
	- Look for appsettings.json and open the file
	- Check and confirm the connection string.
12. Once all set, we can try running the application

Information regarding the API's
========

**Register**
----
  A teacher can register multiple students. A student can also be registered to multiple teachers.

* **URL**

  /api/register

* **Method:**

  `POST`
  
*  **URL Params**

   NONE

* **Data Params**

   **Required:**
   ```
	{
	  "students" :
		[
		  "commonstudent1@gmail.com", 
		  "commonstudent2@gmail.com",
		  "student_only_under_teacher_ken@gmail.com"
		]
	}
	```

* **Success Response:**

  * **Code:** 204 <br />
 
* **Error Response:**

  * **Code:** 404 NOT FOUND <br />

  OR

  * **Code:** 400 UNAUTHORIZED <br />
    **Content:** 
	```
	{
		"message": "Your client has issued a malformed or illegal request."
	}
	```
	
  OR

  * **Code:** 400 UNAUTHORIZED <br />
    **Content:** 
	```
	{
		"message": "One or more person(s) requested is not found."
	}
	```

	
**Common Students**
----
  This will retrieve a list of registered students common to ALL given list of teacher

* **URL**

  /api/commonstudents

* **Method:**

  `GET`
  
*  **URL Params**

   **Required:**
 
   `teacher=teacher=teacherken@gmail.com`
   
   OR
   
   `teacher=teacher=teacherken@gmail.com&teacher=teacherjoe@gmail.com`

* **Data Params**

  None

* **Success Response:**

  * **Code:** 200 <br />
    **Content:**  <br />
	```
	{
	  "students" :
		[
		  "commonstudent1@gmail.com", 
		  "commonstudent2@gmail.com"
		]
	}
	```
 
* **Error Response:**

  * **Code:** 404 NOT FOUND <br />

  OR

  * **Code:** 400 UNAUTHORIZED <br />
    **Content:** 
	```
	{
		"message": "Your client has issued a malformed or illegal request."
	}
	```
	
  OR

  * **Code:** 400 UNAUTHORIZED <br />
    **Content:** 
	```
	{
		"message": "One or more person(s) requested is not found."
	}
	```
	

**Suspend**
----
  This is to suspend a specific student.

* **URL**

  /api/suspend

* **Method:**

  `POST`
  
*  **URL Params**

   NONE

* **Data Params**

   **Required:**
   ```
	{
	  "student" : "studentmary@gmail.com"
	}
	```

* **Success Response:**

  * **Code:** 204 <br />
 
* **Error Response:**

  * **Code:** 404 NOT FOUND <br />

  OR

  * **Code:** 400 UNAUTHORIZED <br />
    **Content:** 
	```
	{
		"message": "Your client has issued a malformed or illegal request."
	}
	```
	
  OR

  * **Code:** 400 UNAUTHORIZED <br />
    **Content:** 
	```
	{
		"message": "One or more person(s) requested is not found."
	}
	```
	
	
**Retrieve for Notification**
----
  This service will send notification from a teacher to students.
  
  Things to note:
  * Student MUST NOT be suspended
  * AND MUST fulfill AT LEAST ONE of the following:
		1. is registered with “teacherken@gmail.com"
		2. has been @mentioned in the notification

* **URL**

  /api/retrievefornotifications

* **Method:**

  `POST`
  
*  **URL Params**

   NONE

* **Data Params**

	* Notification with @mentioned
	```
	{
	  "teacher":  "teacherken@gmail.com",
	  "notification": "Hello students! @studentagnes@gmail.com @studentmiche@gmail.com"
	}
	```
	
	OR
	
	```
	{
	  "teacher":  "teacherken@gmail.com",
	  "notification": "Hey everybody"
	}
	```

* **Success Response:**
  * Response for notification with @mentioned
  * **Code:** 200 <br />
    **Content:**  <br />
	```
	{
	  "recipients":
		[
		  "studentbob@gmail.com",
		  "studentagnes@gmail.com", 
		  "studentmiche@gmail.com"
		]   
	}
	```
	
	OR

	* Response for normal notification
  * **Code:** 200 <br />
    **Content:**  <br />
	```
	{
	  "recipients":
		[
		  "studentbob@gmail.com"
		]   
	}
	```
 
* **Error Response:**

  * **Code:** 404 NOT FOUND <br />

  OR

  * **Code:** 400 UNAUTHORIZED <br />
    **Content:** 
	```
	{
		"message": "Your client has issued a malformed or illegal request."
	}
	```
	
  OR

  * **Code:** 400 UNAUTHORIZED <br />
    **Content:** 
	```
	{
		"message": "One or more person(s) requested is not found."
	}
	```