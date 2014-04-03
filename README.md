This program uses C# to parse the Windows registry for installed applications that can be uninstalled by msiexec.exe command and allow programs to be uninstalled in sequence without user intervention. 

Inspired by those annoying toolbars and adware applications that people always seem to get installed on their computers and I've had to spend a lot of time uninstalling manually.


Features implemented thus far:
Discovery algorithm
msiexec command creation
"Browser Killer": see below

Features that still need to be finished:
GUI
Addition of programs that require user to handle uninstaller manually
threading with progress bar updates
testing

"Browser killer":
A feature I dreamed up while I was uninstalling 100+ toolbars and non-sense from someone's computer: basically, when you're uninstalling these programs, most of them lauch some sort of "uninstall survey" or something which prevents you from starting another uninstall because MSIExec will not launch when another instance of it is running OR any children of the previous MSIExec process is running (which the browser would be). So to prevent the browser launches from preventing the program from working, the code just loops infinitely and kills chrome, firefox, opera, and IE processes if they get launched. This is pretty "virus-like" so some AV pattern matching may detect this as a virus. However, I think it's a worthwhile feature that, when the program is actually fully written, I will probably devote more time to make really good.