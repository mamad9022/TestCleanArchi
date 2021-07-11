namespace TestCleanArch.Application.Common.RabbitMq
{
    public interface IBusPublish
    {
        void Send(string queueName, string data);
    }
}
