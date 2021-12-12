// var innerhtml = document.querySelector('pre').innerText;

// var rowsize = 0;
// var rows = 0;
// var buckets = [];

// for (var i = 0; i < innerhtml.length; i++) {
//     var character = innerhtml[i];
    
//     if (!rowsize) {
//         if (character != '\n') {
//             buckets[i] = parseInt(character)
//         } else {
//             rowsize = i + 1;
//         }
//     } else {
//         if (character != '\n') {
//             buckets[i % rowsize] += parseInt(character)
//         } else {
//             rows ++;
//         }
//     }
// }

// var gammaBits = [];
// var epsilonBits = [];
// for (var i = 0; i < buckets.length; i++) {
//     gammaBits.push(buckets[i] > rows/2);
//     epsilonBits.push(buckets[i] <= rows/2);//in the example, when the number of 1's and 0's was equal, it counted as an epsilon bit.
// }

// /**
//  * @param {string} bits
//  * @returns {number} number
//  */
// function toNumber(bits) {
//     var number = 0;
//     var factor = 1
//     for(var i = 0; i < bits.length; i++) {
//         number += (bits[bits.length - 1 - i]) * factor;
//         factor = factor * 2;
//     }
//     return number
// }

// console.log(gammaBits, toNumber(gammaBits));
// console.log(epsilonBits, toNumber(epsilonBits));

// console.log(toNumber(gammaBits) * toNumber(epsilonBits));

// Part 2
var rows = document.querySelector('pre').innerText.split('\n');

var oxigenBits = [];

function stripMinority(binaryNumbers, columnIndex) {
    if(binaryNumbers.length == 1 || columnIndex >= binaryNumbers[0].length) {
        return binaryNumbers;
    }

    var count = 0;
    for (var rowIndex = 0; rowIndex < binaryNumbers.length; rowIndex++) {
        count += parseInt(binaryNumbers[rowIndex][columnIndex]);
    }

    var oneIsSuperiorBit = count >= binaryNumbers.length/2;

    var majority = [];

    for (var rowIndex = 0; rowIndex < binaryNumbers.length; rowIndex++) {
        var bit = binaryNumbers[rowIndex][columnIndex];
        if (oneIsSuperiorBit && bit == "1" || !oneIsSuperiorBit && bit == "0") {
            majority.push(binaryNumbers[rowIndex]);
        }
    }
    

    return(stripMinority(majority, columnIndex + 1));
}




function stripMajority(binaryNumbers, columnIndex) {
    if(binaryNumbers.length == 1 || columnIndex >= binaryNumbers[0].length) {
        return binaryNumbers;
    }
    
    var count = 0;
    for (var rowIndex = 0; rowIndex < binaryNumbers.length; rowIndex++) {
        count += parseInt(binaryNumbers[rowIndex][columnIndex]);
    }
    
    var oneIsSuperiorBit = count >= binaryNumbers.length/2;
    
    var majority = [];
    
    for (var rowIndex = 0; rowIndex < binaryNumbers.length; rowIndex++) {
        var bit = binaryNumbers[rowIndex][columnIndex];
        if (oneIsSuperiorBit && bit == "0" || !oneIsSuperiorBit && bit == "1") {
            majority.push(binaryNumbers[rowIndex]);
        }
    }
    
    
    return(stripMajority(majority, columnIndex + 1));
}



/**
 * @param {string} bits
 * @returns {number} number
 */
function toNumber(bits) {
    var number = 0;
    var factor = 1
    for(var i = 0; i < bits.length; i++) {
        number += (bits[bits.length - 1 - i]) * factor;
        factor = factor * 2;
    }
    return number
}

console.log(stripMinority(rows, 0), toNumber(stripMinority(rows,0)[0]));
console.log(stripMajority(rows, 0), toNumber(stripMajority(rows,0)[0]));
console.log(toNumber(stripMinority(rows,0)[0]) * toNumber(stripMajority(rows,0)[0]));