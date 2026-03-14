// ============================================
// JAVASCRIPT STRINGS - COMPLETE GUIDE
// ============================================
// Strings are used to store and manipulate text
// Strings are immutable (cannot be changed, only replaced)

// ============================================
// 1. CREATING STRINGS
// ============================================

// Single quotes
var name1 = 'John';

// Double quotes
var name2 = "Jane";

// Template literals (backticks) - ES6
var name3 = `Jack`;

// String constructor (not recommended)
var name4 = new String("Jill");

console.log("=== Creating Strings ===");
console.log("Single quotes:", name1);
console.log("Double quotes:", name2);
console.log("Template literal:", name3);
console.log("String object:", name4);
console.log("Type of name1:", typeof name1);        // "string"
console.log("Type of name4:", typeof name4);        // "object"


// ============================================
// 2. STRING LENGTH
// ============================================
// .length gives the number of characters

console.log("\n=== String Length ===");
var message = "Hello World";
console.log("Message:", message);
console.log("Length:", message.length);  // 11 (includes space)


// ============================================
// 3. ACCESSING CHARACTERS
// ============================================
// Strings are like arrays - each character has an index
// Index starts from 0

console.log("\n=== Accessing Characters ===");
var word = "JavaScript";

// Method 1: Bracket notation
console.log("First character [0]:", word[0]);       // J
console.log("Last character [9]:", word[9]);        // t

// Method 2: charAt() method
console.log("charAt(0):", word.charAt(0));          // J
console.log("charAt(4):", word.charAt(4));          // S

// Method 3: at() method (supports negative index)
console.log("at(0):", word.at(0));                  // J
console.log("at(-1):", word.at(-1));                // t (last character)
console.log("at(-2):", word.at(-2));                // p (second last)

// charCodeAt() - returns ASCII/Unicode value
console.log("charCodeAt(0):", word.charCodeAt(0));  // 74 (ASCII of 'J')


// ============================================
// 4. TEMPLATE LITERALS (BACKTICKS)
// ============================================
// Allows:
// - String interpolation (embed variables)
// - Multi-line strings
// - Expression evaluation

console.log("\n=== Template Literals ===");

var firstName = "John";
var lastName = "Doe";
var age = 25;

// String concatenation (old way)
var oldWay = "Name: " + firstName + " " + lastName + ", Age: " + age;
console.log("Old way:", oldWay);

// Template literal (modern way)
var newWay = `Name: ${firstName} ${lastName}, Age: ${age}`;
console.log("New way:", newWay);

// Expression inside template literal
console.log(`2 + 3 = ${2 + 3}`);
console.log(`Age next year: ${age + 1}`);

// Multi-line string
var multiLine = `This is line 1
This is line 2
This is line 3`;
console.log("Multi-line:\n", multiLine);


// ============================================
// 5. ESCAPE CHARACTERS
// ============================================
// Special characters that need backslash (\)

console.log("\n=== Escape Characters ===");

// \' - Single quote
console.log('It\'s a sunny day');

// \" - Double quote
console.log("He said \"Hello\"");

// \\ - Backslash
console.log("Path: C:\\Users\\Documents");

// \n - New line
console.log("Line1\nLine2");

// \t - Tab
console.log("Name:\tJohn");

// \r - Carriage return
// \b - Backspace


// ============================================
// 6. CHANGING CASE
// ============================================

console.log("\n=== Changing Case ===");
var text = "Hello World";

// toUpperCase() - all uppercase
console.log("toUpperCase():", text.toUpperCase());  // HELLO WORLD

// toLowerCase() - all lowercase
console.log("toLowerCase():", text.toLowerCase());  // hello world

// Capitalize first letter (custom)
function capitalize(str) {
    return str.charAt(0).toUpperCase() + str.slice(1).toLowerCase();
}
console.log("capitalize('jOHN'):", capitalize("jOHN"));  // John


// ============================================
// 7. SEARCHING IN STRINGS
// ============================================

console.log("\n=== Searching in Strings ===");
var sentence = "The quick brown fox jumps over the lazy dog";

// indexOf() - first occurrence index (-1 if not found)
console.log("indexOf('the'):", sentence.indexOf("the"));      // 31
console.log("indexOf('The'):", sentence.indexOf("The"));      // 0
console.log("indexOf('cat'):", sentence.indexOf("cat"));      // -1

// lastIndexOf() - last occurrence index
console.log("lastIndexOf('o'):", sentence.lastIndexOf("o"));  // 41

// search() - like indexOf but supports regex
console.log("search('fox'):", sentence.search("fox"));        // 16
console.log("search(/FOX/i):", sentence.search(/FOX/i));      // 16 (case-insensitive)

// includes() - returns true/false
console.log("includes('quick'):", sentence.includes("quick")); // true
console.log("includes('slow'):", sentence.includes("slow"));   // false

// startsWith() - checks if starts with
console.log("startsWith('The'):", sentence.startsWith("The")); // true
console.log("startsWith('the'):", sentence.startsWith("the")); // false

// endsWith() - checks if ends with
console.log("endsWith('dog'):", sentence.endsWith("dog"));     // true
console.log("endsWith('cat'):", sentence.endsWith("cat"));     // false


// ============================================
// 8. EXTRACTING SUBSTRINGS
// ============================================

console.log("\n=== Extracting Substrings ===");
var str = "Hello, World!";

// slice(start, end) - extracts from start to end (end not included)
console.log("slice(0, 5):", str.slice(0, 5));       // Hello
console.log("slice(7):", str.slice(7));             // World!
console.log("slice(-6):", str.slice(-6));           // World! (from end)
console.log("slice(-6, -1):", str.slice(-6, -1));   // World

// substring(start, end) - similar to slice but no negative index
console.log("substring(0, 5):", str.substring(0, 5));   // Hello
console.log("substring(7):", str.substring(7));         // World!

// substr(start, length) - DEPRECATED, don't use
// console.log("substr(0, 5):", str.substr(0, 5));      // Hello


// ============================================
// 9. REPLACING CONTENT
// ============================================

console.log("\n=== Replacing Content ===");
var original = "Hello World, Hello Universe";

// replace() - replaces FIRST match only
console.log("replace('Hello', 'Hi'):", original.replace("Hello", "Hi"));
// Output: Hi World, Hello Universe

// replace() with regex and global flag - replaces ALL
console.log("replace(/Hello/g, 'Hi'):", original.replace(/Hello/g, "Hi"));
// Output: Hi World, Hi Universe

// Case-insensitive replace
var text2 = "HELLO world";
console.log("replace(/hello/i, 'Hi'):", text2.replace(/hello/i, "Hi"));
// Output: Hi world

// replaceAll() - replaces all occurrences (ES2021)
console.log("replaceAll('Hello', 'Hi'):", original.replaceAll("Hello", "Hi"));
// Output: Hi World, Hi Universe


// ============================================
// 10. SPLITTING & JOINING
// ============================================

console.log("\n=== Splitting & Joining ===");

// split() - converts string to array
var fruits = "Apple,Banana,Orange,Grape";
var fruitArray = fruits.split(",");
console.log("split(','):", fruitArray);
// Output: ['Apple', 'Banana', 'Orange', 'Grape']

// Split by space
var words = "Hello World JavaScript".split(" ");
console.log("split(' '):", words);
// Output: ['Hello', 'World', 'JavaScript']

// Split every character
var chars = "Hello".split("");
console.log("split(''):", chars);
// Output: ['H', 'e', 'l', 'l', 'o']

// Limit splits
var limited = "a-b-c-d-e".split("-", 3);
console.log("split('-', 3):", limited);
// Output: ['a', 'b', 'c']

// join() - converts array back to string
var arr = ["Apple", "Banana", "Orange"];
console.log("join():", arr.join());         // Apple,Banana,Orange
console.log("join(' - '):", arr.join(" - "));   // Apple - Banana - Orange
console.log("join(''):", arr.join(""));         // AppleBananaOrange


// ============================================
// 11. TRIMMING WHITESPACE
// ============================================

console.log("\n=== Trimming Whitespace ===");
var messy = "   Hello World   ";

// trim() - removes whitespace from both ends
console.log("Original: '" + messy + "'");
console.log("trim(): '" + messy.trim() + "'");

// trimStart() / trimLeft() - removes from beginning
console.log("trimStart(): '" + messy.trimStart() + "'");

// trimEnd() / trimRight() - removes from end
console.log("trimEnd(): '" + messy.trimEnd() + "'");


// ============================================
// 12. PADDING STRINGS
// ============================================

console.log("\n=== Padding Strings ===");
var num = "5";

// padStart(length, padString) - adds padding at start
console.log("padStart(3, '0'):", num.padStart(3, "0"));     // 005
console.log("padStart(5, '*'):", num.padStart(5, "*"));     // ****5

// padEnd(length, padString) - adds padding at end
console.log("padEnd(3, '0'):", num.padEnd(3, "0"));         // 500
console.log("padEnd(5, '-'):", num.padEnd(5, "-"));         // 5----

// Practical use: Format numbers
var price = "42";
console.log("Price: $" + price.padStart(6, " "));           // Price: $    42


// ============================================
// 13. REPEATING STRINGS
// ============================================

console.log("\n=== Repeating Strings ===");

// repeat(count) - repeats string n times
console.log("'Ha'.repeat(3):", "Ha".repeat(3));     // HaHaHa
console.log("'*'.repeat(10):", "*".repeat(10));     // **********
console.log("'-'.repeat(20):", "-".repeat(20));     // --------------------


// ============================================
// 14. COMPARING STRINGS
// ============================================

console.log("\n=== Comparing Strings ===");

// Strict equality
console.log("'hello' === 'hello':", "hello" === "hello");   // true
console.log("'Hello' === 'hello':", "Hello" === "hello");   // false

// localeCompare() - for sorting/comparison
// Returns: -1 (before), 0 (equal), 1 (after)
console.log("'a'.localeCompare('b'):", "a".localeCompare("b"));     // -1
console.log("'b'.localeCompare('a'):", "b".localeCompare("a"));     // 1
console.log("'a'.localeCompare('a'):", "a".localeCompare("a"));     // 0

// Case-insensitive comparison
var str1 = "HELLO";
var str2 = "hello";
console.log("Case-insensitive equal:", str1.toLowerCase() === str2.toLowerCase());


// ============================================
// 15. CONCATENATING STRINGS
// ============================================

console.log("\n=== Concatenating Strings ===");

// Method 1: + operator
var concat1 = "Hello" + " " + "World";
console.log("Using +:", concat1);

// Method 2: concat() method
var concat2 = "Hello".concat(" ", "World", "!");
console.log("Using concat():", concat2);

// Method 3: Template literals (best for readability)
var part1 = "Hello";
var part2 = "World";
console.log(`Using template: ${part1} ${part2}`);

// Method 4: Array join
var concat3 = ["Hello", "World"].join(" ");
console.log("Using join:", concat3);


// ============================================
// 16. STRING CONVERSION
// ============================================

console.log("\n=== String Conversion ===");

// Number to String
var number = 42;
console.log("String(42):", String(number));
console.log("(42).toString():", number.toString());
console.log("42 + '':", number + "");

// String to Number
var numStr = "42";
console.log("Number('42'):", Number(numStr));
console.log("parseInt('42'):", parseInt(numStr));
console.log("parseFloat('42.5'):", parseFloat("42.5"));
console.log("+'42':", +numStr);

// Boolean to String
console.log("String(true):", String(true));

// Array to String
console.log("String([1,2,3]):", String([1, 2, 3]));


// ============================================
// 17. USEFUL STRING PATTERNS
// ============================================

console.log("\n=== Useful Patterns ===");

// Reverse a string
var toReverse = "Hello";
var reversed = toReverse.split("").reverse().join("");
console.log("Reverse 'Hello':", reversed);  // olleH

// Count character occurrences
var countStr = "banana";
var count = countStr.split("a").length - 1;
console.log("Count 'a' in 'banana':", count);  // 3

// Remove all spaces
var spacey = "Hello World JavaScript";
var noSpaces = spacey.split(" ").join("");
console.log("Remove spaces:", noSpaces);  // HelloWorldJavaScript

// Or using replace with regex
console.log("Using regex:", spacey.replace(/\s/g, ""));

// Check if string is empty
var emptyStr = "";
console.log("Is empty:", emptyStr.length === 0);  // true
console.log("Is empty (truthy):", !emptyStr);     // true

// Truncate string with ellipsis
function truncate(str, maxLength) {
    if (str.length <= maxLength) return str;
    return str.slice(0, maxLength - 3) + "...";
}
console.log("Truncate:", truncate("Hello World JavaScript", 15));
// Output: Hello World ...

// Extract numbers from string
var mixedStr = "abc123def456";
var numbers = mixedStr.match(/\d+/g);
console.log("Extract numbers:", numbers);  // ['123', '456']

// Check if string contains only letters
var onlyLetters = "HelloWorld";
console.log("Only letters:", /^[a-zA-Z]+$/.test(onlyLetters));  // true

// Remove duplicate characters
var dupeStr = "aabbccdd";
var unique = [...new Set(dupeStr)].join("");
console.log("Remove duplicates:", unique);  // abcd

// Title Case (capitalize each word)
function titleCase(str) {
    return str
        .toLowerCase()
        .split(" ")
        .map(word => word.charAt(0).toUpperCase() + word.slice(1))
        .join(" ");
}
console.log("Title case:", titleCase("hello world javascript"));
// Output: Hello World Javascript

// Slug generator (URL-friendly)
function slugify(str) {
    return str
        .toLowerCase()
        .trim()
        .replace(/\s+/g, "-")
        .replace(/[^a-z0-9-]/g, "");
}
console.log("Slugify:", slugify("Hello World! How are you?"));
// Output: hello-world-how-are-you

// Check palindrome
function isPalindrome(str) {
    var cleaned = str.toLowerCase().replace(/[^a-z0-9]/g, "");
    return cleaned === cleaned.split("").reverse().join("");
}
console.log("'racecar' is palindrome:", isPalindrome("racecar"));  // true
console.log("'hello' is palindrome:", isPalindrome("hello"));      // false


// ============================================
// 18. REGULAR EXPRESSIONS WITH STRINGS
// ============================================

console.log("\n=== Regex with Strings ===");

var testStr = "The quick brown fox jumps over 2 lazy dogs in 2024";

// match() - returns array of matches
console.log("match(/o/g):", testStr.match(/o/g));        // all 'o' letters
console.log("match(/\\d+/g):", testStr.match(/\d+/g));   // all numbers

// matchAll() - returns iterator with detailed match info
var matches = [...testStr.matchAll(/o/g)];
console.log("matchAll() count:", matches.length);

// test() - returns true/false (regex method)
console.log("/fox/.test():", /fox/.test(testStr));       // true
console.log("/cat/.test():", /cat/.test(testStr));       // false

// Common regex patterns:
// /^abc/     - starts with 'abc'
// /abc$/     - ends with 'abc'
// /\d/       - any digit
// /\w/       - any word character (a-z, A-Z, 0-9, _)
// /\s/       - any whitespace
// /./        - any character except newline
// /a+/       - one or more 'a'
// /a*/       - zero or more 'a'
// /a?/       - zero or one 'a'
// /a{3}/     - exactly 3 'a's
// /a{2,4}/   - 2 to 4 'a's
// /[abc]/    - any of a, b, or c
// /[^abc]/   - NOT a, b, or c
// /i flag    - case insensitive
// /g flag    - global (find all)


// ============================================
// 19. STRING IMMUTABILITY
// ============================================

console.log("\n=== String Immutability ===");

// Strings cannot be changed, only replaced
var immutable = "Hello";
immutable[0] = "J";  // This does NOT work!
console.log("After attempting change:", immutable);  // Still "Hello"

// To change, create a new string
var mutable = "J" + immutable.slice(1);
console.log("New string:", mutable);  // "Jello"


// ============================================
// 20. SUMMARY CHEAT SHEET
// ============================================

console.log("\n=== QUICK REFERENCE ===");
console.log(`
STRING METHODS CHEAT SHEET:

LENGTH & ACCESS:
  str.length          - Get length
  str[0], str.at(-1)  - Access by index
  str.charAt(0)       - Get character at position

CASE:
  str.toUpperCase()   - ALL CAPS
  str.toLowerCase()   - all lowercase

SEARCH:
  str.indexOf('x')    - Find first position
  str.includes('x')   - Check if contains
  str.startsWith('x') - Check beginning
  str.endsWith('x')   - Check ending

EXTRACT:
  str.slice(0, 5)     - Get portion
  str.substring(0, 5) - Get portion (no negative)

MODIFY:
  str.replace(a, b)   - Replace first
  str.replaceAll(a,b) - Replace all
  str.trim()          - Remove whitespace
  str.padStart(n,'0') - Add padding

CONVERT:
  str.split(',')      - String to Array
  arr.join(',')       - Array to String

CREATE:
  'a'.repeat(3)       - Repeat string
  str.concat(a, b)    - Join strings
`);

console.log("\n=== END OF STRING CONCEPTS ===");
