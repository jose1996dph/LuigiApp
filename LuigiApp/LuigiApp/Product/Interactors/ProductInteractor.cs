using LuigiApp.Base.Interactors;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace LuigiApp.Product.Interactors
{
    public class ProductInteractor: BaseInteractor<Models.Product>,IProductInteractor
    {
        public async Task<Models.Product> Get(string code)
        {
            return await DataStore.Select<Models.Product>()
                .FirstOrDefaultAsync(x => x.Code.ToLower() == code.ToLower());
        }

        public async Task<List<Models.Product>> Search(string query)
        {
            return await DataStore.Select<Models.Product>()
                .Where(x => x.Description.ToLower().Contains(query.ToLower()) || x.Code.ToLower().Contains(query.ToLower()))
                .ToListAsync();
        }

        public override async Task<bool> Save(Models.Product product)
        {
            var isExist = await DataStore.Select<Models.Product>().FirstOrDefaultAsync(x => product.Code == x.Code && x.Id != product.Id) != null;
            if (isExist)
            {
                throw new DuplicateNameException(Resources.Literals.CodeExists);
            }

            if (product.Id == 0)
            {
                return await DataStore.Insert(product);
            }
            else
            {
                return await DataStore.Update(product);
            }
        }
    }
}
