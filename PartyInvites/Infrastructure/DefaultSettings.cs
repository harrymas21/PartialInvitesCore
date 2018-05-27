namespace PartyInvites.Infrastructure
{
    public sealed class DefaultSettings
    {
        private static readonly object SyncRoot = new object();

        private DefaultSettings() { }

        private static DefaultSettings instance;
        public static DefaultSettings Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    if (instance == null)
                        instance = new DefaultSettings();

                    instance.Hostname = "192.168.1.120";
                    instance.Database = "PartyInvitesDB";
                    instance.Collection = "PartyInvites";
                }
                return instance;
            }
        }

        public string Hostname { get; private set; }

        public string Database { get; private set; }

        public string Collection { get; private set; }
    }
}
