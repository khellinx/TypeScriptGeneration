using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TypeScriptGeneration.Converters;
using TypeScriptGeneration.TypeMapping;

namespace TypeScriptGeneration
{
    public class ConvertConfiguration
    {
        private readonly IList<IConverter> _converters;

        public ConvertConfiguration()
        {
            _converters = new List<IConverter>
            {
                new EnumConverter(),
                new ClassConverter()
            };
            PredefinedMapping = new BuiltInPredefinedMappings();
            ClassConfiguration = new ClassConvertConfiguration();
            ShouldConvertProperty = ShouldConvertPropertyImpl;
        }

        public IEnumerable<IConverter> Converters => _converters;
        public IPredefinedMapping PredefinedMapping { get; }

        public ConvertConfiguration AddConverter(IConverter converter)
        {
            _converters.Insert(0, converter);
            return this;
        }
        
        public ClassConvertConfiguration ClassConfiguration { get; }

        public Func<Type, string> GetTypeName { get; set; } = type => type.GetCleanName();
        public Func<Type, PropertyInfo, string> GetPropertyName { get; set; } = (type, prop) => string.IsNullOrEmpty(prop.Name) ? prop.Name : prop.Name.Substring(0, 1).ToLower() + prop.Name.Substring(1);
        public Func<Type, bool> ShouldConvertType { get; set; } = type => true;
        public Func<Type, PropertyInfo, bool> ShouldConvertProperty { get; set; }

        private bool ShouldConvertPropertyImpl(Type type, PropertyInfo property)
        {
            var shouldConvert = true;
            if (ClassConfiguration.InheritanceConfig.TryGetValue(type, out var disc))
            {
                shouldConvert = disc.DiscriminatorProperty.Name != property.Name;
            }

            return shouldConvert;
        }

        public Func<Type, string, string> GetEnumValueName { get; set; } = (e, enumValueName) => enumValueName;
        public Func<Type, string> GetFileDirectory { get; set; } = type => "/";
        public Func<Type, string> GetFileName { get; set; } = type => type.GetCleanName();
    }
}