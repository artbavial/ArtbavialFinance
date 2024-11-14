using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtbavialMyFinance.Models
{
	public class Currency
	{
		public long Id { get; set; } // Изменено на long
		public string Name { get; set; } // Название валюты (например, "USD", "EUR")
		public string Symbol { get; set; } // Символ валюты (например, "$", "€")
		public decimal ExchangeRate { get; set; } // Курс валюты относительно базовой валюты
	}
}
