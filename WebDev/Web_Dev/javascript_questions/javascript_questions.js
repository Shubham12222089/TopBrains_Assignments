//1)Sample program to print a welcome message
console.log("Welcome to JavaScript Programming!");


//2)program to read a number user and display it
var readline = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

readline.question("Enter a number: ", num => {
    console.log("You entered:", Number(num));
    readline.close();
});


//3)program to read a floating point number from user 
var readline = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

readline.question("Enter float value: ", num => {
    console.log("Value =", parseFloat(num));
    readline.close();
});



////4)program to read a string from user and display it on the screen 
var readline = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

readline.question("Enter text: ", str => {
    console.log("You entered:", str);
    readline.close();
});


//5)program to perform all arithmetic operations

var readline = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

readline.question("Enter first number: ", a => {
    readline.question("Enter second number: ", b => {

        a = Number(a);
        b = Number(b);

        console.log("Addition =", a + b);
        console.log("Subtraction =", a - b);
        console.log("Multiplication =", a * b);
        console.log("Division =", a / b);
        console.log("Modulus =", a % b);

        readline.close();
    });
});


//6) program to find the area of circle 
var readline = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

readline.question("Enter radius: ", r => {
    r = Number(r);
    let area = Math.PI * r * r;

    console.log("Area =", area);
    readline.close();
});


//7) program to find whether the given number is Even or Odd
var readline = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

readline.question("Enter number: ", num => {
    num = Number(num);

    if (num % 2 === 0)
        console.log("Even");
    else
        console.log("Odd");

    readline.close();
});



//8)program to find the greatest of 2 numbers

var readline = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

readline.question("Enter first number: ", a => {
    readline.question("Enter second number: ", b => {

        a = Number(a);
        b = Number(b);

        console.log("Greatest =", (a > b ? a : b));

        readline.close();
    });
});



//9) program to find whether a given number is positive ,negative or zero

var readline = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

readline.question("Enter number: ", num => {
    num = Number(num);

    if (num > 0)
        console.log("Positive");
    else if (num < 0)
        console.log("Negative");
    else
        console.log("Zero");

    readline.close();
});



//10) program to find the greatest of three numbers
var readline = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

readline.question("Enter first number: ", a => {
    readline.question("Enter second number: ", b => {
        readline.question("Enter third number: ", c => {

            a = Number(a);
            b = Number(b);
            c = Number(c);

            let greatest = Math.max(a, b, c);
            console.log("Greatest =", greatest);

            readline.close();
        });
    });
});



//10a) program to find the greatest of three numbers using nested if

var readline = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

readline.question("Enter first number: ", a => {
    readline.question("Enter second number: ", b => {
        readline.question("Enter third number: ", c => {

            a = Number(a);
            b = Number(b);
            c = Number(c);

            if (a > b) {
                if (a > c)
                    console.log("Greatest =", a);
                else
                    console.log("Greatest =", c);
            } else {
                if (b > c)
                    console.log("Greatest =", b);
                else
                    console.log("Greatest =", c);
            }

            readline.close();
        });
    });
});




//11) program to find the greatest of 3 numbers using conditional operator 
var readline = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

readline.question("Enter first number: ", a => {
    readline.question("Enter second number: ", b => {
        readline.question("Enter third number: ", c => {

            a = Number(a);
            b = Number(b);
            c = Number(c);

            let greatest = (a > b) ? ((a > c) ? a : c)
                                   : ((b > c) ? b : c);

            console.log("Greatest =", greatest);

            readline.close();
        });
    });
});




//12) program to read student num,name,marks and calculate  total and average and print result and division

var readline = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

readline.question("Enter student number: ", num => {
    readline.question("Enter name: ", name => {
        readline.question("Enter mark1: ", m1 => {
            readline.question("Enter mark2: ", m2 => {
                readline.question("Enter mark3: ", m3 => {

                    m1 = Number(m1);
                    m2 = Number(m2);
                    m3 = Number(m3);

                    let total = m1 + m2 + m3;
                    let avg = total / 3;

                    let division;
                    if (avg >= 60) division = "First";
                    else if (avg >= 50) division = "Second";
                    else if (avg >= 35) division = "Third";
                    else division = "Fail";

                    console.log("Total =", total);
                    console.log("Average =", avg);
                    console.log("Division =", division);

                    readline.close();
                });
            });
        });
    });
});



/*
13)program to read eno,ename,basic salary and calculate  
pf,hra,da,net salary and gross salary and print eno,ename,basic salary,
gross salary and net salary

pf= 12% of basic salary.
hra=20% of basic salary.
da= 15% of basic salary.
gross salary=pf+hra+da+basic salary;
net salary=gross salary - pf;

*/

var readline = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

readline.question("Enter employee number: ", eno => {
    readline.question("Enter employee name: ", ename => {
        readline.question("Enter basic salary: ", basic => {

            basic = Number(basic);

            let pf = basic * 0.12;
            let hra = basic * 0.20;
            let da = basic * 0.15;

            let gross = basic + pf + hra + da;
            let net = gross - pf;

            console.log("Employee No:", eno);
            console.log("Name:", ename);
            console.log("Basic Salary:", basic);
            console.log("Gross Salary:", gross);
            console.log("Net Salary:", net);

            readline.close();
        });
    });
});
