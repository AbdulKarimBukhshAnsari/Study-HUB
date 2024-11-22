import sqlite3
import pandas as pd
import matplotlib.pyplot as plt
import os 

# Connect to the SQLite database
conn = sqlite3.connect('D:\\University Studies\\3rd_Semester\\DSA\\Study Hub\\StudyHub\\bin\\Debug\\net8.0-windows\\GradeTracker.db')  # Ensure the database file is in the same directory or provide the correct path.

# Load data into a pandas DataFrame
query = "SELECT * FROM Grades"  # Replace 'Grades' with your table name if different
df = pd.read_sql_query(query, conn)

# Count the number of occurrences of each test type
test_type_counts = df['TestType'].value_counts()

# Create a pie chart
plt.figure(figsize=(6, 6))
test_type_counts.plot(kind='pie', autopct='%1.1f%%', startangle=90, colors=['skyblue', 'grey'])

# Customize the chart
plt.title('Distribution of Test Types')
plt.ylabel('')  # Hide the y-axis label
plt.tight_layout()

# Show the plot
current_directory = os.path.dirname(os.path.abspath(__file__))

# Create the full path for the plot file
plot_path = os.path.join(current_directory, "plot3.png")

# Save the plot
plt.savefig(plot_path)

# Optionally, close the plot to free resources
# plt.close()
plt.close()
