using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Prezentacja.ViewModel
{
    internal class TextBox : ObserveTextBox
    {
		private int amount_;

		public int amount
		{
			get { return amount_; }
			set { 
				amount_ = value;
				OnPropertyChanged();
			}
		}

		public TextBox()
		{
			Task.Run(() =>
			{
				while (true)
				{
					Debug.WriteLine($"amount: {amount}");
					Thread.Sleep(500);
				}
			});
		}

	}
}
