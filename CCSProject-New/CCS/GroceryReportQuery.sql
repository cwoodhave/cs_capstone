SELECT a.AgencyName, x.Description, SUM(x.total_weight) AS Weight
FROM Agencies a
JOIN DonationTransactions t
	ON t.AgencyID = a.AgencyID
JOIN (SELECT f.Description, l.DonationTransactionID, SUM(l.weight) AS total_weight
		FROM TransactionLineItems l 
		JOIN FoodCategories f
			ON f.FoodCategoryID = l.FoodCategoryID
			AND f.FoodCategoryType = 2
		GROUP BY f.Description, l.DonationTransactionID) x ON x.DonationTransactionID = t.DonationTransactionID
GROUP BY a.AgencyName, x.Description
ORDER BY a.AgencyName, x.Description