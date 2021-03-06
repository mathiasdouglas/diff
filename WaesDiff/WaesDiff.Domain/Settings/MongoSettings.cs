﻿namespace WaesDiff.Domain.Settings
{
    /// <summary>
    /// Class specific to getting the data necessary to connect to the database
    /// </summary>
    public class MongoSettings
    {
        public string ConnectionString { get; set; }

        public string Database { get; set; }

        public string Collection { get; set; }
    }
}
