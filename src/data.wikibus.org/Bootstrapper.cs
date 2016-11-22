using Autofac;
using Nancy.Bootstrappers.Autofac;

namespace Data.Wikibus.Org
{
    /// <summary>
    /// The NAncy bootstrapper
    /// </summary>
    public class Bootstrapper : global::Wikibus.Nancy.Bootstrapper
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
