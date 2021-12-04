using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FreeCourse.Shared.Dtos;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            var discounts = await _connection.QueryAsync<Models.Discount>("select * from discount");
            return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<Models.Discount>> GetById(int id)
        {
            var discount =
                (await _connection.QueryAsync<Models.Discount>("select * from discount where id=@Id", new {Id = id}))
                .SingleOrDefault();
            if (discount == null)
                return Response<Models.Discount>.Fail("Discount not found", 404);

            return Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> Save(Models.Discount discount)
        {
            var saveStatus =
                await _connection.ExecuteAsync("insert into discount (userid,rate,code) values(@UserId,@Rate,@Code)",
                    discount);

            if (saveStatus > 0)
                return Response<NoContent>.Success(204);

            return Response<NoContent>.Fail("an error occurred while adding", 500);
        }

        public async Task<Response<NoContent>> Update(Models.Discount discount)
        {
            var query = "update discount set userid=@UserId, code=@Code, rate=@Rate where id=@Id";
            var status =
                await _connection.ExecuteAsync(query,
                    new {Id = discount.Id, UserId = discount.UserId, Code = discount.Code, Rate = discount.Rate});

            if (status > 0)
                return Response<NoContent>.Success(204);

            return Response<NoContent>.Fail("Discount not found", 404);
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var status = await _connection.ExecuteAsync("delete from discount where id = @Id", new {Id = id});

            return status > 0 ? Response<NoContent>.Success(200) : Response<NoContent>.Fail("Discount not found", 404);
        }

        public async Task<Response<Models.Discount>> GetDiscountByCodeAndUserId(string code, string userId)
        {
            var discounts = await _connection.QueryAsync<Models.Discount>("select * from discount where userid=@UserId and code=@Code",
                new {UserId = userId, Code = code});

            var hasDiscount = discounts.FirstOrDefault();

            if (hasDiscount == null)
            {
                return Response<Models.Discount>.Fail("Discount not found", 404);
            }

            return Response<Models.Discount>.Success(hasDiscount, 200);
        }
    }
}