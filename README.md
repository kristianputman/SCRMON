# SCRMON

This code was created to run on a system where CCTV is pointed at a fixed location with little to no variation in colour or lighting is expected. If the CCTV visuals are placed on a single screen, the program will screenshot the monitor at the provided millisecond interval, sample pixels at the given granularity (1 being lowest), and then compare the pixel samples over time against the selected =/- variance in the RGB channels of the pixel. If the pixels being sampled deviate too much between stills, the program sounds an alarm and logs the time of the alarm.

The code took 5 minutes, it is not complete, it has not been error checked, it is not a finished product. It's only purpose is a demonstration of concept (at most). It is quite easy to modify or incorporate the same code into a more advanced system that notifies via email or text message however, or logs the times of variation continuously.

If you have any questions, please send an email to Kris@Kloak.org with the subject line RE: SCRMON.
