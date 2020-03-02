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