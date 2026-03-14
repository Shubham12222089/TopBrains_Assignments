// let fruits=["Orange","Ball","Apple","Dog"];
// fruits.sort();
// fruits.forEach(element => {
//     console.log(element);
// });

var nums = [10, 20, 30, 40, 50];
console.log("Original:", nums);
nums.sort();
nums.reverse();
console.log(nums);
nums.splice(1,2,200,300);
console.log(nums);

var matrix = [
    [1, 2, 3],
    [4, 5, 6],
    [7, 8, 9]
];

console.log("Matrix:");
for(var i =0;i<matrix.length;i++){
    for(var j =0;j<matrix[i].length;j++){
        console.log(`Matrix[${i}],[${j}] = ${matrix[i][j]}`);
    }
}
