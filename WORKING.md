# Updated by NGUYEN DINH DUC

1. Understand requirements, init git repo (add angular project also): 1 hours
2. Implement features for Back-End: 3 hours
   - Apply repository pattern
   - Use UseInMemoryDatabase
   - Use CsvHelper to read data from Csv file and save to InMemoryDatabase
3. Implement features for Front-End: 3 hours
4. Define Response/Request object: 30 mins
5. Implement filter and sort function: 1 hours
6. Implement paging on UI: 30 mins
7: Update UI and fix some issues: 1 hours

Grand total: 10 hours


# Run 
1. cd ./app
2. npm install
3. npm start --> Angular App will work on port 8080
4. run Back-End application --> It will work on port 5000

# Need to update
1. Create a database and create a stored procedure for searching if the data growing.
2. Lazy loading for any additional module into Angular App, currently, it's unnecessary.
3. Should create an abstract service to implement all common API request such as CRUD, search...