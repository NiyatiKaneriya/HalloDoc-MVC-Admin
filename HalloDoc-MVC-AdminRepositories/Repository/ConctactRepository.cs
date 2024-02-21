using HalloDoc_MVC_AdminDBEntity.Data;
using HalloDoc_MVC_AdminDBEntity.Models;
using HalloDoc_MVC_AdminRepositories.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc_MVC_AdminRepositories.Repository
{
	public class ConctactRepository : IContactRepository
	{
		private readonly ApplicationDbContext _context;

		public ConctactRepository(ApplicationDbContext context)
		{
			_context = context;
		}

        public async Task<RequestClient> niyati()
		{
			
			return await _context.RequestClients.FirstOrDefaultAsync(x => x.RequestClientId == 4);
        }
    }
}
