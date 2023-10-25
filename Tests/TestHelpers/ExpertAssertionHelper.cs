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
                    AssertAreEqual(expected, expert);
                    exists = true;
                    break;
                }
            }
            Assert.True(exists, "ExpertId was not found in array of experts");
        }

        public static void AssertAreEqual(Expert expected, Expert actual)
        {
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.FirstName, actual.FirstName);
            Assert.Equal(expected.LastName, actual.LastName);
            Assert.Equal(expected.Role, actual.Role);
            Assert.Equal(expected.Technology, actual.Technology);
            Assert.Equal(expected.Description, actual.Description);
        }
    }
}