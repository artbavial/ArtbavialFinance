using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtbavialMyFinance.Models
{
	public class Currency
	{
		public long Id { get; set; } //  Идентификатор Валюты

		/// <summary>
		/// Название валюты
		/// </summary>	
		public string Name { get; set; }

		/// <summary>
		/// Код валюты
		/// </summary>
		public string CodeCurrency { get; set; } // Название валюты (например, "USD", "EUR")

		/// <summary>
		/// Символ валюты
		/// </summary>
		public string Symbol { get; set; } // Символ валюты (например, "$", "€")

		/// <summary>
		/// Является ли валюта базовой
		/// </summary>
		public bool IsBaseCurrency { get; set; }

		/// <summary>
		/// Курс валюты относительно базовой валюты
		/// </summary>
		public decimal ExchangeRate { get; set; } // Курс валюты относительно базовой валюты
	}
}
