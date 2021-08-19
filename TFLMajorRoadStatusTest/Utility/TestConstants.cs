namespace TFLMajorRoadStatusTest.Utility
{
    internal static class TestConstants
    {
        public const string argsNull = "argsnull";
        public const string argEmpty = "argsEmpty";
        public const string validRoadArgsType = "validRoad";
        public const string inValidRoadArgsType = "InvalidRoad";
        public const string errorRoadArgsType = "ErrorRoad";
        public const string argValidRoad = "A2";
        public const string argInvalidRoad = "A233";
        public const string testException = "TestException";
        public const string sendAsync = "SendAsync";
        public const string roadDpName = "A2";
        public const string statusSeverity = "Good";
        public const string StatusSeverityDescription = "Good";
        public const string Id = "A2";
        public const string expectedMessage = "The Status of the A2 is as follows\r\n\tRoad Status is Good\r\n\tRoad Status Description is Good\r\n";
        public const string invalidRoadResponseContent = "A233 is not valid";
        public const string errorResponseContent = "Unable to retrieve the status for the road";
        public const string roadResponseContent = "[{\"$type\":\"Tfl.Api.Presentation.Entities.RoadCorridor, Tfl.Api.Presentation.Entities\",\"id\":\"a2\",\"displayName\":\"A2\",\"statusSeverity\":\"Good\",\"statusSeverityDescription\":\"No Exceptional Delays\",\"bounds\":\"[[-0.0857,51.44091],[0.17118,51.49438]]\",\"envelope\":\"[[-0.0857,51.44091],[-0.0857,51.49438],[0.17118,51.49438],[0.17118,51.44091],[-0.0857,51.44091]]\",\"url\":\"/Road/a2\"}]";
    }
}
