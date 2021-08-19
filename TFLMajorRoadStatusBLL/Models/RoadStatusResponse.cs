namespace TFLMajorRoadStatusBLL.Models
{
    /// <summary>
    /// This model class is used to hold the response data which is sent from tfl road end point
    /// </summary>
    public class RoadStatusResponse
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string StatusSeverity { get; set; }
        public string StatusSeverityDescription { get; set; }
        public string Bounds { get; set; }
        public string Envelope { get; set; }
        public string Url { get; set; }
    }
}
