namespace Titan.ECS3.Messaging
{
    internal delegate void MessageHandler<in T>(T message);
}
