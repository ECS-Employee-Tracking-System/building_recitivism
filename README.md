# building_recitivism
main application for Collaborative Senior Project class

Issues will be listed in this repository and linke in the projects tab to be imported into jira.

## Stored Procedures
**GetCenter (@CenterID)**
Allows for the retrieval of one center an all its staffmemers by using @CenterId param or no param returns all centers and their respective staffmembers ordered by the centers Region, then name  both ascending

**GetStaffMember (@StaffMemberId)**
Allows the user to get a list of StaffMembers with their associated education and centers, use the @StaffMemberID to retrieve just one StaffMember

**AddNewStaffMember (@CenterName,@CenterCounty,@CenterRegion,@DegreeAbrv,@DegreeLevel,@DegreeType,@DegreeDetail,@FirstName,@LastName,@Email,@DateOfHire,@Position,@DirectorCredentials,@DCExpiration,@CDAInProgress,@CDAType,@CDAExpiration,@CDARenewalProcess,@Comments,@Goal,@MidYear,@EndYear,@GoalMet,@TAndAApp,@AppApp,@ClassCompleted,@ClassPaid,@RequiredHours,@HoursEarned,@Notes,@TermDate)**
All nullable except @FirstName and @LastName
Allows the user to Add a new staff member with every available option, procedure will check @CenterID against @CenterName, @EducationID against @DegreeAbbrv and @DegreeType
--@StaffMemberId against @FirstName and LastName to see if item already exist in the database  
