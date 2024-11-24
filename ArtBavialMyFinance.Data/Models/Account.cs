using ArtbavialFinance.Models;
using ArtBavialMyFinance.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtBavialMyFinance.Models
{
	/// <summary>
	/// Класс для представления счета пользователя
	/// </summary>
	public class Account
	{
		/// <summary>
		/// Идентификатор счета
		/// </summary>
		[Key]
		public long Id { get; set; }

		/// <summary>
		/// Название счета
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Баланс на счету
		/// </summary>
		public decimal Balance { get; set; }

		/// <summary>
		/// Идентификатор валюты
		/// </summary>
		public long CurrencyId { get; set; }

		/// <summary>
		/// Связь с валютой
		/// </summary>
		public Currency Currency { get; set; }

		/// <summary>
		/// Является ли этот счет основным
		/// </summary>
		public bool IsPrimaryAccount
		{
			get; set;
		}

		/// <summary>
		/// Тип счета
		/// </summary>
		public AccountType Type { get; set; }
		/// <summary>
		/// Идентификатор пользователя, владельца счета
		/// </summary>
		public long UserId { get; set; }

		/// <summary>
		/// Связь с пользователем
		/// </summary>
		public User User { get; set; }
	}
}
