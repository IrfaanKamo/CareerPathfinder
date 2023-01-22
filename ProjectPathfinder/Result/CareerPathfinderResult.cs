using ProjectPathfinder.Areas.Admin.ViewModels;
using System;

// Developer Note - This result object makes use of version tolerant serialisation. If changes are required to this result in
// (12/07/2016)     future, we will be required to version this file by assigning the optional attribute to the affected fields.
//                  For more reference, please view: https://msdn.microsoft.com/en-us/library/ms229752(v=vs.110).aspx 

namespace ProjectPathfinder.Result
{
    ///<summary>
    ///Encapsulates the information required to display the results to the student.
    ///</summary>
    [Serializable]
    public class CareerPathfinderResult
    {
        public ResultFeedback resultFeedback;

        public CareerPathfinderResult()
        {
            resultFeedback = new ResultFeedback();
        }
    }
}