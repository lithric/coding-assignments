var control = document.getElementById("abba");
var line = document.createElement("span");
line.id = "line";
line.innerHTML = "|";
line.style.position = "absolute";

function abbra() {
    control.appendChild(line);
}

function cada() {
    line.hidden = line.hidden ? false:true;
}

abbra();
var lineBlink = setInterval(() => {cada()},500);

function moveLeft() {
    if (line.previousSibling.textContent.at(-1)) {
        line.after(line.previousSibling.textContent.at(-1));
        line.previousSibling.textContent = line.previousSibling.textContent.slice(0,-1);
    }
}