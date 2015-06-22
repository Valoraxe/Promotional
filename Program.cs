//Still needs to have: Validation for decimal inputs (BitCoin value and GBP) 
//Sort records in descending date order
//Transfer dd/mm/yyyy to %d-%m-%Y date format
//Validation for E-mail addresses (does it have @ and . in the right places?)
using System;

class Program
{
	static void Main()
	{
	string [,] Results; 
    Results = new string[,] {{"Cash", "16 Jun 2014 10:23am","10.00","NothingHere","NothingHere"},
    {"Debit-Card", "16 Jun 2014 8:05am", "23.50", "Card No: 1234567890123456", "Holder: This Guy"},
    {"Credit-Card", "16 Jun 2014 12:10pm", "28.99", "Card No: 1234567890123456", "Holder: This Girl"},
    {"PayPal", "17 Jun 2014 0:03am", "60.00", "Email: this.email@hotmail.com","NothingHere"},
    {"Bitcoin", "16 Jun 201 14:10pm", "17.21", "0.06828458 BTC", "Address: 1JPgALvMpKg2erHni5pwQ01JPgALvMp"}};

	bool RunProgram = true;
	while(RunProgram == true)
	{
		Console.WriteLine("Welcome, enter '1' to Read stored transactions, or enter '2' to insert a new one. To exit the program, enter '0'.");		
	
		string UserInput = Console.ReadLine();
		int UserChoice;
		if(int.TryParse(UserInput, out UserChoice))
		{
			UserChoice = int.Parse(UserInput);
		}
		else
		{
			UserChoice = 3;
		}
		switch (UserChoice)
		{
		case 1:
			Console.Clear();
			float GrandTotal = 0;
			float Currency = 0;
			int ArrayLimit = Results.GetLength(0);
			for(int x = 0;x<ArrayLimit;x++)
			{
				Currency = float.Parse(Results[x,2]);
				GrandTotal = GrandTotal + Currency;
			}
			Console.WriteLine("\n-----------------------------");
			Console.WriteLine("\nCurrent Register (Press Enter to leave)");
			Console.WriteLine("\n-----------------------------");

			for(int y = 0;y<ArrayLimit;y++)
			{
				Console.Write("\n"+Results[y,0]+ "   "+Results[y,1]+ ",     "+Results[y,2]+ " GBP");

				if(Results[y,3] != "NothingHere")
				{
					Console.Write(",     "+Results[y,3]);
				}
				else
				{
					Console.Write("\n");
				}
				if(Results[y,4] != "NothingHere")
				{
					Console.Write(",     "+Results[y,4]+ "\n");
				}
				else
				{
					Console.Write("\n");
				}
			}

			Console.WriteLine("\n-----------------------------");
			Console.WriteLine("\nTotal: "+GrandTotal+ " GBP");
			Console.WriteLine("\n-----------------------------");
			Console.ReadLine();
            Console.Clear();
		break;
		case 2:
			Console.Clear();
			int PaymentOption;
            long Cardcheck;
			int ArraySize = Results.GetLength(0);
			ArraySize--;
			DateTime CurrentTime;
            string MyTime;
			string CurrencyLine;
			string CardNumber;
			string HolderName;
			string Email;
			string BitCoin;	
			string Address;
				Console.WriteLine("What is the Payment type? Enter '1' for Cash, '2' for Debit-Card, '3' for Credit-Card, '4' for PayPal, or '5' for Bitcoin. You can Enter '9' to return to the main menu.\n");
				string PaymentChoice = Console.ReadLine();
				if(int.TryParse(PaymentChoice, out PaymentOption))
				{
					PaymentOption = int.Parse(PaymentChoice);
				}
				else
				{
					PaymentOption = 6;
				}
			
				switch (PaymentOption)
				{
				case 1:
					Begin1:
					Console.WriteLine("\nPlease enter the amount that was paid in whole pounds\n");
					CurrencyLine = Console.ReadLine();
					if(int.TryParse(CurrencyLine, out PaymentOption))
					{}
					else
					{
						Console.WriteLine("\nInvalid amount, please try again!\n");
						goto Begin1;
					}
					Results[ArraySize,0] = "Cash";
					CurrentTime = DateTime.Now;
                    MyTime = CurrentTime.ToString();
					Results[ArraySize,1] = MyTime; 
					Results[ArraySize,2] = CurrencyLine;
                    Results[ArraySize,3] = "NothingHere";
                    Results[ArraySize,4] = "NothingHere";
					Console.Clear();
					Console.WriteLine("\nDatabase Updated\n");
				break;
				case 2:
					Begin2:
					Console.WriteLine("\nPlease enter the amount that was paid in whole pounds\n");
					CurrencyLine = Console.ReadLine();
					if(int.TryParse(CurrencyLine, out PaymentOption))
					{}
					else
					{
						Console.WriteLine("\nInvalid amount, please try again!\n");
						goto Begin2;
					}
					Middle2:
					Console.WriteLine("\nPlease enter the Debit Card Number\n");
					CardNumber = Console.ReadLine();
                    if (long.TryParse(CardNumber, out Cardcheck) && CardNumber.Length == 16)
					{}
					else
					{
						Console.WriteLine("\nInvalid card, please try again!\n");
						goto Middle2;
					}

					CardNumber = "Card No: "+CardNumber;
					Console.WriteLine("\nEnter the name of the Card Holder\n");
					HolderName = Console.ReadLine();
					HolderName = "Holder: "+HolderName;
					Results[ArraySize,0] = "Debit-Card";
					CurrentTime = DateTime.Now;
                    MyTime = CurrentTime.ToString();
					Results[ArraySize,1] = MyTime;
					Results[ArraySize,2] = CurrencyLine;
					Results[ArraySize,3] = CardNumber;
					Results[ArraySize,4] = HolderName;
					Console.Clear();
					Console.WriteLine("\nDatabase Updated\n");
				break;
				case 3:
					Begin:
					Console.WriteLine("\nPlease enter the amount that was paid in whole 						pounds\n");
					CurrencyLine = Console.ReadLine();
					if(int.TryParse(CurrencyLine, out PaymentOption))
					{}
					else
					{
						Console.WriteLine("\nInvalid amount, please try again!\n");
						goto Begin;
					}
					Middle:
					Console.WriteLine("\nPlease enter the Debit Card Number\n");
					CardNumber = Console.ReadLine();
					if(long.TryParse(CardNumber, out Cardcheck) && CardNumber.Length == 16)
					{}
					else
					{
						Console.WriteLine("\nInvalid card, please try again!\n");
						goto Middle;
					}
					CardNumber = "Card No: "+CardNumber;
					Console.WriteLine("\nEnter the name of the Card Holder\n");
					HolderName = Console.ReadLine();
					HolderName = "Holder: "+HolderName;
					Results[ArraySize,0] = "Credit-Card";
					CurrentTime = DateTime.Now;
                    MyTime = CurrentTime.ToString();
					Results[ArraySize,1] = MyTime;
					Results[ArraySize,2] = CurrencyLine;
					Results[ArraySize,3] = CardNumber;
					Results[ArraySize,4] = HolderName;
					Console.Clear();
					Console.WriteLine("\nDatabase Updated\n");
				break;
				case 4:
					Begin4:
					Console.WriteLine("\nPlease enter the amount that was paid in whole pounds\n");
					CurrencyLine = Console.ReadLine();
					if(int.TryParse(CurrencyLine, out PaymentOption))
					{}
					else
					{
						Console.WriteLine("\nInvalid amount, please try again!\n");
						goto Begin4;
					}
					Console.WriteLine("\nPlease enter the e-mail address\n");
					Email = Console.ReadLine();
					Email = "Email: "+Email;
					Results[ArraySize,0] = "PayPal";
					CurrentTime = DateTime.Now;
                    MyTime = CurrentTime.ToString();
					Results[ArraySize,1] = MyTime;
					Results[ArraySize,2] = CurrencyLine;
					Results[ArraySize,3] = Email;
                    Results[ArraySize,4] = "NothingHere";
					Console.Clear();
					Console.WriteLine("\nDatabase Updated\n");
				break;
				case 5:
					Begin5:
					Console.WriteLine("\nPlease enter the amount that was paid in whole pounds\n");
					CurrencyLine = Console.ReadLine();
					if(int.TryParse(CurrencyLine, out PaymentOption))
					{}
					else
					{
						Console.WriteLine("Invalid amount, please try again!");
						goto Begin5;
					}
					Middle5:
					Console.WriteLine("\nInsert the amount of BitCoins Paid\n");
					BitCoin = Console.ReadLine();
					if(int.TryParse(CurrencyLine, out PaymentOption))
					{}
					else
					{
						Console.WriteLine("Invalid amount, please try again!");
						goto Middle5;
					}
					BitCoin = BitCoin+ " BTC";
					End:
					Console.WriteLine("\nEnter BitCoin Address\n");
					Address = Console.ReadLine();
					if(Address.Length == 31)
					{}
					else
					{
						Console.WriteLine("\nInvalid Address, please try again!\n");
						goto End;
					}
					Address = "Address: "+Address;
					Results[ArraySize,0] = "BitCoin";
					CurrentTime = DateTime.Now;
                    MyTime = CurrentTime.ToString();
					Results[ArraySize,1] = MyTime;
					Results[ArraySize,2] = CurrencyLine;
					Results[ArraySize,3] = Address;
					Console.Clear();
					Console.WriteLine("\nDatabase Updated\n");
				break;
				case 9:
					Console.Clear();
				break;
				default:
					Console.WriteLine("\nInvalid Character, please try again!\n");
				break;
               }
		break;

		case 0:
			RunProgram = false;
		break;
		
		default:
			Console.WriteLine("\nInvalid Character, please try again!\n");
		break;
        }
	
      }

	}
}