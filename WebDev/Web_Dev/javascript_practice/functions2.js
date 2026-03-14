console.log("ES6 Destructring\n");
function getCoordinates(){
    return {x:50,y:75};
}
let {x,y} = getCoordinates();
console.log("X: ",x);
console.log("Y: ",y);

console.log("\nES6 Destructring\n");
var sumExpression = function(num1,num2){
    var total = num1+num2;
    return total;
};

console.log(sumExpression(5,10));

var sum = sumExpression(7,25);
console.log(sum);

let person1 = {
    name : "Pallavi",
    greet : function(){
        console.log("Normal funtion this.name: ",this.name);
    }
};
person1.greet();
