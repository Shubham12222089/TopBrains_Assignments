// ============================================
// JAVASCRIPT FUNCTIONS - COMPLETE GUIDE
// ============================================
// A function is a reusable block of code that performs a specific task
// Think of it like a recipe - define once, use many times!

// ============================================
// 1. FUNCTION DECLARATION (Most Common Way)
// ============================================
// Syntax: function functionName(parameters) { code }
// - "function" keyword starts the declaration
// - Give it a meaningful name
// - Parameters are inputs (optional)
// - Code inside { } runs when function is called

// Simple function with no parameters
function sayHello() {
    console.log("Hello, World!");
}

// Function with parameters (inputs)
function greet(name) {
    console.log("Hello, " + name + "!");
}

// Function with return value (output)
function add(a, b) {
    return a + b;  // "return" sends back a value
}

console.log("=== Function Declaration ===");
sayHello();                          // Call function
greet("John");                       // Pass argument
var sum = add(5, 3);                 // Store returned value
console.log("5 + 3 =", sum);


// ============================================
// 2. FUNCTION EXPRESSION
// ============================================
// Store a function in a variable
// The function can be named or anonymous (no name)

// Anonymous function (no name after "function")
var multiply = function(a, b) {
    return a * b;
};

// Named function expression
var divide = function divideNumbers(a, b) {
    return a / b;
};

console.log("\n=== Function Expression ===");
console.log("4 × 5 =", multiply(4, 5));
console.log("20 ÷ 4 =", divide(20, 4));


// ============================================
// 3. ARROW FUNCTIONS (ES6 - Modern Way)
// ============================================
// Shorter syntax using => (fat arrow)
// Great for simple, short functions

// Full syntax
var subtract = (a, b) => {
    return a - b;
};

// Short syntax (one line = implicit return)
var square = (x) => x * x;

// Single parameter (no parentheses needed)
var double = x => x * 2;

// No parameters (empty parentheses required)
var getPI = () => 3.14159;

// Multiple statements (need curly braces and return)
var calculate = (a, b) => {
    var sum = a + b;
    var product = a * b;
    return { sum, product };  // Return object
};

console.log("\n=== Arrow Functions ===");
console.log("10 - 3 =", subtract(10, 3));
console.log("5² =", square(5));
console.log("7 × 2 =", double(7));
console.log("PI =", getPI());
console.log("calc(4, 5):", calculate(4, 5));


// ============================================
// 4. PARAMETERS & ARGUMENTS
// ============================================
// Parameters = variables in function definition
// Arguments = actual values passed when calling

// Default parameters (ES6) - value if not provided
function greetUser(name = "Guest", greeting = "Hello") {
    console.log(greeting + ", " + name + "!");
}

console.log("\n=== Parameters & Arguments ===");
greetUser();                         // Uses defaults: Hello, Guest!
greetUser("Alice");                  // Hello, Alice!
greetUser("Bob", "Hi");              // Hi, Bob!

// Rest parameters (...) - collect remaining arguments into array
function sumAll(...numbers) {
    var total = 0;
    for (var num of numbers) {
        total += num;
    }
    return total;
}

console.log("Sum of 1,2,3,4,5:", sumAll(1, 2, 3, 4, 5));

// Combining regular and rest parameters
function introduce(greeting, ...names) {
    console.log(greeting + ": " + names.join(", "));
}
introduce("Welcome", "Alice", "Bob", "Charlie");


// ============================================
// 5. RETURN STATEMENT
// ============================================
// - Sends value back to caller
// - Stops function execution
// - Function without return gives "undefined"

function checkAge(age) {
    if (age < 0) {
        return "Invalid age";  // Early return
    }
    if (age >= 18) {
        return "Adult";
    }
    return "Minor";
}

// Return multiple values using object
function getPersonInfo(name, age) {
    return {
        name: name,
        age: age,
        isAdult: age >= 18
    };
}

// Return multiple values using array
function minMax(arr) {
    return [Math.min(...arr), Math.max(...arr)];
}

console.log("\n=== Return Statement ===");
console.log("Age 25:", checkAge(25));
console.log("Age 15:", checkAge(15));
console.log("Person:", getPersonInfo("John", 30));
console.log("Min/Max of [5,2,8,1]:", minMax([5, 2, 8, 1]));


// ============================================
// 6. SCOPE - Where Variables Are Accessible
// ============================================
// Global Scope: Accessible everywhere
// Local/Function Scope: Only inside function
// Block Scope: Only inside { } (let, const)

var globalVar = "I'm global";  // Global scope

function scopeExample() {
    var localVar = "I'm local";  // Function scope
    console.log(globalVar);       // Can access global
    console.log(localVar);        // Can access local
    
    if (true) {
        var functionScoped = "var is function scoped";
        let blockScoped = "let is block scoped";
        const alsoBlock = "const is block scoped";
    }
    
    console.log(functionScoped);  // Works! var ignores block
    // console.log(blockScoped);  // Error! let is block-scoped
}

console.log("\n=== Scope ===");
scopeExample();
console.log(globalVar);            // Works
// console.log(localVar);          // Error! Not accessible


// ============================================
// 7. HOISTING - Functions Move to Top
// ============================================
// Function declarations are "hoisted" (moved to top)
// You can call them before they appear in code

console.log("\n=== Hoisting ===");

// This works! Function declaration is hoisted
hoistedFunction();

function hoistedFunction() {
    console.log("I was called before my definition!");
}

// Function expressions are NOT fully hoisted
// var notHoisted = function() { };  // Only var is hoisted, not the function


// ============================================
// 8. CALLBACK FUNCTIONS
// ============================================
// A function passed as argument to another function
// Used for: events, async operations, array methods

// Basic callback example
function processData(data, callback) {
    console.log("Processing:", data);
    callback(data);  // Call the passed function
}

function displayResult(result) {
    console.log("Result:", result.toUpperCase());
}

console.log("\n=== Callback Functions ===");
processData("hello", displayResult);

// Anonymous callback
processData("world", function(data) {
    console.log("Anonymous callback:", data + "!");
});

// Arrow function callback
processData("javascript", (data) => {
    console.log("Arrow callback:", data.length + " characters");
});

// Array methods with callbacks
var numbers = [1, 2, 3, 4, 5];

// forEach - do something with each element
console.log("\nforEach:");
numbers.forEach(function(num, index) {
    console.log("  Index " + index + ": " + num);
});

// map - transform each element
var doubled = numbers.map(num => num * 2);
console.log("map (doubled):", doubled);

// filter - keep elements that pass test
var evens = numbers.filter(num => num % 2 === 0);
console.log("filter (evens):", evens);

// find - get first matching element
var firstBig = numbers.find(num => num > 3);
console.log("find (> 3):", firstBig);

// reduce - combine all into single value
var total = numbers.reduce((acc, num) => acc + num, 0);
console.log("reduce (sum):", total);


// ============================================
// 9. IMMEDIATELY INVOKED FUNCTION EXPRESSION (IIFE)
// ============================================
// Function that runs immediately when defined
// Used to create private scope, avoid global pollution

console.log("\n=== IIFE ===");

// Standard IIFE
(function() {
    var privateVar = "I'm private";
    console.log("IIFE executed!", privateVar);
})();

// IIFE with parameters
(function(name) {
    console.log("Hello from IIFE,", name + "!");
})("World");

// Arrow function IIFE
(() => {
    console.log("Arrow IIFE!");
})();

// Named IIFE (for debugging)
(function namedIIFE() {
    console.log("Named IIFE!");
})();


// ============================================
// 10. HIGHER-ORDER FUNCTIONS
// ============================================
// Functions that:
// 1. Take other functions as arguments, OR
// 2. Return functions

console.log("\n=== Higher-Order Functions ===");

// Function that returns a function
function createMultiplier(multiplier) {
    return function(number) {
        return number * multiplier;
    };
}

var triple = createMultiplier(3);
var quadruple = createMultiplier(4);

console.log("Triple 5:", triple(5));      // 15
console.log("Quadruple 5:", quadruple(5)); // 20

// Function that takes function as argument
function operate(a, b, operation) {
    return operation(a, b);
}

console.log("Add:", operate(10, 5, (x, y) => x + y));
console.log("Subtract:", operate(10, 5, (x, y) => x - y));
console.log("Multiply:", operate(10, 5, (x, y) => x * y));


// ============================================
// 11. CLOSURES
// ============================================
// A closure is a function that remembers variables
// from its outer scope even after outer function has finished
// Think: function + its surrounding state

console.log("\n=== Closures ===");

// Counter using closure
function createCounter() {
    var count = 0;  // Private variable
    
    return {
        increment: function() {
            count++;
            return count;
        },
        decrement: function() {
            count--;
            return count;
        },
        getCount: function() {
            return count;
        }
    };
}

var counter = createCounter();
console.log("Initial:", counter.getCount());    // 0
console.log("Increment:", counter.increment()); // 1
console.log("Increment:", counter.increment()); // 2
console.log("Decrement:", counter.decrement()); // 1

// Another counter - separate state!
var counter2 = createCounter();
console.log("Counter2:", counter2.getCount());  // 0 (independent)

// Practical closure: private data
function createBankAccount(initialBalance) {
    var balance = initialBalance;  // Private!
    
    return {
        deposit: function(amount) {
            if (amount > 0) {
                balance += amount;
                return "Deposited: $" + amount;
            }
        },
        withdraw: function(amount) {
            if (amount > 0 && amount <= balance) {
                balance -= amount;
                return "Withdrew: $" + amount;
            }
            return "Insufficient funds";
        },
        getBalance: function() {
            return "$" + balance;
        }
    };
}

var account = createBankAccount(100);
console.log("\nBank Account:");
console.log(account.deposit(50));
console.log(account.withdraw(30));
console.log("Balance:", account.getBalance());
// console.log(account.balance);  // undefined - it's private!


// ============================================
// 12. RECURSION
// ============================================
// A function that calls itself
// Must have: base case (when to stop) + recursive case

console.log("\n=== Recursion ===");

// Factorial: 5! = 5 × 4 × 3 × 2 × 1 = 120
function factorial(n) {
    // Base case: stop when n is 0 or 1
    if (n <= 1) {
        return 1;
    }
    // Recursive case: n × factorial of (n-1)
    return n * factorial(n - 1);
}

console.log("5! =", factorial(5));  // 120

// Countdown
function countdown(n) {
    if (n <= 0) {
        console.log("Done!");
        return;
    }
    console.log(n);
    countdown(n - 1);
}

console.log("\nCountdown:");
countdown(5);

// Sum of array using recursion
function sumArray(arr) {
    if (arr.length === 0) {
        return 0;
    }
    return arr[0] + sumArray(arr.slice(1));
}

console.log("Sum [1,2,3,4]:", sumArray([1, 2, 3, 4]));

// Fibonacci: 0, 1, 1, 2, 3, 5, 8, 13...
function fibonacci(n) {
    if (n <= 1) return n;
    return fibonacci(n - 1) + fibonacci(n - 2);
}

console.log("Fibonacci(7):", fibonacci(7));  // 13


// ============================================
// 13. THIS KEYWORD IN FUNCTIONS
// ============================================
// "this" refers to the object that called the function
// Changes based on HOW function is called

console.log("\n=== 'this' Keyword ===");

// In regular function: this = global object (or undefined in strict mode)
function showThis() {
    console.log("Regular function this:", typeof this);
}
showThis();

// In object method: this = the object
var person = {
    name: "John",
    greet: function() {
        console.log("Hello, I'm " + this.name);
    }
};
person.greet();  // this = person object

// Arrow functions DON'T have their own "this"
// They inherit from surrounding scope
var team = {
    name: "Developers",
    members: ["Alice", "Bob"],
    
    // Regular function - this works
    showTeam: function() {
        console.log("Team: " + this.name);
        
        // Arrow function inherits this from showTeam
        this.members.forEach(member => {
            console.log("  " + member + " from " + this.name);
        });
    }
};
team.showTeam();


// ============================================
// 14. FUNCTION BINDING (call, apply, bind)
// ============================================
// Manually set what "this" refers to

console.log("\n=== Call, Apply, Bind ===");

function introduce(greeting, punctuation) {
    console.log(greeting + ", I'm " + this.name + punctuation);
}

var user1 = { name: "Alice" };
var user2 = { name: "Bob" };

// call() - calls function with specified "this", args separately
introduce.call(user1, "Hello", "!");     // Hello, I'm Alice!
introduce.call(user2, "Hi", ".");        // Hi, I'm Bob.

// apply() - same as call, but args as array
introduce.apply(user1, ["Hey", "!!"]);   // Hey, I'm Alice!!

// bind() - returns NEW function with "this" permanently set
var aliceIntro = introduce.bind(user1);
aliceIntro("Greetings", "~");            // Greetings, I'm Alice~

// Practical use: borrowing methods
var nums = { numbers: [1, 2, 3, 4, 5] };
var result = Array.prototype.slice.call(nums.numbers, 1, 4);
console.log("Borrowed slice:", result);


// ============================================
// 15. GENERATOR FUNCTIONS
// ============================================
// Special functions that can pause and resume
// Use function* and yield keyword

console.log("\n=== Generator Functions ===");

function* countUp() {
    yield 1;
    yield 2;
    yield 3;
}

var gen = countUp();
console.log(gen.next());  // { value: 1, done: false }
console.log(gen.next());  // { value: 2, done: false }
console.log(gen.next());  // { value: 3, done: false }
console.log(gen.next());  // { value: undefined, done: true }

// Practical: ID generator
function* idGenerator() {
    var id = 1;
    while (true) {
        yield id++;
    }
}

var getId = idGenerator();
console.log("ID:", getId.next().value);  // 1
console.log("ID:", getId.next().value);  // 2
console.log("ID:", getId.next().value);  // 3

// Iterate over generator
function* range(start, end) {
    for (var i = start; i <= end; i++) {
        yield i;
    }
}

console.log("Range 1-5:", [...range(1, 5)]);


// ============================================
// 16. ASYNC FUNCTIONS (ES2017)
// ============================================
// Handle asynchronous operations cleanly
// async/await = cleaner alternative to Promises

console.log("\n=== Async Functions ===");

// Simulating async operation with Promise
function delay(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

// Async function
async function asyncExample() {
    console.log("Start");
    await delay(100);  // Wait for delay to complete
    console.log("After 100ms delay");
    return "Done!";
}

// Calling async function
asyncExample().then(result => console.log(result));

// Async with fetch-like pattern
async function fetchData(id) {
    // Simulate API call
    await delay(50);
    return { id: id, name: "Item " + id };
}

async function getMultipleData() {
    // Sequential (one after another)
    var data1 = await fetchData(1);
    var data2 = await fetchData(2);
    console.log("Sequential:", data1, data2);
    
    // Parallel (all at once - faster!)
    var [item1, item2, item3] = await Promise.all([
        fetchData(10),
        fetchData(20),
        fetchData(30)
    ]);
    console.log("Parallel:", item1, item2, item3);
}

getMultipleData();


// ============================================
// 17. FUNCTION PROPERTIES & METHODS
// ============================================
// Functions are objects with properties and methods!

console.log("\n=== Function Properties ===");

function exampleFunc(a, b, c) {
    return a + b + c;
}

// name - function name
console.log("Name:", exampleFunc.name);

// length - number of expected parameters
console.log("Parameters:", exampleFunc.length);

// toString() - returns function code as string
console.log("Code:", exampleFunc.toString());


// ============================================
// 18. PURE FUNCTIONS
// ============================================
// A pure function:
// 1. Same input always gives same output
// 2. No side effects (doesn't modify external state)

console.log("\n=== Pure Functions ===");

// PURE - depends only on input, no side effects
function pureAdd(a, b) {
    return a + b;
}

console.log(pureAdd(2, 3));  // Always 5
console.log(pureAdd(2, 3));  // Always 5

// IMPURE - modifies external state
var total = 0;
function impureAdd(value) {
    total += value;  // Side effect!
    return total;
}

console.log(impureAdd(5));   // 5
console.log(impureAdd(5));   // 10 (different result!)

// IMPURE - uses external state
var tax = 0.1;
function calculatePrice(price) {
    return price + (price * tax);  // Depends on external variable
}


// ============================================
// 19. FUNCTION COMPOSITION
// ============================================
// Combine simple functions to build complex operations

console.log("\n=== Function Composition ===");

// Small, focused functions
var addOne = x => x + 1;
var double = x => x * 2;
var square = x => x * x;

// Manual composition
var result = square(double(addOne(3)));  // ((3+1)*2)² = 64
console.log("Manual composition:", result);

// Compose function - right to left
function compose(...fns) {
    return function(x) {
        return fns.reduceRight((acc, fn) => fn(acc), x);
    };
}

var transform = compose(square, double, addOne);
console.log("Composed:", transform(3));  // 64

// Pipe function - left to right (more readable)
function pipe(...fns) {
    return function(x) {
        return fns.reduce((acc, fn) => fn(acc), x);
    };
}

var pipeline = pipe(addOne, double, square);
console.log("Piped:", pipeline(3));  // 64


// ============================================
// 20. CURRYING
// ============================================
// Transform function with multiple args into
// sequence of functions with single arg

console.log("\n=== Currying ===");

// Normal function
function normalAdd(a, b, c) {
    return a + b + c;
}

// Curried version
function curriedAdd(a) {
    return function(b) {
        return function(c) {
            return a + b + c;
        };
    };
}

console.log("Normal:", normalAdd(1, 2, 3));
console.log("Curried:", curriedAdd(1)(2)(3));

// Arrow function currying (shorter)
var curriedMultiply = a => b => c => a * b * c;
console.log("Curried multiply:", curriedMultiply(2)(3)(4));

// Practical: create specialized functions
var addTen = curriedAdd(10);
var addTenThenFive = addTen(5);
console.log("10 + 5 + 3 =", addTenThenFive(3));

// Generic curry helper
function curry(fn) {
    return function curried(...args) {
        if (args.length >= fn.length) {
            return fn.apply(this, args);
        }
        return function(...more) {
            return curried.apply(this, args.concat(more));
        };
    };
}

var curriedSum = curry((a, b, c) => a + b + c);
console.log("Auto curry:", curriedSum(1)(2)(3));
console.log("Auto curry:", curriedSum(1, 2)(3));
console.log("Auto curry:", curriedSum(1, 2, 3));


// ============================================
// 21. MEMOIZATION
// ============================================
// Cache function results for performance
// If same input, return cached result instead of recalculating

console.log("\n=== Memoization ===");

// Without memoization - slow for large numbers
function slowFibonacci(n) {
    if (n <= 1) return n;
    return slowFibonacci(n - 1) + slowFibonacci(n - 2);
}

// With memoization - fast!
function memoize(fn) {
    var cache = {};
    return function(...args) {
        var key = JSON.stringify(args);
        if (cache[key] === undefined) {
            cache[key] = fn.apply(this, args);
        }
        return cache[key];
    };
}

var fastFibonacci = memoize(function fib(n) {
    if (n <= 1) return n;
    return fastFibonacci(n - 1) + fastFibonacci(n - 2);
});

console.log("Fibonacci(10):", fastFibonacci(10));
console.log("Fibonacci(20):", fastFibonacci(20));


// ============================================
// SUMMARY - FUNCTION TYPES QUICK REFERENCE
// ============================================
console.log("\n=== QUICK REFERENCE ===");
console.log(`
FUNCTION TYPES:
  function name() {}       - Declaration (hoisted)
  var fn = function() {}   - Expression
  var fn = () => {}        - Arrow function

PARAMETERS:
  function(a, b) {}        - Regular parameters
  function(a = 1) {}       - Default parameter
  function(...args) {}     - Rest parameters

IMPORTANT CONCEPTS:
  return                   - Send value back
  callback                 - Function as argument
  closure                  - Function remembers scope
  recursion               - Function calls itself
  this                    - Context object

ADVANCED:
  IIFE         - (function(){})()  - Runs immediately
  Higher-order - Takes/returns functions
  async/await  - Handle async operations
  Generators   - function* with yield

FUNCTIONAL:
  Pure         - Same input = same output
  Currying     - Split args into chain
  Composition  - Combine functions
  Memoization  - Cache results
`);

console.log("\n=== END OF FUNCTION CONCEPTS ===");
