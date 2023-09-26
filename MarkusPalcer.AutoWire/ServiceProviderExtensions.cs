namespace MarkusPalcer.AutoWire;

public static class ServiceProviderExtensions
{
    public static void UseForAutoWirer(this IServiceProvider self)
    {
        AutoWire.ServiceProvider = self;
    }
}