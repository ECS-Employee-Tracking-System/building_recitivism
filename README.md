## ECS Employee Tracking System
Made with love by Christopher Kingdon, Austin Dolby, Michael Dimmitt, Jerrad Monagan

### Dashboard Page (View/Only and Admin)

### Color Legend: 
located in the top left corner will affect the Dashboard by changing the cells that contain certifications nearing or having passed the expiration date. 

### Pagination
Pagination defaults to 10. 
<br>to change how many records show on the dashboard per page:
<br>go to the dashboard, a "Show" box is visible in the left center of the page.
<br>Allowing users to toggle how many entries they want to set as the page default.

### Current Filter 
Dashboard by default gives all results with no filter. 

When more than zero filters exist the drop_down below Dash Board can be selected allowing the click of "Load Filter" button to quickly load that filter to the dashboard.

Filters can be constructed with the "Create Filter Button"

The Filter enabled on the page is considered the current filter. 

A view only user can manipulate the current view but not make changes to filters past the current session (no changes to the database). View only users are able to load filters, toggle columns, change the number of records shown in the current view and search to modify the current view. After changing the current view to an acceptable format, a user is able to export the data on the page to excel for further analysis. 

If a user wants to persist a specific filter they must request an admin to build that view for them. 

### Dashboard Page (Admin)

### Edit Column
an edit column should always be present as the leftmost column on the page. 
<br>allowing easy editing for tracking information.

### Current Filter Operations
Current filter can be Searched, Edited, Saved, Exported to excel, Modified to different Columns, or Deleted.

If you wanted to make a new filter right away or delete a specific filter that is available via buttons. 

The dashboard will readjust the current filter to default filter after a filter is deleted.

### Create New Filter
"Create New Filter, located on the admin dashboard allows a search action to take place which will show in the resultant dashboard output. This feature allows multiple options for FirstName, LastName, Email, Position, Completed Certifications, Education Level, Education Type, Education Details, Center Names, Center Counties, Center Region.

Other fields include: Begin Date of Hire, End Date of Hire, Goal, Mid Year, End Year, Goal Met, Class Completed, Class Paid, Begin Required Hours, End Required Hours, Begin Hours Earned, End Hours Earned, Begin Term Date, End Term Date, TimeUntilExpire(meaning certs), should check position requirements, check whether user is Inactive.

### View Only User / Admin  Workflow: 

How to log into the system:
1. Login (center of screen)

How to reset password:
1. After logging in click the tap in the top right of the screen.

How to view the Dashboard
1. After logging into the system the Dashboard should display. 
2. If the user ever needs to get back to the Dashboard they can use the navigation bar at the top of the screen. 

How to export an excel report for your own filtering and pie charts:
1. After logging into the system the Dashboard should display.
2. Click the "Export Current View to Excel" button at the right center of the page.

### Admin User Workflow

#### How to enter a new manager with View Only or Admin permissions:
1. Login -> (resolves to DashBoard page) -> click "Admin" in the navbar at top of page.
2. Look under "Maintenance section"
3. click "Manage User Accounts" on the admin dashboard and fill out instructions on the page.

#### How to enter an employee for tracking and assign them predefined certifications:
1. Login -> (resolves to DashBoard page) -> click "Admin" in the navbar at top of page. 
2. Look under Manage Staff
3. click "Add Staff Member" on the admin dashboard and fill out instructions on the page.

#### How to Add and Edit Positions and Certifications
1. Login -> (resolves to DashBoard page) -> click "Admin" in the navbar at top of page.
2. Look under "Maintenance section"
3. click "Edit Positions" or "Edit Certs"
4. A view will display the current information along with buttons to add, edit or delete values.

#### When the tracking year ends and all employees must have the tracking reset regardless of progress.
1. Login -> (resolves to DashBoard page) -> click "Admin" in the navbar at top of page.
2. Look under "Maintenance section"
3. click "Annual Reset"
4. A warning should appear before any change occurs.

#### How to create a new center:
1. Login -> (resolves to DashBoard page) -> click "Center" in the navbar at top of page.
2. Buttons should be present giving the ability to Create new centers and filter out unused centers for deletion.

### Hardware Information:
Program runs with Asp.net

Certifications are built through metadata columns in the sql database.

If the employees, request the full data exported for the application, an internal developer should be able to write a query in the sql database to provide an export the information. By left Joining all the tables.
