Distributed Programming Assignment TODOs

- check if w.WindDirectionInDegrees works again.

NOTE: true users should have real emails which are validaded. to test 
on multiple mails.



- paypal.
- deployment.

- make token bkend properly (context issue)
- submit buttons of register/login shuld be disabled if fields are empty.









Current weather @Valletta:
http://api.openweathermap.org/data/2.5/weather?q=Valletta&units=metric&appid=d12e761b653064784c9d028522c92fe9



- use weather api of our choice. but one is suggested.
1. retrieve weather data once a day
- store in db of your choice.
2. Register
- 3 em fuq moodle steps. (Login with access token)
- 4. nuzaw wind speed, wind direction, humidity, temperature. (use checkboxs)
- can be done in the register (more simple)
- 5. convertion we can do this as an equation to change between degrees and ferh
do it in front end
- 6. sandbox paypal account. use (sandbox.paypal.com)
- paypal integration within agunlar em tutorial fuq moodle.
- use sms web apis gives you some smses for free. else
you can use an api which sends an email
-7 upload nad deploy website (publish from visual studio)






paypal business email?


a db for website should also be created for hosting.
vs status should be turned on. (to deploy it from visual studio)
get publish settings (will download a file)


from vs
	Publish only the web
	import profile


need to also host the db.
dont forget to change connectionstring with hosting 

----------------------------------------------------------------------------------------------------------------------------------

Changes done to try to fix problem.

Vs 
- added enable.corse in reg  (app_start/webApiConfig)
- added corse on user controller (UserController)

iis Manager
- Anonymous Authentication set to enabled (edit and chose Application pool identity.)
- added Access-Control-Allow-Origin from iis value *
- Authorization rules allow Anonymous Users

changed all applicationhost files.
~\Documents\IISExpress\config\applicationhost  (from pcUser)
C:\Program Files (x86)\IIS Express\AppServer\applicationhost
C:\Program Files\IIS Express\AppServer\applicationhost
- added 2 headers inside elements  <httpProtocol><customHeaders> 
(Note also altered same file in x86)


- note: postman desktop app gave error 401 while chrome app didn't


-----------------------------------------------------------------------------------------------------------------------------------
