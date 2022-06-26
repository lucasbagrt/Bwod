using AutoMapper;
using Bwod.ProductAPI.Data.ValueObjects;
using Bwod.ProductAPI.Model;
using Bwod.ProductAPI.Model.Context;
using Bwod.ProductAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Bwod.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySQLContext _context;
        private IMapper _mapper;

        public ProductRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            List<Product> products = await _context!.Products!.ToListAsync();
            return _mapper!.Map<List<ProductVO>>(products);
        }
        public async Task<ProductVO> FindById(int id)
        {
            Product? product = await _context!.Products!.Where(t => t.id == id).FirstOrDefaultAsync();
            return _mapper!.Map<ProductVO>(product);
        }
        public async Task<ProductVO> Create(ProductVO vo)
        {
            Product? product = _mapper!.Map<Product>(vo);
            _context!.Products!.Add(product);
            await _context.SaveChangesAsync();
            return _mapper!.Map<ProductVO>(product);
        }
        public async Task<ProductVO> Update(ProductVO vo)
        {
            Product? product = _mapper!.Map<Product>(vo);
            _context!.Products!.Update(product);
            await _context.SaveChangesAsync();
            return _mapper!.Map<ProductVO>(product);
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                Product? product = await _context!.Products!.Where(t => t.id == id).FirstOrDefaultAsync();
                if (product == null) return false;
                _context!.Products!.Remove(product);
                await _context!.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
