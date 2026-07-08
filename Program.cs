// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

//Exercise 1: The First Safety Net (LO 1.1: Environment + Null Safety)

//Step 1 — See What the Compiler Catches
    // string region = null ;
    // Console.WriteLine(region.ToUpper());
// Step 2 — Fix It Three Ways
           /* string? region = null;

            string? upperRegion = region?.ToUpper();
            Console.WriteLine($"Region (conditional): {upperRegion}");

            string displayRegion = region ?? "Unassigned";
            Console.WriteLine($"Region (coalesced): {displayRegion}");

            region ??= "Addis Ababa";
            Console.WriteLine($"Region (assigned): {region}");
            */
// Step 3 — Declare Your First TMS Variables
/*
string studentName = "Abeba";
string studentId = "STU-001";
int enrollmentCount = 3;
decimal grantAmount = 1999.99m; // 'm' suffix marks a decimal literal
DateTime enrolledAt = DateTime.UtcNow;
string? campusRegion = null;
Console.WriteLine($"Student: {studentName} ({studentId})");
Console.WriteLine($"Courses: {enrollmentCount}");
Console.WriteLine($"Grant: {grantAmount:F2}");
Console.WriteLine($"Enrolled: {enrolledAt:yyyy-MM-dd}");
Console.WriteLine($"Campus: {campusRegion ?? "Not assigned"}");
*/
//Exercise 2: The Ministry Audit Failure (LO 1.2: Primitives)
// Step 1 — See the Bug
/*
double grantPerStudent = 1999.99;
double totalAllocation = grantPerStudent * 100_000;
Console.WriteLine($"Total allocated (double): {totalAllocation}");
*/
//Step 2 — Fix It
decimal grantPerStudent = 1999.99m;
decimal totalAllocation = grantPerStudent * 100_000m;
Console.WriteLine($"Total allocated (decimal): {totalAllocation}");
Console.WriteLine($"Total allocated (formatted): {totalAllocation:F2}");

//Exercise 3: Pipeline Data Corruption (LO 1.3 & 1.4: Encapsulation)
var enrollment = new EnrollmentRecord(
    "STU-001",
    "CS-401",
    DateTime.UtcNow
);

Console.WriteLine(enrollment);

// Try this later to see the compiler error:
// enrollment.CourseCode = "HACKED";

var corrected = enrollment with
{
    CourseCode = "CS-402"
};

Console.WriteLine(corrected);

var duplicate = new EnrollmentRecord(
    "STU-001",
    "CS-401",
    enrollment.EnrolledAt
);

Console.WriteLine($"Same data? {enrollment == duplicate}");
//Exercise 3 — Part 2: Course Capacity with the field Keyword
var course = new Course
{
    Code = "CS-401",
    Title = "Advanced C#",
    Capacity = 30
};

Console.WriteLine($"Course: {course.Title} (Capacity: {course.Capacity})");

// Invalid capacity
try
{
    course.Capacity = -5;
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}

// Invalid title
try
{
    course.Title = "";
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}
// Exercise 3B: Interface Contract Wiring (LO 1.4: OOP Contracts)
void PrintGradeReport(IEnumerable<IGradable> assessments)
{
    Console.WriteLine("--- Grade Report ---");

    foreach (var item in assessments)
    {
        Console.WriteLine(
            $"{item.Title}: {item.CalculateGrade():F2}%"
        );
    }
}

IGradable[] cohortAssessments =
[
    new Quiz
    {
        Title = "C# Basics",
        CorrectAnswers = 18,
        TotalQuestions = 20
    },

    new LabAssignment
    {
        Title = "Registration API",
        FunctionalityScore = 90m,
        CodeQualityScore = 85m
    }
];


PrintGradeReport(cohortAssessments);
