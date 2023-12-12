using AppRepository.Entities;

namespace AppRepository.Interfaces
{
    public interface IAdoptionRepository
    {
        Task BulkInsert(List<AdoptRequest> adoptions);
        void Insert(AdoptRequest request);
        Task InsertAsync(AdoptRequest request);
    }
}
