namespace Wikibus.Sources.Nancy
{
    public class IriTemplateMapping
    {
        public IriTemplateMapping(string variable, string property)
        {
            this.Variable = variable;
            this.Property = property;
        }

        public string Variable { get; set; }

        public string Property { get; set; }

        public string Type => "IriTemplateMapping";
    }
}