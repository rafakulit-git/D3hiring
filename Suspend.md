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