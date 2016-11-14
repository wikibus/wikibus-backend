using Autofac;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using wikibus.sources.nancy;

namespace data.wikibus.org
{
    /// <summary>
    /// The NAncy bootstrapper
    /// </summary>
    public class Bootstrapper : global::wikibus.nancy.Bootstrapper
    {
        /// <summary>
        /// Configures the application container.
        /// </summary>
        /// <param name="existingContainer">The existing container.</param>
        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            existingContainer.Update(builder =>
            {
                builder.RegisterType<Settings>().AsImplementedInterfaces();
            });

            base.ConfigureApplicationContainer(existingContainer);
        }
    }
}
