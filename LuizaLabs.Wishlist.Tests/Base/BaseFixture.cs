using LuizaLabs.Wishlist.App.Interfaces.Wrappers;
using Moq;
using System.Text.RegularExpressions;

namespace LuizaLabs.Wishlist.Tests.Base
{
    public class BaseFixture<TSUT>
    {
        public readonly string REGEX_FILTER_ERROR_INFO_STRING = "ErrorId:.*?(Status.*)";
        public readonly string REGEX_VALIDATE_ERROR_INFO_LOG = "ErrorId:.*?({0})";

        public TSUT SUT { get; set; }
        public Mock<ILoggerWrapper<TSUT>> mockLogger { get; set; }
        public FakerProvider faker { get; set; }

        public void VerifyLogErrorInfo(string expectedErrorInfo)
        {
            var regexMatch = new Regex(REGEX_FILTER_ERROR_INFO_STRING, RegexOptions.Singleline).Match(expectedErrorInfo);
            var expectedLogInfoFiltered = regexMatch.Groups[1].Value;

            mockLogger.Verify(l => l.LogInformation(It.IsRegex(
                string.Format(REGEX_VALIDATE_ERROR_INFO_LOG, expectedLogInfoFiltered),
                RegexOptions.Singleline)));
        }
    }
}
