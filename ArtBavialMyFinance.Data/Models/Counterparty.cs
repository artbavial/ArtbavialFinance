using ArtbavialFinance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtBavialMyFinance.Data.Models
{
	public class Counterparty
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public CounterpartyType Type { get; set; } // Тип контрагента
		public long UserId { get; set; } // Связь с пользователем
		public User User { get; set; }
	}

}
