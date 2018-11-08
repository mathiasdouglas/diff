namespace WaesDiff.Domain.Settings
{
    /// <summary>
    /// Main class responsible for inject the data from appSettings
    /// </summary>
    public class Settings
    {
        public MongoSettings Mongo { get; set; }

        public MessageSettings Messages { get; set; }
    }
}
