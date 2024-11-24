using ArtbavialFinance.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtBavialMyFinance.Data.Models
{
	public class OperationCategory
	{
		[Key]
		public long Id { get; set; }
		public string Name { get; set; }
		public TransactionType Type { get; set; } // Связь с типом транзакции
		public long UserId { get; set; } // Связь с пользователем
		public User User { get; set; }
	}

}
