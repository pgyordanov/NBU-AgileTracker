﻿namespace AgileTracker.Common.Application.Mapping
{
    using System;
    using System.Linq;
    using System.Reflection;

    using AutoMapper;

    public class BaseMappingProfile : Profile
    {
        public BaseMappingProfile() => this.ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

        protected virtual void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly
                .GetExportedTypes()
                .Where(t =>
                    t.GetInterfaces()
                        .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                const string mappingMethodName = "Mapping";

                var methodInfo = type.GetMethod(mappingMethodName) ?? type.GetInterface("IMapFrom`1")?.GetMethod(mappingMethodName);

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
