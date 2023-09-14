using BusinessModels;
using Xunit;

namespace Tests.TestHelpers
{
    public static class ExpertAssertionHelper
    {
        public static void AssertContainsExpert(Expert expected, Expert[] actual)
        {
            bool exists = false;
            foreach(var expert in actual)
            {
                if(expert.Id == expected.Id)
                {
                    Assert.Equal(expected.FirstName, expert.FirstName);
                    Assert.Equal(expected.LastName, expert.LastName);
                    exists = true;
                    break;
                }
            }
            Assert.True(exists, "ExpertId was not found in array of experts");
        }
    }
}