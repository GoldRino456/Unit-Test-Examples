# Unit Tests

This repo is an extension of the Coding Tracker project I previously created. Specifically the goal of this project to demonstrate how unit tests are created and used to verify integrity of pieces of a project as it grows and changes.
## Unit Test Information

- Each unit test verifies functionality of different input validation methods of the main app (input to DateTime or Int, a range of DateTimes, and a specific Int from a list). 
- Tests were created with different variations of input, both valid and invalid, to ensure edge cases are accounted for. 




## Lessons Learned

- Unit tests are one of the most vital ways to ensure changes made down the line won't cause issues with other areas of a given project. Creating them within .NET, at least using MSTest, was incredibly easy. I did end up having to make minor changes to the base project to expose the logic behind my validation to the test project, but once accessible, setting up each test took only a few lines of code each. Also, the video linked below in Acknowledgements was a great guide for getting up and running with Unit Tests in C#. I also really liked the naming convention he used and highly recommend that for anyone that may be wondering how you should go about naming the individual tests.
## Acknowledgements

 - [The C# Academy](https://www.thecsharpacademy.com/)
 - [README Editor](https://readme.so/editor)
 - ["Unit Testing C# Code" by Programming with Mosh](https://www.youtube.com/watch?v=HYrXogLj7vg) - A great review for unit tests and easily explains how to handle them in C#. Also 10/10 on the test naming convention. 
