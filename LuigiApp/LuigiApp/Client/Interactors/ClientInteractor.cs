using LuigiApp.Base.Interactors;
using LuigiApp.Resources;
using System.Data;
using System.Threading.Tasks;

namespace LuigiApp.Client.Interactors
{
    public class ClientInteractor: BaseInteractor<Models.Client>, IClientInteractor
    {
        public Task<Models.Client> Get(string dni) => DataStore.Select <Models.Client>().FirstOrDefaultAsync(x=> x.Dni == dni);

        public override async Task<bool> Save(Models.Client client)
        {
            var isExist = await DataStore.Select<Models.Client>().FirstOrDefaultAsync(x => client.Dni == x.Dni && x.Id != client.Id) != null;
            if (isExist)
            {
                throw new DuplicateNameException(Literals.DniExists);
            }

            if (client.Id == 0)
            {
                return await DataStore.Insert(client);
            }
            else
            {
                return await DataStore.Update(client);
            }
        }
    }
}
