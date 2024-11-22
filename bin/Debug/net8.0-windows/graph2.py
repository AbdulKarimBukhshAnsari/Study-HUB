import sqlite3
import pandas as pd
import matplotlib.pyplot as plt
import os 

# Connect to the SQLite database
conn = sqlite3.connect('D:\\University Studies\\3rd_Semester\\DSA\\Study Hub\\StudyHub\\bin\\Debug\\net8.0-windows\\GradeTracker.db')  # Ensure the database file is in the same directory or provide the correct path.

# Load data into a pandas DataFrame
query = "SELECT * FROM Grades"  # Replace 'Grades' with your table name if different
df = pd.read_sql_query(query, conn)

# Calculate the average grade per month
# Calculate the average grade per subject
avg_grades = df.groupby('Subject')['Percentage'].mean()

# Create a bar chart
plt.figure(figsize=(8, 6))
avg_grades.plot(kind='bar', color='skyblue')

# Customize the chart
plt.title('Average Grades by Subject')
plt.ylabel('Average Grade')
plt.xlabel('Subject')
plt.xticks(rotation=45)
plt.tight_layout()

# Show the plot

current_directory = os.path.dirname(os.path.abspath(__file__))

# Create the full path for the plot file
plot_path = os.path.join(current_directory, "plot2.png")

# Save the plot
plt.savefig(plot_path)

# Optionally, close the plot to free resources
# plt.close()
plt.close()
