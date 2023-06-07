using AutoMapper;

namespace HC.Infrastructure.PackageConfiguration.AutoMapper;

public interface IHaveCustomMapping
{
    void CreateMappings(Profile profile);
}
