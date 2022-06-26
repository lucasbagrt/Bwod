using Bwod.ProductAPI.Data.ValueObjects;

namespace Bwod.ProductAPI.Repository.IRepository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductVO>> FindAll();
        Task<ProductVO> FindById(int id);
        Task<ProductVO> Create(ProductVO vo);
        Task<ProductVO> Update(ProductVO vo);
        Task<bool> Delete(int id);
    }
}
