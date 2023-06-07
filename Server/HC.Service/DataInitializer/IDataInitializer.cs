using HC.Common.Markers;

namespace HC.Service.DataInitializer;

public interface IDataInitializer : IScopedDependency
{
    void InitializeData();
}
