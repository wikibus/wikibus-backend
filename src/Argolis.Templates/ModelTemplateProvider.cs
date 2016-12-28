using System;
using System.Reflection;

namespace Argolis.Templates
{
    public interface IModelTemplateProvider
    {
        string GetTemplate(Type type);

        string GetAbsoluteTemplate(Type type);
    }

    public class ModelTemplateProvider : IModelTemplateProvider
    {
        private readonly IBaseUriProvider baseUriProvider;

        public ModelTemplateProvider(IBaseUriProvider baseUriProvider)
        {
            this.baseUriProvider = baseUriProvider;
        }

        public string GetTemplate(Type type)
        {
            if (type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(Hydra.Resources.Collection<>))
            {
                return type.GetGenericArguments()[0].GetCustomAttribute<CollectionIdentifierTemplateAttribute>().Template;
            }

            return type.GetCustomAttribute<IdentifierTemplateAttribute>(false).Template;
        }

        public string GetAbsoluteTemplate(Type type)
        {
            if (type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(Hydra.Resources.Collection<>))
            {
                return this.baseUriProvider.BaseUri + type.GetGenericArguments()[0].GetCustomAttribute<CollectionIdentifierTemplateAttribute>().Template;
            }

            return this.baseUriProvider.BaseUri + type.GetCustomAttribute<IdentifierTemplateAttribute>(false).Template;
        }
    }
}