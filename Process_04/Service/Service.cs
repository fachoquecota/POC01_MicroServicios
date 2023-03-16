using Process_04.Dependency;
using Process_04.Repository;

namespace Process_04.Service
{
    public class Service : IService
    {
        private readonly IDependency _dependency;
        private readonly IRepository _repository;  

        public Service(IDependency dependency, IRepository repository)
        {
            _dependency= dependency;
            _repository= repository;
        }
        public string GetData()
        {
            var data = _repository.GetData();
            return $"{data} - {_dependency.GetData()}";
        }
    }
}
