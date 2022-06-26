using AutoMapper;
using Bwod.CartAPI.Data.ValueObjects;
using Bwod.CartAPI.Model;
using Bwod.CartAPI.Model.Context;
using Bwod.CartAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Bwod.CartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly MySQLContext _context;
        private IMapper _mapper;

        public CartRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> ApplyCoupon(string userId, string couponCode)
        {
            var header = await _context.CartHeaders.FirstOrDefaultAsync(t => t.user_id == userId);
            if (header != null)
            {
                header.coupon_code = couponCode;
                _context.CartHeaders.Update(header);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> RemoveCoupon(string userId)
        {
            var header = await _context.CartHeaders.FirstOrDefaultAsync(t => t.user_id == userId);
            if (header != null)
            {
                header.coupon_code = "";
                _context.CartHeaders.Update(header);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> ClearCart(string userId)
        {
            var cartHeader = await _context.CartHeaders.FirstOrDefaultAsync(t => t.user_id == userId);
            if (cartHeader != null)
            {
                _context.CartDetails.RemoveRange(
                    _context.CartDetails.Where(t => t.cart_header_id == cartHeader.id)
                    );
                _context.CartHeaders.Remove(cartHeader);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CartVO> FindCartByUserId(string userId)
        {
            Cart cart = new()
            {
                cart_header = await _context.CartHeaders.FirstOrDefaultAsync(t => t.user_id == userId) ?? new CartHeader(),
            };
            cart.cart_details = _context.CartDetails
                .Where(t => t.cart_header_id == cart.cart_header.id)
                .Include(t => t.product);
            return _mapper.Map<CartVO>(cart);
        }     
        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            try
            {
                CartDetail cartDetail = await _context.CartDetails.FirstOrDefaultAsync(t => t.id == cartDetailsId);

                int total = _context.CartDetails.Where(t => t.cart_header_id == cartDetail.cart_header_id).Count();

                _context.CartDetails.Remove(cartDetail);
                if (total == 1)
                {
                    var cartHeaderToRemove = await _context.CartHeaders.FirstOrDefaultAsync(t => t.id == cartDetail.cart_header_id);
                    _context.CartHeaders.Remove(cartHeaderToRemove);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<CartVO> SaveOrUpdateCart(CartVO vo)
        {
            Cart cart = _mapper.Map<Cart>(vo);            
            var product = await _context.Products.FirstOrDefaultAsync(
                p => p.id == vo.cart_details.FirstOrDefault().product_id);

            if (product == null)
            {
                _context.Products.Add(cart.cart_details.FirstOrDefault().product);
                await _context.SaveChangesAsync();
            }            

            var cartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(
                c => c.user_id == cart.cart_header.user_id);

            if (cartHeader == null)
            {                
                _context.CartHeaders.Add(cart.cart_header);
                await _context.SaveChangesAsync();
                cart.cart_details.FirstOrDefault().cart_header_id = cart.cart_header.id;
                cart.cart_details.FirstOrDefault().product = null;
                _context.CartDetails.Add(cart.cart_details.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
            else
            {
                var cartDetail = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                    p => p.product_id == vo.cart_details.FirstOrDefault().product_id &&
                    p.cart_header_id == cartHeader.id);

                if (cartDetail == null)
                {                    
                    cart.cart_details.FirstOrDefault().cart_header_id = cartHeader.id;
                    cart.cart_details.FirstOrDefault().product = null;
                    _context.CartDetails.Add(cart.cart_details.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
                else
                {                    
                    cart.cart_details.FirstOrDefault().product = null;
                    cart.cart_details.FirstOrDefault().count += cartDetail.count;
                    cart.cart_details.FirstOrDefault().id = cartDetail.id;
                    cart.cart_details.FirstOrDefault().cart_header_id = cartDetail.cart_header_id;
                    _context.CartDetails.Update(cart.cart_details.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
            }
            return _mapper.Map<CartVO>(cart);
        }
    }
}
