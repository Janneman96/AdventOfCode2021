var jankyMethod = document.querySelector('pre').innerText.split('\n')
jankyMethod.pop(); // remove newline after last command

var horizontalPosition = 0;
var depth = 0;

for (var i = 0; i < jankyMethod.length; i++) {
    var command = jankyMethod[i].split(" ")[0];
    var amount = parseInt(jankyMethod[i].split(" ")[1]);// eerst vergeten te parsen, daarna parseint ipv parseInt, daarna kreeg ik nog NaN omdat ik hier [0] had staan
    switch(command) {
        case "forward":
            horizontalPosition += amount;
            break;
        case "down":
            depth += amount;
            break;
        case "up":
            depth -= amount;
            break;
        default:
            console.error('unknown command ' + command)
    }
}

console.log(horizontalPosition * depth);

// part 2
var jankyMethod = document.querySelector('pre').innerText.split('\n')
jankyMethod.pop(); // remove newline after last command

var horizontalPosition = 0;
var depth = 0;
var aim = 0;

for (var i = 0; i < jankyMethod.length; i++) {
    var command = jankyMethod[i].split(" ")[0];
    var amount = parseInt(jankyMethod[i].split(" ")[1]);
    switch(command) {
        case "forward":
            horizontalPosition += amount;
            depth += aim * amount;
            break;
        case "down":
            aim += amount;
            break;
        case "up":
            aim -= amount;
            break;
        default:
            console.error('unknown command ' + command)
    }
}

console.log(horizontalPosition * depth);

// part 2 alternative

var items = document.querySelector('pre').innerText.split('\n')
items.pop(); // remove newline after last command

var horizontalPosition = 0
var depth = 0
var aim = 0

function forward() { horizontalPosition += amount; depth += aim * amount}
function up() { aim -= amount }
function down() { aim += amount }
for (var i = 0; i < items.length; i++) {
    var command = items[i].split(" ")[0];
    var amount = parseInt(items[i].split(" ")[1]);
    window[command](amount);
}

console.log(horizontalPosition * depth);

// part 2 janky alternative
var horizontalPosition = 0
var depth = 0
var aim = 0

function forward(amount) { horizontalPosition += amount; depth += aim * amount}
function up(amount) { aim -= amount }
function down(amount) { aim += amount }

var jankyMethod = document.querySelector('pre').innerText.split(' ').join('(').split('\n').join(');');
jankyMethod = '<script type="text/javascript">' + jankyMethod + "console.log(horizontalPosition * depth);</script>"
document.write(jankyMethod);