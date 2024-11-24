using ArtBavialMyFinance.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ArtbavialFinance.Models;

namespace ArtBavialMyFinance.Models
{
	/// <summary>
	/// Класс для представления транзакции
	/// </summary>
	public class Transaction
	{
		/// <summary>
		/// Идентификатор транзакции
		/// </summary>
		[Key]
		public long Id { get; set; }

		/// <summary>
		/// Идентификатор счета, к которому относится транзакция
		/// </summary>
		public long AccountId { get; set; }

		/// <summary>
		/// Связь с счетом
		/// </summary>
		public Account Account { get; set; }

		/// <summary>
		/// Сумма транзакции
		/// </summary>
		public decimal Amount { get; set; }

		/// <summary>
		/// Дата транзакции
		/// </summary>
		public DateTime Date { get; set; }

		/// <summary>
		/// Тип транзакции (например, доход, расход)
		/// </summary>
		public TransactionType Type { get; set; }

		/// <summary>
		/// Описание транзакции
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Идентификатор пользователя, владельца транзакции
		/// </summary>
		public long UserId { get; set; }

		/// <summary>
		/// Связь с пользователем
		/// </summary>
		public User User { get; set; }
	}
}
