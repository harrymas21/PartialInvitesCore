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
                    instance.Port = 5672;
                    instance.UserName = "user";
                    instance.Password = "user";
                    instance.VirtualHost = "vhost1";
                }
                return instance;
            }
        }

        public string Hostname { get; private set; }

        public string Database { get; private set; }

        public string Collection { get; private set; }

        public int Port { get; private set; }

        public string UserName { get; private set; }

        public string Password { get; private set; }

        public string VirtualHost { get; private set; }
    }
}
