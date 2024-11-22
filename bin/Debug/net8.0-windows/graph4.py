import sqlite3
import pandas as pd
import matplotlib.pyplot as plt
import os 

# Connect to the SQLite database
conn = sqlite3.connect('D:\\University Studies\\3rd_Semester\\DSA\\Study Hub\\StudyHub\\bin\\Debug\\net8.0-windows\\GradeTracker.db')  # Ensure the database file is in the same directory or provide the correct path.

# Load data into a pandas DataFrame
query = "SELECT * FROM Grades"  # Replace 'Grades' with your table name if different
df = pd.read_sql_query(query, conn)

# Group by Subject and Test Type to calculate average grades
avg_grades_by_test_type = df.groupby(['Subject', 'TestType'])['Percentage'].mean().unstack()

# Create a stacked bar chart
avg_grades_by_test_type.plot(kind='bar', stacked=True, figsize=(8, 6), color=['skyblue', 'grey'])

# Customize the chart
plt.title('Grade Distribution by Subject and Test Type')
plt.ylabel('Average Grade')
plt.xlabel('Subject')
plt.xticks(rotation=45)
plt.legend(title='Test Type', loc='upper left')
plt.tight_layout()

# Show the plot
current_directory = os.path.dirname(os.path.abspath(__file__))

# Create the full path for the plot file
plot_path = os.path.join(current_directory, "plot4.png")

# Save the plot
plt.savefig(plot_path)

# Optionally, close the plot to free resources
# plt.close()
plt.close()
