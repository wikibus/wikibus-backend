using System;
using System.Reflection;

namespace Argolis.Templates
{
    public interface IModelTemplateProvider
    {
        string GetTemplate(Type type);
    }

    public class ModelTemplateProvider : IModelTemplateProvider
    {
        public string GetTemplate(Type type)
        {
            if (type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(Hydra.Resources.Collection<>))
            {
                return type.GetGenericArguments()[0].GetCustomAttribute<CollectionIdentifierTemplateAttribute>().Template;
            }

            return type.GetCustomAttribute<IdentifierTemplateAttribute>(false).Template;
        }
    }
}