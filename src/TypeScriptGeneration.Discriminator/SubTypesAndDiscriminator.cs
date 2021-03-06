using System;
using System.Collections.Generic;
using System.Reflection;

namespace TypeScriptGeneration
{
    public class SubTypesAndDiscriminator
    {
        public PropertyInfo DiscriminatorProperty { get; set; }
        public object DiscriminatorValue { get; set; }
        public bool GenerateStaticTypeProperty { get; set; }
        public Dictionary<Type, object> SubTypesWithDiscriminatorValue { get; }

        public SubTypesAndDiscriminator(PropertyInfo discriminatorProperty, object rootValue, bool generateStaticTypeProperty)
        {
            DiscriminatorProperty = discriminatorProperty;
            DiscriminatorValue = rootValue;
            SubTypesWithDiscriminatorValue = new Dictionary<Type, object>();
        }
    }
}