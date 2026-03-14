var name ="John Doe";
var age =30;
var isStudent=true;
var hobbies = ['reading','travelling','coding'];
var address={
    street:"123 Main St",
    city:"Anytown",
    country:"Usa"
};

console.log("Name:"+ name);
console.log("Age: " + age);
console.log("Is Student: "+ isStudent);
console.log("Hoobies: "+ hobbies.join(", "));
console.log("Address: "+ address.street + ", " + address.city + ", "+ address.country);