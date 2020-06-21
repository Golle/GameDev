namespace Titan.ECS.Messaging
{
    internal delegate void MessageHandler<T>(in T message);
}
