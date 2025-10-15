using Evote360.Core.Application;
namespace Evote360.Core.Application.Interfaces;

public interface IGenericServices<TDtoModel> 
{
    Task<Result<List<TDtoModel>>> GetAllAsync();
    Task<Result<TDtoModel>> GetByIdAsync(int id);
    Task<Result<TDtoModel>> AddAsync(TDtoModel dtoModel);
    Task<Result<List<TDtoModel>>> AddRangeAsync(List<TDtoModel> dtoModels);
    Task<Result<TDtoModel>> UpdateAsync(int id, TDtoModel dtoModel);
    Task<Result> DeleteAsync(int id);
}