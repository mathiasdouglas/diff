namespace WaesDiff.Domain.Settings
{
    /// <summary>
    /// Class responsible to getting Messages for the project
    /// </summary>
    public class MessageSettings
    {
        public string EmptyError { get; set; }

        public string NoDiffFound { get; set; }

        public string NoDataForDiff { get; set; }

        public string DataEqual { get; set; }

        public string ErrorDuringDiff { get; set; }

        public string ErrorOnConverting { get; set; }

        public string Inconclusive { get; set; }

        public string NotSameSize { get; set; }

        public string DataDifference { get; set; }
    }
}
