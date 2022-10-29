namespace ApiCore.Attributes
{
    /// <summary>
    /// defines type of property; set <see cref="PropertyType"/> to define it; all properties with this <see cref="Attribute"/> will be processed into a request and their corrispondent body types like Header, Get or Post elements.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyTypeAttribute : Attribute
    {
        public PropertyType Type;
        public PropertyTypeAttribute(PropertyType type)
        {
            this.Type = type;
        }
    }

    /// <summary>
    /// stores types of properties
    /// </summary>
    public enum PropertyType
    {
        Header,
        Post,
        Get
    }
}
