import sqlite3
import pandas as pd
import matplotlib.pyplot as plt
import os 

# Connect to the SQLite database
conn = sqlite3.connect("D:\\University Studies\\3rd_Semester\\DSA\\Study Hub\\StudyHub\\bin\\Debug\\net8.0-windows\\GradeTracker.db")  # Ensure the database file is in the same directory or provide the correct path.

# Load data into a pandas DataFrame
query = "SELECT * FROM Grades"  # Replace 'Grades' with your table name if different
df = pd.read_sql_query(query, conn)

# Display the loaded data (optional)
# Calculate the average grade per subject
# Calculate the average grade per month
monthly_trends = df.groupby('Month')['Percentage'].mean()

# Create a line chart
plt.figure(figsize=(8, 6))
plt.plot(monthly_trends.index, monthly_trends.values, marker='o', linestyle='-', color='skyblue', label='Average Grade', linewidth = 3)

# Customize the chart
plt.title('Monthly Grade Trends')
plt.xlabel('Month')
plt.ylabel('Average Grade')
plt.xticks(rotation=45)
plt.grid()
plt.legend()
plt.tight_layout()

# Show the plot

# Get the current directory where the script is located
current_directory = os.path.dirname(os.path.abspath(__file__))

# Create the full path for the plot file
plot_path = os.path.join(current_directory, "plot1.png")

# Save the plot
plt.savefig(plot_path)

# Optionally, close the plot to free resources
plt.close()

