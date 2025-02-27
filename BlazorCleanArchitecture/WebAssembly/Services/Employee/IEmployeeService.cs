
namespace WebAssembly.Services.Employee
{
    public interface IEmployeeService
    {
        Task<Data.Employee?> GetById(int id, CancellationToken cancellationToken );

        Task<List<Data.Employee>?> GetAll(CancellationToken cancellationToken);
    
        Task<int> Add(Data.Employee entity, CancellationToken cancellationToken);
    
        void Update(Data.Employee entity);
    
        void Delete(int id);
    }
}
