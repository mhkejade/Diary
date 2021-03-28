# Diary
Basic Diary App

# Instructions
1. Setup manually a local sql server express database (see enhancements). Execute the table create scripts in Diary/DBComponents folder.
2. update appsettings.json (Diary project) for the correct connectionstrings.
3. open solution in VS. launch application via IIS Express. should launch as localhost:7001

# Enhancements
1. use docker for sql server express as not to setup db manually OR
2. use SQL Lite in memory db
3. use EF instead of tsql datamanager services
4. enhance unit test
5. add functional test if using a different db
6. implement proper RESTful
