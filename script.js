var response = [];
var names = [];
Array.prototype.sample = function(){
    return this[Math.floor(Math.random()*this.length)];
  }
function* tenner() {
    enterName:
    for (let j=0; ; j++) {
        for (let i=0; names.length<3; i++) {
            yield prompt("enter name");
            names.push(response[0]);
            yield prompt(`your name is ${response[0]} correct?`);
            if (response[0] !== "yes") {
                alert("that name has been removed from the list")
                names.pop();
            }
        }

        yield prompt(`ok, you have entered the names ${names.slice(0,-1)},and ${names.slice(-1)} correct?`);

        if (response[0] !== "yes") {
            prompt("your joking")
            if (response[0] !== "yes") {
                alert("fine, re-entering names");
                names = [];
                continue enterName;
            }
        }
    }
}

function* one() {
    yield prompt(["enter a noun","enter a name","enter a job"].sample());
}

eh = one();
function onner(object = null) {
    response.unshift(object ? 1:eh.next().value);
    console.log(response[1])
    response[0] ? onner():1;
    return
}
function twoer(start = 0, len = 100) {
    var ranum = Math.round(start+Math.random()*len);
    console.log(ranum);
    var lis = Object.keys([...Array(len+1)]);
    lis.forEach((x,i) => lis[i]=x-(-start));
    function guess() {
        let i=0;
        while (num !== ranum) {
            lis.splice(lis.indexOf(num),1);
        }
    }
    var out = guess(lis[i]);
    console.log(lis);
}
twoer();