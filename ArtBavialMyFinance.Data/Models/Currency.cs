using ArtbavialFinance.Models;
using System.ComponentModel.DataAnnotations;

namespace ArtBavialMyFinance.Models
{
	/// <summary>
	/// Класс для представления валюты
	/// </summary>
	public class Currency
	{
		/// <summary>
		/// Идентификатор валюты
		/// </summary>
		[Key]
		public long Id { get; set; }


		/// <summary>
		/// Код валюты (например, USD, EUR)
		/// </summary>
		public string CodeCurrency { get; set; }

		/// <summary>
		/// Название валюты (например, Доллар, Евро)
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Символ валюты (например, $, €)
		/// </summary>
		public string Symbol { get; set; }

		/// <summary>
		/// Курс обмена относительно базовой валюты
		/// </summary>
		public decimal ExchangeRate { get; set; }

		/// <summary>
		/// Признак базовой валюты
		/// </summary>
		public bool IsBaseCurrency { get; set; }

		/// <summary>
		/// Идентификатор пользователя, владельца валюты
		/// </summary>
		public long UserId { get; set; }

		/// <summary>
		/// Связь с пользователем
		/// </summary>
		public User User { get; set; }
	}
}
