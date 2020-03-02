
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
