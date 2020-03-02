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