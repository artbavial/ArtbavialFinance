using ArtBavialMyFinance.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtbavialMyFinance.Models
{

	/// <summary>
	/// Модель для представления счета.
	/// </summary>
	public class Account
	{
		/// <summary>
		/// Уникальный идентификатор счета.
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// Название счета.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Баланс счета.
		/// </summary>
		public decimal Balance { get; set; }

		/// <summary>
		/// Идентификатор валюты счета.
		/// </summary>
		public long CurrencyId { get; set; }

		/// <summary>
		/// Связанная валюта счета.
		/// </summary>
		public Currency Currency { get; set; }

		/// <summary>
		/// Тип счета (например, "Наличные", "Банковская карта", "Вклад").
		/// </summary>
		public AccountType Type { get; set; }  // Добавлен enum для типа счета

		/// <summary>
		/// Является ли счет главным
		/// </summary>
		public bool IsPrimaryAccount { get; set; }
	}

}
