using System.Collections.Generic;
using System;
            
namespace Retire
{
	public enum BudgetType
	{
		Auto,
		Auto_Gas,
		Auto_Insurance,
		Digital,
		Digital_Movies,
		Digital_Music,
		Digital_Subscription,
		Entertainment,
		Entertainment_Movies,
		Entertainment_SportingEvents,
		Home,
		Home_Mortgage,
		Income,
		Income_Airbnb,
		Income_Unemployment,
		Income_Salary,
		Income_Misc,
		Medical,
		Medical_Insurance,
		Shopping,
		Shopping_Subscription,
		Utilities,
		Utilities_Electricity,
		Utilities_Gas, 
		Utilities_InternetCable,
		Utilities_WaterSewerWaste,
		Gift,
		Gift_FamilyFriends
	}

	public class BudgetCategory
	{
		public BudgetType BudgetType;
		public string MainCategory { get; private set; }
		public string SubCategory { get; private set; }

		public BudgetCategory(BudgetType budgetType)
		{
			BudgetType = budgetType;
			var mainAndSub = budgetType.ToString().Split('_');
			MainCategory = mainAndSub[0];
			SubCategory = mainAndSub.Length > 1 ? mainAndSub[1] : "";
		}

		public override string ToString()
		{
			return string.Format($"{MainCategory}:{SubCategory}");
		}
	}

	public static class BudgetCategoryFactory
	{
		private static Dictionary<BudgetType, BudgetCategory> _budgetCategory = new Dictionary<BudgetType, BudgetCategory>();

		public static BudgetCategory GetBudgetCategory(BudgetType budgetType)
		{
			return _budgetCategory[budgetType];
		}

		public static BudgetType GetBudgetType(string mainCategory, string subCategory)
		{
			foreach (var kvp in _budgetCategory)
			{
				if (kvp.Value.MainCategory.Equals(mainCategory) && kvp.Value.SubCategory.Equals(subCategory))
					return kvp.Key;
			}
			throw new ArgumentException($"Invalid Categories: {mainCategory}:{subCategory}");
		}

		public static ICollection<string> GetExpenseCategories()
		{
			var mainCategories = new HashSet<string>();
			foreach (var kvp in _budgetCategory)
			{
				var mainCategory = kvp.Value.MainCategory;
				if (mainCategory != "Income")
				   	mainCategories.Add(kvp.Value.MainCategory);
			}
			return mainCategories;
		}

		public static ICollection<string> GetIncomeCategories()
		{
			var incomeCategories = new HashSet<string>();
			foreach (var kvp in _budgetCategory)
			{
				var category = kvp.Value;
				if (category.MainCategory == "Income")
					incomeCategories.Add(kvp.Value.SubCategory);
			}
			return incomeCategories;
		}

		static BudgetCategoryFactory()
		{
			foreach (BudgetType budgetType in Enum.GetValues(typeof(BudgetType)))
			{
				_budgetCategory.Add(budgetType, new BudgetCategory(budgetType));
			}
		}
	}
}