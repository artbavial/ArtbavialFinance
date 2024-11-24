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
		Income,    // Доход
		Expense,   // Расход
		Neutral    // Нейтрально (например, перевод между счетами)
	}

}
