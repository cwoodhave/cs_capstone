SELECT f.Description, l.DonationTransactionID, SUM(l.weight) AS total_weight
FROM TransactionLineItems l 
JOIN FoodCategories f
	ON f.FoodCategoryID = l.FoodCategoryID
	AND f.FoodCategoryType = 2
GROUP BY f.Description, l.DonationTransactionID