// ============================================
// STUDENT OBJECT ARRAY DEMO - JavaScript
// ============================================

// NOTE: An array of objects is a common way to store structured data
// Each object represents a student with multiple properties

// ----- 1. CREATING AN ARRAY OF STUDENT OBJECTS -----
const students = [
    { id: 1, name: "Alice", age: 20, grade: 85, course: "Computer Science" },
    { id: 2, name: "Bob", age: 22, grade: 72, course: "Mathematics" },
    { id: 3, name: "Charlie", age: 21, grade: 90, course: "Physics" },
    { id: 4, name: "Diana", age: 19, grade: 68, course: "Computer Science" },
    { id: 5, name: "Eve", age: 23, grade: 95, course: "Mathematics" }
];

console.log("=== ALL STUDENTS ===");
console.log(students);


// ----- 2. ACCESSING OBJECTS IN THE ARRAY -----
// NOTE: Use index to access specific student (0-based indexing)

console.log("\n=== ACCESSING SPECIFIC STUDENT ===");
console.log("First student:", students[0]);           // Gets first student
console.log("First student's name:", students[0].name); // Gets property of first student


// ----- 3. LOOPING THROUGH ARRAY OF OBJECTS -----

// Method 1: for loop
console.log("\n=== FOR LOOP ===");
for (let i = 0; i < students.length; i++) {
    console.log(`Student ${i + 1}: ${students[i].name} - Grade: ${students[i].grade}`);
}

// Method 2: for...of loop (cleaner syntax)
console.log("\n=== FOR...OF LOOP ===");
for (const student of students) {
    console.log(`${student.name} is studying ${student.course}`);
}

// Method 3: forEach method
console.log("\n=== FOREACH METHOD ===");
students.forEach((student, index) => {
    console.log(`${index + 1}. ${student.name} (Age: ${student.age})`);
});


// ----- 4. FILTER - Finding students that match a condition -----
// NOTE: filter() returns a NEW array with elements that pass the test

console.log("\n=== FILTER: Students with grade >= 80 ===");
const highScorers = students.filter(student => student.grade >= 80);
console.log(highScorers);

console.log("\n=== FILTER: Computer Science students ===");
const csStudents = students.filter(student => student.course === "Computer Science");
console.log(csStudents);


// ----- 5. MAP - Transforming array elements -----
// NOTE: map() returns a NEW array with transformed elements

console.log("\n=== MAP: Get all student names ===");
const names = students.map(student => student.name);
console.log(names); // ["Alice", "Bob", "Charlie", "Diana", "Eve"]

console.log("\n=== MAP: Create summary strings ===");
const summaries = students.map(student => `${student.name}: ${student.grade}%`);
console.log(summaries);


// ----- 6. FIND - Finding a single object -----
// NOTE: find() returns the FIRST element that matches (not an array)

console.log("\n=== FIND: Student with id 3 ===");
const studentById = students.find(student => student.id === 3);
console.log(studentById);

console.log("\n=== FIND: First student over 21 ===");
const over21 = students.find(student => student.age > 21);
console.log(over21);


// ----- 7. FINDINDEX - Finding position of an object -----
// NOTE: findIndex() returns the INDEX of the first match, or -1 if not found

console.log("\n=== FINDINDEX: Position of Bob ===");
const bobIndex = students.findIndex(student => student.name === "Bob");
console.log("Bob is at index:", bobIndex);







// ============================================
// SUMMARY OF KEY METHODS:
// ============================================
// filter()    - Returns new array with elements that pass test
// map()       - Returns new array with transformed elements
// find()      - Returns first element that matches
// findIndex() - Returns index of first match
// some()      - Returns true if any element passes test
// every()     - Returns true if all elements pass test
// reduce()    - Returns single value calculated from array
// sort()      - Sorts array (modifies original)
// forEach()   - Executes function for each element (no return)
// push()      - Adds to end of array
// pop()       - Removes from end of array
// ============================================
