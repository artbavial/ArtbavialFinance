using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtBavialMyFinance.Data.Models
{
		/// <summary>
		/// Перечисление типов счетов.
		/// </summary>
		public enum AccountType
		{
			Cash,				// Наличные
			BankCard,			// Банковская карта
		    Cryptocurrency,		// Криптовалюта
			Deposit,			// Вклад
			GeneralMoneyAccount // Общий денежный счет.
		}
}

