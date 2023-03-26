/*let obj = {
    name: "Sasha",
    surname: "Ustych"
}

let respond = await fetch("https://localhost:44343/loginForm/loginGame/");
let commits = await respond.text();
let divv = document.createElement('div');
let divThere = document.querySelector('div');
div.innerHTML = commits;
divThere.append(divv);*/

//document.getElementById("sendBtn").addEventListener("click", send);

let but = document.getElementById('idBtn');
but.addEventListener("click", send);
async function send() {
    const response = await fetch("/loginForm/resFunc", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            name: document.getElementById("one").innerText,
            surname: document.getElementById("two").innerText
        })
    });
    let message = await response.json();
    document.getElementById("message").innerText = message.name;
}