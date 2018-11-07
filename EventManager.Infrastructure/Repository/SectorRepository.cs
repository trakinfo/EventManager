using EventManager.Core.DataBaseContext;
using EventManager.Core.DataBaseContext.SQL;
using EventManager.Core.Domain;
using EventManager.Core.Globals;
using EventManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Infrastructure.Repository
{
	public class SectorRepository : Repository<Sector>, ISectorRepository
	{
		//public IEnumerable<Sector> SectorList { get => contentList; }
		//readonly ITicketRepository _ticketRepo;
		public SectorRepository(IDataBaseContext context, ISectorSql sectorSql) : base(context, sectorSql)
		{
			RefreshRepo();
		}

		private void RefreshRepo()
		{
			//_ticketRepo.TicketDateSpan = new DateSpan(DateTime.Now, DateTime.MaxValue);
			contentList = GetListAsync(null, CreateSector).Result;
		}

		public async Task<Sector> GetSector(long id)
		{
			return await Task.FromResult(contentList.Where(S => S.Id == id).FirstOrDefault());
		} 

		public async Task<IEnumerable<Sector>> GetSectorList(string name)
		{
			return await Task.FromResult(contentList.Where(S => S.Name.StartsWith(name)));
		}

		public Sector CreateSector(IDataReader S)
		{
			var sectorId = Convert.ToInt64(S["ID"]);
			return new Sector
				(
					sectorId,
					S["Name"].ToString(),
					S["Description"].ToString(),
					Convert.ToInt32(S["SeatingRangeStart"]),
					Convert.ToInt32(S["SeatingRangeEnd"]),
					Convert.ToUInt32(S["SeatingPrice"]),
					Convert.ToInt64(S["IdLocation"]),
					//_ticketRepo.GetTicketList().Result.Where(T => T.SectorId==sectorId).ToList(),
					new Signature
					(
						S["User"].ToString(),
						S["HostIP"].ToString(),
						Convert.ToDateTime(S["Version"])
					)
				);
		}

		public void CreateInsertParams(IDbCommand cmd)
		{
			throw new NotImplementedException();
		}

		public void CreateUpdateParams(IDbCommand cmd)
		{
			throw new NotImplementedException();
		}

		public void CreateDeleteParams(IDbCommand cmd)
		{
			throw new NotImplementedException();
		}
	}
}
