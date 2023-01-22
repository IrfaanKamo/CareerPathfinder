using ProjectPathfinder.Areas.Member.ViewModels;
using System;

// Developer Note - This test object makes use of version tolerant serialisation. If changes are required to this test in future
//                  we will be required to version this file by assigning the optional attribute to the affected fields.
//                  For more reference, please view: https://msdn.microsoft.com/en-us/library/ms229752(v=vs.110).aspx                   

namespace ProjectPathfinder.Test
{
    ///<summary>
    ///Contains a collection of containers for each step of the test. Each container comes from the respective step view model.
    ///</summary>
    [Serializable]
    public class CareerPathfinderTest
    {
        // Current Step that the User is on
        private int current_step;
        public int CurrentStep
        {
            get { return this.current_step; }
            set { if (value > current_step) current_step = value; }
        }

        //All required steps
        public TestStepTwo stepTwo;
        public TestStepThree stepThree;
        public TestStepFour stepFour;
        public TestStepFive stepFive;
        public TestStepFiveContinuation stepFiveContinuation;
        public TestStepSix stepSix;
        public TestStepSeven stepSeven;
        public TestStepEight stepEight;
        public TestStepNine stepNine;
        public TestStepTen stepTen;
        /*public TestStepEleven_1 stepEleven_1;
        public TestStepEleven_2 stepEleven_2;
        public TestStepEleven_3 stepEleven_3;*/

        //Init
        public CareerPathfinderTest()
        {
            stepTwo = new TestStepTwo();
            stepThree = new TestStepThree();
            stepFour = new TestStepFour();
            stepFive = new TestStepFive();
            stepFiveContinuation = new TestStepFiveContinuation();
            stepSix = new TestStepSix();
            stepSeven = new TestStepSeven();
            stepEight = new TestStepEight();
            stepNine = new TestStepNine();
            stepTen = new TestStepTen();
            /*stepEleven_1 = new TestStepEleven_1();
            stepEleven_2 = new TestStepEleven_2();
            stepEleven_3 = new TestStepEleven_3();*/
        }
    }
}