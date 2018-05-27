namespace PartyInvites.RabbitProducer
{
    public interface IProducer
    {
        void SendMessage(string message);
    }
}
