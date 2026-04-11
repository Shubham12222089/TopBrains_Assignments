
function calculate(operation) {
    let a = parseFloat(document.getElementById("num1").value);
    let b = parseFloat(document.getElementById("num2").value);
    let resultText = document.getElementById("result");

    if (isNaN(a) || isNaN(b)) {
        resultText.innerText = "Please enter both numbers";
        return;
    }

    let result;
    let symbol;

    switch (operation) {
        case "add":
            result = a + b;
            symbol = "+";
            break;
        case "subtract":
            result = a - b;
            symbol = "-";
            break;
        case "multiply":
            result = a * b;
            symbol = "×";
            break;
        case "divide":
            if (b === 0) {
                resultText.innerText = "Cannot divide by zero";
                return;
            }
            result = a / b;
            symbol = "÷";
            break;
    }

    resultText.innerText = `${a} ${symbol} ${b} = ${result}`;
}