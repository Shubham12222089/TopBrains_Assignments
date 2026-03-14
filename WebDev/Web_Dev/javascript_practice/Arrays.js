// ============================================
// JAVASCRIPT ARRAYS - COMPLETE GUIDE
// ============================================

// ============================================
// 1. CREATING ARRAYS
// ============================================

// Method 1: Array Literal (Most Common)
var colors = ["Red", "Green", "Blue", "Pink", "Yellow"];

// Method 2: Array Constructor
var numbers = new Array(1, 2, 3, 4, 5);

// Method 3: Empty Array
var emptyArray = [];

// Method 4: Array with size (creates array with empty slots)
var sized = new Array(5); // Creates array with 5 empty slots

// Method 5: Array.of() - creates array from arguments
var arrOf = Array.of(1, 2, 3);

// Method 6: Array.from() - creates array from iterable
var arrFrom = Array.from("Hello"); // ['H', 'e', 'l', 'l', 'o']

console.log("=== Creating Arrays ===");
console.log("colors:", colors);
console.log("Array.from('Hello'):", arrFrom);

// ============================================
// 2. ACCESSING ARRAY ELEMENTS
// ============================================
console.log("\n=== Accessing Elements ===");

// By index (0-based)
console.log("First element:", colors[0]);
console.log("Last element:", colors[colors.length - 1]);

// Using at() method (supports negative index)
console.log("Using at(0):", colors.at(0));
console.log("Using at(-1):", colors.at(-1)); // Last element

// ============================================
// 3. ARRAY PROPERTIES
// ============================================
console.log("\n=== Array Properties ===");
console.log("Length:", colors.length);
console.log("Is Array:", Array.isArray(colors));

// ============================================
// 4. ADDING ELEMENTS
// ============================================
console.log("\n=== Adding Elements ===");

var fruits = ["Apple", "Banana"];
console.log("Original:", fruits);

// push() - adds to end, returns new length
fruits.push("Orange");
console.log("After push('Orange'):", fruits);

// unshift() - adds to beginning, returns new length
fruits.unshift("Mango");
console.log("After unshift('Mango'):", fruits);

// splice() - adds at specific position
fruits.splice(2, 0, "Grape"); // At index 2, delete 0, add 'Grape'
console.log("After splice(2, 0, 'Grape'):", fruits);

// ============================================
// 5. REMOVING ELEMENTS
// ============================================
console.log("\n=== Removing Elements ===");

var items = ["A", "B", "C", "D", "E"];
console.log("Original:", items);

// pop() - removes from end, returns removed element
var popped = items.pop();
console.log("After pop():", items, "| Removed:", popped);

// shift() - removes from beginning, returns removed element
var shifted = items.shift();
console.log("After shift():", items, "| Removed:", shifted);

// splice() - removes at specific position
var spliced = items.splice(1, 1); // At index 1, delete 1 element
console.log("After splice(1, 1):", items, "| Removed:", spliced);

// delete - removes element but leaves hole (not recommended)
var deleteArr = [1, 2, 3];
delete deleteArr[1];
console.log("After delete arr[1]:", deleteArr); // [1, empty, 3]

// ============================================
// 6. MODIFYING ELEMENTS
// ============================================
console.log("\n=== Modifying Elements ===");

var nums = [10, 20, 30, 40, 50];
console.log("Original:", nums);

// Direct assignment
nums[2] = 300;
console.log("After nums[2] = 300:", nums);

// splice() - replace elements
nums.splice(1, 2, 200, 250); // At index 1, delete 2, add 200 and 250
console.log("After splice(1, 2, 200, 250):", nums);

// fill() - fill with value
var fillArr = [1, 2, 3, 4, 5];
fillArr.fill(0, 2, 4); // Fill with 0 from index 2 to 4 (exclusive)
console.log("After fill(0, 2, 4):", fillArr);

// copyWithin() - copy part of array to another location
var copyArr = [1, 2, 3, 4, 5];
copyArr.copyWithin(0, 3); // Copy from index 3 to index 0
console.log("After copyWithin(0, 3):", copyArr);

// ============================================
// 7. SEARCHING IN ARRAYS
// ============================================
console.log("\n=== Searching in Arrays ===");

var searchArr = ["Apple", "Banana", "Orange", "Apple", "Grape"];

// indexOf() - first occurrence index (-1 if not found)
console.log("indexOf('Apple'):", searchArr.indexOf("Apple"));
console.log("indexOf('Mango'):", searchArr.indexOf("Mango"));

// lastIndexOf() - last occurrence index
console.log("lastIndexOf('Apple'):", searchArr.lastIndexOf("Apple"));

// includes() - returns boolean
console.log("includes('Banana'):", searchArr.includes("Banana"));
console.log("includes('Mango'):", searchArr.includes("Mango"));

// find() - returns first element that passes test
var numArr = [5, 12, 8, 130, 44];
var found = numArr.find(num => num > 10);
console.log("find(num > 10):", found);

// findIndex() - returns index of first element that passes test
var foundIndex = numArr.findIndex(num => num > 10);
console.log("findIndex(num > 10):", foundIndex);

// findLast() - returns last element that passes test
var foundLast = numArr.findLast(num => num > 10);
console.log("findLast(num > 10):", foundLast);

// findLastIndex() - returns index of last element that passes test
var foundLastIndex = numArr.findLastIndex(num => num > 10);
console.log("findLastIndex(num > 10):", foundLastIndex);

// ============================================
// 8. ITERATING ARRAYS
// ============================================
console.log("\n=== Iterating Arrays ===");

var iterArr = ["A", "B", "C"];

// forEach() - executes function for each element
console.log("forEach:");
iterArr.forEach((element, index) => {
    console.log(`  Index ${index}: ${element}`);
});

// for loop
console.log("for loop:");
for (var i = 0; i < iterArr.length; i++) {
    console.log(`  Index ${i}: ${iterArr[i]}`);
}

// for...of loop (ES6)
console.log("for...of:");
for (var item of iterArr) {
    console.log(`  ${item}`);
}

// for...in loop (gets indices as strings)
console.log("for...in:");
for (var index in iterArr) {
    console.log(`  Index ${index}: ${iterArr[index]}`);
}

// entries() - returns iterator with key/value pairs
console.log("entries:");
for (var [index, value] of iterArr.entries()) {
    console.log(`  Index ${index}: ${value}`);
}

// keys() - returns iterator with keys
console.log("keys:", [...iterArr.keys()]);

// values() - returns iterator with values
console.log("values:", [...iterArr.values()]);

// ============================================
// 9. TRANSFORMING ARRAYS
// ============================================
console.log("\n=== Transforming Arrays ===");

var transformArr = [1, 2, 3, 4, 5];

// map() - creates new array with results of function
var doubled = transformArr.map(num => num * 2);
console.log("Original:", transformArr);
console.log("map(num * 2):", doubled);

// filter() - creates new array with elements that pass test
var filtered = transformArr.filter(num => num > 2);
console.log("filter(num > 2):", filtered);

// reduce() - reduces array to single value
var sum = transformArr.reduce((acc, num) => acc + num, 0);
console.log("reduce (sum):", sum);

var product = transformArr.reduce((acc, num) => acc * num, 1);
console.log("reduce (product):", product);

// reduceRight() - same as reduce but from right to left
var rightReduce = ["a", "b", "c"].reduceRight((acc, char) => acc + char, "");
console.log("reduceRight:", rightReduce);

// flat() - flattens nested arrays
var nested = [1, [2, 3], [4, [5, 6]]];
console.log("Original nested:", nested);
console.log("flat(1):", nested.flat(1));
console.log("flat(2):", nested.flat(2));
console.log("flat(Infinity):", nested.flat(Infinity));

// flatMap() - map then flat
var sentences = ["Hello World", "Good Morning"];
var words = sentences.flatMap(sentence => sentence.split(" "));
console.log("flatMap (split sentences):", words);

// ============================================
// 10. SORTING ARRAYS
// ============================================
console.log("\n=== Sorting Arrays ===");

// sort() - sorts in place (converts to strings by default)
var sortArr = [40, 100, 1, 5, 25];
console.log("Original:", [...sortArr]);
console.log("Default sort():", sortArr.sort()); // Wrong for numbers!

// Numeric sort ascending
sortArr = [40, 100, 1, 5, 25];
console.log("Numeric ascending:", sortArr.sort((a, b) => a - b));

// Numeric sort descending
sortArr = [40, 100, 1, 5, 25];
console.log("Numeric descending:", sortArr.sort((a, b) => b - a));

// String sort (case-insensitive)
var strSort = ["banana", "Apple", "cherry", "Date"];
console.log("Case-insensitive sort:", strSort.sort((a, b) => 
    a.toLowerCase().localeCompare(b.toLowerCase())
));

// reverse() - reverses array in place
var revArr = [1, 2, 3, 4, 5];
console.log("reverse():", revArr.reverse());


// ============================================
// 11. COMBINING & SLICING ARRAYS
// ============================================
console.log("\n=== Combining & Slicing Arrays ===");

// concat() - combines arrays (returns new array)
var arr1 = [1, 2, 3];
var arr2 = [4, 5, 6];
var combined = arr1.concat(arr2, [7, 8]);
console.log("concat:", combined);

// spread operator (ES6) - another way to combine
var spread = [...arr1, ...arr2];
console.log("spread operator:", spread);

// slice() - extracts portion (returns new array)
var sliceArr = ["A", "B", "C", "D", "E"];
console.log("slice(1, 4):", sliceArr.slice(1, 4)); // B, C, D
console.log("slice(2):", sliceArr.slice(2)); // C, D, E
console.log("slice(-2):", sliceArr.slice(-2)); // D, E
console.log("Original unchanged:", sliceArr);


// ============================================
// 12. TESTING ARRAY ELEMENTS
// ============================================
console.log("\n=== Testing Array Elements ===");

var testArr = [2, 4, 6, 8, 10];

// every() - tests if ALL elements pass
console.log("every(num % 2 === 0):", testArr.every(num => num % 2 === 0));
console.log("every(num > 5):", testArr.every(num => num > 5));

// some() - tests if ANY element passes
console.log("some(num > 5):", testArr.some(num => num > 5));
console.log("some(num > 10):", testArr.some(num => num > 10));

// ============================================
// 13. CONVERTING ARRAYS
// ============================================
console.log("\n=== Converting Arrays ===");

var convArr = ["Apple", "Banana", "Orange"];

// join() - converts to string with separator
console.log("join():", convArr.join());
console.log("join(' - '):", convArr.join(" - "));
console.log("join(''):", convArr.join(""));

// toString() - converts to comma-separated string
console.log("toString():", convArr.toString());

// String to Array
var str = "Hello,World,JavaScript";
console.log("split(','):", str.split(","));

// Array to Object
var objArr = ["a", "b", "c"];
var obj = { ...objArr };
console.log("Array to Object:", obj);

// ============================================
// 14. DESTRUCTURING ARRAYS
// ============================================
console.log("\n=== Destructuring Arrays ===");

var destructArr = [1, 2, 3, 4, 5];

// Basic destructuring
var [first, second, third] = destructArr;
console.log("first, second, third:", first, second, third);

// Skip elements
var [a, , c] = destructArr;
console.log("Skip second element:", a, c);

// Rest pattern
var [head, ...tail] = destructArr;
console.log("head:", head);
console.log("tail:", tail);

// Default values
var [x, y, z = 10] = [1, 2];
console.log("Default value:", x, y, z);

// Swapping variables
var m = 1, n = 2;
[m, n] = [n, m];
console.log("Swapped:", m, n);

// ============================================
// 15. SPREAD & REST OPERATORS
// ============================================
console.log("\n=== Spread & Rest Operators ===");

// Spread - expands array
var spreadArr = [1, 2, 3];
console.log("Spread in array:", [0, ...spreadArr, 4]);
console.log("Spread in function:", Math.max(...spreadArr));

// Copy array (shallow)
var original = [1, 2, 3];
var copy = [...original];
console.log("Copy:", copy);

// Rest - collects into array
function sumAll(...numbers) {
    return numbers.reduce((a, b) => a + b, 0);
}
console.log("Rest in function:", sumAll(1, 2, 3, 4, 5));

// ============================================
// 16. MULTIDIMENSIONAL ARRAYS
// ============================================
console.log("\n=== Multidimensional Arrays ===");

// 2D Array (Matrix)
var matrix = [
    [1, 2, 3],
    [4, 5, 6],
    [7, 8, 9]
];

console.log("Matrix:");
matrix.forEach(row => console.log("  ", row));

// Accessing elements
console.log("matrix[1][2]:", matrix[1][2]); // 6

// Iterating 2D array
console.log("Iterating matrix:");
for (var row = 0; row < matrix.length; row++) {
    for (var col = 0; col < matrix[row].length; col++) {
        console.log(`  [${row}][${col}] = ${matrix[row][col]}`);
    }
}

// Flatten 2D array
console.log("Flattened:", matrix.flat());

// ============================================
// 17. ARRAY-LIKE OBJECTS
// ============================================
console.log("\n=== Array-Like Objects ===");

// Convert NodeList, arguments, strings to arrays
var arrayLike = { 0: "a", 1: "b", 2: "c", length: 3 };
var realArray = Array.from(arrayLike);
console.log("Array.from(arrayLike):", realArray);

// Using spread on iterable
var strToArr = [..."Hello"];
console.log("Spread string:", strToArr);

// ============================================
// 18. USEFUL ARRAY PATTERNS
// ============================================
console.log("\n=== Useful Patterns ===");

// Remove duplicates
var duplicates = [1, 2, 2, 3, 3, 3, 4];
var unique = [...new Set(duplicates)];
console.log("Remove duplicates:", unique);

// Get random element
var randArr = ["Apple", "Banana", "Orange", "Grape"];
var randomElement = randArr[Math.floor(Math.random() * randArr.length)];
console.log("Random element:", randomElement);

// Shuffle array (Fisher-Yates)
function shuffle(array) {
    var arr = [...array];
    for (var i = arr.length - 1; i > 0; i--) {
        var j = Math.floor(Math.random() * (i + 1));
        [arr[i], arr[j]] = [arr[j], arr[i]];
    }
    return arr;
}
console.log("Shuffled:", shuffle([1, 2, 3, 4, 5]));

// Chunk array
function chunk(array, size) {
    var result = [];
    for (var i = 0; i < array.length; i += size) {
        result.push(array.slice(i, i + size));
    }
    return result;
}
console.log("Chunk [1,2,3,4,5] by 2:", chunk([1, 2, 3, 4, 5], 2));

// Group by
var people = [
    { name: "Alice", age: 25 },
    { name: "Bob", age: 30 },
    { name: "Charlie", age: 25 }
];
var groupedByAge = people.reduce((groups, person) => {
    var key = person.age;
    if (!groups[key]) groups[key] = [];
    groups[key].push(person);
    return groups;
}, {});
console.log("Grouped by age:", groupedByAge);

// Object.groupBy() - Modern way (ES2024)
// var grouped = Object.groupBy(people, person => person.age);

// Count occurrences
var letters = ["a", "b", "a", "c", "b", "a"];
var counts = letters.reduce((acc, letter) => {
    acc[letter] = (acc[letter] || 0) + 1;
    return acc;
}, {});
console.log("Count occurrences:", counts);

// Max/Min in array
var maxMinArr = [3, 1, 4, 1, 5, 9, 2, 6];
console.log("Max:", Math.max(...maxMinArr));
console.log("Min:", Math.min(...maxMinArr));

// Sum and Average
var sumArr = [1, 2, 3, 4, 5];
var total = sumArr.reduce((a, b) => a + b, 0);
var average = total / sumArr.length;
console.log("Sum:", total);
console.log("Average:", average);

// Intersection of arrays
var setA = [1, 2, 3, 4];
var setB = [3, 4, 5, 6];
var intersection = setA.filter(x => setB.includes(x));
console.log("Intersection:", intersection);

// Difference of arrays
var difference = setA.filter(x => !setB.includes(x));
console.log("Difference (A - B):", difference);

// Union of arrays
var union = [...new Set([...setA, ...setB])];
console.log("Union:", union);

console.log("\n=== END OF ARRAY CONCEPTS ===");