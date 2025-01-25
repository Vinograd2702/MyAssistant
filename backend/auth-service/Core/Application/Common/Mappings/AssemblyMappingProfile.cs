using AutoMapper;
using System.Reflection;

namespace auth_servise.Core.Application.Common.Mappings
{
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile(Assembly assembly)
        {
            ApplyMappingsFromAssembly(assembly);
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(type => type.GetInterfaces()
                .Any(i => i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
                .ToList();

            foreach (var type in types)
            {
                var instanse = Activator.CreateInstance(type);
                var metodInfo = type.GetMethod("Mapping");
                metodInfo?.Invoke(instanse, new object[] { this });
            }
        }
    }
}
