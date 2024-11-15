﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtbavialMyFinance.Models
{
	public class Transaction
	{
		public long Id { get; set; } // Изменено на long
		public long AccountId { get; set; } // Изменено на long
		public decimal Amount { get; set; } // Сумма транзакции
		public DateTime Date { get; set; } // Дата транзакции
		public string Type { get; set; } // Тип транзакции (например, "Доход", "Расход")
		public string Description { get; set; } // Описание транзакции
	}
}
