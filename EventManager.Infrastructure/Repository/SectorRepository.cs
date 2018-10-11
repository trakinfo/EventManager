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
		//public IEnumerable<Sector> SectorList { get => objectList; }
		readonly ITicketRepository _ticketRepo;
		public SectorRepository(ITicketRepository ticketRepo, IDataBaseContext context, ISectorSql sectorSql) : base(context, sectorSql)
		{
			_ticketRepo = ticketRepo;
			RefreshRepo();
			RecordAffected -= (s, ex) => RefreshRepo();
			RecordAffected += (s, ex) => RefreshRepo();
		}

		private void RefreshRepo()
		{
			_ticketRepo.TicketDateSpan=new DateSpan(DateTime.Now, DateTime.MaxValue);
			objectList = GetListAsync(null, CreateSector).Result;
		}

		public async Task<Sector> GetSector(long id)
		{
			return await Task.FromResult(objectList.Where(S => S.Id == id).FirstOrDefault());
		} 

		public async Task<IEnumerable<Sector>> GetSectorList(string name)
		{
			return await Task.FromResult(objectList.Where(S => S.Name.StartsWith(name)));
		}

		public Sector CreateSector(IDataReader S)
		{
			return new Sector
				(
					Convert.ToInt64(S["ID"]),
					S["Name"].ToString(),
					S["Description"].ToString(),
					Convert.ToInt32(S["SeatingRangeStart"]),
					Convert.ToInt32(S["SeatingRangeEnd"]),
					Convert.ToUInt32(S["SeatingPrice"]),
					Convert.ToInt64(S["IdLocation"]),
					_ticketRepo.GetTicketList().Result,
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
