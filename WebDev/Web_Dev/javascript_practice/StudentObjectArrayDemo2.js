console.log("Creating an object");

var person={
    name:"Peter",
    age:28,
    gender:"Male",
    displayName : function(){
        console.log("Inside Method -> Name: ", this.name);
    }
};

console.log("Person object",person);

console.log("Accessing object properties");

console.log("Name(dot notation: ",person.name);

console.log("Age(Bracket notation): ",person["age"]);
console.log("Book object example");

var book={
    "name":"Harry potter nook",
    "author":"J.K Rowling",
    "year":2000
};

console.log("Author",book.author);
console.log("Year", book["year"]);

console.log("Looping through object properties");

for(var key in person){
    console.log(key + " : "+ person[key]);
}

console.log("Calling object methods");

person.displayName();
person["displayName"]();
console.log("Complex onject example");

var student={
    name:"Saurabhj",
    age:20,
    skills:["Js","Ts","Cpp"],
    address:{
        city:"Bihar",
        country:"India"
    }
};

console.log("Student name:", student.name);
console.log("FIrst skill: ", student.skills[0]);
console.log("City: ", student.address.city);

var students = [
    {
        id:1,
        name:"Abc",
        age:20,
        grade:"A"
    },
    {
        id:2,
        name:"Mno",
        age:21,
        grade:"A+"
    },
    {
        id:3,
        name:"Xyz",
        age:26,
        grade:"B"
    }
]
console.log("Student Array Created Successfully");
console.log("Accessing First Element:");
console.log("Name : ",students[0].name);
console.log("Grade : ",students[0].grade);

console.log("\nLooping using for Loop\n");
for(let i=0;i<students.length;i++){
    console.log("Student: ",i+1);
    console.log("ID: ",students[i].id);
    console.log("Name: ",students[i].name);
    console.log("Age: ",students[i].age);
    console.log("Grade: ",students[i].grade);
}