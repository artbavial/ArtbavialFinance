using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtBavialMyFinance.Data.Models
{
	/// <summary>
	/// Типы транзакций, используемые в финансовом приложении.
	/// </summary>
	public enum TransactionType
	{
		/// <summary>
		/// Доход - транзакции, которые добавляют деньги на ваш счет (например, зарплата, бонусы).
		/// </summary>
		Income,

		/// <summary>
		/// Расход - транзакции, которые уменьшают баланс вашего счета (например, покупка продуктов, оплата счетов).
		/// </summary>
		Expense,

		/// <summary>
		/// Перевод между счетами - переводы денег между вашими собственными счетами.
		/// </summary>
		Transfer,

		/// <summary>
		/// Инвестиция - транзакции, связанные с вложениями денег (например, покупка акций).
		/// </summary>
		Investment,

		/// <summary>
		/// Снятие наличных - снятие денег в наличные.
		/// </summary>
		Withdrawal,

		/// <summary>
		/// Вклад - внесение денег на счет.
		/// </summary>
		Deposit
	}
}
