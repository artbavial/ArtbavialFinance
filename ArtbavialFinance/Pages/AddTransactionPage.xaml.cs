using System;
using ArtBavialMyFinance.Data;
using ArtBavialMyFinance.Data.Models;
using ArtBavialMyFinance.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;

namespace ArtbavialFinance.Pages
{
	public partial class AddTransactionPage : ContentPage
	{
		private readonly AppDbContext _dbContext;
		public AddTransactionPage(AppDbContext dbContext)
		{
			InitializeComponent();
			_dbContext = dbContext;
		}

		private void OnSaveTransactionClicked(object sender, EventArgs e)
		{
			// Получаем введенные данные
			decimal amount = decimal.Parse(AmountEntry.Text);
			string transactionType = (string)TransactionTypePicker.SelectedItem;
			string description = DescriptionEditor.Text;

			// Валидация данных
			if (amount <= 0)
			{
				DisplayAlert("Error", "Amount must be greater than zero.", "OK");
				return;
			}
			if (transactionType == null)
			{
				DisplayAlert("Error", "Please select a transaction type.", "OK");
				return;
			}
			if (string.IsNullOrWhiteSpace(description))
			{
				DisplayAlert("Error", "Description cannot be empty.", "OK");
				return;
			}

			// Создаем и сохраняем новую транзакцию (пример)
			Transaction newTransaction = new Transaction
			{
				Amount = amount,
				Type = (TransactionType)Enum.Parse(typeof(TransactionType), transactionType),
				Description = description,
				Date = DateTime.Now
			};

			// Сохраните транзакцию в вашей модели данных
			// Например:
			// FinanceManager.AddTransaction(newTransaction);

			// Показываем подтверждение и возвращаемся на предыдущую страницу
			DisplayAlert("Success", "Transaction saved successfully.", "OK");
			Navigation.PopAsync();
		}
	}
}
