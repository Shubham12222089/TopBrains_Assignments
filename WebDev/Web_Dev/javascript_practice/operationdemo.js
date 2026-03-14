// let a=10;
// let b=5;
// let addition =a+b;
// let substraction=a-b;
// let mult = a*b;
// let division = a/b;
// console.log("Addition: ",addition);
// console.log("Substration: ",substraction);
// console.log("Multiplication: ",mult);
// console.log("Division: ",division);

// let radius = 4;
// console.log("Area of Circle is: ",Math.PI*radius**2);
// let num1=10;
// let num2=20;
// if(num1>num2){
//     console.log("num1 is Greater");
// }else if(num2>num1){
//     console.log("num2 is Greater");
// }
// else{
//     console.log("Both are Equal");
// }
var i = 1;
// while(i<10){
//     console.log("Hello");
//     i++;
// }
var person = {"name": "Clark", "surname": "Kent", "age":"36"};
for(var prop in person){
    console.log("<p>"+ prop+" - "+person[prop]+"</p>");
}