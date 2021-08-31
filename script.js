function FindPrime(numb) {
    var i = 2;
    while(i <= Math.sqrt(numb)) {
        if (numb % i == 0) {
            return i;
        }
        i++
    }
    return "prime"
}

alert(FindPrime(prompt("enter a number")));