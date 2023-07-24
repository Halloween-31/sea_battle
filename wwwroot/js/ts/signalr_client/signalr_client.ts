import * as signalR from "../../../lib/microsoft/signalr/dist/browser/signalr.js";
// в .js файлі коментуємо дану стрічку і вона буде брати дане значення з бібліотеки, яка
// записана в .html файлі
// дикий костиль

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .build();

document.querySelector(".btn").addEventListener("click", async (e) => {
    e.preventDefault();

    const textarea: HTMLTextAreaElement = document.querySelector('.form-container > textarea');
    const msg: string = textarea.value;

    const toEmail = getCookie("EnemyEmail");

    const urlParams = new URLSearchParams(window.location.search);
    const enemyEmail = urlParams.get('enemyEmail');        
    console.log(enemyEmail == toEmail)

    /*const user: User = await getMyName();*/
    
    connection.invoke("Send", msg, enemyEmail)
        .catch(function (err) {
            return console.error(err.toString());
        });
});

class User_time implements User {
    name: string;
    surname: string;
    time: string;
}

connection.on("Receive", function (message, user_time: User_time) {
    //console.log("Recieved message: " + message);
    const new_msg: HTMLDivElement = document.createElement('div');
    new_msg.classList.add("div-msg");
    new_msg.innerHTML =
        `<p class="name-time"><b>${user_time.name + " " + user_time.surname}: </b><span>${user_time.time}</span > </p>
        <p class="msg-text">${message}</p>`;

    const div: HTMLDivElement = document.querySelector('.form-container > div');
    div.append(new_msg);

    div.scrollTop = div.scrollHeight;
    const h: string = getComputedStyle(div).height;
    if (parseInt(h) > 250) {
        div.style.height = '250px';
    }    

    const textarea: HTMLTextAreaElement = document.querySelector('.form-container > textarea');
    textarea.value = '';
});

connection.start()
    .then(function () {
        console.log("SignalR started!");
    })
    .catch(function (err) {
        return console.error(err.toString());
    });

class User {
    name: string;
    surname: string;
}

async function getMyName() {
    try {
        const MyEmail = getCookie("MyEmail");

        const urlParams = new URLSearchParams(window.location.search);
        const myEmail = urlParams.get('myEmail');
        console.log(MyEmail == myEmail)

        const res = await fetch("/user/GetMyName", {
            method: "POST",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
            body: JSON.stringify(myEmail)
        });
        const user: User = await res.json();
        return user;
    }
    catch (e) {
        console.log(e);
    }
}

function getCookie(name) {
    let matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}