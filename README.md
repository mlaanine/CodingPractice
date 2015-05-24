# LocalSolarTimeWPF
Computes the offset between sundial and clock time.

A project in Visual Studio to learn how to make a simple Windows desktop app using
C#/.NET and WPF. The longitude and time zone TextBoxes are bound to a custom Location 
object, so the result updates as you type.

Enter your longitude and time zone, the app shows how many minutes to add or subtract 
from the sundial to get clock time.  The equation of time is based on a simplified 
formula, accurate to within roughly one minute, adapted from here:
http://susdesign.com/popups/sunangle/eot.php
